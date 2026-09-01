$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");
    HideFinYearList();
    HideFamilyList();

    FetchClientList();
   
});

$("#ddlClientList").change(function () {

    $("#divLedgerTable").hide();

});

function FetchClientList() {

    let formdata = {
        "LoginId": $('#LoggedInUID').val(),
        "clientcode": ""
    }

    let posturl = "/ClientPortal/Reports/PSPDSPEQUITYCLIENTDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            
            let length = data.length;

            if (length > 0) {
                $("#ddlClientList").append("");
                let html = "";
                $.each(data, function (index, value) {
                    if (value.account_code != 0) {
                        html = html + "<option value=" + value.account_code + ">" + value.client_name + "</option>";
                    }
                });
                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
                if (length > 10) {
                    $('#ddlClientList').select2({
                        minimumInputLength: 4,
                        maximumSelectionLength: 3,
                        dropdownAutoWidth: true,
                        width: 'auto',
                        pagination: {
                            more: true
                        }
                    });
                }
                else {
                    $('#ddlClientList').select2(
                        {
                            dropdownAutoWidth: true,
                            width: 'auto'
                        });
                }
            }
            else {
                $("#ddlClientList").attr("disabled", true);
                alert("No clients mapped under Equity Segment.")
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function ViewData() {
    let formdata = {
        "LoginId": $('#LoggedInUID').val(),
        "Client": $('#ddlClientList :selected').val()
    }

    let posturl = "/ClientPortal/Reports/psp_dsp_eqt_current_fy_ledger";

    $('#tableCurrFYEqLedger').DataTable().destroy();
    $('#divLedgerTable').hide();

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {

                let html = "";
                $("#tbodytableCurrFYEqLedger").html("");
                for (let i = 0; i < data.length; i++) {
                    html = html + "<tr>" +
                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(data[i].tr_date) + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + data[i].exchange + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].narration + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + data[i].document_no + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(data[i].debit_mount.toFixed(2)) + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(data[i].credit_mount.toFixed(2)) + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(data[i].running_balance.toFixed(2)) + "</p></td>" +
                        "</tr>";

                }
                $("#tbodytableCurrFYEqLedger").append(html);

                $('#tableCurrFYEqLedger').DataTable({
                    "order": [], //Disable Initial sort
                    "scrollX": true,
                    "pagingType": "full_numbers",
                    "order": [],
                    "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
                        //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
                        { "bSortable": false, "sWidth": "10%" },
                        { "bSortable": false, "sWidth": "5%"},
                        { "bSortable": false, "sWidth": "50%" },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false }
                    ],
                    "initComplete": function (settings, json) {

                        $('#divLedgerTable').show();
                        $('#tableCurrFYEqLedger').DataTable().columns.adjust().draw();
                    }
                });
            }
            else {
                alert("No Data");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}



//Eq PMS
$("#btnEMailCurrFYLedger").click(function () {

    let famId = "0"
    let client = $('#ddlClientList :selected').val()
    let LoginId = $("#LoggedInUID").val()
    let clientName = $('#ddlClientList :selected').val()

    let strPara = LoginId + "~" + client + "~" + clientName;

    PDFandExcelExport("#btnEMailCurrFYLedger", "28", LoginId, famId, "E", "PDF", strPara);

})

$("#btnCurrFYLedgerPdf").click(function () {

    let famId = "0"
    let client = $('#ddlClientList :selected').val()
    let LoginId = $("#LoggedInUID").val()
    let clientName = $('#ddlClientList :selected').val()

    let strPara = LoginId + "~" + client + "~" + clientName;

    let strDescrip = "Live Ledger Report for " + $('#ddlClientList :selected').text()

    PDFandExcelExport("#btnCurrFYLedgerPdf", "28", LoginId, famId, "X", "PDF", strPara, strDescrip);

})

$("#btnCurrFYLedgerExcel").click(function () {

    let famId = "0"
    let client = $('#ddlClientList :selected').val()
    let LoginId = $("#LoggedInUID").val()
    let clientName = $('#ddlClientList :selected').val()

    let strPara = LoginId + "~" + client + "~" + clientName;

    let strDescrip = "Live Ledger Report for " + $('#ddlClientList :selected').text()

    PDFandExcelExport("#btnCurrFYLedgerExcel", "28", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
})
//Eq PMS
