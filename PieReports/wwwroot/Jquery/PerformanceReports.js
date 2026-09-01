$(window).bind("load", function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    //gtag('config', 'G-6FJ1RE3WC0'); 
    gtag('config', 'G-6FJ1RE3WC0', {
        'page_title': 'PerformanceReports',
        'page_path': '/Reports/PerformanceReports'
    });
    gtag('event', 'PerformanceReports', {
        'event_category': 'PerformanceReports',
        'event_label': 'PerformanceReports'
    });

    fnEnableFamilyList();
    fnEnableFinyearList();


    $("#ddlFinyear").change(function () {
        $("#myModalst_PerformanceLandingPage").show();
        $("#myModalst_Performance").hide();
        $("#myModalst_PerformanceEquity_MF").hide();
        $("#myModalst_PerformanceBonds").hide();
        $("#myModalst_PerformanceEquityPMS").hide();
        $("#myModalst_PerformanceOtherPMS").hide();
        $("#myModalst_PerformanceFDDetails").hide();

        PerformanceholdingDetails();
    });
    $("#ddlFamilyList").change(function () {
        $("#myModalst_PerformanceLandingPage").show();
        $("#myModalst_Performance").hide();
        $("#myModalst_PerformanceEquity_MF").hide();
        $("#myModalst_PerformanceBonds").hide();
        $("#myModalst_PerformanceEquityPMS").hide();
        $("#myModalst_PerformanceOtherPMS").hide();
        $("#myModalst_PerformanceFDDetails").hide();
        fnSetCurrFamId();

        PerformanceholdingDetails();
    });

    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_Performance").hide();
    $("#myModalst_PerformanceEquity_MF").hide();
    $("#myModalst_PerformanceBonds").hide();
    $("#myModalst_PerformanceEquityPMS").hide();
    $("#myModalst_PerformanceOtherPMS").hide();
    $("#myModalst_PerformanceFDDetails").hide();

    setTimeout(function () {
        PerformanceholdingDetails();
        fnSetCurrFamId();
    }, 1000);
});
//Loader
function completeajaxrequest() {
    $("#waitIn").css("display", "none");
}
function startajaxrequest() {
    $("#waitIn").css("display", "block");
}
//Loader

function PerformanceholdingDetails() {

    var formdata = {
        "FINYR": $('#ddlFinyear :selected').val(),
        "Family": $('#ddlFamilyList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/psprptperformanceholdingreportDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            var htmlAll = "";
            var html = "";
            $("#tbodyBindingPerformanceAll").html("");
            $("#tbodyBindingPerformance").html("");
            $("#noReFound").html("");
            if (data != null) {
                if (data.length > 0) {
                    var groups = {};
                    for (var i = 0; i < data.length; i++) {
                        var groupName = data[i].main_client_name_header;
                        if (groupName !== "All Clients") {
                            if (!groups[groupName]) {
                                groups[groupName] = [];
                            }
                            groups[groupName].push(data[i]);
                        }
                    }
                    //For Individual clients
                    $.each(groups, function (index, value1) {
                        html = html + "<tr class = 'GrandTotalRow'><td class = 'GrandTotalRowData' colspan = 14><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold; text-align: left !important;'>" +
                            "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#div_" + value1[0].main_client_id + "' aria-expanded='false' aria-controls='#div_" + value1[0].main_client_id + "'>" +
                            "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i></button>&nbsp;&nbsp;" +
                            index + "</td></tr> <tr><td><div class='collapse' id='div_" + value1[0].main_client_id + "'data-parent='#htmltopdf'><table class='table table-hover'>" +
                            "<thead><tr>" +
                            "<th class='TableHeaderRow'><p><b>Sub Category</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Contribution</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Holding cost</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Market Value</b></p></th>" +
                            "<th class='TableHeaderRow'><p style='width:max-content'><b>% Holding</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Unrealised<br />Short Term<br />Profit /</b><b style='color: red;'>(Loss)</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Unrealised<br />Long Term<br />Profit /</b><b style='color: red;'>(Loss)</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Realised<br />Short Term<br />Profit /</b><b style='color: red;'>(Loss)</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Realised<br />Long Term<br />Profit /</b><b style='color: red;'>(Loss)</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Total<br />Short Term<br />Profit /</b><b style='color: red;'>(Loss)</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Total<br />Long Term<br />Profit /</b><b style='color: red;'>(Loss)</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>Dividend<br>/Interest</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>ABS %</b></p></th>" +
                            "<th class='TableHeaderRow'><p><b>XIRR %</b></p></th>" +
                            "</tr></thead>";
                        $.each(value1, function (index1, value) {
                            if (value.sub_category_display_order == "1") {
                                //if (value.sub_category != "Equity Total" && value.sub_category != "Debt Total" && value.sub_category != "Grand Total" && value.sub_category != "AIF Total" && value.sub_category != "PMS Total") {
                                html = html +
                                    "<tr><td style='cursor: pointer;border: 1px solid #ff0000; text-decoration:underline;' onclick=\"myFunctionloadPerformance(\'" + (value.sub_category == "Direct Equity" || value.sub_category == "InvITs" ||
                                        value.sub_category == "ReITs" || value.sub_category == "Equity PMS" || value.sub_category == "Liqui Loan" || value.sub_category == "Emerging Corporates India Portfolio" ||
                                        value.sub_category == "Moat And Special Situations Portfolio" || value.sub_category == "Marcellus Consistent Compounders Portfolio" || value.sub_category == "Marcellus Little Champs Portfolio" ||
                                        value.sub_category == "Marcellus Kings Of Capital Portfolio" || value.sub_category == "Buoyant Opportunities Scheme" || value.sub_category == "Buoyant Opportunities Strategy - Investor" ||
                                        value.sub_category == "GIRIK MULTICAP GROWTH EQUITY STRATEGY" || value.sub_category == "GIRIK LIQUID STRATEGY" || value.sub_category == "InCred Healthcare Portfolio" ||
                                        value.sub_category == "WHITE OAK INDIA PIONEERS EQUITY PORTFOLIO" ? value.client_id : value.main_client_id) + "\' ,\'" + value.sub_category + "\' ,\'"
                                    + value.pan_number + "\',\'" + value.account_code + "\',\'" + value.main_client_id + "\') \">" + value.sub_category_display + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.contribution) + "'>" + (value.contribution == 0 ? '' : numberWithCommas(value.contribution.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.hld_cost) + "'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.market_Value) + "'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.holding_per) + "'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_unreal_profit) + "'>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.long_unreal_profit) + "'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_real_profit) + "'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.long_real_profit) + "'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_ttl_gain) + "'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.lng_ttl_gain) + "'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData' style='cursor:pointer; text-decoration:underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + value.sub_category + "\')\">" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.return_abs) + "'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.return_xirr) + "'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</td>"
                                    + "</tr>";
                            }
                            else if (value.sub_category_display_order == "2") {
                                //else {
                                //if (value.sub_category == "Equity Total" || value.sub_category == "Debt Total" || value.sub_category == "PMS Total" || value.sub_category == "AIF Total") {
                                html = html +
                                    "<tr class = 'SubTotalRow'><td class = 'TableDataNumericData' style = 'font-weight:bold; text-align: left !important;'>" + value.sub_category_display + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.contribution) + "'>" + (value.contribution == 0 ? '' : numberWithCommas(value.contribution.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.hld_cost) + "'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.market_Value) + "'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.holding_per) + "'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_unreal_profit) + "'>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.long_unreal_profit) + "'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_real_profit) + "'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.long_real_profit) + "'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_ttl_gain) + "'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.lng_ttl_gain) + "'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData' style = 'cursor: pointer;'text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + value.sub_category + "\')\">" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.return_abs) + "'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</td>" +
                                    "<td class = 'TableDataNumericData " + isNegativeValue(value.return_xirr) + "'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</td>"
                                    + "</tr><tr><td></td></tr>";
                            }
                            else if (value.sub_category_display_order == "3") {
                                html = html +
                                    "<tr class = 'GrandTotalRow'><td class = 'GrandTotalRowData' style = 'font-weight:bold; text-align: left !important;'>" + value.sub_category_display + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.contribution) + "'>" + (value.contribution == 0 ? '' : numberWithCommas(value.contribution.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.hld_cost) + "'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.market_Value) + "'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.holding_per) + "'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</td>" +

                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_unreal_profit) + "'>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.long_unreal_profit) + "'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</td>" +

                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_real_profit) + "'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.long_real_profit) + "'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</td>" +

                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_ttl_gain) + "'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.lng_ttl_gain) + "'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</td>" +

                                    "<td class = 'GrandTotalRowData' style = 'cursor: pointer; text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + value.sub_category + "\')\">" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.return_abs) + "'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</td>" +
                                    "<td class = 'GrandTotalRowData " + isNegativeValue(value.return_xirr) + "'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</td>"
                                    + "</tr>"
                                    + "<tr><td></td></tr>";
                            }
                        });
                        html = html + "</table></div></td></tr>";
                    });

                    //For All Clients
                    $.each(data, function (index, value) {
                        if (value.main_client_name == "All Clients") {

                            if (value.sub_category_display_order == "1") {
                                //if (value.sub_category != "Equity Total" && value.sub_category != "Debt Total" && value.sub_category != "Grand Total" && value.sub_category != "AIF & PMS Total") {
                                if (value.sub_category == "Direct Equity" || value.sub_category == "Equity MF" || value.sub_category == "Debt MF" /*|| value.sub_category == "Bonds"*/) {
                                    htmlAll = htmlAll +
                                        "<tr><td style='cursor: pointer;border: 1px solid #ff0000; text-decoration:underline;' onclick=\"myFunctionloadPerformance(\'0\' ,\'" + value.sub_category + "\' ,\'" + value.pan_number + "\',\'" + value.account_code + "\',\'" + value.main_client_id + "\') \">" + value.sub_category_display + "</td>";
                                }
                                else {
                                    htmlAll = htmlAll +
                                        "<tr><td style='border: 1px solid #ff0000;'>" + value.sub_category_display + "</td>";
                                }
                                htmlAll = htmlAll +

                                    //"<tr><td style='font-size:14px;font-family:Roboto, sans-serif; border: 1px solid #ff0000;'>" + value.sub_category_display + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.contribution) + "'>" + (value.contribution == 0 ? '' : numberWithCommas(value.contribution.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.hld_cost) + "'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.market_Value) + "'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.holding_per) + "'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</td>" +

                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.srt_unreal_profit) + "'>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.long_unreal_profit) + "'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.srt_real_profit) + "'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.long_real_profit) + "'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.srt_ttl_gain) + "'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.lng_ttl_gain) + "'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData' style = 'cursor: pointer; text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + value.sub_category + "\')\">" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</td>"
                                    + "</tr>"
                            }

                            else if (value.sub_category_display_order == "2") {
                                //if (value.sub_category == "Equity Total" || value.sub_category == "Debt Total" || value.sub_category == "AIF & PMS Total") {
                                htmlAll = htmlAll +
                                    "<tr class = 'SubTotalRow'><td style='border-right: 1px solid #ff0000; border-left: 1px solid #ff0000;'><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold; text-align: left !important;'> " + value.sub_category_display + "</p></td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.contribution) + "''>" + (value.contribution == 0 ? '' : numberWithCommas(value.contribution.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.hld_cost) + "''>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.market_Value) + "''>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.holding_per) + "''>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</td>" +

                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.srt_unreal_profit) + "''>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.long_unreal_profit) + "''>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.srt_real_profit) + "''>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.long_real_profit) + "''>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.srt_ttl_gain) + "''>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</td>" +
                                    "<td class = 'TableDataNumericData" + isNegativeValue(value.lng_ttl_gain) + "''>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</td>" +

                                    "<td class = 'TableDataNumericData' style = 'cursor: pointer; text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + value.sub_category + "\')\">" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</td>"
                                    + "</tr>"

                            }
                            else if (value.sub_category_display_order == "3") {
                                htmlAll = htmlAll +
                                    "<tr class = 'GrandTotalRow'><td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold; text-align: left !important;'> " + value.sub_category_display + "</p></td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.contribution) + "''>" + (value.contribution == 0 ? '' : numberWithCommas(value.contribution.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.hld_cost) + "''>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.market_Value) + "''>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.holding_per) + "''>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</td>" +

                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.srt_unreal_profit) + "''>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.long_unreal_profit) + "''>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed())) + "</td>" +

                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.srt_real_profit) + "''>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.long_real_profit) + "''>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed())) + "</td>" +

                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.srt_ttl_gain) + "''>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed())) + "</td>" +
                                    "<td class = 'GrandTotalRowData" + isNegativeValue(value.lng_ttl_gain) + "''>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed())) + "</td>" +

                                    "<td class = 'GrandTotalRowData' style = 'cursor: pointer; text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + value.sub_category + "\')\">" + (value.dividend == 0 ? '' : numberWithCommas(value.dividend.toFixed())) + "</td>"
                                    + "</tr>"
                            }
                        }
                    });

                    $("#tbodyBindingPerformanceAll").append(htmlAll);
                    $("#tbodyBindingPerformance").append(html);
                    $("#bFinYear").text($('#ddlFinyear :selected').text());
                    $("#bfamilyName").text($('#ddlFamilyList :selected').text());
                    $('#binddata').show();
                    $('#PerfHeader').show();
                    $('#liRemoveFavourite').show();
                    $('#noshow').show();
                    $("#noReFoundPerformance").html("");

                    $("#btnEMailMainPerfSummary").show();
                    $("#btnExportExcel").show();
                    $("#btnExportPdf").show();

                    var senddata = {
                        "Family_Token": $('#ddlFamilyList :selected').val()
                    }
                    var posturl = "/ClientPortal/Reports/SetFamilyID";
                    $.ajax({
                        url: posturl,
                        type: "post",
                        contentType: "application/json",
                        data: JSON.stringify(senddata),
                        success: function (data) {
                            $("#Curr_Family_Id").val(data.family_Id);
                        },
                        error: function (xhr, err) {
                            alert(err);
                            return "Error";
                        }
                    })

                }
                else {
                    $("#tbodyBindingPerformanceAll").html("");
                    $("#tbodyBindingPerformance").html("");
                    $("#bFinYear").text($('#ddlFinyear :selected').val());
                    $("#bfamilyName").text($('#ddlFamilyList :selected').text());
                    $('#PerfHeader').hide();
                    $("#noReFoundPerformance").html("No Record Found");
                    $('#binddata').hide();
                    $('#liRemoveFavourite').hide();
                    $('#noshow').hide();

                    $("#Curr_Family_Id").val("Error");
                }
            }
            else {
                $("#tbodyBindingPerformanceAll").html("");
                $("#tbodyBindingPerformance").html("");
                $("#bFinYear").text($('#ddlFinyear :selected').val());
                $("#bfamilyName").text($('#ddlFamilyList :selected').text());
                $('#PerfHeader').hide();
                $("#noReFoundPerformance").html("No Record Found. Please select different Family from above.");
                $('#binddata').hide();
                $('#liRemoveFavourite').hide();
                $('#noshow').hide();
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

var client_id1, sub_category1, family_id1, fin_year1;

function myFunctionloadDivident(st_pnl, client_id, sub_category) {
    if (st_pnl != "0") {
        $("#myModalst_dividend").modal({ backdrop: 'static', keyboard: false });
        $("#noReFoundrealisedPerformance").html("");
        $("#tbodyBindingrealisedPerformance").html("");
        client_id1 = client_id;
        sub_category1 = sub_category;
        family_id1 = $('#ddlFamilyList :selected').val();
        fin_year1 = $('#ddlFinyear :selected').val();
        loadDividentXIRRDetails(client_id, sub_category);
    } else {
        alert("value will zero");
    }

};

//Split on basis of subcat
function myFunctionloadPerformance(client_id, sub_category, pan_number = null, account_code = null, main_client_id = null) {
    var family_id, assset_code;

    $("#common_cat_holder").val(sub_category);
    $("#common_client_id_holder").val(client_id);

    family_id = $("#Curr_Family_Id").val();

    if (sub_category == "Direct Equity" || sub_category == "InvITs" || sub_category == "ReITs") {

        if (sub_category == "Direct Equity") {
            assset_code = "Equity";
            $("#myModalst_PerformanceLandingPage").hide();
            $("#myModalst_PerformanceEquity_MF").hide();
            $("#myModalst_Performance").show();

            $("#tbodyBindingrealisedDividend").html("");
            $("#noReFoundrealiseddivindend").html("");
            //$("#popclient_id").val(client_id);
            $("#popsub_category").val(sub_category);
            PerformanceholdingDirectEquityDetails(client_id, sub_category, pan_number, family_id, main_client_id, account_code, assset_code);
        }
        else {
            assset_code = sub_category;
            $("#myModalst_PerformanceLandingPage").hide();
            $("#myModalst_PerformanceEquity_MF").hide();
            $("#ReitsInvits_Performance").show();

            PerformanceholdingDirectEquityDetails(client_id, sub_category, pan_number, family_id, main_client_id, account_code, assset_code);
        }

    }
    else if (sub_category == "Equity MF" || sub_category == "Debt MF") {
        assset_code = "Mutual Fund";
        $("#Nav_Pill_AMC").css("display", "none")
        $("#Nav_Pill_Cat").css("display", "none")
        $("#Nav_Pill_Scheme").css("display", "none")
        $("#Nav_Pill_SIP").css("display", "none")
        $("#Nav_Pill_STP").css("display", "none")

        PerformanceholdingEquityMFDetails(client_id, sub_category, pan_number, family_id);

        MFAMCDetalis(client_id, sub_category, family_id);
        MFCategoryDetails(client_id, sub_category, family_id);
        MFSchemeDetalis(client_id, sub_category, family_id);

        MFSIPDetalis(client_id, sub_category, family_id);
        MFSTPDetalis(client_id, sub_category, family_id);

        $("#myModalst_PerformanceLandingPage").hide();
        $("#myModalst_Performance").hide();
        $("#myModalst_PerformanceEquity_MF").show();

        $("#tbodyBindingrealisedDividend").html("");
        $("#noReFoundrealiseddivindend").html("");
        $("#popclient_idEquityMF").val(client_id); //Required for mailing
        $("#popsub_categoryEquityMF").val(sub_category);

    }
    else if (sub_category == "Bonds") {
        assset_code = "Equity";
        $("#myModalst_Performance").hide();
        $("#myModalst_PerformanceLandingPage").hide();
        $("#myModalst_PerformanceEquity_MF").hide();
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
        $("#myModalst_PerformanceLandingPage").hide();
        $("#myModalst_PerformanceEquityPMS").show();

        $("#tbodyBindingrealisedPerformanceEquityPMS").html("");
        $("#noReFoundrealisedPerformanceEquityPMS").html("");
        $("#popclient_idEquityPMS").val(client_id);
        $("#popsub_categoryEquityPMS").val(sub_category)
        PerformanceholdingEquityPMSDetails(client_id, sub_category, pan_number, family_id);
    }
    else if (sub_category == "Emerging Corporates India Portfolio" || sub_category == "Moat And Special Situations Portfolio" || sub_category == "Marcellus Consistent Compounders Portfolio" ||
        sub_category == "Marcellus Little Champs Portfolio" || sub_category == "Marcellus Kings Of Capital Portfolio" || sub_category == "Buoyant Opportunities Scheme" ||
        sub_category == "Buoyant Opportunities Strategy - Investor" || sub_category == "GIRIK MULTICAP GROWTH EQUITY STRATEGY" ||
        sub_category == "GIRIK LIQUID STRATEGY" || sub_category == "InCred Healthcare Portfolio" || sub_category == "WHITE OAK INDIA PIONEERS EQUITY PORTFOLIO") {
        /* assset_code = "Equity";*/
        $("#myModalst_PerformanceLandingPage").hide();
        $("#myModalst_PerformanceOtherPMS").show();

        $("#tbodyBindingPerformanceOtherPMS").html("");
        //$("#noReFoundrealisedPerformanceEquityPMS").html("");
        $("#popclient_idOtherPMS").val(client_id);
        $("#popsub_categoryOtherPMS").val(sub_category)

        performanceholdingOtherPMS(client_id, sub_category, family_id);
    }
    else if (sub_category == "Liqui Loan") {

        assset_code = "Equity";

        $("#myModalst_PerformanceLandingPage").hide();
        $("#LiquLoan_div").show();
        $("#ddlFinyear").attr("disabled", true);
        $("#ddlFamilyList").attr("disabled", true);

        PerformanceholdingLiquiLoan(account_code);
    }
    else if (sub_category == "Fixed Deposit") {

        $("#myModalst_PerformanceLandingPage").hide();
        $("#myModalst_PerformanceFDDetails").show();

        $("#tbodyBindingPerformanceFDDetails").html("");
        $("#txtFDClient").val(client_id);
        $("#sub_categoryFDDetails").val(sub_category)

        performanceholdingFDDetails(client_id, sub_category, family_id);
    }

    $("#popclient_id").val(client_id);
    $("#popsub_category").val(sub_category);
};

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

            $("#noReFoundrealisedRnIPerformance").html("");
            $("#noReFoundrealisedPerformance").html("");

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
    else if (sub_category == "Equity MF" || sub_category == "Debt MF") {
        $("#tbodyBindingrealisedPerformanceScript").html("");
        $("#noReFoundrealisedPerformanceScript").html("");

        PerformanceholdingDetailsScheme(main_client_id, scrip_code, asset_name, account_code, sub_category);

        $("#myModalst_PerformanceEquity_MF").hide();


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
                $("#ScriptFinYear").text($('#ddlFinyear :selected').val());
                $("#ScriptfamilyName").text($('#ddlFamilyList :selected').text());



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

function PerformanceholdingDetailsScheme(main_client_id, scrip_code, asset_name, account_code, sub_category) {
    var formdata = {
        "JSON_main_client_code": main_client_id,
        "JSON_scrip_code": scrip_code,
        "JSON_sub_category": sub_category,
        "JSON_folio_no": ""
    }

    $("#txtEquiDebtMFMainClient").val(main_client_id);
    $("#txtEquiDebtMFScrip").val(scrip_code);
    $("#txtEquiDebtMFSubCat").val(sub_category);

    var posturl = "/ClientPortal/Reports/psp_dsp_mf_transction_details";
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
            $("#tbodyBindingMFSchemePerf").html("");
            $("#noReFoundMFSchemePerf").html("");
            $("#ScriptClientName").html("");
            $("#ScriptName").html("");
            if (data.length > 0) {

                $("#ScriptClientName").text(data[0].client_name);
                $("#ScriptName").text(data[0].script_name);
                $.each(data, function (index, value) {
                    if (value.tr_type == "Purchase") {
                        html = html + "<tr><td><p class='ReportTableFont paraTextAlignLeft'> " + formatDate(value.invdate) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.tr_type + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.invvalue) + "'>" + numberWithCommas(value.invvalue.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.nav) + "'> " + numberWithCommas(value.nav.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.units) + "'>" + numberWithCommas(value.units.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.bal_units) + "'> " + numberWithCommas(value.bal_units.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.no_of_days) + "'> " + numberWithCommas(value.no_of_days.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + numberWithCommas(value.cmp.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.mktvalue) + "'> " + numberWithCommas(value.mktvalue.toFixed(2)) + "</p></td>" +

                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.urpl) + "'> " + numberWithCommas(value.urpl.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rpl) + "'> " + numberWithCommas(value.rpl.toFixed(2)) + "</p></td >" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.inc_dividend) + "'> " + numberWithCommas(value.inc_dividend.toFixed(2)) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_abs) + "'> " + numberWithCommas(value.return_abs.toFixed(2)) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_xirr) + "'> " + numberWithCommas(value.return_xirr.toFixed(2)) + "</p></td></tr>";
                    }
                    else if (value.tr_type == "Dividend") {
                        html = html + "<tr><td><p class='ReportTableFont paraTextAlignLeft'> " + formatDate(value.invdate) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.tr_type + "</p></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.bal_units) + "'> " + numberWithCommas(value.bal_units.toFixed(2)) + "</p></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td></td>" +

                            "<td></td>" +
                            "<td></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.inc_dividend) + "'> " + numberWithCommas(value.inc_dividend.toFixed(2)) + "</p></td > " +
                            "<td></td>" +
                            "<td></td></tr>";
                    }
                    else if (value.tr_type == "Redemption") {
                        html = html + "<tr><td><p class='ReportTableFont paraTextAlignLeft'> " + formatDate(value.invdate) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.tr_type + "</p></td>" +
                            "<td><p style='font-size:14px;font-family:Roboto, sans-serif; color:red !important; text-align:right;'>" + numberWithCommas(value.invvalue.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont  paraTextAlignRight'> " + numberWithCommas(value.nav.toFixed(2)) + "</p></td>" +
                            "<td><p style='font-size:14px;font-family:Roboto, sans-serif; color:red !important; text-align:right;'>" + numberWithCommas(value.units.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.bal_units) + "'> " + numberWithCommas(value.bal_units.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.no_of_days) + "'> " + numberWithCommas(value.no_of_days.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + numberWithCommas(value.cmp.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.mktvalue) + "'> " + numberWithCommas(value.mktvalue.toFixed(2)) + "</p></td>" +

                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.urpl) + "'> " + numberWithCommas(value.urpl.toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rpl) + "'> " + numberWithCommas(value.rpl.toFixed(2)) + "</p></td >" +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.inc_dividend) + "'> " + numberWithCommas(value.inc_dividend.toFixed(2)) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_abs) + "'> " + numberWithCommas(value.return_abs.toFixed(2)) + "</p></td > " +
                            "<td><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_xirr) + "'> " + numberWithCommas(value.return_xirr.toFixed(2)) + "</p></td></tr>";
                    }
                    else if (value.tr_type == "Current Market Value") {

                        html = html + "<tr class='GrandTotalRow'><td class='GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif;text-align:left;'>" + formatDate(value.invdate) + "</p></td>" +
                            "<td class='GrandTotalRowData paraTextAlignCenter'><p>" + value.tr_type + "</p></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.invvalue) + "'>" + numberWithCommas(value.invvalue.toFixed(2)) + "</p></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.nav) + "'> " + numberWithCommas(value.nav.toFixed(2)) + "</p></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.units) + "'>" + numberWithCommas(value.units.toFixed(2)) + "</p></td>" +
                            "<td class='GrandTotalRowData paraTextAlignCenter'></td>" +
                            "<td class='GrandTotalRowData paraTextAlignCenter'></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.cmp) + "'> " + numberWithCommas(value.cmp.toFixed(2)) + "</p></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.mktvalue) + "'> " + numberWithCommas(value.mktvalue.toFixed(2)) + "</p></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.urpl) + "'> " + numberWithCommas(value.urpl.toFixed(2)) + "</p></td>" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.rpl) + "'> " + numberWithCommas(value.rpl.toFixed(2)) + "</p></td >" +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.inc_dividend) + "'> " + numberWithCommas(value.inc_dividend.toFixed(2)) + "</p></td > " +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_abs) + "'> " + numberWithCommas(value.return_abs.toFixed(2)) + "</p></td > " +
                            "<td class='GrandTotalRowData'><p class='ReportTableFont paraTextAlignRight" + isNegativeValue(value.return_xirr) + "'> " + numberWithCommas(value.return_xirr.toFixed(2)) + "</p></td></tr>";
                    }
                });
                $("#tbodyBindingMFSchemePerf").append(html);
                $("#ScriptFinYear").text($('#ddlFinyear :selected').val());
                $("#ScriptfamilyName").text($('#ddlFamilyList :selected').text());

                $("#EqDebtMFSchemeScriptName").text(data[0].scheme_name);
                $("#EqDebtMFSchemeClientName").text(data[0].client_name);

                $("#btnMFTranPDFExport").show();
                $("#btnMFTranExcelExport").show();
                $("#btnMFTranEmail").show();


                $("#div_MFSchemePerf").show();
            } else {

                $("#btnMFTranPDFExport").hide();
                $("#btnMFTranExcelExport").hide();
                $("#btnMFTranEmail").hide();

                $("#tbodyBindingMFSchemePerf").html("");
                $("#noReFoundMFSchemePerf").append("No Record Found");
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

function PerformanceholdingEquityMFDetails(client_id, sub_category, PAN, family_id) { //For both Equity and Debt MF
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

            $("#tbodyBindingrealisedPerformanceEquityMF").html("");
            $("#noReFoundrealisedPerformanceEquityMF").html("");

            if (data.length > 0) {
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    if (data[i].scrip_code != null) {
                        if (data[i].reord_order == 1) {
                            var groupName = data[i].fund_style;
                        }
                    }
                    else {
                        if (data[i].reord_order == 2) {
                            var groupName = "Grand Total";
                        }
                        else {
                            var groupName = data[i - 1].fund_style;
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
                        html = html + "<tr class = 'SubTotalRow'><td colspan=14><p style='font-size:14px;font-family:Roboto, sans-serif; word-wrap:break-word; white-space:pre; font-weight:bold'> " + index + "</td></tr>";
                    }
                    $.each(value1, function (index1, value) {
                        if (value.scrip_code != null) {
                            html = html + "<tr style='border: 1px solid #ff0000;'>" +
                                "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif; cursor: pointer; text-align: left !important; text-decoration: underline;' onclick=\"myFunctionloadPerformanceScrip_Code(\'" + value.main_client_id + "\' ,\'" + value.scrip_code + "\',\'" + value.asset_name + "\',\'" + value.account_code + "\',\'" + sub_category + "\')\"> " + value.scrip_name + "</p></td>"

                            if (value.date_of_purchase != null) {
                                if (value.date_of_purchase != 'Multiple' && value.date_of_purchase != '-') {
                                    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;'>" + value.date_of_purchase + " (" + value.no_of_days + ")" + "</p></td>"
                                }
                                else {
                                    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;'>" + value.date_of_purchase + "</p></td>"
                                }
                            }
                            html = html +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.hld_cost) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.quantity) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + numberWithCommas(value.quantity.toFixed()) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.avg_cost) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.current_nav) + "><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.current_nav == 0 ? '' : numberWithCommas(value.current_nav.toFixed(2))) + "</p></td>" +

                                "<td class = 'TableDataNumericData " + isNegativeValue(value.market_Value) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed(2))) + "</p></td> " +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.holding_per) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>"

                            html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_unreal_profit + value.long_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_unreal_profit + value.long_unreal_profit == 0 ? '' : numberWithCommas((value.srt_unreal_profit + value.long_unreal_profit).toFixed(2))) + "</p></td>"
                            html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_real_profit + value.long_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_real_profit + value.long_real_profit == 0 ? '' : numberWithCommas((value.srt_real_profit + value.long_real_profit).toFixed(2))) + "</p></td>"
                            html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_ttl_gain + value.lng_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_ttl_gain + value.lng_ttl_gain == 0 ? '' : numberWithCommas((value.srt_ttl_gain + value.lng_ttl_gain).toFixed(2))) + "</p></td>"




                            //if (value.srt_real_profit == null || value.long_real_profit == null) {
                            //    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    if (value.srt_real_profit != 0) {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //    else {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.long_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //}

                            //if (value.srt_ttl_gain == null || value.lng_ttl_gain == null) {
                            //    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    if (value.srt_ttl_gain != 0) {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed(2))) + "</p></td>"
                            //    }
                            //    else {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.lng_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed(2))) + "</p></td>"
                            //    }
                            //}

                            html = html +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.dividend_inc) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.dividend_inc == 0 ? '' : numberWithCommas(value.dividend_inc.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.return_abs) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.return_xirr) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td></tr>";
                        }
                        //Subcategory Total
                        else if (value.scrip_code == null && value.scrip_name != "Grand Total") {
                            html = html + "<tr class='SubTotalRow'>" +
                                "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;' >" + value.scrip_name + "</p></td>" +
                                "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;'></p></td>" + //Date of Purchase

                                "<td class = 'TableDataNumericData " + isNegativeValue(value.hld_cost) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.quantity) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + numberWithCommas(value.quantity.toFixed()) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.avg_cost) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.current_nav) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.current_nav == 0 ? '' : numberWithCommas(value.current_nav.toFixed(2))) + "</p></td>" +

                                "<td class = 'TableDataNumericData " + isNegativeValue(value.market_Value) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed(2))) + "</p></td> " +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.holding_per) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>"

                            html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_unreal_profit + value.long_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_unreal_profit + value.long_unreal_profit == 0 ? '' : numberWithCommas((value.srt_unreal_profit + value.long_unreal_profit).toFixed(2))) + "</p></td>"
                            html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_real_profit + value.long_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_real_profit + value.long_real_profit == 0 ? '' : numberWithCommas((value.srt_real_profit + value.long_real_profit).toFixed(2))) + "</p></td>"
                            html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_ttl_gain + value.lng_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_ttl_gain + value.lng_ttl_gain == 0 ? '' : numberWithCommas((value.srt_ttl_gain + value.lng_ttl_gain).toFixed(2))) + "</p></td>"

                            //if (value.srt_unreal_profit == null || value.long_unreal_profit == null) {
                            //    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    if (value.srt_unreal_profit != 0) {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_unreal_profit == 0 ? '' : numberWithCommas(value.srt_unreal_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //    else {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.long_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //}

                            //if (value.srt_real_profit == null || value.long_real_profit == null) {
                            //    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    if (value.srt_real_profit != 0) {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_real_profit == 0 ? '' : numberWithCommas(value.srt_real_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //    else {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.long_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.long_real_profit == 0 ? '' : numberWithCommas(value.long_real_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //}

                            //if (value.srt_ttl_gain == null || value.lng_ttl_gain == null) {
                            //    html = html + "<td class = 'TableDataNumericData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    if (value.srt_ttl_gain != 0) {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.srt_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_ttl_gain == 0 ? '' : numberWithCommas(value.srt_ttl_gain.toFixed(2))) + "</p></td>"
                            //    }
                            //    else {
                            //        html = html + "<td class = 'TableDataNumericData " + isNegativeValue(value.lng_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.lng_ttl_gain == 0 ? '' : numberWithCommas(value.lng_ttl_gain.toFixed(2))) + "</p></td>"
                            //    }
                            //}

                            html = html +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.dividend_inc) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.dividend_inc == 0 ? '' : numberWithCommas(value.dividend_inc.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.return_abs) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                                "<td class = 'TableDataNumericData " + isNegativeValue(value.return_xirr) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td></tr>"
                                //Insertion of  a Blank row to separate 2 groups
                                + "<tr><td></td></tr>";
                        }
                        //Grand Total
                        else if (value.scrip_code == null && value.scrip_name == "Grand Total") {
                            html = html + "<tr class='GrandTotalRow'>" +
                                "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;' >" + value.scrip_name + "</p></td>" +
                                "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif; text-align: left !important;'></p></td>" + //Date of Purchase
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.hld_cost) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.hld_cost == 0 ? '' : numberWithCommas(value.hld_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.quantity) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + numberWithCommas(value.quantity.toFixed()) + "</p></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.avg_cost) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.avg_cost == 0 ? '' : numberWithCommas(value.avg_cost.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.current_nav) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.current_nav == 0 ? '' : numberWithCommas(value.current_nav.toFixed(2))) + "</p></td>" +

                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.market_Value) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.market_Value == 0 ? '' : numberWithCommas(value.market_Value.toFixed(2))) + "</p></td> " +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.holding_per) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.holding_per == 0 ? '' : numberWithCommas(value.holding_per.toFixed(2))) + "</p></td>"

                            html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_unreal_profit + value.long_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_unreal_profit + value.long_unreal_profit == 0 ? '' : numberWithCommas((value.srt_unreal_profit + value.long_unreal_profit).toFixed(2))) + "</p></td>"
                            html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_real_profit + value.long_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_real_profit + value.long_real_profit == 0 ? '' : numberWithCommas((value.srt_real_profit + value.long_real_profit).toFixed(2))) + "</p></td>"
                            html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_ttl_gain + value.lng_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_ttl_gain + value.lng_ttl_gain == 0 ? '' : numberWithCommas((value.srt_ttl_gain + value.lng_ttl_gain).toFixed(2))) + "</p></td>"

                            //if (value.srt_unreal_profit == null || value.long_unreal_profit == null) {
                            //    html = html + "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    if (value.srt_unreal_profit != 0) {
                            //        html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_unreal_profit + value.long_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_unreal_profit + value.long_unreal_profit == 0 ? '' : numberWithCommas((value.srt_unreal_profit + value.long_unreal_profit).toFixed(2))) + "</p></td>"
                            //    }
                            //    else {
                            //        html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.long_unreal_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.long_unreal_profit == 0 ? '' : numberWithCommas(value.long_unreal_profit.toFixed(2))) + "</p></td>"
                            //    }
                            //}

                            //if (value.srt_real_profit == null || value.long_real_profit == null) {
                            //    html = html + "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //} else {
                            //    html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_real_profit + value.long_real_profit) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_real_profit + value.long_real_profit == 0 ? '' : numberWithCommas((value.srt_real_profit + value.long_real_profit).toFixed(2))) + "</p></td>"
                            //}

                            //if (value.srt_ttl_gain == null || value.lng_ttl_gain == null) {
                            //    html = html + "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif;'></p></td>"
                            //}
                            //else {                                
                            //    html = html + "<td class = 'GrandTotalRowData " + isNegativeValue(value.srt_ttl_gain + value.lng_ttl_gain) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.srt_ttl_gain + value.lng_ttl_gain == 0 ? '' : numberWithCommas((value.srt_ttl_gain + value.lng_ttl_gain).toFixed(2))) + "</p></td>"

                            //}

                            html = html +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.dividend_inc) + "'><p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.dividend_inc == 0 ? '' : numberWithCommas(value.dividend_inc.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.return_abs) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                                "<td class = 'GrandTotalRowData " + isNegativeValue(value.return_xirr) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif;'>" + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td></tr>";
                        }

                    });
                });

                $("#tbodyBindingrealisedPerformanceEquityMF").append(html);
                $("#bheFinYearEquityMF").text($('#ddlFinyear :selected').text());
                $("#bhefamilyNameEquityMF").text($('#ddlFamilyList :selected').text());
                $("#bheclientNameEquityMF").text(main_client_name);
                $("#bhePANEquityMF").text(PAN);
                $("#bheSubCatEquityMF").text(sub_category);

                $("#btnEMailEqMFSummary").show();
                $("#btnEqMFPDFExport").show();
                $("#btnEqMFExcelExport").show();

                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);

                //completeajaxrequest();
            } else {
                $("#tbodyBindingrealisedPerformanceEquityMF").html("");
                $("#noReFoundrealisedPerformanceEquityMF").append("No Record Found");
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

function MFAMCDetalis(client_id, sub_category, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "MainClientID": client_id,
        "Subcategory": sub_category
    }
    var posturl = "/ClientPortal/Reports/pspdspmfAMCallocationDetails";
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

            $("#tbodyBindingMFByAMC").html("");
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].amC_name != "Total") {
                        html = html + "<tr>" +
                            "<td class = 'TableDataNumericData'> <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].amC_name + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].purchase_cost) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].purchase_cost == 0 ? '' : numberWithCommas(data[i].purchase_cost.toFixed())) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].current_value) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_value == 0 ? '' : numberWithCommas(data[i].current_value)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].current_allocation_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_allocation_percent == 0 ? '' : data[i].current_allocation_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].abS_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].abS_percent == 0 ? '' : data[i].abS_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].xirR_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].xirR_percent == 0 ? '' : data[i].xirR_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_inflow) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_inflow == 0 ? '' : numberWithCommas(data[i].future_inflow)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_outflow) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_outflow == 0 ? '' : numberWithCommas(data[i].future_outflow)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_allocation_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_allocation_percent == 0 ? '' : data[i].future_allocation_percent.toFixed(2)) + "</p></td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr class = 'GrandTotalRow'>" +
                            "<td class = 'GrandTotalRowData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].amC_name + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].purchase_cost) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].purchase_cost == 0 ? '' : numberWithCommas(data[i].purchase_cost.toFixed())) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].current_value) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_value == 0 ? '' : numberWithCommas(data[i].current_value)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].current_allocation_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_allocation_percent == 0 ? '' : data[i].current_allocation_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].abS_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].abS_percent == 0 ? '' : data[i].abS_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].xirR_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].xirR_percent == 0 ? '' : data[i].xirR_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_inflow) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_inflow == 0 ? '' : numberWithCommas(data[i].future_inflow)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_outflow) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_outflow == 0 ? '' : numberWithCommas(data[i].future_outflow)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_allocation_percent) + "'> <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_allocation_percent == 0 ? '' : data[i].future_allocation_percent.toFixed(2)) + "</p></td>" +
                            "</tr>"
                    }
                }

                $("#Nav_Pill_AMC").css("display", "block");
                $("#tbodyBindingMFByAMC").append(html);

            } else {
                $("#tbodyBindingMFByAMC").html("");
                //$("#noReFoundrealisedPerformanceEquityMF").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        //complete: function (xhr) {
        //    completeajaxrequest();
        //}
    })
}

function MFCategoryDetails(client_id, sub_category, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "MainClientID": client_id,
        "Subcategory": sub_category
    }
    var posturl = "/ClientPortal/Reports/pspdspmfcategoryallocationDetails";
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

            $("#tbodyBindingMFByCat").html("");
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].category_Name != "Total") {
                        html = html + "<tr>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].category_Name + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].purchase_cost) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].purchase_cost == 0 ? '' : numberWithCommas(data[i].purchase_cost.toFixed())) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].current_value) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_value == 0 ? '' : numberWithCommas(data[i].current_value)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].current_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_allocation_percent == 0 ? '' : data[i].current_allocation_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].abS_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].abS_percent == 0 ? '' : data[i].abS_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].xirR_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].xirR_percent == 0 ? '' : data[i].xirR_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_inflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_inflow == 0 ? '' : numberWithCommas(data[i].future_inflow)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_outflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_outflow == 0 ? '' : numberWithCommas(data[i].future_outflow)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_allocation_percent == 0 ? '' : data[i].future_allocation_percent.toFixed(2)) + "</p></td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr class = 'GrandTotalRow'>" +
                            "<td class = 'GrandTotalRowData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].category_Name + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].purchase_cost) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].purchase_cost == 0 ? '' : numberWithCommas(data[i].purchase_cost.toFixed())) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].current_value) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_value == 0 ? '' : numberWithCommas(data[i].current_value)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].current_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_allocation_percent == 0 ? '' : data[i].current_allocation_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].abS_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].abS_percent == 0 ? '' : data[i].abS_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].xirR_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].xirR_percent == 0 ? '' : data[i].xirR_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_inflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_inflow == 0 ? '' : numberWithCommas(data[i].future_inflow)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_outflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_outflow == 0 ? '' : numberWithCommas(data[i].future_outflow)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_allocation_percent == 0 ? '' : data[i].future_allocation_percent.toFixed(2)) + "</p></td>" +
                            "</tr>"
                    }
                }

                $("#Nav_Pill_Cat").css("display", "block");
                $("#tbodyBindingMFByCat").append(html);

            } else {
                $("#tbodyBindingMFByCat").html("");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        //complete: function (xhr) {
        //    completeajaxrequest();
        //}
    })
}

function MFSchemeDetalis(client_id, sub_category, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "MainClientID": client_id,
        "Subcategory": sub_category
    }
    var posturl = "/ClientPortal/Reports/pspdspmfschemeallocationDetails";
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
            $("#tbodyBindingMFByScheme").html("");
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].scheme_Name != "Total") {
                        html = html + "<tr>" +
                            "<td class = 'TableDataNumericData ' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].scheme_Name + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].purchase_cost) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].purchase_cost == 0 ? '' : numberWithCommas(data[i].purchase_cost)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].current_value) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_value == 0 ? '' : numberWithCommas(data[i].current_value)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].current_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_allocation_percent == 0 ? '' : data[i].current_allocation_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].abS_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].abS_percent == 0 ? '' : data[i].abS_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].xirR_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].xirR_percent == 0 ? '' : data[i].xirR_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_inflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_inflow == 0 ? '' : numberWithCommas(data[i].future_inflow)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_outflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_outflow == 0 ? '' : numberWithCommas(data[i].future_outflow)) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].future_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_allocation_percent == 0 ? '' : data[i].future_allocation_percent.toFixed(2)) + "</p></td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr class = 'GrandTotalRow'>" +
                            "<td class = 'GrandTotalRowData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].scheme_Name + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].purchase_cost) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].purchase_cost == 0 ? '' : numberWithCommas(data[i].purchase_cost)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].current_value) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_value == 0 ? '' : numberWithCommas(data[i].current_value)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].current_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].current_allocation_percent == 0 ? '' : data[i].current_allocation_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].abS_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].abS_percent == 0 ? '' : data[i].abS_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].xirR_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].xirR_percent == 0 ? '' : data[i].xirR_percent.toFixed(2)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_inflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_inflow == 0 ? '' : numberWithCommas(data[i].future_inflow)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_outflow) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_outflow == 0 ? '' : numberWithCommas(data[i].future_outflow)) + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].future_allocation_percent) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif; '> " + (data[i].future_allocation_percent == 0 ? '' : data[i].future_allocation_percent.toFixed(2)) + "</p></td>" +
                            "</tr>"
                    }
                }

                $("#Nav_Pill_Scheme").css("display", "block");
                $("#tbodyBindingMFByScheme").append(html);

            } else {
                $("#tbodyBindingMFByScheme").html("");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        //complete: function (xhr) {
        //    completeajaxrequest();
        //}
    })
}

function MFSIPDetalis(client_id, sub_category, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "MainClientID": client_id,
        "Subcategory": sub_category,
        "tran_type": "SIP"
    }
    var posturl = "/ClientPortal/Reports/pspdspmfsipstpdetailDetails";
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

            $("#tbodyBindingMFSIP").html("");
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].display_order == "1") {
                        html = html + "<tr>" +

                            "<td class = 'TableDataNumericData ' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].source_Scheme_Name + "</p></td>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].folio_no + "</p></td>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + formatDate(data[i].start_Date) + "</p></td>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + formatDate(data[i].end_Date) + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].amount) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + numberWithCommas(data[i].amount) + "</p></td>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].frequency + "</p></td>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].total_Installments + "</p></td>" +
                            "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].pending_Installments + "</p></td>" +
                            "<td class = 'TableDataNumericData " + isNegativeValue(data[i].pending_Amount) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + numberWithCommas(data[i].pending_Amount) + "</p></td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr class = 'GrandTotalRow'>" +
                            "<td class = 'GrandTotalRowData' colspan='4'> <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].source_Scheme_Name + "</p></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].amount) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + numberWithCommas(data[i].amount) + "</p></td>" +
                            "<td class = 'GrandTotalRowData' colspan='3'></td>" +
                            "<td class = 'GrandTotalRowData " + isNegativeValue(data[i].pending_Amount) + "' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + numberWithCommas(data[i].pending_Amount) + "</p></td>" +
                            "</tr>"
                    }
                }

                $("#Nav_Pill_SIP").css("display", "block");
                //$("#Nav_Pill_SIP").show;
                //$("#Nav_Pill_SIP").text("SIP");
                //$("#Nav_Pill_SIP").removeClass("hidden");
                $("#tbodyBindingMFSIP").append(html);

            } else {
                $("#tbodyBindingMFSIP").html("");
                //$("#noReFoundrealisedPerformanceEquityMF").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: completeajaxrequest
    })
}

function MFSTPDetalis(client_id, sub_category, family_id) {
    var formdata = {
        "FINYRData": $('#ddlFinyear :selected').val(),
        "FamilyID": family_id,
        "MainClientID": client_id,
        "Subcategory": sub_category,
        "tran_type": "STP"
    }
    var posturl = "/ClientPortal/Reports/pspdspmfsipstpdetailDetails";
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
            $("#tbodyBindingMFSTP").html("");
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    html = html + "<tr>" +

                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].source_Scheme_Name + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + data[i].folio_no + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + formatDate(data[i].start_Date) + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif; text-align:left !important;'> " + formatDate(data[i].end_Date) + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + numberWithCommas(data[i].amount) + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].frequency + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].target_Scheme_Name + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].total_Installments + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + data[i].pending_Installments + "</p></td>" +
                        "<td class = 'TableDataNumericData' > <p style='font-size:14px;font-family:Roboto, sans-serif;'> " + numberWithCommas(data[i].pending_Amount) + "</p></td>" +

                        "</tr>"
                }

                $("#Nav_Pill_STP").css("display", "block")
                //$("#Nav_Pill_SIP").show;
                //$("#Nav_Pill_SIP").text("SIP");
                //$("#Nav_Pill_SIP").removeClass("hidden");
                $("#tbodyBindingMFSTP").append(html);

            } else {
                $("#tbodyBindingMFSTP").html("");
                //$("#noReFoundrealisedPerformanceEquityMF").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: completeajaxrequest
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

function performanceholdingOtherPMS(client_id, sub_category, family_id) {
    var fyYear = $('#ddlFinyear :selected').val()
    var formdata = {
        "FINYRData": fyYear,
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
                $("#bhefamilyNameEquityPMS").text($('#ddlFamilyList :selected').text());
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
                alert("No data Found")
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

                //html = html + "<tr><td colspan = '2' class = 'SubTotalRow'> Client : " + data[0].client_name + "</td>" +
                //    "<td class = 'SubTotalRow'> Pan : " + data[0].pan_no + "</td></tr>"
                console.log(groups)
                $.each(groups, function (index, value1) {
                    html = html +
                        //"<tr><td class='TableDataNumericData' colspan='3'><p class='paraTextAlignLeft' style='font-size:14px;font-family:Roboto, sans-serif;'> <b> Issuer : </b>" + value1[0].issuerName +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b> Scheme : </b>" + value1[0].schemeName + "&nbsp;&nbsp;&nbsp;&nbsp<b>Amount : </b>" + numberWithCommas(value1[0].amount) +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b>  Interest Rate : </b>" + value1[0].fdInterestRate_perc + "% </br><b>Coupon Type : </b>" + value1[0].fdCouponType +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b> Interest Frequency : </b>" + value1[0].fdInterestFrequency +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b>  Application Date : </b>" + formatDate(value1[0].fdApplicationDate) +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b> Maturity Date : </b>" + formatDate(value1[0].maturity_Date) + "</p><td>" +
                        //"</tr>" +
                        //"<tr><th class='TableHeaderRow'><p><b>Cash Flow Date</b></p></th ><th class='TableHeaderRow'><p><b>Cash Flow Amount</b></p></th><th class='TableHeaderRow'><p><b>Cash Flow Type </b></p></th></tr>" 

                        "<tr class='SubTotalRow'><td>" +
                        "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#FD_acc_" + value1[0].rec_id + "'" +
                        "aria-expanded='false' aria-controls='divi_acc_" + value1[0].rec_id + "'>" +
                        "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                        "</button>&nbsp;<b> Issuer : </b>" + value1[0].issuerName + "&nbsp;&nbsp;&nbsp;&nbsp<b> Scheme : </b>" + value1[0].schemeName + "&nbsp;&nbsp;&nbsp;&nbsp<b>Amount : </b>" + numberWithCommas(value1[0].amount) +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b>  Interest Rate : </b>" + value1[0].fdInterestRate_perc + "% </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Coupon Type : </b>" + value1[0].fdCouponType +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b> Interest Frequency : </b>" + value1[0].fdInterestFrequency +
                        //"&nbsp;&nbsp;&nbsp;&nbsp<b>  Application Date : </b>" + formatDate(value1[0].fdApplicationDate) +
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

                            "<td class='TableDataNumericData " + isNegativeValue(value.return_abs) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.return_abs == 0 ? '' : numberWithCommas(value.return_abs.toFixed(2))) + "</p></td>" +
                            "<td class='TableDataNumericData " + isNegativeValue(value.return_xirr) + "'> <p class='ReportTableFont  paraTextAlignRight'> " + (value.return_xirr == 0 ? '' : numberWithCommas(value.return_xirr.toFixed(2))) + "</p></td></tr>";
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

function PerformanceholdingLiquiLoan(account_code) {

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
            var html = "";


            if (data != null) {
                $("#Liqui_FinYear").text($('#ddlFinyear :selected').val());
                $("#Liqui_Family_Name").text(data.family_name);
                $("#Liqui_Client_Name").text(data.main_client_name + "(" + data.pan_number + ")");
                $("#Net_prin_Investment").text(numberWithCommas(data.net_Principal_Investment.toFixed(2)));
                $("#Port_Val").text(numberWithCommas(data.portfolio_Value.toFixed(2)));

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






$("#modalfinish").click(function () {
    $("#tbodyBindingrealisedPerformance").html("");
    $("#noReFoundrealisedPerformance").html("");
})
function loadDividentXIRRDetails(client_id, fy) {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlFamilyList :selected').val(),
        "main_client_id": client_id
    }
    var posturl = "/ClientPortal/Reports/RealisedDividendDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {

            $("#tbodyBindingrealisedDividend").html("");
            $("#noReFoundrealiseddivindend").html("");
            if (data.length > 0) {
                // startajaxrequest();

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;' id=" + value.family_id + ">" + value.client_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;' id=" + value.family_id + ">" + formatDate(value.dividend_date) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;' id=" + value.family_id + ">" + value.isin + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;' id=" + value.family_id + ">" + value.scrip_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;float:right;' id=" + value.family_id + ">" + numberWithCommas(value.value.toFixed(2)) + "</p></td></tr>";
                });
                $("#tbodyBindingrealisedDividend").append(html);
                $("#divFinYear").text($('#ddlFinyear :selected').val());
                $("#divFamily").text($('#ddlFamilyList :selected').text());
                // completeajaxrequest();
            } else {
                $("#tbodyBindingrealisedDividend").html("");
                $("#noReFoundrealiseddivindend").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function () {

        }
    })
}



$("#btnperformanceExportPopup").click(function () {
    window.location = '/ClientPortal/Excel/DownloadPerformancePopupExcel?client_id=' + Encrypt($('#popclient_id').val()) + '&sub_category=' + Encrypt($('#popsub_category').val()) + '&familyid=' + Encrypt($('#ddlFamilyList :selected').val()) + '&Finy=' + Encrypt($('#ddlFinyear :selected').val());
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
    //$("#btnExport").click(function () {

    //    //window.location = '/ClientPortal/Excel/DownloadPerformanceExcel?GridHtml=' + $('#htmltopdf').html() + ' &Family=' + $('#ddlFamilyList :selected').val()
    //    window.location = '/ClientPortal/Excel/DownloadPerformanceExcel?FINYR=' + Encrypt($('#ddlFinyear :selected').val()) + ' &Family=' + Encrypt($('#ddlFamilyList :selected').val());
    //    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    //    return false;
    //})

    //$("#btnExportPdf").click(function () {

    //    //window.location = '/ClientPortal/Excel/DownloadPerformanceExcel?GridHtml=' + $('#htmltopdf').html() + ' &Family=' + $('#ddlFamilyList :selected').val()
    //    window.location = '/ClientPortal/Excel/DownloadPerformancePdf?FINYR=' + Encrypt($('#ddlFinyear :selected').val()) + '&Family=' + Encrypt($('#ddlFamilyList :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    //    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    //    return false;
    //})
})
$("#btnperformanceExportPdfPopup").click(function () {
    window.location = '/ClientPortal/Excel/DownloadPerformancePopupPdf?client_id=' + Encrypt($('#popclient_id').val()) + '&sub_category=' + Encrypt($('#popsub_category').val()) + '&familyid=' + Encrypt($('#ddlFamilyList :selected').val()) + '&Finy=' + Encrypt($('#ddlFinyear :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})



$("#btnperformanceExportPopupEquityMF").click(function () {
    window.location = '/ClientPortal/Excel/DownloadPerformancePopupExcel?client_id=' + Encrypt($('#popclient_idEquityMF').val()) + '&sub_category=' + Encrypt($('#popsub_categoryEquityMF').val()) + '&familyid=' + Encrypt($('#ddlFamilyList :selected').val()) + '&Finy=' + Encrypt($('#ddlFinyear :selected').val());
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})
$("#btnperformanceExportPdfPopupEquityMF").click(function () {
    window.location = '/ClientPortal/Excel/DownloadPerformancePopupPdf?client_id=' + Encrypt($('#popclient_idEquityMF').val()) + '&sub_category=' + Encrypt($('#popsub_categoryEquityMF').val()) + '&familyid=' + Encrypt($('#ddlFamilyList :selected').val()) + '&Finy=' + Encrypt($('#ddlFinyear :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})


function PerformanceUpdateFev() {
    var formdata = {
        "module_id": $("p[name='Performance Report']").attr("id")
    }
    var posturl = "/ClientPortal/Pie/UpdateFev";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            //  alert(data)
            if (data.sql_status == "Fail") {
                alert(data.sql_message);
            } else {
                FetchFev();
                setTimeout(function () {
                    $(".fetchId").each(function () {
                        if ($(this).attr('id') == $("p[name='Performance Report']").attr("id")) {
                            $("#PerformanceAddtoFavourite").hide();
                            $("#PerformanceRemoveFavourite").show();
                            return false;
                        } else {
                            $("#PerformanceAddtoFavourite").show();
                            $("#PerformanceRemoveFavourite").hide();
                        }
                    })
                }, 500);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function PerformanceDeleteFev() {
    var formdata = {
        "module_id": $("p[name='Performance Report']").attr("id")
    }
    var posturl = "/ClientPortal/Pie/UpdateFevRemove";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            //  alert(data)
            if (data.sql_status == "Fail") {
                alert(data.sql_message);
            } else {
                FetchFev();
                setTimeout(function () {
                    $(".fetchId").each(function () {
                        if ($(this).attr('id') == $("p[name='Performance Report']").attr("id")) {
                            $("#PerformanceAddtoFavourite").hide();
                            $("#PerformanceRemoveFavourite").show();
                            return false;
                        } else {
                            $("#PerformanceAddtoFavourite").show();
                            $("#PerformanceRemoveFavourite").hide();
                        }
                    })
                }, 500);
            }
            $("#PerformanceAddtoFavourite").show();
            $("#PerformanceRemoveFavourite").hide();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function myFunctionRemoveFav(id) {
    var formdata = {
        "module_id": id
    }
    var posturl = "/ClientPortal/Pie/UpdateFevRemove";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            //  alert(data)
            if (data.sql_status == "Fail") {
                alert(data.sql_message);
            } else {
                FetchFev();
                setTimeout(function () {
                    $(".fetchId").each(function () {
                        if ($(this).attr('id') == $("p[name='Performance Report']").attr("id")) {
                            $("#PerformanceAddtoFavourite").hide();
                            $("#PerformanceRemoveFavourite").show();
                            return false;
                        } else {
                            $("#PerformanceAddtoFavourite").show();
                            $("#PerformanceRemoveFavourite").hide();
                        }
                    })
                }, 500);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

setTimeout(function () {
    $(".fetchId").each(function () {
        if ($(this).attr('id') == $("p[name='Performance Report']").attr("id")) {
            $("#PerformanceAddtoFavourite").hide();
            $("#PerformanceRemoveFavourite").show();
            return false;
        } else {
            $("#PerformanceAddtoFavourite").show();
            $("#PerformanceRemoveFavourite").hide();
        }
    })
}, 500);

$("#btnExporttbodyBindingrealisedPerformanceScript").click(function () {
    window.location = '/ClientPortal/Excel/psprptclientperformancecheckDetailsExcel?FINYR=' + Encrypt($('#ddlFinyear :selected').val()) + '&Client=' + Encrypt(main_client_id_Popup) + '&Scrip_Code=' + Encrypt(scrip_code_Popup) + '&subcategory=' + Encrypt(sub_category_Popup) + '&account_code=' + Encrypt(account_code_Popup) + '&asset_code=' + Encrypt(sub_category_Popup);
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnExportPdftbodyBindingrealisedPerformanceScript").click(function () {
    window.location = '/ClientPortal/Excel/psprptclientperformancecheckDetailsPdf?FINYR=' + Encrypt($('#ddlFinyear :selected').val()) + '&Client=' + Encrypt(main_client_id_Popup) + '&Scrip_Code=' + Encrypt(scrip_code_Popup) + '&subcategory=' + Encrypt(sub_category_Popup) + '&account_code=' + Encrypt(account_code_Popup) + '&asset_code=' + Encrypt(sub_category_Popup) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnPerformanceDividendExportXxl").click(function () {
    window.location = '/ClientPortal/Excel/PerformanceDividendpopupExcel?client_id=' + Encrypt(client_id1) + '&finyr=' + Encrypt(fin_year1) + '&family_id=' + Encrypt(family_id1);
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnPerformanceDividendExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/PerformanceDividendpopupPdf?client_id=' + Encrypt(client_id1) + '&finyr=' + Encrypt(fin_year1) + '&family_id=' + Encrypt(family_id1) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();;
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})



//**** Button Clicks ****
//Main Report
$("#btnEMailMainPerfSummary").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()

    var strPara = LoginId + "~" + famId + "~~" + Fin_year;

    //Method used is in Dashboard.js
    PDFandExcelExport("#btnEMailMainPerfSummary", "6", LoginId, famId, "E", "PDF", strPara);

})

$("#btnExportPdf").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()

    var strPara = LoginId + "~" + famId + "~~" + Fin_year;

    var strDescrip = "Performance Holding Report for " + $('#ddlFamilyList :selected').text() + " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnExportPdf", "6", LoginId, famId, "X", "PDF", strPara, strDescrip)
})

$("#btnExportExcel").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()

    var strPara = LoginId + "~" + famId + "~~" + Fin_year;

    var strDescrip = "Performance Holding Report for " + $('#ddlFamilyList :selected').text() + " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport("#btnExportExcel", "6", LoginId, famId, "X", "EXCEL", strPara, strDescrip)
})
//Main Report

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

//ReITs and InvITs Script List
$("#btnEMailRnI").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#DEclient_id").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

    PDFandExcelExport("#btnEMailRnI", "13", LoginId, famId, "E", "PDF", strPara);

})

$("#btnExportRnIPDF").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#DEclient_id").val();


    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

    if (client_id != "0") {
        var strDescrip = "ReITs and InvITs Performance Summary of " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }
    else { //For All clients
        var strDescrip = "ReITs and InvITs Performance Summary of " + $('#ddlFamilyList :selected').text() + " " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }

    PDFandExcelExport("#btnExportRnIPDF", "13", LoginId, famId, "X", "PDF", strPara, strDescrip);

})

$("#btnExportRnIExcel").click(function () {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#DEclient_id").val();

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + "true";

    if (client_id != "0") {
        var strDescrip = "ReITs and InvITs Performance Summary of " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }
    else { //For All clients
        var strDescrip = "ReITs and InvITs Performance Summary of " + $('#ddlFamilyList :selected').text() + " " + $("#bheDEclientName").text() + " for year " + $('#ddlFinyear :selected').text()
    }

    PDFandExcelExport("#btnExportRnIExcel", "13", LoginId, famId, "X", "EXCEL", strPara, strDescrip);
})
//ReITs and InvITs Script List


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

//MF Tran
$("#btnMFTranEmail").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtEquiDebtMFMainClient").val();
    var scrip_code = $("#txtEquiDebtMFScrip").val();
    var subcat = $("#txtEquiDebtMFSubCat").val();

    var strPara = LoginId + "~" + subcat + "~" + client_id + "~~" + scrip_code;

    var strDescrip = subcat + " Transaction Details for " + $("#EqDebtMFSchemeClientName").text() + " of " + $("#EqDebtMFSchemeScriptName").text();

    PDFandExcelExport("#btnMFTranEmail", "35", LoginId, "0", "E", "PDF", strPara, strDescrip);

})

$("#btnMFTranPDFExport").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtEquiDebtMFMainClient").val();
    var scrip_code = $("#txtEquiDebtMFScrip").val();
    var subcat = $("#txtEquiDebtMFSubCat").val();

    var strPara = LoginId + "~" + subcat + "~" + client_id + "~~" + scrip_code;

    var strDescrip = subcat + " Transaction Details for " + $("#EqDebtMFSchemeClientName").text() + " of " + $("#EqDebtMFSchemeScriptName").text();

    PDFandExcelExport("#btnMFTranPDFExport", "35", LoginId, "0", "X", "PDF", strPara, strDescrip);

})

$("#btnMFTranExcelExport").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#txtEquiDebtMFMainClient").val();
    var scrip_code = $("#txtEquiDebtMFScrip").val();
    var subcat = $("#txtEquiDebtMFSubCat").val();

    var strPara = LoginId + "~" + subcat + "~" + client_id + "~~" + scrip_code;

    var strDescrip = subcat + " Transaction Details for " + $("#EqDebtMFSchemeClientName").text() + " of " + $("#EqDebtMFSchemeScriptName").text();

    PDFandExcelExport("#btnMFTranExcelExport", "35", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})
//MF Tran



//Equity & Debt MF & Bonds Summary Report

function EqDebtMFBondsMail(btn_id, Report_Type, Report_format) {

    var famId = $("#Curr_Fam_ID").val()
    var Fin_year = $('#ddlFinyear :selected').val()
    var LoginId = $("#LoggedInUID").val()
    var client_id = $("#common_client_id_holder").val();
    var sub_cat = $("#common_cat_holder").val();
    var client_name

    var report_id = null;
    if (sub_cat == "Equity MF") {
        report_id = "15";
        client_name = $("#bheclientNameEquityMF").text();
    }
    else if (sub_cat == "Debt MF") {
        report_id = "16";
        client_name = $("#bheclientNameEquityMF").text();
    }
    else if (sub_cat == "Bonds") {
        report_id = "17";
        client_name = $("#bheclientNameBonds").text()
    }

    var strPara = LoginId + "~" + famId + "~" + client_id + "~" + Fin_year + "~" + sub_cat;

    var strDescrip = sub_cat + " Performance Summary for " + client_name +
        " for year " + $('#ddlFinyear :selected').text()

    PDFandExcelExport(btn_id, report_id, LoginId, famId, Report_Type, Report_format, strPara, strDescrip);

}

$("#btnEMailEqMFSummary").click(function () {

    EqDebtMFBondsMail("#btnEMailEqMFSummary", "E", "PDF");

})

$("#btnEMailBonds").click(function () {

    EqDebtMFBondsMail("#btnEMailBonds", "E", "PDF");
})

$("#btnEqMFPDFExport").click(function () {

    EqDebtMFBondsMail("#btnEqMFPDFExport", "X", "PDF");
})

$("#btnBondsPDFExport").click(function () {

    EqDebtMFBondsMail("#btnBondsPDFExport", "X", "PDF");
})

$("#btnEqMFExcelExport").click(function () {

    EqDebtMFBondsMail("#btnEqMFExcelExport", "X", "EXCEL");

})

$("#btnBondsExcelExport").click(function () {

    EqDebtMFBondsMail("#btnBondsExcelExport", "X", "EXCEL");
})
//Equity & Debt MF & Bonds Summary Report


//Back Buttons
$("#btnBackEquityMF").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_PerformanceEquity_MF").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})

$("#btnBackDirectEquity").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_Performance").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})

$("#btnBackRnI").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#ReitsInvits_Performance").hide();
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

$("#btnBackMFSchemePerf").click(function () {
    $("#myModalst_PerformanceEquity_MF").show();
    $("#div_MFSchemePerf").hide();
})

$("#btnBackBonds").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_PerformanceBonds").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})

$("#btnBackEqPMS").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_PerformanceEquityPMS").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
$("#btnBackOtherPMS").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_PerformanceOtherPMS").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})

$("#btnBackLiquiLoan").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#LiquLoan_div").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})

$("#btnBackFDDetails").click(function () {
    $("#myModalst_PerformanceLandingPage").show();
    $("#myModalst_PerformanceFDDetails").hide();
    $("#ddlFinyear").attr("disabled", false);
    $("#ddlFamilyList").attr("disabled", false);
})
//Back Buttons

//**** Button Clicks ****
