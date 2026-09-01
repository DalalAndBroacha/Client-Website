$(document).ready(function () {
    $("#div_summary_report").hide();

    GetClientList();

});
$("#ddlFamilyList").change(function () {
    $("#content_div").hide();
    GetClientList();
});
$("#ddlClientList").change(function () {
    $("#content_div").hide();

});
$("#ddlFinyear").change(function () {
    $("#content_div").hide();

});

function ViewReport() {

    let summary;

    if ($("#Holdtype").prop('checked')) {

        summary = "Y";
       
    }
    else {
        summary = "N";
       
    }

    var main_client_id = $('#ddlClientList :selected').val();
    var ToDate = (new Date()).toISOString().split('T')[0];
    var FINYR = $('#ddlFinyear :selected').val();

    $("#content_div").show();

    var formdata1 = {
        "main_client_id": main_client_id,
        "FINYR": FINYR,
        "ToDate": ToDate,
        "summaryFlag": summary
    }


    holdingDetails(formdata1, summary);

    $("#btnEMailHolding").show();
    $("#btnExportPdfHolding").show();
    $("#btnExportExcelHolding").show();
}

function holdingDetails(parameterObj, dspflag) {

    if (dspflag == 'Y') {
        $("#lblReport_Name").text("Summary Holding Report");
        $("#txtHoldingFlag").val("Y");
    }
    else {
      
        $("#lblReport_Name").text("Detail Holding Report");
        $("#txtHoldingFlag").val("N");
    }
    /*$("#content_div").show();*/

    //var main_client_id = $('#ddlClientList :selected').val();
    //var ToDate = (new Date()).toISOString().split('T')[0];

    //var formdata = {
    //    "main_client_id": main_client_id,
    //    "ToDate": ToDate,
    //    "FINYR": $('#ddlFinyear :selected').val(),

    //}
    var posturl = "/ClientPortal/Reports/HoldingSummary";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(parameterObj),

        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var html = "";
            $("#tbodyBindingHolding").html("");

            if (data.length > 0) {



                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].acc_sub_category;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);

                }

                //html = html + "<tr><th class='TableHeaderRow' style='font-weight:bold'>Scrip Name</th><th class='TableHeaderRow' style='font-weight:bold'>Quantity</th>" +
                //    "<th class='TableHeaderRow' style='font-weight:bold'>Wt Cost</th><th class='TableHeaderRow' style='font-weight:bold'>Total Cost</th>" +
                //    "<th class='TableHeaderRow' style='font-weight:bold'>Mkt rate</th><th class='TableHeaderRow' style='font-weight:bold'>Mkt date</th><th class='TableHeaderRow' style='font-weight:bold'>Mkt Value</th>" +
                //    "<th class='TableHeaderRow' style='font-weight:bold'>Hold %</th><th class='TableHeaderRow' style='font-weight:bold'>Profit / Loss</th></tr > "
                $.each(groups, function (index, value1) {
                    if (value1[0].acc_sub_category != "ZZZZZZ") {
                        html = html + "<tr class='SubTotalRow'><td>" +
                            "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#hold_acc_" + value1[0].acc_sub_category + "'" +
                            "aria-expanded='false' aria-controls='divi_acc_" + value1[0].acc_sub_category + "'>" +
                            "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                            "</button>&nbsp;" + value1[0].sub_category + "</td></tr>"
                        html = html + "<tr><td>" +
                            "<div class='collapse' id='hold_acc_" + value1[0].acc_sub_category + "'" +
                            "<div class='global_report_accords'>" + //For setting max height
                            "<table id='trds_Table_" + value1[0].acc_sub_category + "' class='table table-hover w-100'>"
                        if (dspflag == 'Y') {
                            html = html + "<colgroup><col style='width:25%;'><col style='width:10%;'><col style='width:10%;'><col style='width:10%;'><col style='width:10%;'>" +
                                "<col style='width:15%;'><col style='width:10%;'><col style='width:7%;'><col style='width:12%;'></colgroup>" +
                                "<thead class='table_sticky_heads'><tr><th>Scrip Name</th><th>Quantity</th><th>Wt Cost</th><th>Total Cost</th><th>Mkt rate</th><th>Mkt date</th><th>Mkt Value</th>" +
                                "<th>Hold %</th><th>Profit / Loss</th></tr></thead><tbody>"
                        }
                        else {
                            html = html + "<colgroup><col style='width:22%;'><col style='width:10%;'><col style='width:7%;'><col style='width:7%;'><col style='width:10%;'><col style='width:6%;'>" +
                                "<col style='width:6%;'><col style='width:7%;'><col style='width:7%;'><col style='width:6%;'><col style='width:6%;'><col style='width:7%;'></colgroup>" +
                                "<thead class='table_sticky_heads'><tr>"+
                                "<th> Scrip Name</th><th>Date</th><th>Quantity</th><th>Rate</th><th>Value</th><th>hold %</th><th>Cum %</th><th>Market Rate</th>" +
                                "<th>Market Value</th><th>Hold %</th><th>No. of Days</th><th>ST / LT</th></tr></thead><tbody>"
                        }
                        $.each(value1, function (index1, value) {

                            if (dspflag == 'Y') {

                                if (value.scrip_name != "Total") {
                                    html = html + "<tr>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + value.hold_qty + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.holding_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_cost.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.holding_mkt_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + (value.mkt_rate_date != '1900-01-01T00:00:00' ? formatDate(value.mkt_rate_date) : '') + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.mkt_value.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.hold_per.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.profit_loss.toFixed(2)) + "</p></td>" +
                                        "</tr>"
                                }
                                else {
                                    html = html + "<tr class='SubTotalRow'>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_cost.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.mkt_value.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.profit_loss.toFixed(2)) + "</p></td>" +
                                        "</tr>"
                                }
                            }
                            else {
                                if (value.scrip_name != "Total") {
                                    html = html + "<tr>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + formatDate(value.buy_trn_date) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + value.holding_qty + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_trn_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.value.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.holding_per.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.cum_per.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.market_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.markt_value.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.holding_per1.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.no_of_days + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.sT_LT + "</p></td>" +
                                        "</tr>"
                                }
                                else {
                                    html = html + "<tr class='SubTotalRow'>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.value.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.markt_value.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'></p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'></p></td>" +
                                        "</tr>"
                                }
                            }
                        })
                        html = html + "</tbody></table>" +
                            "</div>" + //accords end
                            "</div>" + //Collapse end
                            "</td></tr>"

                    }
                    else {
                        if (dspflag == 'Y') {
                            html = html + "<tr class='GrandTotalRow'><td style='font-weight:900;'>" +
                                "Grand Total  |  Total Cost : " + numberWithCommas(value1[0].total_cost.toFixed(2)) +
                                "  |  Market Value : " + numberWithCommas(value1[0].mkt_value.toFixed(2)) +
                                "  |  Profit / Loss : " + numberWithCommas(value1[0].profit_loss.toFixed(2)) +
                                " </td></tr>"
                        }
                        else {
                            html = html + "<tr class='GrandTotalRow'><td style='font-weight:900;'>" +
                                "Grand Total  |  Total Cost : " + numberWithCommas(value1[0].value.toFixed(2)) +
                                "  |  Market Value : " + numberWithCommas(value1[0].markt_value.toFixed(2)) +
                                " </td></tr>"
                        }
                    }
                });

                $("#tbodyBindingHolding").append(html);
            }
            else {
                $("#content_div").hide();
                alert("Data not found.");
               
            }
        },
        complete: function (xhr) {
            completeajaxrequest();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

function GetClientList() {
    var formdata = {
        "family_id": $('#ddlFamilyList :selected').val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlClientList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.main_client_id + ">" + value.main_client_name + "</option>";
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

$("#btnEMailHolding").click(function () {

    GenerateExportDetails("#btnEMailHolding", "E", "PDF")

})
$("#btnExportExcelHolding").click(function () {

    GenerateExportDetails("#btnExportExcelHolding", "X", "EXCEL")

})
$("#btnExportPdfHolding").click(function () {
    GenerateExportDetails("#btnExportPdfHolding", "X", "PDF")

})

function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF
    var flag = $("#txtHoldingFlag").val();

    var LoginId = $("#LoggedInUID").val()
    var main_client_id = $('#ddlClientList :selected').val();
    var Fy_Year = $('#ddlFinyear :selected').val();
    var To_date = (new Date()).toISOString().split('T')[0];
    var Summary = flag;

    var txt_main_client_id = $('#ddlClientList :selected').text();

    var strPara = LoginId + "~" + main_client_id + "~" + To_date + "~" + Summary + "~" + Fy_Year;

    if (flag == 'Y') {
        var strDescrip = "Summary Holding Report for " + txt_main_client_id + " as on " + Fy_Year

        PDFandExcelExport(evt_btn, "49", LoginId, "0", export_type, export_format, strPara, strDescrip);
    }
    else {
        var strDescrip = "Detail Holding Report for " + txt_main_client_id + " as on " + Fy_Year

        PDFandExcelExport(evt_btn, "50", LoginId, "0", export_type, export_format, strPara, strDescrip);
    }

}

function startajaxrequest() {
    $("#HoldingwaitIn").css("display", "block");
}

function completeajaxrequest() {
    $("#HoldingwaitIn").css("display", "none");
}


    