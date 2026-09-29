let client_count, ipo_cut_off
$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    GetClientList();
});

// [a-zA-Z0-9.\-_]{2,256}@[a-zA-Z]{2,64}

$("#inpCutOff").change(function () {

    if ($(this).prop("checked")) {

        $("#inpPrice").val(ipo_cut_off);
        $("#inpPrice").prop('disabled', true);
    }
    else {
        $("#inpPrice").prop('disabled', false);
    }
    calcTotalAmount();
});

$("#inpPrice").keyup(function () {

    inpLot = parseFloat($("#inpLot").val() == '' ? "0" : $("#inpLot").val());

    if (inpLot > 0) {

        calcTotalAmount();
    }
});
function CalcLots() {

    let inpLot, lotSize, ttlQty;

    inpLot = parseFloat($("#inpLot").val() == '' ? "0" : $("#inpLot").val());

    lotSize = parseFloat($("#inplotSize").text());


    if (inpLot >= 0) {

        ttlQty = inpLot * lotSize

        $("#inpQty").val(ttlQty);

        calcTotalAmount();
    }
    else {
        alert("Lot size cannot be negative.")
        $("#inpLot").val("1");
    }
}

$("#ddlFamilyList").change(function () {

    GetClientList();

});

function calcTotalAmount() {
    let inpPrice, inpQty, tltAmt = 0.0

    inpPrice = parseFloat($("#inpPrice").val() == '' ? "0" : $("#inpPrice").val());
    inpQty = parseFloat($("#inpQty").val() == '' ? "0" : $("#inpQty").val());

    minPrice = parseFloat($("#pTxtIssuePrice").text());
    maxPrice = parseFloat($("#pTxtCutOffPrice").text());

    tltAmt = inpPrice * inpQty;

    if (minPrice <= inpPrice && inpPrice <= maxPrice) {
        if (tltAmt > 200000.00) {
            alert("Amount should not be more than 2,00,000/- \nPlease reduce the number of lots or adjust the price.");
            $("#inpPrice").val(minPrice);
            $("#inpLot").val("1");
        }
        else {
            $("#inpTotalAmt").text(numberWithCommas(tltAmt.toFixed(2)));
        }
    }
    else {
        alert("Price should be between " + minPrice + " & " + maxPrice);
        $("#inpPrice").val(minPrice);
        $("#inpLot").val("1");
    }
}

function lockUnlockInputs(action) {
    if (action == "Enable") {

        $("#ddlClientList").prop('disabled', false);
        $("#inpLot").prop('disabled', false);
        $("#inpPrice").prop('disabled', false);
        $("#inpUPI").prop('disabled', false);
        $("#chkDecla").prop('disabled', false);
        $("#btnSubmitBid").prop('disabled', false);
        $("#inpCutOff").prop('disabled', false);

        
    }
    else if (action == "Disable") {

        $("#ddlClientList").prop('disabled', true);
        $("#inpLot").prop('disabled', true);
        $("#inpPrice").prop('disabled', true);
        $("#inpUPI").prop('disabled', true);
        $("#chkDecla").prop('disabled', true);
        $("#btnSubmitBid").prop('disabled', true);
        $("#inpCutOff").prop('disabled', true);
    }
}

function dspIpoDetails(ISIN, Name) {

    if (client_count > 0) {

        GetIpoDetails(ISIN, Name);
    }
    else {
        alert("Unable to apply since there are no clients present in the family.\nPlease select new family.")
        $("#divBiddingSection").hide();
    }

}

function GetClientList() {
    var formdata = {
        "family_token": $('#ddlFamilyList :selected').val()
    }

    var posturl = "/ClientPortal/IPO/psp_dsp_ipo_clients_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            client_count = data.length;
            if (client_count > 0) {
                $("#ddlClientList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    /*html = html + "<option value=" + value.boid + ">" + value.dsp_client_name + "</option>";*/
                    html = html + "<option value='{\"mcid\":\"" + value.main_client_id + "\",\"BOID\":\"" + value.boid + "\"}'>" + value.dsp_client_name + "</option>";
                });

                $("#ddlClientList").append(html);
                $("#ddlClientList").attr("disabled", false);
            }
            else {

                $("#divBiddingSection").hide();

                $("#ddlClientList").html("");
                $("#ddlClientList").append("<option value=0>No Data</option>");
                $("#ddlClientList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

function GetIpoDetails(ISIN, Name) {
    var formdata = {
        "isin": ISIN,
        "name": Name
    }

    var posturl = "/ClientPortal/IPO/GetIPODetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            
            if (data != null) {
                $("#divBiddingSection").show();

                $("#txtIpoName").text(data.name);
                $("#pTxtISIN").text(data.isin);
                $("#pTxtSymbol").text(data.symbol);
                $("#inplotSize").text(data.strMinbidqty);

                $("#pTxtOpenIssue").text(data.strOpenDateTime);
                $("#pTxtCloseIssue").text(data.strCloseDateTime);

                $("#inpPrice").val(data.strFloorprice)
                $("#pTxtIssuePrice").text(data.strFloorprice);
                $("#pTxtCutOffPrice").text(data.strCeilingprice);
                ipo_cut_off = data.strCeilingprice;
                

                $("#pTxtTickSize").text(data.tickprice);
                $("#pTxtLotSize").text(data.strMinbidqty);
                
                $("#pTxtMinVal").text(data.strMinvalue);

                lockUnlockInputs("Enable")
            }
            else {

                $("#divBiddingSection").hide();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

function InitiateBid() {

    let upiId = $("#inpUPI").val();
    const regex = /[a-zA-Z0-9.\-_]{2,256}@[a-zA-Z]{2,64}/;

    let ttlamt = parseFloat($("#inpTotalAmt").text().replace(/,/g, ''));
    let minamt = parseFloat($("#pTxtMinVal").text().replace(/,/g, ''));

    let cliListVal = JSON.parse($('#ddlClientList :selected').val());
    let boid = cliListVal.BOID;
    let main_client_id = cliListVal.mcid

    const cutOffFlag = $(inpCutOff).prop("checked") ? "Y" : "N";    

    if (ttlamt >= minamt) {
        if ($("#inpUPI").val() == "") {
            alert("UPI Id cannot be blank.")
        }
        else {
            if (regex.test(upiId)) {
                if ($("#chkDecla").prop("checked")) {

                    var formdata = {
                        "dp_id": boid,
                        "main_client_id": main_client_id,
                        "family_token": $('#ddlFamilyList :selected').val(),
                        "ISIN": $('#pTxtISIN').text(),
                        "symbol": $("#pTxtSymbol").text(),
                        "bid_qty": $('#inpQty').val(),
                        "bid_price": $('#inpPrice').val(),
                        "bid_cutoff": cutOffFlag,
                        "bid_ttl_amount": ttlamt.toString(),
                        "upi_id": $('#inpUPI').val(),
                    }

                    var posturl = "/ClientPortal/IPO/psp_amd_ipo_requests";
                    $.ajax({
                        url: posturl,
                        type: "post",
                        contentType: "application/json",
                        data: JSON.stringify(formdata),
                        success: function (data) {
                            
                            if (data != null) {
                                alert("Enter OTP")

                                lockUnlockInputs("Disable")

                                // WCAG 3.3.4 — review step: state exactly what the OTP
                                // will commit, in text, before it is committed.
                                showBidSummary(formdata);
                                $("#divOTPSection").show();
                                $("#inpOtp").focus();
                            }
                            else {
                                $("#divOTPSection").hide();
                            }
                        },
                        error: function (xhr, err) {
                            alert(err)
                        }
                    })
                }
                else {
                    alert("Please accept the declaration.")
                }
            }
            else {
                alert("Please enter valid UPI Id.")
            }
        }
    }
    else {
        alert("Amount of your bid should be greater than minimum amount.")
    }
}

function showBidSummary(bid) {
    var name = $.trim($("#pTxtSymbol").text()) || bid.symbol;
    var price = bid.bid_cutoff === "Y" ? "at the cut-off price" : "at \u20b9" + bid.bid_price + " per share";
    $("#ipoBidSummary").text(
        "You are bidding for " + bid.bid_qty + " shares of " + name + " " + price +
        ". \u20b9" + Number(bid.bid_ttl_amount).toLocaleString("en-IN") +
        " will be blocked through UPI ID " + bid.upi_id + ".");
}

// WCAG 3.3.4 — the user can go back and correct the bid before confirming it.
$(document).on("click", "#btnEditBid", function () {
    $("#divOTPSection").hide();
    $("#inpOtp").val("");
    lockUnlockInputs("Enable");
    $("#inpLot").focus();
    if (window.a11y) { window.a11y.announce("Bid unlocked for editing."); }
});

function SubmitOtp() {

    let cliListVal = JSON.parse($('#ddlClientList :selected').val());
    let boid = cliListVal.BOID;

    var formdata = {
        "dp_id": boid,
        "flag": "V",
        "bid_actioncode":"N",
        "otp": $('#inpOtp').val()
    }

    var posturl = "/ClientPortal/IPO/psp_verify_ipo_otp";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data != null) {

                alert(data.sql_msg);
                
            }
            else {
                alert("Error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

