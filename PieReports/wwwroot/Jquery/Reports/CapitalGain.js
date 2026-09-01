$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();

    setPillsDates();
    GetClientList();

    
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

const fnHideParamsTray = function () {
    $("#paramsTray").hide();
    $("#divDataTblComponent").css("max-height", "75vh");
}

const fnShowParamsTray = function () {
    $("#paramsTray").show();
    $("#divDataTblComponent").css("max-height", "55vh");
}

$("#btnParams").click(function () { //Params Toggle

    if ($('#paramsTray').is(':visible')) {
        
        fnHideParamsTray();
    }
    else {
        fnShowParamsTray();
    }
})

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
                    html = html + "<option value=" + value.main_client_guid + " data-main_client_id=" + value.main_client_id + ">" + value.main_client_name + "</option>";
                });

                //element.dataset.mainClientHash

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

function ViewCapitalGainReport() {

    let main_client_id = $('#ddlClientList :selected').val();
    let from_date = $('#inpStartDate').val();
    let to_date = $('#inpEndDate').val();
    let LoginId = $("#LoggedInUserHash").val()
    let FamilyId = $("#ddlFamilyList :selected").val()
    

    let dsp_start_date = from_date.split("-").reverse().join("-");

    let dsp_end_date = to_date.split("-").reverse().join("-");

    $("#report_start_date").text(dsp_start_date);
    $("#report_end_date").text(dsp_end_date);

    let parameterObj = {
        "LoginId": LoginId,
        "FamilyId" : FamilyId,
        "MainClientHash": main_client_id,
        "from_date": from_date,
        "to_date": to_date
    }

    let PnLurl = "/ClientPortal/Reports/psp_dsp_capital_gain_report";
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
                $.each(groups, function (index, value1) {
                    CatTotalObj = value1.length - 1;

                    if (value1[0].div_category != "GrandTotal") {
                        html = html + "<tr class='SubTotalRow'><td><div class='row'>" +
                            "<div class='col-2'><button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#PnL_acc_" + value1[0].div_category + "'" +
                            "aria-expanded='false' aria-controls='PnL_acc_" + value1[0].div_category + "'>" +
                            "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                            "</button>" + index + "</div>" +
                            "<div class='col-2'><p class='ReportTableFont paraTextAlignLeft'>Total: " + numberWithCommas(value1[CatTotalObj].total_pnl_gf.toFixed(2)) + "</p></div>" +
                            "<div class='col-2'><p class='ReportTableFont paraTextAlignLeft'>Intraday: " + numberWithCommas(value1[CatTotalObj].int_pnl.toFixed(2)) + "</p></div>" +
                            "<div class='col-3'><p class='ReportTableFont paraTextAlignLeft'>Short Term:  " + numberWithCommas(value1[CatTotalObj].st_pnl.toFixed(2)) + "</p></div>" +
                            "<div class='col-3'><p class='ReportTableFont paraTextAlignLeft'>Long Term: " + numberWithCommas(value1[CatTotalObj].lt_pnl_gf.toFixed(2)) + "</p></div>" +
                            "</td></td></tr>"
                        html = html + "<tr><td>" +
                            "<div class='collapse' id='PnL_acc_" + value1[0].div_category + "' data-parent='#div_PnL'>" +
                            "<div class='global_report_accords'>" +
                            "<table id='PnL_Table_" + value1[0].div_category + "' class='table table-hover w-100'>" +
                            //"<colgroup><col style='width:15%;'><col style='width:65%;'><col style='width:20%;'></colgroup>" +
                            "<thead class='table_sticky_heads'><tr><th rowspan='2'>Quantity</th><th colspan='3'>Buy</th><th colspan='3'>Sell</th>" +
                            "<th rowspan='2'>Fair Price</th><th rowspan='2'>Acquisition Cost</th>";
                        html = html + "<th colspan='4'>Gain/Loss With Grandfathering</tr>" +
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
                                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + numberWithCommas(value.buy_trn_qty.toFixed(2)) + "</p></td>" +

                                    //if (value.remarks_in == "") {
                                    //html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.buy_trn_date) + "</p></td>";
                                    //}
                                    //else {
                                    //    html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.buy_trn_date) +
                                    //        "<br> <span style='font-size: 10px; color:orangered;'>" + value.remarks_in + "</span>" +
                                    //        "</p></td>";
                                    //}
                                    //html = html +
                                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.buy_trn_date) + "</p></td>" +


                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_trn_rate.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_amount.toFixed(2)) + "</p></td>" +


                                    //if (value.remarks_out == "") {
                                    //html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.sell_trn_date) + "</p></td>";
                                    //}
                                    //else {
                                    //    html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.sell_trn_date) +
                                    //        "<br> <span style='font-size: 10px; color:orangered;'>" + value.remarks_out + "</span>" +
                                    //        "</p></td>";
                                    //}
                                    //html = html +
                                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(value.sell_trn_date) + "</p></td>" +



                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.sell_trn_rate.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.sell_amount.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + numberWithCommas(value.fair_rate.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + numberWithCommas(value.acquisition_rate.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.lt_pnl_gf.toFixed(2)) + "</p></td>" +
                                    "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_pnl_gf.toFixed(2)) + "</p></td>" +
                                    "</tr>";
                            }
                            else if (value.display_order == "2") {
                                html = html + "<tr class='SubTotalRow'>" +
                                "<td colspan='3'>Sub Total " + value.scrip_name + "</td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.buy_amount.toFixed(2)) + "</p></td>" +
                                "<td colspan='2'></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.sell_amount.toFixed(2)) + "</p></td>" +
                                "<td colspan='2'></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.int_pnl.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.st_pnl.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.lt_pnl_gf.toFixed(2)) + "</p></td>" +
                                "<td><p class='ReportTableFont paraTextAlignRight'>" + numberWithCommas(value.total_pnl_gf.toFixed(2)) + "</p></td>" +
                                "</tr>";
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
                            "Grand Total | <span>Total: " + numberWithCommas(value1[0].total_pnl_gf.toFixed(2)) + " | " +
                            "Intraday Total: " + numberWithCommas(value1[0].int_pnl.toFixed(2)) + " | " +
                            "Short Term Total: " + numberWithCommas(value1[0].st_pnl.toFixed(2)) + " | " +
                            "Long Term Total: " +  numberWithCommas(value1[0].lt_pnl_gf.toFixed(2))  + "</span>" +
                            "</td></tr>"
                    }
                });
                $("#mainTable_PnL").append(html);

                $("#btnPdf").show();

                $("#clientName").text($('#ddlClientList :selected').text());
                $("#timePeriod").text(" for period " + dsp_start_date + " to " + dsp_end_date);

                

                $("#main_div").show();
            }
            else {
                $("#mainTable_PnL").html("");

                html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                    "No data found." +
                    "</p></td></tr>"

                $("#mainTable_PnL").append(html);

                $("#main_div").show();
                
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
    

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

$("#btnPdf").click(function () {

    let strPara, strDescrip;

    const main_client_id_global = $('#ddlClientList :selected').data().main_client_id;
    let LoginId = $("#LoggedInUID").val()
    let from_date = $('#inpStartDate').val();
    let to_date = $('#inpEndDate').val();
    let dsp_from_date = from_date.split("-").reverse().join("-");
    let dsp_to_date = to_date.split("-").reverse().join("-");


    strPara = LoginId + "~" + main_client_id_global + "~" + from_date + "~" + to_date + "~N~N~Y~N~N~Y~N~N";
    strDescrip = "Captial Gain Normal format for " + $('#ddlClientList :selected').text() + " for period " + dsp_from_date + " to " + dsp_to_date;

    PDFandExcelExport("#btnPdf", "24", LoginId, "0", "X", "PDF", strPara, strDescrip);

})

$("#btnGlobalExportExcel").click(function () {
    GenerateExportDetails("#btnGlobalExportExcel", "X", "EXCEL")
})
//Exports

$(".dropDownExcelOpts").click(function (e) {

    let export_format, report_id, export_type, strPara, strDescrip;

    let evtBtn = e.target.id;

    const main_client_id_global = $('#ddlClientList :selected').data().main_client_id;

    let LoginId = $("#LoggedInUID").val()
    let LoginhashId = $("#LoggedInUserHash").val();
    let FamilyId = $("#ddlFamilyList :selected").val();
    let main_client_id = $('#ddlClientList :selected').val();
    let from_date = $('#inpStartDate').val();
    let to_date = $('#inpEndDate').val();
    let dsp_from_date = from_date.split("-").reverse().join("-");
    let dsp_to_date = to_date.split("-").reverse().join("-");

    export_type = "X"

    switch (evtBtn) {
        case "ExcelNormalFormat":
            report_id = "24";
            export_format = "EXCEL";
            
            break;
        case "ExcelItrFormat":
            report_id = "88";
            export_format = "EXCEL";
            
            break;
        default:
            alert("Invalid Input");
    }

    if (report_id == "88") {
        strPara = LoginhashId + "~[AccessToken]~" + FamilyId + "~" + main_client_id + "~" + from_date + "~" + to_date;
        strDescrip = "Captial Gain ITR format for " + $('#ddlClientList :selected').text() + " for period " + dsp_from_date + " to " + dsp_to_date;
    }
    else if (report_id == "24") {
        strPara = LoginId + "~" + main_client_id_global + "~" + from_date + "~" + to_date + "~N~N~Y~N~N~Y~N~N";
        strDescrip = "Captial Gain Normal format for " + $('#ddlClientList :selected').text() + " for period " + dsp_from_date + " to " + dsp_to_date;
    }

    PDFandExcelExport(evtBtn, report_id, LoginId, "0", export_type, export_format, strPara, strDescrip);

})

$(".dropDownEmailOpts").click(function (e) {

    let export_format, report_id, export_type, strPara;

    let evtBtn = e.target.id;

    const main_client_id_global = $('#ddlClientList :selected').data().main_client_id;
    
    let LoginId = $("#LoggedInUID").val()
    let LoginhashId = $("#LoggedInUserHash").val();
    let FamilyId = $("#ddlFamilyList :selected").val();
    let main_client_id = $('#ddlClientList :selected').val();
    let from_date = $('#inpStartDate').val();
    let to_date = $('#inpEndDate').val();
    let dsp_from_date = from_date.split("-").reverse().join("-");
    let dsp_to_date = to_date.split("-").reverse().join("-");

    export_type = "E"

    switch (evtBtn) {
        case "EmailNorForPdf":
            report_id = "24";
            export_format = "pdf";

            break;
        case "EmailNorForXls":
            report_id = "88";
            export_format = "pdf";

            break;
        case "EmailItrForXls":

            report_id = "88";
            export_format = "EXCEL";
            
            break;            

        case "EmailNorForPdfCli":
            report_id = "24";
            export_format = "PDF";
            
            break;
        case "EmailNorForXlsCli":
            report_id = "24";
            export_format = "EXCEL";
            
            break;
        case "EmailItrForXlsCli":
            report_id = "88";
            export_format = "EXCEL";
            
            break;
        default:
            alert("Invalid Input");
    }

    if (report_id == "88") {
        strPara = LoginhashId + "~[AccessToken]~" + FamilyId + "~" + main_client_id + "~" + from_date + "~" + to_date;
        strDescrip = "Captial Gain ITR format for " + $('#ddlClientList :selected').text() + " for period " + dsp_from_date + " to " + dsp_to_date;
    }
    else if (report_id == "24") {
        strPara = LoginId + "~" + main_client_id_global + "~" + from_date + "~" + to_date + "~N~N~Y~N~N~Y~N~N";
        strDescrip = "Captial Gain Normal format for " + $('#ddlClientList :selected').text() + " for period " + dsp_from_date + " to " + dsp_to_date;
    }
    PDFandExcelExport(evtBtn, report_id, LoginId, "0", export_type, export_format, strPara, strDescrip);
})


