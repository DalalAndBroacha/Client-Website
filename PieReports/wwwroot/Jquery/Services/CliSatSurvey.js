$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    checkIfAlreadySubmitted();

});

$("#ddlFamilyList").change(function () {

    checkIfAlreadySubmitted();

});


$("#surveyForm").submit(function (event) {

    event.preventDefault();
    var response_string = "";

    response_string = $('input[name="S1Q1"]:checked').val() + "|" + $('input[name="S1Q2"]:checked').val() + "|" +
        $('input[name="S1Q3"]:checked').val() + "|" + $('input[name="S1Q4"]:checked').val();

    response_string = response_string + "|" + $('input[name="S2Q1"]:checked').val() + "|" + $('input[name="S2Q2"]:checked').val() + "|" +
        $('input[name="S2Q3"]:checked').val() + "|" + $('input[name="S2Q4"]:checked').val() +
        "|" + $('input[name="S2Q5"]:checked').val() + "|" + $('input[name="S2Q6"]:checked').val();

    response_string = response_string + "|" + $('input[name="WebApp"]:checked').val();
    response_string = response_string + "|" + $('input[name="MobApp"]:checked').val();
    response_string = response_string + "|" + $('#txtSugg').val();
    response_string = response_string + "|" + $('input[name="RadioRefer"]:checked').val();

    response_string = response_string + "|" + $('#txtReferName1').val();
    response_string = response_string + "|" + $('#txtReferContact1').val();
    response_string = response_string + "|" + $('#txtReferEmail1').val();

    response_string = response_string + "|" + $('#txtReferName2').val();
    response_string = response_string + "|" + $('#txtReferContact2').val();
    response_string = response_string + "|" + $('#txtReferEmail2').val();

    var formdata = {
        "login_id": $('#LoggedInUID').val(),
        "family_id": $('#ddlFamilyList').val(),
        "survey_id": "1",
        "response": response_string
    }

    var posturl = "/ClientPortal/Pie/psp_amd_survey_response";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            console.log(data);

            if (data != null) {

                $('#survey_status').addClass('alert-success').removeClass('alert-danger');
                $('#survey_container').hide();
                $('#dsp_msg').text("");
                $('#dsp_msg').text(data.sql_message);
                $('#survey_status').show();
                
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

    
});

function checkIfAlreadySubmitted() {
    var formdata = {
        "family_id": $('#ddlFamilyList').val(),
        "survey_id": "1"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_survey_status";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.dsp_flag == "Y") {

                $('#survey_container').show()
                $('#survey_status').hide()

            }
            else {

                $('#survey_status').addClass('alert-danger').removeClass('alert-success');
                $('#survey_container').hide();
                $('#dsp_msg').text("");
                $('#dsp_msg').text(data.sql_message);
                $('#survey_status').show();
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}