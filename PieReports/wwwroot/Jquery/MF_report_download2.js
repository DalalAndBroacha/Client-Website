$(document).ready(function () {

    HideFinYearList();
    CollapseSideMenu();
    FetchClientList();
    FetchReportList();
    ReportForm("1");
    
});


$("#ddlFamilyList").change(function () {
    FetchClientList();
});

$("#ddlReportList").change(function () {
    let report_id = $('#ddlReportList :selected').val()
    ReportForm(report_id);
});

function setDates() {

    var date = (new Date()).toISOString().split('T')[0];

    var formdata = {
        "date": date
    }
    var posturl = "/ClientPortal/Pie/psp_dsp_global_report_date_pills";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                var fy_start_date = formatDateold(data.curr_fy_start_date);
                
                $("#inpEndDate").val(date);
                $("#inpEndDate").attr("max", date);

                $("#inpStartDate").val(fy_start_date);
                $("#inpStartDate").attr("max", date);

            }
            else {
                alert("No data");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchReportList() {

    var formdata = {
        
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_mint_mf_report_download_list";
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

    var formdata = {
        "family_id": $('#ddlFamilyList :selected').val(),
        "asset_class": "4",
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
            $("#ddlClientList").html("");
            var html = "";

            if (data.length > 0) {
                
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.main_client_id + ">" + value.main_client_name + "</option>";
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

function ReportForm(report_id) {

    //let report_id = $('#ddlReportList :selected').val()
    //let report_id = "3";

    var formdata = {
        "report_id": report_id
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_mint_mf_download_ReportCriteria";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            html = "";
            $("#mainDiv").html("");

            let noClients = $('#ddlClientList').prop('disabled');
            if (!noClients) {
                if (data.length > 0) {

                    var today = new Date()
                    let tdate = (today).toISOString().split('T')[0];

                    var groups = {};
                    for (var i = 0; i < data.length; i++) {
                        var groupName = data[i].param_grp;

                        if (!groups[groupName]) {
                            groups[groupName] = [];
                        }
                        groups[groupName].push(data[i]);
                    }
                    html = html + "<div class='row justify-content-around mt-2'>";

                    $.each(groups, function (index, value1) {

                        $.each(value1, function (index1, value) {
                            if (value.param_type == "date") {
                                html = html + "<div class='input-group mb-1 col-3 p-2'>" +
                                    "<label class='m-2' for='" + value.param_id + "'>" + value.param_label + "</label>" +
                                    "<input type='date' class='form-control'  id='" + value.param_id + "' max='" + tdate + "'" +
                                    "name='" + value.param_id + "'" 
                                if (value.default_val != null) {
                                    if (value.default_val == "Todays date") {
                                        html = html + "'value='" + tdate + "'"
                                    }
                                    else {
                                        html = html + "value='" + value.default_val + "'"
                                    }
                                }
                                html = html + "/>" + "</div> "
                            }
                            else if (value.param_type == "text") {
                                html = html + "<div class='input-group mb-1 col-3 p-2'>" +
                                    "<label class='m-2' for='" + value.param_id + "'>" + value.param_label + "</label>" +
                                    "<input type='text' class='form-control' id='" + value.param_id + "' name='" + value.param_id + "'"
                                if (value.default_val != null) {
                                    html = html + "value='" + value.default_val + "'"
                                }
                                html = html + "/>" + "</div> "
                            }
                            else if (value.param_type == "radio") {
                                html = html + "<div class='input-group mb-1 col-3 p-2'>" +
                                    "<label class='m-2' for='" + value.param_id + "'>" + value.param_label + "</label>" +
                                    "<input type='radio'  class='form-control' id='" + value.param_id + "'" + "' name='" + value.param_grp + "'"
                                if (value.default_val != null) {
                                    html = html + "value='" + value.default_val + "'"
                                }
                                html = html + "/>" + "</div> "
                            }
                        });
                    });
                    html = html + "</div>"

                    $("#mainDiv").append(html);
                    $("#mainDiv").show();
                    $("#btnGenReport").show();
                }
                else {
                    alert("Data Not Found.")
                }
            }
            else {
                $("#mainDiv").hide();
                $("#btnGenReport").hide();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
    
}


function generateReport() {

    var formdata = {
        "report_id": report_id
    }

    var posturl = "/ClientPortal/Pie/genrateMfReport";
}


$("#btnSubmit").on("click", function (event) {
    //event.preventDefault();

    var paramsdata = $("#mfdatainp").serialize();    

    $("#paramsdata").val(paramsdata);   

    $('#mfdatainp').trigger('submit');

    //var posturl = "/ClientPortal/Pie/genrateMfReport";
    //$.ajax({
    //    url: posturl,
    //    type: "post",
    //    contentType: "application/json",
    //    data: JSON.stringify(formdata),
    //    success: function (data) {
    //        window.open(data.mfPostUrl)
    //    },
    //    error: function (xhr, err) {
    //        alert(err)
    //    }
    //})

});


