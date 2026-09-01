$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    FetchAssetList();
    FetchBranchList();

});

$("#ddlBranch").change(function () {
    FetchRMList();
});

function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}

function FetchAssetList() {

    var posturl = "/ClientPortal/Pie/AssetList";
    $.ajax({
        url: posturl,
        type: "get",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            $("#ddlAsset").append("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.asset_id + ">" + value.asset_name + "</option>";
            });
            $("#ddlAsset").append(html);    
            $('#ddlAsset').select2();
            
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchBranchList() {

    var posturl = "/ClientPortal/Pie/psp_dsp_branch_list";

    var formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "cat_type": "2"
    }

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
            FetchRMList();

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchRMList() {
    var formdata = {
        "Branch": $('#ddlBranch :selected').val()
    }
    var posturl = "/ClientPortal/Pie/RMList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlRM").html("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.rm_id + ">" + value.rm_login_name + "</option>";
            });
            $("#ddlRM").append(html);
            $('#ddlRM').select2();

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}


function GetRMDetails() {
    var formdata = {
        "Branch": $('#ddlBranch :selected').val(),
        "RM": $('#ddlRM :selected').val(),
        "Asset": $('#ddlAsset :selected').val()
    }
    var posturl = "/ClientPortal/Reports/GetRMDetails";
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
                $('#divTableRMDetails').show();
                $('#tableRMDetails').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                        $("#norecordsMISRMDeatils").hide();
                        $("#report_header_and_buttons").show();
                    },
                    "destroy": true,
                    "pagingType": "full_numbers",
                    "data": data,
                    "dataSrc": "",
                    "columns": [
                        {
                            data: "rm_name",
                            render: function (data, type, src) {
                                return "<p>" + src.rm_name + "</p>";
                            }
                        },
                        {
                            data: "family_name",
                            render: function (data, type, src) {
                                return "<p>" + src.family_name + "</p>";
                            }
                        },
                        {
                            data: "client_name",
                            render: function (data, type, src) {
                                return "<p>" + src.client_name + "</p>";
                            }
                        },
                        {
                            data: "pan_number",
                            render: function (data, type, src) {
                                return "<p>" + src.pan_number + "</p>";
                            }
                        },
                        {
                            data: "asset_Name",
                            render: function (data, type, src) {
                                return "<p>" + src.asset_Name + "</p>";
                            }
                        },
                        {
                            data: "account_code",
                            render: function (data, type, src) {
                                return "<p>" + src.account_code + "</p>";
                            }
                        }
                    ]

                });
            }
            else {
                $('#divTableRMDetails').hide();
                $("#report_header_and_buttons").hide();
                $("#norecordsMISRMDeatils").show();
            }
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
    
}


//**** Button Clicks ****
$("#btnEmailRMDetails").click(function () {

    var famId = "0"
    var LoginId = $("#LoggedInUID").val()

    var Branch = $('#ddlBranch :selected').val()
    var RM = $('#ddlRM :selected').val()
    var asset = $('#ddlAsset :selected').val()

    var strDescrip = "RM Details " + $('#ddlBranch :selected').text() + " " + $('#ddlRM :selected').text();

    var strPara = LoginId + "~" + Branch + "~" + RM + "~" + asset;

    PDFandExcelExport("#btnEmailRMDetails", "18", LoginId, famId, "E", "PDF", strPara, strDescrip);

})

$("#btnPDFRMDetails").click(function () {

    var famId = "0"
    var LoginId = $("#LoggedInUID").val()

    var Branch = $('#ddlBranch :selected').val()
    var RM = $('#ddlRM :selected').val()
    var asset = $('#ddlAsset :selected').val()

    var strPara = LoginId + "~" + Branch + "~" + RM + "~" + asset;

    var strDescrip = "RM Details " + $('#ddlBranch :selected').text() + " " + $('#ddlRM :selected').text();

    PDFandExcelExport("#btnPDFRMDetails", "18", LoginId, famId, "X", "PDF", strPara, strDescrip);

})

$("#btnExportRMDetails").click(function () {

    var famId = "0"
    var LoginId = $("#LoggedInUID").val()

    var Branch = $('#ddlBranch :selected').val()
    var RM = $('#ddlRM :selected').val()
    var asset = $('#ddlAsset :selected').val()

    var strPara = LoginId + "~" + Branch + "~" + RM + "~" + asset;

    var strDescrip = "RM Details " + $('#ddlBranch :selected').text() + " " + $('#ddlRM :selected').text();

    PDFandExcelExport("#btnExportRMDetails", "18", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//**** Button Clicks ****