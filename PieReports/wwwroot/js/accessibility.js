/* ==========================================================================
   accessibility.js — WCAG 2.1 Level AA remediation layer
   --------------------------------------------------------------------------
   Runtime companion to css/accessibility.css. Loaded last on every page.

   Everything here is additive and defensive: it never removes existing
   behaviour, and every DOM write is guarded so a page that already ships the
   correct markup is left alone.

   Public API
     window.a11y.announce(message, assertive)   // 4.1.3 Status Messages
     window.a11y.nameControl(el, name)          // 4.1.2 Name, Role, Value
   ========================================================================== */
(function (window, document) {
    'use strict';

    var a11y = window.a11y || {};
    window.a11y = a11y;

    /* ----------------------------------------------------------------------
       Small helpers
       ---------------------------------------------------------------------- */
    function $all(selector, root) {
        return Array.prototype.slice.call((root || document).querySelectorAll(selector));
    }

    function hasAccessibleName(el) {
        if (!el) { return false; }
        if (el.getAttribute('aria-label')) { return true; }
        if (el.getAttribute('aria-labelledby')) { return true; }
        var text = (el.textContent || '').replace(/\s+/g, ' ').trim();
        if (text) { return true; }
        // An <img alt> or <input value> inside also supplies a name.
        var img = el.querySelector('img[alt]:not([alt=""])');
        if (img) { return true; }
        if (el.tagName === 'INPUT' && el.value) { return true; }
        return false;
    }

    function titleCaseFromId(id) {
        if (!id) { return ''; }
        return id
            .replace(/^(btn|lnk|div|txt|ddl|img)/i, '')
            .replace(/[_\-]+/g, ' ')
            .replace(/([a-z0-9])([A-Z])/g, '$1 $2')
            .replace(/\s+/g, ' ')
            .trim();
    }

    /* ======================================================================
       4.1.3 Status Messages — live regions
       The regions are rendered empty in _Layout.cshtml on page load, because
       a screen reader only announces *changes* to a region that already
       exists. Injecting a populated region announces nothing.
       ====================================================================== */
    function ensureLiveRegions() {
        ['a11y-status', 'a11y-alert'].forEach(function (id) {
            if (document.getElementById(id)) { return; }
            var region = document.createElement('div');
            region.id = id;
            region.setAttribute('role', id === 'a11y-alert' ? 'alert' : 'status');
            region.setAttribute('aria-live', id === 'a11y-alert' ? 'assertive' : 'polite');
            region.setAttribute('aria-atomic', 'true');
            document.body.appendChild(region);
        });
    }

    var announceTimer = null;
    a11y.announce = function (message, assertive) {
        if (!message) { return; }
        ensureLiveRegions();
        var region = document.getElementById(assertive ? 'a11y-alert' : 'a11y-status');
        if (!region) { return; }
        // Clear first so an identical repeated message is still announced,
        // and debounce so rapid updates do not flood the synthesiser.
        window.clearTimeout(announceTimer);
        region.textContent = '';
        announceTimer = window.setTimeout(function () {
            region.textContent = message;
        }, 120);
    };

    a11y.nameControl = function (el, name) {
        if (!el || !name) { return; }
        if (hasAccessibleName(el)) { return; }
        el.setAttribute('aria-label', name);
    };

    /* ======================================================================
       2.4.1 Bypass Blocks — move real focus, not just the viewport
       Anchoring to #main-content scrolls the page but leaves focus at the top
       of the document, so the next Tab returns to the navigation. Setting
       tabindex="-1" and calling focus() fixes that.
       ====================================================================== */
    function wireSkipLink() {
        var link = document.querySelector('.a11y-skip-link');
        if (!link) { return; }
        link.addEventListener('click', function (event) {
            var id = (link.getAttribute('href') || '').replace(/^#/, '');
            // A handful of pages name their main region something else
            // (e.g. #DividendReport, kept because scripts depend on the id).
            var target = (id && document.getElementById(id))
                || document.querySelector('main, [role="main"]');
            if (!target) { return; }
            event.preventDefault();
            if (!target.hasAttribute('tabindex')) {
                target.setAttribute('tabindex', '-1');
            }
            target.focus();
            target.scrollIntoView();
        });
    }

    /* ======================================================================
       1.3.1 / 2.4.1 — guarantee a main landmark exists
       Post-login pages carry `.content-wrapper`; pre-login pages are wrapped
       in <main> directly in the view. This is the safety net for anything
       rendered dynamically.
       ====================================================================== */
    function ensureMainLandmark() {
        if (document.querySelector('main, [role="main"]')) { return; }
        var candidate = document.querySelector('.content-wrapper, .page-body, .main-body');
        if (!candidate) { return; }
        candidate.setAttribute('role', 'main');
        if (!candidate.id) { candidate.id = 'main-content'; }
    }

    /* ======================================================================
       4.1.2 Name, Role, Value — icon-only controls
       Across the portal, icon buttons carry only a `title`. `title` is a
       last-resort name source: it is unreliable on touch, ignored by some
       AT configurations, and never shown to voice-control users. Promote it
       to aria-label and hide the decorative glyph from the tree (1.1.1).
       ====================================================================== */
    function nameIconOnlyControls() {
        var selector = 'button, a[href], [role="button"], summary, input[type="button"], input[type="submit"]';
        $all(selector).forEach(function (el) {
            var icons = $all('i[class*="fa"], i[class*="ti-"], i[class*="icon"], span[class*="fa"]', el);

            // Decorative glyphs never belong in the accessibility tree.
            icons.forEach(function (icon) {
                if (!icon.getAttribute('aria-label') && !icon.textContent.trim()) {
                    icon.setAttribute('aria-hidden', 'true');
                }
            });

            if (hasAccessibleName(el)) { return; }

            var name = el.getAttribute('title')
                || (icons[0] && icons[0].getAttribute('title'))
                || (el.querySelector('[title]') && el.querySelector('[title]').getAttribute('title'))
                || titleCaseFromId(el.id);

            if (name) {
                el.setAttribute('aria-label', name);
                // Keep title for the sighted tooltip, but strip it from the
                // icon so it is not announced twice.
                icons.forEach(function (icon) { icon.removeAttribute('title'); });
            }
        });
    }

    /* ======================================================================
       1.1.1 Non-text Content — images
       An <img> with no alt attribute makes the screen reader read the file
       name. Every image needs the attribute; the only question is whether it
       should be empty.
       ====================================================================== */
    function fixImages() {
        $all('img:not([alt])').forEach(function (img) {
            var link = img.closest('a[href], button');
            if (link && !hasAccessibleName(link)) {
                // Functional image: name the control after its destination.
                var name = link.getAttribute('title') || titleCaseFromId(link.id);
                img.setAttribute('alt', name || 'Link');
            } else {
                // Decorative, or the parent control is already named.
                img.setAttribute('alt', '');
            }
        });

        // alt text that just repeats the file name is worse than none.
        $all('img[alt]').forEach(function (img) {
            var alt = (img.getAttribute('alt') || '').trim();
            if (/^(logo\.(png|svg|jpe?g)|image|img|picture|photo|icon)$/i.test(alt)) {
                var parentNamed = img.closest('a[href], button');
                img.setAttribute('alt', parentNamed && hasAccessibleName(parentNamed) ? '' : alt);
            }
        });
    }

    /* ======================================================================
       1.3.1 Info and Relationships — form labels
       Every control that collects input needs a persistent, programmatically
       associated label. A placeholder is not a label: it disappears on typing.
       ====================================================================== */
    var autofillTokens = [
        [/(^|[^a-z])(user\s*name|username|userid|user_id|login\s*id|loginid)/i, 'username'],
        [/(^|[^a-z])(current\s*password|password|passwd|pwd)/i, 'current-password'],
        [/(new\s*password|confirm\s*password|retype\s*password)/i, 'new-password'],
        [/(^|[^a-z])(e-?mail)/i, 'email'],
        [/(mobile|phone|contact\s*(no|number)|telephone)/i, 'tel'],
        [/(one\s*time\s*password|\botp\b)/i, 'one-time-code'],
        [/(first\s*name)/i, 'given-name'],
        [/(last\s*name|surname)/i, 'family-name'],
        [/(^|[^a-z])(full\s*name|client\s*name|holder\s*name)/i, 'name'],
        [/(date\s*of\s*birth|dob|birth\s*date)/i, 'bday'],
        [/(pin\s*code|pincode|postal\s*code|zip)/i, 'postal-code'],
        [/(^|[^a-z])(city|town)/i, 'address-level2'],
        [/(^|[^a-z])(state)/i, 'address-level1'],
        [/(^|[^a-z])(country)/i, 'country-name'],
        [/(address)/i, 'street-address']
    ];

    function labelTextFor(control) {
        var id = control.id;
        if (id) {
            var explicit = document.querySelector('label[for="' + (window.CSS && CSS.escape ? CSS.escape(id) : id) + '"]');
            if (explicit) { return explicit.textContent.replace(/\s+/g, ' ').trim(); }
        }
        var wrapping = control.closest('label');
        if (wrapping) { return wrapping.textContent.replace(/\s+/g, ' ').trim(); }
        return '';
    }

    function fixFormControls() {
        var controls = $all('input:not([type="hidden"]), select, textarea');

        controls.forEach(function (control) {
            var type = (control.getAttribute('type') || 'text').toLowerCase();
            if (type === 'submit' || type === 'button' || type === 'reset' || type === 'image') { return; }

            var visibleLabel = labelTextFor(control);
            var name = visibleLabel
                || control.getAttribute('aria-label')
                || (control.getAttribute('aria-labelledby') ? 'aria-labelledby' : '');

            /* --- 3.3.2 Labels or Instructions ---------------------------- */
            if (!name) {
                // Fall back to placeholder, then to the input-group prefix
                // label, then to a humanised id. A placeholder-derived
                // aria-label is a floor, not a ceiling — the view markup
                // supplies real <label> elements wherever one was missing.
                var group = control.closest('.input-group');
                var prefix = group && group.querySelector('.input-group-text, .input-group-prepend label');
                var derived = control.getAttribute('placeholder')
                    || (prefix && prefix.textContent.replace(/\s+/g, ' ').trim())
                    || control.getAttribute('title')
                    || titleCaseFromId(control.id || control.name);
                if (derived) {
                    control.setAttribute('aria-label', derived.replace(/^please\s+(enter|select)\s+(your\s+)?/i, '').trim() || derived);
                    name = derived;
                }
            }

            /* --- 1.3.5 Identify Input Purpose ---------------------------- */
            var existing = (control.getAttribute('autocomplete') || '').toLowerCase();
            if (!existing || existing === 'off' || existing === 'on') {
                var haystack = [name, control.id, control.name, control.getAttribute('placeholder')]
                    .filter(Boolean).join(' ');
                for (var i = 0; i < autofillTokens.length; i++) {
                    if (autofillTokens[i][0].test(haystack)) {
                        control.setAttribute('autocomplete', autofillTokens[i][1]);
                        break;
                    }
                }
            }

            /* --- 1.3.5 / usability: surface the right mobile keyboard ---- */
            if (type === 'text' && !control.getAttribute('inputmode')) {
                var idName = (control.id + ' ' + control.name).toLowerCase();
                if (/otp|pin|pincode|mobile|phone|amount|qty|quantity|code$/.test(idName)) {
                    control.setAttribute('inputmode', 'numeric');
                }
            }

            /* --- 3.3.2: state that a field is required, in text ---------- */
            if (control.hasAttribute('required') && !control.hasAttribute('aria-required')) {
                control.setAttribute('aria-required', 'true');
            }
        });

        /* --- 1.3.1: radio and checkbox groups need a group name ---------- */
        var seenGroups = {};
        $all('input[type="radio"][name]').forEach(function (radio) {
            var groupName = radio.name;
            if (seenGroups[groupName]) { return; }
            seenGroups[groupName] = true;
            var group = $all('input[type="radio"][name="' + groupName + '"]');
            if (group.length < 2) { return; }
            if (radio.closest('fieldset')) { return; }
            var container = radio.closest('ul, .form-group, .radio-inline, .row');
            if (!container || container.getAttribute('role') === 'radiogroup') { return; }
            container.setAttribute('role', 'radiogroup');
            if (!container.getAttribute('aria-label')) {
                container.setAttribute('aria-label', titleCaseFromId(groupName) || 'Options');
            }
        });

        /* --- 1.3.1: a bare radio/checkbox followed by <label> with no for -- */
        $all('input[type="radio"], input[type="checkbox"]').forEach(function (box) {
            if (!box.id) { return; }
            if (document.querySelector('label[for="' + box.id + '"]')) { return; }
            if (box.closest('label')) { return; }
            var sibling = box.nextElementSibling;
            while (sibling && sibling.tagName !== 'LABEL' && sibling.tagName !== 'INPUT') {
                sibling = sibling.nextElementSibling;
            }
            if (sibling && sibling.tagName === 'LABEL' && !sibling.getAttribute('for')) {
                sibling.setAttribute('for', box.id);
            }
        });
    }

    /* ======================================================================
       select2 replaces the native <select> with a div. Carry the label across
       so the replacement is still named (4.1.2), and keep it in sync.
       ====================================================================== */
    function fixSelect2() {
        if (!window.jQuery || !window.jQuery.fn || !window.jQuery.fn.select2) { return; }
        var $ = window.jQuery;

        function apply() {
            $all('select.select2-hidden-accessible').forEach(function (select) {
                var name = select.getAttribute('aria-label') || labelTextFor(select);
                var container = select.nextElementSibling;
                if (!container || !container.classList || !container.classList.contains('select2')) { return; }
                var rendered = container.querySelector('.select2-selection');
                if (!rendered) { return; }
                if (name && !rendered.getAttribute('aria-label')) {
                    rendered.setAttribute('aria-label', name);
                }
                // The search field inside the dropdown also needs a name.
                var search = container.querySelector('.select2-search__field');
                if (search && !search.getAttribute('aria-label')) {
                    search.setAttribute('aria-label', (name ? name + ': ' : '') + 'Search');
                }
            });
        }

        apply();
        $(document).on('select2:open select2:close', function () { window.setTimeout(apply, 0); });
    }

    /* ======================================================================
       1.3.1 Info and Relationships — data tables
       Header cells need `scope`; every table needs an accessible name.
       ====================================================================== */
    function fixTables() {
        $all('table').forEach(function (table) {
            // Layout tables must be removed from the accessibility tree.
            var hasHeaders = table.querySelector('th');
            if (!hasHeaders && !table.querySelector('caption')) {
                var cellCount = table.querySelectorAll('td').length;
                var rowCount = table.querySelectorAll('tr').length;
                if (rowCount <= 1 || cellCount <= 2) {
                    if (!table.getAttribute('role')) { table.setAttribute('role', 'presentation'); }
                    return;
                }
            }

            $all('th', table).forEach(function (th) {
                if (th.getAttribute('scope')) { return; }
                var inThead = !!th.closest('thead');
                var isRowHeader = !inThead && th.parentElement && th.parentElement.firstElementChild === th;
                th.setAttribute('scope', isRowHeader ? 'row' : 'col');
            });

            // 2.4.6 / 1.3.1: name the table. Prefer an existing caption, then
            // aria-label, then the nearest preceding heading.
            if (table.querySelector('caption')) { return; }
            if (table.getAttribute('aria-label') || table.getAttribute('aria-labelledby')) { return; }

            // The nearest heading that precedes the table *in the same region*.
            // Crossing a dialog boundary would name a page table after
            // whatever heading a modal happens to contain.
            var region = table.closest('.modal, main, [role="main"], section, .card') || document.body;
            var headings = $all('h1,h2,h3,h4,h5,h6,[role="heading"]', region).filter(function (h) {
                if ((h.closest('.modal, main, [role="main"], section, .card') || document.body) !== region) {
                    return false;
                }
                // Node.DOCUMENT_POSITION_FOLLOWING === 4: the table comes after h.
                return !!(h.compareDocumentPosition(table) & 4);
            });
            var heading = headings[headings.length - 1];

            var label = (heading && heading.textContent.replace(/\s+/g, ' ').trim())
                || titleCaseFromId(table.id);
            if (label) { table.setAttribute('aria-label', label); }
        });

        /* 1.4.10 Reflow — a data table is exempt from the no-horizontal-scroll
           rule only if the scrolling is contained. Most report tables here sit
           directly in the page, so at 320 CSS px the whole document scrolls
           sideways. Wrapping each one in its own scroll container keeps the
           page itself reflowed, and the container is focusable so a keyboard
           user can actually reach the scrollbar (2.1.1). */
        $all('table').forEach(function (table) {
            if (table.closest('.table-responsive, .dataTables_scrollBody, .a11y-table-scroll')) { return; }
            if (table.getAttribute('role') === 'presentation') { return; }
            var wrapper = document.createElement('div');
            wrapper.className = 'a11y-table-scroll';
            table.parentNode.insertBefore(wrapper, table);
            wrapper.appendChild(table);
        });

        /* Whichever container ends up holding the table, make it reachable. */
        $all('.table-responsive, .dataTables_scrollBody, .a11y-table-scroll').forEach(function (box) {
            if (box.hasAttribute('tabindex')) { return; }
            var table = box.querySelector('table');
            if (!table) { return; }
            box.setAttribute('tabindex', '0');
            box.setAttribute('role', 'region');
            var name = table.getAttribute('aria-label')
                || (table.querySelector('caption') && table.querySelector('caption').textContent.trim())
                || 'Data table';
            box.setAttribute('aria-label', name + ' (scrollable)');
        });
    }

    /* ======================================================================
       4.1.2 / 2.1.2 / 2.4.3 — modal dialogs
       Bootstrap 4 already traps focus, closes on Esc and restores focus to
       the trigger. What it does not do is supply the dialog role, the modal
       flag, an accessible name, or a name for the `&times;` close button.
       ====================================================================== */
    function fixModals() {
        $all('.modal').forEach(function (modal, index) {
            if (!modal.getAttribute('role')) { modal.setAttribute('role', 'dialog'); }
            modal.setAttribute('aria-modal', 'true');
            if (!modal.hasAttribute('tabindex')) { modal.setAttribute('tabindex', '-1'); }

            var title = modal.querySelector('.modal-title, .modal-titlecp, .modal-titlecu, h1, h2, h3, h4, h5, h6');
            if (title && !modal.getAttribute('aria-labelledby') && !modal.getAttribute('aria-label')) {
                if (!title.id) { title.id = (modal.id || 'a11y-modal-' + index) + '-title'; }
                modal.setAttribute('aria-labelledby', title.id);
            } else if (!title && !modal.getAttribute('aria-label')) {
                modal.setAttribute('aria-label', titleCaseFromId(modal.id) || 'Dialog');
            }

            $all('.close, [data-dismiss="modal"]', modal).forEach(function (btn) {
                // The × glyph must not be the accessible name.
                $all('span', btn).forEach(function (span) {
                    if (/^[\s×x]*$/i.test(span.textContent)) {
                        span.setAttribute('aria-hidden', 'true');
                    }
                });
                var visible = (btn.textContent || '').replace(/[\s×]/g, '');
                if (!visible && !btn.getAttribute('aria-label')) {
                    btn.setAttribute('aria-label', 'Close dialog');
                }
                if (btn.tagName === 'BUTTON' && !btn.getAttribute('type')) {
                    btn.setAttribute('type', 'button');
                }
            });

            /* 2.4.3 Focus Order — move focus into the dialog on open, and
               make sure at least one focusable control exists there. */
            if (window.jQuery) {
                /* 2.4.3 — focus must return to whatever opened the dialog.
                   Bootstrap only does this for dialogs opened by clicking a
                   [data-toggle="modal"] trigger; the portal opens most of its
                   dialogs from script (ReportIFrameLayout and friends), and
                   those drop focus back to the top of the document. */
                var opener = null;
                window.jQuery(modal).on('show.bs.modal', function () {
                    opener = document.activeElement;
                });
                window.jQuery(modal).on('hidden.bs.modal', function () {
                    if (opener && document.contains(opener) && opener !== document.body) {
                        try { opener.focus(); } catch (e) { /* no-op */ }
                    }
                    opener = null;
                });

                window.jQuery(modal).on('shown.bs.modal', function () {
                    var target = modal.querySelector('[autofocus]')
                        || modal.querySelector('.modal-title')
                        || modal.querySelector('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])')
                        || modal;
                    if (target === modal.querySelector('.modal-title') && !target.hasAttribute('tabindex')) {
                        target.setAttribute('tabindex', '-1');
                    }
                    try { target.focus(); } catch (e) { /* no-op */ }
                });
            }

            /* 2.1.2 No Keyboard Trap — Bootstrap honours Esc unless
               data-keyboard="false" was set. Never let that stand. */
            if (modal.getAttribute('data-keyboard') === 'false') {
                modal.setAttribute('data-keyboard', 'true');
            }
            modal.addEventListener('keydown', function (event) {
                if (event.key !== 'Escape' && event.keyCode !== 27) { return; }
                if (window.jQuery) { window.jQuery(modal).modal('hide'); }
            });
        });
    }

    /* ======================================================================
       4.1.2 — disclosure state on the sidebar treeview and the navbar
       dropdowns. AdminLTE toggles a class but never the ARIA attribute, so
       a screen reader is told the menu is collapsed while it is open.
       ====================================================================== */
    function fixDisclosures() {
        // Sidebar treeview
        $all('.nav-sidebar .nav-item').forEach(function (item) {
            var link = item.querySelector(':scope > .nav-link');
            var submenu = item.querySelector(':scope > .nav-treeview');
            if (!link || !submenu) { return; }
            if (!submenu.id) { submenu.id = 'a11y-submenu-' + Math.random().toString(36).slice(2, 9); }
            link.setAttribute('aria-controls', submenu.id);
            link.setAttribute('aria-expanded', item.classList.contains('menu-open') ? 'true' : 'false');
            // A menu toggle is not a navigation link.
            if (link.getAttribute('href') === '#' || !link.getAttribute('href')) {
                link.setAttribute('role', 'button');
                if (!link.hasAttribute('tabindex')) { link.setAttribute('tabindex', '0'); }
            }
        });

        // AdminLTE mutates the class; mirror it onto aria-expanded.
        if (window.MutationObserver) {
            var observer = new MutationObserver(function (records) {
                records.forEach(function (record) {
                    var item = record.target;
                    if (!item.classList || !item.classList.contains('nav-item')) { return; }
                    var link = item.querySelector(':scope > .nav-link');
                    if (!link || !link.hasAttribute('aria-expanded')) { return; }
                    link.setAttribute('aria-expanded', item.classList.contains('menu-open') ? 'true' : 'false');
                });
            });
            $all('.nav-sidebar .nav-item').forEach(function (item) {
                observer.observe(item, { attributes: true, attributeFilter: ['class'] });
            });
        }

        // The sidebar pushmenu toggle. AdminLTE records the collapsed state as
        // a class on <body>; mirror it so the toggle reports the truth.
        var pushmenu = document.querySelector('[data-widget="pushmenu"]');
        if (pushmenu && window.MutationObserver) {
            var syncPushmenu = function () {
                var collapsed = document.body.classList.contains('sidebar-collapse');
                pushmenu.setAttribute('aria-expanded', collapsed ? 'false' : 'true');
            };
            syncPushmenu();
            new MutationObserver(syncPushmenu)
                .observe(document.body, { attributes: true, attributeFilter: ['class'] });
        }

        // Navbar dropdowns: the markup ships aria-expanded="true" hard-coded.
        $all('[data-toggle="dropdown"]').forEach(function (toggle) {
            var parent = toggle.parentElement;
            var open = parent && parent.classList.contains('show');
            toggle.setAttribute('aria-expanded', open ? 'true' : 'false');
            toggle.setAttribute('aria-haspopup', 'true');
        });
        if (window.jQuery) {
            window.jQuery(document)
                .on('show.bs.dropdown', function (e) {
                    var t = e.target.querySelector('[data-toggle="dropdown"]');
                    if (t) { t.setAttribute('aria-expanded', 'true'); }
                })
                .on('hide.bs.dropdown', function (e) {
                    var t = e.target.querySelector('[data-toggle="dropdown"]');
                    if (t) { t.setAttribute('aria-expanded', 'false'); }
                });
        }
    }

    /* ======================================================================
       2.1.1 Keyboard — anything that acts like a button must behave like one
       `<a href="#">` and `<div onclick>` are not keyboard-operable in the way
       their role implies. Where the markup could not be changed to a real
       <button> (dynamically generated rows, third-party widgets), give them
       the role, tab stop and key handlers they are missing.
       ====================================================================== */
    function fixFauxButtons() {
        var clickable = $all('[onclick]').filter(function (el) {
            var tag = el.tagName;
            if (tag === 'BUTTON' || tag === 'INPUT' || tag === 'SELECT' || tag === 'TEXTAREA') { return false; }
            if (tag === 'A' && el.getAttribute('href') && !/^#!?$/.test(el.getAttribute('href'))
                && !/^javascript:/i.test(el.getAttribute('href'))) { return false; }
            return true;
        });

        clickable.forEach(function (el) {
            if (!el.getAttribute('role')) { el.setAttribute('role', 'button'); }
            if (!el.hasAttribute('tabindex')) { el.setAttribute('tabindex', '0'); }
            if (el.dataset.a11yKeys === 'true') { return; }
            el.dataset.a11yKeys = 'true';
            el.addEventListener('keydown', function (event) {
                var key = event.key;
                if (key !== 'Enter' && key !== ' ' && key !== 'Spacebar') { return; }
                // A real <a href> already activates on Enter — only add Space.
                if (el.tagName === 'A' && el.hasAttribute('href') && key === 'Enter') { return; }
                event.preventDefault();
                el.click();
            });
        });

        // Links whose only job is to open a dialog or run script are buttons.
        $all('a[href="#"], a[href="#!"], a[href^="javascript:"]').forEach(function (link) {
            if (link.getAttribute('role')) { return; }
            link.setAttribute('role', 'button');
        });
    }

    /* ======================================================================
       2.4.4 Link Purpose (In Context)
       A link reading "Read more", "Click here" or a bare URL is not
       self-explanatory in a links list. Extend the accessible name with the
       row / section context rather than replacing the visible text (2.5.3).
       ====================================================================== */
    var vagueLinkText = /^(click here|here|read more|more|details|view|download|link|more info|learn more)\.?$/i;

    function fixVagueLinks() {
        $all('a[href]').forEach(function (link) {
            var text = (link.textContent || '').replace(/\s+/g, ' ').trim();
            if (!text || !vagueLinkText.test(text)) { return; }
            if (link.getAttribute('aria-label')) { return; }

            var row = link.closest('tr');
            var context = '';
            if (row) {
                var firstCell = row.querySelector('th, td');
                if (firstCell && firstCell !== link.closest('td')) {
                    context = firstCell.textContent.replace(/\s+/g, ' ').trim();
                }
            }
            if (!context) {
                var card = link.closest('.card, section, article');
                var heading = card && card.querySelector('h1,h2,h3,h4,h5,h6');
                if (heading) { context = heading.textContent.replace(/\s+/g, ' ').trim(); }
            }
            if (!context) { return; }

            // Keep the visible string at the start of the name so voice
            // control still matches it (2.5.3 Label in Name).
            var extra = document.createElement('span');
            extra.className = 'a11y-visually-hidden';
            extra.textContent = ' — ' + context;
            link.appendChild(extra);
        });

        // 2.4.4: a link that opens a new window should say so.
        $all('a[target="_blank"]').forEach(function (link) {
            if (link.dataset.a11yNewWindow === 'true') { return; }
            if (/opens in a new/i.test(link.getAttribute('aria-label') || link.textContent || '')) { return; }
            link.dataset.a11yNewWindow = 'true';
            if (!link.getAttribute('rel')) { link.setAttribute('rel', 'noopener noreferrer'); }
            var note = document.createElement('span');
            note.className = 'a11y-visually-hidden';
            note.textContent = ' (opens in a new tab)';
            link.appendChild(note);
        });
    }

    /* ======================================================================
       3.3.1 Error Identification + 3.3.3 Error Suggestion
       Inline validation messages in the portal are plain elements that appear
       and disappear via display:none. Wire them to their field and to a live
       region so they are identified in text, associated programmatically, and
       announced.
       ====================================================================== */
    function wireValidationMessages() {
        var messageSelector = '.text-danger, .field-validation-error, .invalid-feedback, .validation-summary-errors';

        $all(messageSelector).forEach(function (message, index) {
            if (!message.id) { message.id = 'a11y-err-' + index; }

            // Associate with the field it describes, when we can find one.
            var forField = message.getAttribute('data-valmsg-for');
            var field = forField
                ? document.querySelector('[name="' + forField + '"]')
                : null;

            if (!field) {
                var group = message.closest('.form-group, .input-group, .col, .row');
                field = group && group.querySelector('input:not([type="hidden"]), select, textarea');
            }
            if (!field) { return; }

            var describedBy = (field.getAttribute('aria-describedby') || '').split(/\s+/).filter(Boolean);
            if (describedBy.indexOf(message.id) === -1) {
                describedBy.push(message.id);
                field.setAttribute('aria-describedby', describedBy.join(' '));
            }

            // aria-invalid is set only once validation has actually failed,
            // never on initial render.
            var visible = message.offsetParent !== null && (message.textContent || '').trim() !== '';
            if (visible) {
                field.setAttribute('aria-invalid', 'true');
            }
        });

        // Announce errors that appear after load.
        if (!window.MutationObserver) { return; }
        var observer = new MutationObserver(function (records) {
            records.forEach(function (record) {
                var el = record.target;
                if (!el.matches || !el.matches(messageSelector)) { return; }
                var text = (el.textContent || '').replace(/\s+/g, ' ').trim();
                var shown = el.offsetParent !== null;
                if (shown && text) { a11y.announce(text, true); }
            });
        });
        $all(messageSelector).forEach(function (el) {
            observer.observe(el, {
                attributes: true, attributeFilter: ['style', 'class'],
                childList: true, characterData: true, subtree: true
            });
        });
    }

    /* ======================================================================
       4.1.3 Status Messages — DataTables
       "Showing 1 to 10 of 248 entries" and "No matching records found" change
       silently after a search or a page change.
       ====================================================================== */
    function wireDataTablesAnnouncements() {
        if (!window.jQuery || !window.MutationObserver) { return; }
        var observer = new MutationObserver(function (records) {
            records.forEach(function (record) {
                var text = (record.target.textContent || '').replace(/\s+/g, ' ').trim();
                if (text) { a11y.announce(text); }
            });
        });
        $all('.dataTables_info').forEach(function (info) {
            info.setAttribute('role', 'status');
            info.setAttribute('aria-live', 'polite');
            observer.observe(info, { childList: true, characterData: true, subtree: true });
        });

        // The DataTables search box is an unlabelled input inside a <label>
        // whose text is "Search:" — that is fine, but the length <select> and
        // the pagination buttons need names.
        $all('.dataTables_paginate a.paginate_button').forEach(function (btn) {
            if (!btn.getAttribute('role')) { btn.setAttribute('role', 'button'); }
        });
    }

    /* ======================================================================
       2.1.1 Keyboard + 4.1.2 Name, Role, Value — bootstrap4-toggle switches
       css/accessibility.css puts the real checkbox back in the tab order (the
       plugin hides it with display:none). This supplies the rest of the
       contract: exactly one toggle per activation, an accessible name, and a
       tree that does not read out both switch captions as the name.
       ====================================================================== */
    function fixToggleSwitches() {
        var boxes = $all('.toggle > input[type="checkbox"], .toggle input[type="checkbox"]');
        if (!boxes.length) { return; }

        boxes.forEach(function (box) {
            var wrap = box.closest('.toggle');
            if (!wrap || wrap.dataset.a11yToggle === 'true') { return; }
            wrap.dataset.a11yToggle = 'true';

            // The plugin delegates click on div[data-toggle=toggle] and calls
            // toggle() there. Now that the checkbox sits on top and handles
            // the click natively, letting it bubble would toggle twice.
            box.addEventListener('click', function (event) { event.stopPropagation(); });

            // The wrapper is decoration around a real checkbox; role="button"
            // on it would announce a second, phantom control.
            wrap.removeAttribute('role');

            /* ---- accessible name ------------------------------------- */
            var caption = $all('.toggle-on, .toggle-off, .toggle-handle', wrap);
            var name = box.getAttribute('aria-label');

            if (!name) {
                // A real <label for> from the page — not the two the plugin
                // generates, which say "Yes" and "No" and would combine into
                // a nonsense name.
                var own = box.id
                    ? $all('label[for="' + (window.CSS && CSS.escape ? CSS.escape(box.id) : box.id) + '"]')
                        .filter(function (l) { return !l.classList.contains('toggle-on')
                                                   && !l.classList.contains('toggle-off'); })
                    : [];
                if (own.length) { name = own[0].textContent.replace(/\s+/g, ' ').trim(); }
            }

            if (!name) {
                // Inside a data table the column header is the label.
                var cell = box.closest('td, th');
                var row = cell && cell.parentElement;
                var table = cell && cell.closest('table');
                if (cell && row && table) {
                    var index = Array.prototype.indexOf.call(row.children, cell);
                    var head = table.querySelector('thead tr');
                    var th = head && head.children[index];
                    if (th) { name = th.textContent.replace(/\s+/g, ' ').trim(); }
                }
            }

            if (!name) {
                name = titleCaseFromId(box.id || box.name);
            }

            if (name) {
                box.setAttribute('aria-label', name);
                // Only now is it safe to take the plugin's captions out of the
                // tree — the control keeps a name without them.
                caption.forEach(function (c) { c.setAttribute('aria-hidden', 'true'); });
            }

            /* ---- 1.4.1 Use of Color / 1.3.1 -------------------------------
               A disabled switch is a read-only status indicator. Its state is
               shown only by the switch's colour and position, which a screen
               reader cannot see, so state it in text. */
            if (box.disabled) {
                var onText = box.getAttribute('data-on') || 'On';
                var offText = box.getAttribute('data-off') || 'Off';
                var status = document.createElement('span');
                status.className = 'a11y-visually-hidden';
                status.textContent = ': ' + (box.checked ? onText : offText);
                if (name) { box.setAttribute('aria-label', name + status.textContent); }
            }
        });
    }

    /* ======================================================================
       2.1.1 Keyboard — scrollable regions
       A container that scrolls but cannot be focused is unreachable for a
       keyboard-only user: there is no way to move the scrollbar. Report
       dialogs with long bodies hit this.
       ====================================================================== */
    function fixScrollableRegions() {
        var candidates = $all('.modal-body, .card-body, .table-responsive, [style*="overflow"]');
        candidates.forEach(function (el) {
            if (el.hasAttribute('tabindex')) { return; }
            var style = window.getComputedStyle(el);
            var scrollsY = /(auto|scroll)/.test(style.overflowY) && el.scrollHeight > el.clientHeight + 2;
            var scrollsX = /(auto|scroll)/.test(style.overflowX) && el.scrollWidth > el.clientWidth + 2;
            if (!scrollsY && !scrollsX) { return; }
            // If something inside is already focusable the user can reach the
            // content by tabbing, and the browser scrolls to it.
            if (el.querySelector('a[href], button, input, select, textarea, [tabindex]:not([tabindex="-1"])')) {
                return;
            }
            el.setAttribute('tabindex', '0');
            if (!el.getAttribute('role')) { el.setAttribute('role', 'region'); }
            if (!el.getAttribute('aria-label')) {
                var heading = el.querySelector('h1,h2,h3,h4,h5,h6,caption');
                el.setAttribute('aria-label',
                    (heading ? heading.textContent.replace(/\s+/g, ' ').trim() + ' — ' : '') + 'scrollable content');
            }
        });
    }

    /* ======================================================================
       4.1.2 Name, Role, Value / 2.1.1 Keyboard — tab widgets
       The ARIA Authoring Practices tab pattern, implemented on real
       <button role="tab"> elements. It replaces jQuery UI Tabs, which builds
       the pattern on the <li> and leaves a focusable <a href> inside each
       one — a nested interactive control that announces as a link and does
       nothing when activated.

       Markup contract (see Views/Login/Index.cshtml):
         <div data-a11y-tabs>
           <ul role="tablist" aria-label="...">
             <li role="presentation">
               <button role="tab" aria-selected aria-controls="panelId" tabindex>
           <div id="panelId" role="tabpanel" aria-labelledby="tabId" [hidden]>
       ====================================================================== */
    function wireTabs() {
        $all('[data-a11y-tabs]').forEach(function (group) {
            var tabs = $all('[role="tab"]', group);
            if (tabs.length < 2) { return; }

            function panelFor(tab) {
                return document.getElementById(tab.getAttribute('aria-controls'));
            }

            function activate(tab, moveFocus) {
                tabs.forEach(function (t) {
                    var selected = t === tab;
                    t.setAttribute('aria-selected', selected ? 'true' : 'false');
                    // Roving tabindex: one tab stop for the whole tablist, so
                    // Tab moves past the widget instead of through every tab.
                    t.setAttribute('tabindex', selected ? '0' : '-1');
                    var panel = panelFor(t);
                    if (panel) { panel.hidden = !selected; }
                });
                if (moveFocus) { tab.focus(); }
            }

            tabs.forEach(function (tab, index) {
                tab.addEventListener('click', function () { activate(tab, false); });

                tab.addEventListener('keydown', function (event) {
                    var next = null;
                    switch (event.key) {
                        case 'ArrowRight':
                        case 'ArrowDown':
                            next = tabs[(index + 1) % tabs.length];
                            break;
                        case 'ArrowLeft':
                        case 'ArrowUp':
                            next = tabs[(index - 1 + tabs.length) % tabs.length];
                            break;
                        case 'Home':
                            next = tabs[0];
                            break;
                        case 'End':
                            next = tabs[tabs.length - 1];
                            break;
                        default:
                            return;
                    }
                    event.preventDefault();
                    // Automatic activation. The APG allows it when switching
                    // panels is cheap, and it is here: the panels are already
                    // in the DOM.
                    activate(next, true);
                    next.click();
                });
            });

            // Reconcile the initial state with the markup, in case a panel was
            // rendered without `hidden`.
            var current = tabs.filter(function (t) {
                return t.getAttribute('aria-selected') === 'true';
            })[0] || tabs[0];
            activate(current, false);
        });
    }

    /* ======================================================================
       2.4.5 Multiple Ways — menu search
       The portal previously offered only one way to reach a page: the
       navigation menu. This filters that same menu, giving a second route.
       It is a plain text filter over the rendered menu, so it needs no
       server round-trip and stays correct as the menu changes per role.
       ====================================================================== */
    function wireNavSearch() {
        var input = document.getElementById('a11y-nav-search');
        var nav = document.getElementById('main-sidebar-nav');
        if (!input || !nav) { return; }

        var status = document.getElementById('a11y-nav-search-status');
        var topItems = $all(':scope > ul > li.nav-item', nav);

        function textOf(el) {
            return (el.textContent || '').replace(/\s+/g, ' ').trim().toLowerCase();
        }

        function filter() {
            var q = input.value.trim().toLowerCase();
            var matches = 0;

            topItems.forEach(function (item) {
                if (!q) {
                    item.hidden = false;
                    $all('.nav-treeview > li.nav-item', item).forEach(function (sub) { sub.hidden = false; });
                    return;
                }
                var subs = $all('.nav-treeview > li.nav-item', item);
                var anySub = false;
                subs.forEach(function (sub) {
                    var hit = textOf(sub).indexOf(q) !== -1;
                    sub.hidden = !hit;
                    if (hit) { anySub = true; matches += 1; }
                });
                var selfHit = textOf(item).indexOf(q) !== -1;
                item.hidden = !(selfHit || anySub);
                if (selfHit && !anySub) {
                    matches += 1;
                    subs.forEach(function (sub) { sub.hidden = false; });
                }
                // Open the branch so the matches are actually reachable.
                if (!item.hidden) { item.classList.add('menu-open'); }
            });

            if (!status) { return; }
            if (!q) {
                status.textContent = '';
            } else {
                status.textContent = matches === 1
                    ? '1 menu item matches ' + input.value.trim()
                    : matches + ' menu items match ' + input.value.trim();
            }
        }

        var debounce = null;
        input.addEventListener('input', function () {
            window.clearTimeout(debounce);
            debounce = window.setTimeout(filter, 200);
        });

        // 3.2.2 On Input — Enter must not submit anything or change context.
        input.addEventListener('keydown', function (event) {
            if (event.key === 'Enter') { event.preventDefault(); filter(); }
            if (event.key === 'Escape') { input.value = ''; filter(); }
        });
    }

    /* ======================================================================
       4.1.3 Status Messages — report loading spinners
       Every report page has a `display:none` spinner that jQuery shows while
       the AJAX call runs. A hidden live region announces nothing, and the
       region's *content* never changes — only its visibility — so the
       announcement is routed through the always-present #a11y-status region.
       ====================================================================== */
    function wireLoadingIndicators() {
        var indicators = $all('.a11y-loading-indicator');
        if (!indicators.length || !window.MutationObserver) { return; }

        indicators.forEach(function (el) {
            el.setAttribute('role', 'status');
            el.setAttribute('aria-live', 'polite');
            // 1.1.1 — the spinner image itself is decorative (alt=""); this
            // text is what a screen reader reads if it lands on the region.
            if (!el.querySelector('.a11y-visually-hidden')) {
                var label = document.createElement('span');
                label.className = 'a11y-visually-hidden';
                label.textContent = 'Loading';
                el.appendChild(label);
            }

            var wasVisible = el.offsetParent !== null;
            var observer = new MutationObserver(function () {
                var visible = el.offsetParent !== null;
                if (visible === wasVisible) { return; }
                wasVisible = visible;
                a11y.announce(visible ? 'Loading, please wait.' : 'Loading complete.');
            });
            observer.observe(el, { attributes: true, attributeFilter: ['style', 'class', 'hidden'] });
        });
    }

    /* ======================================================================
       2.2.2 Pause, Stop, Hide — the login notification ticker
       ====================================================================== */
    function wireMarqueePause() {
        var banner = document.getElementById('divNotification');
        var text = banner && banner.querySelector('.marq-text');
        if (!banner || !text) { return; }
        if (banner.querySelector('.a11y-marquee-toggle')) { return; }

        banner.setAttribute('role', 'region');
        banner.setAttribute('aria-label', 'Site notice');

        var button = document.createElement('button');
        button.type = 'button';
        button.className = 'btn btn-sm btn-light a11y-marquee-toggle';
        button.setAttribute('aria-pressed', 'false');
        button.textContent = 'Pause notice';
        button.style.margin = '.25rem';
        button.addEventListener('click', function () {
            var paused = banner.classList.toggle('a11y-marquee-paused');
            button.setAttribute('aria-pressed', paused ? 'true' : 'false');
            button.textContent = paused ? 'Resume notice' : 'Pause notice';
        });
        banner.appendChild(button);
    }

    /* ======================================================================
       2.2.1 Timing Adjustable — session expiry warning
       The server expires the session after 30 idle minutes with no notice.
       Warn a full minute out, in a real dialog that takes focus, and let the
       user extend without losing the page.
       ====================================================================== */
    function wireSessionTimeout() {
        var config = window.a11ySessionConfig;
        if (!config || !config.timeoutMinutes) { return; }

        var TIMEOUT_MS = config.timeoutMinutes * 60 * 1000;
        var WARN_BEFORE_MS = 60 * 1000;          // a full minute, not 20s
        var MAX_EXTENSIONS = 10;                 // at least 10, per 2.2.1
        var extensionsUsed = 0;
        var warnTimer = null;
        var expiryTimer = null;
        var countdownTimer = null;
        var lastFocused = null;

        var dialog = document.getElementById('a11y-timeout-dialog');
        if (!dialog) { return; }

        var countdownEl = dialog.querySelector('[data-a11y-countdown]');
        var extendBtn = dialog.querySelector('[data-a11y-extend]');
        var logoutBtn = dialog.querySelector('[data-a11y-logout]');
        var panel = dialog.querySelector('.a11y-timeout-dialog__panel');

        function closeDialog() {
            dialog.classList.remove('is-open');
            dialog.setAttribute('aria-hidden', 'true');
            window.clearInterval(countdownTimer);
            if (lastFocused && document.contains(lastFocused)) {
                try { lastFocused.focus(); } catch (e) { /* no-op */ }
            }
        }

        function openDialog() {
            lastFocused = document.activeElement;
            dialog.classList.add('is-open');
            dialog.setAttribute('aria-hidden', 'false');

            var remaining = Math.round(WARN_BEFORE_MS / 1000);
            function paint() {
                if (countdownEl) {
                    countdownEl.textContent = remaining + (remaining === 1 ? ' second' : ' seconds');
                }
                // Announce at coarse intervals only, never every second.
                if (remaining === 60 || remaining === 30 || remaining === 10) {
                    a11y.announce('Your session expires in ' + remaining + ' seconds.', true);
                }
                remaining -= 1;
                if (remaining < 0) { window.clearInterval(countdownTimer); }
            }
            paint();
            window.clearInterval(countdownTimer);
            countdownTimer = window.setInterval(paint, 1000);

            if (extendBtn) { try { extendBtn.focus(); } catch (e) { /* no-op */ } }
        }

        function scheduleTimers() {
            window.clearTimeout(warnTimer);
            window.clearTimeout(expiryTimer);
            warnTimer = window.setTimeout(openDialog, Math.max(TIMEOUT_MS - WARN_BEFORE_MS, 1000));
            expiryTimer = window.setTimeout(function () {
                if (config.logoutUrl) { window.location.href = config.logoutUrl; }
            }, TIMEOUT_MS);
        }

        function extendSession() {
            if (extensionsUsed >= MAX_EXTENSIONS) {
                a11y.announce('No further extensions are available. Please save your work and sign in again.', true);
                return;
            }
            extensionsUsed += 1;
            closeDialog();
            scheduleTimers();
            a11y.announce('Session extended.');
            if (config.keepAliveUrl) {
                var request = new XMLHttpRequest();
                request.open('GET', config.keepAliveUrl, true);
                request.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
                request.send();
            }
        }

        if (extendBtn) { extendBtn.addEventListener('click', extendSession); }
        if (logoutBtn && config.logoutUrl) {
            logoutBtn.addEventListener('click', function () { window.location.href = config.logoutUrl; });
        }

        // 2.1.2 No Keyboard Trap — Esc dismisses, and focus stays inside
        // while the dialog is open.
        dialog.addEventListener('keydown', function (event) {
            if (event.key === 'Escape') {
                event.preventDefault();
                extendSession();
                return;
            }
            if (event.key !== 'Tab') { return; }
            var focusable = $all('button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])', panel)
                .filter(function (el) { return el.offsetParent !== null; });
            if (!focusable.length) { return; }
            var first = focusable[0];
            var last = focusable[focusable.length - 1];
            if (event.shiftKey && document.activeElement === first) {
                event.preventDefault();
                last.focus();
            } else if (!event.shiftKey && document.activeElement === last) {
                event.preventDefault();
                first.focus();
            }
        });

        // Real user activity resets the clock without needing the dialog.
        var resetPending = false;
        ['click', 'keydown', 'submit'].forEach(function (evt) {
            document.addEventListener(evt, function () {
                if (dialog.classList.contains('is-open') || resetPending) { return; }
                resetPending = true;
                window.setTimeout(function () { resetPending = false; }, 30000);
                scheduleTimers();
            }, true);
        });

        scheduleTimers();
    }

    /* ======================================================================
       3.2.2 On Input — a <select> must not navigate on change
       ====================================================================== */
    function guardJumpMenus() {
        $all('select[onchange]').forEach(function (select) {
            var handler = select.getAttribute('onchange') || '';
            if (!/location\.(href|assign|replace)|window\.open|\.submit\(/i.test(handler)) { return; }
            // A jump menu that navigates before the user has finished
            // arrowing through the options is a 3.2.2 failure. Warn in text.
            if (select.getAttribute('aria-describedby')) { return; }
            var hint = document.createElement('p');
            hint.id = 'a11y-jump-hint-' + Math.random().toString(36).slice(2, 8);
            hint.className = 'a11y-visually-hidden';
            hint.textContent = 'Choosing an option from this list loads a new page.';
            select.parentNode.insertBefore(hint, select);
            select.setAttribute('aria-describedby', hint.id);
        });
    }

    /* ======================================================================
       3.2.1 On Focus — nothing may change context on focus alone.
       autofocus is acceptable only on a page whose sole purpose is that one
       field. Strip it everywhere else.
       ====================================================================== */
    function guardOnFocus() {
        var autofocused = $all('[autofocus]');
        if (autofocused.length > 1) {
            autofocused.slice(1).forEach(function (el) { el.removeAttribute('autofocus'); });
        }
    }

    /* ======================================================================
       Boot
       ====================================================================== */
    function init() {
        try { ensureLiveRegions(); } catch (e) { /* no-op */ }
        try { wireSkipLink(); } catch (e) { /* no-op */ }
        try { ensureMainLandmark(); } catch (e) { /* no-op */ }
        try { fixImages(); } catch (e) { /* no-op */ }
        try { nameIconOnlyControls(); } catch (e) { /* no-op */ }
        try { fixFormControls(); } catch (e) { /* no-op */ }
        try { fixSelect2(); } catch (e) { /* no-op */ }
        try { fixTables(); } catch (e) { /* no-op */ }
        try { fixModals(); } catch (e) { /* no-op */ }
        try { fixDisclosures(); } catch (e) { /* no-op */ }
        try { fixFauxButtons(); } catch (e) { /* no-op */ }
        try { fixVagueLinks(); } catch (e) { /* no-op */ }
        try { wireValidationMessages(); } catch (e) { /* no-op */ }
        try { wireDataTablesAnnouncements(); } catch (e) { /* no-op */ }
        try { wireLoadingIndicators(); } catch (e) { /* no-op */ }
        try { wireNavSearch(); } catch (e) { /* no-op */ }
        try { wireTabs(); } catch (e) { /* no-op */ }
        try { fixToggleSwitches(); } catch (e) { /* no-op */ }
        try { fixScrollableRegions(); } catch (e) { /* no-op */ }
        try { wireMarqueePause(); } catch (e) { /* no-op */ }
        try { guardJumpMenus(); } catch (e) { /* no-op */ }
        try { guardOnFocus(); } catch (e) { /* no-op */ }
        try { wireSessionTimeout(); } catch (e) { /* no-op */ }
    }

    /* Content injected later (AJAX report tables, DataTables redraws,
       dynamically built modals) needs the same treatment. */
    function reapply() {
        try { fixImages(); } catch (e) { /* no-op */ }
        try { nameIconOnlyControls(); } catch (e) { /* no-op */ }
        try { fixFormControls(); } catch (e) { /* no-op */ }
        try { fixTables(); } catch (e) { /* no-op */ }
        try { fixModals(); } catch (e) { /* no-op */ }
        try { fixFauxButtons(); } catch (e) { /* no-op */ }
        try { wireDataTablesAnnouncements(); } catch (e) { /* no-op */ }
        try { fixToggleSwitches(); } catch (e) { /* no-op */ }
        try { fixScrollableRegions(); } catch (e) { /* no-op */ }
    }

    a11y.refresh = reapply;

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

    // jQuery-driven pages finish rendering after DOMContentLoaded.
    if (window.jQuery) {
        window.jQuery(function () {
            // jQuery UI Tabs initialises in its own $(function(){}); run after it.
            window.setTimeout(reapply, 300);
        });
        window.jQuery(document).ajaxComplete(function () { window.setTimeout(reapply, 150); });
    }
})(window, document);
