$(document).ready(function () {

    //window.dataLayer = window.dataLayer || [];
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

    if ($("#txtAccCode").val() != "0" && $("#txtAccCode").val() != "") {
        myFunctionLoadData()
    }
});

function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}

function myFunctionLoadData() {

    var acc_code = $("#txtAccCode").val();

    loadXXIRModelDetails(acc_code);
    loadIRRclientflow(acc_code);
    loadXIRREquityClient(acc_code);

    //var url = "/ClientPortal/Reports/XIRR_Summary?acc_code=" + acc_code;
    //window.location.href = url;
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
            
            $("#tbodyBindingXIRRPnL").html("");
            if (data.length > 0) {
                
                var html = "";
                $.each(data, function (index, value) {
                    if (value.fin_year != "Inception") {
                        html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'>" + value.fin_year + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.int_real_profit.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.srt_real_profit.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.long_real_profit.toFixed(2)) + "</td>" +
                            "<td style='cursor: pointer;text-decoration: underline;text-align:right;' onclick =\"loadPNLModal(\'" + client_id + "\',\'" + value.fy + "\',\'" + value.fin_year + "\')\">" + numberWithCommas((parseFloat(value.long_real_profit) + parseFloat(value.srt_real_profit) + parseFloat(value.int_real_profit)).toFixed(2)) + "</td>" +
                            "<td style='cursor: pointer;text-decoration: underline;text-align:right;'  onclick =\"loadDividentModal(\'" + client_id + "\',\'" + value.fy + "\',\'" + value.fin_year + "\')\">" + numberWithCommas(value.dividend.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.abs_ret.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; cursor: pointer;text-decoration: underline;' onclick =\"XirrFlow(\'" + client_id + "\',\'" + value.fy + "\')\">" + numberWithCommas(value.xirr.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.index_xirr.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.midcap_xirr.toFixed(2)) + "</td>" +
                            "</tr>";
                    }
                    else {
                        html = html + "<tr class = 'GrandTotalRow' style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd; font-weight:bold;'>" + value.fin_year + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold;'>" + numberWithCommas(value.int_real_profit.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold;'>" + numberWithCommas(value.srt_real_profit.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold;'>" + numberWithCommas(value.long_real_profit.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold;'>" + numberWithCommas((parseFloat(value.long_real_profit) + parseFloat(value.srt_real_profit) + parseFloat(value.int_real_profit)).toFixed(2)) + "</td>" +
                            "<td style ='text-align:right; font-weight:bold;'>" + numberWithCommas(value.dividend.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold;'>" + numberWithCommas(value.abs_ret.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold; cursor:pointer;text-decoration: underline;' onclick =\"XirrFlow(\'" + client_id + "\',\'" + value.fy + "\')\">" + numberWithCommas(value.xirr.toFixed(2)) + "</td>" +
                            "<td style='border: 1px solid #dddddd;text-align:right; font-weight:bold;'>" + numberWithCommas(value.index_xirr.toFixed(2)) + "</td>" +
                            "<td style='text-align:right; font-weight:bold;'>" + numberWithCommas(value.midcap_xirr.toFixed(2)) + "</td>" +
                            "</tr>";
                    }
                });
                $("#XIRR_Summary").show();
                $("#XIRR_PnL_table").show();
                $("#noReFoundxxir").html("");
                $("#tbodyBindingXIRRPnL").append(html);
                bindreferLink(client_id);
                return true;
            } else {
                
                $("#tbodyBindingXIRRPnL").html("");
                $("#XIRR_PnL_table").hide();
                $("#noReFoundxxir").append("<div class='SubTotalRow'><p class='ReportTableFont paraTextAlignCenter'>" +
                    "No data found. Please check account code entered." +
                    "</p></div>");
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

function bindreferLink(client_id) {

    var formdata = {
        "account_code": client_id
    }

    var posturl = "/ClientPortal/Reports/bindreferLink";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {
            $("#referlink").html("");
            if (data.length > 0) {
                var html = "";
                $.each(data, function (index, value) {

                    html = "<span id='spreferlink' style='font-size:13px;cursor:pointer;text-decoration:underline;color:blue' onclick=\"gettradeexception(\'" + value.client_code + "\')\">" + value.remarks + "</span>"

                });
                $('#referlink').append(html);
            }
            else {
                $("#referlink").html("");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
    });
}
function gettradeexception(client_code) {
    var formdata = {
        "client_code": client_code
    }
    console.log(formdata)
    var posturl = "/ClientPortal/Reports/gettradeexception";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),

        success: function (data) {

            $("#tbodyxirrtradeexception").html("");
            if (data.length > 0) {
                $('#exception_form').modal('show')

                var html = "";
                $.each(data, function (index, value) {

                    html = html + "<tr style = 'border: 1px solid #ff0000;' > " +
                        "<td style = 'border: 1px solid #ff0000;'><p class='ReportTableFont paraTextAlignLeft'>" + value.script_name + "</p></td>" +
                        "<td style = 'border: 1px solid #ff0000;'><p class='ReportTableFont paraTextAlignLeft'>" + value.rec_type + "</p></td>" +
                        "</tr>"

                });
                $('#tbodyxirrtradeexception').append(html);
            }
            else {
                $("#tbodyxirrtradeexception").html("");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
    });
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

            if (data != null) {

                var netInflow = data.inflow_amount - data.outflow_amount;

                $("#tbodyBindingclientflow").append("<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + data.login_Name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + data.client_name + "(" + data.account_code + ")</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + formatDate(data.ac_open_date) + "</p></td></tr>");

                $("#tbodyBindingclientflow1").append("<tr><td style='border: 1px solid #dddddd;'><p><b>Opening Stock Valuation *</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"loadXIRROpeningVal(\'" + data.account_code + "\')\">" + numberWithCommas(data.opening_stock.toFixed(2)) +
                    "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Opening Ledger Balance *</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(data.opening_ledger.toFixed(2)) + "</td></tr>" +
                    "<tr><td style='border: 1px solid #dddddd;'><p><b>Inflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"EquityClientInFlowDetails(\'" + data.account_code + "\')\">" + numberWithCommas(data.inflow_amount.toFixed(2)) +
                    "</td></tr > <tr><td style='border: 1px solid #dddddd;'><p><b>Outflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"EquityClientOutFlowDetails(\'" + data.account_code + "\')\">" + numberWithCommas(data.outflow_amount.toFixed(2)) +
                    "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Netflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(netInflow.toFixed(2)) +
                    "<tr><td style='border: 1px solid #dddddd;'><p><b>Closing Ledger Balance</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(data.closing_ledger.toFixed(2)) +
                    "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Current Valuation</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick=\"loadXIRRClosingVal('" + data.account_code + "')\">" + numberWithCommas(data.current_value.toFixed(2)) + "</td></tr>" +
                    "<tr><td colspan='2' style='border: 1px solid #dddddd;'><p class='negavtiveValueField'>* For accounts opened after 01/04/2012 value is 0</p></td></tr>");
                $("#client_acc_code").val(data.account_code);
                $("#client_name").val(data.client_name);
                $("#family_name").val(data.login_Name);

                $("#XIRR_Summary").show();
            }
            else {
                $("#XIRR_Summary").hide();
            }
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
                    + "<td style = 'border: 1px solid #dddddd;' > <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;text-align:right;'>" + numberWithCommas(((value.amount.toFixed(2) * 100) / value.net_amount.toFixed(2)).toFixed(2)) + "</p></td ></tr> ";
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
                $("#divViewRepo").hide();
                $("#XIRR_Summary").hide();
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

            let hldCost, currVal, PnL = 0.00;

            if (data.length > 0) {

                $("#divViewRepo").hide();
                $("#XIRR_Summary").hide();
                $("#XIRR_Client_List").hide();
                $("#div_XIRR_Opening_Val").hide();

                $('#XIRR_Closing_table').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                        let api = this.api();

                        $("#txtFamName").text($("#family_name").val());
                        $("#txtCliName").text($("#client_name").val() + " (" + $("#client_acc_code").val() + ")");

                        $("#txtTHC").text(numberWithCommas(api.column(4).data().sum().toFixed()));
                        $("#txtTVal").text(numberWithCommas(api.column(6).data().sum().toFixed()));
                        $("#txtTPnL").text(numberWithCommas(api.column(8).data().sum().toFixed()));

                        $("#client_acc_code").val()
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
                                hldCost += parseFloat(src.holding_cost);
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

                //$("#tbodyclientDetailsClosing").html("");
                //$("#tbodyclientDetailsClosing").append("<tr><td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_acc_code").val() + "</p></td>" +
                //    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#client_name").val() + "</p></td>" +
                //    "<td style='border: 1px solid #dddddd; text-align: center;'><p>" + $("#family_name").val() + "</p></td>");
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

            $("#divViewRepo").hide();

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
            $("#XIRR_Summary").hide();
            $("#Div_CurrHolding").hide();
            $("#div_XIRR_Outflow").hide();

            $("#inpFlowType").val("0");
            $("#inpClientCode").val(client_id);

            
            
            $("#btnPdfXIRRCashFlow").show();
            $("#btnExcelXIRRCashFlow").show();

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

                    $("#divViewRepo").hide();
                    $("#XIRR_Summary").hide();
                    $("#Div_CurrHolding").hide();
                    $("#div_XIRR_Inflow").hide();

                    $("#inpFlowType").val("1");
                    $("#inpClientCode").val(client_id);

                    $("#btnPdfXIRRCashFlow").show();
                    $("#btnExcelXIRRCashFlow").show();

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

                $("#txtFY").val(fin_year);
                $("#txtAcccode").val(client_id);

                $("#btnExportXIRRWorkingXls").show();
                $("#btnExportXIRRWorkingPdf").show();
                $("#btnExportXIRRWorkingEmail").show();


                $("#divViewRepo").hide();
                $("#XIRR_Summary").hide();
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

                //$('#XIRR_dividend').modal({ backdrop: 'static', keyboard: false });
                

            }
            else {
                console.log("Error")
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })



}

function modalPopScript(client_code, scrip, scrip_name) {
    let formdata = {
        "client_Code": client_code,
        "script_code": scrip
    }
    const posturl = "/ClientPortal/Reports/psp_dsp_client_script_trades";
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
                let html = "";

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignCenter'>" + formatDate(value.value_date) + "</td>" +
                        "<td class='paraTextAlignCenter'>" + formatDate(value.tr_date) + "</td>" +
                        "<td>" + value.remarks + "</td>" +
                        "<td class='paraTextAlignRight'>" + value.cr_qty.toFixed(2) + "</td>" +
                        "<td class='paraTextAlignRight'>" + value.dr_qty.toFixed(2) + "</td>" +
                        "<td class='paraTextAlignRight'>" + value.tr_rate.toFixed(2) + "</td>" +
                        "<td class='paraTextAlignRight'>" + value.running_balance.toFixed(2) + "</td>" +
                        "</tr>"
                });

                $('#tradeCliName').text($("#client_name").val());
                $('#tradeScripName').text(scrip_name);
                $("#tbodyScriptDet").append(html);
                completeajaxrequestwaitIn();
                $('#CliScripTradesModal').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                console.log("Error")
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}


function loadDividentModal(account_code, fin_year, fy_display) {
    var formdata = {
        "account_code": account_code,
        "fin_year": fin_year
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_equity_client_fy_dividend";

    $("#txtXirrDiviFY").val(fin_year);
    $("#txtXirrDiviCliCode").val(account_code);

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

                $("#tbodyXirrDivi").html("");
                var html = "";
                let grandTotal = 0;

                let cliNameAcc = $("#client_name").val() + " (" + account_code + ")"

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td>" + formatDate(value.trans_date) + "</td>" +
                        "<td>" + value.scrip_name + "</td>" +
                        "<td>" + value.amount.toFixed(2) + "</td>" +
                        "</tr>"
                    grandTotal += value.amount;
                });

                html = html + "<tr class ='GrandTotalRow'>" +
                    "<td class='GrandTotalRowData' style='text-align:left;' colspan='2'>Total</td>" +
                    "<td class='GrandTotalRowData' style='text-align:left;'>" + grandTotal.toFixed(2) + "</td></tr>"


                $("#tbodyXirrDivi").append(html);
                completeajaxrequestwaitIn();

                $('#XirrDiviCliName').text(cliNameAcc);
                $('#XirrDiviFY').text(fy_display);

                $('#btnEMailXirrDivi').show();
                $('#btnExportPdfXirrDivi').show();
                $('#btnExportXirrDivi').show();

                $('#XIRR_dividend').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                console.log("No Dividend data present.")
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function loadPNLModal(account_code, fin_year, fy_display) {

    var formdata = { //Subcategory and source defined in repository
        "Client": account_code,
        "FINYR": fin_year
    }
    var PnLurl = "/ClientPortal/Reports/psp_rpt_detailed_realised_gain_loss";

    $("#txtXirrPNLFY").val(fin_year);
    $("#txtXirrPNLCliCode").val(account_code);

    $.ajax({
        url: PnLurl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.length > 0) {

                $("#tbodyXirrPNL").html("");
                var html = "";
                let prev_scrip_name = "";

                $.each(data, function (index, value) {

                    if (value.dsp_order == "1") {

                        if (prev_scrip_name == "") {
                            prev_scrip_name = value.script_Name;
                            html = html + "<tr><td style='font-weight:bold; text-align:left;' colspan='10'>" + value.script_Name + "</td></tr>"
                        }
                        else if (prev_scrip_name != value.script_Name)
                        {
                            prev_scrip_name = value.script_Name;
                            html = html + "<tr><td style='font-weight:bold; text-align:left;' colspan='11'>" + value.script_Name + "</td></tr>"
                        }
                        html = html + "<tr><td style='text-align:center;'>" + numberWithCommas(value.buy_trn_qty.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + formatDate(value.buy_trn_date) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.buy_trn_rate.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.buy_amt.toFixed(2))  + "</td>" +
                            "<td style='text-align:right;'>" + formatDate(value.sell_trn_date) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.sell_trn_rate.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas((value.sell_amt).toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.lt_pnl.toFixed(2)) + "</td>" +
                            "<td style='text-align:right;'>" + numberWithCommas(value.total_amt.toFixed(2)) + "</td>" +
                            "</tr>"
                    }
                    else {
                        if (value.dsp_order == "2") {
                            html = html + "<tr class='SubTotalRow'>" +
                                "<td colspan='3' style='font-weight:bold; text-align:left;'>" + value.script_Name + "</td>" +
                                "<td style='text-align:right;'>" + numberWithCommas(value.buy_amt.toFixed(2)) + "</td>" +
                                "<td colspan=2></td>" +
                                "<td style='text-align:right;'>" + numberWithCommas((value.sell_amt).toFixed(2)) + "</td>" +
                                "<td style='text-align:right;'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</td>" +
                                "<td style='text-align:right;'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</td>" +
                                "<td style='text-align:right;'>" + numberWithCommas(value.lt_pnl.toFixed(2)) + "</td>" +
                                "<td style='text-align:right;'>" + numberWithCommas(value.total_amt.toFixed(2)) + "</td>" +
                                "</tr>"
                        }
                        else if (value.dsp_order == "3") {
                            html = html + "<tr class='GrandTotalRow'>" +
                                "<td class='GrandTotalRowData' colspan='3' style='font-weight:bold; text-align:left;'>Grand Total</td>" +
                                "<td class='GrandTotalRowData' style='text-align:right;'>" + numberWithCommas(value.buy_amt.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' colspan=2></td>" +
                                "<td class='GrandTotalRowData' style='text-align:right;'>" + numberWithCommas((value.sell_amt).toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:right;'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:right;'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:right;'>" + numberWithCommas(value.lt_pnl.toFixed(2)) + "</td>" +
                                "<td class='GrandTotalRowData' style='text-align:right;'>" + numberWithCommas(value.total_amt.toFixed(2)) + "</td>" +
                                "</tr>"
                        }
                    }
                    
                });

                $("#tbodyXirrPNL").append(html);

                let cliNameAcc = $("#client_name").val() + " (" + account_code + ")"

                $('#XirrPNLCliName').text(cliNameAcc);
                $('#XirrPNLFY').text(fy_display);



                $('#btnExportXirrPNL').show();
                $('#btnExportPdfXirrPNL').show();
                $('#btnEMailXirrPNL').show();


                $('#XIRR_PNL').modal({ backdrop: 'static', keyboard: false });

            }
            else {
                alert("No PNL Data.")
            }


        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

//back buttons

$("#btnXIRRback").click(function () {
    $("#divViewRepo").show();
    $("#XIRR_Client_List").show();

    $("#XIRR_Summary").hide();
})


$("#btnOPBalback").click(function () {

    $("#divViewRepo").show();

    $("#tbodyclientDetailsClosing").html("");
    $("#tbodyclientDetailsOPBal").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();

    $("#XIRR_Summary").show();
})

$("#btnCashFlowBack").click(function () {

    $("#divViewRepo").show();

    $("#tbodyclientDetailsInflowOutFlow").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();
    $("#Inflow_Outflow").hide();
    $("#XIRRWorking").hide();

    $("#XIRR_Summary").show();
})


$("#btnXIRRWorkingback").click(function () {

    $("#divViewRepo").show();

    $("#XIRRWorkingclientDetailsflow").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();
    $("#Inflow_Outflow").hide();
    $("#XIRRWorking").hide();

    $("#XIRR_Summary").show();
})

$("#btnClsBalback").click(function () {

    $("#divViewRepo").show();

    $("#tbodyclientDetailsClosing").html("");
    $("#tbodyclientDetailsOPBal").html("");

    $("#XIRR_Client_List").hide();
    $("#XIRR_Level_3").hide();

    $("#XIRR_Summary").show();
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


//Xirr Dividend
$("#btnEMailXirrDivi").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#txtXirrDiviCliCode").val();
    var fin_year = $("#txtXirrDiviFY").val();

    var famId = "0"

    var strPara = LoginId + "~" + fin_year + "~" + client_code + "~" + client_code;

    var strDescrip = $("#paraXirrDivi").text();

    PDFandExcelExport("#btnEMailXirrDivi", "38", LoginId, famId, "E", "PDF", strPara, strDescrip);
})

$("#btnExportPdfXirrDivi").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#txtXirrDiviCliCode").val();
    var fin_year = $("#txtXirrDiviFY").val();

    var famId = "0"

    var strPara = LoginId + "~" + fin_year + "~" + client_code + "~" + client_code;

    var strDescrip = $("#paraXirrDivi").text();


    PDFandExcelExport("#btnExportPdfXirrDivi", "38", LoginId, famId, "X", "PDF", strPara, strDescrip);
})

$("#btnExportXirrDivi").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#txtXirrDiviCliCode").val();
    var fin_year = $("#txtXirrDiviFY").val();

    var famId = "0"

    var strPara = LoginId + "~" + fin_year + "~" + client_code + "~" + client_code;

    var strDescrip = $("#paraXirrDivi").text();

    PDFandExcelExport("#btnExportXirrDivi", "38", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//Xirr Dividend

//Xirr PNL
$("#btnEMailXirrPNL").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#txtXirrPNLCliCode").val();
    var fin_year = $("#txtXirrPNLFY").val();

    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + client_code + "~" + fin_year + "~" + "Direct Equity" + "~" + "false" + "~" + "true";

    var strDescrip = $("#paraXirrPNL").text();

    PDFandExcelExport("#btnEMailXirrPNL", "39", LoginId, famId, "E", "PDF", strPara, strDescrip);
})

$("#btnExportPdfXirrPNL").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#txtXirrPNLCliCode").val();
    var fin_year = $("#txtXirrPNLFY").val();

    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + client_code + "~" + fin_year + "~" + "Direct Equity" + "~" + "false" + "~" + "true";

    var strDescrip = $("#paraXirrPNL").text();


    PDFandExcelExport("#btnExportPdfXirrPNL", "39", LoginId, famId, "X", "PDF", strPara, strDescrip);
})

$("#btnExportXirrPNL").click(function () {

    var LoginId = $("#LoggedInUID").val()
    var client_code = $("#txtXirrPNLCliCode").val();
    var fin_year = $("#txtXirrPNLFY").val();

    var famId = "0"

    var strPara = LoginId + "~" + client_code + "~" + client_code + "~" + fin_year + "~" + "Direct Equity" + "~" + "false" + "~" + "true";

    var strDescrip = $("#paraXirrPNL").text();

    PDFandExcelExport("#btnExportXirrPNL", "39", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//Xirr PNL


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
    let checked = $('input[name="ShowIndustryXls"]:checked').val();
    let client_code = $("#client_acc_code").val();
    let LoginId = $("#LoggedInUID").val()
    let HoldingType = $("#txtHoldingType").val()
    let famId = "0"
    let client_code_dsp = $("#txtCliName").text();

    let checkedDesc;

    if (checked == "true") {
        checkedDesc = " (Industry wise)";
    }
    else {
        checkedDesc = " (Without Industry)";
    }

    let strPara = LoginId + "~" + client_code + "~" + famId + "~" + HoldingType + "~" + checked;

    let strDescrip = "Holding Statement for account " + client_code_dsp + checkedDesc;

    PDFandExcelExport("#btnExcelXIRRClosingBal", "12", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

    return false;
}

function ExportValuationPDF() {
    let checked = $('input[name="ShowIndustryPDF"]:checked').val();
    let client_code = $("#client_acc_code").val();
    let LoginId = $("#LoggedInUID").val()
    let HoldingType = $("#txtHoldingType").val()
    let famId = "0"
    let client_code_dsp = $("#txtCliName").text();

    let checkedDesc;

    if (checked == "true") {
        checkedDesc = " (Industry wise)";
    }
    else {
        checkedDesc = " (Without Industry)";
    }

    let strPara = LoginId + "~" + client_code + "~" + famId + "~" + HoldingType + "~" + checked;

    let strDescrip = "Holding Statement for account " + client_code_dsp + checkedDesc;

    PDFandExcelExport("#btnPDFXIRRClosingBal", "12", LoginId, famId, "X", "PDF", strPara, strDescrip);

    return false;
}

//btnExcelXIRRCashFlow

$("#btnPdfXIRRCashFlow").click(function () {

    let LoginId = $("#LoggedInUID").val()
    let flowType =  $("#inpFlowType").val();
    let client_code = $("#inpClientCode").val();

    let famId = "0"

    //LoginId~ClientCode~Clients~FlowType

    let strPara = LoginId + "~" + client_code + "~" + client_code + "~" + flowType;

    let strDescrip = "Xirr " + (flowType == "1" ? "Outflow" : "Inflow") + " for account " + client_code;


    PDFandExcelExport("#btnPdfXIRRCashFlow", "48", LoginId, famId, "X", "PDF", strPara, strDescrip);
})

$("#btnExcelXIRRCashFlow").click(function () {

    let LoginId = $("#LoggedInUID").val()
    let flowType = $("#inpFlowType").val();
    let client_code = $("#inpClientCode").val();

    let famId = "0"

    let strPara = LoginId + "~" + client_code + "~" + client_code + "~" + flowType;

    let strDescrip = "Xirr " + (flowType == "1" ? "Outflow" : "Inflow") + " for account " + client_code;

    PDFandExcelExport("#btnExcelXIRRCashFlow", "48", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})

//btnPdfXIRRCashFlow


//btnExportXIRRWorkingXls
$("#btnExportXIRRWorkingEmail").click(function () {

    GenerateExportXirrWorkingDetails("#btnExportXIRRWorkingEmail", "E", "PDF")

})

$("#btnExportXIRRWorkingPdf").click(function () {

    GenerateExportXirrWorkingDetails("#btnExportXIRRWorkingPdf", "X", "PDF")

})

$("#btnExportXIRRWorkingXls").click(function () {

    GenerateExportXirrWorkingDetails("#btnExportXIRRWorkingXls", "X", "EXCEL")

})
//btnExportXIRRWorkingXls

function GenerateExportXirrWorkingDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    const LoginId = $("#LoggedInUID").val()

    const fy = $('#txtFY').val();
    const clientcode = $('#txtAcccode').val();
    

    let strPara = LoginId + "~" + fy + "~" + clientcode + "~" + clientcode;

    let strDescrip = "Xirr Working of Account(" + clientcode + ") for year " + fy;

    PDFandExcelExport(evt_btn, "52", LoginId, "0", export_type, export_format, strPara, strDescrip);

}