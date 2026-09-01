$(document).ready(function () {

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    //gtag('config', 'G-6FJ1RE3WC0'); 
    gtag('config', 'G-6FJ1RE3WC0', {
        'page_title': 'DividendDetails',
        'page_path': '/Reports/DividendDetails'
    });
    gtag('event', 'DividendDetails', {
        'event_category': 'DividendDetails',
        'event_label': 'DividendDetails'
    });


    loadDividentDetails();

    $("#ddlFinyear").change(function () {
        loadDividentDetails();
    });


    $("a.collapse").click(function (e) {
        e.preventDefault();
        var $div = $(this).next('.panel-collapse collapse show');
        $(".panel-collapse collapse show").not($div).hide();
        if ($div.is(":panel-collapse collapse")) {
            $div.hide()
        } else {
            $div.show();
        }
    });
});
$(".findId").each(function () {
    if ($(this).attr('id') == $(".fetchId").attr("id")) {
        $("#liAddtoFavourite").show();
        $("#liRemoveFavourite").hide();
    } else {
        $("#liRemoveFavourite").show();
        $("#liAddtoFavourite").hide();
    }
});


$("#btndivindendExport").click(function () {
    window.location = '/ClientPortal/Excel/DownloadDividendDetailsExcel?finYear=' + Encrypt($('#ddlFinyear :selected').val());
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnExportDividentPdf").click(function () {
    window.location = '/ClientPortal/Excel/DownloadDividendDetailsPdf?finYear=' + Encrypt($('#ddlFinyear :selected').val()) + '&FinyearValue=' + $('#ddlFinyear :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btndivindendPopupExport").click(function () {
    window.location = '/ClientPortal/Excel/DownloadDividendDetailsPopupExcel?finYear=' + Encrypt($('#ddlFinyear :selected').val());
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

$("#btnExportDividentPopupPdf").click(function () {
    window.location = '/ClientPortal/Excel/DownloadDividendDetailsPopupPdf?finYear=' + Encrypt($('#ddlFinyear :selected').val())+ '&FinyearValue=' + $('#ddlFinyear :selected').text();
    // $("#btnExport").load("/ClientPortal/Excel/DownloadPerformanceExcel", { FINYR: $('#ddlFinyear :selected').val(), Family: $('#ddlFamilyList :selected').val() });
    return false;
})

//Load Grid
function loadDividentDetails() {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlFamilyList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/DividendDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {
            if (data != "") {
                startajaxrequest();
                $("#dataLoadVettingDetails").html("");
                $("#dataLoadVettingDetails").append(data);
                $("#bFinYear").text($('#ddlFinyear :selected').val());
                completeajaxrequest();
                $('#myModal1').show();
                $("#liRemoveFavourite").show();
                $("#ddlFamilyList").attr("disabled", true);
            } else {
                $("#dataLoadVettingDetails").html("");
                $("#dataLoadVettingDetails").append("No Record Found");
                $('#myModal1').hide();
                $("#bFinYear").text($('#ddlFinyear :selected').val());
                $("#liRemoveFavourite").hide();
                $("#ddlFamilyList").attr("disabled", true);
            }
        },
        error: function (xhr, err) {
             alert(xhr+"   ,  "+err);
        },
        complete: completeajaxrequest
    })
}

function completeajaxrequest() {    
    $("#wait").css("display", "none");
}
function startajaxrequest() {    
    $("#wait").css("display", "block");
}
var finYear; 
$("#myModal1").click(function () {
    searchfunctuin();
    finYear = $('#ddlFinyear :selected').val();
    $("#myModal11").modal("show");
})
function searchfunctuin() {
    var formdata = {
        "yearID": $('#ddlFinyear :selected').val(),
        "familyListID": $('#ddlfamilyLists :selected').val()
    }

    var posturl = "/ClientPortal/Reports/DividendSearDetails";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#bFinYear").text($('#ddlFinyear :selected').val());
            var html = "";
            //var items = [];
            $("#trsear").html("");
            $.each(data, function (index, value) {
                html = html + " <tr><td> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;' id=" + value.family_id + ">" + value.family_name + "</p></td><td> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;' id=" + value.family_id + ">" + value.client_name + "</p></td><td> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;' id=" + value.family_id + ">" + formatDate(value.dividend_date) + "</p></td><td> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;' id=" + value.family_id + ">" + value.isin + "</p></td><td> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;' id=" + value.family_id + ">" + value.scrip_name + "</p></td><td> <p style='font-size:14px;font-family:Roboto, sans-serif;word-wrap:break-word;overflow: hidden;float:right;' id=" + value.family_id + ">" + numberWithCommas(value.value.toFixed(2)) + "</p></td></tr>";
               // items.push(html);
            });
           // items.sort();
            $("#trsear").append(html);
            $("#pFinYear").text($('#ddlFinyear :selected').val());
        },
        error: function (xhr, err) {
            alert(err)
        },
        // complete: completeajaxrequest
    })
}

//function formatDateold(dateStr) {
//    const d = new Date(dateStr);
//    var datevalu = d.getDate().toString().padStart(2, '0') + '/' + d.getMonth().toString().padStart(2, '0') + '/' + d.getFullYear();

//    //var pattern = /(.*?)\/(.*?)\/(.*?)$/;
//    //var result = datevalu.replace(pattern, function (match, p1, p2, p3) {
//    //    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
//    //    return (p2 < 10 ? "0" + p2 : p2) + "-" + months[(p1 - 1)] + "-" + p3;
//    //});
//    return datevalu;

//}
function myFunction() {
    var input, filter, table, tr, td, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[4];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}


// Clck Event for Expand and collaps Row Data
$(".panel-group").on("click", ".details-control", function () {
    //toggle rows
    var tr = $(this).closest('tr');
    var row = table.row(tr);
    if (row.child.isShown()) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    }
    else {
        
        tr.addClass('shown');
    }
});


function DividendUpdateFev() {
    var formdata = {
        "module_id": $("p[name='Dividend Interest Report']").attr("id")
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
                        if ($(this).attr('id') == $("p[name='Dividend Interest Report']").attr("id")) {
                            $("#DividendAddtoFavourite").hide();
                            $("#DividendRemoveFavourite").show();
                            return false;
                        } else {
                            $("#DividendAddtoFavourite").show();
                            $("#DividendRemoveFavourite").hide();
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
function DividendDeleteFev() {
    var formdata = {
        "module_id": $("p[name='Dividend Interest Report']").attr("id")
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
                        if ($(this).attr('id') == $("p[name='Dividend Interest Report']").attr("id")) {
                            $("#DividendAddtoFavourite").hide();
                            $("#DividendRemoveFavourite").show();
                            return false;
                        } else {
                            $("#DividendAddtoFavourite").show();
                            $("#DividendRemoveFavourite").hide();
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
                        if ($(this).attr('id') == $("p[name='Dividend Interest Report']").attr("id")) {
                            $("#DividendAddtoFavourite").hide();
                            $("#DividendRemoveFavourite").show();
                            return false;
                        } else {
                            $("#DividendAddtoFavourite").show();
                            $("#DividendRemoveFavourite").hide();
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
        if ($(this).attr('id') == $("p[name='Dividend Interest Report']").attr("id")) {
            $("#DividendAddtoFavourite").hide();
            $("#DividendRemoveFavourite").show();
            return false;
        } else {
            $("#DividendAddtoFavourite").show();
            $("#DividendRemoveFavourite").hide();
        }
    })
}, 500);

 