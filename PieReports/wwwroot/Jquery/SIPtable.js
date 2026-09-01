$(document).ready(function () {

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    //gtag('config', 'G-6FJ1RE3WC0'); 
    gtag('config', 'G-6FJ1RE3WC0', {
        'page_title': 'SIPtable',
        'page_path': '/Reports/SIPtable'
    });
    gtag('event', 'SIPtable', {
        'event_category': 'SIPtable',
        'event_label': 'SIPtable'
    });


    loadSIPDetails();

});

function loadSIPDetails() {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlfamilyLists :selected').val()
    }

    var posturl = "/ClientPortal/Reports/SIPDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestwaitsipli();
        },
        success: function (data) {
            if (data != "") {
                startajaxrequestwaitsipli();
                $("#dataLoadSIPDetails").html("");
                $("#dataLoadSIPDetails").append(data);
                completeajaxrequestwaitsipli();
                $("#bFinYear").text($('#ddlFinyear :selected').val());
                $("#liRemoveFavourite").show();
                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);
               // $("#bfamilyName").text($('#ddlFamilyList :selected').text());
            } else {
                $("#ddlFinyear").attr("disabled", true);
                $("#ddlFamilyList").attr("disabled", true);
                $("#dataLoadSIPDetails").html("");
                $("#dataLoadSIPDetails").append("No Record Found");
                completeajaxrequestwaitsipli();
                $('#myModalSIP').hide();
                $("#bFinYear").text($('#ddlFinyear :selected').val());                
                $("#liRemoveFavourite").hide();
            }
        },
        error: function (xhr, err) {

            // alert(xhr+"   ,  "+err);
        }
    })
}


$("#btnsipExport").click(function () {
    window.location = '/ClientPortal/Excel/DownloadSIPDetailsExcel';
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/DownloadSIPDetailsPdf';
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnsipExportExpire").click(function () {
    window.location = '/ClientPortal/Excel/DownloadExpiringSIPDetailsExcel';
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnExportPdfExpire").click(function () {
    window.location = '/ClientPortal/Excel/DownloadExpiringSIPDetailsPdf';
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

function completeajaxrequestwaitsipli() {
    $("#waitsipli").css("display", "none");
}
function startajaxrequestwaitsipli() {
    $("#waitsipli").css("display", "block");
}

$("#myModalSIP").click(function () {
    searchfunction();
    $("#myModal11").modal("show");
})

function searchfunction() {
    var formdata = "";
    var posturl = "/ClientPortal/Reports/SIPSarchDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {            
        /* if (data[n].terminating > 0) {*/
           
                var html = "";
            $("#trsear").html("");
            $.each(data, function (index, value) {
                $("#noReFound").html("");
                
                if (value.terminating > 0) {
                    html = html + "<tr><td style='border: 1px solid #dddddd;'><p style='font-size:15px;'>" + value.main_client_name + "</p><p style='font-size:12px;'>" + value.login_Name + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:15px;text-align:center;'>" + (value.mobile == "" ? '-' : value.mobile) + "</p><p style='font-size:12px;text-align:center;'>" + value.email_Id + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:15px;'>" + value.scheme_Name + "</p><p style='font-size:12px;'>" + value.folio_no + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:15px;text-align:center;'>" + formatDate(value.end_Date) + "</p><p style='font-size:12px;text-align:center;'>" + formatDate(value.start_Date) + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:15px;text-align:right;'>" + numberWithCommas(value.amount.toFixed(2)) + "</p><p style='font-size:12px;text-align:right;'>" + value.frequency + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:15px;'>" + value.bank_Name + "<p/><p style='font-size:12px;'>" + value.ac_No + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:15px;color:red;'>" + (value.terminating == 0 ? '-' : value.terminating) + "<p/><p></p></td></tr>";
                } else {
                    $("#noReFound").append("No Record Found");
                }
            });

    $("#trsear").append(html);
            $("#pFinYear").text($('#ddlFinyear :selected').val());
        //     else {
        //    $("#trsear").html("");
        //    $("#noReFound").append("No Record Found");

        //}
        },
        error: function (xhr, err) {
            alert(err)
        },
        // complete: completeajaxrequest
    })
}


function SIPUpdateFev() {
    var formdata = {
        "module_id": $("p[name='SIP Reports']").attr("id")
    }
    var posturl = "/ClientPortal/Pie/UpdateFev";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            //  alert(data)
            if (data.sql_status == "Fail") {
                alert(data.sql_message);
            } else {
                FetchFev();
                setTimeout(function () {
                    $(".fetchId").each(function () {
                        if ($(this).attr('id') == $("p[name='SIP Reports']").attr("id")) {
                            $("#sipliAddtoFavourite").hide();
                            $("#sipliRemoveFavourite").show();
                            return false;
                        } else {
                            $("#sipliAddtoFavourite").show();
                            $("#sipliRemoveFavourite").hide();
                        }
                    })
                }, 1000);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function SIPDeleteFev() {
    var formdata = {
        "module_id": $("p[name='SIP Reports']").attr("id")
    }
    var posturl = "/ClientPortal/Pie/UpdateFevRemove";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            //  alert(data)
            if (data.sql_status == "Fail") {
                alert(data.sql_message);
            } else {
                FetchFev();
                setTimeout(function () {
                    $(".fetchId").each(function () {
                        if ($(this).attr('id') == $("p[name='SIP Reports']").attr("id")) {
                            $("#sipliAddtoFavourite").hide();
                            $("#sipliRemoveFavourite").show();
                            return false;
                        } else {
                            $("#sipliAddtoFavourite").show();
                            $("#sipliRemoveFavourite").hide();
                        }
                    })
                }, 1000);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function myFunctionRemoveFav(id) {
    var formdata = {
        "module_id": id
    }
    var posturl = "/ClientPortal/Pie/UpdateFevRemove";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            //  alert(data)
            if (data.sql_status == "Fail") {
                alert(data.sql_message);
            } else {
                FetchFev();
                setTimeout(function () {
                    $(".fetchId").each(function () {
                        if ($(this).attr('id') == $("p[name='SIP Reports']").attr("id")) {
                            $("#sipliAddtoFavourite").hide();
                            $("#sipliRemoveFavourite").show();
                            return false;
                        } else {
                            $("#sipliAddtoFavourite").show();
                            $("#sipliRemoveFavourite").hide();
                        }
                    })
                }, 1000);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

setTimeout(function () {
    $(".fetchId").each(function () {
        if ($(this).attr('id') == $("p[name='SIP Reports']").attr("id")) {
            $("#sipliAddtoFavourite").hide();
            $("#sipliRemoveFavourite").show();
            return false;
        } else {
            $("#sipliAddtoFavourite").show();
            $("#sipliRemoveFavourite").hide();
        }
    })
}, 1000);