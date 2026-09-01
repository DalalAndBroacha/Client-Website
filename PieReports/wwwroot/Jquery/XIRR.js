$(document).ready(function () {

    window.dataLayer = window.dataLayer || [];
    //function gtag() { dataLayer.push(arguments); }
    //gtag('js', new Date());

    ////gtag('config', 'G-6FJ1RE3WC0'); 
    //gtag('config', 'G-6FJ1RE3WC0', {
    //    'page_title': 'XIRR_All_Clients',
    //    'page_path': '/Reports/SIPtable'
    //});
    //gtag('event', 'XIRR_All_Clients', {
    //    'event_category': 'XIRR_All_Clients',
    //    'event_label': 'XIRR_All_Clients'
    //});
    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    console.log(globalDataGridPageLenght);
    
    startajaxrequestwaitIn();

    initialTable();
    
});
function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}


function initialTable() {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
    }
    var posturl = "/ClientPortal/Reports/PSPDSPEQUITYDEALERTRACKTDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.length > 0) {

                console.log(data);

                var html = "";
                $("#XIRR_Family").DataTable().destroy();
                $("#div_Xirr_Family_all").html("");

                html = html + "<table id='XIRR_Family' class='table table-hover w-100 table-bordered'>" +
                    "<thead><tr>" +
                    "<th style='border: 1px solid #dddddd;' colspan='2'><p><b>Client Details</b></p></th>" +
                    "<th style='border: 1px solid #dddddd;' colspan='4'><p><b>Current Value</b></p></th>" +
                    "<th style='border: 1px solid #dddddd;' colspan='2'><p><b>Current Financial Year</b></p></th>" +
                    "<th style='border: 1px solid #dddddd;' colspan='3'><p><b>Since Inception</b></p></th>" +
                    "</tr><tr>" +
                    "<th>Name</th>" +
                    "<th>Account Code</th>" +
                    "<th>Ledger</th>" +
                    "<th>Stock</th>" +
                    "<th>Liquid Bees</th>" +
                    "<th>Cash(%)</th>" +
                    "<th>XIRR</th>" +
                    "<th>Nifty XIRR</th>" +
                    "<th>ABS</th>" +
                    "<th>XIRR</th>" +
                    "<th>Nifty XIRR</th>" +
                    "</tr></thead>" +
                    "<tbody id='tBodyXIRRData'>";

                for (var i = 0; i < data.length; i++) {

                    html = html + "<tr>" +
                        "<td class='paraTextAlignLeft'><p>" + data[i].client_name + "</p></td>" +

                        //"<a class='badge Lightred_Icon' href='/ClientPortal/Reports/GlobalReport?family_token=" + family_token + "&main_client_id=" + value.main_client_id + "'>Global</a>" +
                        "<td class='paraTextAlignCenter'><p><a style='color:black; text-decoration:underline;' href='/ClientPortal/Reports/XIRR_Summary?acc_code=" + data[i].account_code + "' >" + data[i].account_code + "</a></p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].cl_ledger) + "'>" + numberWithCommas(data[i].cl_ledger.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].cl_valuation_amt) + "'>" + numberWithCommas(data[i].cl_valuation_amt.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].liquid_bees_amt) + "'>" + numberWithCommas(data[i].liquid_bees_amt.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].stk_percent) + "'>" + numberWithCommas(data[i].stk_percent.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].xirr_ret_yr) + "'>" + numberWithCommas(data[i].xirr_ret_yr.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].nifty_ret_yr) + "'>" + numberWithCommas(data[i].nifty_ret_yr.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].abs_ret) + "'>" + numberWithCommas(data[i].abs_ret.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].xirr_ret) + "'>" + numberWithCommas(data[i].xirr_ret.toFixed(2)) + "</p></td>" +
                        "<td class='paraTextAlignRight'><p class='" + isNegativeValue(data[i].nifty_ret) + "'>" + numberWithCommas(data[i].nifty_ret.toFixed(2)) + "</p></td>" +
                        "</tr>"
                }

                html = html + "</tbody></table>";

                $("#div_Xirr_Family_all").append(html);

                $('#XIRR_Family').DataTable({
                    "initComplete": function (settings, json) {
                        $("#XIRR_Client_List").show();
                        $('#XIRR_Family').DataTable().columns.adjust().draw();
                        completeajaxrequestwaitIn();
                    },
                    "order": [],
                    "scrollX": true,
                    "pageLength": globalDataGridPageLenght,
                    "lengthMenu": [3, 5, 10, 25, 50, 100],
                });
            }

            else {
                $("#XIRR_Client_List").show();
                $("#div_Xirr_Family_all").append("No Data");
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}


function myFunctionLoadData(acc_code) {
    var flag = false;
    flag = loadXXIRModelDetails(acc_code);
    loadIRRclientflow(acc_code);
    loadXIRREquityClient(acc_code);

    //var url = "/ClientPortal/Reports/XIRR_Summary?acc_code=" + acc_code;
    //window.location.href = url;

    if (flag == true) {
        $("#XIRR_Level_2").show();
    }
}


function loadXXIRModelDetails(client_id) {
    var formdata = {
        "account_code": client_id
    }
    var posturl = "/ClientPortal/Reports/EquityClientFyFactorDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            var html = "";
            $("#noReFoundxxir").html("");
            $("#tbodyBindingXIRRPnL").html("");
            if (data.length > 0) {
                // startajaxrequest();
                var html = "";
                $.each(data, function (index, value) {
                    if (value.fin_year != "Inception") {
                        html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'>" + value.fin_year + "</td>";
                    }
                    else {
                        html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd; font-weight:bold;'>" + value.fin_year + "</td>";
                    }
                    html = html + "  <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.int_real_profit.toFixed(2)) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.srt_real_profit.toFixed(2)) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.long_real_profit.toFixed(2)) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas((parseFloat(value.long_real_profit) + parseFloat(value.srt_real_profit) + parseFloat(value.int_real_profit)).toFixed(2)) + "</td>"
                    if (value.fin_year != "Inception") {
                        html = html + "<td style = 'cursor: pointer;text-decoration: underline;text-align:right;'  onclick =\"myFunctionloadDividentModal(\'" + value.dividend + "\' ,\'" + client_id + "\',\'" + value.fy + "\')\">" + numberWithCommas(value.dividend.toFixed(2)) + "</td>";
                    }
                    else {
                        html = html + "<td style = 'text-align:right;'>" + numberWithCommas(value.dividend.toFixed(2)) + "</td>";
                    }
                    html = html +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.abs_ret.toFixed(2)) + "</td>" +
                    "<td style='border: 1px solid #dddddd;text-align:right; cursor: pointer;text-decoration: underline;' onclick =\"XirrFlow(\'" + client_id + "\',\'" + value.fy + "\')\">" + numberWithCommas(value.xirr.toFixed(2)) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.index_xirr.toFixed(2)) + "</td>" +
                        "<td style='text-align:right;'>" + numberWithCommas(value.midcap_xirr.toFixed(2)) + "</td>" +
                        "</tr>";
                    $("#XIRR_Level_2").show();
                    $("#XIRR_Client_List").hide();
                });
                $("#tbodyBindingXIRRPnL").append(html);
                return true;
            } else {

                $("#tbodyBindingXIRRPnL").html("");
                $("#XIRR_Client_List").show();
                $("#noReFoundxxir").append("No Record Found");
                return false;
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        }
    })
}

function loadIRRclientflow(client_id) {
    var formdata = {
        "account_code": client_id
    }
    var posturl = "/ClientPortal/Reports/EquityClientFlowDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {

            $("#tbodyBindingclientflow").html("");
            $("#tbodyBindingclientflow1").html("");
            $("#tbodyBindingclientflow").append("<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + data.login_Name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + data.client_name + "(" + data.account_code + ")</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + formatDate(data.ac_open_date) + "</p></td></tr>");

            $("#tbodyBindingclientflow1").append("<tr><td style='border: 1px solid #dddddd;'><p><b>Opening Stock Valuation *</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"loadXIRROpeningVal(\'" + data.account_code + "\')\">" + numberWithCommas(data.opening_stock.toFixed(2)) + "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Opening Ledger Balance *</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(data.opening_ledger.toFixed(2)) + "</td></tr>" +
                "<tr><td style='border: 1px solid #dddddd;'><p><b>Inflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"EquityClientInFlowDetails(\'" + data.account_code + "\')\">" + numberWithCommas(data.inflow_amount.toFixed(2)) +
                "</td></tr > <tr><td style='border: 1px solid #dddddd;'><p><b>Outflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"EquityClientOutFlowDetails(\'" + data.account_code + "\')\">" + numberWithCommas(data.outflow_amount.toFixed(2)) + "</td></tr ><tr><td style='border: 1px solid #dddddd;'><p><b>Closing Ledger Balance</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(data.closing_ledger.toFixed(2)) +
                "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Current Valuation</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"loadXIRRClosingVal(\'" + data.account_code + "\')\">" + numberWithCommas(data.current_value.toFixed(2)) + "</td></tr>" +
                "<tr><td colspan='2' style='border: 1px solid #dddddd;'><p class='negavtiveValueField'>* For accounts opened after 01/04/2012 value is 0</p></td></tr>");
            $("#client_acc_code").val(data.account_code);
            $("#client_name").val(data.client_name);
            $("#family_name").val(data.login_Name);
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}

function loadXIRREquityClient(client_id) {
    var formdata = {
        "account_code": client_id
    }
    var posturl = "/ClientPortal/Reports/EquityClientDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {
            var html = "";

            $("#tbodyBindingclientflow2").html("");
            $.each(data, function (index, value) {
                html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + value.scrip_industry + "</p></td>"
                    + "<td style = 'border: 1px solid #dddddd;' > <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;text-align:right;'>" + numberWithCommas(value.amount.toFixed(2)) + "</p></td >"
                    +"<td style = 'border: 1px solid #dddddd;' > <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;text-align:right;'>" + numberWithCommas(((value.amount.toFixed(2) * 100) / value.net_amount.toFixed(2)).toFixed(2)) + "</p></td ></tr> ";
            });
            $("#tbodyBindingclientflow2").append(html);
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}

function loadXIRROpeningVal(client_id) {

    $("#txtHoldingType").val("1");

    var formdata = {
        "account_code": client_id,
        "holding_type": "1"
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_equity_client_holding";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            if (data.length > 0) {
                // startajaxrequest();

                $("#XIRR_Level_2").hide();
                $("#XIRR_Client_List").hide();
                $("#div_XIRR_Closing_Val").hide();

                $('#XIRR_Opening_table').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                    },
                    "pagingType": "full_numbers",
                    "destroy": true,
                    "data": data,
                    "dataSrc": "",
                    "columns": [
                        {
                            data: "scrip_name",
                            render: function (data, type, src) {
                                return "<p class= 'paraTextAlignLeft'>" + src.scrip_name + "</p>";
                            }
                        },
                        {
                            data: "dp_holding_qty",
                            render: function (data, type, src) {
                                if (src.cl_ledger < 0) {
                                    return "<p class='negavtiveValueField'>" + numberWithCommas(src.dp_holding_qty.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.dp_holding_qty.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "holding_qty",
                            render: function (data, type, src) {
                                if (src.holding_qty < 0) {
                                    return "<p class='negavtiveValueField'>" + numberWithCommas(src.holding_qty.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.holding_qty.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "holding_mkt_rate",
                            render: function (data, type, src) {
                                if (src.holding_mkt_rate < 0) {
                                    return "<p class='negavtiveValueField'>" + numberWithCommas(src.holding_mkt_rate.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.holding_mkt_rate.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "market_value",
                            render: function (data, type, src) {
                                
                                if (src.market_value < 0) {
                                    return "<p class='negavtiveValueField'>" + numberWithCommas(src.market_value.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.market_value.toFixed()) + "</p>";
                            }
                        },

                        {
                            data: "net_market_value",
                            render: function (data, type, src) {
                                if (src.net_market_value < 0) {
                                    return "<p class='negavtiveValueField'>" + src.net_market_value.toFixed(2) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.net_market_value.toFixed(2) + "</p>";
                            }
                        },
                        {
                            data: "latest_buy",
                            render: function (data, type, src) {
                                if (src.latest_buy < 0) {
                                    return "<p class='negavtiveValueField'>" + src.latest_buy.toFixed() + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.latest_buy.toFixed() + "</p>";
                            }
                        },
                        {
                            data: "latest_sell",
                            render: function (data, type, src) {
                                if (src.latest_sell < 0) {
                                    return "<p class='negavtiveValueField'>" + src.latest_sell.toFixed() + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.latest_sell.toFixed() + "</p>";
                            }
                        },
                        {
                            data: "remarks"
                        }
                    ]

                });

                $("#tbodyclientDetailsOPBal").append("<tr><td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_acc_code").val() + "</p></td>" +
                    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_name").val() + "</p></td>" +
                    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#family_name").val() + "</p></td>");

                $("#XIRR_Level_3").show();
                $("#XIRR_Opening_table").show();
                $("#div_XIRR_Opening_Val").show();
            }
            else {
                alert("No Data");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        }
    })
}

function loadXIRRClosingVal(client_id) {

    $("#txtHoldingType").val("2");

    var formdata = {
        "account_code": client_id,
        "holding_type": "2"
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_equity_client_holding";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            if (data.length > 0) {

                $("#XIRR_Level_2").hide();
                $("#XIRR_Client_List").hide();
                $("#div_XIRR_Opening_Val").hide();

                $('#XIRR_Closing_table').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                    },
                    "pagingType": "full_numbers",
                    "destroy": true,
                    "data": data,
                    "dataSrc": "",
                    "columns": [
                        {
                            data: "scrip_name",
                            render: function (data, type, src) {
                                return "<p onclick=\"modalPopScript(\'" + src.client_code + "','" + src.scrip_code + "','" + src.scrip_name + "')\" style='cursor:pointer; text-decoration:underline' class='paraTextAlignLeft'>" + src.scrip_name + "</p>"
                            }
                        },
                        {
                            data: "dp_holding_qty",
                            render: function (data, type, src) {
                                if (src.cl_ledger < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.dp_holding_qty.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.dp_holding_qty.toFixed()) + "</p>"
                            }
                        },
                        {
                            data: "holding_qty",
                            render: function (data, type, src) {
                                if (src.holding_qty < 0) {
                                    return "<p onclick=\"modalPopQuantity(\'" + src.client_code + "','" + src.scrip_code + "')\" style='cursor:pointer; text-decoration:underline' class='negavtiveValueField paraTextAlignRight'> " + numberWithCommas(src.holding_qty.toFixed()) + "</p>";
                                }
                                return "<p onclick=\"modalPopQuantity(\'" + src.client_code + "','" + src.scrip_code + "')\" style='cursor:pointer; text-decoration:underline' class='paraTextAlignRight'>" + numberWithCommas(src.holding_qty.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "holding_rate",
                            render: function (data, type, src) {
                                if (src.holding_rate < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.holding_rate.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.holding_rate.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "holding_cost",
                            render: function (data, type, src) {
                                if (src.holding_cost < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.holding_cost.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.holding_cost.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "holding_mkt_rate",
                            render: function (data, type, src) {
                                if (src.holding_mkt_rate < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.holding_mkt_rate.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.holding_mkt_rate.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "market_value",
                            render: function (data, type, src) {
                                
                                if (src.market_value < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.market_value.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.market_value.toFixed()) + "</p>"
                            }
                        },
                        {
                            data: "net_market_value",
                            render: function (data, type, src) {
                                if (src.net_market_value < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.net_market_value.toFixed(2)) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.net_market_value.toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: "gainloss",
                            render: function (data, type, src) {
                                if (src.gainloss < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.gainloss.toFixed()) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + numberWithCommas(src.gainloss.toFixed()) + "</p>";
                            }
                        },
                        {
                            data: "return_abs",
                            render: function (data, type, src) {
                                if (src.return_abs < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + src.return_abs.toFixed(2) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.return_abs.toFixed(2);
                            }
                        }, 
                        {
                            data: "return_xirr",
                            render: function (data, type, src) {
                                if (src.return_xirr < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + src.return_xirr.toFixed(2) + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.return_xirr.toFixed(2) + "</p>";
                            }
                        },
                        {
                            data: "latest_buy",
                            render: function (data, type, src) {
                                if (src.latest_buy < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + src.latest_buy.toFixed() + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.latest_buy.toFixed() + "</p>";
                            }
                        },
                        {
                            data: "latest_sell",
                            render: function (data, type, src) {
                                if (src.latest_sell < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + src.latest_sell.toFixed() + "</p>";
                                }
                                return "<p class= 'paraTextAlignRight'>" + src.latest_sell.toFixed() + "</p>";
                            }
                        },
                        {
                            data: "remarks"
                        }
                    ]

                });

                $("#tbodyclientDetailsClosing").append("<tr><td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_acc_code").val() + "</p></td>" +
                    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_name").val() + "</p></td>" +
                    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#family_name").val() + "</p></td>");
                $("#XIRR_Level_3").show();
                $("#XIRR_Closing_table").show();
                $("#div_XIRR_Closing_Val").show();
                $("#Div_CurrHolding").show();
            }
            else {
                alert("No Data");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        }
    })
}


function EquityClientInFlowDetails(client_id) {
    var formdata = {
        "account_code": client_id,
        "flow_type": "0"
    }
    var posturl = "/ClientPortal/Reports/EquityClientInFlowOutFlowDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {

            $("#tbodyclientDetailsInflowOutFlow").append("<tr><td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_acc_code").val() + "</p></td>" +
                "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_name").val() + "</p></td>" +
                "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#family_name").val() + "</p></td>");

            $('#XIRR_Inflow_table').DataTable({
                "initComplete": function (settings, json) {
                    completeajaxrequestwaitIn();
                },
                "pagingType": "full_numbers",
                "searching": false,
                "ordering": false,
                "destroy": true,
                "data": data,
                "dataSrc": "",
                "columns": [
                    {
                        data: "trans_date",
                        render: function (data, type, src) {
                            return "<p class='paraTextAlignCenter'>" + formatDate(src.trans_date) + "</p>";
                        }
                    },
                    {
                        data: "remarks",
                        render: function (data, type, src) {
                            
                            return "<p class='paraTextAlignLeft'>" + src.remarks + "</p>";
                            
                        }
                    },
                    {
                        data: "amount",
                        render: function (data, type, src) {
                            
                            return "<p class='paraTextAlignRight'>" + numberWithCommas(src.amount.toFixed(2)) + "</p>";
                            //return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.amount.toFixed(2)) + "</p>";
                        }
                    }
                    
                ]

            });
            $("#XIRR_Level_2").hide();
            $("#Div_CurrHolding").hide();
            $("#div_XIRR_Outflow").hide();

            $("#XIRR_Level_3").show();
            $("#Inflow_Outflow").show();
            $("#div_XIRR_Inflow").show();
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        }
    })
}

function EquityClientOutFlowDetails(client_id) {
    var formdata = {
        "account_code": client_id,
        "flow_type": "1"
    }
    var posturl = "/ClientPortal/Reports/EquityClientInFlowOutFlowDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {

            $.ajax({
                url: posturl,
                type: "post",
                contentType: "application/json",
                data: JSON.stringify(formdata),
                beforeSend: function (xhr) {
                    startajaxrequestwaitIn();
                },
                success: function (data) {

                    $("#tbodyclientDetailsInflowOutFlow").append("<tr><td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_acc_code").val() + "</p></td>" +
                        "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_name").val() + "</p></td>" +
                        "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#family_name").val() + "</p></td>");

                    
                    $('#XIRR_Outflow_table').DataTable({
                        "initComplete": function (settings, json) {
                            completeajaxrequestwaitIn();
                        },
                        "pagingType": "full_numbers",
                        "searching": false,
                        "ordering": false,
                        "destroy": true,
                        "data": data,
                        "dataSrc": "",
                        "columns": [
                            {
                                data: "trans_date",
                                render: function (data, type, src) {
                                    return "<p class='paraTextAlignCenter'>" + formatDate(src.trans_date) + "</p>";
                                }
                            },
                            {
                                data: "remarks",
                                render: function (data, type, src) {

                                    return "<p class='paraTextAlignLeft'>" + src.remarks + "</p>";

                                }
                            },
                            {
                                data: "amount",
                                render: function (data, type, src) {
                                    return "<p class='paraTextAlignRight'>" + numberWithCommas(src.amount.toFixed(2)) + "</p>";
                                    //return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.amount.toFixed(2)) + "</p>";
                                }
                            }

                        ]

                    });

                    $("#XIRR_Level_2").hide();
                    $("#Div_CurrHolding").hide();
                    $("#div_XIRR_Inflow").hide();
                    

                    $("#XIRR_Level_3").show();
                    $("#div_XIRR_Outflow").show();
                    $("#Inflow_Outflow").show();
                    
                },
                error: function (xhr, err) {
                    // alert(xhr+"   ,  "+err);
                },
                complete: function (xhr) {
                    completeajaxrequestwaitIn();
                }
            })
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        }
    })
}


function XirrFlow(client_id, fin_year) {

    

    var formdata = {
        "account_code": client_id,
        "fin_year": fin_year
    }
    

    var posturl = "/ClientPortal/Reports/psp_dsp_equity_client_fy_factors";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            if (data.length > 0) {


                $("#XIRRWorkingclientDetailsflow").append("<tr><th style='background-color: #ffb3b3; font-weight: bold; border: 1px solid #dddddd; text-align:center;'>Client</p></th>" +
                    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_name").val() + " (" + $("#client_acc_code").val() + ")</p></td></tr>");

                $('#XIRRWorking_table').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                    },
                    "pagingType": "full_numbers",
                    "searching": false,
                    "ordering": false,
                    "destroy": true,
                    "data": data,
                    "dataSrc": "",
                    "columns": [
                        {
                            data: "date",
                            render: function (data, type, src) {
                                return "<p class='paraTextAlignLeft'>" + formatDate(src.date) + "</p>";
                            }
                        },
                        {
                            data: "remarks",
                            render: function (data, type, src) {

                                return "<p class='paraTextAlignLeft'>" + src.remarks + "</p>";

                            }
                        },
                        {
                            data: "amount",
                            render: function (data, type, src) {
                                if (src.amount >= 0) {
                                    return "<p class='paraTextAlignRight'>" + numberWithCommas(src.amount.toFixed(2)) + "</p>";
                                }
                                else {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.amount.toFixed(2)) + "</p>";
                                }
                            }
                        }

                    ]

                });
                $("#XIRR_Level_2").hide();
                $("#Div_CurrHolding").hide();
                $("#div_XIRR_Outflow").hide();
                $("#Inflow_Outflow").hide();
                $("#div_XIRR_Inflow").hide();

                $("#XIRR_Level_3").show();
                $("#XIRRWorking").show();
            }
            else {
                alert("No data found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        }
    })
}

function modalPopQuantity(client_code, scrip) {

    var formdata = {
        "client_Code": client_code,
        "script_code": scrip
    }
    var posturl = "/ClientPortal/Pie/psp_dsp_current_holding_drill_down";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            if (data.length > 0) {

                $("#tbodyOpenTradesDet").html("");
                var html = "";

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td>" + formatDate(value.tr_date) + "</td>" +
                        "<td>" + value.remarks + "</td>" +
                        "<td>" + value.cr_qty.toFixed(2) + "</td>" +
                        "<td>" + value.tr_rate.toFixed(2) + "</td>" +
                        "<td class='" + isNegativeValue(value.srt_unreal_profit) + "'>" + numberWithCommas(value.srt_unreal_profit.toFixed(2)) + "</td>" +
                        "<td class='" + isNegativeValue(value.long_unreal_profit) + "'>" + numberWithCommas(value.long_unreal_profit.toFixed(2)) + "</td>" +
                        "</tr>"
                });

                $('#OpenTradesCMP').text(data[0].cmp);
                $('#OpenTradesCliName').text(data[0].client_name);
                $('#OpenTradesScripName').text(data[0].script_name);
                
                $("#tbodyOpenTradesDet").append(html);
                completeajaxrequestwaitIn();
                $('#OpenTradesModal').modal({ backdrop: 'static', keyboard: false });

            }
            else {
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })


    
}

function modalPopScript(client_code, scrip, scrip_name) {
    var formdata = {
        "client_Code": client_code,
        "script_code": scrip
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_client_script_trades";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            if (data.length > 0) {

                $("#tbodyScriptDet").html("");
                var html = "";

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td>" + formatDate(value.tr_date) + "</td>" +
                        "<td>" + value.remarks + "</td>" +
                        "<td>" + value.cr_qty.toFixed(2) + "</td>" +
                        "<td>" + value.dr_qty.toFixed(2) + "</td>" +
                        "<td>" + value.tr_rate.toFixed(2) + "</td>" +
                        "<td>" + value.running_balance.toFixed(2) + "</td>" +
                        "</tr>"
                });


                $('#tradeCliName').text($("#client_name").val());
                $('#tradeScripName').text(scrip_name);
                $("#tbodyScriptDet").append(html);
                completeajaxrequestwaitIn();
                $('#CliScripTradesModal').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

//back buttons

$("#btnXIRRback").click(function () {
    $("#XIRR_Client_List").show();
    $("#XIRR_Level_2").hide();
})


$("#btnOPBalback").click(function () {
    $("#tbodyclientDetailsClosing").html("");
    $("#tbodyclientDetailsOPBal").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();

    $("#XIRR_Level_2").show();
})

$("#btnCashFlowBack").click(function () {
    $("#tbodyclientDetailsInflowOutFlow").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();
    $("#Inflow_Outflow").hide();
    $("#XIRRWorking").hide();

    $("#XIRR_Level_2").show(); 
})


$("#btnXIRRWorkingback").click(function () {
    $("#XIRRWorkingclientDetailsflow").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();
    $("#Inflow_Outflow").hide();
    $("#XIRRWorking").hide();

    $("#XIRR_Level_2").show();
})

$("#btnClsBalback").click(function () {
    $("#tbodyclientDetailsClosing").html("");
    $("#tbodyclientDetailsOPBal").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();

    $("#XIRR_Level_2").show();
})

//back buttons


//XIRR Client Summary Report
$("#btnEMailXIRRPageClientWise").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#client_acc_code").val();

    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + "";

    PDFandExcelExport("#btnEMailXIRRPageClientWise", "8", LoginId, famId, "E", "PDF", strPara);
})

$("#btnExportPdfXIRRPageClientWise").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#client_acc_code").val();
    var client_name = $("#client_name").val();
    

    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + "";

    var strDescrip = "XIRR Report for " + client_name;


    PDFandExcelExport("#btnExportPdfXIRRPageClientWise", "8", LoginId, famId, "X", "PDF", strPara, strDescrip);
})

$("#btnExportXIRRPageClientWise").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#client_acc_code").val();
    var client_name = $("#client_name").val();


    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + "";

    var strDescrip = "XIRR Report for " + client_name;

    PDFandExcelExport("#btnExportXIRRPageClientWise", "8", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//XIRR Client Summary Report


//XIRR ALL Client Report 
function GenerateExportDetailsXIRRAllCLients(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    var LoginId = $("#LoggedInUID").val()

    var strPara = LoginId + "~0~0";

    var strDescrip = "XIRR All Clients Summary";

    PDFandExcelExport(evt_btn, "9", LoginId, "0", export_type, export_format, strPara, strDescrip);

}


$("#btnEMailXIRRMainPage").click(function () {

    GenerateExportDetailsXIRRAllCLients("#btnEMailXIRRMainPage", "E", "PDF")

})

$("#btnExportPdfXIRRMainPage").click(function () {

    GenerateExportDetailsXIRRAllCLients("#btnExportPdfXIRRMainPage", "X", "PDF")

})

$("#btnExportXIRRMainPagexls").click(function () {

    GenerateExportDetailsXIRRAllCLients("#btnExportXIRRMainPagexls", "X", "EXCEL")

})
//XIRR ALL Client Report

//XIRR Closing Report

function SendValuationEmail() {
    var checked = $('input[name="ShowIndustryEmail"]:checked').val();
    var client_code = $("#client_acc_code").val();
    var LoginId = $("#LoggedInUID").val()
    var HoldingType = $("#txtHoldingType").val()
    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + famId + "~" + HoldingType + "~" + checked;

    PDFandExcelExport("#btnEmailXIRRClosingBal", "12", LoginId, famId, "E", "PDF", strPara);

    return false;
}

function ExportValuationExcel() {
    var checked = $('input[name="ShowIndustryXls"]:checked').val();
    var client_code = $("#client_acc_code").val();
    var LoginId = $("#LoggedInUID").val()
    var HoldingType = $("#txtHoldingType").val()
    var famId = "0"

    var checkedDesc;

    if (checked == "true") {
        checkedDesc = " (Industry wise)";
    }
    else {
        checkedDesc = " (Without Industry)";
    }

    var strPara = LoginId + "~" + client_code + "~" + famId + "~" + HoldingType + "~" + checked;

    var strDescrip = "Holding Statement for account " + client_code + checkedDesc;

    PDFandExcelExport("#btnExcelXIRRClosingBal", "12", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

    return false;
}

function ExportValuationPDF() {
    var checked = $('input[name="ShowIndustryPDF"]:checked').val();
    var client_code = $("#client_acc_code").val();
    var LoginId = $("#LoggedInUID").val()
    var HoldingType = $("#txtHoldingType").val()
    var famId = "0"

    var checkedDesc;

    if (checked == "true") {
        checkedDesc = " (Industry wise)";
    }
    else {
        checkedDesc = " (Without Industry)";
    }

    var strPara = LoginId + "~" + client_code + "~" + famId + "~" + HoldingType + "~" + checked;

    var strDescrip = "Holding Statement for account " + client_code + checkedDesc;

    PDFandExcelExport("#btnPDFXIRRClosingBal", "12", LoginId, famId, "X", "PDF", strPara, strDescrip);

    return false;
}


//XIRR Closing Report





//Pie Chart
//var colors = ["#727cf5", "#6c757d", "#0acf97", "#fa5c7c", "#e3eaef"],
//    dataColors = $("#simple-pie").data("colors");
//    dataColors && (colors = dataColors.split(","));
//var options = {
//    chart: { height: 320, type: "pie" },
//    series: [44, 55, 41, 17, 15],
//    labels: ["Series 1", "Series 2", "Series 3", "Series 4", "Series 5"],
//    colors: colors,
//    legend: { show: !0, position: "bottom", horizontalAlign: "center", verticalAlign: "middle", floating: !1, fontSize: "14px", offsetX: 0, offsetY: 7 },
//    responsive: [{ breakpoint: 600, options: { chart: { height: 240 }, legend: { show: !1 } } }]
//},
//    chart = new ApexCharts(document.querySelector("#simple-pie"), options);
//    chart.render();

//function treeviewchart() {
//    var options = {
//        series: [
//            {
//                data: [
//                    {
//                        x: 'New Delhi',
//                        y: 218
//                    },
//                    {
//                        x: 'Kolkata',
//                        y: 149
//                    },
//                    {
//                        x: 'Mumbai',
//                        y: 184
//                    },
//                    {
//                        x: 'Ahmedabad',
//                        y: 55
//                    },
//                    {
//                        x: 'Bangaluru',
//                        y: 84
//                    },
//                    {
//                        x: 'Pune',
//                        y: 31
//                    },
//                    {
//                        x: 'Chennai',
//                        y: 70
//                    },
//                    {
//                        x: 'Jaipur',
//                        y: 30
//                    },
//                    {
//                        x: 'Surat',
//                        y: 44
//                    },
//                    {
//                        x: 'Hyderabad',
//                        y: 68
//                    },
//                    {
//                        x: 'Lucknow',
//                        y: 28
//                    },
//                    {
//                        x: 'Indore',
//                        y: 19
//                    },
//                    {
//                        x: 'Kanpur',
//                        y: 29
//                    }
//                ]
//            }
//        ],
//        legend: {
//            show: false
//        },
//        chart: {
//            height: 350,
//            type: 'treemap'
//        },
//        title: {
//            text: 'Distibuted Treemap (different color for each cell)',
//            align: 'center'
//        },
//        colors: [
//            '#3B93A5',
//            '#F7B844',
//            '#ADD8C7',
//            '#EC3C65',
//            '#CDD7B6',
//            '#C1F666',
//            '#D43F97',
//            '#1E5D8C',
//            '#421243',
//            '#7F94B0',
//            '#EF6537',
//            '#C0ADDB'
//        ],
//        plotOptions: {
//            treemap: {
//                distributed: true,
//                enableShades: false
//            }
//        }
//    };

//    var chart = new ApexCharts(document.querySelector("#Treemap"), options);
//    chart.render();
//    $("#XIRR_Client_List").show();
//}