$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    GetClientList();
    setPillsDates();
});

function startMINTajaxrequest() {
    $("#GlobalMFwaitIn").css("display", "block");
}
function completeMINTajaxrequest() {
    $("#GlobalMFwaitIn").css("display", "none");
}

$("#ddlFamilyList").change(function () {

    GetClientList();
    $("#main_div").hide();

});
$("#ddlClientList").change(function () {

    $("#main_div").hide();
});


$("#inpStartDate").change(function () {

    var date = $(this).val();
    var Tdaydate = (new Date()).toISOString().split('T')[0];

    var maxDate = formatDateold(addDays(date, 364));

    //$("#inpEndDate").prop("disabled", false);

    if (maxDate > Tdaydate) {
        $("#inpEndDate").val(Tdaydate);
        $("#inpEndDate").attr("min", date);
        $("#inpEndDate").attr("max", Tdaydate);
    }
    else {
        $("#inpEndDate").val(maxDate);
        $("#inpEndDate").attr("min", date);
        $("#inpEndDate").attr("max", maxDate);
    }

});

$("#chkPnL").change(function () {
    if ($(this).prop("checked")) {
        $("#divGF").show();
    }
    else {
        $("#divGF").hide();
    }
})

$("#chkHolding").change(function () {
    if ($(this).prop("checked")) {
        $("#divHldType").show();
    }
    else {
        $("#divHldType").hide();
    }
})

function setExternalVals() {
    $('#ddlClientList').val($('#txtMainCli').val())
}

function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}

function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}

function setPillsDates() {

    var date = (new Date()).toISOString().split('T')[0];

    var formdata = {
        "date": date
    }
    var posturl = "/ClientPortal/Pie/psp_dsp_global_report_date_pills";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                var fy_start_date = formatDateold(data.curr_fy_start_date);
                var fy_end_date = formatDateold(data.curr_fy_end_date);
                var prev_start_date = formatDateold(data.prev_fy_start_date);
                var prev_end_date = formatDateold(data.prev_fy_end_date);

                if (data.q1_Flag == "N") {
                    $("#pill_Q1").addClass("badge-secondary").removeClass("badge-danger");
                    $("#pill_Q1").prop('disabled', true);
                }
                if (data.q2_Flag == "N") {
                    $("#pill_Q2").addClass("badge-secondary").removeClass("badge-danger");
                    $("#pill_Q2").prop('disabled', true);
                }
                if (data.q3_Flag == "N") {
                    $("#pill_Q3").addClass("badge-secondary").removeClass("badge-danger");
                    $("#pill_Q3").prop('disabled', true);
                }

                $("#txt_Q1").val(formatDateold(data.q1_date));
                $("#txt_Q2").val(formatDateold(data.q2_date));
                $("#txt_Q3").val(formatDateold(data.q3_date));

                $("#txt_prev_fy_start_date").val(prev_start_date);
                $("#txt_prev_fy_end_date").val(prev_end_date);
                $("#txt_curr_fy_start_date").val(fy_start_date);
                $("#txt_curr_fy_end_date").val(fy_end_date);

                $("#inpEndDate").val(date);
                $("#inpEndDate").attr("max", date);

                $("#inpStartDate").val(fy_start_date);
                $("#inpStartDate").attr("max", date);

            }
            else {
                alert("No data");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function ChaninedsetPillsDates() {

    var date = (new Date()).toISOString().split('T')[0];

    var formdata = {
        "date": date
    }
    var posturl = "/ClientPortal/Pie/psp_dsp_global_report_date_pills";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                var fy_start_date = formatDateold(data.curr_fy_start_date);
                var fy_end_date = formatDateold(data.curr_fy_end_date);
                var prev_start_date = formatDateold(data.prev_fy_start_date);
                var prev_end_date = formatDateold(data.prev_fy_end_date);

                if (data.q1_Flag == "N") {
                    $("#pill_Q1").addClass("badge-secondary").removeClass("badge-danger");
                    $("#pill_Q1").prop('disabled', true);
                }
                if (data.q2_Flag == "N") {
                    $("#pill_Q2").addClass("badge-secondary").removeClass("badge-danger");
                    $("#pill_Q2").prop('disabled', true);
                }
                if (data.q3_Flag == "N") {
                    $("#pill_Q3").addClass("badge-secondary").removeClass("badge-danger");
                    $("#pill_Q3").prop('disabled', true);
                }

                $("#txt_Q1").val(formatDateold(data.q1_date));
                $("#txt_Q2").val(formatDateold(data.q2_date));
                $("#txt_Q3").val(formatDateold(data.q3_date));

                $("#txt_prev_fy_start_date").val(prev_start_date);
                $("#txt_prev_fy_end_date").val(prev_end_date);
                $("#txt_curr_fy_start_date").val(fy_start_date);
                $("#txt_curr_fy_end_date").val(fy_end_date);

                $("#inpEndDate").val(date);
                $("#inpEndDate").attr("max", date);

                $("#inpStartDate").val(fy_start_date);
                $("#inpStartDate").attr("max", date);

                ViewGlobalReport();

            }
            else {
                alert("No data");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function setPickerDates(clicked_id) {

    var Tdaydate = (new Date()).toISOString().split('T')[0];

    if (clicked_id == "pill_Q1") {

        $("#inpStartDate").val($("#txt_curr_fy_start_date").val());
        $("#inpEndDate").val($("#txt_Q1").val());

        $("#inpStartDate").prop('disabled', true);
        $("#inpEndDate").prop('disabled', true);

    }
    else if (clicked_id == "pill_Q2") {

        $("#inpStartDate").val($("#txt_curr_fy_start_date").val());
        $("#inpEndDate").val($("#txt_Q2").val());

        $("#inpStartDate").prop('disabled', true);
        $("#inpEndDate").prop('disabled', true);

    }
    else if (clicked_id == "pill_Q3") {

        $("#inpStartDate").val($("#txt_curr_fy_start_date").val());
        $("#inpEndDate").val($("#txt_Q3").val());

        $("#inpStartDate").prop('disabled', true);
        $("#inpEndDate").prop('disabled', true);

    }
    else if (clicked_id == "pill_curr_fy") {

        $("#inpStartDate").val($("#txt_curr_fy_start_date").val());

        if (Tdaydate < $("#txt_curr_fy_end_date").val()) {
            $("#inpEndDate").val(Tdaydate);
        }
        else {
            $("#inpEndDate").val($("#txt_curr_fy_end_date").val());
        }

        $("#inpStartDate").prop('disabled', true);
        $("#inpEndDate").prop('disabled', true);

    }
    else if (clicked_id == "pill_prev_fy") {

        $("#inpStartDate").val($("#txt_prev_fy_start_date").val());
        $("#inpEndDate").val($("#txt_prev_fy_end_date").val());

        $("#inpStartDate").prop('disabled', true);
        $("#inpEndDate").prop('disabled', true);

    }
    else if (clicked_id == "pill_cst_date") {


        if (Tdaydate < $("#txt_curr_fy_end_date").val()) {
            $("#inpEndDate").val(Tdaydate);
        }
        else {
            $("#inpEndDate").val($("#txt_curr_fy_end_date").val());
        }

        $("#inpStartDate").val($("#txt_curr_fy_start_date").val());

        $("#inpEndDate").attr("min", $("#txt_curr_fy_start_date").val());

        $("#inpStartDate").prop('disabled', false);
        $("#inpEndDate").prop('disabled', false);
    }

}

function GetClientList() {

    var formdata = {
        "family_id": $('#ddlFamilyList :selected').val()
    }

    var posturl = "/ClientPortal/Mint/psp_dsp_mint_mf_clients";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlClientList").html("");
            var html = "";

            if (data.length > 0) {
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.main_client_id + ">" + value.mint_clientname + "</option>";
                });

                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
            }
            else {
                $("#ddlClientList").append("<option value=0>No Clients</option>");
                $("#ddlClientList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function ViewGlobalReport() {

    let showTrades, showDivi, showPnL, showGF, showHolding, showsummary, firstDsp = "";

    //To remove active Tabs from previous call
    $("#UL_nav_pills").children().each(function () {
        $(this).find("a").removeClass("active")
    });

    $("#divTabContent").children().each(function () {
        $(this).removeClass("active")
    });
    //To remove active Tabs from previous call

    //Holding
    if ($("#chkHolding").prop('checked')) {

        showHolding = "Yes";
        $("#txtHoldingFlag").val("Y");
        $("#Nav_Pill_Holding").show();
        if (firstDsp == "") {
            firstDsp = "HOLDING";
        }
    }
    else {
        showHolding = "No";
        $("#txtHoldingFlag").val("N");
        $("#Nav_Pill_Holding").hide();
    }
    //Holding
    //Holding Type
    if ($("#chkHldType").prop('checked')) {

        showsummary = "Yes";
        var summary = 'Y'
        $("#txtHoldingtypeFlag").val("Y");
    }
    else {
        showsummary = "No";
        var summary = 'N'
        $("#txtHoldingtypeFlag").val("N");
    }
    //Holding Type
    //trades
    if ($("#chkTrades").prop('checked')) {

        showTrades = "Yes";
        $("#txtHoldTradesFlag").val("Y");
        $("#Nav_Pill_Trades").show();

        if (firstDsp == "") {
            firstDsp = "Trades";
        }
    }
    else {
        showTrades = "No";
        $("#txtHoldTradesFlag").val("N");
        $("#Nav_Pill_Trades").hide();
    }
    //trades
    //Dividend
    if ($("#chkDivi").prop('checked')) {

        showDivi = "Yes";
        $("#txtHoldDiviFlag").val("Y");
        $("#Nav_Pill_Dividend").show();

        if (firstDsp == "") {
            firstDsp = "Dividend";
        }
    }
    else {
        showDivi = "No";
        $("#txtHoldDiviFlag").val("N");
        $("#Nav_Pill_Dividend").hide();
    }
    //Dividend
    /*//PNL
    if ($("#chkPnL").prop('checked')) {

        showPnL = "Yes";
        $("#txtHoldPnLFlag").val("Y");
        $("#Nav_Pill_PnL").show();
        if (firstDsp == "") {
            firstDsp = "PNL";
        }
    }
    else {
        showPnL = "No";
        $("#txtHoldPnLFlag").val("N");
        $("#Nav_Pill_PnL").hide();
    }
    //PNL
    //PNL Type
    if ($("#chkGF").prop('checked')) {

        showGF = "Yes";
        $("#txtHoldGFFlag").val("Y");
    }
    else {
        showGF = "No";
        $("#txtHoldGFFlag").val("N");
    }
    //PNL Type
    */

    let main_client_id = $('#ddlClientList :selected').val();
    let from_date = $('#inpStartDate').val();
    let to_date = $('#inpEndDate').val();
    let FINYR = $('#ddlFinyear :selected').val();

    let dsp_start_date = from_date.split("-").reverse().join("-");
    let dsp_end_date = to_date.split("-").reverse().join("-");

    $("#report_start_date").text(dsp_start_date);
    $("#report_end_date").text(dsp_end_date);

    var formdata = {
        "main_client_id": main_client_id,
        "FINYR": FINYR,
        "to_date": to_date,
        "from_date": from_date,
        "showHolding": showHolding,
        "showTrades": showTrades,
        "showDivi": showDivi
        //"showPnL": showPnL
    }

    var posturl = "/ClientPortal/MINT/FetchMFGlobalReport";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startMINTajaxrequest();
        },
        success: function (data) {

            let html_trades = "";
            let html_divi = "";
            let html_holding = "";
            //let html_PnL = "";

            $("#tbodyTrades").html("");
            $("#tbodyHolding").html("");
            $("#tbodyDividend").html("");

            let tradesData = data.transactionData;
            let diviData = data.diviData;
            let holdingData = data.portfolioReturnData;
            //let PnLData = data.pnLData;

            console.log(data)
            //console.log(diviData)
            //console.log(holdingData)


            if (data.status == "Success") {
                if (tradesData.length > 0) {
                    $.each(tradesData, function (index1, value) {
                        html_trades = html_trades + "<tr>" +
                            "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.dspNavDate + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.schemeName + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.txnType + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(parseFloat(value.units).toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(parseFloat(value.nav).toFixed(2)) + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(parseFloat(value.totalAmount).toFixed(2)) + "</p></td>" +
                            "</tr>"

                        //numberWithCommas(value.totalAmount.toFixed())

                    });
                    $("#tbodyTrades").append(html_trades);
                    $("#mainTable_trades").show();
                }
                else {
                    let html = "";
                    html = html + "<tr class='SubTotalRow'><td colspan=6><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Trades section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#tbodyTrades").append(html);
                    $("#mainTable_trades").show();
                }

                if (diviData.length > 0) {
                    $.each(diviData, function (index1, value) {
                        if (value.schemeName != "Total") {
                            html_divi = html_divi + "<tr>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.dspNavDate + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.schemeName + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.totalAmount.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        }
                        else if (value.schemeName == "Total") {
                            html_divi = html_divi + "<tr class='SubTotalRow'>" +
                                "<td colspan=2><p class='ReportTableFont paraTextAlignLeft'>" + value.schemeName + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.totalAmount.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        }

                    });
                    $("#tbodyDividend").append(html_divi);
                    $("#mainTable_dividend").show();
                }
                else {
                    let html = "";
                    html = html + "<tr class='SubTotalRow'><td colspan=3><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Dividend section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#tbodyDividend").append(html);
                    $("#mainTable_dividend").show();
                }

                if (holdingData.length > 0) {
                    $.each(holdingData, function (index1, value) {
                        if (value.schemeName != "Total") {
                            html_holding = html_holding + "<tr>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.schemeName + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.balanceUnits.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.avgCost.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.purchaseValue.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.dspCurrentNavDate + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.currentNav.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.currentAmount.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.share.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.gain.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        }
                        else if (value.schemeName == "Total") {
                            html_holding = html_holding + "<tr class='SubTotalRow'>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.schemeName + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.purchaseValue.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'></p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.currentAmount.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.gain.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        }

                    });
                    $("#tbodyHolding").append(html_holding);
                    $("#mainTable_holding").show();
                }
                else {
                    let html = "";
                    html = html + "<tr class='SubTotalRow'><td colspan=9><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Holding section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#tbodyHolding").append(html);
                    $("#mainTable_holding").show();
                }

                /* PNL
                if (PnLData.length > 0) {
                    $.each(PnLData, function (index1, value) {
                        if (value.schemeName != "Total") {
                            html_PnL = html_PnL + "<tr>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.units + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.purchaseDate + "</p></td>" +
                                
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.balanceUnits.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.avgCost.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.purchaseValue.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.dspCurrentNavDate + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.currentNav.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.currentAmount.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.share.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.gain.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        }
                    //    else if (value.schemeName == "Total") {
                    //        html_PnL = html_PnL + "<tr class='SubTotalRow'>" +
                    //            "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.schemeName + "</p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.purchaseValue.toFixed(2)) + "</p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignCenter'></p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.currentAmount.toFixed(2)) + "</p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'></p></td>" +
                    //            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.gain.toFixed(2)) + "</p></td>" +
                    //            "</tr>"
                    //    }

                    });
                    $("#tbodyHolding").append(html_holding);
                    $("#mainTable_holding").show();
                }
                else {
                    let html = "";
                    html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Holding section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#tbodyHolding").append(html);
                    $("#mainTable_holding").show();
                }
                */

            }
            else {
                alert(data.message)
            }
        },
        complete: function (xhr) {
            completeMINTajaxrequest();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

    // To set Active Tab based on 
    if (firstDsp == "HOLDING") {
        $("#Nav_Pill_Holding .nav-link").addClass("active");
        $("#div_Holding").addClass("active");
    }
    else if (firstDsp == "Trades") {
        $("#Nav_Pill_Trades .nav-link").addClass("active");
        $("#div_Trades").addClass("active");
    }
    else if (firstDsp == "Dividend") {
        $("#Nav_Pill_Dividend .nav-link").addClass("active");
        $("#div_Dividend").addClass("active");

    }
    //else if (firstDsp == "PNL") {
    //    $("#Nav_Pill_PnL .nav-link").addClass("active");
    //    $("#div_PnL").addClass("active");
    //}

    $("#btnEMailMainGlobal").show();
    $("#btnGlobalExportPdf").show();
    $("#btnGlobalExportExcel").show();
    $("#main_div").show();

}


//Exports
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    var LoginId = $("#LoggedInUID").val()

    var main_client_id = $('#ddlClientList :selected').val();
    var start_date = $('#inpStartDate').val();
    var end_date = $('#inpEndDate').val();

    var txt_main_client_id = $('#ddlClientList :selected').text();
    var dsp_start_date = start_date.split("-").reverse().join("-");
    var dsp_end_date = end_date.split("-").reverse().join("-");

    var holdingFlag = $("#txtHoldingFlag").val();
    var holdingTypeflag = $("#txtHoldingtypeFlag").val();
    var tradesFlag = $("#txtHoldTradesFlag").val();
    var DiviFlag = $("#txtHoldDiviFlag").val();
    var IntrFlag = $("#txtHoldIntrFlag").val();
    var PnLFlag = $("#txtHoldPnLFlag").val();
    var GFFlag = $("#txtHoldGFFlag").val();

    var strPara = LoginId + "~" + main_client_id + "~" + start_date + "~" + end_date + "~" + tradesFlag + "~" +
                PnLFlag + "~" + DiviFlag + "~" + IntrFlag + "~" + GFFlag + "~" + holdingFlag + "~" + holdingTypeflag;

    var strDescrip = "Global Report for " + txt_main_client_id + " from " + dsp_start_date + " to " + dsp_end_date

    PDFandExcelExport(evt_btn, "24", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

$("#btnEMailMainGlobal").click(function () {

    //var LoginId = $("#LoggedInUID").val()

    //var main_client_id = $('#ddlClientList :selected').val();
    //var start_date = $('#inpStartDate').val();
    //var end_date = $('#inpEndDate').val();

    //var strPara = LoginId + "~" + main_client_id + "~" + start_date + "~" + end_date;

    //PDFandExcelExport("#btnEMailMainGlobal", "24", LoginId, "0", "E", "PDF", strPara);

    GenerateExportDetails("#btnEMailMainGlobal", "E", "PDF")

})

$("#btnGlobalExportPdf").click(function () {

    //var LoginId = $("#LoggedInUID").val()

    //var main_client_id = $('#ddlClientList :selected').val();
    //var start_date = $('#inpStartDate').val();
    //var end_date = $('#inpEndDate').val();

    //var txt_main_client_id = $('#ddlClientList :selected').text();
    //var dsp_start_date = start_date.split("-").reverse().join("-");
    //var dsp_end_date = end_date.split("-").reverse().join("-");


    //var strPara = LoginId + "~" + main_client_id + "~" + start_date + "~" + end_date;

    //var strDescrip = "Global Report for " + txt_main_client_id + " from " + dsp_start_date + " to " + dsp_end_date

    //PDFandExcelExport("#btnGlobalExportPdf", "24", LoginId, "0", "X", "PDF", strPara, strDescrip);

    GenerateExportDetails("#btnGlobalExportPdf", "X", "PDF")

})

$("#btnGlobalExportExcel").click(function () {

    //var LoginId = $("#LoggedInUID").val()

    //var main_client_id = $('#ddlClientList :selected').val();
    //var start_date = $('#inpStartDate').val();
    //var end_date = $('#inpEndDate').val();

    //var txt_main_client_id = $('#ddlClientList :selected').text();
    //var dsp_start_date = start_date.split("-").reverse().join("-");
    //var dsp_end_date = end_date.split("-").reverse().join("-");

    //var strPara = LoginId + "~" + main_client_id + "~" + start_date + "~" + end_date;

    //var strDescrip = "Global Report for " + txt_main_client_id + " from " + dsp_start_date + " to " + dsp_end_date

    //PDFandExcelExport("#btnGlobalExportExcel", "24", LoginId, "0", "X", "EXCEL", strPara, strDescrip);

    GenerateExportDetails("#btnGlobalExportExcel", "X", "EXCEL")
})
//Exports


