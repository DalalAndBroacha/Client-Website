$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    GetDetails();

});

//$(".ValiInp").keyup(function (event) {
//    console.log($(this).val())
//    console.log(/[^a-zA-Z0-9 -|]/.test($(this).val()))
//    //if (/[^a-zA-Z0-9 -|]/.test($(this).val())) {
//    //    event.preventDefault();
//    //}
//});

$("#PassTog").change(function () {

    ResetValis();

    if ($(this).prop("checked")) {
        $("#divChangePass").show();
        $("#PassMsg").show();
        $("#btnUpdate").prop('disabled', true);

    }
    else {
        $("#divChangePass").hide();
        $("#PassMsg").hide();
        $("#btnUpdate").prop('disabled', false);
    }
})


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
        $("#txtConfPass").prop('disabled', false);
    }
    else {
        $("#txtConfPass").prop('disabled', true);
    }
})


$("#txtConfPass").keyup(function () {
    var oldpass = $("#txtNewPass").val();
    var newpass = $("#txtConfPass").val();

    if (oldpass == newpass) {
        $("#matchConfPass").show();
        $("#matchConfPass").html("Passwords match.");
        $("#matchConfPass").addClass("pass_valid").removeClass("pass_invalid");


        $("#btnUpdate").prop('disabled', false);
    }
    else {
        $("#matchConfPass").show();
        $("#matchConfPass").html("Passwords do not match.");
        $("#matchConfPass").addClass("pass_invalid").removeClass("pass_valid");
        $("#btnUpdate").prop('disabled', true);
    }
})

function ResetValis() {

    $("#pass_letters").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_capitals").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_number").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_length").addClass("pass_invalid").removeClass("pass_valid");
    $("#pass_sym").addClass("pass_invalid").removeClass("pass_valid");

    $("#txtNewPass").val('');
    $("#matchConfPass").hide();

    $("#txtConfPass").val('');
    $("#txtConfPass").prop('disabled', true);
}


function GetDetails() {
    let formdata = {
        "LoginId": $("#LoggedInUserHash").val()
    }

    const posturl = "/ClientPortal/Pie/psp_dsp_profile_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data != null) {

                $("#txtUID").val(data.display_id);
                $("#txtDspName").val(data.family_name);
                $("#txtNickName").val(data.nickname);
                $("#txtContactNum").val(data.mobile_No);
                $("#txtEmail").val(data.email_Id);

                $('#chkSendMail').prop('checked', false);

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

    var passwordFlag = "0";

    if ($("#PassTog").prop('checked')) { //To reset password
        passwordFlag = "1";
    }
    else {
        passwordFlag = "0";
    }



    var formdata = {
        family_name: $("#txtDspName").val(),
        contact_No: $("#txtContactNum").val(),
        email_Id: $("#txtEmail").val(),
        nickname: $("#txtNickName").val(),
        passFlag: passwordFlag,
        newPassword: $("#txtNewPass").val()
    }

    var posturl = "/ClientPortal/Pie/psp_amd_profile_details";
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
