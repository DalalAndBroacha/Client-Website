$(document).ready(function () {
    $("#btn_Submit_div").hide();
    $("#download_link").hide();
    $("#error_msg").hide();


    HideFinYearList();
    HideFamilyList();
    CollapseSideMenu();

    FetchClientList();
    FetchReportList();
    

});

function FetchClientList() {
    $("#download_link").hide();
    var LoginId = $("#LoggedInUID").val();
    var formdata = {
        "Loginid": LoginId
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_ws_client_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlWSClientList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    //html = html + "<option value=" + value.ws_client_id + ">" + value.client_name + "</option>";
                    html = html + "<option value='{\"Id\":\"" + value.ws_client_id + "\",\"Date\":\"" + value.maturity_date + "\"}'>" + value.client_name + "</option>";
                });


                $("#ddlWSClientList").append(html);
                $("#ddlWSClientList").attr("disabled", false);

            }
            else {
                alert("PMS clients not found in these login.")
                $("#ddlReportList").html("");
                $("#InputData").hide();
                $("#btn_Submit").hide();
                $("#error_msg").hide();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
$("#ddlWSClientList").change(function () {
    //$("#btn_Submit_div").hide();
    let val1 = JSON.parse($('#ddlWSClientList :selected').val());

    $("#download_link").hide();
    $("#error_msg").hide();
    ReportForm();
});

function FetchReportList() {
    $("#error_msg").hide();
    $("#download_link").hide();
    var LoginId = $("#LoggedInUID").val();
    var formdata = {
        "Loginid": LoginId
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_ws_download_report_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlReportList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.report_id + ">" + value.report_name + "</option>";
                });
                $("#ddlReportList").append(html);
                $('select option:contains("Portfolio Fact Sheet")').prop('selected', true);
                $("#ddlReportList").attr("disabled", false);
                ReportForm();

            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

$("#ddlReportList").change(function () {
    $("#download_link").hide();
    $("#error_msg").hide();
    ReportForm();
});

function ReportForm() {
    let today = ""
    today = new Date()
    today.setDate(today.getDate() - 1)
    let date = (today).toISOString().split('T')[0];

    let report_id = $('#ddlReportList :selected').val()
    var formdata = {
        "report_id": report_id,
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_download_ReportCriteria";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            $("#InputData").html("");
            if (data.length > 1) {
                let groups = {};
                for (let i = 0; i < data.length; i++) {
                    let groupName = data[i].field;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                html = html + "<div class ='row' id= 'formdata'>" 
                //"<div class='input-group mb-1 col-6' style='padding:10px; display:none''><input type ='text' class='form-control' value = 'C' name = 'scope' ></div > " +
                //    "<div class='input-group mb-1 col-6' style='padding:10px;display:none'><input type ='text' class='form-control' value = " + client_id + " name = 'scopeId' ></div > " 
                    
                $.each(groups, function (index, value1) {
                    if (value1.length > 1) {

                        if (value1[0].flag == 'N') {

                            if (value1[0].defaultValue_CP == '') {

                                html = html + "<div style='display:none'><input type='hidden' class='select_field' value=" + value1[0].field + ">" +
                                    "<select type='hidden' id='Coptionlist' class = 'form-control drpdwn_control' name = " + value1[0].field + "> <option value = " + value1[0].defaultValue + "></option ></select ></div> "
                            }
                            else {
                                html = html + "<div style='display:none'><input type='hidden' class='select_field' value=" + value1[0].field + ">" +
                                    "<select type='hidden' id='noptionlist' class = 'form-control drpdwn_control' name = " + value1[0].field + "> <option value = " + value1[0].defaultValue_CP + "></option ></select ></div> "
                            }
                        }
                        else {
                            html = html + "<div class='input-group mb-1 col-3' style='padding:10px'><label for='optionlist'>" + value1[0].label + " </label></div>" +
                                "<div class='input-group mb-1 col-1'></div>" +
                                "<div class='input-group mb-1 col-6' style='padding:10px'>" +
                                "<select id='optionlist' class = 'form-control drpdwn_control criteriaoption'  name = " + value1[0].field + ">"
                            $.each(value1, function (index1, value) {
                                html = html + "<option class= 'optionlist1' value=" + value.code + ">" + value.name + "</option>"

                            });
                            html = html +
                                "</select ></div > "
                        }
                    }
                    else {
                        $.each(value1, function (index1, value) {
                            let val1 = JSON.parse($('#ddlWSClientList :selected').val());
                            let maturity_date = formatDateold(val1.Date);
                            let Eod_date = formatDateold(value.eoD_date)

                            //let maturity1_date = val1.Date;
                            //var tmpmat_date = new Date(maturity1_date);
                            //let maturity_date = formatDateold(tmpmat_date.setDate(tmpmat_date.getDate() + 1))

                            let maxDate = (maturity_date < Eod_date && maturity_date != '1900-01-01') ? maturity_date : Eod_date;

                            if (value.flag == 'N') {
                                if (value.type == 'date' && value.field == 'fDate') {
                                    html = html +
                                        "<div class='input-group mb-1 col-6' style='padding:10px; display:none'><input type = 'date' class='form-control text_control' id ='nfromdate' value =" + maxDate + " name = " + value.field + "> </div>" +
                                        "<div ><input type = 'hidden' id ='dateflag' value =" + value.flag + "> </div>"
                                }
                                else if (value.type == 'date' && value.field == 'tDate') {
                                    html = html +
                                        "<div class='input-group mb-1 col-6' style='padding:10px; display:none'><input type = 'date' class='form-control text_control' id ='ntodate' value =" + maxDate + "  name = " + value.field + "> </div>" +
                                        "<div ><input type = 'text' id ='dateflag' value =" + value.flag + "> </div>"
                                }
                                else {
                                    if (value.defaultValue_CP == '') {
                                        html = html +
                                            "<div class='input-group mb-1 col-6' style='padding:10px; display:none'><input type = 'text' class='form-control drpdwn_control' name = " + value.field + " value = " + value.defaultValue + "> </div>"
                                    }
                                    else {
                                        html = html +
                                            "<div class='input-group mb-1 col-6' style='padding:10px; display:none'><input type = 'text' class='form-control drpdwn_control' name = " + value.field + " value = " + value.defaultValue_CP + "> </div>"
                                    }
                                }
                            }
                            else {

                                if (value.type == 'date' && value.field == 'fDate') {                                    
                                    html = html + "<div class='input-group mb-1 col-3' style='padding:10px' > <label for='dateinput'>" + value1[0].label + " </label></div>" +
                                        "<div class='input-group mb-1 col-1'></div>" +
                                        "<div class='input-group mb-1 col-6' style='padding:10px'><input type = 'date' class='form-control text_control' id ='fromdate' value =" + maxDate + " max =" + maxDate + " name = " + value.field + "> </div>"
                                }
                                else if (value.type == 'date' && value.field == 'tDate') {
                                    html = html + "<div class='input-group mb-1 col-3' style='padding:10px' > <label for='dateinput'>" + value1[0].label + " </label></div>" +
                                        "<div class='input-group mb-1 col-1'></div>" +
                                        "<div class='input-group mb-1 col-6' style='padding:10px'><input type = 'date' class='form-control text_control' id ='todate' value =" + maxDate + " max= " + maxDate + " name = " + value.field + "> </div>"
                                }
                            }
                        });
                    }
                });
                html = html +
                    //"<div class='input-group mb-1 col-6' style='padding:10px; display:none'><input type ='text' class='form-control' value = 'false' name = 'mailSelf' ></div > " +
                    "</div >" 

                /*$("#todate").max;*/

                $("#InputData").append(html);
                $("#btn_Submit_div").show();

            }
            else {
                alert("Data Not Found.")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function savedata() {
/*    $("#btn_Submit_div").hide();*/
    $("#error_msg").hide();
    $("#download_link").hide();

    let val1 = JSON.parse($('#ddlWSClientList :selected').val());

    var LoginId = $("#LoggedInUID").val();
    let report_id = $('#ddlReportList :selected').val()
    let report_name = $('#ddlReportList :selected').text()
    let client_name = $('#ddlWSClientList :selected').text()
    //let client_id = $('#ddlWSClientList :selected').val()
    let client_id = val1.Id;
    let fromdate = $('#fromdate').val()
    let nfromdate = $('#nfromdate').val()
    let todate = $('#todate').val()
    let flag = $('#dateflag').val()

    let mainString = "";
    let criteriastring = "";

    if (fromdate > todate) {
        alert("Please select valid date.")
    }
    else {
        if (flag == 'N') {
            mainString = '"scope":"C", "scopeId":\"' + client_id + '\","tDate":\"' + todate + '\","fDate":\"' + nfromdate + '\","mailSelf": "false",';
        }
        else {
            mainString = '"scope":"C", "scopeId":\"' + client_id + '\","tDate":\"' + todate + '\","fDate":\"' + fromdate + '\","mailSelf": "false",';
        }

        //$("#formdata .form-control").each(function (index) {
        //    key = $(this).prop('name');
        //    value = $(this).val();
        //    //array.push('\'' + key + '\':' + '\'' + value + '\',');
        //    mainString += '\"' + key + '\":' + '\"' + value + '\",'
        //});
        $("#formdata .drpdwn_control").each(function (index) {
            key = $(this).prop('name');
            value = $(this).val();
            //array1.push('\'field:' + '\'' + key + '\'' + ', \'defaultValue\':' + '\'' + value + '\'');
            criteriastring += '{\"field\":' + '\"' + key + '\"' + ',\"defaultValue\":' + '\"' + value + '\"},'
        });

        criteriastring = criteriastring.slice(0, -1)

        let var2 = "{" + '\"menuId\":' + '\"' + report_id + '\",' + mainString + '\"reportCriteria\":[' + criteriastring + "]}";

        var formdata = {
            "reportCriteria": var2,
            "reportID": report_id,
            "Login_Id": LoginId,
            "Report_name": report_name,
            "client_name": client_name
        }
        var posturl = "/ClientPortal/Reports/psp_dsp_other_pms_report_parameter";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {
                if (data != null && data.status != 'false') {
                    if (data.filename != "") {
                        $("#download_link").html("");
                        html1 = "";
                        html1 = html1 + "<a style =' background-color: #28a745;border-color: black;color: white;margin-left:40%' class='btn DnB - RedLabel' href = 'getDocument?FP=" + data.filename + ".pdf' target='_blank'> View Report </a>";

                        alert("Click on view report button to view and download report.\nYou can also download the report from 'Go to Downloads' icon on top right, it will be available for 24 hours.")
                        $("#download_link").append(html1);
                        $("#download_link").show();
                    }
                    else {
                        alert('File not found.')
                    }
                }
                else {
                    $("#error_msg").html("");
                    errhtml = "";
                    errhtml = errhtml + "<p>" + data.error_msg + "</p>";
                    $("#error_msg").append(errhtml);
                    $("#error_msg").show();
                    alert(data.error_msg)
                    
                }
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
}


//$("#dateinput").change(function () {
//    $('dateinput').each(function () {
//        if (this.value != '')
//            values.push(this.value);
//        console.log(values)
//    });
//});

