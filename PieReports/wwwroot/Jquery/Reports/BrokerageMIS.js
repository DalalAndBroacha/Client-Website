$(document).ready(function () {
    $("[data-widget='pushmenu']").PushMenu("collapse");

    HideFinYearList();
    HideFamilyList();
    getSetTday();
    
});

function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}


function getSetTday() {
    var tdate = (new Date()).toISOString().split('T')[0];
    $("#inpAsOnDate").val(tdate);
    $("#inpAsOnDate").attr("max", tdate);
}

function ViewMIS() {
    startajaxrequestGlobal();
    ExchangeWise_Brokerage();
    BranchWise_Brokerage();   
}


function ExchangeWise_Brokerage() {
    var formdata = {
        "as_on_date": $('#inpAsOnDate').val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_exchangewise_brokerage";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {

                var html = "";
                $("#tbodyExchange").html("");

                for (var i = 0; i < data.length; i++) {

                    if (data[i].exchange_name != "Total") {
                        html = html + "<tr>" +
                            "<td class='paraTextAlignCenter'>" + data[i].exchange_name + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ftd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ftd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].mtd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].mtd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ytd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ytd_brokerage.toFixed(2)) + "</td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr>" +
                            "<td class='paraTextAlignCenter font-weight-bold'>" + data[i].exchange_name + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ftd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ftd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].mtd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].mtd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ytd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ytd_brokerage.toFixed(2)) + "</td>" +
                            "</tr>"
                    }
                }
                
                $("#tbodyExchange").append(html);
            }
            else {
                console.log("No");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

function BranchWise_Brokerage() {

    var formdata = {
        "as_on_date": $('#inpAsOnDate').val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_branchwise_brokerage";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        complete: function (xhr) {
            completeajaxrequestGlobal();
            $("#report_date_Brokerage_MIS").text(formatDate($('#inpAsOnDate').val()));
            $("#div_data").show();
        },
        success: function (data) {
            if (data.length > 0) {
                var html = "";
                $("#tbodyBranch").html("");

                for (var i = 0; i < data.length; i++) {

                    if (data[i].branch_Name != "Total") {
                        html = html + "<tr>" +
                            "<td class='paraTextAlignCenter'>" + data[i].branch_Name + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ftd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ftd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].mtd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].mtd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ytd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].ytd_brokerage.toFixed(2)) + "</td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr>" +
                            "<td class='paraTextAlignCenter font-weight-bold'>" + data[i].branch_Name + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ftd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ftd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].mtd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].mtd_brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ytd_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight font-weight-bold'>" + numberWithCommas(data[i].ytd_brokerage.toFixed(2)) + "</td>" +
                            "</tr>"
                    }
                }

                $("#tbodyBranch").append(html);
            }
            else {
                console.log("No");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}


//Exports
$("#btnEMailBrokerage_MIS").click(function () {

    var LoginId = $("#LoggedInUID").val();
    var as_on_date = $('#inpAsOnDate').val();

    var strPara = LoginId + "~" + as_on_date;

    var strDescrip = "Brokerage MIS as on " + formatDate($('#inpAsOnDate').val());

    PDFandExcelExport("#btnEMailBrokerage_MIS", "31", LoginId, "0", "E", "PDF", strPara, strDescrip);

})

$("#btnBrokerage_MISExportPdf").click(function () {

    var LoginId = $("#LoggedInUID").val();
    var as_on_date = $('#inpAsOnDate').val();

    var strPara = LoginId + "~" + as_on_date;
    var strDescrip = "Brokerage MIS as on " + formatDate($('#inpAsOnDate').val());

    PDFandExcelExport("#btnBrokerage_MISExportPdf", "31", LoginId, "0", "X", "PDF", strPara, strDescrip);

})

$("#btnBrokerage_MISExportExcel").click(function () {

    var LoginId = $("#LoggedInUID").val();
    var as_on_date = $('#inpAsOnDate').val();

    var strPara = LoginId + "~" + as_on_date;

    var strDescrip = "Brokerage MIS as on " + formatDate($('#inpAsOnDate').val());

    PDFandExcelExport("#btnBrokerage_MISExportExcel", "31", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})
//Exports

