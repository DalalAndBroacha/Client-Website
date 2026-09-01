$(document).ready(function () {

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    //gtag('config', 'G-6FJ1RE3WC0'); 
    gtag('config', 'G-6FJ1RE3WC0', {
        'page_title': 'IncomeReport',
        'page_path': '/Reports/IncomeReport'
    });
    gtag('event', 'IncomeReport', {
        'event_category': 'IncomeReport',
        'event_label': 'IncomeReport'
    });

    setTimeout(function () {
        loadIncomeDetails();
    }, 500);

    
    $("#ddlFinyear").change(function () {
        $("#hideshow").hide();
        loadIncomeDetails();
    });
    $("#ddlFamilyList").change(function () {
        $("#hideshow").hide();
        loadIncomeDetails();
    });
    $("#hideshow").hide();

    $("#inputPeriodStatus").change(function () { 
        PeriodValue2functuin();
    });
    $("#inputYearStatus").change(function () {
        if ($('#inputPeriodStatus :selected').val() != "3" && $('#inputPeriodStatus :selected').val() != "4") {
            $("#inputPeriod_ValueStatus").html("<option value=" + $('#inputYearStatus :selected').val() + ">" + $('#inputYearStatus :selected').val() + "</option>")
        } else {
            PeriodValue3functuin();
        }
    });

    $("#btnCloseModal").click(function () {
        $("#tbodyBindingrealised").html("");
        $("#tbodyBindingrealisedHeader").html("");
        $("#inputPeriodStatus").html("");
        $("#inputYearStatus").html("");
        $("#inputPeriod_ValueStatus").html("");
        $("#myModalst_pnl").modal("hide");
    })
});


//Load Grid
function loadIncomeDetails() {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlFamilyList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/IncomeDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            $("#noReFound").html(""); 
            $("#tbodyBinding").html("");
            var html = "";
            if (data.length > 0) {
                startajaxrequestIncome();              
                $("#tbodyBinding").html("");
                var html = "";
                $("#hideshow").show();
                $.each(data, function (index, value) {
                 
                    html = html + "<tr><td style='border: 1px solid #dddddd;'>" + value.client_name + "</td> <td style='border: 1px solid #dddddd;'>" + value.sub_category + "</td>" +
                        (value.int_pnl <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;cursor: pointer;text-decoration: underline;'>" + (value.int_pnl == 0 ? '-' : numberWithCommas(value.int_pnl.toFixed(2))) + "<img src='../images/down1.png' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.int_pnl == 0 ? '-' : numberWithCommas(value.int_pnl.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        (value.dividend <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;cursor: pointer;text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.dividend == 0 ? '-' : numberWithCommas(value.dividend.toFixed(2))) + "<img src='../images/down1.png' class='img-responsive7' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;cursor: pointer;text-decoration: underline;' onclick=\"myFunctionloadDivident(\'" + value.dividend + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.dividend == 0 ? ' -' : numberWithCommas(value.dividend.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        (value.st_pnl <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;cursor: pointer;text-decoration: underline;' onclick=\"myFunction(\'" + value.st_pnl + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.st_pnl == 0 ? '-' : numberWithCommas(value.st_pnl.toFixed(2))) + "<img src='../images/down1.png' class='img-responsive7' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;cursor: pointer;text-decoration: underline;' onclick=\"myFunction(\'" + value.st_pnl + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.st_pnl == 0 ? '-' : numberWithCommas(value.st_pnl.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        (value.lt_pnl <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;cursor: pointer;text-decoration: underline;' onclick=\"myFunction(\'" + value.lt_pnl + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.lt_pnl == 0 ? '-' : numberWithCommas(value.lt_pnl.toFixed(2))) + "<img src='../images/down1.png' class='img-responsive7' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;' onclick=\"myFunction(\'" + value.lt_pnl + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.lt_pnl == 0 ? '-' : numberWithCommas(value.lt_pnl.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        (value.lt_pnl_gf <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;cursor: pointer;text-decoration: underline;' onclick=\"myFunction(\'" + value.lt_pnl_gf + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.lt_pnl_gf == 0 ? '-' : numberWithCommas(value.lt_pnl_gf.toFixed(2))) + "<img src='../images/down1.png' class='img-responsive7' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;' onclick=\"myFunction(\'" + value.lt_pnl_gf + "\' ,\'" + value.main_client_id + "\',\'" + $.trim(value.sub_category) + "\')\">" + (value.lt_pnl_gf == 0 ? '-' : numberWithCommas(value.lt_pnl_gf.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        (value.total_pnl <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.total_pnl == 0 ? '-' : numberWithCommas(value.total_pnl.toFixed(2))) + "<img src='../images/down1.png' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.total_pnl == 0 ? '-' : numberWithCommas(value.total_pnl.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        (value.total_pnl_gf <= 0 ? "<td style = 'border: 1px solid #dddddd;text-align:right;color: #ff0000;'>" + (value.total_pnl_gf == 0 ? '-' : numberWithCommas(value.total_pnl_gf.toFixed(2))) + "<img src='../images/down1.png' style='width: 10px;float:right;' /></td>" : "<td style = 'border: 1px solid #dddddd;text-align:right;color: #008000;'>" + (value.total_pnl_gf == 0 ? '-' : numberWithCommas(value.total_pnl_gf.toFixed(2))) + "<img src='../images/Up1.png' class='img-responsive7' style='width: 10px;float:right;'/></td>") +
                        "</tr>";
                });  
                $("#tbodyBinding").append(html);
                completeajaxrequestIncome();
                $("#bFinYear").text($('#ddlFinyear :selected').val());
                $("#bfamilyName").text($('#ddlFamilyList :selected').text());
                $("#liRemoveFavourite").show();
            } else {
                $("#tbodyBinding").html("");
                $("#noReFound").html("");
                $("#hideshow").hide();
                $("#noReFound").append("No Record Found");
                $("#bFinYear").text($('#ddlFinyear :selected').val());
                $("#bfamilyName").text($('#ddlFamilyList :selected').text());
                $("#liRemoveFavourite").hide();
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: completeajaxrequestIncome
    })
}
function myFunction(st_pnl, client_id, sub_category) {
    if (st_pnl != "0") {
        $("#myModalst_pnl").modal({ backdrop: 'static', keyboard: false });
        $("#txtclient_id").val(client_id);
        $("#txtsub_category").val(sub_category);
        Period1functuin(client_id, sub_category);
    } else {
        alert("value will zero");
    }
    
};


//Loader
function completeajaxrequestIncome() {
    $("#waitInIncome").css("display", "none");
}
function startajaxrequestIncome() {
    $("#waitInIncome").css("display", "block");
}
function IncomeRealisedDetails(client_id, sub_category) {
    var formdata = {
        "FINYEAR": $('#ddlFinyear :selected').val(),
        "ClientID": client_id,
        "Subcategory": sub_category,
        "Rtpperiod": $('#inputPeriodStatus :selected').val(),
        "Rptperiod_value": $('#inputYearStatus :selected').val(),
    }

    var posturl = "/ClientPortal/Reports/RealisedDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            var html = "";
            $("#tbodyBindingrealised").html("");
            $("#tbodyBindingrealisedHeader").html("");
            $("#noReFoundrealised").html("");
            if (data.length > 0) {
                startajaxrequestIncome();
               
                var html = "";
                var html2 = "";
                html2 = html2 + "<tr><th>" + data[0].family_name + "</th><th>" + data[0].client_name + "</th>  </tr>";
                $("#tbodyBindingrealisedHeader").append(html2);
                $.each(data, function (index, value) {
                    html = html + "<tr><td style='border: 1px solid #dddddd;text-align:right;' colspan='11'>" + value.script_Name + "</td></tr><tr><td style='border: 1px solid #dddddd;text-align:right;'>" + value.buy_trn_qty + "</td><td style='border: 1px solid #dddddd;text-align:center;'>" + value.buy_trn_date + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.buy_trn_rate + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.buy_trn_rate + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.sell_trn_date + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.sell_trn_rate + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.sell_trn_rate + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.it_pnl_gf + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.st_pnl + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.it_pnl + "</td><td style='border: 1px solid #dddddd;text-align:right;'>" + value.int_pnl + "</td></tr>";
                          });
                $("#tbodyBindingrealised").append(html);
              //  $("#pFinYear").text($('#ddlFinyear :selected').val());
                $("#pFamily").text($('#ddlFamilyList :selected').text());
                completeajaxrequestIncome();
            } else {
                $("#tbodyBindingrealised").html("");
               // $("#hideshow").hide();
                $("#noReFoundrealised").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: completeajaxrequestIncome
    })
}
function Period1functuin(client_id, sub_category) {
    var posturl = "/ClientPortal/Reports/IncomePeriodDetails";
    $.ajax({
        url: posturl,
        type: "GET",
        contentType: "application/json",
        data: { period_type: "0", Year_value_data: "0" },
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            var htm = "";
            $("#inputPeriodStatus").html("");
            $.each(data, function (key, value) {
                htm = htm + "<option value=" + value.year_value + ">" + value.year + "</option>";
            });
            $("#inputPeriodStatus").append(htm);
           
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function () {
            completeajaxrequestIncome();
            PeriodValue2functuin(client_id, sub_category);
           
        }

    })
}
function PeriodValue2functuin(client_id, sub_category) { 
    var periodType = "";
    if ($('#inputPeriodStatus :selected').val() == "3" || $('#inputPeriodStatus :selected').val() == "4") {
        periodType = "2";
    } else {
        periodType = $('#inputPeriodStatus :selected').val();
    }
    var posturl = "/ClientPortal/Reports/IncomePeriodDetails";
    $.ajax({
        url: posturl,
        type: "GET",
        contentType: "application/json",
        data: { period_type: periodType, Year_value_data: $('#ddlFinyear :selected').val() },
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            var htmYear = "";
            
                $("#inputYearStatus").html(""); 
            $.each(data, function (key, value) {
                if ($('#ddlFinyear :selected').val() == value.year_value) {
                    htmYear = htmYear + "<option value=" + value.year_value + ">" + value.year + "</option>";
                }
            }); 
                $("#inputYearStatus").append(htmYear);  
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function () {
            completeajaxrequestIncome();
            if ($('#inputPeriodStatus :selected').val() != "3" && $('#inputPeriodStatus :selected').val() != "4") {
                $("#inputPeriod_ValueStatus").html("<option value=" + $('#inputYearStatus :selected').val() + ">" + $('#inputYearStatus :selected').val() + "</option>")
            } else {
                PeriodValue3functuin();
            }
            IncomeRealisedDetails(client_id, sub_category);
        },
    })
}
function PeriodValue3functuin() {
   
    var posturl = "/ClientPortal/Reports/IncomePeriodDetails";
    $.ajax({
        url: posturl,
        type: "GET",
        contentType: "application/json",
        data: { period_type: $('#inputPeriodStatus :selected').val(), Year_value_data: $('#inputYearStatus :selected').val() },
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            
            var htmYearValue = "";
            $("#inputPeriod_ValueStatus").html(""); 
            $.each(data, function (key, value) {
                if (value.year_value != null) {
                    htmYearValue = htmYearValue + "<option value=" + value.year + ">" + value.year_value + "</option>";
                } else {
                    htmYearValue = htmYearValue + "<option value=" + value.period_value + ">" + value.period_value_display + "</option>";
                }
            });
            $("#inputPeriod_ValueStatus").append(htmYearValue);
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function () {
            completeajaxrequestIncome();
        },
    })
}

function myFunctionloadDivident(st_pnl, client_id, sub_category) {
    if (st_pnl != "0") {
        $("#myModalst_dividend").modal({ backdrop: 'static', keyboard: false });
        $("#txtclient_id_divindend").val(client_id); 
        $("#txtsub_category_divindend").val(sub_category);
        $("#popclient_id").html("");
        $("#tbodyBindingrealisedDividend").html("");
        $("#noReFoundrealiseddivindend").html("");
        loadDividentIncomeDetails(client_id, sub_category);
        $("#popclient_id").val(client_id);
    } else {
        alert("value will zero");
    }

};

function loadDividentIncomeDetails(client_id, sub_category) {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlFamilyList :selected').val(),
        "main_client_id": client_id
    }
    var posturl = "/ClientPortal/Reports/RealisedDividendDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
          
            $("#tbodyBindingrealisedDividend").html("");
            $("#noReFoundrealiseddivindend").html("");
            if (data.length > 0) {
                startajaxrequestIncome();

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr style='border: 1px solid #dddddd;'><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;' id=" + value.family_id + ">" + value.client_name + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;' id=" + value.family_id + ">" + formatDate(value.dividend_date) + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:center;' id=" + value.family_id + ">" + value.isin + "</p></td><td style='border: 1px solid #dddddd;'> <p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;' id=" + value.family_id + ">" + value.scrip_name + "</p></td><td style='border: 1px solid #dddddd;'><p style='font-size:14px;font-family:Roboto, sans-serif;white-space:pre;text-align:right;' id=" + value.family_id + ">" + numberWithCommas(value.value.toFixed(2)) + "</p></td></tr>";
                });
                $("#tbodyBindingrealisedDividend").append(html);
               
                $("#divFinYear").text($('#ddlFinyear :selected').val());
                $("#divFamily").text($('#ddlFamilyList :selected').text());

                completeajaxrequestIncome();
            } else {
                $("#tbodyBindingrealisedDividend").html("");
                $("#noReFoundrealiseddivindend").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: completeajaxrequestIncome
    })
}

$("#btnViewReportModal").click(function () {
    var period_value = "";
    if ($('#inputPeriodStatus :selected').val() == "3") {
        period_value = $("#inputPeriod_ValueStatus").val();
    } else if ($('#inputPeriodStatus :selected').val() == "4") {
        period_value = $("#inputPeriod_ValueStatus").val();
    }
    else {
        period_value = $('#inputYearStatus :selected').val()
    }
    var formdata = {
        "FINYEAR": $('#inputYearStatus :selected').val(),
        "ClientID": $('#txtclient_id').val(),
        "Subcategory": $('#txtsub_category').val(),
        "Rtpperiod": $('#inputPeriodStatus :selected').val(),
        "Rptperiod_value": period_value,
    }

    var posturl = "/ClientPortal/Reports/RealisedDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            var html = "";
            $("#tbodyBindingrealised").html("");
            $("#tbodyBindingrealisedHeader").html("");
            $("#noReFoundrealised").html("");
            if (data.length > 0) {
                startajaxrequestIncome();
                
                var html = "";
                var html2 = "";
                html2 = html2 + "<th>" + data[0].family_name + "</th><th>" + data[0].client_name + "</th>";
                $("#tbodyBindingrealisedHeader").append(html2);
                $.each(data, function (index, value) {
                    //html = html + "<tr><td>" + value.script_Name + "</td> <td>" + formatDate(value.buy_trn_date) + "</td>";
                    html = html + "<td>" + value.script_Name + "</td> <td>" + formatDate(value.buy_trn_date) + "</td>";   
                });
                $("#tbodyBindingrealised").append(html);
                completeajaxrequestIncome();
            } else {
                $("#tbodyBindingrealised").html("");
               
                // $("#hideshow").hide();
                $("#noReFoundrealised").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: completeajaxrequestIncome
    })
})

$("#btnIncomeExport").click(function () {
    window.location = '/ClientPortal/Excel/DownloadIncomeDetailsExcel?finYear=' + Encrypt($('#ddlFinyear :selected').val()) + ' &Family=' + Encrypt($('#ddlFamilyList :selected').val());
    return false;
})

$("#btnExportPdf").click(function () {
    window.location = '/ClientPortal/Excel/DownloadIncomeDetailsPdf?finYear=' + Encrypt($('#ddlFinyear :selected').val()) + ' &Family=' + Encrypt($('#ddlFamilyList :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    return false;
})

$("#btnincomedivExportPopup").click(function () {
    window.location = '/ClientPortal/Excel/DownloadIncomeDividendopupExcel?IDfinyear=' + Encrypt($('#ddlFinyear :selected').val()) + '&IDfamilyid=' + Encrypt($('#ddlFamilyList :selected').val()) + '&client_id=' + Encrypt($('#popclient_id').val());
    return false;
})

$("#btnincomedivExportPdfPopup").click(function () {
    window.location = '/ClientPortal/Excel/DownloadIncomeDividendopupPdf?IDfinyear=' + Encrypt($('#ddlFinyear :selected').val()) + '&IDfamilyid=' + Encrypt($('#ddlFamilyList :selected').val()) + '&client_id=' + Encrypt($('#popclient_id').val()) + '&FinyearPdf=' + $('#ddlFinyear :selected').text() + '&FamilyNamePdf=' + $('#ddlFamilyList :selected').text();
    return false;
})
var client_id1, sub_category1;
$("#btnIncomeRealisedProfitLossReportExport").click(function () {
    window.location = '/ClientPortal/Excel/RealisedDetailsExcel?FINYEAR=' + Encrypt($('#ddlFinyear :selected').val()) + '&ClientID=' + Encrypt(client_id1) + '&Subcategory=' + Encrypt(sub_category1) + '&Rtpperiod=' + Encrypt($('#inputPeriodStatus :selected').val()) + '&Rptperiod_value=' + Encrypt($('#inputYearStatus :selected').val());
    return false;
})

$("#btnIncomeRealisedProfitLossReportPdf").click(function () {
    window.location = '/ClientPortal/Excel/RealisedDetailsPdf?FINYEAR=' + Encrypt($('#ddlFinyear :selected').val()) + '&ClientID=' + Encrypt(client_id1) + '&Subcategory=' + Encrypt(sub_category1) + '&Rtpperiod=' + Encrypt($('#inputPeriodStatus :selected').val()) + '&Rptperiod_value=' + Encrypt($('#inputYearStatus :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text() + '&FamilyName=' + $('#ddlFamilyList :selected').text();
    return false;
})

function IncomeUpdateFev() {
    var formdata = {
        "module_id": $("p[name='Realized Gain/Loss report']").attr("id")
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
                        if ($(this).attr('id') == $("p[name='Realized Gain/Loss report']").attr("id")) {
                            $("#IncomeAddtoFavourite").hide();
                            $("#IncomeRemoveFavourite").show();
                            return false;
                        } else {
                            $("#IncomeAddtoFavourite").show();
                            $("#IncomeRemoveFavourite").hide();
                        }
                    })
                }, 500);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function IncomeDeleteFev() {
    var formdata = {
        "module_id": $("p[name='Realized Gain/Loss report']").attr("id")
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
                        if ($(this).attr('id') == $("p[name='Realized Gain/Loss report']").attr("id")) {
                            $("#IncomeAddtoFavourite").hide();
                            $("#IncomeRemoveFavourite").show();
                            return false;
                        } else {
                            $("#IncomeAddtoFavourite").show();
                            $("#IncomeRemoveFavourite").hide();
                        }
                    })
                }, 500);
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
                        if ($(this).attr('id') == $("p[name='Realized Gain/Loss report']").attr("id")) {
                            $("#IncomeAddtoFavourite").hide();
                            $("#IncomeRemoveFavourite").show();
                            return false;
                        } else {
                            $("#IncomeAddtoFavourite").show();
                            $("#IncomeRemoveFavourite").hide();
                        }
                    })
                }, 500);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

setTimeout(function () {
    $(".fetchId").each(function () {
        if ($(this).attr('id') == $("p[name='Realized Gain/Loss report']").attr("id")) {
            $("#IncomeAddtoFavourite").hide();
            $("#IncomeRemoveFavourite").show();
            return false;
        } else {
            $("#IncomeAddtoFavourite").show();
            $("#IncomeRemoveFavourite").hide();
        }
    })
}, 500);