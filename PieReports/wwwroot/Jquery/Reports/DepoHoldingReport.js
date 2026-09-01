$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");

    GetClientList();
    HideFinYearList();

    let date = (new Date()).toISOString().split('T')[0];
    $("#as_on_date").val(date);
    $("#as_on_date").attr("max", date);

});

function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}

$("#ddlFamilyList").change(function () {
    $("#main_data_div").hide();
    GetClientList();
});

$("#ddlClientList").change(function () {
    $("#main_data_div").hide();
});

function ViewDeopHoldingReport() {
    FetchData();
}

function GetClientList() {
    let formdata = {
        "family_id": $('#ddlFamilyList :selected').val()
    }

    let posturl = "/ClientPortal/Pie/psp_dsp_client_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlClientList").html("");
                let html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.main_client_guid + ">" + value.main_client_name + "</option>";
                });

                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
            }
            else {
                $("#ddlClientList").append("<option value=0>No Data</option>");
                $("#ddlClientList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}


function FetchData() {

    let formdata = {
        "LoginHashId": $("#LoggedInUserHash").val(),
        "FamilyId": $('#ddlFamilyList :selected').val(),
        "main_client_id": $('#ddlClientList :selected').val(),
        "as_on_date": $("#as_on_date").val()

        //"LoginId": $('#ddlFamilyList :selected').val(),
        //"client_id": $('#ddlClientList :selected').val(),
        //"summary_flag": 'Y'
    }

    let posturl = "/ClientPortal/Reports/psp_rpt_cdsl_holding_report_familyhead";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestGlobal();
        },
        complete: function (xhr) {
            completeajaxrequestGlobal();
        },
        success: function (data) {

            if (data.length > 0) {
                $("#btnDepoHoldingPdf").show();
                $("#btnDepoHoldingExcel").show();
                $("#btnEMailDepoHolding").show();

                $("#tbl_depo_holding").html("");
                let html = "";
                let groups = {};

                console.log(data);

                for (let i = 0; i < data.length; i++) {
                    let groupName = data[i].bo_id;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                $.each(groups, function (index, value1) {

                    html = html + "<tr class='SubTotalRow'><td>" +
                        "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#acc_" + value1[0].bo_id + "'" +
                        "aria-expanded='false' aria-controls='acc_" + value1[0].bo_id + "'>" +
                        "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                        "</button>&nbsp;" + value1[0].bo_id + " | Valuation: " + numberWithCommas(value1[0].valuation.toFixed(2)) + "</td></tr>"
                    html = html + "<tr><td>" +
                        "<div class='collapse' id='acc_" + value1[0].bo_id + "' data-parent='#main_data_div'>" +
                        "<div class='global_report_accords'>" + //For setting max height
                        "<table id='trds_Table_" + value1[0].bo_id + "' class='table table-hover w-100'>" +
                        //"<colgroup><col style='width:10%;'><col style='width:25%;'><col style='width:25%;'><col style='width:10%;'><col style='width:10%;'>" +
                        //"<col style='width:10%;'><col style='width:15%;'></colgroup>" +
                        "<thead class='table_sticky_heads'><tr><th>ISIN</th><th>ISIN Name</th><th>Free</th><th>Current</th><th>Demat</th>" +
                        "<th>Remat</th><th>Freeze</th><th>Locked</th><th>Pledged</th><th>Total</th><th>Rate Date</th><th>Rate</th><th>Valuation</th></tr></thead><tbody>"
                    $.each(value1, function (index1, value) {
                        if (value.display_order == "1") {
                            html = html + "<tr>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.isin + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.isiN_Name + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.free.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.current.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.demat.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.remat.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.freeze.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.locked.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.pledge.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.cdsl_holding.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.rate_date) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.rate.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.valuation.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        }
                    })
                    html = html + "</tbody></table>" +
                        "</div>" + //global_report_accords end
                        "</div>" + //Collapse end
                        "</td></tr>"
                });

                $("#tbl_depo_holding").append(html);
                $("#main_data_div").show();
            }
            else {
                $("#tbl_depo_holding").html("");
                $("#tbl_depo_holding").append("<tr><th>No Data Found</th></tr>");
                $("#main_data_div").show();
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

$("#btnDepoHoldingPdf").click(function () {

    GenerateExportDetails("#btnDepoHoldingPdf", "X", "PDF")

})
$("#btnDepoHoldingExcel").click(function () {

    GenerateExportDetails("#btnDepoHoldingExcel", "X", "EXCEL")

})
$("#btnEMailDepoHolding").click(function () {

    GenerateExportDetails("#btnEMailDepoHolding", "E", "PDF")

})
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()
    let LoginhashId = $("#LoggedInUserHash").val()
    let access_token = "";
    let FamilyId = $('#ddlFamilyList :selected').val();
    let dp_main_client_id = $('#ddlClientList :selected').val();
    let ToDate = $("#as_on_date").val()

    let strPara = LoginhashId + "~[AccessToken]~" + FamilyId + "~" + dp_main_client_id + "~" + ToDate;

    let strDescrip = "Depository Holding Report for " + $('#ddlClientList :selected').text();

    PDFandExcelExport(evt_btn, "72", LoginId, "0", export_type, export_format, strPara, strDescrip);

}
