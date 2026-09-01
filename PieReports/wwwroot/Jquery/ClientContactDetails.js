$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");

    HideFinYearList();

});

$("#ddlFamilyList").change(function () {

    $("#main_data_div").hide();

});

function startajaxrequestClientDetails() {
    $("#ClientDetailswaitIn").css("display", "block");
}
function completeajaxrequestClientDetails() {
    $("#ClientDetailswaitIn").css("display", "none");
}



function ViewReport() {

    GetFamilyDetails();
    GetClientDetails();

    $("#main_data_div").show();

}


function GetClientDetails() {

    var formdata = {
        "family_id": $('#ddlFamilyList :selected').val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_client_contact_details";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        complete: function (xhr) {
            completeajaxrequestClientDetails();
        },
        success: function (data) {

            if (data.length > 0) {

                $("#tbl_contact_details").html("");
                var html = "";
                var groups = {};

                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].pan_number;

                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                $.each(groups, function (index, value1) {

                    html = html + "<tr class='SubTotalRow'><td>" +
                        "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#acc_" + value1[0].main_client_id + "'" +
                        "aria-expanded='false' aria-controls='divi_acc_" + value1[0].main_client_id + "'>" +
                        "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i>" +
                        "</button>&nbsp;" + value1[0].main_client_name + "(PAN Number: " + value1[0].pan_number + ")</td></tr>"
                    html = html + "<tr><td>" +
                        "<div class='collapse' id='acc_" + value1[0].main_client_id + "' data-parent='#main_data_div'>" +
                        //"<div class='global_report_accords'>" + //For setting max height
                        "<table id='details_table_" + value1[0].main_client_id + "' class='table table-head-fixed table-hover w-100'>" +
                        "<colgroup><col style='width:10%;'><col style='width:15%;'><col style='width:10%;'><col style='width:50%;'>" +
                        "<col style='width:10%;'></colgroup>" +
                        "<thead><tr><th>Segment</th><th>Client Name</th><th>Account Code/ Folio Number</th><th>Contact Details</th>" +
                        "<th>Aadhar Number</th></tr></thead><tbody>"
                    $.each(value1, function (index1, value) {
                        html = html + "<tr>" +
                            "<td><p class='ReportTableFont paraTextAlignCenter'>" + value.segment + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.client_name + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignLeft'>" + value.account_code + "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignLeft'><b>Address : </b>" + value.address + "<br>" +
                            "<b>Email : </b>" + value.email + "<br>" +
                            "<b>Contact 1 : </b>" + value.contact_no1 + "<br>" +
                            "<b>Contact 2 : </b>" + value.contact_no2 + "<br>" +
                            "</p></td>" +
                            "<td><p class='ReportTableFont paraTextAlignLeft'><p>"; //For Aadhar
                        if (value.aadhar_no1 != "") {
                            html = html + "Holder 1: " + value.aadhar_no1 + "<br>"
                        }
                        if (value.aadhar_no2 != "") {
                            html = html + "Holder 2: " + value.aadhar_no2 + "<br>"
                        }
                        if (value.aadhar_no3 != "") {
                            html = html + "Holder 3: " + value.aadhar_no3
                        }
                            
                        html = html + "</p></td></tr>"
                    })
                    html = html + "</tbody></table>" +
                        //"</div>" + //global_report_accords end
                        "</div>" + //Collapse end
                        "</td></tr>"
                });
                $("#tbl_contact_details").append(html);
            }
            else {
                $("#tbl_contact_details").html("");

                html = html + "<tr class='SubTotalRow'><td><p class='ReportTableFont paraTextAlignCenter'>" +
                    "No data found." +
                    "</p></td></tr>"

                $("#tbl_contact_details").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetFamilyDetails() {

    var formdata = {
        "LoginId": $('#ddlFamilyList :selected').val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_family_details";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestClientDetails();
        },
        success: function (data) {

            if (data != null) {

                $("#tbl_fam_details").html("");
                var html = "";

                html = html + "<thead><tr><th>User name</th><th>Family Name</th><th>Family Contact</th><th>Family E-mail ID</th>" +
                    "<th>Branch Name</th></tr></thead>" +
                    "<tbody><tr>" +
                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + data.family_code + "</p></td>" +
                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + data.family_name + "</p></td>" +
                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + data.contact_No + "</p></td>" +
                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + data.email_Id + "</p></td>" +
                    "<td><p class='ReportTableFont paraTextAlignCenter'>" + data.branch_Name + "</p></td>" +
                    "</tbody></tr>"

                $("#tbl_fam_details").append(html);
            }

            
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}