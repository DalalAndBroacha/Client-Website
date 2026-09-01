$(document).ready(function () {

    FetchFamily();
    FetchFev();
    $('#ddlFinyear').select2();

    $('#preservedFamToken').val($('#ddlFamilyList :selected').val());
    $('#preservedFamText').val($('#ddlFamilyList :selected').text());

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    //gtag('config', 'G-6FJ1RE3WC0'); 
    gtag('config', 'G-6FJ1RE3WC0', {
        'page_title': 'Dashboard',
        'page_path': '/Dashboard'
    });
    gtag('event', 'Dashboard', {
        'event_category': 'Dashboard',
        'event_label': 'Log In'
    });
});



function onlyNumberKey(evt) { 
    //Used for OTP fields or Number only fields does not take ". - +"
    // Only ASCII character in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57)) {
        return false;
    }
    return true;
}


function decimalNumberKey(evt) {
    if ((evt.keyCode >= 48 || evt.keyCode <= 57) || evt.keyCode === 46) {
        //if (this.value.split('.').length === 2) {
        //    return false;
        //}
        //else {
        //    return true;
        //}
        return true;
    }
    else {
        return false;
    }
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function fnDisableFamilyList() {
    $("#ddlFamilyList").attr("disabled", true);
}

function fnEnableFamilyList() {
    $("#ddlFamilyList").attr("disabled", false);
}

function fnDisableFinyearList() {
    $("#ddlFinyear").attr("disabled", true);
}

function fnEnableFinyearList() {
    $("#ddlFinyear").attr("disabled", false);
}

function HideFamilyList() {

    $('#main_UL li:eq(3)').hide();
    $('#main_UL li:eq(4)').hide();
}
function ShowFamilyList() {

    $('#main_UL li:eq(3)').show();
    $('#main_UL li:eq(4)').show();
}

function CollapseSideMenu() {

    $("[data-widget='pushmenu']").PushMenu("collapse");

}

//Used to run Async functions in order | 1 sec = 1000 milli secs setTimeout's 2nd parameter
function executeAsynchronously(functions, timeout) {
    for (var i = 0; i < functions.length; i++) {
        setTimeout(functions[i], timeout);
    }
}

function HideFinYearList() {
    $('#main_UL li:eq(1)').hide();
    $('#main_UL li:eq(2)').hide();
}
function ShowFinYearList() {
    $('#main_UL li:eq(1)').show();
    $('#main_UL li:eq(2)').show();
}

function ChangeDashboardModel() {   
    $("#ChangePasswordM").modal("show");
} 
function ChangeUsernameDashboardModel() {   
    $("#ChangeUsernameM").modal("show");
} 
function ReportIFrameLayout(obj,text) {
    $(".modal-title").html(text);
    var url = obj;
    $("#myModalLayout iframe").attr("src", url);
    $("#myModalLayout").modal("show");

    //test1 = obj;
    //document.write('<iframe height="450" frameborder="0" scrolling="yes" style="width:80%;" src="' + test1 + '" type= "text/javascript"></iframe>');
}


//Dates
function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}

function formatDateForDatePicker(dateStr) {

    var dateAr = dateStr.split('/');

    var newDate = dateAr[2] + '-' + dateAr[1] + '-' + dateAr[0];

    return newDate;
}

function formatDate_yyyymmdd(dateStr) { //For Data-order
    var d = new Date(dateStr),
        month = '' + (d.getMonth() + 1), // +1 Since in getMonth() method Jan = 0
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return (year + month + day);
}


function formatDateold(dateStr) { //Sql datetime to yyyy-mm-dd for Date picker
    var d = new Date(dateStr),
        month = '' + (d.getMonth() + 1), // +1 Since in getMonth() method Jan = 0
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function invertDate(dateStr) { // dd-mm-yyyy to yyyy-mm-dd for Date picker
    return dateStr.split("-").reverse().join("-");
}


function formatDateIndianStandard(dateStr) {
    var d = new Date(dateStr),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month, year].join('-');
}


function formatDate(date) {
    if (date !== undefined && date !== "") {
        var myDate = new Date(date);
        var day = '' + myDate.getDate();
        var month = [
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec",
        ][myDate.getMonth()];
        var year = myDate.getFullYear().toString().slice(2);

        if (day.length < 2) {
            day = '0' + day;
        }

        var str = day + "-" + month + "-" + year;
        return str;
    }
    return "";
}

function formatDateTime(date) {


   return new Date(date).toISOString().
        replace(/T/, ' ').      // replace T with a space
        replace(/\..+/, '')     // delete the dot and everything after
}

function formatDateTimeForSort(date) {

    var newDate = new Date(date).toISOString().
        replace(/T/, ' ').
        replace(/\..+/, '')

    newDate = newDate.replaceAll("-", "").replaceAll(":", "").replace(" ", "")

    return newDate
}

//Dates


function numberWithCommas(x) {
   /* us return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");*/
    return x.toString().replace(/\B(?=(?:(\d\d)+(\d)(?!\d))+(?!\d))/g, ',');
    
}
function ReportIFrame(obj, text) {
    $(".modal-title").html(text);
    var url = obj;
    $("#myModal iframe").attr("src", url);
    $("#myModal").modal("show");
}
function CommonUpdateFev() {
    var formdata = {
        "module_id": $("p[name='Common Documents']").attr("id")
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
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
//function CommonDeleteFev() {
//    var formdata = {
//        "module_id": $("p[name='Common Documents']").attr("id")
//    }
//    var posturl = "/ClientPortal/Pie/UpdateFevRemove";
//    $.ajax({
//        url: posturl,
//        type: "post",
//        contentType: "application/json",
//        data: JSON.stringify(formdata),
//        success: function (data) {
//            //  alert(data)
//            if (data.sql_status == "Fail") {
//                alert(data.sql_message);
//            } else {
//                FetchFev();
//            }
//        },
//        error: function (xhr, err) {
//            alert(err)
//        }
//    })
//}

//function myFunctionRemoveFav(id) {
//    var formdata = {
//        "module_id": id
//    }
//    var posturl = "/ClientPortal/Pie/UpdateFevRemove";
//    $.ajax({
//        url: posturl,
//        type: "post",
//        contentType: "application/json",
//        data: JSON.stringify(formdata),
//        success: function (data) {
//            //  alert(data)
//            if (data.sql_status == "Fail") {
//                alert(data.sql_message);
//            } else {
//                FetchFev();
//            }
//        },
//        error: function (xhr, err) {
//            alert(err)
//        }
//    })
//}
function FetchFamily() {

    var length = $('#ddlFamilyList').children('option').length;

    if (length < 501) {
        $('#ddlFamilyList').select2();
    }
    else {

        var LoginId = $("#LoggedInUID").val()

        $('#ddlFamilyList').select2({
            ajax: {
                url: "/ClientPortal/Pie/SearchFamilyName",
                dataType: 'json',
                delay: 750, // wait 750 milliseconds before triggering the request
                data: function (params) {
                    return {
                        loginID: LoginId,
                        display_flag:"1",
                        searchTerm: params.term // search term
                    };
                },
                processResults: function (data, params) {

                    params.page = params.page || 1; //indicate that infinite scrolling can be used

                    var results = [];
                    $.each(data, function (k, v) { //parse the results into the format expected by Select2
                        results.push({
                            id: v.family_Token,
                            text: v.family_Name
                        });
                    });

                    return {
                        results: results,
                        pagination: {
                            more: (params.page * 30) < data.total_count
                        }
                    };
                },
                
            },
            minimumInputLength: 4,
            maximumInputLength: 100,
            templateResult: repoFormatResult,
            templateSelection: repoFormatSelection
        });
    }
}

function repoFormatResult(repo) {
    if (repo.loading) {
        return repo.text;
    }
    else {
        var $container = $(
            "<option value=" + repo.id + ">" + repo.text + "</option>"
        );
        return $container;
    }
}

function repoFormatSelection(repo) {
    return repo.family_Name || repo.text;
}

function FetchFev() {
    var formdata = {

    }

    var posturl = "/ClientPortal/Pie/FetchFev";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            var json = $.parseJSON(data);
            $("#divFetchfev").html("");
            var html = "";
            $.each(json, function (index, value) {
                html = html + "<div class='dropdown-divider'></div><a href='" + value.CP_source_path + "' class='dropdown-item'><i class='mr-2 fetchId' id='" + value.id + "'></i> " + value.name + "</a><a href='#'><img onclick=\"myFunctionRemoveFav(\'" + value.id + "\')\" style='width: 20px; margin-right: 20px; margin-top:-32px; float:right;' id='theImg' src='/ClientPortal/images/RemoveFavourite.png' /></a>";
            });
            // $("#divFetchfev").html("");
            // $("#ddlFamilyList").append(html);
            $("#divFetchfev").append(html);
        },
        error: function (xhr, err) {
            alert(err)
        },
    })
}
function print(id) {
    var divToPrint = $(id).html();// document.getElementById('printValue');

    var newWin = window.open('', 'Print-Window');

  //  newWin.document.open();

    newWin.document.write('<html><body onload="window.print()">' + divToPrint + '</body></html>');

    newWin.document.close();

    setTimeout(function () { newWin.close(); }, 10);
}

function Encrypt(txtUserName) {

    var encryptedlogin = $.base64.encode(txtUserName);
    return encryptedlogin;
}


function fnSetCurrFamId()
{
    var famId = $('#ddlFamilyList :selected').val()
    var formdata = {
        "Family_Token": famId
    }
    var posturl = "/ClientPortal/Exports/SetFamilyID";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#Curr_Fam_ID").val(data.family_Id);
        },
        error:
            function (jqXHR, textStatus, errorThrown) {
                console.log("Failed to set Fam ID")
            },
    })
}


//***PDF and Excel download common method
//For btn_id '#' is required eg: #btnExportPdf, all the parameter values must be in string.
function PDFandExcelExport(btn_id, report_id, LogIn_ID, fam_id, export_type, export_format, parameters = " ", description = "DnB Report.") {

    var Fin_year = $('#ddlFinyear :selected').val()
    
    var formdata = {
        "loginID": LogIn_ID,
        "family_id": fam_id,
        "FINYR": Fin_year,
        "report_id": report_id,
        "export_type": export_type,
        "export_format": export_format,
        "parameters": parameters,
        "description": description
    }
    var posturl = "/ClientPortal/Exports/EmailMainReport";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $(btn_id).hide();

            if (export_type == "E") {
                alert("Report will be mailed to you within 2-3 minutes.");
            }
            else if (export_type == "X") {
                alert("The requested report will be available in Downloads->Reports. Kindly download it from there.");
            }
        },
        error:
            function (jqXHR, textStatus, errorThrown) {

            },
    })
    return false;
}
//***PDF and Excel download common method


