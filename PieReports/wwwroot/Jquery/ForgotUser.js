$(document).ready(function () {
    $("#seUserNAme").hide();

   // var regexnamem = /^[7-9][0-9]{9}$/;
    var regexnamem = "^[7-9][0-9]{7}|[7-9][0-9]{12}$";
    $('#txtmobile').on('keyup', function () {
        if (!$(this).val().match(regexnamem)) {
            $(".msgg").text("Incorrect Format");
        }
        else {
            $(".msgg").text("");
        }
    });

    var regexname = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    $('#txtemail').on('keyup', function () {
        if (!$(this).val().match(regexname)) {
            $(".msg").text("Incorrect Format");
        }
        else {
            $(".msg").text("");
        }
    });

    $("#otp-screen .form-control").keyup(function () {
        if (this.value.length == 0) {
            $(this).blur().parent().prev().children('.form-control').focus();
        }
        else if (this.value.length == this.maxLength) {
            $(this).blur().parent().next().children('.form-control').focus();
        }
    });

    $("#btnVerify").on("click", function () {
        var formdata = {
            "otp1": $("#otp1").val(),
            "otp2": $("#otp2").val(),
            "otp3": $("#otp3").val(),
            "otp4": $("#otp4").val(),
            "otp5": $("#otp5").val(),
            "otp6": $("#otp6").val(),
        };
        var canvasFields = new Array();
        canvasFields.push(formdata);

        var myJsonString = JSON.stringify(canvasFields);
        $.ajax({
            url: "otpVerify",
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {

                if (data.sql_status == "Success") {
                    $("#divOtpHide").hide();
                    $("#seUserNAme").show()
                } else {
                    $("#myform").html(data.sql_message);
                }
            },
            error: function (xhr, err) {

            }
        })
    });
    $("#btnusernameForgote").on("click", function () {
        var formdata = {
            "new_username": $("#txtnew_username").val(),
             
        };
        var canvasFields = new Array();
        canvasFields.push(formdata);

        var myJsonString = JSON.stringify(canvasFields);
        $.ajax({
            url: "/ClientPortal/Login/ForgotUser",
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {
                $("#spnmsguser").html(data.sql_message)
                if (data.sql_message == "Nickname Updated successfully") {
                    // WCAG 2.2.1 Timing Adjustable — this used to redirect to the
                    // sign-in page three seconds after the message appeared, which
                    // is a timed change of context the user cannot stop, and too
                    // short for the message to be read or announced. The user now
                    // moves on when ready.
                    $("#spnmsguser").append(
                        ' <a href="/ClientPortal/Login/Index">Sign in with your new username</a>');
                } else {
                    //$("#myform").html(data.sql_message);
                }
            },
            error: function (xhr, err) {

            }
        })
    });

    $("#htdResendOTP").on("click", function () {
        var formdata = {
            "email_id": $("#txtemailotp").val(),
            "mobile_number": $("#txtmobileotp").val(),
            "account_code": $("#txtAccountotp").val(),
            "pan_number": $("#txtPanotp").val()
        };
        var canvasFields = new Array();
        canvasFields.push(formdata);

        var myJsonString = JSON.stringify(canvasFields);
        $.ajax({
            url: "ResentAccountOtp",
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {
                if (data.sql_status == "Success") {
                    $("#myform").html("OTP RE-Send");
                }
            },
            error: function (xhr, err) {

            }
        })
    });
});
