/*----------AutoComplete For ReportList----*/

var text = "", value = ""; //Sets text and value of AutoComplete

$(document).ready(function () {
    $('#txtReport').keyup(function () {
        var reportName = $('#txtReport').val();
        $("#txtReport").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Reports.aspx/SearchReportList",
                    data: "{'reportName':'" + reportName + "'}",
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('~')[1],
                                val: item.split('~')[0]
                            }
                        }));
                    },
                    error: function (XHR, textStatus, errorThrown) {
                        alert('web-service connection problem');
                    }
                });
            },
            select: function (event, ui) {
                text = ui.item.label;
                $(this).val(text);
                value = ui.item.val;
            },
            close: function (e, i) {
                setAutoCompleteValues();
            },

            //minimum length before call
            minLength: 3
        });
    });

    $('#txtReport').blur(function () {
        if ($('#hdnReportName').val() != $('#txtReport').val()) {
            $("#txtReport").val('');
            $('#hdnReportName').val('');
            $('#hdnReportId').val('');
        }
    });

    function setAutoCompleteValues() {
        if (value != "" && $('#txtReport').val() != "") {
            $('#hdnReportId').val(value.split('|')[0]);
            $('#hdnReportName').val(text);
        }
    }
});

/*----------------------------------Dynemically generate control for report---------------------------------------------------*/

var Report = function (dvReport, outerDvReport, btnShowReport, submitFunction) {
    this.filterType = {
        TEXTBOX: 1, //  TextBox 
        DROPDOWNBOX: 2, // Drop-downbox
        CHECKBOX: 3, // Checkbox
        DATE: 4, // Textbox with date control
        RADIOBUTTON: 5, // Radio Button 
        TEXTAREA: 6, //Text-area
        AUTOCOMPLETE: 7 // Autocomplete TextBox 
    }
    this.txtReport = $('#txtReport');
    this.btnSearch = $('#btnSearch');
    this.div = $('#' + dvReport);
    this.submitButtonId = btnShowReport;
    this.formId = outerDvReport;
    this.submitFunction = submitFunction;
    this.div.html('');
    this.reportValidator = null;
    this.txtReportData = $('#txtReport');
}

Report.prototype = {
    table: null,
    controls: [],
    init: function () {
        this.btnSearch.click(this.searchProcessControl);
        this.txtReportData.change(this.changeReport);
    },
    changeReport: function () {
        var _this = this.changeReport ? this : report;
        $('#dvReport').empty();
        $('#btnShowReport').removeClass('show');
        $('#btnShowReport').addClass('hide');
        $('#innerDvChart').empty();
        $('.paginate-report').empty();
    },
    searchProcessControl: function () {
        var _this = this.searchProcessControl ? this : report;
        var txtReport = _this.txtReport.val();
        var reportId = $('#hdnReportId').val();
        if (txtReport == "") {
            alert('Please select report form list');
        }
        else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Reports.aspx/GetReportData",
                data: "{'reportId':'" + reportId + "'}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    this.controls = [];
                    var autoCompleteParam = [];
                    var dynamicHTML = '<table id="tblReport" cell-padding="0" cell-spacing="0">';
                    report.div.html('');

                    for (var i = 0; i < result.length; i++) {
                        if (result[i].FilterTypeId) {
                            var control = {};
                            var classAttr = 'class="';
                            classAttr += result[i].IsRequired ? 'required ' : '';
                            classAttr += result[i].ValidationClass ? result[i].ValidationClass : '';
                            dynamicHTML += '<tr>';
                            switch (result[i].FilterTypeId) {
                                case report.filterType.DATE:
                                    dynamicHTML += '<td>';
                                    dynamicHTML += result[i].IsRequired ? '<span class="mandatory-field">*</span>' : '';
                                    dynamicHTML += '<label for="txt' + result[i].ParameterName + '">' + result[i].Caption + '</label>';
                                    dynamicHTML += '</td>';
                                    dynamicHTML += '<td colspan="3">';
                                    dynamicHTML += '<input type="text" id="txt' + result[i].ParameterName + '" ' + classAttr + '"/>';
                                    dynamicHTML += '</td>';

                                    control.Label = result[i].ParameterId;
                                    control.Id = "txt" + result[i].ParameterName;
                                    control.Type = "DATE";
                                    this.controls.push(control);
                                    break;

                                case report.filterType.AUTOCOMPLETE:
                                    dynamicHTML += '<td>';
                                    dynamicHTML += result[i].IsRequired ? '<span class="mandatory-field">*</span>' : '';
                                    dynamicHTML += '<label for="txt' + result[i].ParameterName + '">' + result[i].Caption + '</label>';
                                    dynamicHTML += '</td>';
                                    dynamicHTML += '<td colspan="3">';
                                    dynamicHTML += '<input type="text" id="txt' + result[i].ParameterName + '" ' + classAttr + '" />';
                                    dynamicHTML += '</td>';

                                    autoCompleteParam.push({ textBoxId: 'txt' + result[i].ParameterName, serviceURL: result[i].AjaxUrl, textProperty: result[i].PropertyKeyName, valueProperty: result[i].PropertyValueName, data: result[i].Data });
                                    control.Id = "hdn" + result[i].ParameterName;
                                    control.Type = "AUTOCOMPLETE";

                                    control.Label = result[i].ParameterId;
                                    this.controls.push(control);
                                    break;
                                case this.filterType.TEXTBOX:
                                    dynamicHTML += '<td>';
                                    dynamicHTML += result[i].IsRequired ? '<span class="mandatory-field">*</span>' : '';
                                    dynamicHTML += '<label for="txt' + result[i].ParameterName + '">' + result[i].Caption + '</label>';
                                    dynamicHTML += '</td>';
                                    dynamicHTML += '<td colspan="3">';
                                    dynamicHTML += '<input type="text" id="txt' + result[i].ParameterName + '" ' + classAttr + '" />';
                                    dynamicHTML += '</td>';

                                    control.Label = result[i].ParameterId;
                                    control.Id = "txt" + result[i].ParameterName;
                                    control.Type = "TEXTBOX";
                                    this.controls.push(control);
                                    break;
                                case this.filterType.DROPDOWN:
                                    dynamicHTML += '<td>';
                                    dynamicHTML += reports[i].IsRequired ? '<span class="mandatory-field">*</span>' : '';
                                    dynamicHTML += '<label for="' + reports[i].ParameterName + '">' + reports[i].Caption + '</label>';
                                    dynamicHTML += '</td>';
                                    dynamicHTML += '<td colspan="3">';
                                    dynamicHTML += '<select id="ddl' + reports[i].ParameterName + '" ' + classAttr + '">';
                                    dynamicHTML += '<option value="0">--Select--</option>';
                                    dynamicHTML += '</select>'
                                    dynamicHTML += '</td>';

                                    control.Label = reports[i].ParameterId;
                                    control.Id = "ddl" + reports[i].ParameterName;
                                    control.Type = "DROPDOWN";
                                    this.controls.push(control);

                                    break;
                                case this.filterType.TEXTAREA:
                                    dynamicHTML += '<td>';
                                    dynamicHTML += result[i].IsRequired ? '<span class="mandatory-field">*</span>' : '';
                                    dynamicHTML += '<label for="txtarea' + result[i].ParameterName + '">' + result[i].Caption + '</label>';
                                    dynamicHTML += '</td>';
                                    dynamicHTML += '<td colspan="3">';
                                    dynamicHTML += '<textarea rows="3" cols="30" id="txt' + result[i].ParameterName + '" ' + classAttr + '"></textarea>';
                                    dynamicHTML += '</td>';

                                    control.Label = result[i].ParameterId;
                                    control.Id = "txtArea" + result[i].ParameterName;
                                    control.Type = "TEXTAREA";
                                    this.controls.push(control);
                                    break;
                                default: alert('Invalid Filter Type');
                                    break;
                            }
                            dynamicHTML += "</tr>"
                        }
                    }
                    dynamicHTML += "</table>";
                    report.div.html(dynamicHTML);

                    /*for date-picker*/
                    $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy', showOn: 'button', changeMonth: true,
                        changeYear: true, yearRange: '1980:2030', buttonImageOnly: true, buttonImage: '../../Styles/Images/date_picker.png'
                    });

                    $(".datepicker").attr("readonly", true);
                    $(".datepicker").addClass('txtDisable');

                    // Initializing autocomplete textbox
                    for (var i = 0; i < autoCompleteParam.length; i++) {
                        var autoCompleteObj = autoCompleteParam[i];
                        report.initAutoComplete(autoCompleteObj['textBoxId'], autoCompleteObj['serviceURL'], autoCompleteObj['textProperty'], autoCompleteObj['valueProperty'], autoCompleteObj['data']);
                    }
                    $('#btnShowReport').removeClass('hide');
                    $('#btnShowReport').addClass('show');

                    // Initializing validation
                    report.initFormValidation();

                    // Initialize TabIndex
                    report.initTabIndex();
                },
                error: function (XHR, textStatus, errorThrown) {
                    alert('web-service connection problem');
                }
            });
        }
    },
    initFormValidation: function () {
        report.reportValidator = new Validator();
        var obj = {};
        var buttonObj = {};
        buttonObj[report.submitButtonId] = report.submitFunction;
        obj['formId'] = report.formId;
        obj['buttons'] = buttonObj;
        report.reportValidator.init(obj);
    },
    initAutoComplete: function (textBoxId, serviceURL, textProperty, valueProperty, data) {
        new Autocomplete(textBoxId, serviceURL, textProperty, valueProperty, data);
    },
    initTabIndex: function () {
        var controls = this.getControlList();
        var nextIndex = 1;
        for (var i = 0; i < controls.length; i++) {
            switch (controls[i].Type) {
                case 'DATE':
                    $('#' + controls[i].Id).attr('tabindex', nextIndex++);
                    break;
                case 'AUTOCOMPLETE':
                    $('#' + controls[i].Id.replace('hdn', 'txt')).attr('tabindex', nextIndex++);
                    break;
            }
        }
        $('#' + this.submitButtonId).attr('tabindex', nextIndex++);
    },
    getControlList: function () {
        return this.controls;
    },
    isFormValid: function () {
        return report.reportValidator.isValidForm();
    }
}

/*-------------------------Common AutoComplete for Report field --------------------*/
var Autocomplete = function (textBoxId, ajaxUrl, propertyKeyName, propertyValueName, data) {
    this.textBox = $('#' + textBoxId);
    this.ajaxURL = ajaxUrl;
    this.propertyKeyName = propertyKeyName;
    this.propertyValueName = propertyValueName;
    this.data = data;
    this.init();
}

Autocomplete.prototype = {
    previousText: null,
    previousValue: null,
    init: function () {
        var autoComplete = this;
        if (autoComplete.ajaxURL == null) {
            alert('Ajax URL not specified for autocomplete textbox ' + autoComplete.textBox[0].id.replace('txt', ''));
        }
        this.textBox.blur(autoComplete, this.blur);
        this.textBox.append('<input type="hidden" id="hdn' + autoComplete.textBox.attr('id').replace('txt', '') + '"/>');
        autoComplete.textBox.keyup(function () {
            var textValue = autoComplete.textBox.val();
            autoComplete.textBox.autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: autoComplete.ajaxURL,
                        dataType: "json",
                        type: "POST",
                        data: "{'dataValue':'" + autoComplete.data + "','name':'" + textValue + "'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (result) {
                            response($.map(eval(result.d), function (item) {
                                return {
                                    label: item.split('~')[1],
                                    val: item.split('~')[0]
                                }
                            }));
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            if ($.parseJSON(XMLHttpRequest.responseText).Message.length != 0) {
                                alert($.parseJSON(XMLHttpRequest.responseText).Message);
                            } else {
                                alert('Please contact system administrator');
                            }
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    $('#hdn' + autoComplete.textBox.attr('id').replace('txt', '')).val(ui.item.val);
                    autoComplete.previousText = ui.item.label;
                    autoComplete.previousValue = ui.item.val;
                }
            });
        });
    },
    blur: function (event) {
        var autoComplete = arguments[0].data;

        if (autoComplete.textBox.val() != autoComplete.previousText) {
            autoComplete.textBox.val('');
        }
        else if (autoComplete.previousText != null && autoComplete.textBox.val() != autoComplete.previousText) {
            $('#hdn' + autoComplete.textBox.attr('id').replace('txt', '')).val('');
            autoComplete.textBox.addClass('validation-error');
            autoComplete.textBox.attr('title', 'Please select value from suggestion list.');
        } else if (autoComplete.previousText != null && autoComplete.textBox.val() == autoComplete.previousText) {
            $('#hdn' + autoComplete.textBox.attr('id').replace('txt', '')).val(autoComplete.previousValue);
            autoComplete.textBox.removeClass('validation-error');
            autoComplete.textBox.attr('title', '');
        }
    }
}
/*--------------------Validation Control Part-------------------*/
var Validator = function () {
    this.formId = null;
    this.memberArguments = null;
    this.isFormValid = true;
}

Validator.prototype = {
    init: function (args) {
        if (arguments.length > 1) {
            if (arguments[0]) {
                this.formId = arguments[0];
            } else {
                alert('Please initialize validation engine with form id as a first argument.');
            }

            if (arguments[2]) {
                if (isFunction(arguments[2])) {
                    var eventArg = [this, arguments[2]];
                } else {
                    alert('Third argument should be function.');
                }

                if (typeof arguments[1] == 'String') {
                    $('#' + arguments[1]).click(eventArg, this.validate);
                } else {
                    alert('Second argument should be button id.');
                }

            } else if (arguments) {
                alert('Please supply button id as a second argument and its callback function as third argument.');
            }

        } else if (arguments.length == 1) {
            this.memberArguments = arguments;
            if (args.formId) {
                this.formId = args.formId
            } else {
                alert('Please initialize validation engine with form id.');
            }
            if (args.buttons) {
                for (var key in args.buttons) {
                    var eventArg = [this, args.buttons[key]];
                    $('#' + key).click(eventArg, this.validate);
                }

            } else {
                alert('Please initialize validation engine with button id, which will raise the event.');
            }

        } else if (arguments.length == 0) {
            throw 'No arguments were supplied for initializing validation engine';
        }

        this.bindNumeric();
        this.maskDate();
    },
    validateRequiredFields: function () {
        var isValid = true;
        var requiredFields = $('#' + this.formId + ' .required');
        if (requiredFields.length > 0) {
            var focusChanged = this.onFocusChange;
            $.each(requiredFields, function () {
                $(this).blur(this, focusChanged);
                if (this.value.length == 0) {
                    isValid = false;
                    $(this).addClass('validation-error');
                    $(this).attr('title', 'This field is required.');
                } else if (this.tagName == 'SELECT' && this.value == '0') {
                    isValid = false;
                    $(this).addClass('validation-error');
                    $(this).attr('title', 'Please select any value.');
                } else {
                    $(this).removeClass('validation-error');
                    $(this).removeAttr('title');
                }
            });
        }
        return isValid;
    },
    validateRegExFields: function () {
        var isValid = true;
        if (this.memberArguments.regExFields) {
            var regExFields = this.memberArguments.regExFields;
            for (var key in regExFields) {
                var textBox = $('#' + key);
                var pattern = regExFields[key]['pattern'];
                var errorMsg = regExFields[key]['errorMsg'];
                $('#' + key).blur(textBox, onRegExFieldFocusChange);
                var regEx = new RegExp(pattern);
                if (!regEx.test(textBox.value)) {
                    isValid = false;
                    $(this).addClass('validation-error');
                    $(this).attr('title', errorMsg);
                } else {
                    $(this).removeClass('validation-error');
                    $(this).removeAttr('title');
                }
            }
        }
        return isValid;
    },
    validateEmail: function () {
        var isValid = true;
        var emailFields = $('#' + this.formId + ' .email');
        if (emailFields.length > 0) {
            var focusChanged = this.onEmailFieldFocusChange;
            $.each(emailFields, function () {
                $(this).blur(this, focusChanged);
                if (this.value != '' && !emailPattern.test(this.value)) {
                    isValid = false;
                    $(this).addClass('validation-error');
                    $(this).attr('title', 'Please enter a valid e-mail address.');
                }
                else {
                    $(this).removeClass('validation-error');
                    $(this).removeAttr('title');
                }
            });
        }
        return isValid;
    },
    bindNumeric: function () {
        var numericFields = $('#' + this.formId + ' .numeric');
        if (numericFields.length > 0) {
            var focusChanged = this.onNumericFieldFocusChanged;
            $.each($('#' + this.formId + ' .numeric'), function () {
                $(this).blur(this, focusChanged);
            });

            $(numericFields).keydown(function (e) {
                var k = e.which;
                if ((k < 48 || k > 57) && (k < 96 || k > 105)) {
                    if (!isUserFriendlyChar(k, "Decimals")) {
                        e.preventDefault();
                        return false;
                    }
                }
            });
        }
    },
    maskDate: function () {
        var dateFields = $('#' + this.formId + ' .date');
        if (dateFields.length > 0) {
            $.each($('#' + this.formId + ' .date'), function () {
                $(this).datepicker({ dateFormat: 'dd/mm/yy' }).mask("99/99/9999", { placeholder: "_" });
                $('#ui-datepicker-div').css({ 'background-color': '#fff', 'padding': '5px' });
            });
        }
    },
    validateNumeric: function () {
        var isValid = true;
        var numericFields = $('#' + this.formId + ' .numeric');
        if (numericFields.length > 0) {
            $.each(numericFields, function () {
                if (!numericPattern.test(this.value)) {
                    isValid = false;
                    $(this).addClass('validation-error');
                    $(this).attr('title', 'Please enter a valid number.');
                }
                else if (this.value.length > 0) {
                    $(this).removeClass('validation-error');
                    $(this).removeAttr('title');
                }
            });
        }
        return isValid;
    },
    validateTelephoneNumber: function () {
        var isValid = true;
        var telephoneFields = $('#' + this.formId + ' .telephone');
        if (telephoneFields.length > 0) {
            $.each(telephoneFields, function () {
                if (!telephoneNumberPattern.test(this.value)) {
                    isValid = false;
                    $(this).addClass('validation-error');
                    $(this).attr('title', 'Please enter a valid telephone number.');
                }
                else if (this.value.length > 0) {
                    $(this).removeClass('validation-error');
                    $(this).removeAttr('title');
                }
            });
        }
        return isValid;
    },
    validate: function (eventArg) {
        var v1 = eventArg.data[0].validateRequiredFields();
        var v2 = eventArg.data[0].validateRegExFields();
        var v3 = eventArg.data[0].validateEmail();
        var v4 = eventArg.data[0].validateNumeric();
        var v5 = eventArg.data[0].validateTelephoneNumber();
        eventArg.data[0].isFormValid = v1 && v2 && v3 && v4 && v5;
        if (eventArg.data[0].isFormValid) {
            eventArg.data[1]();
        }
        return eventArg.data.isFormValid;
    },
    clear: function () {
        $('#' + this.formId + ' .required').removeClass('validation-error');
    },
    onFocusChange: function (evenArg) {
        if (evenArg.data.value.length == 0) {
            $(evenArg.data).addClass('validation-error');
            $(evenArg.data).attr('title', 'This field is required.');
        } else {
            $(evenArg.data).removeClass('validation-error');
            $(evenArg.data).removeAttr('title');
        }
    },
    onRegExFieldFocusChange: function (evenArg) {
        var textBoxId = evenArg.data.id;
        var pattern = this.memberArguments.regExFields[textBoxId]['pattern'];
        var errorMsg = this.memberArguments.regExFields[textBoxId]['errorMsg'];

        if (!regEx.test(evenArg.data.value)) {
            $(evenArg.data).addClass('validation-error');
            $(evenArg.data).attr('title', errorMsg);
        } else {
            $(evenArg.data).removeClass('validation-error');
            $(evenArg.data).removeAttr('title');
        }
    },
    onEmailFieldFocusChange: function (eventArg) {
        if (eventArg.data.value != '' && !emailPattern.test(eventArg.data.value)) {
            $(this).addClass('validation-error');
            $(this).attr('title', 'Please enter a valid e-mail address.');
        }
        else {
            $(this).removeClass('validation-error');
            $(this).removeAttr('title');
        }
    },
    onNumericFieldFocusChanged: function (eventArg) {
        if (!numericPattern.test(eventArg.data.value)) {
            $(this).addClass('validation-error');
            $(this).attr('title', 'Please enter a valid number.');
        }
        else {
            $(this).removeClass('validation-error');
            $(this).removeAttr('title');
        }
    }
};

String.prototype.toHTMLTable = function (title) {
    var jsonObj = jQuery.parseJSON(this.toString());
    var dynamicTable = '';
    if (jsonObj.columns && jsonObj.rows) {
        dynamicTable = '<div class="dvTitle">' + title + '</div><table class="tblReport">';
        // Making Header Rows (Columns)
        dynamicTable += '<thead><tr>';
        for (var i = 0; i < jsonObj.columns.length; i++) {
            if (i == 0) continue;
            dynamicTable += '<th>';
            dynamicTable += jsonObj.columns[i];
            dynamicTable += '</th>';
        }
        dynamicTable += '</tr></thead>';

        // Making Data Rows
        dynamicTable += '<tbody>';
        for (var i = 0; i < jsonObj.rows.length; i++) {
            dynamicTable += '<tr>';
            for (var j = 0; j < jsonObj.rows[i].length; j++) {
                if (j == 0) continue;
                dynamicTable += '<td>';
                dynamicTable += jsonObj.rows[i][j];
                dynamicTable += '</td>';
            }
            dynamicTable += '</tr>';
        }
        dynamicTable += '</tbody><table>';
        return dynamicTable;
    } else {
        alert('Supplid JSON string is not valid for table.');
    }
}