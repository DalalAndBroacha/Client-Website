$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    //FetchScriptList();
    $('#ddlScript').select2();

    $('#ddlScriptUpdate').select2({
        dropdownParent: $("#edit_report") //Used because of Modal Ref - https://github.com/select2/select2/issues/600#issuecomment-102857595
    });
    

    $("#tblReportsData").DataTable({
        "initComplete": function (settings, json) {

            $('#tblReportsData').DataTable().columns.adjust().draw();
        },
        // For more Info on DOM https://datatables.net/reference/option/dom
        // DOM example - https://stackoverflow.com/questions/39407881/pagination-at-top-and-bottom-with-datatables#:~:text=You%20can%20put%20pagination%20at,'p%3E%3E%22%2C%20%7D)%3B
        "dom": "<'row justify-content-between'<'col-sm-3'l><'col-sm-3'f><'col-sm-3'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "pageLength": globalDataGridPageLenght,
        "lengthMenu": [25, 50, 100],
        "pagingType": "full_numbers",
        "order": [],
        'autoWidth': false,
        "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
            //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
            { "sWidth": "10%" },
            { "sWidth": "25%" },
            { "sWidth": "3%" },
            { "sWidth": "3%" },
            {
                "sWidth": "3%",
                "mRender": function (data, type, full) {
                    return numberWithCommas(Number(data).toFixed(2));
                }
            },
            { "sWidth": "20%", "bSortable": false, "bSearchable": false },
            { "sWidth": "20%", "bSortable": false, "bSearchable": false }
        ]
    });

    $('#main_div').show();
});

//function startajaxrequestGlobal() {
//    $("#GlobalwaitIn").css("display", "block");
//}

//function completeajaxrequestGlobal() {
//    $("#GlobalwaitIn").css("display", "none");
//}


$("#ddlCatList").change(function () {

    const JsObj = JSON.parse(this.value);

    if (JsObj.stock_related_flag == "Y") {
        $('#stockRelated').show();
    }
    else {
        $('#stockRelated').hide();
    }

})

$("#ddlCatListUpdate").change(function () {

    const JsObj = JSON.parse(this.value);

    if (JsObj.stock_related_flag == "Y") {
        $('#stockRelatedUpdate').show();
    }
    else {
        $('#stockRelatedUpdate').hide();
    }

})

$("#btnUpload").on('click', function (event) {

    const catType = $("#stockRelated").is(":visible")

    if ($("#ddlScript").val() == "0" && catType) {
        alert("Enter Script")
        event.preventDefault();
    }
    else {
        $("#btnUpload").trigger("submit");
    }
})


$("#btnUploadUpdate").on('click', function (event) {

    const catType = $("#stockRelatedUpdate").is(":visible")

    if ($("#ddlScriptUpdate").val() == "0" && catType) {
        alert("Enter Script")
        event.preventDefault();
    }
    else {
        $("#btnUploadUpdate").trigger("submit");
    }
})


$(document).on("click", ".Edit_rec", function () {

        let abc = new Array();
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            abc.push($(this).text());
        });

        //Changes to display table will disrupt abc Array to Hidden Inputs bindings
        //console.log(abc) 

        let rec_id = $(this).parents('tr').find('input:hidden[name=dsp_id]').val()
        let scrip_id = $(this).parents('tr').find('input:hidden[name=scrip_id]').val()
        let web_dsp = $(this).parents('tr').find('input:hidden[name=dsp_web_flag]').val()



        $("#RepoTitleUpdate").val(abc[1]);
        $("#rec_id_Update").val(rec_id);
        $("#fileURL_Update").val(abc[5]);


        const theText = abc[2];

        if (web_dsp == "Y") {
            $('#chkDspWebUpdate').prop('checked', true); // Checks it
        }
        else {
            $('#chkDspWebUpdate').prop('checked', false); // Unchecks it
        }


        $("#ddlCatListUpdate").find('option:selected').removeAttr("selected");
        $("#ddlCatListUpdate option:contains(" + theText + ")").attr('selected', 'selected');
        $("#ddlCatListUpdate").trigger("change");

        $("#inptrgtPriceUpdate").val(parseFloat(abc[4].replace(/,/g, '')));
        $("#ddlRecoUpdate").val(abc[3]);

        $("#ddlScriptUpdate").val(scrip_id);
        $("#ddlScriptUpdate").trigger("change");

        $('#edit_report').modal({ backdrop: 'static', keyboard: false });
        


    

});

$(document).on("click", ".Del_rec", function () {
    // WCAG 3.3.4 Error Prevention — deleting data needs a confirmation step,
    // with a button that names the consequence. The original body runs
    // unchanged, with `this` preserved, once the user confirms.
    var a11yTrigger = this;
    var a11yAsk = (window.a11y && window.a11y.confirm)
        ? window.a11y.confirm({ title: 'Delete this research report?', message: 'The report will be removed from the list and from the website. This cannot be undone.', confirmLabel: 'Delete report' })
        : Promise.resolve(window.confirm('Delete this research report?'));
    a11yAsk.then(function (confirmed) {
        if (!confirmed) { return; }
        (function () {

            let abc = new Array();
            $(this).parents("tr").find("td:not(:last-child)").each(function () {
                abc.push($(this).text());
            });

            let rec_id = $(this).parents('tr').find('input:hidden[name=dsp_id]').val()

            let formdata = {
                "flag": "D",
                "rec_id": Number(rec_id),
                "repo_title": abc[1],
                "URL": abc[5],
                "category": abc[2]
            }

            let posturl = "/ClientPortal/Research/deleteReport";
            $.ajax({
                url: posturl,
                type: "post",
                contentType: "application/json",
                data: JSON.stringify(formdata),
                success: function (data) {
                    alert(data);

                    /*The reload() function takes an optional parameter that can be set to true to force
                    a reload from the server rather than the cache.The parameter defaults to false,
                    so by default the page may reload from the browser's cache.*/
                    location.reload(true);

                },
                error: function (xhr, err) {
                    alert(err)
                }
            })


        }).call(a11yTrigger);
    });
});

$(document).on("click", ".Push_Notification", function () {

    const targetBtn = $(this);
    let rec_id = $(this).parents('tr').find('input:hidden[name=dsp_id]').val()

    let posturl = "/ClientPortal/Research/pushMobileNotification";

    let formdata = {
        "rec_id": rec_id,
    }

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            alert(data.sql_msg);
            targetBtn.hide();

        },
        error: function (xhr, err) {
            alert(err)
        }
    })

});