$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    ModRmFetchFamily();
    ChangeFetchFamily();

    $('#ddlRMList').select2({ dropdownCssClass: 'bigdrop'});
    $('#ddlBranchList').select2({ dropdownCssClass: 'bigdrop' });
    $('#ddlModBranch').select2({ dropdownCssClass: 'bigdrop' });
    $('#ddlAddRMList').select2({ dropdownCssClass: 'bigdrop' });
});

$("#txtCreateFamPAN").bind("paste", function (e) {
    //bwopb0151Q
    let pastedData = e.originalEvent.clipboardData.getData('text');

    const isPANvalid = ValidatePAN(pastedData.toUpperCase());

    if (isPANvalid) {

        $("#btnSearchPAN").prop('disabled', false);
    }
    else {
        $("#btnSearchPAN").prop('disabled', true);
        alert("Wrong PAN")
    }
});
$('#txtCreateFamPAN').on('keyup', function (e) {

    if (this.value.length == 10) {
        //ADGPV8394M
        let final = this.value
        final = final.toUpperCase();

        const isPANvalid = ValidatePAN(final);

        if (isPANvalid) {
            $("#btnSearchPAN").prop('disabled', false);
        }
        else {

            $("#btnSearchPAN").prop('disabled', true);
            alert("Wrong PAN")
        }

        return this.value.toUpperCase();
    }
    else {
        $("#btnSearchPAN").prop('disabled', true);
    }
});

$("#ddlChangeFamilyList").change(function () {

    let formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "family_id": $('#ddlChangeFamilyList :selected').val()
    }

    const posturl = "/ClientPortal/Pie/psp_dsp_single_family_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            $("#tbodyChangeFamdetails").html("");
            if (data != null) {
                html = html + "<tr>" +
                    "<td>" + data.display_id + "</td>" +
                    "<td>" + data.family_name + "</td>" +
                    "</tr>";
            }
            else {
                alert("Error in fetching family details");
            }
            $("#tbodyChangeFamdetails").append(html);
            $("#divChangeFamDetails").show();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

});

$("#ddlRMFamilyList").change(function () {

    $("#divModBranch").show()
    $("#divModRM").show()

    let formdata = {
        "family_token": $('#ddlRMFamilyList :selected').val()
    }

    const posturl = "/ClientPortal/Pie/FamilyMappingBranchRmDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            if (data != null) {
                $("#currBranch").text(data.branchResult.branch_name)
                $("#tbodyRmList").html("");
                $.each(data.rmResult, function (index, value) {
                    html = html + "<tr>" +
                        "<td><input type='hidden' name='rmId' value='" + value.rm_Id + "'/>" + value.rm_name + "</td>" +
                        "<td><button class='btn Del_rec' type='button' title='Delete'><i class='fas fa-trash-alt fa-lg White_Red_Icon'></i></button></td>" +
                        "</tr>"
                });
                
                $("#tbodyRmList").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
});
function initCreateFam() {
    $("#divNewFamDetails").show();
    $("#divUpdateFam").hide();

    $("#btnSearchPAN").prop('disabled', true);
    $("#txtCreateFamPAN").prop('disabled', true);


}
function initUpdateFam() {
    $("#divUpdateFam").show();
    $("#divNewFamDetails").hide();

    $("#btnSearchPAN").prop('disabled', true);
    $("#txtCreateFamPAN").prop('disabled', true);
}
function ModRmFetchFamily() {

    let length = $('#ddlRMFamilyList').children('option').length;

    if (length < 501) {
        $('#ddlRMFamilyList').select2();
    }
    else {

        let LoginId = $("#LoggedInUID").val()

        $('#ddlRMFamilyList').select2({
            ajax: {
                url: "/ClientPortal/Pie/SearchFamilyName",
                dataType: 'json',
                delay: 750, // wait 750 milliseconds before triggering the request
                data: function (params) {
                    return {
                        loginID: LoginId,
                        display_flag: "1",
                        searchTerm: params.term // search term
                    };
                },
                processResults: function (data, params) {

                    params.page = params.page || 1; //indicate that infinite scrolling can be used

                    let results = [];
                    $.each(data, function (k, v) { //parse the results into the format expected by Select2
                        results.push({
                            id: v.family_Token,
                            text: v.family_Name
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: (params.page * 30) < data.total_count
                        }
                    };
                },

            },
            minimumInputLength: 4,
            maximumInputLength: 100,
            templateResult: repoFormatResult,
            templateSelection: repoFormatSelection
        });
    }
}
function ChangeFetchFamily() {

    let length = $('#ddlChangeFamilyList').children('option').length;

    if (length < 501) {
        $('#ddlChangeFamilyList').select2();
    }
    else {

        let LoginId = $("#LoggedInUID").val()

        $('#ddlChangeFamilyList').select2({
            ajax: {
                url: "/ClientPortal/Pie/SearchFamilyName",
                dataType: 'json',
                delay: 750, // wait 750 milliseconds before triggering the request
                data: function (params) {
                    return {
                        loginID: LoginId,
                        display_flag: "1",
                        searchTerm: params.term // search term
                    };
                },
                processResults: function (data, params) {

                    params.page = params.page || 1; //indicate that infinite scrolling can be used

                    let results = [];
                    $.each(data, function (k, v) { //parse the results into the format expected by Select2
                        results.push({
                            id: v.family_Token,
                            text: v.family_Name
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: (params.page * 30) < data.total_count
                        }
                    };
                },

            },
            minimumInputLength: 4,
            maximumInputLength: 100,
            templateResult: repoFormatResult,
            templateSelection: repoFormatSelection
        });
    }
}
function searchPAN() {
    let pan = $("#txtCreateFamPAN").val();

    let formdata = {
        "pan": pan
    }
    const posturl = "/ClientPortal/Pie/psp_dsp_family_mapping_fam_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            $("#btnCreateFam").show();
            $("#btnUpdateFam").show();
            $("#tbodyCurrFamdetails").html("");
            if (data != null) {

                $("#CurrFamId").val(data.family_id);
                
                html = html + "<tr>" +
                    "<td>" + data.client_name + "</td>";
                if (data.unmap_flag == "1") {
                    html = html + "<td style='color:red;'>" + data.family_name + "</td>"
                }
                else {
                    html = html + "<td>" + data.family_name + "</td>"
                }
                html = html + "<td>" + data.contact_no + "</td>" +
                    "<td>" + data.email_id + "</td>" +
                    "</tr>" +
                    "<tr>" +
                    "<th>Branch</th>" +
                    "<th colspan=3>RM</th>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>" + data.branch_name + "</td>" +
                    "<td colspan=3>" + data.rm_name + "</td>" +
                    "</tr>"
                
            }
            else {

                $("#btnCreateFam").hide();
                $("#btnUpdateFam").hide();

                html = html + "<tr class='SubTotalRow'><td colspan='6'><p class='ReportTableFont paraTextAlignCenter'>" +
                    "PAN does not exist in the system." +
                    "</p></td></tr>"
            }
            $("#tbodyCurrFamdetails").append(html);
            $("#divCurrFamDetails").show();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}
function ChangeFam() {
    let formdata = {
        "login_id": $("#LoggedInUID").val(),
        "new_fam_guid": $('#ddlChangeFamilyList :selected').val(),
        "pan": $("#txtCreateFamPAN").val(),
        "old_fam_id": $("#CurrFamId").val()
    }

    const posturl = "/ClientPortal/Pie/psp_amd_family_mapping_change_family";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            alert(data.sql_msg);
            location.reload();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function CreateFamily() {

    $("#divErrorList").hide();
    $("#btnCreateFam").prop('disabled', true);

    let login_id = $("#LoggedInUID").val();
    let family_name = $("#inpFamName").val();
    let email_id = $("#inpEmail").val();
    let mobile = $("#inpMobile").val();
    let branch = $("#ddlBranchList").val();
    let rm_list = $('#ddlRMList').val().toString();

    let formdata = {
        "login_id": login_id,
        "family_name": family_name,
        "email_id": email_id,
        "mobile": mobile,
        "branch": branch,
        "rm_list": rm_list,
        "pan": $("#txtCreateFamPAN").val()
    }

    let posturl = "/ClientPortal/Pie/psp_amd_family_mapping_create_fam";
    
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {
                if (data.sql_status == "0") {

                    let errHtml = "";
                    $("#errorList").html("");
                    
                    errHtml = "<ul style='list-style:square;'>"

                    $.each(data.errorList, function (key, val) {
                        $.each(val, function (key1, val1) {
                            errHtml = errHtml + "<li>" + val1.errorMessage + "</li>"
                        });
                    });
                    if (data.sql_msg != null) {
                        errHtml = errHtml + "<li>" + data.sql_msg + "</li>"
                    }
                    errHtml = errHtml + "</ul>";
                    $("#errorList").append(errHtml);
                    $("#divErrorList").show();
                    $("#btnCreateFam").prop('disabled', false);
                }
                else {

                    alert(data.sql_msg);
                    location.reload();
                }   
            }
            else {
                alert("Error in processing request.")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}
function ChangeBranch() {
    let formdata = {
        "login_id": $("#LoggedInUID").val(),
        "family_token": $("#ddlRMFamilyList").val(),
        "new_branch_id": $("#ddlModBranch").val()
    }

    const posturl = "/ClientPortal/Pie/psp_amd_family_mapping_branch_update";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {
                $("#ddlRMFamilyList").trigger("change");
                alert(data.sql_msg);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function AddNewRm() {

    let rm_string = $("#ddlAddRMList").val().toString();

    if (rm_string != "") {

        let formdata = {
            "login_id": $("#LoggedInUID").val(),
            "family_token": $("#ddlRMFamilyList").val(),
            "rm_id": $("#ddlAddRMList").val().toString(),
            "amd_flag": "A"
        }

        const posturl = "/ClientPortal/Pie/PspAmdFamilyMappingRmUpdate";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {
                if (data != null) {

                    $("#ddlAddRMList").val(""); //To clear Select2 selection
                    $("#ddlAddRMList").trigger("change"); //To clear Select2 selection

                    $("#ddlRMFamilyList").trigger("change");
                    alert(data.sql_msg);
                }
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        alert("Please select RM.")
    }
}

$(document).on("click", ".Del_rec", function () {

    let rec_id = $(this).parents('tr').find('input:hidden[name=rmId]').val()

    let formdata = {
        "login_id": $("#LoggedInUID").val(),
        "family_token": $("#ddlRMFamilyList").val(),
        "amd_flag": "D",
        "rm_id": rec_id
    }

    const posturl = "/ClientPortal/Pie/PspAmdFamilyMappingRmUpdate";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {
                $("#ddlRMFamilyList").trigger("change");
                alert(data.sql_msg);
            }
            else {
                alert("Error!");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

});