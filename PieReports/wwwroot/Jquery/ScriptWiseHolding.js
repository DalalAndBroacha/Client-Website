$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    $('#ddlRepoFormat').select2({
        minimumResultsForSearch: -1
    });
    $('#ddlAsset').select2({
        minimumResultsForSearch: -1
    });

    FetchScriptList();
    //$('#ddlAsset').multiselect({
    //    buttonWidth: '120px'
    //});
    //$('#ddlRepoFormat').multiselect({
    //    buttonWidth: '120px'
    //});

});

$("#ddlAsset").change(function () {
    $("#mainReport").hide();
    FetchScriptList();
});

$("#ddlScript").change(function () {
    $("#mainReport").hide();
});

$("#ddlRepoFormat").change(function () {
    $("#mainReport").hide();
});

function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}

function FetchBranchList() {

    var formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "cat_type": "2"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_branch_list";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlBranch").append("");
            var html = "";
            $.each(data, function (index, value) {
                if (index == "0") {
                    html = html + "<option value=" + value.id + " selected>" + value.name + "</option>";
                }
                else {
                    html = html + "<option value=" + value.id + ">" + value.name + "</option>";
                }
            });
            $("#ddlBranch").append(html);
            //$('#ddlBranch').select2();

            $('#ddlBranch').multiselect({
                buttonWidth: '200px',
                maxHeight: 200,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true
            });

            FetchRMList();

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchRMList() {

    var formdata = {
        "Branch": $('#ddlBranch :selected').val()
    }
    var posturl = "/ClientPortal/Pie/RMList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlRM").html("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.rm_id + ">" + value.rm_login_name + "</option>";
            });
            $("#ddlRM").append(html);
            //$('#ddlRM').select2();

            $('#ddlRM').multiselect({
                buttonWidth: '200px',
                maxHeight: 200,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                includeSelectAllOption: true
            });

            $('#ddlRM').multiselect('rebuild');

            FetchScriptList();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchScriptList() {

    //var BranchValues = $("#ddlBranch").val().join(",");

    let formdata = {
        "rm": $("#LoggedInUID").val(),
        "Asset" : $('#ddlAsset :selected').val()
    }
    let posturl = "/ClientPortal/Pie/psp_rpt_scripwiseholding_scrip";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlScript").html("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.scrip_code + ">" + value.scriP_NAME + "</option>";
            });
            $("#ddlScript").append(html);

            $('#ddlScript').select2();

            //$('#ddlScript').multiselect({
            //    buttonWidth: '200px',
            //    maxHeight: 200,
            //    enableFiltering: true,
            //    enableCaseInsensitiveFiltering: true,
            //    includeFilterClearBtn: false,
            //});

            //$('#ddlScript').multiselect('rebuild');

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetMISScriptDetails() {
    $("#norecordsScriptwiseDetails").hide();
    $('#divScriptwiseDetailsCondensed').hide();
    $('#divScriptwiseDetailsDetailed').hide();

    $('#tableScriptwiseDetailsCondensed').DataTable().destroy();
    $('#tableScriptwiseDetailsDetailed').DataTable().destroy();

    //var BranchValues = $("#ddlBranch").val().join(",");
    
    let script_id = $('#ddlScript :selected').val();

    let formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "Asset": $('#ddlAsset :selected').val(),
        "Script": script_id
    }
    let posturl = "/ClientPortal/Reports/psp_rpt_scripwise_client_holding_cp"; //psp_rpt_scripwise_client_holding_cp
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            var report_format = $('#ddlRepoFormat :selected').val();
            $("#norecordsScriptwiseDetails").html("");
            $("#tbodyScriptwiseDetailsCondensed").html("");
            $("#tbodyScriptwiseDetailsDetailed").html("");
            
            if (data.length > 0) {

                $("#norecordsScriptwiseDetails").hide();

                //$("#inpPrintBranchId").val(BranchValues);
                //$("#inpPrintRMId").val(RMValues);

                //$("#inpPrintBranchName").val($("#ddlBranch").text().join(","));
                //$("#inpPrintRMName").val($("#ddlRM").text().join(","));

                var htmlCondensed = "";
                var htmlDetailed = "";
                var srtQty, lngQty, DpQty, HoldinCost, CurrVal, TotalQty;
                srtQty = lngQty = DpQty = HoldinCost = CurrVal = TotalQty = 0;

                if (report_format == 1) { //Condensed
                    for (var i = 0; i < data.length; i++) {

                        srtQty += data[i].st_qty;
                        lngQty += data[i].lt_qty;
                        DpQty += data[i].holding_qty;
                        HoldinCost += data[i].hld_cost;
                        CurrVal += data[i].current_value;

                        htmlCondensed = htmlCondensed + "<tr>" +
                            "<td class='paraTextAlignLeft'>" + data[i].main_client_name + "</td>" +
                            "<td class='paraTextAlignCenter' style='text-decoration:underline; cursor:pointer;' onclick=\"drillDown(\'" + data[i].client_id + "','" + script_id + "','" + data[i].main_client_name + "')\">" + data[i].account_code + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_qty) + ">" + numberWithCommas(data[i].holding_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].st_qty) + ">" + numberWithCommas(data[i].st_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].lt_qty) + ">" + numberWithCommas(data[i].lt_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_rate) + ">" + numberWithCommas(data[i].holding_rate.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].hld_cost) + ">" + numberWithCommas(data[i].hld_cost.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_mkt_rate) + ">" + numberWithCommas(data[i].holding_mkt_rate.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].current_value) + ">" + numberWithCommas(data[i].current_value.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_qty) + ">" + numberWithCommas(data[i].holding_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].latest_buy) + ">" + numberWithCommas(data[i].latest_buy.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].latest_sell) + ">" + numberWithCommas(data[i].latest_sell.toFixed(2)) + "</td>";
                        if (data[i].remarks == null) {
                            htmlCondensed = htmlCondensed + "<td class='paraTextAlignCenter'> - </td>" +
                                "<td class='paraTextAlignLeft'>" + data[i].branch_name + "</td>" +
                                "<td class='paraTextAlignLeft'>" + data[i].rm_name + "</td>" +
                                "</tr>";
                        }
                        else {
                            htmlCondensed = htmlCondensed + "<td class='paraTextAlignLeft'>" + data[i].remarks + "</td>" +
                                "<td class='paraTextAlignLeft'>" + data[i].branch_name + "</td>" +
                                "<td class='paraTextAlignLeft'>" + data[i].rm_name + "</td>" +
                                "</tr>";
                        }
                    }

                    TotalQty = srtQty + lngQty;
                     
                    $("#TotalSrt").text(numberWithCommas(srtQty.toFixed(2)));
                    $("#TotalLT").text(numberWithCommas(lngQty.toFixed(2)));
                    $("#TotalDP").text(numberWithCommas(DpQty.toFixed(2)));
                    $("#TotalQty").text(numberWithCommas(TotalQty.toFixed(2)));
                    $("#TotalHoldin").text(numberWithCommas(HoldinCost.toFixed(2)));
                    $("#TotalCurVal").text(numberWithCommas(CurrVal.toFixed(2)));

                    $("#tbodyScriptwiseDetailsCondensed").append(htmlCondensed);
                    $('#tableScriptwiseDetailsCondensed').DataTable({
                        //"destroy": true,
                        "scrollX": true,
                        "pagingType": "full_numbers",

                        "initComplete": function (settings, json) {
                            completeajaxrequestwaitIn();

                            $("#btnEmailSWCW_HoldingReport").show();
                            $("#btnPDFSWCW_HoldingReport").show();
                            $("#btnExportSWCW_HoldingReport").show();

                            $("#mainReport").show();
                            $("#report_header_and_buttons").show();
                            $('#dataDiv').show();
                            $('#divScriptwiseDetailsCondensed').show();
                            $('#tableScriptwiseDetailsCondensed').DataTable().columns.adjust().draw();
                        }
                    });
                    
                }
                else if (report_format == 2) { //Detailed

                    for (var i = 0; i < data.length; i++) {

                        srtQty += data[i].st_qty;
                        lngQty += data[i].lt_qty;
                        DpQty += data[i].holding_qty;
                        HoldinCost += data[i].hld_cost;
                        CurrVal += data[i].current_value;

                        htmlDetailed = htmlDetailed + "<tr>" +
                            "<td class='paraTextAlignLeft'>" + data[i].main_client_name + "</td>" +
                            "<td class='paraTextAlignCenter' style='text-decoration:underline; cursor:pointer;' onclick=\"drillDown(\'" + data[i].client_id + "','" + script_id + "','" + data[i].main_client_name + "')\">" + data[i].account_code + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_qty) + ">" + numberWithCommas(data[i].holding_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].st_qty) + ">" + numberWithCommas(data[i].st_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].lt_qty) + ">" + numberWithCommas(data[i].lt_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_rate) + ">" + numberWithCommas(data[i].holding_rate.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].hld_cost) + ">" + numberWithCommas(data[i].hld_cost.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_mkt_rate) + ">" + numberWithCommas(data[i].holding_mkt_rate.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].current_value) + ">" + numberWithCommas(data[i].current_value.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].dividend.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].holding_qty) + ">" + numberWithCommas(data[i].holding_qty.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].latest_buy) + ">" + numberWithCommas(data[i].latest_buy.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].latest_sell) + ">" + numberWithCommas(data[i].latest_sell.toFixed(2)) + "</td>"
                            if (data[i].remarks == null) {
                                htmlDetailed = htmlDetailed + "<td class='paraTextAlignCenter'> - </td>";
                            }
                            else {
                                htmlDetailed = htmlDetailed + "<td class='paraTextAlignLeft'>" + data[i].remarks + "</td>";
                        }
                        htmlDetailed = htmlDetailed +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].srt_unreal_profit) + ">" + numberWithCommas(data[i].srt_unreal_profit.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].long_unreal_profit) + ">" + numberWithCommas(data[i].long_unreal_profit.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].return_xirr) + ">" + numberWithCommas(data[i].return_xirr.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'" + isNegativeValue(data[i].return_cagr) + ">" + numberWithCommas(data[i].return_cagr.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignLeft'>" + data[i].branch_name + "</td>" +
                            "<td class='paraTextAlignLeft'>" + data[i].rm_name + "</td>" +
                            "</tr>";
                    }

                    TotalQty = srtQty + lngQty;

                    $("#TotalSrt").text(numberWithCommas(srtQty.toFixed(2)));
                    $("#TotalLT").text(numberWithCommas(lngQty.toFixed(2)));
                    $("#TotalDP").text(numberWithCommas(DpQty.toFixed(2)));
                    $("#TotalQty").text(numberWithCommas(TotalQty.toFixed(2)));
                    $("#TotalHoldin").text(numberWithCommas(HoldinCost.toFixed(2)));
                    $("#TotalCurVal").text(numberWithCommas(CurrVal.toFixed(2)));

                    $("#tbodyScriptwiseDetailsDetailed").append(htmlDetailed);
                    $("#tableScriptwiseDetailsDetailed").DataTable({
                        "initComplete": function (settings, json) {
                            completeajaxrequestwaitIn();

                            $("#btnEmailSWCW_HoldingReport").show();
                            $("#btnPDFSWCW_HoldingReport").show();
                            $("#btnExportSWCW_HoldingReport").show();


                            $("#mainReport").show();
                            $("#report_header_and_buttons").show();
                            $('#dataDiv').show();
                            $('#divScriptwiseDetailsDetailed').show();
                            $('#tableScriptwiseDetailsDetailed').DataTable().columns.adjust().draw();
                        },
                        //"destroy": true,
                        "scrollX": true,
                        "pagingType": "full_numbers"
                    });
                }
            }
            else {
                $("#mainReport").show();
                $("#norecordsScriptwiseDetails").append("No data found.");
                $("#norecordsScriptwiseDetails").show();
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function drillDown(cli_code, scrip_id, cli_name) {

    var formdata = {
        "client_Code": cli_code,
        "script_code": scrip_id
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


                $('#tradeCliName').text(cli_name);
                $('#tradeScripName').text($('#ddlScript :selected').text());
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


//**** Button Clicks ****
$("#btnEmailSWCW_HoldingReport").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();
    var asset = $('#ddlAsset :selected').val();
    var script = $('#ddlScript :selected').val();
    var report_format = $('#ddlRepoFormat :selected').val();


    var strPara = LoginId + "~" + asset + "~" + script + "~" + report_format;

    var strDescrip = "Script Wise Client Wise Holding Report for Script/Scheme- " + $('#ddlScript :selected').text() + " (" +
        $('#ddlRepoFormat :selected').text() + ")"

    PDFandExcelExport("#btnEmailSWCW_HoldingReport", "19", LoginId, famId, "E", "PDF", strPara, strDescrip);

})

$("#btnPDFSWCW_HoldingReport").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();
    var asset = $('#ddlAsset :selected').val();
    var script = $('#ddlScript :selected').val();
    var report_format = $('#ddlRepoFormat :selected').val();

    var strPara = LoginId + "~" + asset + "~" + script + "~" + report_format;

    var strDescrip = "Script Wise Client Wise Holding Report for Script/Scheme- " + $('#ddlScript :selected').text() + " (" +
        $('#ddlRepoFormat :selected').text() + ")"

    PDFandExcelExport("#btnPDFSWCW_HoldingReport", "19", LoginId, famId, "X", "PDF", strPara, strDescrip);

})

$("#btnExportSWCW_HoldingReport").click(function () {

    var famId = "0"
    var LoginId = $("#LoggedInUID").val()
    var asset = $('#ddlAsset :selected').val()
    var script = $('#ddlScript :selected').val();
    var report_format = $('#ddlRepoFormat :selected').val();

    var strPara = LoginId + "~" + asset + "~" + script + "~" + report_format;

    var strDescrip = "Script Wise Client Wise Holding Report for Script/Scheme- " + $('#ddlScript :selected').text() + " (" +
        $('#ddlRepoFormat :selected').text() + ")"

    PDFandExcelExport("#btnExportSWCW_HoldingReport", "19", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//**** Button Clicks ****

