$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    FetchClientList();
    FetchClientIncomeRange();
    FetchKYCRelationList();
    $("#divDdlListsContent").show();

    //$("#div_resend_otp1_timer").hide();
});

$("#ddlFamilyList").change(function () {

    resetEntireForm();
    
    FetchClientList();
    $("#divDdlListsContent").show();    

});

$("#ddlClientList").change(function () {

    resetEntireForm();

});

$("#IDchkEmail").change(function () {
    if ($(this).prop("checked")) {
        $("#txtNewEmail").prop('disabled', false);
        $("#ddlNewEmailRela").prop('disabled', false);
    }
    else {
        $("#txtNewEmail").val('');
        $("#txtNewEmail").prop('disabled', true);
        $("#ddlNewEmailRela").prop('disabled', true);
    }
})

$("#IDchkMob").change(function () {
    if ($(this).prop("checked")) {
        $("#txtNewMob").prop('disabled', false);
        $("#ddlNewMobRela").prop('disabled', false);
    }
    else {
        $("#txtNewMob").val('');
        $("#txtNewMob").prop('disabled', true);
        $("#ddlNewMobRela").prop('disabled', true);
    }
})

$("#IDchkIncomeRange").change(function () {
    if ($(this).prop("checked")) {
        $("#txt_NW").prop('disabled', false);
        $("#NW_date").prop('disabled', false);
        $("#ddlIncomeRangeList").prop('disabled', false);
    }
    else {
        $("#txt_NW").val('');
        $("#NW_date").val('');
        $("#txt_NW").prop('disabled', true);
        $("#NW_date").prop('disabled', true);
        $("#ddlIncomeRangeList").prop('disabled', true);
    }
})

//For dynamically created element you have to use event delegation
$(document).on("change", "input[name='EditChecks']", function () { 

    let atLeastOneIsChecked = $('input[name="EditChecks"]:checked').length > 0;

    if (atLeastOneIsChecked) {
        $("#btnEditRecs").show();
    }
    else {
        $("#btnEditRecs").hide();
    }

});

function AnimateBackgroundRed(target_div) {
    $(target_div).animate({ backgroundColor: '#FF0000' }, 'slow', function () {
        $(target_div).animate({ backgroundColor: '#FFFFFF' }, 'slow');
    });
}

function resetEntireForm() {

    //$("#txtValiOTP").val("");
    $("#txtVerifyOTP").val("");

    $('#verifyOTPtoken').val("");
    //$('#ValidateOTPtoken').val("");

    $("#OTP_section").hide();
    $("#OTP_section_Vali").hide();
    $("#dsp_table").hide();
    $("#dsp_table_legend").hide();

    $("#editable_row").hide();
    $("#btnEditRecs").hide();

    $("#edit_details_container :input").prop("disabled", false);
    $("#decla_div :input").prop("disabled", false);
    $("#editable_row form")[0].reset();

    $("#IDchkEmail").bootstrapToggle("off")
    $("#IDchkMob").bootstrapToggle("off")
    $("#IDchkIncomeRange").bootstrapToggle("off")

    $("#timer1").hide();
    $("#timer2").hide();

    $("#error_msg1").hide();
    $("#error_msg2").hide();

    $("#dsp_msg").hide();
    $("#dsp_msg_final").hide();
   
}

//function checkNumberFieldLength(elem) {
//    if (elem.value.length > 6) {
//        elem.value = elem.value.slice(0, 6);
//    }
//}

function FetchClientList() {

    let formdata = {
        "family_id": $('#ddlFamilyList :selected').val(),
        "asset_class": "1,6",
        "pan_no": "",
        "level": "1"
    }

    const posturl = "/ClientPortal/Pie/psp_dsp_client_accounts";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlClientList").html("");
                let html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.main_client_id + ">" + value.main_client_name + "</option>";
                });

                $("#divNoEqClients").hide();
                $("#divDdlListsContent").show();
                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
            }
            else {
                //$("#ddlClientList").append("<option value=0>No Data</option>");
                //$("#ddlClientList").attr("disabled", true);
                $("#divNoEqClients").show();
                $("#divDdlListsContent").hide();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchClientIncomeRange() {

    let posturl = "/ClientPortal/Pie/psp_dsp_kyc_income_range";
    $.ajax({
        url: posturl,
        type: "get",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlIncomeRangeList").html("");
                let html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.id + ">" + value.display_value + "</option>";
                });
                $("#ddlIncomeRangeList").append(html);

                $("#ddlIncomeRangeList").attr("disabled", false);
                //$('#ddlAccountsList').select2();
            }
            else {
                $("#ddlIncomeRangeList").append("<option value=0>No Data</option>");
                $("#ddlIncomeRangeList").attr("disabled", true);
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchKYCRelationList() {

    let posturl = "/ClientPortal/Pie/psp_dsp_kyc_relations";
    $.ajax({
        url: posturl,
        type: "get",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $(".KYC_relationList").html("");
                let html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.id + ">" + value.relation_name + "</option>";
                });
                $(".KYC_relationList").append(html);

                $(".KYC_relationList").attr("disabled", false);
                //$('#ddlAccountsList').select2();
            }
            else {
                $(".KYC_relationList").append("<option value=0>No Data</option>");
                $(".KYC_relationList").attr("disabled", true);
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetKYCDetails() {
    let formdata = {
        "main_client_id": $('#ddlClientList :selected').val(),
    }

    let posturl = "/ClientPortal/Pie/psp_dsp_get_kyc_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {
                resetEntireForm();

                //$('#dsp_table').DataTable().destroy();
                $("#dsp_table").html("");

                let html = "";
                html = html + 
                    "<thead><tr>" +
                    "<th>Edit</th>" +
                    "<th>Account</th>" +
                    "<th>Email</th>" +
                    "<th>Relation</th>" +
                    "<th>Mobile</th>" +
                    "<th>Relation</th>" +
                    "<th>Income Range</th>" +
                    "<th>Income Range Date</th>" +
                    "<th>Networth</th>" +
                    "<th>Networth Date</th>" +
                    "<th>Remarks</th>" +
                    "</tr></thead><tbody>";
                $.each(data, function (index, value) {

                    
                    if (value.enable_flag == "0") {
                        html = html + "<tr class='DnB-DisabledRow'>" +
                            "<td class='text-center'><input name='EditChecks' class='KYC_EditChecks' type='checkbox' disabled></td>"
                    }
                    else {
                        html = html + "<tr>" +
                            "<td class='text-center'><input name='EditChecks' class='KYC_EditChecks' type='checkbox'></td>"
                    }
                    if (value.account_flag == "1") {
                        html = html + "<td class='td_acc'>" + value.account_code + "<br><b style='font-size:8px; color:red;'>" + value.account_remarks + "</b></td>";
                    }
                    else {
                        html = html + "<td class='td_acc'>" + value.account_code + "</td>";
                    }
                    html = html +
                        "<td class='td_email'>" + value.email + "</td>" +
                        "<td>" + value.email_relation + "</td>" +
                        "<td class='td_mob'>" + value.mobile + "</td>" +
                        "<td>" + value.mobile_relation + "</td>";

                    if (value.enable_flag != "0") {
                        if (value.income_range_flag == "1") {
                            html = html + "<td>" + value.income_range + "</td>";
                        }
                        else {
                            html = html + "<td class='SubTotalRow'>" + value.income_range + "</td>";
                        }
                    }
                    else {
                        html = html + "<td>" + value.income_range + "</td>";
                    }
                    if (value.income_range_date == "") {
                        html = html + "<td><p class='paraTextAlignCenter'> - </p></td>";
                    }
                    else {
                        html = html + "<td><p class='paraTextAlignCenter'>" + value.income_range_date + "</p></td>"
                    }

                    if (value.enable_flag != "0" & value.networth_flag == "1") {
                        if (value.networth_flag == "1") {
                            html = html + "<td>" + value.networth + "</td>";
                        }
                        else {
                            html = html + "<td class='SubTotalRow'>" + value.networth + "</td>";
                        }
                    }
                    else {
                        html = html + "<td>" + value.networth + "</td>";
                    }

                    if (value.networth_date == "") {
                        html = html + "<td><p class='paraTextAlignCenter'> - </p></td>"
                    }
                    else {
                        html = html + "<td><p class='paraTextAlignCenter'>" + value.networth_date + "</p></td>"
                    }

                    html = html + "<td>" + value.remarks + "</td>" +
                        "</tr>"
                });
                html = html + "</tbody>";

                $("#dsp_table").append(html);
                $("#dsp_table").show();

                $("#dsp_table_legend").show();
                
                //$("#btnEditRecs").show();
                $("#btnSubmitForm").show();
                

                //$('#dsp_table').dataTable({
                //    "scrollX": true,
                //    "searching": false,
                //    "ordering": false,
                //    "paging": false,
                //    "lengthChange": false,
                //    "info": false,
                //    "initComplete": function (settings, json) {
                        
                //        $('#dsp_table').DataTable().columns.adjust().draw();
                //    }
                //});
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function UpdateKYCDetails() {
    let newEmail, emailChk, emailRela, newMob, MobChk, MobRela, IRchk, IncomeRange, newNW, NW_date;

    if ($("#IDchkEmail").prop('checked')) {

        emailChk = "1";

        newEmail = $("#txtNewEmail").val();
        emailRela = $("#ddlNewEmailRela :selected").val();
    }
    else {
        emailChk = "0";
        newMob = "";
        MobRela = "";
    }

    if ($("#IDchkMob").prop('checked')) {

        MobChk = "1";

        newMob = $("#txtNewMob").val();
        MobRela = $("#ddlNewMobRela :selected").val();
    }
    else {
        MobChk = "0";
        newMob = "";
        MobRela = "";
    }

    if ($("#IDchkIncomeRange").prop('checked')) {

        IRchk = "1";

        IncomeRange = $("#ddlIncomeRangeList :selected").val();
        
        newNW = $("#txt_NW").val();
        NW_date = $("#NW_date").val();

        if (NW_date == "") {
            NW_date = "1999-12-31";
        }
    }
    else {
        IRchk = "0";
        newNW = "";
        IncomeRange = "";
        NW_date = "1999-12-31";
    }

    let formdata = {
        "main_client_id": $('#txtNewEmail').val(),
        "OTP1_Token": $("#verifyOTPtoken").val(),
        "email_update": emailChk,
        "email_address": newEmail,
        "email_relation": emailRela,
        "mobile_update": MobChk,
        "mobile_number": newMob,
        "mobile_relation": MobRela,
        "income_update": IRchk,
        "income_range": IncomeRange,
        "networth": newNW,
        "networth_date": NW_date
    }


    const posturl = "/ClientPortal/Pie/psp_amd_equity_kyc_capture_update_request";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                if (data.sql_status == "Success") {
                    $("#btnSubmitForm").hide();
                    $("#OTP_section_Vali").show();
                    $("#ValidateOTPtoken").val(data.token_no);

                    //$("#dsp_msg_final").append(data.sql_message);
                    $("#dsp_msg_final").html("");
                    $("#dsp_msg_final").append(data.sql_message);
                    $("#dsp_msg_final").show();

                }
                else {
                    alert("Relation Validation failure");
                }
            }
            else {
                alert("Error");
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}


$("#btnEditRecs").click(function () {

    let atLeastOneIsChecked = $('input[name="EditChecks"]:checked').length > 0;
    if (atLeastOneIsChecked) {

        let account_list = "";
        let checks_counter = $('input[name="EditChecks"]:checked').length - 1;

        $('input[name="EditChecks"]:checked').each(function (index, value) {

            if (checks_counter != index) {
                account_list = account_list + $(this).closest('tr').children('.td_acc').text() + ",";
            }
            else {
                account_list = account_list + $(this).closest('tr').children('.td_acc').text();
            }

            $(this).closest('tr').addClass("SubTotalRow");

        });
        
        let formdata = {
            "account_code": account_list,
            "login_id": $("#LoggedInUID").val()
        }

        let posturl = "/ClientPortal/Pie/psp_amd_equity_kyc_initiate_update_request";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {

                $("#verifyOTPtoken").val(data.token_no);

                $("#dsp_msg").html("");
                $("#dsp_msg").append(data.sql_message);
                $("#dsp_msg").show();
                
                $("#btnEditRecs").hide();
                $("#OTP_section").show();
                $("#dsp_table").find("input").attr("disabled", "disabled");
                ResendTimer("OTP1");
                
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        alert("Please select atleast 1 row to edit.");
    }

})

$("#btnSubmitVerifyOTP").click(function () {

    let formdata = {
        "otp": $("#txtVerifyOTP").val(),
        "token_no": $("#verifyOTPtoken").val(),
        "request_stage": "1"
    }

    let posturl = "/ClientPortal/Pie/psp_amd_equity_kyc_authenticate_update_request";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.sql_status == "Success") {
                
                $("#timer1").hide();
                $("#error_msg1").hide();
                $("#OTP_section").hide();
                $("#dsp_msg").hide();
                $("#editable_row").show();
                
            }
            else {
                if (data.sql_message == "OTP Expired") {
                    $("#timer1").hide();
                    $("#error_msg1").html("");
                    $("#error_msg1").append(data.sql_message);
                    $("#error_msg1").append("&nbsp;<a class='alert-link' onclick=\"Resend_OTP('OTP1')\" href='#'>Resend OTP</a>");
                    $("#error_msg1").show();
                }
                else {
                    $("#timer1").hide();
                    $("#error_msg1").html("");
                    $("#error_msg1").append(data.sql_message);
                    $("#error_msg1").show();
                }
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })

})

$("#btnSubmitForm").click(function () {

    if ($("#IDchkEmail").prop('checked') || $("#IDchkMob").prop('checked') || $("#IDchkIncomeRange").prop('checked')) {
        // To check if user has selected atleast 1 field to edit.
        let IsFormValid = false;
        if (($("#txtNewEmail").val() == '' && $("#IDchkEmail").prop('checked'))
            || ($("#IDchkMob").prop('checked') && $("#txtNewMob").val() == '')) {
            // To check if user has selected atleast 1 field to edit and it is not blank.
            alert("Selected field cannot be empty.")
            IsFormValid = false;
        }
        else {
            if ($("#IDchkEmail").prop('checked')) {
                let email_value = $("#txtNewEmail").val();
                let isvalidEmail = isEmail(email_value); //E-Mail validation. Method in Dashboard.js
                if (isvalidEmail) {
                    IsFormValid = true;
                }
                else {
                    alert("Entered email is not in proper format.")
                    IsFormValid = false;
                }
            }
            else {
                if (($("#IDchkIncomeRange").prop('checked') && $("#txt_NW").val() != '' && $("#NW_date").val() == '')) {

                    alert("Please select Networth date.")
                    IsFormValid = false;

                }
                else if (($("#IDchkIncomeRange").prop('checked') && $("#txt_NW").val() == '' && $("#NW_date").val() != '')) {

                    alert("Please enter Networth.")
                    IsFormValid = false;

                }
                else {
                    IsFormValid = true;
                }
            }

        }
        if (IsFormValid) {

            let BothDeclaAreChecked = $('input[name="declaCheks"]:checked').length == 2;

            if (BothDeclaAreChecked) { //To Check both Declaration are checked
                $("#edit_details_container :input").prop("disabled", true);
                $("#decla_div :input").prop("disabled", true);
                UpdateKYCDetails();
                ResendTimer("OTP2");

            }
            else {
                alert("Please confirm both the declarations.")

                AnimateBackgroundRed("#decla_div")
            }
        }
    }
    else {
        alert("Please select at least one field to edit.")
        AnimateBackgroundRed("#edit_details_container")
        
    }
})

$("#btnSubmitValiOTP").click(function () {

    let formdata = {
        "otp": $("#txtValiOTP").val(),
        "token_no": $("#ValidateOTPtoken").val(),
        "request_stage": "3"
    }
    let posturl = "/ClientPortal/Pie/psp_amd_equity_kyc_authenticate_update_request";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.sql_status == "Success") {

                alert(data.sql_message);
                resetEntireForm();
                GetKYCDetails();

            }
            else {
                if (data.sql_message == "OTP Expired") {
                    $("#btnSubmitValiOTP").prop('disabled', false);
                    $("#timer2").hide();
                    $("#error_msg2").html("");
                    $("#error_msg2").append(data.sql_message);
                    $("#error_msg2").append("&nbsp;<a class='alert-link' onclick=\"Resend_OTP('OTP2')\" href='#'>Resend OTP</a>");
                    $("#error_msg2").show();
                }
                else {
                    $("#btnSubmitValiOTP").prop('disabled', false);
                    $("#timer2").hide();
                    $("#error_msg2").html("");
                    $("#error_msg2").append(data.sql_message);
                    $("#error_msg2").show();
                }
            }

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
    $("#btnSubmitValiOTP").prop('disabled', true);
})

function ResendTimer(source) {
    let timeLeft = 5;
    //let elem = document.getElementById('timer');
    $('#timer1').hide();
    $("#timer2").hide();

    $("#error_msg1").hide();
    $("#error_msg2").hide();

    let timerId = setInterval(countdown, 1000);

    function countdown() {
        if (timeLeft == -1) {
            clearTimeout(timerId);
            if (source == "OTP1") {
                $('#timer1').show();
            }
            else if (source == "OTP2") {
                $("#timer2").show();
            }


        } else {
            //elem.innerHTML = timeLeft + ' seconds remaining';
            timeLeft--;
        }
    }
}

function Resend_OTP(soruce) {
    let stage_id, token_num;
    if (soruce == "OTP1") {
        stage_id = "1";
        token_num = $("#verifyOTPtoken").val();
        $('#timer1').hide();
    }
    else if (soruce == "OTP2") {
        stage_id = "3";
        token_num = $("#ValidateOTPtoken").val();
        $('#timer2').hide();
    }
    let formdata = {
        "stage": stage_id,
        "token_no": token_num
    }
    let posturl = "/ClientPortal/Pie/psp_amd_equity_kyc_resend_request";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.sql_status == "Success") {
                if (soruce == "OTP1") {
                    $("#dsp_msg").html("");
                    $("#dsp_msg").append(data.sql_message);
                }
                else if (soruce == "OTP2") {
                    $("#dsp_msg_final").html("");
                    $("#dsp_msg_final").append(data.sql_message);
                }
                
                ResendTimer(soruce);
            }
            else {
                if (soruce == "OTP1") {
                    $('#timer1').show();
                }
                else if (soruce == "OTP2") {
                    $('#timer2').show();
                }
                alert(data.sql_message);
            }
            
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GoToHistory() {
    let url = "/ClientPortal/Pie/KYC_History";
    window.location.href = url;
}