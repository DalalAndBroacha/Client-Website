$(document).ready(function () {
    CollapseSideMenu();

    $("#AssetAllocationLandingPage").hide();
    $("#myModalst_Performance").hide();
    $("#myModalst_PerformanceScript").hide();
    $("#myModalst_PerformanceBonds").hide();
    $("#myModalst_PerformanceEquityPMS").hide();
    $("#myModalst_PerformanceOtherPMS").hide();
    $("#LiquLoan_div").hide();
    $("#myModalst_PerformanceFDDetails").hide();

    if ($("#LoggedInRoleName").val() == "Family" || $("#LoggedInRoleName").val() == "Client") {
        fnSetCurrFamId();
        GetFamilyData();
    }

});
//Loader
function completeajaxrequest() {
    $("#waitIn").css("display", "none");
}
function startajaxrequest() {
    $("#waitIn").css("display", "block");
}
//Loader
$("#ddlFamilyList").change(function () {
    fnSetCurrFamId();
    $("#AssetAllocationLandingPage").hide();
    Chart.helpers.each(Chart.instances, function (instance) {
        instance.destroy();
    })

    GetFamilyData();
});
$("#ddlFinyear").change(function () {

    $("#AssetAllocationLandingPage").hide();
    Chart.helpers.each(Chart.instances, function (instance) {
        instance.destroy();
    })

    GetFamilyData();
});

let chartcolour = ["#f1948a", "#bb8fce", "#85c1e9", "#76d7c4", "#a569bd", "#7dcea0", "#f8c471", "#85929e", "#e59866", "#27ae60", "#b2babb", "#eaeded"];

function GetFamilyData() {


    let formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "acces_token": "",
        "family_id": $('#ddlFamilyList :selected').val(),
        "fin_year": $('#ddlFinyear :selected').val()
    }
    let posturl = "/ClientPortal/Reports/AssetAllocation";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            let html1 = "";
            let html2 = "";
            let canvahtml = "";
            let categoryforchart = [];
            let valuesforchart = [];
            let ChartTitle = "";

            $("#Summary_Familywise").html("");
            $("#ClientWise_data").html("");
            $("#ClientCategoryWiseData").html("");
            $("#Clientcategorywise_ChartDiv").html("");

            $("#Curr_Family_Id").val(data.family_id);

            if (data != null && data.sql_status == "Success") {
                $("#AssetAllocationLandingPage").show();
                $("#btnExportPdfAssetAllocation").show();
                $("#btnExportExcelAssetAllocation").show();
                $("#btnEMailAssetAllocation").show();

                $.each(data.summaryData, function (index, value) {

                    if (value.display_category == "Total") {
                        html = html + "<tr class='SubTotalRow'>" +
                            "<td> <p class='ReportTableFont paraTextAlignLeft'>" + value.display_category + "</p></td >" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.market_value.toFixed(2)) + "</p></td>" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>100</p></td> </tr>"
                    }
                    else {
                        html = html + "<tr>" +
                            "<td> <p class='ReportTableFont paraTextAlignLeft'>" + value.display_category + "</p></td >" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.market_value.toFixed(2)) + "</p></td>" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>" + value.percentageOfPortfolio.toFixed(2) + "</p></td> </tr>"

                        categoryforchart.push(value.display_category)
                        valuesforchart.push(value.market_value)
                    }
                });
                create_Chart(categoryforchart, valuesforchart, "Summarychart", "")

                categoryforchart = [];
                valuesforchart = [];

                $.each(data.clientData, function (index, value) {

                    if (value.client_name == "Total") {
                        html1 = html1 + "<tr class='SubTotalRow'>" +
                            "<td> <p class='ReportTableFont paraTextAlignLeft'>" + value.client_name + "</p></td >" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.market_value.toFixed(2)) + "</p></td>" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>100</p></td> </tr>"
                    }
                    else {
                        html1 = html1 + "<tr>" +
                            "<td> <p class='ReportTableFont paraTextAlignLeft'>" + value.client_name + " (" + value.pan + ")" + "</p></td >" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.market_value.toFixed(2)) + "</p></td>" +
                            "<td> <p class='ReportTableFont paraTextAlignRight'>" + value.percentageOfPortfolio.toFixed(2) + "</p></td> </tr>"

                        categoryforchart.push(value.client_name)
                        valuesforchart.push(value.market_value)

                    }
                });
                create_Chart(categoryforchart, valuesforchart, "ClientWiseDataChart", "")


                let GrpclientCategoryData = Object.groupBy(data.clientCategoryData, ({ client_name }) => client_name)


                categoryforchart = [];
                valuesforchart = [];

                $.each(GrpclientCategoryData, function (index, value) {

                    html2 = html2 + "<tr class = 'GrandTotalRow'><td class = 'GrandTotalRowData' colspan = 14><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold; text-align: left !important;'>" +
                        "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#div_" + value[0].main_client_id + "' aria-expanded='false' aria-controls='#div_" + value[0].main_client_id + "'>" +
                        "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i></button>&nbsp;&nbsp;" +
                        index + " (" + value[0].pan + ")" + "</td></tr> <tr><td><div class='collapse' id='div_" + value[0].main_client_id + "'data-parent='#ClientCategoryWiseData'><table class='table table-hover'>" +

                        "<tr><th>Category</th><th>Holding</th><th>Holding %</th></tr>"
                    $.each(value, function (index1, value1) {
                        if (value1.display_category.includes("Total") == true) {
                            
                            html2 = html2 + "<tr class='SubTotalRow'>" +
                                "<td> <p class='ReportTableFont paraTextAlignLeft'>" + value1.display_category + "</p></td >" +
                                "<td> <p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value1.market_value) + "</p></td>" +
                                "<td> <p class='ReportTableFont paraTextAlignRight'>" + value1.hld_per_clientwise.toFixed(2) + "</p></td> </tr>"
                        }
                        else {
                            html2 = html2 + "<tr>"
                            if (value1.category == "Bonds" || value1.category == "Direct Equity" || value1.category == "Liqui Loan" || value1.category == "Equity PMS" ||
                                value1.main_category == "Other PMS" || value1.category == "Fixed Deposit") {
                                html2 = html2 + "<td style='cursor: pointer;border: 1px solid #ff0000; text-decoration:underline;' " +
                                    "onclick =\"SecondPageReport(\'" + (value1.main_category == "Equity" || value1.category == "InvITs" ||
                                        value1.category == "ReITs" || value1.main_category == "PMS" || value1.category == "Liqui Loan" ||
                                        value1.main_category == "Other PMS" ? value1.client_id : value1.main_client_id) +
                                    "\' ,\'" + value1.category + "\',\'" + value1.pan + "\',\'" + value1.main_client_id + "\',\'" + value1.account_code +
                                    "\',\'" + value1.source + "\',\'" + value1.main_category + "\',\'" + value1.client_name + "\')\">" + value1.display_category + "</td >"

                            }
                            else if (value1.main_category == "AIF") {
                                html2 = html2 + "<td>" + value1.display_category + "</td >"
                            }
                            else {
                                html2 = html2 + "<td style='cursor: pointer;border: 1px solid #ff0000; text-decoration:underline;' " +
                                    "onclick =\"downloadMintReport(\'" + value1.pan + "\',\'" + value1.mint_client_name + "\',\'" + value1.category + "\')\">" + value1.display_category + "</td >"
                            }
                            html2 = html2 + "<td> <p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value1.market_value) + "</p></td>" +
                                "<td> <p class='ReportTableFont paraTextAlignRight'>" + value1.hld_per_clientwise + "</p></td> </tr>"

                            categoryforchart.push(value1.display_category)
                            valuesforchart.push(value1.market_value)
                        }
                    });
                    html2 = html2 + "</table></div></td></tr>";
                });

                $("#Summary_Familywise").append(html);
                $("#ClientWise_data").append(html1);
                $("#ClientCategoryWiseData").append(html2);


                $("#Fin_year").text($('#ddlFinyear :selected').val());
                $("#FamilyName").text(data.family);
            }
            else {
                $("#AssetAllocationLandingPage").hide();
                alert("data not found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function downloadMintReport(pan, Mint_client_name, category) {
    let asondate = "";
    let finyear = $('#ddlFinyear :selected').val()
    let addedfinyear = parseInt($('#ddlFinyear :selected').val()) + 1;
    let today = new Date();


    if ((today.getMonth()) > 3 && today.getFullYear() == finyear) {

        asondate = (new Date()).toISOString().split('T')[0];
    }
    else if ((today.getMonth()) <= 3 && today.getFullYear() - 1 == finyear) {

        asondate = (new Date()).toISOString().split('T')[0];
    }
    else {
        asondate = addedfinyear + "-03-31";
    }

    let formdata = {
        "pan": pan,
        "AsOnDatePortReturn": asondate,
        "Mint_client_name": Mint_client_name,
        "category": category,
    }
    console.log(formdata)
    let posturl = "/ClientPortal/Mint/DownloadMintReport";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            if (data != null) {
                if (data.status == "Success") {
                    let fpurl = "getDocument?FP=" + data.mfPostUrl;
                    window.open(fpurl);
                }
                else {
                    alert(data.message);
                }

            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}
function SecondPageReport(client_id, sub_category, pan_number, main_client_id, account_code,source, main_category, client_name) {

    let family_id = $("#Curr_Family_Id").val();
    $("#common_client_id_holder").val(client_id);

    let assset_code;

    if (sub_category == "Direct Equity" || sub_category == "InvITs" || sub_category == "ReITs") {

        if (sub_category == "Direct Equity") {
            assset_code = "Equity";
            $("#AssetAllocationLandingPage").hide();
            //$("#myModalst_PerformanceEquity_MF").hide();
            $("#myModalst_Performance").show();

            $("#tbodyBindingrealisedDividend").html("");
            /*      $("#noReFoundrealiseddivindend").html("");*/
            /*$("#popsub_category").val(sub_category);*/
            PerformanceholdingDirectEquityDetails(client_id, sub_category, pan_number, family_id, main_client_id, account_code, assset_code);
        }
        else {
            assset_code = sub_category;
            $("#AssetAllocationLandingPage").hide();
            //$("#myModalst_PerformanceEquity_MF").hide();
            $("#ReitsInvits_Performance").show();

            PerformanceholdingDirectEquityDetails(client_id, sub_category, pan_number, family_id, main_client_id, account_code, assset_code);
        }

    }
    else if (sub_category == "Bonds") {
        assset_code = "Equity";
        $("#AssetAllocationLandingPage").hide();
        $("#myModalst_PerformanceBonds").show();

        $("#tbodyBindingrealisedPerformanceBonds").html("");
        $("#noReFoundrealisedPerformanceBonds").html("");
        $("#popclient_idBonds").val(client_id);
        $("#popsub_categoryBonds").val(sub_category)

        PerformanceholdingBondsDetails(client_id, sub_category, pan_number, family_id);
        PerfBondCashFlowDetails(client_id, sub_category, pan_number, family_id);

    }
    else if (sub_category == "Equity PMS") {
        assset_code = "Equity";
        $("#AssetAllocationLandingPage").hide();
        $("#myModalst_PerformanceEquityPMS").show();

        $("#tbodyBindingrealisedPerformanceEquityPMS").html("");
        $("#noReFoundrealisedPerformanceEquityPMS").html("");
        $("#popclient_idEquityPMS").val(client_id);
        $("#popsub_categoryEquityPMS").val(sub_category)
        PerformanceholdingEquityPMSDetails(client_id, sub_category, pan_number, family_id);
    }
    else if (main_category == "Other PMS") {
        /* assset_code = "Equity";*/
        $("#AssetAllocationLandingPage").hide();
        $("#myModalst_PerformanceOtherPMS").show();

        $("#tbodyBindingPerformanceOtherPMS").html("");
        //$("#noReFoundrealisedPerformanceEquityPMS").html("");
        $("#popclient_idOtherPMS").val(client_id);
        $("#popsub_categoryOtherPMS").val(sub_category)

        performanceholdingOtherPMS(client_id, sub_category, family_id, source, main_category);
    }
    else if (sub_category == "Liqui Loan") {
        assset_code = "Equity";

        $("#AssetAllocationLandingPage").hide();
        $("#tbody_Liquiloan_Investments").html("");
        $("#tbody_Liquiloan_CashFlow").html("");
        $("#LiquLoan_div").show();
        $("#ddlFinyear").attr("disabled", true);
        $("#ddlFamilyList").attr("disabled", true);

        PerformanceholdingLiquiLoan(account_code, pan_number, client_name);
    }
    else if (sub_category == "Fixed Deposit") {

        $("#AssetAllocationLandingPage").hide();
        $("#myModalst_PerformanceFDDetails").show();

        $("#tbodyBindingPerformanceFDDetails").html("");
        $("#txtFDClient").val(client_id);
        $("#sub_categoryFDDetails").val(sub_category)

        performanceholdingFDDetails(client_id, sub_category, family_id);
    }
}
function PerformanceholdingDirectEquityDetails(client_id, sub_category, PAN, family_id, main_client_id, account_code, assset_code) {

    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "ClientID": client_id,
        "Subcategory": sub_category
    }
    var posturl = "/ClientPortal/Reports/psprptperformanceholdingdirectequityreportDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var html = "";
            var main_client_name = "";
            $("#tbodyBindingrealisedPerformance").html("");
            $("#tbodyBindingRnIPerformance").html("");

            //$("#noReFoundrealisedRnIPerformance").html("");
            //$("#noReFoundrealisedPerformance").html("");

            if (data.length > 0) {
                var groups = {};

                for (var i = 0; i < data.length; i++) {
                    if (data[i].scrip_code != null) {
                        if (data[i].reord_order == 1) {
                            var groupName = data[i].scrip_industry;
                        }
                    }
                    else {
                        if (data[i].reord_order == 2) {
                            var groupName = "Grand Total";
                        }
                        else {
                            var groupName = data[i - 1].scrip_industry;
                        }
                    }
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }

                main_client_name = data[0].main_client_name;
                $.each(groups, function (index, value1) {
                    if (value1[0].scrip_name != "Grand Total") {
                        html = html + "<tr class='SubTotalRow'><td colspan = 16><p style='font-size:14px;font-family:Roboto, sans-serif; word-wrap:break-word; white-space:pre; font-weight:bold'> " + index + "</td></tr>";
                    }
                    $.each(value1, function (index1, value) {
                        if (value.scrip_code != null) {
                            html = html + "<tr>";

                            if (client_id == "0") {

                                html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;width:max-content;'>" + value.scrip_name + "</p></td>"
                            }
                            else {
                                html = html +
                                    "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;cursor: pointer; text-decoration: underline; width:max-content;' onclick=\"myFunctionloadPerformanceScrip_Code(\'" + main_client_id + "\',\'" + value.scrip_code +
                                    "\',\'" + assset_code + "\','" + account_code + "\',\'" + sub_category + "\')\"> " + value.scrip_name + "</p></td>"
                            }
                            html = html +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight " + isNegativeValue(value.quantity) + "'> " + (value.quantity == 0 ? '' : numberWithCommas(value.quantity)) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.avg_cost) + "'> " + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.hld_cost) + "'> " + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed(2))) + "</p></td>" +

                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.market_Value) + "'> " + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</p></td> " +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.holding_per) + "'> " + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.dividend) + "'> " + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_unreal_profit) + "'> " + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.long_unreal_profit) + "'> " + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_real_profit) + "'> " + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.long_real_profit) + "'> " + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_ttl_gain) + "'> " + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.lng_ttl_gain) + "'> " + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</p></td>" +

                                "<td class = 'TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight" + isNegativeValue(value.return_abs) + "'> " + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight" + isNegativeValue(value.return_xirr) + "'> " + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td>" +

                                "</tr > ";
                        }
                        //SubTotal
                        else if (value.scrip_code == null && value.scrip_name != "Grand Total") {
                            html = html + "<tr class='SubTotalRow'>" +
                                "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important; width: max-content;' >" + value.scrip_name + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.quantity) + "'>" + (value.quantity == 0 ? '' : numberWithCommas(value.quantity)) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.avg_cost) + "'>" + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.hld_cost) + "'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'>" + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed(2))) + "</p></td>" +

                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.market_Value) + "'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</p></td> " +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.holding_per) + "'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.dividend) + "'>" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_unreal_profit) + "'>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.long_unreal_profit) + "'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_real_profit) + "'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.long_real_profit) + "'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_ttl_gain) + "'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.lng_ttl_gain) + "'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</p></td>" +

                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_abs) + "'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_xirr) + "'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td>" +
                                "</tr>"

                                + "<tr><td></td></tr>";
                        }
                        else if (value.scrip_code == null && value.scrip_name == "Grand Total") {
                            html = html + "<tr class='GrandTotalRow'>" +
                                "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;' >" + value.scrip_name + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.quantity) + "'> " + (value.quantity == 0 ? '' : numberWithCommas(value.quantity)) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.avg_cost) + "'> " + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.hld_cost) + "'> " + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed(2))) + "</p></td>" +

                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.market_Value) + "'> " + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</p></td> " +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.holding_per) + "'> " + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.dividend) + "'> " + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_unreal_profit) + "'> " + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.long_unreal_profit) + "'> " + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_real_profit) + "'> " + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.long_real_profit) + "'> " + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.srt_ttl_gain) + "'> " + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.lng_ttl_gain) + "'> " + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</p></td>" +

                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_abs) + "'> " + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_xirr) + "'> " + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td>" +

                                "</tr > ";
                        }
                    });
                });


                if (sub_category == "Direct Equity") {
                    $("#tbodyBindingrealisedPerformance").append(html);
                    $("#bheDEFinYear").text($('#ddlFinyear :selected').text());
                    $("#bheDEfamilyName").text($('#ddlFamilyList :selected').text());
                    $("#bheDEclientName").text(main_client_name);
                    $("#bheDEPAN").text(PAN);

                    $("#DEclient_id").val(client_id);

                    $("#btnEMailDirectEquity").show();
                    $("#btnExportDEExcel").show();
                    $("#btnExportDEPDF").show();
                }
                else {
                    $("#tbodyBindingRnIPerformance").append(html);
                    $("#RnIFinYear").text($('#ddlFinyear :selected').text());
                    $("#RnIfamilyName").text($('#ddlFamilyList :selected').text());
                    $("#RnIclientName").text(main_client_name);
                    $("#RnIPAN").text(PAN);

                    $("#DEclient_id").val(client_id);

                    $("#btnExportRnIPDF").show();
                    $("#btnExportRnIExcel").show();
                    $("#btnEMailRnI").show();
                }

                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

                //completeajaxrequest();
            } else {
                $("#tbodyBindingrealisedPerformance").html("");
                $("#noReFoundrealisedPerformance").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}
function myFunctionloadPerformanceScrip_Code(main_client_id, scrip_code, asset_name, account_code, sub_category) {
    if (sub_category == "Direct Equity" || sub_category == "InvITs" || sub_category == "ReITs") {
        $("#tbodyBindingrealisedPerformanceScript").html("");
        $("#noReFoundrealisedPerformanceScript").html("");

        PerformanceholdingAllDetailsScrip_Code(main_client_id, scrip_code, asset_name, account_code, sub_category);
        //$("#myModalst_PerformanceScript").modal({ backdrop: 'static', keyboard: false });

        $("#myModalst_Performance").hide();
    }
};
function PerformanceholdingAllDetailsScrip_Code(main_client_id, scrip_code, asset_name, account_code, sub_category) {
    var formdata = {
        "FINYR": $('#ddlFinyear :selected').val(),
        "Client": main_client_id,
        "Scrip_Code": scrip_code,
        "subcategory": sub_category,
        "account_code": account_code,
        "asset_code": asset_name
    }

    var posturl = "/ClientPortal/Reports/psprptclientperformancecheckDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var html = "";
            $("#tbodyBindingrealisedPerformanceScript").html("");
            $("#noReFoundrealisedPerformanceScript").html("");
            $("#ScriptClientName").html("");
            $("#ScriptName").html("");



            if (data.length > 0) {

                $("#ScriptClientName").text(data[0].client_name);
                $("#ScriptName").text(data[0].script_name);
                $.each(data, function (index, value) {
                    if (value.flag == "B" || value.flag == "S") {
                        html = html + "<tr><td><p style='font-size:14px;font-family:Roboto, sans-serif;text-align:left;'> " + formatDate(value.buy_sell_trn_date) + "</p></td>";
                        if (value.flag == "S") {
                            html = html + "<td><p style='font-size:14px;font-family:Roboto, sans-serif; color:red !important; text-align:right;'> -" + numberWithCommas(value.buy_sell_trn_qty.toFixed(2)) + "</p></td>"
                        }
                        else {
                            html = html + "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_sell_trn_qty.toFixed(2)) + "</p></td>"
                        }
                        html = html + "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.buy_sell_trn_rate) + "'> " + (value.buy_sell_trn_rate == 0 ? '' : numberWithCommas(value.buy_sell_trn_rate.toFixed(2))) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed(2))) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.dividend) + "'> " + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed(2))) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ust_pnl) + "'> " + (value.ust_pnl == 0 ? '' : numberWithCommas(value.ust_pnl.toFixed(2))) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ult_pnl) + "'> " + (value.ult_pnl == 0 ? '' : numberWithCommas(value.ult_pnl.toFixed(2))) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rst_pnl) + "'> " + (value.rst_pnl == 0 ? '' : numberWithCommas(value.rst_pnl.toFixed(2))) + "</p></td >" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rlt_pnl) + "'> " + (value.rlt_pnl == 0 ? '' : numberWithCommas(value.rlt_pnl.toFixed(2))) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ttl_srt_gain) + "'> " + (value.ttl_srt_gain == 0 ? '' : numberWithCommas(value.ttl_srt_gain.toFixed(2))) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ttl_long_gain) + "'> " + (value.ttl_long_gain == 0 ? '' : numberWithCommas(value.ttl_long_gain.toFixed(2))) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.absolute) + "'> " + (value.absolute == 0 ? '' : numberWithCommas(value.absolute.toFixed(2))) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.xirr) + "'> " + (value.xirr == 0 ? '' : numberWithCommas(value.xirr.toFixed(2))) + "</p></td></tr>";
                    }
                    else {
                        if (value.flag == "Balance QTY") {
                            html = html +
                                "<tr class='SubTotalRow'><td><p style='font-size:14px;font-family:Roboto, sans-serif;text-align:left;'>Balance Quantity</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + (value.buy_sell_trn_qty == 0 ? '' : numberWithCommas(value.buy_sell_trn_qty.toFixed(2))) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'> " + (value.buy_sell_trn_rate == 0 ? '' : numberWithCommas(value.buy_sell_trn_rate.toFixed(2))) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed(2))) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.dividend) + "'> " + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed(2))) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ust_pnl) + "'> " + (value.ust_pnl == 0 ? '' : numberWithCommas(value.ust_pnl.toFixed(2))) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ult_pnl) + "'> " + (value.ult_pnl == 0 ? '' : numberWithCommas(value.ult_pnl.toFixed(2))) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rst_pnl) + "'> " + (value.rst_pnl == 0 ? '' : numberWithCommas(value.rst_pnl.toFixed(2))) + "</p></td >" +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rlt_pnl) + "'> " + (value.rlt_pnl == 0 ? '' : numberWithCommas(value.rlt_pnl.toFixed(2))) + "</p></td > " +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ttl_srt_gain) + "'> " + (value.ttl_srt_gain == 0 ? '' : numberWithCommas(value.ttl_srt_gain.toFixed(2))) + "</p></td > " +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ttl_long_gain) + "'> " + (value.ttl_long_gain == 0 ? '' : numberWithCommas(value.ttl_long_gain.toFixed(2))) + "</p></td > " +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.absolute) + "'> " + (value.absolute == 0 ? '' : numberWithCommas(value.absolute.toFixed(2))) + "</p></td > " +
                                "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.xirr) + "'> " + (value.xirr == 0 ? '' : numberWithCommas(value.xirr.toFixed(2))) + "</p></td></tr>";
                        }
                        else if (value.flag == "Total") {
                            html = html +
                                "<tr class='GrandTotalRow'><td class='GrandTotalRowDataWithoutBorders'><p style='font-size:14px;font-family:Roboto, sans-serif;text-align:left;'>Total</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.buy_sell_trn_qty) + "'>" + (value.buy_sell_trn_qty == 0 ? '' : numberWithCommas(value.buy_sell_trn_qty.toFixed(2))) + "</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.buy_sell_trn_rate) + "'> " + (value.buy_sell_trn_rate == 0 ? '' : numberWithCommas(value.buy_sell_trn_rate.toFixed(2))) + "</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed(2))) + "</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.dividend) + "'> " + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed(2))) + "</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ust_pnl) + "'> " + (value.ust_pnl == 0 ? '' : numberWithCommas(value.ust_pnl.toFixed(2))) + "</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ult_pnl) + "'> " + (value.ult_pnl == 0 ? '' : numberWithCommas(value.ult_pnl.toFixed(2))) + "</p></td>" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rst_pnl) + "'> " + (value.rst_pnl == 0 ? '' : numberWithCommas(value.rst_pnl.toFixed(2))) + "</p></td >" +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rlt_pnl) + "'> " + (value.rlt_pnl == 0 ? '' : numberWithCommas(value.rlt_pnl.toFixed(2))) + "</p></td > " +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ttl_srt_gain) + "'> " + (value.ttl_srt_gain == 0 ? '' : numberWithCommas(value.ttl_srt_gain.toFixed(2))) + "</p></td > " +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.ttl_long_gain) + "'> " + (value.ttl_long_gain == 0 ? '' : numberWithCommas(value.ttl_long_gain.toFixed(2))) + "</p></td > " +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.absolute) + "'> " + (value.absolute == 0 ? '' : numberWithCommas(value.absolute.toFixed(2))) + "</p></td > " +
                                "<td class='GrandTotalRowDataWithoutBorders'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.xirr) + "'> " + (value.xirr == 0 ? '' : numberWithCommas(value.xirr.toFixed(2))) + "</p></td></tr>";
                        }
                    }
                });




                $("#DEScriptName").text(data[0].script_name);
                $("#DEScriptCode").val(data[0].scrip_Code);
                $("#DEAccountCode").val(account_code);
                $("#DEMainClientID").val(main_client_id);

                $("#DEScriptClientName").text(data[0].client_name);

                $("#tbodyBindingrealisedPerformanceScript").append(html);
                //$("#ScriptFinYear").text($('#ddlFinyear :selected').val());
                //$("#ScriptfamilyName").text($('#ddlFamilyList :selected').text());



                $("#btnEMailDirectEquityScriptwise").show();
                $("#btnDEScriptExcel").show();
                $("#btnDEScriptPDF").show();
                $("#myModalst_PerformanceScript").show();

                $("#DEbtnIndicator").val(sub_category);


            } else {
                $("#tbodyBindingrealisedPerformanceScript").html("");
                $("#noReFoundrealisedPerformanceScript").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}
function PerformanceholdingBondsDetails(client_id, sub_category, PAN, family_id) {

    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "ClientID": client_id,
        "Subcategory": sub_category,
    }

    var posturl = "/ClientPortal/Reports/psprptperformanceholdingdirectequityreportDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            let html = "";
            let main_client_name = "";

            $("#tbodyBindingrealisedPerformanceBonds").html("");
            $("#noReFoundrealisedPerformanceBonds").html("");


            if (data.length > 0) {
                let groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].category;
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }

                main_client_name = data[0].main_client_name;

                $.each(groups, function (index, value1) {
                    if (value1[0].display_order == "1") {
                        html = html + "<tr><td class = 'SubTotalRow' colspan=12><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold'> " + index + "</td></tr>";
                    }
                    $.each(value1, function (index1, value) {
                        if (value.display_order == "1") {
                            html = html + "<tr>" +
                                "<td class = 'TableDataNumericData'> <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left;'> " + value.scrip_name + "</p></td>" +
                                "<td class = 'TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight'> " + value.rating + "</p></td>" +
                                "<td class = 'TableDataNumericData'> <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + value.coupon + "%" + "</p></td>" +
                                "<td class = 'TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.interest_payment_date_remarks == null ? '-' : value.interest_payment_date_remarks) + "</p></td>" +
                                "<td style='border-top:1px solid red'><p class='ReportTableFont paraTextAlignLeft'>";
                            if (value.maturity_date != null) {
                                html = html + "Maturity Date : " + formatDate(value.maturity_date) + "<br>";
                            }
                            if (value.call_date != null) {
                                html = html + "Call : " + value.call_date + "<br>";
                            }
                            if (value.put_date != null) {
                                html = html + "Put : " + value.put_date;
                            }

                            html = html + "</p></td>" +
                                "<td class='TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight'> " + formatDate(value.date_of_purchase) + "</p></td>";
                            if (value.cdsl_quantity == value.quantity) {
                                html = html + "<td class='TableDataNumericData " + isNegativeValue(value.cdsl_quantity) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.cdsl_quantity == 0 ? '' : numberWithCommas(value.cdsl_quantity.toFixed())) + "</p></td>" +
                                    "<td class='TableDataNumericData " + isNegativeValue(value.quantity) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.quantity == 0 ? '' : numberWithCommas(value.quantity.toFixed())) + "</p></td> "
                            }
                            else {
                                html = html + "<td style='background-color: #e35656;' class='TableDataNumericData " + isNegativeValue(value.cdsl_quantity) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.cdsl_quantity == 0 ? '' : numberWithCommas(value.cdsl_quantity.toFixed())) + "</p></td>" +
                                    "<td style='background-color: #e35656;'  class='TableDataNumericData " + isNegativeValue(value.quantity) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.quantity == 0 ? '' : numberWithCommas(value.quantity.toFixed())) + "</p></td> "
                            }
                            html = html + "<td class='TableDataNumericData " + isNegativeValue(value.face_value) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.face_value == 0 ? '' : numberWithCommas(value.face_value.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.total_holding) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.total_holding == 0 ? '' : numberWithCommas(value.total_holding.toFixed())) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.purchase_price) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.purchase_price == 0 ? '' : numberWithCommas(value.purchase_price.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.principal_value) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.principal_value == 0 ? '' : numberWithCommas(value.principal_value.toFixed(2))) + "</p></td></tr>";
                        }
                        else if (value.display_order == "2") {
                            html = html + "<tr class='SubTotalRow'>" +
                                "<td class='TableDataNumericData' colspan='9'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left;'> " + value.scrip_name + "</p></td>" +
                                "<td class='TableDataNumericData " + isNegativeValue(value.total_holding) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.total_holding == 0 ? '' : numberWithCommas(value.total_holding.toFixed(2))) + "</p></td>" +
                                "<td style='border-bottom:1px solid red'></td>" +
                                "<td class TableDataNumericData " + isNegativeValue(value.principal_value) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.principal_value == 0 ? '' : numberWithCommas(value.principal_value.toFixed(2))) + "</p></td>" +
                                + "</tr>"
                        }
                        else {
                            html = html + "<tr class='GrandTotalRow'>" +
                                "<td class = 'GrandTotalRowData' colspan='9'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left;'> " + value.scrip_name + "</p></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.total_holding) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.total_holding == 0 ? '' : numberWithCommas(value.total_holding.toFixed(2))) + "</p></td>" +
                                "<td style='border-bottom:1px solid red'></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.principal_value) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.principal_value == 0 ? '' : numberWithCommas(value.principal_value.toFixed(2))) + "</p></td>" +
                                + "</tr><tr></tr>"
                        }
                    });
                });

                $("#tbodyBindingrealisedPerformanceBonds").append(html);
                $("#bheFinYearBonds").text($('#ddlFinyear :selected').text());
                $("#bhefamilyNameBonds").text($('#ddlFamilyList :selected').text());
                $("#bheclientNameBonds").text(main_client_name);
                $("#bhePANBonds").text(PAN);
                $('#BondTablesData').show();

                $('#btnBondsPDFExport').show();
                $('#btnBondsExcelExport').show();
                $('#btnEMailBonds').show();

                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

                //completeajaxrequest();

            } else {
                $("#tbodyBindingrealisedPerformanceBonds").html("");
                $("#noReFoundrealisedPerformanceBonds").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}
function PerfBondCashFlowDetails(client_id, sub_category, PAN, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "ClientID": client_id,
        "Subcategory": sub_category
    }
    var posturl = "/ClientPortal/Reports/psprptperformanceholdingbondsreportcashflowDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        //beforeSend: function (xhr) {
        //    startajaxrequest();
        //},
        success: function (data) {
            var html = "";
            var main_client_name = "";

            let grpcolspan = 5;

            $("#Performance_Bonds_Cash_Flow").html("");
            if (data.length > 0) {
                main_client_name = data[0].main_client_name;

                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    let groupName = data[i].sub_category;
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }

                var divigroup = {};
                for (var i = 0; i < data.length; i++) {
                    let groupName = data[i].dividend_month;
                    if (!divigroup[groupName]) {
                        divigroup[groupName] = [];
                    }
                    divigroup[groupName].push(data[i].dividend_month);
                }

                html = html + "<thead><tr>" +
                    "<th class='TableHeaderRow'><p><b>Security Name</b></p></th>" +
                    "<th class='TableHeaderRow'><p><b>%</b></p></th>" +
                    "<th class='TableHeaderRow'><p><b>IP Date</b></p></th>" +
                    "<th class='TableHeaderRow'><p><b>Maturity Date</b></p></th>";

                $.each(divigroup, function (index, value1) {

                    html = html + "<th class='TableHeaderRow'><p><b>" + formatDate(index).slice(3) + "</b></p></th>";
                    grpcolspan += 1;
                });

                html = html + "<th class='TableHeaderRow'><p><b>Total</b></p></th>" +
                    "</tr></thead><tbody>"

                $.each(groups, function (index, value1) {
                    html = html + "<tr><td class = 'SubTotalRow' colspan=" + grpcolspan + "><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold'>" + index + "</td></tr>";
                    $.each(value1, function (index1, value) {
                        html = html + "<tr>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left;'> " + value.scrip_name + "</p></td>" +
                            "<td class = 'TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight'> " + value.coupon + "</p></td>" +
                            "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'> " + value.interest_payment_date_remarks + "</p></td>" +
                            "<td class = 'TableDataNumericData'> <p class='ReportTableFont  paraTextAlignRight'> " + formatDate(value.maturity_date) + "</p></td>";

                        $.each(divigroup, function (diviIndex, diviVal) {

                            if (diviVal[0] == value.dividend_month) {
                                html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.value) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.value == 0 ? '' : numberWithCommas(value.value.toFixed(2))) + "</p></td>";
                            }
                            else {
                                html = html + "<td class='TableDataNumericData'><p class='ReportTableFont'></p></td>";
                            }

                        });
                        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.value) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.value == 0 ? '' : numberWithCommas(value.value.toFixed(2))) + "</p></td>"
                            + "</tr>"
                    });
                });
                html = html + "</tbody>"


                $("#Performance_Bonds_Cash_Flow").append(html);
                $("#bheFinYearBond").text($('#ddlFinyear :selected').val());
                $("#bhefamilyNameBond").text($('#ddlFamilyList :selected').text());
                $("#bheclientNameBond").text(main_client_name);
                $('#BondTablesData').show();
                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

            }
            else {
                $("#theadBondsCashflow").html("");
                $("#tbodyBindingrealisedPerformanceBondsCashflow").html("");
                $("#noReFoundrealisedPerformanceBonds").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: completeajaxrequest
    })
}
function PerformanceholdingEquityPMSDetails(client_id, sub_category, PAN, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "ClientID": client_id,
        "Subcategory": sub_category
    }

    var posturl = "/ClientPortal/Reports/psprptperformanceholdingdirectequityreportDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var html = "";
            var main_client_name = "";
            var totalCorp = 0;
            var commenceDate = "";

            $("#txtPMSClient").val(client_id);

            $("#tbodyBindingrealisedPerformanceEquityPMS").html("");
            $("#noReFoundrealisedPerformanceEquityPMS").html("");
            if (data.length > 0) {
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].asset_name;
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }

                main_client_name = data[0].main_client_name;
                commenceDate = data[0].commence_date;
                totalCorp = parseInt(data[0].total_corpus);

                $.each(groups, function (index, value1) {
                    html = html + "<tr class='SubTotalRow'><td colspan='16'><p style='font-size:14px;font-family:Roboto, sans-serif; word-wrap:break-word; white-space:pre; font-weight:bold'> " + index + "</td></tr>";
                    $.each(value1, function (index1, value) {
                        html = html + "<tr>" +
                            "<td class='TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'> " + value.scrip_name + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.cdsl_quantity) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + value.quantity + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.avg_cost) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.cmp) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.cmp == 0 ? '' : numberWithCommas(value.cmp.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.hld_cost) + "'><p class='ReportTableFont  paraTextAlignRight'> " + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.market_Value) + "'><p class='ReportTableFont  paraTextAlignRight'> " + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</p></td> " +
                            "<td class='TableDataNumericData " + isNegativeValue(value.holding_per) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.dividend) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.srt_unreal_profit) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.long_unreal_profit) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.srt_real_profit) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.long_real_profit) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.srt_ttl_gain) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.lng_ttl_gain) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</p></td>" +
                            
                            //"<td class='TableDataNumericData " + isNegativeValue(value.return_abs) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                            //"<td class='TableDataNumericData " + isNegativeValue(value.return_xirr) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td>";
                        "</tr>"

                    });
                });

                $("#tbodyBindingrealisedPerformanceEquityPMS").append(html);
                $("#bheFinYearEquityPMS").text($('#ddlFinyear :selected').val());
                $("#bhefamilyNameEquityPMS").text($('#ddlFamilyList :selected').text());
                $("#pmsClientName").text(main_client_name);
                $("#dateOfJoinin").text(formatDate(commenceDate));
                $("#ttlCorpus").text(numberWithCommas(totalCorp.toFixed(2)));
                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

                $("#btnEqPMSPDFExport").show();
                $("#btnEqPMSExcelExport").show();
                $("#btnEMailEqPMSSummary").show();


                //completeajaxrequest();
            } else {
                $("#noReFoundrealisedPerformanceEquityPMS").html("");
                $("#noReFoundrealisedPerformanceEquityPMS").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}
function performanceholdingOtherPMS(client_id, sub_category, family_id,source, main_category) {
    var fyYear = $('#ddlFinyear :selected').val()
    var formdata = {
        "FINYRData": fyYear,
        "FamilyID": family_id,
        "ClientID": client_id,
        "Subcategory": sub_category,
        "main_category": main_category,
        "source": source,
    }
    console.log(formdata)
    var posturl = "/ClientPortal/Reports/psprptperformanceholdingdirectequityreportDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var main_client_name = "";
            var html = "";
            $("#txtOtherPMSClient").val(client_id);
            $("#txtOtherPMSFamily").val(family_id);
            $("#popsub_categoryOtherPMS").val(sub_category);

            $("#tbodyBindingPerformanceOtherPMS").html("");
            /* $("#noReFoundrealisedPerformanceEquityPMS").html("");*/
            if (data.length > 0) {
                //var groups = {};
                //for (var i = 0; i < data.length; i++) {
                //    var groupName = data[i].asset_name;
                //    if (!groups[groupName]) {
                //        groups[groupName] = [];
                //    }
                //    groups[groupName].push(data[i]);
                //}

                main_client_name = data[0].client_name;

                $("#flagason").text(formatDate(data[0].market_date));
                $("#SourceOtherPMS").val(data[0].source);

                //commenceDate = data[0].commence_date;
                //totalCorp = parseInt(data[0].total_corpus);
                //$.each(groups, function (index, value1) {
                //    html = html + "<tr class='SubTotalRow'><td colspan='16'><p style='font-size:14px;font-family:Roboto, sans-serif; word-wrap:break-word; white-space:pre; font-weight:bold'> " + index + "</td></tr>";
                $.each(data, function (index1, value) {
                    if (value.scrip_name == 'Total') {
                        html = html + "<tr class='SubTotalRow'><td class='TableDataNumericData'><p class='paraTextAlignLeft' style='font-size:14px;font-family:Roboto, sans-serif;'> " + value.scrip_name + "</p></td>" +
                            "<td class='TableDataNumericData'></td><td class='TableDataNumericData'></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.total_cost) + "</p></td>" +
                            "<td class='TableDataNumericData'></td><td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.market_Value) + "</p></td>" +
                            "<td class='TableDataNumericData'></td><td class='TableDataNumericData'></td><td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.profit_loss) + "</p></td>" +
                            "</tr> ";
                    }
                    else {
                        html = html + "<tr>" +
                            "<td class='TableDataNumericData'><p class='paraTextAlignLeft' style='font-size:14px;font-family:Roboto, sans-serif;'> " + value.scrip_name + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + value.quantity + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.unit_cost) + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.total_cost) + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + value.market_rate + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.market_Value) + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + formatDate(value.market_date) + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + value.hold_per + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.profit_loss) + "</p></td>"
                    }
                    /*});*/
                });

                $("#tbodyBindingPerformanceOtherPMS").append(html);
                $("#bheFinYearEquityPMS").text($('#ddlFinyear :selected').val());
                /* $("#bhefamilyNameEquityPMS").text($('#ddlFamilyList :selected').text());*/
                $("#txtOtherPMS_perfo").text(main_client_name);

                $("#bheclientNameOtherPMS").text(main_client_name);
                $("#bheFinYearOtherPMS").text(fyYear);
                $("#OtherPMSsubcategory").text(sub_category);


                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

                $("#btnOtherPMSPDFExport").show();
                $("#btnEqPMSExcelExport").show();
                $("#btnEMailOtherPMSSummary").show();

                //completeajaxrequest();
            } else {
                alert("Data not Found.")
                //$("#noReFoundrealisedPerformanceEquityPMS").html("");
                //$("#noReFoundrealisedPerformanceEquityPMS").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })

}
function performanceholdingFDDetails(client_id, sub_category, family_id) {


    var fyYear = $('#ddlFinyear :selected').val()
    var formdata = {
        "FINYRData": fyYear,
        "ClientID": client_id,  //passing main_client_id as client_id
        "Subcategory": sub_category,
    }
    var posturl = "/ClientPortal/Reports/psprptperformanceholdingdirectequityreportDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var main_client_name = "";
            var From_date = "";
            var To_date = "";
            var html = "";
            var html1 = "";

            $("#txtFDClient").val(client_id);
            $("#txtFDDetailsFamily").val(family_id);
            $("#sub_categoryFDDetails").val(sub_category);

            $("#tbodyBindingPerformanceFDDetails").html("");
            if (data.length > 0) {
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].rec_id;
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                main_client_name = data[0].client_name;
                From_date = formatDate(data[0].from_date)
                To_date = formatDate(data[0].to_date)
                PAN = data[0].pan_no;

                html1 = "Client : " + main_client_name + " (" + PAN + ")";

                $.each(groups, function (index, value1) {
                    html = html +
                        "<tr class='SubTotalRow'><td>" +
                        "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#FD_acc_" + value1[0].rec_id + "'" +
                        "aria-expanded='false' aria-controls='divi_acc_" + value1[0].rec_id + "'>" +
                        "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                        "</button>&nbsp;<b> Issuer : </b>" + value1[0].issuerName + "&nbsp;&nbsp;&nbsp;&nbsp<b> Scheme : </b>" + value1[0].schemeName + "&nbsp;&nbsp;&nbsp;&nbsp<b>Amount : </b>" + numberWithCommas(value1[0].amount) +
                        "&nbsp;&nbsp;&nbsp;&nbsp<b> Maturity Date : </b>" + formatDate(value1[0].maturity_Date) + "</p><td>" +
                        "</td></tr>"

                    html = html + "<tr><td>" +
                        "<div class='collapse' id='FD_acc_" + value1[0].rec_id + "'" +
                        "<div class='global_report_accords'>" + //For setting max height
                        "<table id='trds_Table_" + value1[0].rec_id + "' class='table table-hover w-100'>" +

                        "<tr><td class = 'SubTotalRow' colspan = '3'>" +
                        "&nbsp;&nbsp;&nbsp;&nbsp<b>  Interest Rate : </b>" + value1[0].fdInterestRate_perc + "% &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Coupon Type : </b>" + value1[0].fdCouponType +
                        "&nbsp;&nbsp;&nbsp;&nbsp<b> Interest Frequency : </b>" + value1[0].fdInterestFrequency +
                        "&nbsp;&nbsp;&nbsp;&nbsp<b>  Application Date : </b>" + formatDate(value1[0].fdApplicationDate) +
                        "</td></tr>" +

                        "<tr><th class='TableHeaderRow'><p><b>Cash Flow Date</b></p></th><th class='TableHeaderRow'><p><b>Cash Flow Amount</b></p></th><th class='TableHeaderRow'><p><b>Cash Flow Type </b></p></th></tr>"
                    $.each(value1, function (index1, value) {
                        html = html +
                            "<tr><td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignCenter'> " + formatDate(value.cash_flow_date) + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.cash_flow_amount.toFixed()) + "</p></td>" +
                            "<td class='TableDataNumericData'><p class='ReportTableFont  paraTextAlignRight'> " + value.cash_flow_type + "</p></td>"
                    });
                    html = html + "</table>" +
                        "</div>" + //accords end
                        "</div>" + //Collapse end
                        "</td></tr>"
                });

                $("#tbodyBindingPerformanceFDDetails").append(html);

                $("#dsp_clientDetails").append(html1);
                $("#txtFDclientName").text(main_client_name);
                /*$("#bheFinYearFDDetails").text(fyYear);*/
                $("#bhePANFDDetails").text(PAN);
                $("#report_start_date").text(From_date);
                $("#report_end_date").text(To_date);


                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

                $("#btnFDDetailsPDFExport").show();
                $("#btnFDDetailsExcelExport").show();
                $("#btnEMailFDDetailsSummary").show();

                //completeajaxrequest();
            } else {
                alert("No data Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}

function PerformanceholdingLiquiLoan(account_code, pan_number, client_name) {

    $("#btnLiquiLoanPDFExport").hide();
    $("#btnLiquiLoanExcelExport").hide();
    $("#btnPrintLiquiLoan").hide();
    $("#btnEMailLiquiLoanSummary").hide();

    $("#Performance_LiquiLoan_ClientDetails").show();


    $("#Liqui_FinYear").text($('#ddlFinyear :selected').val());
    $("#Liqui_Family_Name").text($('#ddlFamilyList :selected').text());

    $("#Liqui_Client_Name").text(client_name);
    $("#liqui_PAN").text(pan_number);

    fn_psp_dsp_liquiloan_investment(account_code)
    fn_psp_dsp_liquiloan_investor_dashboard(account_code)
    fn_psp_dsp_liquiloan_investor_ledger(account_code)

}
function fn_psp_dsp_liquiloan_investment(account_code) {

    var formdata = {
        "investor_id": account_code
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_liquiloan_investment";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            $("#tbody_Liquiloan_Investments").html("");
            
            var html = "";
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    html = html + "<tr>" +
                        "<td class='tableDataWithRedBorders paraTextAlignLeft'>" + formatDate(data[i].investment_Date) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignLeft'>" + data[i].scheme_details + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignRight'>" + (data[i].amount == 0 ? '' : (data[i].amount.toFixed(2))) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignRight'>" + (data[i].xirr == 0 ? '' : (data[i].xirr.toFixed(2))) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignRight'>" + (data[i].interest_Repaid == 0 ? '' : numberWithCommas(data[i].interest_Repaid.toFixed(2))) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignRight'>" + (data[i].principal_Repaid == 0 ? '' : numberWithCommas(data[i].principal_Repaid.toFixed(2))) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignRight'>" + (data[i].total_Repaid == 0 ? '' : numberWithCommas(data[i].total_Repaid.toFixed(2))) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignRight'>" + (data[i].portfolio_Value == 0 ? '' : numberWithCommas(data[i].portfolio_Value.toFixed(2))) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignLeft'>" + formatDate(data[i].end_Date) + "</td>" +
                        "</tr>";
                }
                $("#tbody_Liquiloan_Investments").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}

function fn_psp_dsp_liquiloan_investor_dashboard(account_code) {

    var formdata = {
        "investor_id": account_code
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_liquiloan_investor_dashboard";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                $("#Net_prin_Investment").text(numberWithCommas(data.net_Principal_Investment.toFixed(2)));
                $("#Port_Val").text(numberWithCommas(data.portfolio_Value.toFixed(2)));

            }
            else {
                /*alert("Data not found .")*/
                $("#Performance_LiquiLoan_ClientDetails").hide();
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
    })
}

function fn_psp_dsp_liquiloan_investor_ledger(account_code) {

    var formdata = {
        "investor_id": account_code
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_liquiloan_investor_ledger";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#tbody_Liquiloan_CashFlow").html("");
            var html = "";
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    html = html + "<tr>" +
                        "<td class='tableDataWithRedBorders paraTextAlignCenter'>" + formatDate(data[i].date) + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignCenter'>" + data[i].transaction_Type + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignCenter'>" + data[i].type + "</td>" +
                        "<td class='tableDataWithRedBorders paraTextAlignCenter'>" + numberWithCommas(data[i].amount.toFixed(2)) + "</td>" +
                        "</tr>";
                }
                $("#tbody_Liquiloan_CashFlow").append(html);
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

$("#btnBackDirectEquity").click(function () {
    $("#AssetAllocationLandingPage").show();
    $("#myModalst_Performance").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
$("#btnBackDirectEquityScriptwise").click(function () {

    var sub_cat = $("#DEbtnIndicator").val();

    if (sub_cat == "Direct Equity") {
        $("#myModalst_Performance").show();
        $("#myModalst_PerformanceScript").hide();
    }
    else {
        $("#ReitsInvits_Performance").show();
        $("#myModalst_PerformanceScript").hide();
    }

})
$("#btnBackBonds").click(function () {
    $("#AssetAllocationLandingPage").show();
    $("#myModalst_PerformanceBonds").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
$("#btnBackEqPMS").click(function () {
    $("#AssetAllocationLandingPage").show();
    $("#myModalst_PerformanceEquityPMS").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
$("#btnBackOtherPMS").click(function () {
    $("#AssetAllocationLandingPage").show();
    $("#myModalst_PerformanceOtherPMS").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
$("#btnBackLiquiLoan").click(function () {
    $("#AssetAllocationLandingPage").show();
    $("#LiquLoan_div").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
$("#btnBackFDDetails").click(function () {
    $("#AssetAllocationLandingPage").show();
    $("#myModalst_PerformanceFDDetails").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})

$("#btnExportPdfAssetAllocation").click(function () {

    GenerateExportDetails("#btnExportPdfAssetAllocation", "X", "PDF")

})
$("#btnExportExcelAssetAllocation").click(function () {

    GenerateExportDetails("#btnExportExcelAssetAllocation", "X", "EXCEL")

})
$("#btnEMailAssetAllocation").click(function () {

    GenerateExportDetails("#btnEMailAssetAllocation", "E", "PDF")

})

//Direct Equity Script List
$("#btnEMailDirectEquity").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#DEclient_id").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

    PDFandExcelExport("#btnEMailDirectEquity", "13", LoginId, famId, "E", "PDF", strPara);

})
$("#btnExportDEPDF").click(function () {

    var famId = $("#Curr_Fam_ID").val();
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#DEclient_id").val();


    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

    if (client_id != "0") {
        var strDescrip = "Direct Equity Performance Summary of " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }
    else { //For All clients
        var strDescrip = "Direct Equity Performance Summary of " + $('#ddlFamilyList :selected').text() + " " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }

    PDFandExcelExport("#btnExportDEPDF", "13", LoginId, famId, "X", "PDF", strPara, strDescrip);

})
$("#btnExportDEExcel").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#DEclient_id").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

    if (client_id != "0") {
        var strDescrip = "Direct Equity Performance Summary of " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }
    else { //For All clients
        var strDescrip = "Direct Equity Performance Summary of " + $('#ddlFamilyList :selected').text() + " " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }

    PDFandExcelExport("#btnExportDEExcel", "13", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
})
//Direct Equity Script List


//Direct Equity Scriptwise Report
$("#btnEMailDirectEquityScriptwise").click(function () {

    var Fin_year = $('#ddlFinyear :selected').val();
    var main_client_id = $("#DEMainClientID").val();
    var account_code = $("#DEAccountCode").val();
    var scrip_code = $("#DEScriptCode").val();
    var famId = $("#Curr_Fam_ID").val()

    var LoginId = $("#LoggedInUID").val()

    var strPara = Fin_year + "~" + main_client_id + "~" + scrip_code + "~" + "Direct Equity" + "~" + account_code + "~" + "Equity";

    PDFandExcelExport("#btnEMailDirectEquityScriptwise", "14", LoginId, famId, "E", "PDF", strPara);

})
$("#btnDEScriptPDF").click(function () {

    var Fin_year = $('#ddlFinyear :selected').val();
    var main_client_id = $("#DEMainClientID").val();
    var account_code = $("#DEAccountCode").val();
    var scrip_code = $("#DEScriptCode").val();
    var famId = $("#Curr_Fam_ID").val()

    var LoginId = $("#LoggedInUID").val()

    var strPara = Fin_year + "~" + main_client_id + "~" + scrip_code + "~" + "Direct Equity" + "~" + account_code + "~" + "Equity";

    var strDescrip = $("#DEScriptClientName").text() + "'s Equity Script Performance Summary for " + $("#DEScriptName").text() +
        " for year " + $('#ddlFinyear :selected').text();

    PDFandExcelExport("#btnDEScriptPDF", "14", LoginId, famId, "X", "PDF", strPara, strDescrip);

})
$("#btnDEScriptExcel").click(function () {

    var Fin_year = $('#ddlFinyear :selected').val();
    var main_client_id = $("#DEMainClientID").val();
    var account_code = $("#DEAccountCode").val();
    var scrip_code = $("#DEScriptCode").val();
    var famId = $("#Curr_Fam_ID").val()

    var LoginId = $("#LoggedInUID").val()

    var strPara = Fin_year + "~" + main_client_id + "~" + scrip_code + "~" + "Direct Equity" + "~" + account_code + "~" + "Equity";

    var strDescrip = $("#DEScriptClientName").text() + "'s Equity Script Performance Summary for " + $("#DEScriptName").text() +
        " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnDEScriptExcel", "14", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//Direct Equity Scriptwise Report

//Other PMS
$("#btnOtherPMSPDFExport").click(function () {

    var famId = $("#txtOtherPMSFamily").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtOtherPMSClient").val();
    var sub_category = $("#popsub_categoryOtherPMS").val();
    var source = $("#SourceOtherPMS").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + source + "~" + sub_category;
    var strDescrip = "Other PMS Performance Summary for " + $("#txtOtherPMS_perfo").text() + " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnOtherPMSPDFExport", "54", LoginId, famId, "X", "PDF", strPara, strDescrip);

})
$("#btnOtherPMSExcelExport").click(function () {

    var famId = $("#txtOtherPMSFamily").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtOtherPMSClient").val();
    var sub_category = $("#popsub_categoryOtherPMS").val();
    var source = $("#SourceOtherPMS").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + source + "~" + sub_category;

    var strDescrip = "Other PMS Performance Summary for " + $("#txtOtherPMS_perfo").text() + " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnOtherPMSExcelExport", "54", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
})
$("#btnEMailOtherPMSSummary").click(function () {

    var famId = $("#txtOtherPMSFamily").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtOtherPMSClient").val();
    var sub_category = $("#popsub_categoryOtherPMS").val();
    var source = $("#SourceOtherPMS").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + source + "~" + sub_category;

    PDFandExcelExport("#btnEMailOtherPMSSummary", "54", LoginId, famId, "E", "PDF", strPara);

})
//Other PMS

//ReITs and InvITs Script List
//$("#btnEMailRnI").click(function () {

//    var famId = $("#Curr_Fam_ID").val()
//    var Fin_year = $('#ddlFinyear :selected').val()
//    var LoginId = $("#LoggedInUID").val()
//    var client_id = $("#DEclient_id").val();

//    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

//    PDFandExcelExport("#btnEMailRnI", "13", LoginId, famId, "E", "PDF", strPara);

//})

//$("#btnExportRnIPDF").click(function () {

//    var famId = $("#Curr_Fam_ID").val()
//    var Fin_year = $('#ddlFinyear :selected').val()
//    var LoginId = $("#LoggedInUID").val()
//    var client_id = $("#DEclient_id").val();


//    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

//    if (client_id != "0") {
//        var strDescrip = "ReITs and InvITs Performance Summary of " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
//    }
//    else { //For All clients
//        var strDescrip = "ReITs and InvITs Performance Summary of " + $('#ddlFamilyList :selected').text() + " " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
//    }

//    PDFandExcelExport("#btnExportRnIPDF", "13", LoginId, famId, "X", "PDF", strPara, strDescrip);

//})

//$("#btnExportRnIExcel").click(function () {

//    var famId = $("#Curr_Fam_ID").val()
//    var Fin_year = $('#ddlFinyear :selected').val()
//    var LoginId = $("#LoggedInUID").val()
//    var client_id = $("#DEclient_id").val();

//    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

//    if (client_id != "0") {
//        var strDescrip = "ReITs and InvITs Performance Summary of " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
//    }
//    else { //For All clients
//        var strDescrip = "ReITs and InvITs Performance Summary of " + $('#ddlFamilyList :selected').text() + " " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
//    }

//    PDFandExcelExport("#btnExportRnIExcel", "13", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
//})
//ReITs and InvITs Script List

//Eq PMS
$("#btnEMailEqPMSSummary").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtPMSClient").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "Equity PMS";

    PDFandExcelExport("#btnEMailEqPMSSummary", "27", LoginId, famId, "E", "PDF", strPara);

})
$("#btnEqPMSPDFExport").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtPMSClient").val();


    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "Equity PMS";

    var strDescrip = "Equity PMS Performance Summary for " + $("#pmsClientName").text() + " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnEqPMSPDFExport", "27", LoginId, famId, "X", "PDF", strPara, strDescrip);

})
$("#btnEqPMSExcelExport").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtPMSClient").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "Equity PMS";

    var strDescrip = "Equity PMS Performance Summary for " + $("#pmsClientName").text() + " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnEqPMSExcelExport", "27", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
})
//Eq PMS

//Bonds Summary Report
function EqDebtMFBondsMail(btn_id, Report_Type, Report_format) {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#common_client_id_holder").val();
    var sub_cat = "Bonds"
    var report_id = "17";
    var client_name = $("#bheclientNameBonds").text()

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + sub_cat;

    var strDescrip = sub_cat + " Performance Summary for " + client_name +
        " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport(btn_id, report_id, LoginId, famId, Report_Type, Report_format, strPara, strDescrip);
}
$("#btnEMailBonds").click(function () {

    EqDebtMFBondsMail("#btnEMailBonds", "E", "PDF");
})
$("#btnBondsPDFExport").click(function () {

    EqDebtMFBondsMail("#btnBondsPDFExport", "X", "PDF");
})
$("#btnBondsExcelExport").click(function () {

    EqDebtMFBondsMail("#btnBondsExcelExport", "X", "EXCEL");
})
//Bonds Summary Report

//FD Details
$("#btnFDDetailsPDFExport").click(function () {

    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtFDClient").val();
    var famId = "0"

    var strPara = LoginId + "~" + Fin_year + "~" + client_id;

    var strDescrip = "Fixed Deposit Details for " + $("#txtFDclientName").text() + " for " + Fin_year

    PDFandExcelExport("#btnFDDetailsPDFExport", "55", LoginId, famId, "X", "PDF", strPara, strDescrip);

})
$("#btnFDDetailsExcelExport").click(function () {

    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtFDClient").val();
    var famId = "0"

    var strPara = LoginId + "~" + Fin_year + "~" + client_id;

    var strDescrip = "Fixed Deposit Details for " + $("#txtFDclientName").text() + " for " + Fin_year
    PDFandExcelExport("#btnFDDetailsExcelExport", "55", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
})
$("#btnEMailFDDetailsSummary").click(function () {

    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtFDClient").val();
    var famId = "0"


    var strPara = LoginId + "~" + Fin_year + "~" + client_id;
    PDFandExcelExport("#btnEMailFDDetailsSummary", "55", LoginId, famId, "E", "PDF", strPara);

})
//FD Details
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()
    let LoginHash = $("#LoggedInUserHash").val()
    let access_token = "";
    let family_id = $('#ddlFamilyList :selected').val();
    let Fy_Year = $('#ddlFinyear :selected').val();

    let txt_family = $('#ddlFamilyList :selected').text();

    let strPara = LoginId + "~" + access_token + "~" + family_id + "~" + Fy_Year;

    let strDescrip = "Asset Allocation Report for " + txt_family + " for FY " + Fy_Year


    PDFandExcelExport(evt_btn, "57", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

function create_Chart(xValues, yValues, ChartID, ChartTitle) {

    barColors = chartcolour;

    //window.mychart = new Chart(ChartID, {
    myChart = new Chart(ChartID, {
        type: "pie",
        data: {
            labels: xValues,

            datasets: [{
                backgroundColor: barColors,
                data: yValues,
                //borderColor: [
                //    'rgba(255, 99, 132, 1)',
                //    'rgba(54, 162, 235, 1)',
                //    'rgba(255, 206, 86, 1)',
                //    'rgba(75, 192, 192, 1)',
                //    'rgba(153, 102, 255, 1)',
                //    'rgba(255, 159, 64, 1)'
                //],
                // borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            showTooltips: true,
            //plugins: {
            //    tooltip: {
            //        callbacks: {
            //            label: function (tooltipItem) {
            //                tooltipItem.label + ': ' + tooltipItem.raw.toFixed(2);
            //            }
            //        }
            //    }
            //},
            //title: {
            //    display: true,
            //     text: ChartTitle
            //},
            "legend": {
                "display": false,
                "position": "bottom",
                "align": "center"
            }
        }
    });

}