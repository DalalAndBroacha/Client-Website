$(document).ready(function () {



    CollapseSideMenu();
    HideFinYearList();

    
    console.log($('#modalDsp').val())
    if ($('#modalDsp').val() == "True") {
        $('#LoginModal').modal('show');
    }
    FetchData();    
});

function startajaxrequestGlobal() {
    $("#DashwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#DashwaitIn").css("display", "none");
}




$("#ddlFamilyList").change(function () {

    $("#initDashboard").hide();
    FetchData();

    //$('#preservedFamToken').val($('#ddlFamilyList :selected').val());
    //$('#preservedFamText').val($('#ddlFamilyList :selected').text());

});

function FetchData() {

    startajaxrequestGlobal(); //Ends in GetFamAssetDetails

    var fam_token = $('#ddlFamilyList :selected').val()
    
    executeAsynchronously(
        [ /*GetRMDetails(fam_token),*/ GetClientDetails(fam_token), GetClientAssetDetails(fam_token),
            GetFamAssetDetails(fam_token), GetSIPDetails(fam_token), GetXirrDetails(fam_token), GetNotifications(fam_token)], 1000);

    $("#initDashboard").show();
}

function GetRMDetails(family_token) {
    var formdata = {
        "family_token": family_token
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_RM_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tbodyRMDetails").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td>" + value.rm_name + "</td>" +
                        "<td>" + value.email_Id + "</td>" +
                        "<td>" + value.contact_No + "</td>" +
                        "</tr>";
                });

                $("#tbodyRMDetails").append(html);
                completeajaxrequestGlobal();
            }
            else {
                $("#tbodyRMDetails").html("");
                html = "<tr><td colspan = 3 class='text-center'>No Data available.</td></tr>"
                $("#tbodyRMDetails").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetClientDetails(family_token) {
    let formdata = {
        "family_token": family_token
    }

    let famName = $('#ddlFamilyList :selected').text()

    let posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_client_list";
    /*var posturl = "/Pie/psp_dsp_client_portal_dashboard_client_list";*/
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tbodyClientDetails").html("");
                let html = "";
                $.each(data, function (index, value) {
                    
                    html = html + "<tr>" +
                        "<td>" + value.main_client_name + "</td>" +
                        "<td>" + value.pan_number + "</td>" +
                        "<td style='text-align:center;'>" +
                        "<a class='badge Lightred_Icon' href='/ClientPortal/Reports/GlobalReport?family_token=" + family_token + "&main_client_id=" + value.main_client_id + "&family_name=" + famName + "'>Global</a>" +
                        "</td>" +
                        "</tr>";
                });

                $("#tbodyClientDetails").append(html);
            }
            else {
                $("#tbodyClientDetails").html("");
                html = "<tr><td colspan = 3 class='text-center'>No Data available.</td></tr>"
                $("#tbodyClientDetails").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetFamAssetDetails(family_token) {
    var formdata = {
        "family_token": family_token,
        "flag": "0"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_asset_allocation";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tbodyAssetFamDetails").html("");
                var html = "";
                $.each(data, function (index, value) {
                    if (value.display_order != "99") {
                        html = html + "<tr>" +
                            "<td>" + (value.asset_name == "InvITs" ? value.category : value.asset_name) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_cost.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.market_cost.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignCenter'>" + value.mkt_per.toFixed(2) + "</td>" +
                            "</tr>";
                    }
                    else {
                        html = html + "<tr class='GrandTotalRow'>" +
                            "<td class='GrandTotalRowData paraTextAlignLeft'>" + value.asset_name + "</td>" +
                            "<td class='GrandTotalRowData paraTextAlignRight'>" + numberWithCommas(value.holding_cost.toFixed(2)) + "</td>" +
                            "<td class='GrandTotalRowData paraTextAlignRight'>" + numberWithCommas(value.market_cost.toFixed(2)) + "</td>" +
                            "<td class='GrandTotalRowData paraTextAlignCenter'>" + value.mkt_per.toFixed(2) + "</td>" +
                            "</tr>";
                    }
                });

                $("#tbodyAssetFamDetails").append(html);
            }
            else {
                $("#tbodyAssetFamDetails").html("");
                html = "<tr><td colspan = 4 class='text-center'>No Data available.</td></tr>"
                $("#tbodyAssetFamDetails").append(html);
            }
        },
        complete: function (xhr) {
            completeajaxrequestGlobal();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetClientAssetDetails(family_token) {
    var formdata = {
        "family_token": family_token,
        "flag": "1"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_asset_allocation";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tbodyAssetCliDetails").html("");
                var html = "";

                $.each(data, function (index, value) {
                    if (value.display_order != "99") {
                        html = html + "<tr>" +
                            "<td>" + value.client_name + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_cost.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.market_cost.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignCenter'>" + value.mkt_per.toFixed(2) + "</td>" +
                            "</tr>";
                    }
                    else {
                        html = html + "<tr class='GrandTotalRow'>" +
                            "<td class='GrandTotalRowData paraTextAlignLeft'>" + value.client_name + "</td>" +
                            "<td class='GrandTotalRowData paraTextAlignRight'>" + numberWithCommas(value.holding_cost.toFixed(2)) + "</td>" +
                            "<td class='GrandTotalRowData paraTextAlignRight'>" + numberWithCommas(value.market_cost.toFixed(2)) + "</td>" +
                            "<td class='GrandTotalRowData paraTextAlignCenter'>" + value.mkt_per.toFixed(2) + "</td>" +
                            "</tr>";
                    }
                });

                $("#tbodyAssetCliDetails").append(html);
            }
            else {
                $("#tbodyAssetCliDetails").html("");
                html = "<tr><td colspan = 4 class='text-center'>No Data available.</td></tr>"
                $("#tbodyAssetCliDetails").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetSIPDetails(family_token) {
    var formdata = {
        "family_token": family_token
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_SIP_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tbodySIPdetails").html("");
                var html = "";

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignLeft'>" + value.main_client_name + "</td>" +
                        "<td class='paraTextAlignCenter'><p>" + value.scheme_Name + "</p><p class='tableDataLowerCol'>" + value.folio_no +"</p></td>" +
                        "<td class='paraTextAlignCenter'><p>" + formatDate(value.start_Date) + " to</p>"
                        + (value.terminating == "0" ? "<p>" + formatDate(value.end_Date) + "</p>" : "<p style='color:red;' title='Expiring shortly'>" + formatDate(value.end_Date) + "</p>") + "</td>" +
                        "<td class='paraTextAlignCenter'><p>" + numberWithCommas(value.amount.toFixed(2)) + "</p><p class='tableDataLowerCol'>" + value.frequency +"</p></td>" +
                        "</tr>"; 
                });

                //

                $("#tbodySIPdetails").append(html);
            }
            else {
                $("#tbodySIPdetails").html("");
                html = "<tr><td colspan = 4 class='text-center'>No Data available.</td></tr>"
                $("#tbodySIPdetails").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetXirrDetails(family_token) {
    var formdata = {
        "family_token": family_token
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_xirr_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tbodyXirrdetails").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td><p><a style='color:black; text-decoration:underline;' href='/ClientPortal/Reports/XIRR_Summary?acc_code=" + value.account_code + "'>" + value.client_name + "</a></p></td>" +
                        //"<td class='paraTextAlignCenter'><p><a style='color:black; text-decoration:underline;' href='/ClientPortal/Reports/XIRR_Summary?acc_code=" + data[i].account_code + "' >" + data[i].account_code + "</a></p></td>" +
                        "<td class='paraTextAlignRight" + isNegativeValue(value.stock_val) + "'>" + numberWithCommas(value.stock_val.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight" + isNegativeValue(value.abs_ret) + "'>" + value.abs_ret.toFixed(2) + "</td>" +
                        "<td class='paraTextAlignRight" + isNegativeValue(value.xirr_ret) + "'>" + value.xirr_ret.toFixed(2) + "</td>" +
                        "</tr>";
                });

                $("#tbodyXirrdetails").append(html);
            }
            else {
                $("#tbodyXirrdetails").html("");
                html = "<tr><td colspan = 4 class='text-center'>You do not have Equity account with us.</td></tr>"
                $("#tbodyXirrdetails").append(html);
            }
        },

        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetNotifications(family_token) {
    var formdata = {
        "family_token": family_token
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_client_portal_dashboard_notification";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                //$("#tbodyXirrdetails").html("");
                //var html = "";

                //$.each(data, function (index, value) {
                //    html = html + "<tr>" +
                //        "<td>" + value.client_name + "</td>" +
                //        "<td class='paraTextAlignRight" + isNegativeValue(value.stock_val) + "'>" + numberWithCommas(value.stock_val.toFixed(2)) + "</td>" +
                //        "<td class='paraTextAlignRight" + isNegativeValue(value.abs_ret) + "'>" + value.abs_ret.toFixed(2) + "</td>" +
                //        "<td class='paraTextAlignRight" + isNegativeValue(value.xirr_ret) + "'>" + value.xirr_ret.toFixed(2) + "</td>" +
                //        "</tr>";
                //});

                //$("#tbodyXirrdetails").append(html);
            }
            else {
                //html = "<tr><td colspan = 4 class='text-center'>You do not have Equity account with us.</td></tr>"
                //$("#tbodyXirrdetails").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}