$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    setPickerDates();

});

function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}

function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}


$("#inpStartDate").change(function () {

    var date = $(this).val();
    var Tdaydate = (new Date()).toISOString().split('T')[0];

    $("#inpEndDate").attr("min", date);

    var maxDate = formatDateold(addDays(date, 91));

    

    if (maxDate > Tdaydate) {
        $("#inpEndDate").val(Tdaydate);
        $("#inpEndDate").attr("max", Tdaydate);
    }
    else {
        $("#inpEndDate").val(maxDate);
        $("#inpEndDate").attr("max", maxDate);
    }

    $("#inpEndDate").prop("disabled", false);

});

function setPickerDates() {

    var Tdaydate = (new Date()).toISOString().split('T')[0];

    $("#inpStartDate").attr("max", Tdaydate);
    $("#inpStartDate").attr("min", "2022-04-01");

}


function ViewLog() {

    startajaxrequestGlobal();

    var from_date = $('#inpStartDate').val();
    var to_date = $('#inpEndDate').val();

    var formdata = {
        "start_date": from_date,
        "end_date": to_date
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_periodic_kyc_log";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),

        success: function (data) {

            if (data.length > 0) {
                
                $("#btnLogExcel").show();

                $("#KYC_Log_Table").DataTable().destroy();
                $("#tbodyKYC_Log").html("");
                var html = "";

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td>" + value.request_id + "</td>" +
                        "<td>" + value.client_name + "</td>" +
                        "<td>" + value.account_code + "</td>" +
                        "<td>" + value.income_update + "</td>" +
                        "<td>" + value.new_income_value + "</td>" +
                        "<td>" + value.email_update + "</td>" +
                        "<td>" + value.new_email_value + "</td>" +
                        "<td>" + value.mobile_update + "</td>" +
                        "<td>" + value.new_mobile_value + "</td>" +
                        "<td data-sort='" + formatDateTimeForSort(value.request_date) + "'>" + formatDateTime(value.request_date) + "</td>" +
                        "<td>" + (value.request_completion_date == "1900-01-01T00:00:00" ? " - " : formatDateTime(value.request_completion_date)) + "</td>" +
                        "<td>" + value.remarks + "</td>" +
                        "</tr>"
                });

                $("#tbodyKYC_Log").append(html);

                $('#KYC_Log_Table').DataTable({
                    "initComplete": function (settings, json) {
                        $("#noRecordsFound").hide()
                        $("#main_div").show()
                        completeajaxrequestGlobal()
                        $('#KYC_Log_Table').DataTable().columns.adjust().draw();
                    },
                    "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
                        //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": true },
                        { "bSortable": true },
                        { "bSortable": false },
                    ],
                    "order": [],
                    "scrollX": true
                });

            }
            else {

                alert("No Data");

                $("#KYC_Log_Table").DataTable().destroy();
                $("#main_div").hide()
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}



//Excel Btn

$("#btnLogExcel").click(function () {

    var LoginId = $("#LoggedInUID").val()

    var start_date = $('#inpStartDate').val();
    var end_date = $('#inpEndDate').val();

    var dsp_start_date = start_date.split("-").reverse().join("-");
    var dsp_end_date = end_date.split("-").reverse().join("-");

    var strPara = start_date + "~" + end_date;

    var strDescrip = "KYC Log for period " + dsp_start_date + " - " + dsp_end_date;

    PDFandExcelExport("#btnLogExcel", "34", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})

//Excel Btn