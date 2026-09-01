$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");

    startajaxrequestSurveyResp();
    HideFinYearList();
    HideFamilyList();
    fetchSurveyList();

});

$("#ddlSurveyFamilyList").change(function () {

    EnableDisableViewDataBtn();
    $('#tbl_response_dsp').DataTable().destroy();
    $("#dsp_div").hide();

});

function startajaxrequestSurveyResp() {
    $("#waitIn").css("display", "block");
}
function endajaxrequestSurveyResp() {
    $("#waitIn").css("display", "none");
}


function EnableDisableViewDataBtn() {
    if ($('#ddlSurveyFamilyList :selected').val() == "0") {
        $("#viewData").attr("disabled", true);
    }
    else {
        $("#viewData").attr("disabled", false);
    }
}

function fetchSurveyList() {

    var posturl = "/ClientPortal/Pie/psp_dsp_survey_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {

                $("#ddlSurveyList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.survey_id + ">" + value.survey_name + "</option>";
                });

                $("#ddlSurveyList").append(html);
                $("#ddlSurveyList").attr("disabled", false);
                fetchSurveyFamilyList();
            }
            else {
                $("#ddlSurveyList").append("<option value=0>No Data</option>");
                $("#ddlSurveyList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

function fetchSurveyFamilyList() {

    var formdata = {
        "survey_id": $('#ddlSurveyList :selected').val(),
        "login_id": $("#LoggedInUID").val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_survey_family_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlSurveyFamilyList").html("");
                var html_list = "";
                var filled = 0;
                var pending = 0;

                $.each(data, function (index, value) {
                    if (value.survey_flag == "0") {
                        html_list = html_list + "<option style='color:red;' value=0>" + value.family_Name + "</option>";
                        pending += 1;
                    }
                    else {
                        html_list = html_list + "<option value=" + value.family_Id + ">" + value.family_Name + "</option>";
                        filled += 1;
                    }
                });

                if (data.length > 1) {
                    $("#totalFam").text(data.length);
                    $("#surveyFilled").text(filled);
                    $("#surveyPend").text(pending);
                    $("#surveySummary").show();
                }

                EnableDisableViewDataBtn();

                $("#ddlSurveyFamilyList").append(html_list);
                $("#ddlSurveyFamilyList").attr("disabled", false);
            }
            else {
                $("#ddlSurveyFamilyList").append("<option value=0>No Data</option>");
                $("#ddlSurveyFamilyList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            endajaxrequestSurveyResp();
            $("#divDdlListsContent").show();
        }
    })

}

function GetSurveyDetails() {

    var formdata = {
        "survey_id": $('#ddlSurveyList :selected').val(),
        "family_id": $('#ddlSurveyFamilyList :selected').val()
    }
    
    var posturl = "/ClientPortal/Pie/psp_dsp_captured_survey_responses";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestSurveyResp();
        },
        success: function (data) {
            if (data.length > 0) {
                console.log(data);
                var html = "";
                var filledByName = data[0].filled_by_name;
                var filledOn = data[0].survey_filled_date;
                var BranchName = data[0].branch_name;
                var RMName = data[0].rm_name;
                

                $('#tbl_response_dsp').DataTable().destroy();
                $("#tbody_tbl_response_dsp").html("");

                $.each(data, function (index, value) {
                    html = html + "<tr><td>" + value.question + "</th>" +
                        "<th>" + value.response + "</th></tr>"
                });

                $("#tbody_tbl_response_dsp").append(html);

                $('#tbl_response_dsp').DataTable({
                    "initComplete": function (settings, json) {

                        $("#FamName").val($('#ddlSurveyFamilyList :selected').text());
                        $("#FilledBy").val(filledByName);
                        $("#FilledOn").val(filledOn);
                        $("#BranchName").val(BranchName);
                        $("#RMName").val(RMName);

                        $("#dsp_div").show();
                        $('#tbl_response_dsp').DataTable().columns.adjust().draw();
                    },
                    "destroy": true,
                    "order": [],
                    "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
                        //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
                        { "bSortable": false },
                        { "bSortable": false }
                    ],
                    "paging": false,
                    "info": false,
                    "lengthChange": false
                });
            }
            else {
                alert("Error");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            endajaxrequestSurveyResp();
        }
    })

}