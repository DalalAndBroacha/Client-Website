$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");

    HideFinYearList();
    FetchClientList();

    //$("#div_resend_otp1_timer").hide();
});

$("#ddlFamilyList").change(function () {

    $("#dsp_div").hide();
    FetchClientList();

});

$("#ddlClientList").change(function () {

    $("#dsp_div").hide();

});

function GoToKYCUpdate() {
    var url = "/ClientPortal/Pie/KYC_Update";
    window.location.href = url;
}

function FetchClientList() {

    var formdata = {
        "family_id": $('#ddlFamilyList :selected').val(),
        "asset_class": "1,6",
        "pan_no": "",
        "level": "1"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_accounts";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlClientList").html("");
                var html = "";
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

function ViewHistory() {

    var formdata = {
        "main_client_id": $('#ddlClientList :selected').val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_get_kyc_update_history";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                var html = "";
                $("#histroy_table").html("");

                html = html + "<thead><tr><th>Request Date</th><th>Account Code</th><th>New E-mail</th><th>New Mobile</th>" +
                    "<th>New Income</th><th>Networth</th><th>Networth Date</th><th>Remarks</th><th>Status</th></tr></thead>" +
                    "<tbody>";
                for (var i = 0; i < data.length; i++) {
                    html = html + "<tr>" +
                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(data[i].request_date) + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].account_code + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].new_email + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].new_mobile + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].new_income_range_dsp + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignCenter'>" + (data[i].new_networth == "" ? "-" : data[i].new_networth) + "</p></td>";
                    if (formatDate(data[i].new_networth_date) == "31-Dec-99") {
                        html = html + "<td><p class='ReportTableFont paraTextAlignCenter'> - </p></td>";
                    }
                    else {
                        html = html + "<td><p class='ReportTableFont paraTextAlignCenter'>" + formatDate(data[i].new_networth_date) + "</p></td>";
                    }
                    html = html + "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].remarks + "</p></td>" +
                        "<td><p class='ReportTableFont paraTextAlignLeft'>" + data[i].status + "</p></td>" +
                        "</tr>";
                }
                html = html + "</tbody>";
                $("#histroy_table").append(html);
                $("#noRecordsFoundHistory").hide();
                $("#dsp_div").show(); 
            }
            else {
                $("#histroy_table").html("");
                $("#noRecordsFoundHistory").show();
                $("#dsp_div").show();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}