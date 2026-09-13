$(document).ready(function () {  
   /* $('.select2').select2()*/
    //var regexname = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$/;
    //$('#txtConfirmPassword').on('keyup', function () {
    //    if (!$(this).val().match(regexname)) {
    //        $(".msg").text("Incorrect Format");
    //    }
    //    else {
    //        $(".msg").text("");
    //    }
    //});

    //$("#btnchange").prop('disabled', true);
    //    var myInput = document.getElementById("txtNewPassword");
    //    var letter = document.getElementById("letter");
    //    var capital = document.getElementById("capital");
    //    var number = document.getElementById("number");
    //    var length = document.getElementById("length");
    //    var sym = document.getElementById("sym");
        // When the user clicks on the password field, show the message box onkeyup keypress
    //myInput.onfocus = function () {
    //        document.getElementById("message").style.display = "block";
    //}

    // When the user clicks outside of the password field, hide the message box
    //myInput.onblur = function () {
    //        document.getElementById("message").style.display = "none";
    //}

    // When the user starts to type something inside the password field
    //myInput.onkeyup = function () {
    //    // Validate lowercase letters
    //    var lowerCaseLetters = /[a-z]/g;
    //    if (myInput.value.match(lowerCaseLetters)) {
    //        letter.classList.remove("invalid");
    //        letter.classList.add("valid");
    //    } else {
    //        letter.classList.remove("valid");
    //        letter.classList.add("invalid");
    //    }

    //    // Validate capital letters
    //    var upperCaseLetters = /[A-Z]/g;
    //    if (myInput.value.match(upperCaseLetters)) {
    //        capital.classList.remove("invalid");
    //        capital.classList.add("valid");
    //    } else {
    //        capital.classList.remove("valid");
    //        capital.classList.add("invalid");
    //    }

    //    // Validate numbers
    //    var numbers = /[0-9]/g;
    //    if (myInput.value.match(numbers)) {
    //        number.classList.remove("invalid");
    //        number.classList.add("valid");
    //    } else {
    //        number.classList.remove("valid");
    //        number.classList.add("invalid");
    //    }

    //    // Validate length
    //    if (myInput.value.length >= 8) {
    //        length.classList.remove("invalid");
    //        length.classList.add("valid");
    //    } else {
    //        length.classList.remove("valid");
    //        length.classList.add("invalid");
    //    }

    //    var symbol = /(?=.*[!@#$%^&*])/;
    //    if (myInput.value.match(symbol)) {
    //        sym.classList.remove("invalid");
    //    sym.classList.add("valid");
    //     } else {
    //        sym.classList.remove("valid");
    //    sym.classList.add("invalid");
    //    }
    //}

    //function checkPasswordMatch() {
    //    var password = $("#txtNewPassword").val();
    //    var confirmPassword = $("#txtConfirmPassword").val();
    //    if (password != confirmPassword) {
    //        $("#CheckPasswordMatch").html("New & Confirm Passwords do not match.");
    //        $("#CheckPasswordMatch").css("color", "red");
    //        $("#btnsub").prop('disabled', true); btnchange
    //        $("#btnchange").prop('disabled', true);
    //    }
    //    else {
    //        $("#CheckPasswordMatch").html("New & Confirm Passwords match.");
    //        $("#CheckPasswordMatch").css("color", "green");
    //        $("#btnsub").prop('disabled', false);
    //        $("#btnchange").prop('disabled', false);
    //    }
    //}
   
    //    $("#txtConfirmPassword").keyup(checkPasswordMatch);


    //if ($("#spnmsg").text() == "Password Updated successfully") {
    //    var counter = 0;
    //    var interval = setInterval(function () {
    //        counter++;
    //        // Display 'counter' wherever you want to display it.
    //        if (counter == 3) {
    //            // Display a login box
    //            window.location = "Index";  
    //        }
    //    }, 1000);
    //}

    //$("#btnsub").on("click", function () {
    //    if ($("#txtNewPassword").val() == "") {
    //        $("#CheckPasswordMatch").html("Please enter the detail");
    //      //  $("#CheckPasswordMatch").css("color", "green");
    //        return false;
    //    }
    //    if ($("#txtConfirmPassword").val() == "") {
    //        $("#CheckPasswordMatch").html("Please enter the detail");
    //       // $("#CheckPasswordMatch").css("color", "green");
    //        return false;
    //    }
    //    var formdata = {
    //        "NewPassword": $("#txtNewPassword").val(),
    //        "ConfirmPassword": $("#txtConfirmPassword").val(),
           
    //    };
    //    checkPasswordMatch();
    //    var canvasFields = new Array();
    //    canvasFields.push(formdata);
    //    var myJsonString = JSON.stringify(canvasFields);
    //    $.ajax({
    //        type: "post",
    //        url: "/ClientPortal/Login/DashChangePassword",            
    //        contentType: "application/json",
    //        data: JSON.stringify(formdata),
    //        success: function (data) {
    //            if (data.sql_message = "Password Updated successfully") {
    //                $("#spnmsg").html(data.sql_message);
    //                var counter = 0;
    //                var interval = setInterval(function () {
    //                    counter++;
    //                    // Display 'counter' wherever you want to display it.
    //                    if (counter == 3) {
    //                        // Display a login box
    //                        window.location = "/ClientPortal/Login/Index";
    //                    }
    //                }, 1000);
    //            }
              
    //        },
    //        error: function (xhr, err) {

    //        }
    //    })
    //});
});



$("#txtChangeNewPassword").keyup(function () {
    var ChangePassmyInput = document.getElementById("txtChangeNewPassword");
    

    var lowcase, upcase, numbs, leng, symflag = 0;

    if (ChangePassmyInput.value != '') {
        // Validate lowercase letters
        var PlowerCaseLetters = /[a-z]/g;
        if (ChangePassmyInput.value.match(PlowerCaseLetters)) {
            $("#ChangePassletter").addClass("valid").removeClass("invalid");
            lowcase = 1;
        } else {
            $("#ChangePassletter").addClass("invalid").removeClass("valid");
            lowcase = 0;
        }

        // Validate capital letters
        var upperCaseLetters = /[A-Z]/g;
        if (ChangePassmyInput.value.match(upperCaseLetters)) {
            $("#ChangePasscapital").addClass("valid").removeClass("invalid");
            upcase = 1;
        } else {
            $("#ChangePasscapital").addClass("invalid").removeClass("valid");
            upcase = 0;
        }

        // Validate numbers
        var Pnumbers = /[0-9]/g;
        if (ChangePassmyInput.value.match(Pnumbers)) {
            $("#ChangePassnumber").addClass("valid").removeClass("invalid");
            numbs = 1;
        } else {
            $("#ChangePassnumber").addClass("invalid").removeClass("valid");
            numbs = 0;
        }

        // Validate length
        if (ChangePassmyInput.value.length >= 8) {
            $("#ChangePasslength").addClass("valid").removeClass("invalid");
            leng = 1;
        } else {
            $("#ChangePasslength").addClass("invalid").removeClass("valid");
            leng = 0;
        }

        var symbol = /(?=.*[!@#$%^&*])/;
        if (ChangePassmyInput.value.match(symbol)) {
            $("#ChangePasssym").addClass("valid").removeClass("invalid");
            symflag = 1;
        } else {
            $("#ChangePasssym").addClass("invalid").removeClass("valid");
            symflag = 0;
        }
    }
    else {
        ResetValis();
    }

    if (upcase == 1 && lowcase == 1 && leng == 1 && numbs == 1 && symflag == 1) {
        $("#txtConfirmNewPassword").prop('disabled', false);
    }
    else {
        $("#txtConfirmNewPassword").prop('disabled', true);
    }
})


$("#txtConfirmNewPassword").keyup(function () {
    var oldpass = $("#txtChangeNewPassword").val();
    var newpass = $("#txtConfirmNewPassword").val();

    if (oldpass == newpass) {
        $("#changePassMatchConfPass").show();
        $("#changePassMatchConfPass").html("Passwords match.");
        $("#changePassMatchConfPass").addClass("valid").removeClass("invalid");


        $("#btnchange").prop('disabled', false);
    }
    else {
        $("#changePassMatchConfPass").show();
        $("#changePassMatchConfPass").html("Passwords do not match.");
        $("#changePassMatchConfPass").addClass("invalid").removeClass("valid");
        $("#btnchange").prop('disabled', true);
    }
})


function ResetValis() {

    $("#pass_letters").addClass("invalid").removeClass("valid");
    $("#pass_capitals").addClass("invalid").removeClass("valid");
    $("#pass_number").addClass("invalid").removeClass("valid");
    $("#pass_length").addClass("invalid").removeClass("valid");
    $("#pass_sym").addClass("invalid").removeClass("valid");

    $("#txtConfirmNewPassword").val('');
    $("#txtConfirmNewPassword").prop('disabled', true);
}


// WCAG 4.1.2 Name, Role, Value — the reveal control is a toggle button, so
// its pressed state and accessible name must track the field's actual state.
$("#changePassNewPassEye").on('click', function (event) {
    event.preventDefault();
    var $toggle = $(this);
    if ($('#txtChangeNewPassword').attr("type") == "text") {
        $('#txtChangeNewPassword').attr('type', 'password');
        $('#changePassNewPassEye i').addClass("fa-eye-slash");
        $('#changePassNewPassEye i').removeClass("fa-eye");
        $toggle.attr('aria-pressed', 'false').attr('aria-label', 'Show password');
        if (window.a11y) { window.a11y.announce('Password hidden.'); }
    } else if ($('#txtChangeNewPassword').attr("type") == "password") {
        $('#txtChangeNewPassword').attr('type', 'text');
        $('#changePassNewPassEye i').removeClass("fa-eye-slash");
        $('#changePassNewPassEye i').addClass("fa-eye");
        $toggle.attr('aria-pressed', 'true').attr('aria-label', 'Hide password');
        if (window.a11y) { window.a11y.announce('Password shown.'); }
    }
});