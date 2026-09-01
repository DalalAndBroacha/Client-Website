$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    fetchSurveyList();

});

$("#ddlClientList").change(function () {

    $("#main_div").hide();

});


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


function ViewSurveys() {

    var formdata = {
        "survey_id": $('#ddlSurveyList :selected').val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_survey";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#surveyCont").html("");
                var html = "";
                var groups = {};
                var groupName = "";
                var RadioBtnGrpgroups = {};
                //console.log(data)
                for (var i = 0; i < data.length; i++) {
                    groupName = data[i].question_sequnce;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                $.each(groups, function (index, value1) {
                    html = html + "<div class='surveyQuesTabs'>";
                    console.log(index);
                    //console.log(value1.length);
                    
                    if (value1[0].control_name == "radiobuttongroup") {
                        var Opts = ['Bad', 'Neutral' ,'Good', 'Excellent']
                        //$.each(value1, function (index1, value2) {
                        //    html = html + "<p>RadioBtnGrp" + index1 +
                        //        "</p>"
                        //})
                    }
                    else if (value1[0].control_name == "radiobutton") {

                        for (var i = 0; i < value1.length; i++) {
                            groupName = value1[i].sub_question_sequence;

                            if (!RadioBtnGrpgroups[groupName]) {
                                RadioBtnGrpgroups[groupName] = [];
                            }
                            RadioBtnGrpgroups[groupName].push(value1[i]);
                        }

                        console.log(RadioBtnGrpgroups);
                        var counter = 0
                        $.each(RadioBtnGrpgroups, function (index1, value2) {
                            console.log(value2);
                            console.log(index1);
                            
                            if (counter == 0) {
                                html = html + "<p class='lead'>" + value2[0].question + "</p>";
                            }
                            html = html + "<input type='radio' id='S3Yes' name='fav_language' value='1'>" +
                                "<span class='ml-1' for='S3Yes'>" + value2[0].display_value + "</span><br>"
                                //"<input type='radio' id='S3No' name='fav_language' value='0'>" +
                                //"<label class='ml-1' for='S3No'>" + value2.display_value + "</label>"
                            counter += 1;
                        })
                    }
                    else if (value1[0].control_name == "textbox") {
                        html = html + "<p>" + value1[0].question + "</p>" +
                            "<input type='text' class='form-control' id='txt'>";

                    }
                    html = html + "</div>";
                });
                html = html + "<div class='row justify-content-center'><input type='submit' class='btn btn-success mt-1' id='btnSub'></div>";
                $("#surveyCont").append(html);
            }
            else {
                console.log("Error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}