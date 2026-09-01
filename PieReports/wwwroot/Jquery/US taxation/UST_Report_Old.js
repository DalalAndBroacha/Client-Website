$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    FetchClientList();
    FetchCalendarYearList();
    
    $("#div_summary_report").hide();

    $("#btnUSTExportPdf").hide();
    $("#btnUSTExportExcel").hide();
    $("#btnEMailMainUST").hide();

});
$("#ddlCategoryList").change(function () {
    $("#div_data").hide();
    $("#div_data_usd").hide();
});
$("#ddlCalYearList").change(function () {
    $("#div_data").hide();
    $("#div_data_usd").hide();
});

$("#ddlUSClientList").change(function () {
    FetchCategoryList();
    $("#div_data").hide();
    $("#div_data_usd").hide();
});

function FetchCalendarYearList() {

    var posturl = "/ClientPortal/Pie/psp_dsp_calendar_year";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlCalYearList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.calendar_value + ">" + value.calendar_dsp + "</option>";
                });

                $("#ddlCalYearList").append(html);
                $("#ddlCalYearList").attr("disabled", false);

                $('#inp_date').attr("min", data[0].min_cal_date);
                $('#inp_date').attr("max", data[0].max_cal_date);
            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function FetchClientList() {

    var formdata = {
        "country": "US"
    }

    var posturl = "/ClientPortal/Pie/GetNRIClientList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlUSClientList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.client_id + ">" + value.name + "</option>";
                });

                $("#ddlUSClientList").append(html);
                $("#ddlUSClientList").attr("disabled", false);


                FetchCategoryList();
            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function FetchCategoryList() {
    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val()
    }
    var posturl = "/ClientPortal/Pie/psp_dsp_nri_category";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlCategoryList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.category_id + ">" + value.category_name + "</option>";
                });

                $("#ddlCategoryList").append(html);
                $("#ddlCategoryList").attr("disabled", false);

            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetSummaryData() {
    let main_client_id = $('#ddlUSClientList :selected').val()
    let client_category = $('#ddlCategoryList :selected').val() 
    let cal_year = $('#ddlCalYearList :selected').val()
    let txt_main_client_id = $('#ddlUSClientList :selected').text();
    let txt_category = $('#ddlCategoryList :selected').text();

    var formdata = {
        "main_client_id": main_client_id,
        "client_category": client_category,
        "cal_year": cal_year,
        "flag": "R"
    }
    var posturl = "/ClientPortal/Reports/psp_rpt_nri_client_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            html = "";
            htmlusd = "";
            htmlcurrency = "";

            $("#display_table").html("");
            $("#display_tableusd").html("");
            $("#currency_rate").html("");

            if (data.length > 1) {
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].category_order;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);

                }

                console.log(groups)

                html = html + "<tr><th colspan = 5 class='TableHeaderRow' style='font-weight:bold'>Summary Report (INR)</th></tr> <tr><th class='TableHeaderRow'></th><th class='TableHeaderRow'></th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'> Gross Income(INR)</th><th class='TableHeaderRow' style='font-weight:bold'>Tax Withheld (INR)</th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'>Net Income (INR)</th></tr > "

                htmlusd = htmlusd + "<tr><th colspan = 5 class='TableHeaderRow' style='font-weight:bold'>Summary Report (USD)</th></tr> <tr><th class='TableHeaderRow'></th><th class='TableHeaderRow'></th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'> Gross Income(USD)</th><th class='TableHeaderRow' style='font-weight:bold'>Tax Withheld (USD)</th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'>Net Income (USD)</th></tr > "

                htmlcurrency = htmlcurrency + "<tr><td style = 'border: 1px solid red;'><b>Note:</b> For conversion of INR to USD, we have used conversion rate of <b>" + Number(data[1].currency).toFixed(2) + "</b> as given on the website of IRS as the average for the year " + cal_year + ".</td></tr>"
                $.each(groups, function (index, value1) {
                    $.each(value1, function (index1, value) {
                        if (index1 == 0 && value.category_order != "5") {
                            html = html + "<tr><td rowspan = " + value1.length + " style='font-weight:bold;border-bottom: 1px solid red;' class='TableHeaderRow'>" + value1[0].category + "</td>" +
                                "<td style = 'border-right: 1px solid red;'>" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";

                            htmlusd = htmlusd + "<tr><td rowspan = " + value1.length + " style='font-weight:bold;border-bottom: 1px solid red;' class='TableHeaderRow'>" + value1[0].category + "</td>" +
                                "<td style = 'border-right: 1px solid red;'>" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";

                        }
                        else if (value.category_order == "5") {
                            html = html + "<tr><td  style='font-weight:bold;border:1px solid red;' class='TableHeaderRow'>" + value1[0].category + "</td>" +
                                "<td style = 'border:1px solid red;font-weight:bold'>" + value.subcategory + "</td><td style = 'border:1px solid red;text-align:center;font-weight:bold'>" + numberWithCommas(value.amount.toFixed(2)) +
                                "</td><td style = 'border:1px solid red;text-align:center;font-weight:bold;text-decoration:underline;cursor:pointer' onclick = \"TaxWithHeld(\'" + main_client_id + "\',\'" + cal_year + "\',\'" + client_category + "\',\'" + txt_main_client_id + "\',\'" + txt_category + "\' )\" > " +
                                /*"</td><td style = 'border:1px solid red;text-align:center;font-weight:bold;text-decoration:underline;cursor:pointer' onclick = \"TaxWithHeld(\'\', \'X\', \'PDF\',\'" + value.category + "\', \'Dividend\')\" > " +*/
                                numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style = 'border:1px solid red;text-align:center;font-weight:bold'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";

                            htmlusd = htmlusd + "<tr><td style='font-weight:bold;border:1px solid red;' class='TableHeaderRow'>" + value1[0].category + "</td>" +
                                "<td style = 'border:1px solid red;font-weight:bold'>" + value.subcategory + "</td><td style = 'border:1px solid red;text-align:center;font-weight:bold'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border:1px solid red;text-align:center;font-weight:bold'> " +
                                numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style = 'border:1px solid red;text-align:center;font-weight:bold'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";
                        }
                        else if (value.subcategory == "Total") {
                                if (value.category == "Bank Account") {
                                    html = html + "<tr><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;'>" + value.subcategory + "</td>" +
                                        "<td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;text-align:center;text-decoration: underline;cursor:pointer' onclick = \"BankInterest(\'" + main_client_id + "\',\'" + cal_year + "\',\'" + client_category + "\',\'" + txt_main_client_id + "\',\'" + txt_category + "\' )\" >" +
                                        numberWithCommas(value.amount.toFixed(2)) + "</td><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style='font-weight:bold;" +
                                        "border-bottom: 1px solid red; border-right: 1px solid red; text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";
                                }
                                else {
                                    html = html + "<tr><td style='font-weight:bold;border-bottom: 1p solid red;border-right: 1px solid red;'>" + value.subcategory + "</td><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;text-align:center;' >" +
                                        numberWithCommas(value.amount.toFixed(2)) + "</td><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style='font-weight:bold;" +
                                        "border-bottom: 1px solid red; border-right: 1px solid red; text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";
                                }
                            htmlusd = htmlusd + "<tr><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;'>" + value.subcategory + "</td><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;text-align:center'>" +
                                numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style='font-weight:bold;border-bottom: 1px solid red;border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style='font-weight:bold;" +
                                "border-bottom: 1px solid red; border-right: 1px solid red; text-alixgn:center'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";
                        }
                        else
                        {
                            if (value.subcategory == "Dividend Received") {
                                html = html + "<tr><td style = 'cursor:pointer; border-right: 1px solid red;text-decoration: underline;' onclick =\"Dividend(\'" + main_client_id + "\',\'" + cal_year + "\',\'" + client_category + "\',\'" + txt_main_client_id + "\',\'" + txt_category + "\',\'" + value.category + "\')\" >" + (value.category == "Equity" ? value.subcategory : "Interest Received") + "</td>" +
                                    "<td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td>" +
                                    "<td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.tax_Amount.toFixed(2)) + "</td>" +
                                    "<td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";


                                htmlusd = htmlusd + "<tr><td style = 'border-right: 1px solid red;'>" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                    numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";

                            }
                            else {

                                html = html + "<tr><td style = 'border-right: 1px solid red;'>" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                    numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";

                                htmlusd = htmlusd + "<tr><td style = 'border-right: 1px solid red;'>" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                    numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";

                            }
                            
                                //if (value.subcategory == "Dividend Received" && value.category == "Equity") {
                                //    html = html + "<tr><td style = 'cursor:pointer; border-right: 1px solid red;text-decoration: underline;' onclick =\"Dividend(\'" + main_client_id + "\',\'" + cal_year + "\',\'" + client_category + "\',\'" + txt_main_client_id + "\',\'" + txt_category + "\',\'" + value.category + "\')\" >" + value.subcategory + "</td>" +
                                //        "<td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td>" +
                                //        "<td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.tax_Amount.toFixed(2)) + "</td>" +
                                //        "<td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";
                                //    //html = html + "<tr><td style = 'cursor:pointer; border-right: 1px solid red;text-decoration: underline;' onclick =\"ExportDividend(\'\', \'X\', \'PDF\',\'" + value.category + "\', \'Dividend\')\" >" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                //    //    numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";

                                //    htmlusd = htmlusd + "<tr><td style = 'border-right: 1px solid red;'>" + value.subcategory + "</td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                //        numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";
                                //}
                                //else if (value.subcategory == "Dividend Received" && value.category == "Bonds") {
                                //    html = html + "<tr><td style = 'cursor:pointer; border-right: 1px solid red;text-decoration: underline;' onclick =\"ExportDividend(\'\', \'X\', \'PDF\',\'" + value.category + "\', \'Bonds Interest\')\" > Interest Received </td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                //        numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";
                                //    //html = html + "<tr><td style = 'cursor:pointer; border-right: 1px solid red;text-decoration: underline;' onclick =\"ExportDividend(\'\', \'X\', \'PDF\',\'" + value.category + "\', \'Bonds Interest\')\" > Interest Received </td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                //    //    numberWithCommas(value.tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_IRS.toFixed(2)) + "</td></tr>";

                                //    htmlusd = htmlusd + "<tr><td style = 'border-right: 1px solid red;'> Interest Received </td><td style = 'border-right: 1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " +
                                //        numberWithCommas(value.usD_Tax_Amount.toFixed(2)) + "</td><td style = 'border-right: 1px solid red;text-align:center'> " + numberWithCommas(value.net_Income_USD.toFixed(2)) + "</td></tr>";
                                //}
                                
                            }
                    });
                });
                $("#div_summary_report").show();

                $("#IRSUSDTab").show();
                $("#btnUSTExportPdf").show();
                $("#btnUSTExportExcel").show();
                $("#btnEMailMainUST").show();

                $("#div_data").show();
                $("#display_table").append(html);
                $("#div_data_usd").show();
                $("#display_tableusd").append(htmlusd);
                $("#currency_rate").append(htmlcurrency);

                GetHighBal();
                GetHighnav();
            }
            else {
                $("#div_data").hide();
                $("#div_data_usd").hide();
                alert("Data not found")
            }
        },
        error: function (xhr, err) {

            alert(err)
        }
    })
}
function GetHighBal() {
    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val(),
        "client_category": $('#ddlCategoryList :selected').val(),
        "cal_year": $('#ddlCalYearList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/psp_rpt_nri_Highest_balance"
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            html = "";
            htmlusd = "";
            $("#display_table_high_bal").html("");
            $("#display_table_high_balusd").html("");

            if (data.length > 0) {
                html = html + "<tr><th colspan=4 class='TableHeaderRow' style='font-weight:bold'>Highest balance in bank (INR)</th></tr>" +
                    "<tr><th class='TableHeaderRow' style='font-weight:bold'>Particulars</th><th class='TableHeaderRow' style='font-weight:bold'>Account No</th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'>Amount</th><th class='TableHeaderRow' style='font-weight:bold'>Date</th></tr > "

                htmlusd = htmlusd + "<tr><th colspan=4 class='TableHeaderRow' style='font-weight:bold'>Highest balance in bank (USD)</th></tr>" +
                    "<tr><th class='TableHeaderRow' style='font-weight:bold'>Particulars</th><th class='TableHeaderRow' style='font-weight:bold'>Account No</th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'>Amount (USD)</th><th class='TableHeaderRow' style='font-weight:bold'>Date</th></tr > "
                $.each(data, function (index, value) {
                    if (index == data.length - 1 ) {
                        html = html + "<tr><td style = 'border-left:1px solid red;border-right:1px solid red;border-bottom:1px solid red'>" + value.particulars + "</td><td style = 'border-right:1px solid red;border-bottom:1px solid red'>" + value.bank_account_no +
                            "</td><td style = 'border-right:1px solid red;border-bottom:1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right:1px solid red;border-bottom:1px solid red;text-align:center'>" +
                            formatDate(value.trans_date) + "</td></tr>"

                        htmlusd = htmlusd + "<tr><td style = 'border-left:1px solid red;border-right:1px solid red;border-bottom:1px solid red'>" + value.particulars + "</td><td style = 'border-right:1px solid red;border-bottom:1px solid red'>" + value.bank_account_no +
                            "</td><td style = 'border-right:1px solid red;border-bottom:1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right:1px solid red;border-bottom:1px solid red;text-align:center'>" +
                            formatDate(value.trans_date) + "</td></tr>"
                    }
                    else {
                        html = html + "<tr><td style = 'border-left:1px solid red;border-right:1px solid red;'>" + value.particulars + "</td><td style = 'border-right:1px solid red;'>" + value.bank_account_no +
                            "</td><td style = 'border-right:1px solid red;text-align:center'>" + numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border-right:1px solid red;text-align:center'>" +
                            formatDate(value.trans_date) + "</td></tr>"

                        htmlusd = htmlusd + "<tr><td style = 'border-left:1px solid red;border-right:1px solid red;'>" + value.particulars + "</td><td style = 'border-right:1px solid red;'>" + value.bank_account_no +
                            "</td><td style = 'border-right:1px solid red;text-align:center'>" + numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border-right:1px solid red;text-align:center'>" +
                            formatDate(value.trans_date) + "</td></tr>"
                    }
                });
                $("#display_table_high_bal").append(html);
                $("#display_table_high_balusd").append(htmlusd);
            }
            else {
               
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function GetHighnav() {
    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val(),
        "client_category": $('#ddlCategoryList :selected').val(),
        "cal_year": $('#ddlCalYearList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/psp_rpt_nri_Highest_nav"
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            html = "";
            htmlusd = "";

            $("#display_table_high_nav").html("");
            $("#display_table_high_navusd").html("");

            if (data.length > 0) {
                html = html + "<tr><th colspan=4 class='TableHeaderRow' style='font-weight:bold'>Highest NAV (INR)</th></tr>" +
                    "<tr><th class='TableHeaderRow' style='font-weight:bold'>Particulars</th><th class='TableHeaderRow' style='font-weight:bold'>Boid</th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'>Amount</th><th class='TableHeaderRow' style='font-weight:bold'>Date</th></tr > "

                htmlusd = htmlusd + "<tr><th colspan=4 class='TableHeaderRow' style='font-weight:bold'>Highest NAV (USD)</th></tr>" +
                    "<tr><th class='TableHeaderRow' style='font-weight:bold'>Particulars</th><th class='TableHeaderRow' style='font-weight:bold'>Boid</th>" +
                    "<th class='TableHeaderRow' style='font-weight:bold'>Amount (USD)</th><th class='TableHeaderRow' style='font-weight:bold'>Date</th></tr > "
                $.each(data, function (index, value) {
                    html = html + "<tr><td style = 'border:1px solid red;'>" + value.particulars + "</td><td style = 'border:1px solid red;'>" + value.boid + "</td><td style = 'border:1px solid red;text-align:center'>" +
                        numberWithCommas(value.amount.toFixed(2)) + "</td><td style = 'border:1px solid red;text-align:center'>" + formatDate(value.trans_date) + "</td></tr>"

                    htmlusd = htmlusd + "<tr><td style = 'border:1px solid red;'>" + value.particulars + "</td><td style = 'border:1px solid red;'>" + value.boid + "</td><td style = 'border:1px solid red;text-align:center'>" +
                        numberWithCommas(value.usD_amount.toFixed(2)) + "</td><td style = 'border:1px solid red;text-align:center'>" + formatDate(value.trans_date) + "</td></tr>"
                });
                $("#display_table_high_nav").append(html);
                $("#display_table_high_navusd").append(htmlusd);
            }
            else {
               
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function BankInterest(main_client_id, cal_year, client_cat, txt_main_client_id, txt_category) {
    var formdata = {
        "main_client_id": main_client_id,
        "client_cat": client_cat,
        "cal_year": cal_year,
        "flag": "C"
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_Nri_bank_interest_details_drill";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            html = "";
            $("#BankInterest_table").html("");
            if (data.length > 0) {
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].bank_account_no;
          
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                html = html + "<tr><th colspan = 3 class='TableHeaderRow' style='font-weight:bold'>Client : " + txt_main_client_id + " (" + txt_category + ")</th></tr>" 

                $.each(groups, function (index, value1) {
                    if (value1[0].display_order == 3)
                    {
                        html = html + "<tr><td colspan = 3 style = 'border-bottom: 1px solid red' ></td></tr>" 
                        $.each(value1, function (index1, value) {
                            html = html + "<tr><td colspan = 2 style='font-weight:bold;border-top:1px solid red;border:1px solid red;'>GrandTotal</td><td style='font-weight:bold;border:1px solid red;text-align:center;border-top:1px solid red;'>" + numberWithCommas(value.amount.toFixed(2)) + "</td></tr>"
                        });
                    }
                    else {
                        html = html + "<tr><td colspan = 3 style = 'border-bottom: 1px solid red' ></td></tr>" +
                            "<tr><td colspan = 3 style='font-weight:bold;border:1px solid red; text-align:left' >" + value1[0].bank_account_type + " : " + value1[0].bank_account_no + " </th></tr>" +
                            "<tr><th class='TableHeaderRow' style='font-weight:bold'>Date</th><th class='TableHeaderRow' style='font-weight:bold'>Narration</th><th class='TableHeaderRow' style='font-weight:bold'>Amount</th></tr>"
                        $.each(value1, function (index1, value) {
                            if (value.display_order == 1) {
                                html = html + "<tr><td style = 'text-align:center;border-left:1px solid red;border-right:1px solid red;'>" + formatDate(value.trans_date) + "</td><td style ='border-right:1px solid red;'>" + value.narration + "</td><td style = 'text-align:center;border-right:1px solid red;'>" + numberWithCommas(value.amount.toFixed(2)) + "</td></tr>"
                            }
                            else if (value.display_order == 2) {
                                html = html + "<tr><td colspan = 2 style='font-weight:bold;border-top:1px solid red;border:1px solid red;'>Total</td><td style='font-weight:bold;border:1px solid red;text-align:center;border-top:1px solid red;'>" + numberWithCommas(value.amount.toFixed(2)) + "</td></tr>"
                            }
                        });
                    }
                });

                //$("#div_summary_report").hide();
                
                $("#BankInterest_table").append(html);

                $("#btnEMailMainUSTBankInterest").show();
                $("#btnUSTBankInterestExportPdf").show();
                $("#btnUSTBankInterestExportExcel").show();

                $('#NRI_Bank_Interest').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function TaxWithHeld(main_client_id, cal_year, client_cat, txt_main_client_id, txt_category) {
    var formdata = {
        "main_client_id": main_client_id,
        "client_cat": client_cat,
        "cal_year": cal_year
    }
    var posturl = "/ClientPortal/Reports/psp_rpt_nri_client_taxwithheld_drill";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            html = "";
            $("#NRI_TaxWithHeld_table").html("");

            let totalTax = 0;
            
            if (data.length > 0) {
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].category_name;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }

                html = html + "<tr><th colspan=6 class='TableHeaderRow' style='font-weight:bold'>Client : " + txt_main_client_id + " (" + txt_category + ")</th></tr>" 
                html = html + "<tr><td colspan=6 style='border-bottom: 1px solid red'></td></tr>";
                $.each(groups, function (index, value1) {
                    if (index != "Grand Total") {
                        html = html + "<tr><td class='SubTotalRow' colspan=6>" + index + "</td></tr>" +
                            "<tr><th>Date</th>" +
                            "<th>Narration</th>" +
                            "<th>LTCG Amount</th>" +
                            "<th>STCG Amount</th>" +
                            "<th>Interest</th>" +
                            "<th>Dividend</th>" + "</tr>"
                    }
                    $.each(value1, function (index1, value) {
                        if (value.display_order == 1) {
                            html = html + "<tr>" +
                                "<td style = 'text-align:center;'>" + formatDate(value.trans_date) + "</td>" +
                                "<td>" + value.narration + "</td>" +
                                "<td style = 'text-align:center;'>" + numberWithCommas(value.ltcG_amount.toFixed(2)) + "</td>" +
                                "<td style = 'text-align:center;'>" + numberWithCommas(value.stcG_amount.toFixed(2)) + "</td>" +
                                "<td style = 'text-align:center;'>" + numberWithCommas(value.interest.toFixed(2)) + "</td>" +
                                "<td style = 'text-align:center;'>" + numberWithCommas(value.dividend.toFixed(2)) + "</td>" +
                                "</tr>"
                        }
                        else if (value.display_order == 2) {
                            html = html + "<tr class='SubTotalRow'><td colspan=2 >" + value.narration + "</td>" +
                                "<td style='text-align:center;'>" + numberWithCommas(value.ltcG_amount.toFixed(2)) + "</td>" +
                                "<td style='text-align:center;'>" + numberWithCommas(value.stcG_amount.toFixed(2)) + "</td>" +
                                "<td style='text-align:center;'>" + numberWithCommas(value.interest.toFixed(2)) + "</td>" +
                                "<td style='text-align:center;'>" + numberWithCommas(value.dividend.toFixed(2)) + "</td>" +
                                "</tr>";
                        }
                        else if (value.display_order == 3) {

                            totalTax = value.ltcG_amount + value.stcG_amount + value.interest + value.dividend

                            html = html + "<tr class='GrandTotalRow'><td class='GrandTotalRowData' colspan=2 style='text-align:left;' >" + value.narration + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:center;'>" + numberWithCommas(value.ltcG_amount.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:center;'>" + numberWithCommas(value.stcG_amount.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:center;'>" + numberWithCommas(value.interest.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:center;'>" + numberWithCommas(value.dividend.toFixed(2)) + "</td>" +
                                "</tr>";

                            html = html + "<tr class='GrandTotalRow'><td class='GrandTotalRowData' colspan=2 style='text-align:left;'>Total Tax Witheld</td>" +
                                "<td class='GrandTotalRowData' colspan=4>" + numberWithCommas(totalTax.toFixed(2)) + "</td>" +
                                "</tr>";
                        }
                    });
                    

                    //html = html + "<tr><td colspan = 3 style = 'border-bottom: 1px solid red'></td></tr>" +
                    //    "<tr><td colspan = 3 style='font-weight:bold;border:1px solid red; text-align:left' >" + value1[0].bank_account_type + " : " + value1[0].bank_account_no + " </th></tr>" +
                    //    "<tr><th class='TableHeaderRow' style='font-weight:bold'>Date</th><th class='TableHeaderRow' style='font-weight:bold'>Narration</th><th class='TableHeaderRow' style='font-weight:bold'>Amount</th></tr>"
                    //$.each(value1, function (index1, value) {
                    //    if (value.display_order == 1) {
                    //        html = html + "<tr><td style = 'text-align:center;border-left:1px solid red;border-right:1px solid red;'>" + formatDate(value.trans_date) + "</td><td style ='border-right:1px solid red;'>" + value.narration + "</td><td style = 'text-align:center;border-right:1px solid red;'>" + numberWithCommas(value.amount.toFixed(2)) + "</td></tr>"
                    //    }
                    //    else if (value.display_order == 2) {
                    //        html = html + "<tr><td colspan = 2 style='font-weight:bold;border-top:1px solid red;border:1px solid red;'>Total</td><td style='font-weight:bold;border:1px solid red;text-align:center;border-top:1px solid red;'>" + numberWithCommas(value.amount.toFixed(2)) + "</td></tr>"
                    //    }
                    //});
                    
                });
                
                $("#NRI_TaxWithHeld_table").append(html);

                $("#btnUSTNRI_TaxWithHeldPdf").show();
                $("#btnUSTNRI_TaxWithHeldExcel").show();
                $("#btnEMailMainUSTNRI_TaxWithHeld").show();

                $('#NRI_TaxWithHeld').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function Dividend(main_client_id, cal_year, client_cat, txt_main_client_id, txt_category, sub_cat) {
    var formdata = {
        "main_client_id": main_client_id,
        "client_category": client_cat,
        "cal_year": cal_year,
        "sub_category": sub_cat
    }
    var posturl = "/ClientPortal/Reports/psp_rpt_nri_client_dividend";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            $("#DiviInt_table").html("");

            let total = 0;
            console.log(data);

            html = html + "<tr><th colspan=3 class='TableHeaderRow' style='font-weight:bold'>Client : " + txt_main_client_id + " (" + txt_category + ")</th></tr>";
            html = html + "<tr><th>Date</th><th>Security Name</th><th>Amount</th></tr>";

            $.each(data, function (index, value) {
                total = total + value.value;

                html = html + "<tr><td style = 'text-align:center;'>" + formatDate(value.dividend_date) + "</td>" +
                    "<td>" + value.scrip_name + "</td>" +
                    "<td style = 'text-align:center;'>" + numberWithCommas(value.value.toFixed(2)) + "</td>" +
                    "<tr/>"
            });

            html = html + "<tr class='GrandTotalRow'><td colspan=2 class='GrandTotalRowData' style='text-align:left;'>Total</td>" +
                "<td class='GrandTotalRowData'>" + numberWithCommas(total.toFixed(2))  + "</td></tr>";

            $("#DiviInt_table").append(html);

            $("#btnUSTDivi_IntPdf").show();
            $("#btnUSTDivi_IntExcel").show();
            $("#btnEMailMainUSTDivi_Int").show();

            $('#txthidCat').val(sub_cat);

            $('#NRI_Divi_Int').modal({ backdrop: 'static', keyboard: false });
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

$("#btnEMailMainUST").click(function () {

    GenerateExportDetails("#btnEMailMainUST", "E", "PDF")

})
$("#btnUSTExportPdf").click(function () {

    GenerateExportDetails("#btnUSTExportPdf", "X", "PDF")

})
$("#btnUSTExportExcel").click(function () {

    GenerateExportDetails("#btnUSTExportExcel", "X", "EXCEL")
})

function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    var LoginId = $("#LoggedInUID").val()

    var main_client_id = $('#ddlUSClientList :selected').val();
    var client_category = $('#ddlCategoryList :selected').val();
    var cal_year = $('#ddlCalYearList :selected').val();
    var txt_main_client_id = $('#ddlUSClientList :selected').text();

    var strPara = LoginId + "~" + main_client_id + "~" + client_category + "~" + cal_year;

    var strDescrip = "US Taxation Report for " + txt_main_client_id + " for year " + cal_year 

    PDFandExcelExport(evt_btn, "36", LoginId, "0", export_type, export_format, strPara, strDescrip);
}

function ExportDividend(evt_btn, export_type, export_format, category, desc) { //nri dividend

    var LoginId = $("#LoggedInUID").val();

    var main_client_id = $('#ddlUSClientList :selected').val();
    var client_category = $('#ddlCategoryList :selected').val();
    var cal_year = $('#ddlCalYearList :selected').val();
    var txt_main_client_id = $('#ddlUSClientList :selected').text();

    var strPara = main_client_id + "~" + cal_year + "~" + client_category + "~" + category;

    var strDescrip = "US Taxation " + desc  +" Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport(evt_btn, "43", LoginId, "0", export_type, export_format, strPara, strDescrip);
}

//function BankInterest(evt_btn, export_type, export_format, desc) { //nri dividend

//    var LoginId = $("#LoggedInUID").val();

//    var main_client_id = $('#ddlUSClientList :selected').val();
//    var client_category = $('#ddlCategoryList :selected').val();
//    var cal_year = $('#ddlCalYearList :selected').val();
//    var txt_main_client_id = $('#ddlUSClientList :selected').text();

//    var strPara = main_client_id + "~" + cal_year + "~" + client_category;

//    var strDescrip = "US Taxation " + desc + " Report for " + txt_main_client_id + " for year " + cal_year

//    PDFandExcelExport(evt_btn, "44", LoginId, "0", export_type, export_format, strPara, strDescrip);
//}

// ***** Bank Interest *****
$("#btnEMailMainUSTBankInterest").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category;

    let strDescrip = "US Taxation Bank Interest Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnEMailMainUSTBankInterest", "44", LoginId, "0", "E", "PDF", strPara, strDescrip);

})
$("#btnUSTBankInterestExportPdf").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category;

    let strDescrip = "US Taxation Bank Interest Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnUSTBankInterestExportPdf", "44", LoginId, "0", "X", "PDF", strPara, strDescrip);

})
$("#btnUSTBankInterestExportExcel").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category;

    let strDescrip = "US Taxation Bank Interest Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnUSTBankInterestExportExcel", "44", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})
// ***** Bank Interest *****



// ***** TaxWithheld *****
$("#btnEMailMainUSTNRI_TaxWithHeld").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category;

    let strDescrip = "US Taxation Taxwitheld Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnEMailMainUSTNRI_TaxWithHeld", "45", LoginId, "0", "E", "PDF", strPara, strDescrip);

})
$("#btnUSTNRI_TaxWithHeldPdf").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category;

    let strDescrip = "US Taxation Taxwitheld Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnUSTNRI_TaxWithHeldPdf", "45", LoginId, "0", "X", "PDF", strPara, strDescrip);

})
$("#btnUSTNRI_TaxWithHeldExcel").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category;

    let strDescrip = "US Taxation Taxwitheld Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnUSTNRI_TaxWithHeldExcel", "45", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})
// ***** TaxWithheld *****

// ***** Dividend/Interest *****
$("#btnEMailMainUSTDivi_Int").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();
    let cat = $('#txthidCat').val();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category + "~" + cat;

    let strDescrip = "US Taxation " + cat + " Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnEMailMainUSTDivi_Int", "43", LoginId, "0", "E", "PDF", strPara, strDescrip);

})
$("#btnUSTDivi_IntPdf").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();
    let cat = $('#txthidCat').val();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category + "~" + cat;

    let strDescrip = "US Taxation " + cat + " Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnUSTDivi_IntPdf", "43", LoginId, "0", "X", "PDF", strPara, strDescrip);

})
$("#btnUSTDivi_IntExcel").click(function () {

    let LoginId = $("#LoggedInUID").val();

    let main_client_id = $('#ddlUSClientList :selected').val();
    let client_category = $('#ddlCategoryList :selected').val();
    let cal_year = $('#ddlCalYearList :selected').val();
    let txt_main_client_id = $('#ddlUSClientList :selected').text();
    let cat = $('#txthidCat').val();

    let strPara = main_client_id + "~" + cal_year + "~" + client_category + "~" + cat;

    let strDescrip = "US Taxation " + cat + " Report for " + txt_main_client_id + " for year " + cal_year

    PDFandExcelExport("#btnUSTDivi_IntExcel", "43", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})
// ***** Dividend/Interest *****







