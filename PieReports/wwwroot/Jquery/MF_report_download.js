$(document).ready(function () {

    HideFinYearList();
    CollapseSideMenu();
    FetchClientList();
    FetchReportList();

    let today = new Date()
    let tdate = (today).toISOString().split('T')[0];
    $(".dateInput").val(tdate)
    $(".dateInput").attr("max", tdate)
});

function startMINTajaxrequest() {
    $("#waitInMINT").css("display", "block");
}
function completeMINTajaxrequest() {
    $("#waitInMINT").css("display", "none");
}

$("#ddlFamilyList").change(function () {
    FetchClientList();
});

$("#ddlReportList").change(function () {
    let report_id = $('#ddlReportList :selected').val()
    showReports(report_id)
});

function showReports(report_id) {
    let noClients = $('#ddlClientList').prop('disabled');
    if (!noClients) {
        if (report_id == "1") {
            $("#divPortfolioSummary").show();

            $("#divPortfolioReturn").hide();
            $("#divCapitalGainDetailed").hide();
            $("#divCapitalGainSummary").hide();
        }
        else if (report_id == "2") {
            $("#divPortfolioReturn").show();

            $("#divPortfolioSummary").hide();
            $("#divCapitalGainDetailed").hide();
            $("#divCapitalGainSummary").hide();
        }
        else if (report_id == "3") {
            $("#divCapitalGainDetailed").show();

            $("#divPortfolioSummary").hide();
            $("#divPortfolioReturn").hide();
            $("#divCapitalGainSummary").hide();
        }
        else if (report_id == "4") {
            $("#divCapitalGainSummary").show();

            $("#divPortfolioSummary").hide();
            $("#divPortfolioReturn").hide();
            $("#divCapitalGainDetailed").hide();
        }
    }
    else {
        $("#divPortfolioSummary").hide();
        $("#divPortfolioReturn").hide();
        $("#divCapitalGainDetailed").hide();
        $("#divCapitalGainSummary").hide();
    }
}



function FetchReportList() {

    let formdata = {
        
    }

    const posturl = "/ClientPortal/Pie/psp_dsp_mint_mf_report_download_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlReportList").html("");
                var html = "";
                /*html = html + "<option value=0>Select Report</option>";*/
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.report_id + ">" + value.report_name + "</option>";
                });
                $("#ddlReportList").append(html);
                $("#ddlReportList").attr("disabled", false);
            }
            else {
                $("#ddlReportList").append("<option value=0>No Data</option>");
                $("#ddlReportList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchClientList() {

    let formdata = {
        "family_id": $('#ddlFamilyList :selected').val()
    }

    const posturl = "/ClientPortal/Mint/psp_dsp_mint_mf_clients";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlClientList").html("");
            let html = "";

            if (data.length > 0) {
                
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.main_client_id + ">"
                        + value.mint_clientname + " (" + value.pan_no + ")</option>";
                });

                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
            }
            else {
                $("#ddlClientList").append("<option value=0>No Clients</option>");
                $("#ddlClientList").attr("disabled", true);
            }
            $("#ddlReportList").trigger("change");
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function generateReport(formData) {

    const posturl = "/ClientPortal/MINT/generateMfReport";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formData),
        beforeSend: function (xhr) {
            startMINTajaxrequest();
        },
        success: function (data) {

            if (data != null) {
                if (data.status == "Success") {

                    let fpurl = "getDocument?FP=" + data.mfPostUrl;
                    window.open(fpurl);

                    //let fpurl = "getDocument?FP=B7D7A077-23E9-4F73-973F-DD83FCEE1AF620241003.PDF";

                    //$("#FpDwn").prop("href", fpurl);
                    //window.open("getDocument?FP=B7D7A077-23E9-4F73-973F-DD83FCEE1AF620241003.PDF");
                }
                else {
                    alert(data.message);
                }
                
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeMINTajaxrequest();
        }
    })
}



$("#btnSubmitPortfolioSummary").on("click", function (event) {
    event.preventDefault();

    var formdata = {
        "ddlClientList": $('#ddlClientList :selected').val(),
        "ddlReportList": $('#ddlReportList :selected').val(),
        "inpAsOnDatePortSummary": $("#inpAsOnDatePortSummary").val()
    }

    generateReport(formdata)

});

$("#btnSubmitPortfolioReturn").on("click", function (event) {
    event.preventDefault();

    var formdata = {
        "ddlClientList": $('#ddlClientList :selected').val(),
        "ddlReportList": $('#ddlReportList :selected').val(),
        "inpAsOnDatePortReturn": $("#inpAsOnDatePortReturn").val()
    }

    generateReport(formdata)

});

$("#btnSubmitCapitalGainDetailed").on("click", function (event) {
    event.preventDefault();

    var formdata = {
        "ddlClientList": $('#ddlClientList :selected').val(),
        "ddlReportList": $('#ddlReportList :selected').val(),
        "ddlFinYearList": $('#inpFinYearGainDetailed :selected').val()
    }

    generateReport(formdata)
});

$("#btnSubmitCapitalGainSummary").on("click", function (event) {
    event.preventDefault();

    var formdata = {
        "ddlClientList": $('#ddlClientList :selected').val(),
        "ddlReportList": $('#ddlReportList :selected').val(),
        "ddlFinYearList": $('#inpFinYearGainSummary :selected').val()
    }

    generateReport(formdata)

});



