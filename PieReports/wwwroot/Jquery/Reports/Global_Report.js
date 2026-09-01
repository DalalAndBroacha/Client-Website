$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();


    if ($("#txtFamToken").val() == "0") {
        setPillsDates();
        GetClientList();
    }
    else {
        let data = {
            id: $('#txtFamToken').val(),
            text: $('#txtFamName').val()
        };

        if ($('#ddlFamilyList').find("option[value='" + data.id + "']").length) {
            $('#ddlFamilyList').val(data.id).trigger('change');
        } else {
            let newOption = new Option(data.text, data.id, true, true);
            $('#ddlFamilyList').append(newOption).trigger('change');
        }

        //$('#ddlFamilyList').val($('#txtFamToken').val());
        //$('#ddlFamilyList').trigger('change');
        GetClientListChained(); 
    }
});

$("#ddlFamilyList").change(function () {

    GetClientList();
    $("#main_div").hide();

});
$("#ddlClientList").change(function () {

    $("#main_div").hide();
});


$("#inpStartDate").change(function () {

    let date = $(this).val();
    let Tdaydate = (new Date()).toISOString().split('T')[0];

    let maxDate = formatDateold(addDays(date, 365));

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

    let date = (new Date()).toISOString().split('T')[0];

    let formdata = {
        "date": date
    }
    let posturl = "/ClientPortal/Pie/psp_dsp_global_report_date_pills";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                let fy_start_date = formatDateold(data.curr_fy_start_date);
                let fy_end_date = formatDateold(data.curr_fy_end_date);
                let prev_start_date = formatDateold(data.prev_fy_start_date);
                let prev_end_date = formatDateold(data.prev_fy_end_date);

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

    let date = (new Date()).toISOString().split('T')[0];

    let formdata = {
        "date": date
    }
    let posturl = "/ClientPortal/Pie/psp_dsp_global_report_date_pills";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                let fy_start_date = formatDateold(data.curr_fy_start_date);
                let fy_end_date = formatDateold(data.curr_fy_end_date);
                let prev_start_date = formatDateold(data.prev_fy_start_date);
                let prev_end_date = formatDateold(data.prev_fy_end_date);

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

    let Tdaydate = (new Date()).toISOString().split('T')[0];

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

function GetClientListChained() {
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
                    html = html + "<option value=" + value.main_client_id + ">" + value.main_client_name + "</option>";
                });

                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);

                $('#ddlClientList').select2();

                //$('#ddlClientList').val($('#txtMainCli').val())

                setTimeout($('#ddlClientList').val($('#txtMainCli').val()).trigger('change'), 2000)


                //385559

                ChaninedsetPillsDates();
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

function ViewGlobalReport() {

    let showTrades, showLedger, showDivi, showInterest, showPnL, showGF, showHolding, showsummary, summary, firstDsp = "";

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

        if (firstDsp == "") {
            firstDsp = "HOLDING";
        }
    }
    else {
        showHolding = "No";
        $("#txtHoldingFlag").val("N");
    }
    //Holding

    //Holding Type
    if ($("#chkHldType").prop('checked')) {

        showsummary = "Yes";
        summary = 'Y'
        $("#txtHoldingtypeFlag").val("Y");
    }
    else {
        showsummary = "No";
        summary = 'N'
        $("#txtHoldingtypeFlag").val("N");
    }
    //Holding Type
    //trades
    if ($("#chkTrades").prop('checked')) {

        showTrades = "Yes";
        $("#txtHoldTradesFlag").val("Y");

        if (firstDsp == "") {
            firstDsp = "Trades";
        }
    }
    else {
        showTrades = "No";
        $("#txtHoldTradesFlag").val("N");
    }
    //trades

    //ledger
    if ($("#chkLedger").prop('checked')) {

        showLedger = "Yes";
        $("#txtHoldLedgerFlag").val("Y");

        if (firstDsp == "") {
            firstDsp = "Ledger";
        }
    }
    else {
        showLedger = "No";
        $("#txtHoldLedgerFlag").val("N");
    }
    //ledger

    //Dividend
    if ($("#chkDivi").prop('checked')) {

        showDivi = "Yes";
        $("#txtHoldDiviFlag").val("Y");

        if (firstDsp == "") {
            firstDsp = "Dividend";
        }
    }
    else {
        showDivi = "No";
        $("#txtHoldDiviFlag").val("N");
    }
    //Dividend

    //Interest
    if ($("#chkIntr").prop('checked')) {

        showInterest = "Yes";
        $("#txtHoldIntrFlag").val("Y");

        if (firstDsp == "") {
            firstDsp = "Interest";
        }
    }
    else {
        showInterest = "No";
        $("#txtHoldIntrFlag").val("N");
    }
    //Interest

    //PNL
    if ($("#chkPnL").prop('checked')) {

        showPnL = "Yes";
        $("#txtHoldPnLFlag").val("Y");

        if (firstDsp == "") {
            firstDsp = "PNL";
        }
    }
    else {
        showPnL = "No";
        $("#txtHoldPnLFlag").val("N");
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

    let main_client_id = $('#ddlClientList :selected').val();
    let from_date = $('#inpStartDate').val();
    let to_date = $('#inpEndDate').val();
    let FINYR = $('#ddlFinyear :selected').val();

    let dsp_start_date = from_date.split("-").reverse().join("-");

    let dsp_end_date = to_date.split("-").reverse().join("-");

    $("#report_start_date").text(dsp_start_date);
    $("#report_end_date").text(dsp_end_date);

    

    let formdata1 = {
        "main_client_id": main_client_id,
        "from_date": from_date,
        "to_date": to_date
    }
    let formdata2 = {
        "main_client_id": main_client_id,
        "FINYR": "0",
        "ToDate": to_date,
        "summaryFlag": summary
    }
    executeAsynchronously(
        [GetHoldingDetails(formdata2, showHolding, showsummary), GetTradeDetails(formdata1, showTrades), GetLedgerDetails(formdata1, showLedger), GetDividendDetails(formdata1, showDivi),
        GetInterestDetails(formdata1, showInterest), GetGainLossDetails(formdata1, showPnL, showGF)], 2000);


    // To set Active Tab based on 
    if (firstDsp == "HOLDING") {
        $("#Nav_Pill_Holding .nav-link").addClass("active");
        $("#div_Holding").addClass("active");
    }
    else if (firstDsp == "Trades") {
        $("#Nav_Pill_Trades .nav-link").addClass("active");
        $("#div_Trades").addClass("active");
    }
    else if (firstDsp == "Ledger") {
        $("#Nav_Pill_Ledger .nav-link").addClass("active");
        $("#div_Ledger").addClass("active");

    }
    else if (firstDsp == "Dividend") {
        $("#Nav_Pill_Dividend .nav-link").addClass("active");
        $("#div_Dividend").addClass("active");

    }
    else if (firstDsp == "Interest") {
        $("#Nav_Pill_Interest .nav-link").addClass("active");
        $("#div_Interest").addClass("active");

    }
    else if (firstDsp == "PNL") {
        $("#Nav_Pill_PnL .nav-link").addClass("active");
        $("#div_PnL").addClass("active");
    }

    $("#btnEMailMainGlobal").show();
    $("#btnGlobalExportPdf").show();
    $("#btnGlobalExportExcel").show();
    $("#main_div").show();

}

function GetTradeDetails(parameterObj, dspflag) {

    if (dspflag == "Yes") {

        $("#Nav_Pill_Trades").show();

        let tradesurl = "/ClientPortal/Reports/psp_dsp_global_report_trades";
        $.ajax({
            url: tradesurl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            beforeSend: function (xhr) {
                startajaxrequestGlobal();
            },
            success: function (data) {
                let html_trades = "";
                if (data.length > 0) {

                    $("#mainTable_trades").html("");
                    
                    let groups = {};

                    for (let i = 0; i < data.length; i++) {
                        let groupName = data[i].sub_category;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);
                    }

                    $.each(groups, function (index, value1) {

                        html_trades = html_trades + "<tr class='SubTotalRow'><td>" +
                            "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#trds_acc_" + value1[0].div_category + "'" +
                            "aria-expanded='false' aria-controls='divi_acc_" + value1[0].div_category + "'>" +
                            "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                            "</button>&nbsp;" + index + "</td></tr>"
                        html_trades = html_trades + "<tr><td>" +
                            "<div class='collapse' id='trds_acc_" + value1[0].div_category + "' data-parent='#div_Trades'>" +
                            "<div class='global_report_accords'>" + //For setting max height
                            "<table id='trds_Table_" + value1[0].div_category + "' class='table table-hover w-100'>" +
                            "<colgroup><col style='width:10%;'><col style='width:25%;'><col style='width:25%;'><col style='width:10%;'><col style='width:10%;'>" +
                            "<col style='width:10%;'><col style='width:15%;'></colgroup>" +
                            "<thead class='table_sticky_heads'><tr><th>Transaction Date</th><th>Scrip Name</th><th>Remarks</th><th>Transaction Type</th><th>Quantity</th>" +
                            "<th>Rate</th><th>Amount</th></tr></thead><tbody>"
                        $.each(value1, function (index1, value) {
                            html_trades = html_trades + "<tr>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.trans_date) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.remark + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.dr_cr + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.trn_qty.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.trn_rate.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.amount.toFixed(2)) + "</p></td>" +
                                "</tr>"

                        })
                        html_trades = html_trades + "</tbody></table>" +
                            "</div>" + //global_report_accords end
                            "</div>" + //Collapse end
                            "</td></tr>"
                    });
                    $("#mainTable_trades").append(html_trades);
                }
                else {
                    $("#mainTable_trades").html("");

                    html_trades = html_trades + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Trades section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#mainTable_trades").append(html_trades);
                }
            },
            complete: function (xhr) {
                completeajaxrequestGlobal();
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        $("#Nav_Pill_Trades").hide();
    }


}

function GetLedgerDetails(parameterObj, dspflag) {

    if (dspflag == "Yes") {

        $("#Nav_Pill_Ledger").show();

        let posturl = "/ClientPortal/Reports/psp_dsp_global_report_ledger";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            success: function (data) {
                let html = "";
                if (data.length > 0) {

                    $("#mainTable_Ledger").html("");
                    
                    let groups = {};

                    for (let i = 0; i < data.length; i++) {
                        let groupName = data[i].sub_category;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);
                    }

                    $.each(groups, function (index, value1) {

                        html = html + "<tr class='SubTotalRow'><td>" +
                            "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#ledger_acc_" + value1[0].div_category + "'" +
                            "aria-expanded='false' aria-controls='ledger_acc_" + value1[0].div_category + "'>" +
                            "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                            "</button>&nbsp;" + index + "</td></tr>"
                        html = html + "<tr><td>" +
                            "<div class='collapse' id='ledger_acc_" + value1[0].div_category + "' data-parent='#div_Ledger'>" +
                            "<div class='global_report_accords'>" + //For setting max height
                            "<table id='Ledger_Table_" + value1[0].div_category + "' class='table table-hover w-100'>" +
                            "<colgroup><col style='width:10%;'><col style='width:5%;'><col style='width:50%;'><col style='width:5%;'>" +
                            "<col style='width:10%;'><col style='width:10%;'><col style='width:10%;'></colgroup>" +
                            "<thead class='table_sticky_heads'><tr><th>Date</th><th>Exchange</th><th>Narration</th>" +
                            "<th>Doc. No.</th><th>Debit</th><th>Credit</th><th>Balance</th></tr></thead><tbody>"
                        $.each(value1, function (index1, value) {
                            html = html + "<tr>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.tr_date) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.exchange + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.narration + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.document_no + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.debit_mount.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.credit_mount.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.running_balance.toFixed(2)) + "</p></td>" +
                                "</tr>"
                        })
                        html = html + "</tbody></table>" +
                            "</div>" + //global_report_accords end
                            "</div>" + //Collapse end
                            "</td></tr>"


                    });
                    $("#mainTable_Ledger").append(html);
                }
                else {
                    $("#mainTable_Ledger").html("");

                    html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Ledger. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#mainTable_Ledger").append(html);
                }

            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        $("#Nav_Pill_Ledger").hide();
    }
}

function GetDividendDetails(parameterObj, dspflag) {

    if (dspflag == "Yes") {

        $("#Nav_Pill_Dividend").show();

        parameterObj.flag = "D";

        let posturl = "/ClientPortal/Reports/psp_dsp_global_report_dividend";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            success: function (data) {
                let html = "";
                if (data.length > 0) {

                    $("#mainTable_dividend").html("");
                    
                    let groups = {};

                    for (let i = 0; i < data.length; i++) {
                        let groupName = data[i].sub_category;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);
                    }

                    $.each(groups, function (index, value1) {

                        if (value1[0].div_category != "GrandTotal") {
                            html = html + "<tr class='SubTotalRow'><td>" +
                                "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#divi_acc_" + value1[0].div_category + "'" +
                                "aria-expanded='false' aria-controls='divi_acc_" + value1[0].div_category + "'>" +
                                "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                                "</button>&nbsp;" + index + "&nbsp;| <span>Total: " + numberWithCommas(value1[0].value.toFixed(2)) + "</span>" +
                                "</td></tr>"
                            html = html + "<tr><td>" +
                                "<div class='collapse' id='divi_acc_" + value1[0].div_category + "' data-parent='#div_Dividend'>" +
                                "<div class='global_report_accords'>" + //For setting max height
                                "<table id='Divi_Table_" + value1[0].div_category + "' class='table table-hover w-100'>" +
                                "<colgroup><col style='width:15%;'><col style='width:65%;'><col style='width:20%;'></colgroup>" +
                                "<thead class='table_sticky_heads'><tr><th>Date</th><th>Scrip Name</th><th>Amount</th></tr></thead><tbody>"
                            $.each(value1, function (index1, value) {
                                if (value.display_order == "1") {
                                    html = html + "<tr>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.dividend_date) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.value.toFixed(2)) + "</p></td>" +
                                        "</tr>"
                                }
                            })
                            html = html + "</tbody></table>" +
                                "</div>" + //global_report_accords end
                                "</div>" + //Collapse end
                                "</td></tr>"
                        }
                        else {
                            html = html + "<tr class='GrandTotalRow'><td style='font-weight:900;'>" +
                                "Grand Total : " + numberWithCommas(value1[0].value.toFixed(2)) +
                                "</td></tr>"
                        }
                    });
                    $("#mainTable_dividend").append(html);
                }
                else {
                    $("#mainTable_dividend").html("");

                    html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Dividend. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#mainTable_dividend").append(html);
                }
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        $("#Nav_Pill_Dividend").hide();
    }
}

function GetInterestDetails(parameterObj, dspflag) {

    if (dspflag == "Yes") {

        $("#Nav_Pill_Interest").show();

        parameterObj.flag = "I";

        const posturl = "/ClientPortal/Reports/psp_dsp_global_report_dividend";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            success: function (data) {
                let html = "";
                if (data.length > 0) {

                    $("#mainTable_interest").html("");
                    
                    let groups = {};

                    for (let i = 0; i < data.length; i++) {
                        let groupName = data[i].sub_category;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);
                    }

                    $.each(groups, function (index, value1) {

                        if (value1[0].div_category != "GrandTotal") {
                            html = html + "<tr class='SubTotalRow'><td>" +
                                "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#Intrst_acc_" + value1[0].div_category + "'" +
                                "aria-expanded='false' aria-controls='Intrst_acc_" + value1[0].div_category + "'>" +
                                "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                                "</button>&nbsp;" + index + "&nbsp;| <span>Total: " + numberWithCommas(value1[0].value.toFixed(2)) + "</span>" +
                                "</td></tr>"
                            html = html + "<tr><td>" +
                                "<div class='collapse' id='Intrst_acc_" + value1[0].div_category + "' data-parent='#div_Interest'>" +
                                "<div class='global_report_accords'>" + //For setting max height
                                "<table id='Intrst_Table_" + value1[0].div_category + "' class='table table-hover w-100'>" +
                                "<colgroup><col style='width:15%;'><col style='width:65%;'><col style='width:20%;'></colgroup>" +
                                "<thead class='table_sticky_heads'><tr><th>Date</th><th>Scrip Name</th><th>Amount</th></tr></thead><tbody>"
                            $.each(value1, function (index1, value) {
                                if (value.display_order == "1") {
                                    html = html + "<tr>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.dividend_date) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.scrip_name + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.value.toFixed(2)) + "</p></td>" +
                                        "</tr>"
                                }
                            })
                            html = html + "</tbody></table>" +
                                "</div>" + //global_report_accords end
                                "</div>" + //Collapse end
                                "</td></tr>"
                        }
                        else {
                            html = html + "<tr class='GrandTotalRow'><td style='font-weight:900;'>" +
                                "Grand Total : " + numberWithCommas(value1[0].value.toFixed(2)) +
                                "</td></tr>"
                        }
                    });
                    $("#mainTable_interest").append(html);
                }
                else {
                    $("#mainTable_interest").html("");

                    html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Interest. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#mainTable_interest").append(html);
                }
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        $("#Nav_Pill_Interest").hide();
    }
}

function GetGainLossDetails(parameterObj, dspflag, dspGF) {

    if (dspflag == "Yes") {

        $("#Nav_Pill_PnL").show();

        let PnLurl = "/ClientPortal/Reports/psp_dsp_global_report_gain_loss";
        $.ajax({
            url: PnLurl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            success: function (data) {
                let html = "";
                if (data.length > 0) {

                    $("#mainTable_PnL").html("");
                    
                    let groups = {};
                    let CatTotalObj = "";

                    for (let i = 0; i < data.length; i++) {
                        let groupName = data[i].sub_category;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);
                    }

                    //console.log(groups);

                    $.each(groups, function (index, value1) {
                        CatTotalObj = value1.length - 1;

                        if (value1[0].div_category != "GrandTotal") {
                            html = html + "<tr class='SubTotalRow'><td><div class='row'>" +
                                "<div class='col-2'><button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#PnL_acc_" + value1[0].div_category + "'" +
                                "aria-expanded='false' aria-controls='PnL_acc_" + value1[0].div_category + "'>" +
                                "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                                "</button>" + index + "</div>" +
                                "<div class='col-2'><p class='ReportTableFont paraTextAlignLeft'>Total: " + (dspGF == "Yes" ? numberWithCommas(value1[CatTotalObj].total_pnl_gf.toFixed(2)) : numberWithCommas(value1[CatTotalObj].total_pnl.toFixed(2))) + "</p></div>" +
                                "<div class='col-2'><p class='ReportTableFont paraTextAlignLeft'>Intraday: " + numberWithCommas(value1[CatTotalObj].int_pnl.toFixed(2)) + "</p></div>" +
                                "<div class='col-3'><p class='ReportTableFont paraTextAlignLeft'>Short Term:  " + numberWithCommas(value1[CatTotalObj].st_pnl.toFixed(2)) + "</p></div>" +
                                "<div class='col-3'><p class='ReportTableFont paraTextAlignLeft'>Long Term: " + (dspGF == "Yes" ? numberWithCommas(value1[CatTotalObj].lt_pnl_gf.toFixed(2)) : numberWithCommas(value1[CatTotalObj].lt_pnl.toFixed(2))) + "</p></div>" +
                                "</td></td></tr>"
                            html = html + "<tr><td>" +
                                "<div class='collapse' id='PnL_acc_" + value1[0].div_category + "' data-parent='#div_PnL'>" +
                                "<div class='global_report_accords'>" +
                                "<table id='PnL_Table_" + value1[0].div_category + "' class='table table-hover w-100'>" +
                                //"<colgroup><col style='width:15%;'><col style='width:65%;'><col style='width:20%;'></colgroup>" +
                                "<thead class='table_sticky_heads'><tr><th rowspan='2'>Quantity</th><th colspan='3'>Buy</th><th colspan='3'>Sell</th>" +
                                "<th rowspan='2'>Fair Price</th><th rowspan='2'>Acquisition Cost</th>";
                            html = html + (dspGF == "Yes" ? "<th colspan='4'>Gain/Loss With Grandfathering</tr>" : "<th colspan='4'>Gain/Loss Without Grandfathering</tr>") +
                                "<tr><th>Date</th><th>Rate</th><th>Amount</th><th>Date</th><th>Rate</th><th>Amount</th>" +
                                "<th>Intraday</th><th>Short Term</th><th>Long Term</th><th>Total</th></tr>" +
                                "</thead><tbody>";
                            $.each(value1, function (index1, value) {

                                if (index1 == "0") {
                                    html = html + "<tr class='SubTotalRow'><th colspan='13'><p class='ReportTableFont paraTextAlignLeft'>" + value1[0].scrip_name + "</p></tr>";
                                }
                                else {
                                    if (value1[index1].scrip_name != value1[index1 - 1].scrip_name & value.display_order != "3") {
                                        html = html + "<tr class='SubTotalRow'><th colspan='13'><p class='ReportTableFont paraTextAlignLeft'>" + value1[index1].scrip_name + "</p></tr>";
                                    }
                                }
                                if (value.display_order == "1") {
                                    html = html + "<tr>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + numberWithCommas(value.buy_trn_qty.toFixed(2)) + "</p></td>";

                                if (value.remarks_in == "") {
                                    html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.buy_trn_date) + "</p></td>";
                                }
                                else {
                                    console.log(value);
                                    html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.buy_trn_date) +
                                        "<br> <span style='font-size: 10px; color:orangered;'>" + value.remarks_in + "</span>" +
                                        "</p></td>";
                                }

                                html = html +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_trn_rate.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_amount.toFixed(2)) + "</p></td>";

                                if (value.remarks_out == "" ) {
                                    html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.sell_trn_date) + "</p></td>";
                                }
                                else {
                                    html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.sell_trn_date) +
                                        "<br> <span style='font-size: 10px; color:orangered;'>" + value.remarks_out + "</span>" +
                                        "</p></td>";
                                }

                                    html = html +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.sell_trn_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.sell_amount.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + numberWithCommas(value.fair_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + numberWithCommas(value.acquisition_rate.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</p></td>";
                                    if (dspGF == "Yes") {
                                        html = html +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.lt_pnl_gf.toFixed(2)) + "</p></td>" +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_pnl_gf.toFixed(2)) + "</p></td>";
                                    }
                                    else {
                                        html = html +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.lt_pnl.toFixed(2)) + "</p></td>" +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_pnl.toFixed(2)) + "</p></td>";
                                    }
                                    html = html + "</tr>";
                                }
                                else if (value.display_order == "2") {
                                    html = html + "<tr class='SubTotalRow'>" +
                                        "<td colspan='3'>Sub Total " + value.scrip_name + "</td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_amount.toFixed(2)) + "</p></td>" +
                                        "<td colspan='2'></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.sell_amount.toFixed(2)) + "</p></td>" +
                                        "<td colspan='2'></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</p></td>" +
                                        "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</p></td>";
                                    if (dspGF == "Yes") {
                                        html = html +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.lt_pnl_gf.toFixed(2)) + "</p></td>" +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_pnl_gf.toFixed(2)) + "</p></td>";
                                    }
                                    else {
                                        html = html +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.lt_pnl.toFixed(2)) + "</p></td>" +
                                            "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_pnl.toFixed(2)) + "</p></td>";
                                    }
                                    html = html + "</tr>";
                                }
                            })
                            html = html + "</tbody></table>" +
                                "</div>" +
                                "</div>" +
                                "</td></tr>"
                        }
                        else {
                            //html = html + "<tr class='GrandTotalRow'><td style='font-weight:900;'>" +
                            //    "Grand Total | <span>Intraday Total: " + numberWithCommas(value1[0].int_pnl.toFixed(2)) + " | " +
                            //    "Short Term Total: " + numberWithCommas(value1[0].st_pnl.toFixed(2)) + " | " +
                            //    "Long Term Total: " + numberWithCommas(value1[0].lt_pnl_gf.toFixed(2)) + " | " +
                            //    "Total: " + numberWithCommas(value1[0].total_pnl.toFixed(2)) + "</span>" +
                            //    "</td></tr>"

                            html = html + "<tr class='GrandTotalRow'><td style='font-weight:900;'>" +
                                "Grand Total | <span>Total: " + (dspGF == "Yes" ? numberWithCommas(value1[0].total_pnl_gf.toFixed(2)) : numberWithCommas(value1[0].total_pnl.toFixed(2))) + " | " +
                                "Intraday Total: " + numberWithCommas(value1[0].int_pnl.toFixed(2)) + " | " +
                                "Short Term Total: " + numberWithCommas(value1[0].st_pnl.toFixed(2)) + " | " +
                                "Long Term Total: " + (dspGF == "Yes" ? numberWithCommas(value1[0].lt_pnl_gf.toFixed(2)) : numberWithCommas(value1[0].lt_pnl.toFixed(2))) + "</span>" +
                                "</td></tr>"
                        }
                    });
                    $("#mainTable_PnL").append(html);

                    /*$("#gainLossLegend").append('Legend - * = Transmission | $ = Merger/De-Merger');*/
                }
                else {
                    $("#mainTable_PnL").html("");

                    html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Gain/Loss section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#mainTable_PnL").append(html);
                }


            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        $("#Nav_Pill_PnL").hide();
    }
}

function GetHoldingDetails(parameterObj, dspflag, dspsummary) {
    if (dspflag == "Yes") {

        $("#Nav_Pill_Holding").show();
        let posturl = "/ClientPortal/Reports/HoldingSummary";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            beforeSend: function (xhr) {
                startajaxrequestGlobal();
            },
            success: function (data) {
                $("#mainTable_holding").html("");
                let html = "";

                if (data.length > 0) {
                    let groups = {};
                    for (let i = 0; i < data.length; i++) {
                        let groupName = data[i].acc_sub_category;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);

                    }
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
                            if (dspsummary == 'Yes') {
                                html = html + "<colgroup><col style='width:25%;'><col style='width:10%;'><col style='width:10%;'><col style='width:10%;'><col style='width:10%;'>" +
                                    "<col style='width:15%;'><col style='width:10%;'><col style='width:7%;'><col style='width:12%;'></colgroup>" +
                                    "<thead class='table_sticky_heads'><tr><th>Scrip Name</th><th>Quantity</th><th>Wt Cost</th><th>Total Cost</th><th>Mkt rate</th><th>Mkt date</th><th>Mkt Value</th>" +
                                    "<th>Hold %</th><th>Profit / Loss</th></tr></thead><tbody>"
                            }
                            else {
                                html = html + "<colgroup><col style='width:22%;'><col style='width:10%;'><col style='width:7%;'><col style='width:7%;'><col style='width:10%;'><col style='width:6%;'>" +
                                    "<col style='width:6%;'><col style='width:7%;'><col style='width:7%;'><col style='width:6%;'><col style='width:6%;'><col style='width:7%;'></colgroup>" +
                                    "<thead class='table_sticky_heads'><tr>" +
                                    "<th> Scrip Name</th><th>Date</th><th>Quantity</th><th>Rate</th><th>Value</th><th>hold %</th><th>Cum %</th><th>Market Rate</th>" +
                                    "<th>Market Value</th><th>Hold %</th><th>No. of Days</th><th>ST / LT</th></tr></thead><tbody>"
                            }
                            $.each(value1, function (index1, value) {

                                if (dspsummary == 'Yes') {

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
                            if (dspsummary == 'Yes') {
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

                    $("#mainTable_holding").append(html);
                }
                else {
                    $("#mainTable_holding").html("");

                    html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                        "No data found for Holding section. Please check other sections(Tabs) or select different time frame or client." +
                        "</p></td></tr>"

                    $("#mainTable_holding").append(html);

                }
            },
            complete: function (xhr) {
                completeajaxrequestGlobal();
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        $("#Nav_Pill_Holding").hide();
    }
}



//Exports
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()

    let main_client_id = $('#ddlClientList :selected').val();
    let start_date = $('#inpStartDate').val();
    let end_date = $('#inpEndDate').val();

    let txt_main_client_id = $('#ddlClientList :selected').text();
    let dsp_start_date = start_date.split("-").reverse().join("-");
    let dsp_end_date = end_date.split("-").reverse().join("-");

    let holdingFlag = $("#txtHoldingFlag").val();
    let holdingTypeflag = $("#txtHoldingtypeFlag").val();
    let tradesFlag = $("#txtHoldTradesFlag").val();
    let ledgerFlag = $("#txtHoldLedgerFlag").val();
    let DiviFlag = $("#txtHoldDiviFlag").val();
    let IntrFlag = $("#txtHoldIntrFlag").val();
    let PnLFlag = $("#txtHoldPnLFlag").val();
    let GFFlag = $("#txtHoldGFFlag").val();

    let strPara = LoginId + "~" + main_client_id + "~" + start_date + "~" + end_date + "~" + tradesFlag + "~" +
        ledgerFlag + "~" + PnLFlag + "~" + DiviFlag + "~" + IntrFlag + "~" + GFFlag + "~" + holdingFlag + "~" + holdingTypeflag;

    let strDescrip = "Global Report for " + txt_main_client_id + " from " + dsp_start_date + " to " + dsp_end_date

    PDFandExcelExport(evt_btn, "24", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

$("#btnEMailMainGlobal").click(function () {
    GenerateExportDetails("#btnEMailMainGlobal", "E", "PDF")
})

$("#btnGlobalExportPdf").click(function () {
    GenerateExportDetails("#btnGlobalExportPdf", "X", "PDF")
})

$("#btnGlobalExportExcel").click(function () {
    GenerateExportDetails("#btnGlobalExportExcel", "X", "EXCEL")
})
//Exports