$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");
    HideFinYearList();
    HideFamilyList();


    var Tdaydate = (new Date()).toISOString().split('T')[0];
    $("#interactDate").attr("max", Tdaydate);

    FetchSurveyDump();

});

function startajaxrequestSurveyDump() {
    $("#waitIn").css("display", "block");
}
function endajaxrequestSurveyDump() {
    $("#waitIn").css("display", "none");
}


function FetchSurveyDump() {
    var formdata = {
        "survey_id": "1",
        "login_id": $("#LoggedInUID").val(),
        "flag": "2"
    }
    var posturl = "/ClientPortal/Pie/psp_dsp_survey_respnse_dump";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestSurveyDump(); //Ends in Init Complete of DataTable
        },
        success: function (data) {

            if (data.length > 0) {
                var html = ""
                $("#tableSurveyDataDump").html("");

                html = html + "<thead>" +
                    "<tr class='table-active'>" +
                    "<th>Branch</th>" +
                    "<th>Family</th>" +
                    "<th>RM</th>" +
                    "<th>Filled By</th>" +
                    "<th>Filled On</th>" +
                    "<th>Regular communication of the Investment opportunities received from D & B for Equity</th>" +
                    "<th>Regular communication of the Investment opportunities received from D & B for Mutual Fund</th>" +
                    "<th>Regular communication of the Investment opportunities received from D & B for Fixed Income product</th>" +
                    "<th>Regular communication of the Investment opportunities received from D & B for PMS & AIF</th>" +
                    "<th>Response to the Enquiries</th>" +
                    "<th>Frequency of Communication as per Expectations</th>" +
                    "<th>Quality of service provided</th>" +
                    "<th>Response to your request/suggestions</th>" +
                    "<th>Response to the problems/complaints</th>" +
                    "<th>Service Delivery performance</th>" +
                    "<th>Are you aware of the D&B Web application?</th>" +
                    "<th>Are you aware of the D&B Mobile application?</th>" +
                    "<th>Any Suggestions you would like to share with us.</th>" +
                    "<th>Would you like to refer D&B services to others.</th>" +
                    "<th>Referal 1 - Name</th>" +
                    "<th>Referal 1 - Contact</th>" +
                    "<th>Referal 1 - E-mail</th>" +
                    "<th>Referal 2 - Name</th>" +
                    "<th>Referal 2 - Contact</th>" +
                    "<th>Referal 2 - E-mail</th>" +
                    "</thead><tbody>"

                for (var i = 0; i < data.length; i++) {

                    if (data[i].request_status == 'C') {
                        html = html + "<tr class='table-success'>";
                    }
                    else {
                        html = html + "<tr>";
                    } 
                    html = html +
                    "<td><p class='ReportTableFont'>" + data[i].branch_name + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].family_name + "<br>" +
                        "<a style='color:blue; font-size:11px;' href='#' onclick=\"FetchRemarksData(\'1',\'" + data[i].family_id + "' ,\'" + data[i].family_name + "' ,\'" + data[i].filled_on + "\' ,\'" + data[i].request_status + "\')\">Remarks</a></p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].rm_name + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].filled_by + "</p></td>" +
                    "<td  data-sort='" + formatDate_yyyymmdd(data[i].filled_on) + "'><p class='ReportTableFont'>" + formatDate(data[i].filled_on) + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q1 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q2 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q3 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q4 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q5 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q6 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q7 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q8 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q9 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q10 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q11 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q12 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q13 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q14 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q15 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q16 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q17 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q18 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q19 + "</p></td>" +
                    "<td><p class='ReportTableFont'>" + data[i].q20 + "</p></td>" +
                    "</tr>"

                }

                html = html + "</tbody>"

                $("#tableSurveyDataDump").append(html);

                $('#tableSurveyDataDump').DataTable({
                    //"destroy": true,
                    "scrollX": true,
                    "pagingType": "full_numbers",
                    "order": [[4, 'desc']],
                    "destroy": true,
                    "columnDefs": [
                        { "width": "150px", targets: [0, 2, 3], 'searchable': true, orderable: true },
                        { "width": "300px", targets: 1, orderable: true },
                        { "width": "75px", targets: 4, 'searchable': true, orderable: true  },
                        { "width": "900px", targets: 17, orderable: false },
                        { "width": "150px", targets: '_all', 'searchable': false, orderable: false},
                    ],

                    "initComplete": function (settings, json) {
                            

                        $("#report_header_and_buttons").show();
                        $('#dataDiv').show();
                        $('#tableSurveyDataDump').DataTable().columns.adjust().draw();
                        endajaxrequestSurveyDump();
                    }
                });

                   
            }
            else {
                console.log("No Data");
                $("#report_header_and_buttons").hide();
                endajaxrequestSurveyDump();
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchRemarksData(survey_id, family_id, family_name, filled_on, req_stat) {

    $("#txtHidSurveyId").val(survey_id)
    $("#txtHidFamId").val(family_id)
    $("#txtHidFilledOn").val(formatDateold(filled_on))

    var formdata = {
        "login_id": $("#LoggedInUID").val(),
        "rec_id": "0",
        "survey_id": survey_id,
        "family_id": family_id,
        "survey_date": formatDateold(filled_on),
        "action": "D",
        "interact_date": '1999-01-01',
        "remarks": ""
    }
    var posturl = "/ClientPortal/Pie/psp_amd_survey_interaction";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                $("#FamTitle").text(family_name);

                if (data.remarks != null && data.remarks != "") {
                    $("#txtRemarks").val(data.remarks);
                    $("#txtRemarks").prop("readonly", true);
                }
                else {
                    $("#txtRemarks").prop("readonly", false);
                }

                if (data.interact_date != null && data.interact_date != "") {
                    $("#interactDate").val(formatDateold(data.interact_date));
                    $("#interactDate").prop("readonly", true);
                }
                else {
                    $("#interactDate").prop("readonly", false);
                }

                if (req_stat == 'C') {
                    $("#btnSave").hide();
                }
                else {
                    $("#btnSave").show();
                }
                $('#remarksModal').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                $("#txtRemarks").val("");
                $("#txtRemarks").prop("readonly", false);

                $("#interactDate").val("");
                $("#interactDate").prop("readonly", false);

                $("#btnSave").show();
                $('#remarksModal').modal({ backdrop: 'static', keyboard: false });
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
   
}

function saveRemarksData() {
    if ($("#interactDate").val() == "" || $("#txtRemarks").val() == "") {
        alert("Please fill both the fields.")
    }
    else {
        
        if ($("#txtRemarks").val().length > 8) {


            var formdata = {
                "login_id": $("#LoggedInUID").val(),
                "rec_id": "0",
                "survey_id": $("#txtHidSurveyId").val(),
                "family_id": $("#txtHidFamId").val(),
                "survey_date": $("#txtHidFilledOn").val(),
                "action": "A",
                "status": "C",
                "interact_date": $("#interactDate").val(),
                "remarks": $("#txtRemarks").val()
            }
            var posturl = "/ClientPortal/Pie/psp_amd_survey_interaction";

            $.ajax({
                url: posturl,
                type: "post",
                contentType: "application/json",
                data: JSON.stringify(formdata),
                success: function (data) {
                    $('#remarksModal').modal('hide');
                    alert("Updated!!!");
                    $('#dataDiv').hide();
                    FetchSurveyDump();


                },
                error: function (xhr, err) {
                    alert(err)
                }
            })
        }
        else {
            alert("Remarks should have more than 8 characters.");
        }
    }
}



$("#btnSDumpExportPdf").click(function () {

    var LoginId = $("#LoggedInUID").val()

    var strPara = "1~" + LoginId + "~2"

    var strDescrip = "Survey Dump"

    PDFandExcelExport("#btnSDumpExportPdf", "32", LoginId, "0", "X", "PDF", strPara, strDescrip);

})

$("#btnSDumpExportExcel").click(function () {

    var LoginId = $("#LoggedInUID").val()

    var strPara = "1~" + LoginId + "~2"

    var strDescrip = "Survey Dump"

    PDFandExcelExport("#btnSDumpExportExcel", "32", LoginId, "0", "X", "EXCEL", strPara, strDescrip);

})

$("#btnEMailSDump").click(function () { //Excel Attachment in Email  

    var LoginId = $("#LoggedInUID").val()

    var strPara = "1~" + LoginId + "~2"

    var strDescrip = "Survey Dump"

    PDFandExcelExport("#btnEMailSDump", "32", LoginId, "0", "E", "EXCEL", strPara, strDescrip);

})

