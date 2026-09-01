$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    //GetDetails();
    $("#divResetPass").hide();

});

$("#ddlFamilyList").change(function () {

    GetDetails();

});
$("#eyeTog").on('click', function (event) {
    event.preventDefault();
    if ($('#txtNewPass').attr("type") == "text") {
        $('#txtNewPass').attr('type', 'password');
        $('#eyeTog').addClass("fa-eye-slash");
        $('#eyeTog').removeClass("fa-eye");
    } else if ($('#txtNewPass').attr("type") == "password") {
        $('#txtNewPass').attr('type', 'text');
        $('#eyeTog').removeClass("fa-eye-slash");
        $('#eyeTog').addClass("fa-eye");
    }
});

$("#sys_pass").on('click', function (event) {

    $('#txtNewPass').prop('readonly', true);
    $("#btnUpdate").prop('disabled', false);
    $("#PassMsg").hide();

    let posturl = "/ClientPortal/Pie/System_password";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data != null) {
             
                $("#txtNewPass").val(data);
            }
            else {
                alert("err");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
});
$("#usr_pass").on('click', function (event) {

    $('#txtNewPass').prop('readonly', false);

    $("#txtNewPass").val('');

    $("#divChangePass").show();
    $("#PassMsg").show();
    $("#btnUpdate").prop('disabled', true);

});

$("#txtNewPass").keyup(function () {
    var myInput = document.getElementById("txtNewPass");

    var lowcase, upcase, numbs, leng, symflag = 0;

    if (myInput.value != '') {
        // Validate lowercase letters
        var PlowerCaseLetters = /[a-z]/g;
        if (myInput.value.match(PlowerCaseLetters)) {
            $("#pass_letters").addClass("pass_valid").removeClass("pass_invalid");
            lowcase = 1;
        } else {
            $("#pass_letters").addClass("pass_invalid").removeClass("pass_valid");
            lowcase = 0;
        }

        // Validate capital letters
        var upperCaseLetters = /[A-Z]/g;
        if (myInput.value.match(upperCaseLetters)) {
            $("#pass_capitals").addClass("pass_valid").removeClass("pass_invalid");
            upcase = 1;
        } else {
            $("#pass_capitals").addClass("pass_invalid").removeClass("pass_valid");
            upcase = 0;
        }

        // Validate numbers
        var Pnumbers = /[0-9]/g;
        if (myInput.value.match(Pnumbers)) {
            $("#pass_number").addClass("pass_valid").removeClass("pass_invalid");
            numbs = 1;
        } else {
            $("#pass_number").addClass("pass_invalid").removeClass("pass_valid");
            numbs = 0;
        }

        // Validate length
        if (myInput.value.length >= 8) {
            $("#pass_length").addClass("pass_valid").removeClass("pass_invalid");
            leng = 1;
        } else {
            $("#pass_length").addClass("pass_invalid").removeClass("pass_valid");
            leng = 0;
        }

        var symbol = /(?=.*[!@#$%^&*])/;
        if (myInput.value.match(symbol)) {
            $("#pass_sym").addClass("pass_valid").removeClass("pass_invalid");
            symflag = 1;
        } else {
            $("#pass_sym").addClass("pass_invalid").removeClass("pass_valid");
            symflag = 0;
        }
    }
    else {
        ResetValis();
    }
    if (upcase == 1 && lowcase == 1 && leng == 1 && numbs == 1 && symflag == 1) {
        $("#btnUpdate").prop('disabled', false);
    }
    else {
        $("#btnUpdate").prop('disabled', true);
    }

    //if (upcase == 1 && lowcase == 1 && leng == 1 && numbs == 1 && symflag == 1) {
    //    $("#txtConfPass").prop('disabled', false);
    //}
    //else {
    //    $("#txtConfPass").prop('disabled', true);
//}
});

$("#PassTog").change(function () {

    ResetValis();

    if ($(this).prop("checked")) {
        $("#divResetPass").show();
        //$("#PassMsg").show();
        //$("#btnUpdate").prop('disabled', true);

        let posturl = "/ClientPortal/Pie/System_password";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(),
            success: function (data) {
                if (data != null) {
                    $('#txtNewPass').prop('readonly', true);
                    $("#txtNewPass").val(data);
                }
                else {
                    alert("err");
                }
            },
            error: function (xhr, err) {
                alert("error")
            }
        })

    }
    else {
        $("#divResetPass").hide();
        //$("#PassMsg").hide();
        $("#btnUpdate").prop('disabled', false);
    }
})
function ResetValis() {

    $("#pass_letters").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_capitals").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_number").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_length").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_sym").addClass("pass_invalid").removeClass("pass_valid");

    $("#txtNewPass").val('');
    //$("#matchConfPass").hide();

    //$("#txtConfPass").val('');
    //$("#txtConfPass").prop('disabled', true);
}

function GetDetails() {
    var formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "family_id": $('#ddlFamilyList :selected').val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_single_family_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                $("#txtUID").val(data.display_id);
                $("#txtDspName").val(data.family_name);
                $("#txtContactNum").val(data.mobile_No);
                $("#txtEmail").val(data.email_Id);

                $('#chkSendMail').prop('checked', false);
                $('#chkchangepass').prop('checked', false);

                $("#dsp_div").show();
                $("#updateSucc").hide();
                $("#norecords").hide();
                
            }
            else {

                $("#norecords").show()
                $("#dsp_div").hide()
                $("#updateSucc").hide();
                
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function SaveDetails() {

    let passwordFlag = "0";
    let emailFlag = "0";
    let passtype = "U"

    if ($('#PassTog').prop("checked") && $('#txtNewPass')!= '') {
        passwordFlag = "1"
    }
    else {
        passwordFlag = "0"
    }
    if ($('#PassTog').prop("checked") && $("#chkchangepass").prop('checked')) {
        passtype = "S"
    }
    else {
        passtype = "U"
    }
    if ($('#PassTog').prop("checked") && $("#chkSendMail").prop('checked')) {
        emailFlag = "1"
    }
    else {
        emailFlag = "0"
    }
/*    console.log('passwordFlag = ' + passwordFlag + "---" + 'emailFlag = ' + emailFlag + "---" + 'passtype = ' + passtype)*/

    let formdata = {
        Login_Id: $("#LoggedInUID").val(),
        family_name: $("#txtDspName").val(),
        family_id: $('#ddlFamilyList :selected').val(),
        contact_No: $("#txtContactNum").val(),
        email_Id: $("#txtEmail").val(),
        passFlag: passwordFlag,
        newPassword: $("#txtNewPass").val(),
        send_mail: emailFlag,
        passtype: passtype

    }

    let posturl = "/ClientPortal/Pie/psp_amd_family_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.sql_status == "Success") {

                $("#updateSucc").show();
                $("#norecords").hide()
                $("#dsp_div").hide()
            }
            else {
                alert(data.sql_msg);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
    
}
