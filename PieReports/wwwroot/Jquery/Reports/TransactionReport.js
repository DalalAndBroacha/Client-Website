$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    GetClientList();
   
});

$("#ddlFamilyList").change(function () {
    GetClientList();
    $("#main_div").hide();

});
$("#ddlClientList").change(function () {
    GetScripList();
    $("#main_div").hide();
});

$("#ddlScripList").change(function () {
    $("#main_div").hide();
});

function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}

function GetClientList() {
    let formdata = {
        "family_hash": $('#ddlFamilyList :selected').val(),
        "flag" : "0"
    }

    let posturl = "/ClientPortal/Reports/psp_dsp_family_equity_client_list";
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
                    html = html + "<option value=" + value.client_code + ">" + value.client_name + "</option>";
                });

                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
                $('#ddlClientList').select2();
                GetScripList();
                
            }
            else {
                $("#ddlClientList").append("<option value=0>No Data</option>");
                $("#ddlClientList").attr("disabled", true);

                $("#ddlScripList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}
function GetScripList() {

    $("#ddlScripList").attr("disabled", true);

    let formdata = {
        "client_id": $('#ddlClientList :selected').val()
    }

    let posturl = "/ClientPortal/Reports/psp_dsp_eq_client_scrip_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlScripList").html("");
            if (data.length > 0) {
                let html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.scrip_code + ">" + value.scrip_name + "</option>";
                });

                $("#ddlScripList").append(html);
                $("#ddlScripList").attr("disabled", false);

                $('#ddlScripList').select2();
            }
            else {
                $("#ddlScripList").append("<option value=0>No Data</option>");
                $("#ddlScripList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

function FetchData() {
    let formdata = {
        "client_Code": $('#ddlClientList :selected').val(),
        "script_code": $('#ddlScripList :selected').val()
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

                $("#tbodyScriptDet").append(html);
                $("#tranClientName").text($('#ddlClientList :selected').text());
                $("#tranScripName").text($('#ddlScripList :selected').text());
                
                $("#btnEmail").show();
                $("#btnPdf").show();
                $("#btnExcel").show();

                $("#main_div").show();
                completeajaxrequestwaitIn();
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

$("#btnEmail").click(function () {

    GenerateExportDetails("#btnEmail", "E", "PDF")

})

$("#btnPdf").click(function () {

    GenerateExportDetails("#btnPdf", "X", "PDF")

})

$("#btnExcel").click(function () {

    GenerateExportDetails("#btnExcel", "X", "EXCEL")

})

function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    const LoginId = $("#LoggedInUID").val()

    const Client_id = $('#ddlClientList :selected').val();
    const Scrip_id = $('#ddlScripList :selected').val();

    const Client_Name = $('#ddlClientList :selected').text();
    const Scrip_Name = $('#ddlScripList :selected').text();


    let strPara = LoginId + "~" + Client_id + "~" + Scrip_id;

    let strDescrip = "Transaction Report of " + Client_Name + " for Scrip - " + Scrip_Name;

    PDFandExcelExport(evt_btn, "91", LoginId, "0", export_type, export_format, strPara, strDescrip);

}


//psp_dsp_eq_client_scrip_list