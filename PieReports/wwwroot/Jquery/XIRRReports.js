$(document).ready(function () {
    loadXXIRDetails();

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    //gtag('config', 'G-6FJ1RE3WC0'); 
    gtag('config', 'G-6FJ1RE3WC0', {
        'page_title': 'XIRRReports',
        'page_path': '/Reports/XIRRReports'
    });
    gtag('event', 'XIRRReports', {
        'event_category': 'XIRRReports',
        'event_label': 'XIRRReports'
    });

    $("#ddlFinyear").change(function () {
        // $("#hideshow").hide();
        loadXXIRDetails();
    });
    $("#ddlFamilyList").change(function () {
        //   $("#hideshow").hide();
        loadXXIRDetails();
    });
});


//Load Grid
function loadXXIRDetails() {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlFamilyList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/PSPDSPEQUITYDEALERTRACKTDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) { 
            // var json = $.parseJSON(data);
            var html = "";
            if (data.length > 0) {
               // startajaxrequest();
                $("#tbodyBinding").html("");
                var html = ""; 
                $.each(data, function (index, value) {
                    //html = html + "<tr style='border: 1px solid #dddddd;'><td style='cursor: pointer;border: 1px solid #dddddd;text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.account_code + "\')\">" + value.client_name + "</td>" +
                    //    (value.op_valuation_amt <= 0 ? "<td style = 'text-align:right;color: #ff0000' > " + (value.op_valuation_amt == 0 ? '-' : numberWithCommas(value.op_valuation_amt.toFixed(2))) + "<img src='../images/Down1.png' style='width: 10px;' /></td>" : "<td style = 'text-align:right;color: #008000;' > " + (value.op_valuation_amt == 0 ? '-' : numberWithCommas(value.op_valuation_amt.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;' /></td>") +
                    //    //"<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.op_valuation_amt.toFixed(2)) + "</td>" +
                    //    "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.inflow_amt.toFixed(2)) + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.outflow_amt.toFixed(2)) + "</td>" +
                    //    "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.cl_ledger.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.cl_valuation_amt.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.liquid_bees_amt.toFixed(2)) + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.focused_amt.toFixed(2)) + "</td>" +
                    //    "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.focused_percent.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.xirr_ret_yr.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.nifty_ret_yr.toFixed(2)) + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.abs_ret.toFixed(2)) + "</td>" +
                    //    "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.xirr_ret.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.nifty_ret.toFixed(2)) + "</td> </tr>";
                    //    });
                    //html=  html + "<tr style='border: 1px solid #dddddd;'><td style='cursor: pointer;border: 1px solid #dddddd;text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.account_code + "\')\">" + value.client_name + "</td>" +
                    //    (value.op_valuation_amt <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.op_valuation_amt == 0 ? '-' : numberWithCommas(value.op_valuation_amt.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.op_valuation_amt == 0 ? '-' : numberWithCommas(value.op_valuation_amt.toFixed(2))) +"<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    //"<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.op_valuation_amt.toFixed(2)) + "</td>" +
                    //    (value.inflow_amt <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.inflow_amt == 0 ? '-' : numberWithCommas(value.inflow_amt.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.inflow_amt == 0 ? '-' : numberWithCommas(value.inflow_amt.toFixed(2))) +"<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>")+
                    //    (value.outflow_amt <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000';> " + (value.outflow_amt == 0 ? '-' : numberWithCommas(value.outflow_amt.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.outflow_amt == 0 ? '-' : numberWithCommas(value.outflow_amt.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.cl_ledger <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.cl_ledger == 0 ? '-' : numberWithCommas(value.cl_ledger.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.cl_ledger == 0 ? '-' : numberWithCommas(value.cl_ledger.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.cl_valuation_amt <= 0 ? "< td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.cl_valuation_amt == 0 ? '-' : numberWithCommas(value.cl_valuation_amt.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.cl_valuation_amt == 0 ? '-' : numberWithCommas(value.cl_valuation_amt.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.liquid_bees_amt <= 0 ? " <td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.liquid_bees_amt == 0 ? '-' : numberWithCommas(value.liquid_bees_amt.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.liquid_bees_amt == 0 ? '-' : numberWithCommas(value.liquid_bees_amt.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.focused_amt <= 0 ? "</td><td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.focused_amt == 0 ? '-' : numberWithCommas(value.focused_amt.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.focused_amt == 0 ? '-' : numberWithCommas(value.focused_amt.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.focused_percent <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.focused_percent == 0 ? '-' : numberWithCommas(value.focused_percent.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.focused_percent == 0 ? '-' : numberWithCommas(value.focused_percent.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.xirr_ret_yr <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.xirr_ret_yr == 0 ? '-' : numberWithCommas(value.xirr_ret_yr.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.xirr_ret_yr == 0 ? '-' : numberWithCommas(value.xirr_ret_yr.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.nifty_ret_yr <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.nifty_ret_yr == 0 ? '-' : numberWithCommas(value.nifty_ret_yr.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.nifty_ret_yr == 0 ? '-' : numberWithCommas(value.nifty_ret_yr.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.abs_ret <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.abs_ret == 0 ? '-' : numberWithCommas(value.abs_ret.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.abs_ret == 0 ? '-' : numberWithCommas(value.abs_ret.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.xirr_ret <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.xirr_ret == 0 ? '-' : numberWithCommas(value.xirr_ret.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.xirr_ret == 0 ? '-' : numberWithCommas(value.xirr_ret.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    (value.nifty_ret <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.nifty_ret == 0 ? '-' : numberWithCommas(value.nifty_ret.toFixed(2))) + "<img src='../images/down1.png' style='width: 20px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.nifty_ret == 0 ? '-' : numberWithCommas(value.nifty_ret.toFixed(2))) + "<img src='../images/Up1.png' style='width: 20px;float:right;'/></td>") +
                    //    "</tr>";
                    // "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.focused_amt == 0 ? '-' : numberWithCommas(value.focused_amt.toFixed(2))) + "</td>" +
                   // "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.focused_percent == 0 ? '-' : numberWithCommas(value.focused_percent.toFixed(2))) + "</td>" +
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='cursor: pointer;border: 1px solid #dddddd;text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.account_code + "\')\">" + value.client_name + "</td>" +
                        "<td style ='border: 1px solid #dddddd;text-align:right;'>" + (value.op_valuation_amt == 0 ? '-' : numberWithCommas(value.op_valuation_amt.toFixed(2))) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.inflow_amt == 0 ? '-' : numberWithCommas(value.inflow_amt.toFixed(2))) + "</td>" +
                        "<td style ='border: 1px solid #dddddd;text-align:right;'>" + (value.outflow_amt == 0 ? '-' : numberWithCommas(value.outflow_amt.toFixed(2))) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.cl_ledger == 0 ? '-' : numberWithCommas(value.cl_ledger.toFixed(2))) + "</td>" +
                        "<td style ='border: 1px solid #dddddd;text-align:right;'>" + (value.cl_valuation_amt == 0 ? '-' : numberWithCommas(value.cl_valuation_amt.toFixed(2))) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.cl_ledger == 0 && value.cl_valuation_amt==0 ? '-' : numberWithCommas(value.cl_ledger + value.cl_valuation_amt)) + "</td>" +                       
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.xirr_ret_yr == 0 ? '-' : numberWithCommas(value.xirr_ret_yr.toFixed(2))) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.nifty_ret_yr == 0 ? '-' : numberWithCommas(value.nifty_ret_yr.toFixed(2))) + "</td>" +
                        (value.abs_ret <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.abs_ret == 0 ? '-' : numberWithCommas(value.abs_ret.toFixed(2))) + "<img src='../images/down1.png' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.abs_ret == 0 ? '-' : numberWithCommas(value.abs_ret.toFixed(2))) + "<img src='../images/Up1.png' style='width: 10px;float:right;'/></td>") +
                        (value.xirr_ret <= 0 ? "<td style='border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.xirr_ret == 0 ? '-' : numberWithCommas(value.xirr_ret.toFixed(2))) + "<img src='../images/down1.png' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.xirr_ret == 0 ? '-' : numberWithCommas(value.xirr_ret.toFixed(2))) + "<img src='../images/Up1.png' style='width: 10px;float:right;'/></td>") +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + (value.nifty_ret == 0 ? '-' : numberWithCommas(value.nifty_ret.toFixed(2))) + "</td>"  +
                        "</tr>";
                });
                $("#tbodyBinding").append(html);
                $("#xFinYear").text($('#ddlFinyear :selected').val());
                $("#finyearshow").text($('#ddlFinyear :selected').text());
                $("#xfamilyName").text($('#ddlFamilyList :selected').text());
                $("#ddlFamilyList").attr("disabled", true);
                $("#hideshow").show("");
                $("#noReFound").html("");
                $('#liRemoveFavourite').show();
               // completeajaxrequest();  "\' ,\'" + value.client_id + "\',\'" + $.trim(value.sub_category) + 
            } else {
                $("#xFinYear").text($('#ddlFinyear :selected').val());
                $("#xfamilyName").text($('#ddlFamilyList :selected').text());
                $("#ddlFamilyList").attr("disabled", true);
                $("#tbodyBinding").html("");
                $("#hideshow").hide("");
                $("#noReFound").html("No Record Found");
                $('#liRemoveFavourite').hide();
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
       // complete: completeajaxrequest
    })
}
function myFunctionloadDivident(client_id) {
    $("#myModalst_XXIR1").modal({ backdrop: 'static', keyboard: false });
    $("#noReFoundxxir").html("");
    $("#tbodyBindingxxir").html("");
    $("#popclient").html("");
    loadXXIRModelDetails(client_id);
    loadIRRclientflow(client_id);
    loadIRREquityClient(client_id);
    $("#popclient").val(client_id);
};
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
            //startajaxrequest();
        },
        success: function (data) { 
            var html = "";
            $("#noReFoundxxir").html("");
            $("#tbodyBindingxxir").html("");
            if (data.length > 0) {
                // startajaxrequest();
                
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'>" + value.fin_year + "</td>";
                    if (value.fin_year != "Inception") {
                        html = html + "<td style = 'cursor: pointer;text-decoration: underline;text-align:right;'  onclick =\"myFunctionloadDividentModal(\'" + value.dividend + "\' ,\'" + client_id + "\',\'" + value.fy + "\')\">" + numberWithCommas(value.dividend.toFixed(2)) + "</td>";

                    } else {
                        html = html + "<td style = 'text-align:right;'>" + numberWithCommas(value.dividend.toFixed(2)) + "</td>";
                    }
                    html = html + "  <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.int_real_profit.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.srt_real_profit.toFixed(2)) + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.long_real_profit.toFixed(2)) + "</td>" +
                        "<td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.abs_ret.toFixed(2)) + "</td> <td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.xirr.toFixed(2)) + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(value.index_xirr.toFixed(2)) + "</td><td style='text-align:right;'>" + numberWithCommas(value.midcap_xirr.toFixed(2)) + "</td></tr>";
                    

                    });
                $("#tbodyBindingxxir").append(html);
                // completeajaxrequest();  "\' ,\'" + value.client_id + "\',\'" + $.trim(value.sub_category) + 
            } else {
                $("#tbodyBindingxxir").html("");

                $("#noReFoundxxir").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        // complete: completeajaxrequest
    })
}

var st_pnl1, client_id1, fy1,family_id1;
function myFunctionloadDividentModal(st_pnl, client_id,fy) {
    if (st_pnl != "0") {
        $("#myModalst_dividend").modal({ backdrop: 'static', keyboard: false });
        $("#txtclient_id_divindend").val(client_id);
        $("#tbodyBindingrealisedDividend").html("");
        $("#noReFoundrealiseddivindend").html("");
        st_pnl1 = st_pnl;
        client_id1 = client_id;
        fy1 = fy;
        family_id1 = $('#ddlFamilyList :selected').val();
        loadDividentXIRRDetails(client_id, fy);
    } else {
        alert("value will zero");
    }

};
function loadDividentXIRRDetails(client_id, fy) {
    var formdata = {
        "yearID": fy,
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
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;' id=" + value.family_id + ">" + value.client_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;' id=" + value.family_id + ">" + formatDate(value.dividend_date) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;' id=" + value.family_id + ">" + value.isin + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;' id=" + value.family_id + ">" + value.scrip_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;float:right;text-align:right;' id=" + value.family_id + ">" + numberWithCommas(value.value.toFixed(2)) + "</p></td></tr>";
                });
                $("#tbodyBindingrealisedDividend").append(html);
                $("#divxFinYear").text($('#ddlFinyear :selected').val());
                $("#divxFamily").text($('#ddlFamilyList :selected').text());
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
function formatDateold(dateStr) {
    const d = new Date(dateStr);
    var datevalu = d.getDate().toString().padStart(2, '0') + '/' + d.getMonth().toString().padStart(2, '0') + '/' + d.getFullYear();
     
    return datevalu;

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

            $("#tbodyBindingclientflow1").append("<tr><td style='border: 1px solid #dddddd;'><p><b>Opening Stock Valuation *</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;'>" + numberWithCommas(data.opening_stock.toFixed(2)) + "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Opening Ledger Balance *</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(data.opening_ledger.toFixed(2)) + "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Inflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick =\"myFunctionloadInflowModal(\'" + data.account_code + "\' ,\'" + client_id + "\',\'" + data.ac_open_date + "\',\'" + "0" +  "\')\">" + numberWithCommas(data.inflow_amount.toFixed(2)) + "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Outflow Amount</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick =\"myFunctionloadInflowModal(\'" + data.account_code + "\' ,\'" + client_id + "\',\'" + data.ac_open_date + "\',\'" + "1" + "\')\">" + numberWithCommas(data.outflow_amount.toFixed(2)) + "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Closing Ledger Balance</b></p></td><td style='border: 1px solid #dddddd;text-align:right;'>" + numberWithCommas(data.closing_ledger.toFixed(2)) + "</td></tr><tr><td style='border: 1px solid #dddddd;'><p><b>Current Valuation</b></p></td><td style='border: 1px solid #dddddd;text-align:right;cursor: pointer;text-decoration:underline;' onclick =\"myFunctionloadValuationModal(\'" + data.account_code + "\' ,\'" + client_id + "\')\">" + numberWithCommas(data.current_value.toFixed(2)) + "</td></tr>");

        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}
var account_code1, client_id1, ac_open_date1, flow1;

function myFunctionloadInflowModal(account_code, client_id, ac_open_date,flow) {
    $("#myModalst_Inflow").modal({ backdrop: 'static', keyboard: false });
    account_code1 = account_code;
    client_id1 = client_id;
    ac_open_date1 = ac_open_date;
    flow1 = flow;
    loadXIRRclientInflow(account_code, client_id, ac_open_date, flow); 

};
var account_code1, client_id1;
function myFunctionloadValuationModal(account_code,client_id) {
    $("#myModalst_Valuation").modal({ backdrop: 'static', keyboard: false });
    account_code1 = account_code;
    client_id1 = client_id;
    loadXIRRclientValuationflow(account_code, client_id);

}; 

$("#btnInflowExportExcel")

function loadXIRRclientInflow(account_code, client_id, ac_open_date, flow) {
    var formdata = {
        "account_code": account_code,
        "trans_date": ac_open_date,
        "flow_type": flow
    }
    var posturl = "/ClientPortal/Reports/PspDspOutFlowDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {
            $("#tbodyBindingrealisedInflow").html("");
            $("#noReFoundrealisedInflow").html("");
            if (data !=null && data.length > 0) {
                // startajaxrequest();

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;'>" + formatDate(value.trans_date) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;'>" + value.remarks + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;float:right;text-align:right;cursor: pointer;text-decoration:underline;' onclick =\"myFunctionloadInflowDetailsModal(\'" + account_code + "\' ,\'" + value.trans_date + "\',\'" + flow + "\')\">" + numberWithCommas(value.amount.toFixed(2)) + "</p></td></tr>" ;
                });
                $("#tbodyBindingrealisedInflow").append(html);
                $("#divxinFinYear").text($('#ddlFinyear :selected').val()); 
                // completeajaxrequest();
            } else {
                $("#tbodyBindingrealisedInflow").html("");
                $("#noReFoundrealisedInflow").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}


function loadXIRRclientValuationflow(account_code, client_id) {
    var formdata = {
        "account_code": account_code,
        "client_code": client_id
    }
    var posturl = "/ClientPortal/Reports/EquityClientHoldingDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {
            $("#tbodyBindingrealisedValuation").html("");
            $("#noReFoundrealisedValuation").html("");
            if (data != null && data.length > 0) {
                // startajaxrequest();

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;'>" + value.scrip_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;cursor: pointer;text-decoration:underline;' onclick =\"myFunctionloadValutionDetailsModal(\'" + value.scrip_code + "\' ,\'" + value.client_code + "\')\">" + value.holding_qty + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;'>" + value.dp_holding_qty + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.holding_rate.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.holding_cost.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.holding_mkt_rate.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.market_value.toFixed(2)) + "</p></td></tr>";
                });
                $("#tbodyBindingrealisedValuation").append(html);
                $("#divxinFinYear").text($('#ddlFinyear :selected').val());
                // completeajaxrequest();
            } else {
                $("#tbodyBindingrealisedValuation").html("");
                $("#noReFoundrealisedValuation").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}




function loadIRREquityClient(client_id) {
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
                html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;'>" + value.scrip_industry + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;text-align:right;'>" + numberWithCommas(value.amount.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;white-space:pre;text-align:right;'>" + numberWithCommas(((value.amount.toFixed(2) * 100) / value.net_amount.toFixed(2)).toFixed(2)) + "</p></td></tr > ";
            });
            $("#tbodyBindingclientflow2").append(html);
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}

var account_code2, trans_date2, flow2;
function myFunctionloadInflowDetailsModal(account_code, trans_date,flow) {
    $("#myModalst_InflowDetails").modal({ backdrop: 'static', keyboard: false });
    account_code2 = account_code;
    trans_date2 = trans_date;
    flow2 = flow;
    loadXIRRclientInflowDetails(account_code, trans_date, flow);

};
function loadXIRRclientInflowDetails(account_code, trans_date, flow) {
    var formdata = {
        "account_code": account_code,
        "trans_date": trans_date,
        "flow_type": flow
    }
    var posturl = "/ClientPortal/Reports/pspdspinflowoutflowDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {
            $("#tbodyBindingrealisedInflowDetails").html("");
            $("#noReFoundrealisedInflowDetails").html("");
            if (data != null && data.length > 0) {
                // startajaxrequest();

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;'>" + formatDate(value.trans_date) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;'>" + value.script_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;float:right;text-align:right;'>" + numberWithCommas(value.trn_qty.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;float:right;text-align:right;'>" + numberWithCommas(value.trn_rate.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;float:right;text-align:right;'>" + numberWithCommas(value.amount.toFixed(2)) + "</p></td></tr>";
                });
                $("#tbodyBindingrealisedInflowDetails").append(html);
                $("#divxinFinYear").text($('#ddlFinyear :selected').val());
                // completeajaxrequest();
            } else {
                $("#tbodyBindingrealisedInflowDetails").html("");
                $("#noReFoundrealisedInflowDetails").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}

var script_code1, client_code1;
function myFunctionloadValutionDetailsModal(script_code, client_code) {
    $("#myModalst_ValuationDetails").modal({ backdrop: 'static', keyboard: false });
    script_code1 = script_code;
    client_code1 = client_code;
    loadXIRRclientValutionDetails(script_code, client_code);

};
function loadXIRRclientValutionDetails(script_code, client_code) {
    var formdata = {
        "script_code": script_code,
        "client_code": client_code
    }
    var posturl = "/ClientPortal/Reports/pspdspcurrentholdingdrilldownDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            //startajaxrequest();
        },
        success: function (data) {
            $("#tbodyBindingrealisedValuationDetails").html("");
            $("#noReFoundrealisedValuationDetails").html("");
            if (data != null && data.length > 0) {
                // startajaxrequest();

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;'>" + value.script_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;'>" + formatDate(value.tr_date) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:center;'>" + numberWithCommas(value.cr_qty.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.tr_rate.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.srt_unreal_profit.toFixed(2)) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;text-align:right;'>" + numberWithCommas(value.long_unreal_profit.toFixed(2)) + "</p></td></tr>";
                });
                $("#tbodyBindingrealisedValuationDetails").append(html); 
            } else {
                $("#tbodyBindingrealisedValuationDetails").html("");
                $("#noReFoundrealisedValuationDetails").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        //  complete: completeajaxrequest
    })
}

$("#btnxirrExport").click(function () {
    window.location = '/ClientPortal/Excel/DownloadXIRRDetailsExcel?finYear=' + Encrypt($('#ddlFinyear :selected').val());
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
});

$("#btnExportPdf").click(function () {

    //window.location = '/ClientPortal/Excel/DownloadPerformanceExcel?GridHtml=' + $('#htmltopdf').html() + ' &Family=' + $('#ddlFamilyList :selected').val()
    window.location = '/ClientPortal/Excel/DownloadXIRRDetailsPdf?finYear=' + Encrypt($('#ddlFinyear :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
});

$("#btnxirrExportPopup").click(function () {
    window.location = '/ClientPortal/Excel/DownloadXIRRPopupExcel?ClientID=' + Encrypt($('#popclient').val());
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnExportPdfPopup").click(function () {
    window.location = '/ClientPortal/Excel/DownloadXIRRPopupPDF?ClientID=' + Encrypt($('#popclient').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnInflowExportExcel").click(function () {
    window.location = '/ClientPortal/Excel/PspDspOutFlowDetailsExcel?account_code=' + Encrypt(account_code1) + '&flow=' + Encrypt(flow1);
     return false;
});

$("#btnInflowExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/PspDspOutFlowDetailsPdf1?account_code=' + Encrypt(account_code1) + '&flow=' + Encrypt(flow1) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    return false;
});

$("#btnxirrvaluationExportxxl").click(function () {
    window.location = '/ClientPortal/Excel/XIRRclientValuationflowExcel?account_code=' + Encrypt(account_code1) + '&client_id=' + Encrypt(client_id1);
    return false;
});

$("#btnxirrvaluationExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/XIRRclientValuationflowPdf?account_code=' + Encrypt(account_code1) + '&client_id=' + Encrypt(client_id1) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    return false;
});

$("#btnxirrDivInOutFlowExportXxl").click(function () {
    window.location = '/ClientPortal/Excel/XIRRDividendinoutflowExcel?client_id=' + Encrypt(client_id1) + '&fy=' + Encrypt(fy1) + '&family_id=' + Encrypt(family_id1);
    return false;
});

$("#btnDivInOutFlowExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/XIRRDividendinoutflowPdf?client_id=' + Encrypt(client_id1) + '&fy=' + Encrypt(fy1) + '&family_id=' + Encrypt(family_id1) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    return false;
});

$("#btnxirrInOutFlowExportXxl").click(function () {
    window.location = '/ClientPortal/Excel/XIRRInoutflowAmountExcel?account_code=' + Encrypt(account_code2) + '&trans_date=' + Encrypt(trans_date2) + '&flow=' + Encrypt(flow2);
    return false;
});

$("#btnxirrInOutFlowExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/XIRRInoutflowAmountPdf?account_code=' + Encrypt(account_code2) + '&trans_date=' + Encrypt(trans_date2) + '&flow=' + Encrypt(flow2) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    return false;
});

$("#btnxirrValuationHoldingExportXxl").click(function () {
    window.location = '/ClientPortal/Excel/XIRRValuationHoldingExcel?client_Code=' + Encrypt(client_code1) + '&script_code=' + Encrypt(script_code1);
    return false;
});

$("#btnxirrValuationHoldingExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/XIRRValuationHoldingPdf?client_Code=' + Encrypt(client_code1) + '&script_code=' + Encrypt(script_code1) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();;
    return false;
});

function XIRRUpdateFev() {
    var formdata = {
        "module_id": $("p[name='XIRR Reports']").attr("id")
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
                        if ($(this).attr('id') == $("p[name='XIRR Reports']").attr("id")) {
                            $("#XIRRAddtoFavourite").hide();
                            $("#XIRRRemoveFavourite").show();
                            return false;
                        } else {
                            $("#XIRRAddtoFavourite").show();
                            $("#XIRRRemoveFavourite").hide();
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
function XIRRDeleteFev() {
    var formdata = {
        "module_id": $("p[name='XIRR Reports']").attr("id")
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
                        if ($(this).attr('id') == $("p[name='XIRR Reports']").attr("id")) {
                            $("#XIRRAddtoFavourite").hide();
                            $("#XIRRRemoveFavourite").show();
                            return false;
                        } else {
                            $("#XIRRAddtoFavourite").show();
                            $("#XIRRRemoveFavourite").hide();
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
                        if ($(this).attr('id') == $("p[name='XIRR Reports']").attr("id")) {
                            $("#XIRRAddtoFavourite").hide();
                            $("#XIRRRemoveFavourite").show();
                            return false;
                        } else {
                            $("#XIRRAddtoFavourite").show();
                            $("#XIRRRemoveFavourite").hide();
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
        if ($(this).attr('id') == $("p[name='XIRR Reports']").attr("id")) {
            $("#XIRRAddtoFavourite").hide();
            $("#XIRRRemoveFavourite").show();
            return false;
        } else {
            $("#XIRRAddtoFavourite").show();
            $("#XIRRRemoveFavourite").hide();
        }
    })
}, 500);