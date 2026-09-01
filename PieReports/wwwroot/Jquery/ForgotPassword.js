$(document).ready(function () {

    var regexname = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    $('#txtEmailId').on('keyup', function () {
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
                    window.location = "ChangePassword";
                } else {
                    $("#myform").html(data.sql_message);
                }
            },
            error: function (xhr, err) {
                
            }
        })
    });

    $("#htdResendOTP").on("click", function () {
        var formdata = {
            "Username": $("#txttxtUsername").val(),
            "EmailId": $("#txttxtEmailId").val() 
        };
        var canvasFields = new Array();
        canvasFields.push(formdata);

        var myJsonString = JSON.stringify(canvasFields);
        $.ajax({
            url: "ResentForgotPassword",
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
