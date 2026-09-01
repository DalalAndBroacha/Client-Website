$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    FetchBranchList();

});
function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}

function FetchBranchList() {

    var formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "cat_type": "1"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_branch_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlBranch").append("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.id + ">" + value.name + "</option>";
            });
            $("#ddlBranch").append(html);
            $('#ddlBranch').select2();

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function GetFamDetails() {
    var formdata = {
        "login_id": $("#LoggedInUID").val(),
        "branch_id": $('#ddlBranch :selected').val(),
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_brach_family_details";
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
                var html = "";
                $("#tableFamDetails").DataTable().destroy();
                $("#divDetails").html("");

                html = html + "<table id='tableFamDetails' class='table table-hover w-100 table-bordered'><thead><tr>" +
                    "<th>Branch Name</th>" +
                    "<th>Branch Head Name</th>" +
                    "<th>RM Name</th>" +
                    "<th>Family Code</th>" +
                    "<th>Family Name</th>" +
                    "<th>Email - Id</th>" +
                    "<th>Contact Number</th>" +
                    "</tr>" +
                    "</thead><tbody>";

                for (var i = 0; i < data.length; i++) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignLeft'>" + data[i].branch_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].branch_head + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].rm + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].family_code + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].family_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].fam_Email + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].fam_contact + "</td>" +
                        "</tr>"

                }
                html = html + "</tbody>";

                $("#divDetails").append(html);
                
                $('#tableFamDetails').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                        $("#report_header_and_buttons").show();
                        $("#divDetails").show();
                        $('#tableFamDetails').DataTable().columns.adjust().draw();
                    },
                    "pagingType": "full_numbers",
                    "scrollX": true
                });
            }
            else {
                $("#divDetails").hide();
                $("#norecordsMISFamDeatils").html("");
                $("#norecordsMISFamDeatils").append("No Record Found");
                completeajaxrequestwaitIn();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

//**** Button Clicks ****
$("#btnEmailFamDetails").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();
    var Branch = $('#ddlBranch :selected').val();

    var strPara = LoginId + "~" + Branch;

    var strDescrip = "Family Details for Branch: " + $('#ddlBranch :selected').text();

    PDFandExcelExport("#btnEmailFamDetails", "20", LoginId, famId, "E", "PDF", strPara, strDescrip);

})

$("#btnPDFFamDetails").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();
    var Branch = $('#ddlBranch :selected').val();

    var strPara = LoginId + "~" + Branch;

    var strDescrip = "Family Details for Branch: " + $('#ddlBranch :selected').text();

    PDFandExcelExport("#btnPDFFamDetails", "20", LoginId, famId, "X", "PDF", strPara, strDescrip);

})

$("#btnExportFamDetails").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();
    var Branch = $('#ddlBranch :selected').val();

    var strPara = LoginId + "~" + Branch;

    var strDescrip = "Family Details for Branch: " + $('#ddlBranch :selected').text();

    PDFandExcelExport("#btnExportFamDetails", "20", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//**** Button Clicks ****