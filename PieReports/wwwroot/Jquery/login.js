$(document).ready(function () {
    $("#UCC_TabBOID").hide();
    $("#UCC_TabFolio").hide();
    $("#Content_Verify_OTP_PAN").hide();
    $("#Content_Verify_OTP_UCC").hide();
    $("#lblInvalid").hide();
    $("#Div_Multi_MobNo").hide();
    $('#div_resend_otp_timer_PAN').hide();
    $('#div_resend_otp_timer_UCC').hide();
    $('#lblInvalidBOID').hide();
    $('#lblInvalidFolio').hide();
    $('#lblInvalidTC').hide();
    $("#otp_msg_UCC").hide();
    $("#otp_msg_PAN").hide();
    
    

    // WCAG 4.1.2 Name, Role, Value — the reveal control is a toggle button.
    // Its pressed state and its accessible name must both track the actual
    // state of the field, otherwise the button lies to screen reader users.
    $("#show_hide_password button, #show_hide_password a").on('click', function (event) {
        event.preventDefault();
        var $toggle = $(this);
        if ($('#show_hide_password input').attr("type") == "text") {
            $('#show_hide_password input').attr('type', 'password');
            $('#show_hide_password i').addClass("fa-eye-slash");
            $('#show_hide_password i').removeClass("fa-eye");
            $toggle.attr('aria-pressed', 'false').attr('aria-label', 'Show password');
            if (window.a11y) { window.a11y.announce('Password hidden.'); }
        } else if ($('#show_hide_password input').attr("type") == "password") {
            $('#show_hide_password input').attr('type', 'text');
            $('#show_hide_password i').removeClass("fa-eye-slash");
            $('#show_hide_password i').addClass("fa-eye");
            $toggle.attr('aria-pressed', 'true').attr('aria-label', 'Hide password');
            if (window.a11y) { window.a11y.announce('Password shown.'); }
        }
    });
});


//Tabs code
// WCAG 4.1.2 Name, Role, Value — jQuery UI Tabs was removed here.
// It builds the tab pattern on the <li> and leaves a focusable <a href> inside
// each one, so every tab shipped a nested interactive control that announced
// as a link but did nothing on activation. It also stamped role="tablist" on
// the UCC radio-button list, claiming a tablist with no tabs in it.
// The tab widget is now the ARIA Authoring Practices pattern implemented on
// real <button role="tab"> elements — see wireTabs() in js/accessibility.js.
//Tabs code

function Active_Tab(btn_val) {
    if (btn_val === 1) {
        $("#Nav_LoginByIDPass").addClass('cst_UL_active_tab').removeClass('cst_UL_tab');

        $("#Nav_LoginByPAN").addClass('cst_UL_tab').removeClass('cst_UL_active_tab');
        $("#Nav_LoginByUCC").addClass('cst_UL_tab').removeClass('cst_UL_active_tab');
    }
    else if (btn_val === 2) {

        $('#txtPAN').val('');

        $("#Nav_LoginByPAN").addClass('cst_UL_active_tab').removeClass('cst_UL_tab');

        $("#Nav_LoginByIDPass").addClass('cst_UL_tab').removeClass('cst_UL_active_tab');
        $("#Nav_LoginByUCC").addClass('cst_UL_tab').removeClass('cst_UL_active_tab');
    }
    else if (btn_val === 3) {

        $('#txt_UCC_TradingCode').val('');
        $('#txt_UCC_Folio').val('');
        $('#txt_UCC_BOID').val('');

        $("#Nav_LoginByUCC").addClass('cst_UL_active_tab').removeClass('cst_UL_tab');

        $("#Nav_LoginByIDPass").addClass('cst_UL_tab').removeClass('cst_UL_active_tab');
        $("#Nav_LoginByPAN").addClass('cst_UL_tab').removeClass('cst_UL_active_tab');
    }
}

function UCC_Tabs(btn_val) {
    if (btn_val === 1) {
        $("#UCC_TabTradingCode").show();

        $('#txt_UCC_TradingCode').val('');

        $("#txt_UCC_TradingCode").prop('disabled', false);
        $("#btnGetOTPTC").prop('disabled', false);

        $("#otp_msg_UCC").hide();
        $("#UCC_TabBOID").hide();
        $("#UCC_TabFolio").hide();
        $("#Content_Verify_OTP_UCC").hide();
    }
    else if (btn_val === 2) {
        $("#UCC_TabFolio").show();

        $('#txt_UCC_Folio').val('');

        $("#txt_UCC_Folio").prop('disabled', false);
        $("#btnGetOTPFolio").prop('disabled', false);

        $("#otp_msg_UCC").hide();
        $("#UCC_TabBOID").hide();
        $("#UCC_TabTradingCode").hide();
        $("#Content_Verify_OTP_UCC").hide();
    }
    else if (btn_val === 3) {
        $("#UCC_TabBOID").show();

        $('#txt_UCC_BOID').val('');

        $("#txt_UCC_BOID").prop('disabled', false);
        $("#btnGetOTPBOID").prop('disabled', false);

        $("#otp_msg_UCC").hide();
        $("#UCC_TabTradingCode").hide();
        $("#UCC_TabFolio").hide();
        $("#Content_Verify_OTP_UCC").hide();
    }
}

function GetOTPMethod() {
    var txtPANCard = $("#txtPAN").val();
    
    if (txtPANCard !== "") {
        if (ValidatePAN(txtPANCard)) {
            $("#btnGetOTP").prop('disabled', true);
            $("#txtPAN").prop('disabled', true);
            PANGetMobileDetails(txtPANCard);
        }
        else {
            $("#lblInvalid").show();
        }
    }
    else {
        $("#lblInvalid").text("Enter your PAN");
        $("#lblInvalid").show();
    }
}

function ValidatePAN(txtPANCard) {

    var regex = /([A-Z]){5}([0-9]){4}([A-Z]){1}$/;
    if (regex.test(txtPANCard)) {
        $("#lblInvalid").hide();
        return true;
    } else {
        return false;
    }
}

function PANGetMobileDetails(txtPANCard) {
    var formdata = {
        "pan_number": txtPANCard,
        "Mob_No": "",
        "Flag": "P"
    }
    var posturl = "/ClientPortal/Login/GetMobileDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            var html = "";
            
            if (data.length > 0) {
                if (data.length != 1) {
                    
                    for (var i = 0; i < data.length; i++) {
                        if (i == 0) {
                            html = html + "<input type='radio' checked='checked' id='MobNo" + i + "' name='otp_moblist' value=" + data[i].mobile + "><label for='MobNo" + i + "'> " + data[i].masked_mobile + "</label><br>";
                        }
                        else {
                            html = html + "<input type='radio' id='MobNo" + i + "' name='otp_moblist' value=" + data[i].mobile + "><label for='MobNo" + i + "'> " + data[i].masked_mobile + "</label><br>";
                        }
                    }

                    $("#otp_msg_PAN").text(data[0].sql_message);
                    $("#otp_msg_PAN").show();
                    $("#Div_Mobile_List").append(html);
                    $("#Div_Multi_MobNo").show();
                }
                else {
                    $("#txtOTPTokenPAN").val(data[0].token_no);
                    $("#otp_msg_PAN").text(data[0].sql_message);
                    $("#otp_msg_PAN").show();

                    if (data[0].sql_status == 'Fail') {
                        $("#Content_Verify_OTP_PAN").hide();
                        $("#btnGetOTP").prop('disabled', false);
                        $("#txtPAN").prop('disabled', false);
                    }
                    else {
                        $("#Content_Verify_OTP_PAN").show();
                        ResendTimer('PAN');
                    }
                }
                
            } 
        },
        error:
            function (jqXHR, textStatus, errorThrown) {
                
            },
        complete: function () {

        }
    })
}

function SendOTPtoMethod() {
    let selected_mob = $('input[name="otp_moblist"]:checked').val();
    let txtPANCard = $("#txtPAN").val();

    let formdata = {
        "pan_number": txtPANCard,
        "Mob_No": selected_mob,
        "Flag": "P"
    }
    let posturl = "/ClientPortal/Login/GetMobileDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            let html = "";
            if (data.length > 0) {
                $("#Div_Multi_MobNo").hide();
                $("#otp_msg_PAN").text(data[0].sql_message);
                $("#txtOTPTokenPAN").val(data[0].token_no);
                $("#Content_Verify_OTP_PAN").show();
                ResendTimer('PAN');
            }
            else {

            }
        },
        error:
            function (jqXHR, textStatus, errorThrown) {

            },
        complete: function () {

        }
    })
}

function UCCGetOTPMethod(asset) {
    var ucc_value = "";
    var post_ajax = false;

    if (asset == "1") {
        ucc_value = $("#txt_UCC_TradingCode").val();

        if (ucc_value != '') {
            post_ajax = true;
            $("#txt_UCC_TradingCode").prop('disabled', true);
            $("#btnGetOTPTC").prop('disabled', true);
            $('#lblInvalidTC').hide();
        }
        else {
            $('#lblInvalidTC').show();
        }

    }
    else if (asset == "4") {
        ucc_value = $("#txt_UCC_Folio").val();
        if (ucc_value != '') {
            post_ajax = true;
            $("#txt_UCC_Folio").prop('disabled', true);
            $("#btnGetOTPFolio").prop('disabled', true);

            $('#lblInvalidFolio').hide();
            
        }
        else {
            $('#lblInvalidFolio').show();
        }

        
    }
    else if (asset == "6") {
        ucc_value = $("#txt_UCC_BOID").val();

        if (ucc_value != '') {
            post_ajax = true;
            $("#txt_UCC_BOID").prop('disabled', true);
            $("#btnGetOTPBOID").prop('disabled', true);

            $('#lblInvalidBOID').hide();
        }
        else {
            $('#lblInvalidBOID').show();
        }
    }

    var formdata = {
        "pan_number": "",
        "Mob_No": "",
        "Flag": "U",
        "JSON_Asset_Class": asset,
        "UCC_Value": ucc_value

    }
    if (post_ajax == true) {
        var posturl = "/ClientPortal/Login/GetMobileDetails";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {
                console.log(data);
                if (data.length > 0) {
                    if (data[0].sql_status != 'Fail') {
                        $("#otp_msg_UCC").addClass('alert-success').removeClass('alert-danger');
                        $("#otp_msg_UCC").text(data[0].sql_message);
                        $("#otp_msg_UCC").show();
                        $("#Content_Verify_OTP_UCC").show();
                        $("#txtOTPTokenUCC").val(data[0].token_no);
                        ResendTimer('UCC');
                    }
                    else {
                        $("#otp_msg_UCC").addClass('alert-danger').removeClass('alert-success');
                        $("#otp_msg_UCC").text(data[0].sql_message);
                        $("#otp_msg_UCC").show();
                        $("#Content_Verify_OTP_UCC").hide();

                        $("#txt_UCC_TradingCode").prop('disabled', false);
                        $("#btnGetOTPTC").prop('disabled', false);
                        $("#txt_UCC_Folio").prop('disabled', false);
                        $("#btnGetOTPFolio").prop('disabled', false);
                        $("#txt_UCC_BOID").prop('disabled', false);
                        $("#btnGetOTPBOID").prop('disabled', false);
                    }
                }
            },
            error:
                function (jqXHR, textStatus, errorThrown) {

                },
            complete: function () {

            }
        })
    }
}

function ResendTimer(source) {
    var timeLeft = 5;
    //var elem = document.getElementById('timer');
    
    var timerId = setInterval(countdown, 1000);

    function countdown() {
        if (timeLeft == -1) {
            clearTimeout(timerId);
            if (source == 'PAN') {
                $('#div_resend_otp_timer_PAN').show();
            }
            else if (source == 'UCC') {
                $("#div_resend_otp_timer_UCC").show();
            }

            
        } else {
            //elem.innerHTML = timeLeft + ' seconds remaining';
            timeLeft--;
        }
    }
}

function Resend_OTP(source) {

    var otp_token;

    if (source == 'PAN')
    {
        otp_token = $('#txtOTPTokenPAN').val()
        $('#div_resend_otp_timer_PAN').hide()
        
    }
    else if (source == 'UCC')
    {
        otp_token = $('#txtOTPTokenUCC').val()
        $('#div_resend_otp_timer_UCC').hide()
    }

    var formdata = {
        "token_no": otp_token
    }
    var posturl = "/ClientPortal/Login/ResendMobileOTP";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.length > 0) {
                //$("#Div_Multi_MobNo").hide();
                //$("#otp_msg_PAN").text(data[0].sql_message);
                
                if (source == 'PAN') {
                    $("#txtOTPTokenPAN").val(data[0].token_no);

                    $("#otp_msg_PAN").text(data[0].sql_message);
                    $("#otp_msg_PAN").addClass('alert-success').removeClass('alert-danger');
                    $("#otp_msg_PAN").show();

                    $("#Content_Verify_OTP_PAN").show();
                    ResendTimer('PAN');

                }
                else if (source == 'UCC') {
                    $("#txtOTPTokenUCC").val(data[0].token_no);

                    $("#otp_msg_UCC").text(data[0].sql_message);
                    $("#otp_msg_UCC").addClass('alert-success').removeClass('alert-danger');
                    $("#otp_msg_UCC").show();

                    $("#Content_Verify_OTP_UCC").show();
                    ResendTimer('UCC');
                }
                
            }
            else {

            }
        },
        error:
            function (jqXHR, textStatus, errorThrown) {

            },
        complete: function () {

        }
    })
}

$("#img_captcha").click(function () {
    resetCaptchaImage();
});

function resetCaptchaImage() {
    d = new Date();
    $("#img_captcha").attr("src", "/ClientPortal/Pie/GetCaptchaImage/" + d.getTime());
}