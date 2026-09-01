$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    $('#ddlBHRMList').select2({ dropdownCssClass: 'bigdrop' });
    $('#ddlScripList').select2({ dropdownCssClass: 'bigdrop', maximumSelectionLength: 10 });
    $('#ddlDataList').select2({ dropdownCssClass: 'bigdrop' });

    if ($("#txtHidScripCode").val() != "0") {
        const scrip_code = $("#txtHidScripCode").val()
        const rm_id = $("#txtHidRmId").val()

        let Transpodata = {
            "LoginId": $("#LoggedInUID").val(),
            "rm_id": rm_id,
            "scrip_code": scrip_code,
            "data": "2"
        };

        if ($('#ddlScripList').find("option[value='" + scrip_code + "']").length) {
            
            $('#ddlScripList').val(scrip_code).trigger('change');
        }
        if ($('#ddlBHRMList').find("option[value='" + rm_id + "']").length) {
            $('#ddlBHRMList').val(rm_id).trigger('change');
        }
        
        $('#ddlDataList').val("2").trigger('change');

        ViewData(Transpodata);
    }
    
});

$("#ddlBHRMList").change(function () {
    $('#divDataTable').hide();
});
$("#ddlScripList").change(function () {
    $('#divDataTable').hide();
});
$("#ddlDataList").change(function () {
    $('#divDataTable').hide();
});
function completeajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "block");
}
function ViewData(paramObj = {}) {

    let parameterObj;

    if (Object.keys(paramObj).length === 0 && paramObj.constructor === Object) {
        parameterObj = {
            "LoginId": $("#LoggedInUID").val(),
            "rm_id": $("#ddlBHRMList").val(),
            "scrip_code": $("#ddlScripList").val().join(),
            "data": $("#ddlDataList").val()
        }
    }
    else {
        parameterObj = paramObj
    }

    const url = "/ClientPortal/Reports/psp_dsp_clientwise_holding_list";

    $.ajax({
        url: url,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(parameterObj),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {

            $('#tblData').DataTable().destroy();
            $("#tbodyData").html("");
            let html = "";

            if (data.length > 0) {
                $("#EqMisScripName").text(data[0].scrip_name);
                $("#EqMisScripCmp").text(data[0].mkt_rate);
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignLeft'>" + value.family_Name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + value.client_name + "</td>" +
                        "<td class='paraTextAlignCenter'>" + value.account_code + "</td>" +
                        "<td class='paraTextAlignLeft'>" + value.rm_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + value.scrip_name + "</td>" +                        
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_qty.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.purchase_rate.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.purchase_value.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_value.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.portfolio_value.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_percentage.toFixed(2)) + "</td>" +
                    "</tr>"
                });

                $("#tbodyData").append(html);

                DrawDataTable();
            }
            else {
                alert("No data found.");
            }
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function DrawDataTable() {

    $('#tblData').DataTable({
        fixedHeader: true,
        autoWidth: false,
        columnDefs: [{
            width: "15%",
            targets: 0
        },
        {
            width: "15%",
            targets: 1
        },
        {
            width: "10%",
            targets: 2
        },
        {
        width: "10%",
        targets: 3
        },
        {
            width: "10%",
            targets: 4
        },
        {
            searchable: false,
            width: "5%",
            targets: 5
        },
        {
            searchable: false,
            width: "10%",
            targets: 6
        },
        {
            searchable: false,
            width: "5%",
            targets: 7
        },
        {
            searchable: false,
            width: "5%",
            targets: 8
        },
        {
            searchable: false,
            width: "10%",
            targets: 9
        },
        {
            searchable: false,
            width: "5%",
            targets: 10
        }
        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries. "
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        order: [
            [0, 'asc'],
            [1, 'asc'],
            [4, 'asc']
        ],
        "initComplete": function (settings, json) {
            $('#tblData').DataTable().columns.adjust().draw();

            $('#btnEmail').show();
            $('#btnExcel').show();

            $('#divDataTable').show();
        }
    });
}
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()
    let scrip_code = $("#ddlScripList").val().join()
    let rm_id = $("#ddlBHRMList").val()
    let data = $("#ddlDataList").val()

    let strPara = LoginId + "~" + scrip_code + "~" + rm_id + "~" + data;

    let strDescrip = "Scrip Holding Client List of " + $("#EqMisScripName").text() + " for " + $('#ddlBHRMList :selected').text()

    PDFandExcelExport(evt_btn, "56", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

$("#btnEmail").click(function () { //Email
    GenerateExportDetails("#btnEmail", "E", "EXCEL")
})

$("#btnExcel").click(function () { //Excel
    GenerateExportDetails("#btnExcel", "X", "EXCEL")
})