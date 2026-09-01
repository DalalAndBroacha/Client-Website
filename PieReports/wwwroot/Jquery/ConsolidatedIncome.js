$(document).ready(function () {

    //window.dataLayer = window.dataLayer || [];
    //function gtag() { dataLayer.push(arguments); }
    //gtag('js', new Date());

    ////gtag('config', 'G-6FJ1RE3WC0'); 
    //gtag('config', 'G-6FJ1RE3WC0', {
    //    'page_title': 'IncomeReport',
    //    'page_path': '/Reports/IncomeReport'
    //});
    //gtag('event', 'IncomeReport', {
    //    'event_category': 'IncomeReport',
    //    'event_label': 'IncomeReport'
    //});

    //setTimeout(function () {
    //    loadIncomeDetails();
    //}, 500);
    loadIncomeDetails();

    $("#ddlFinyear").change(function () {
        $("#hideshow").hide();
        loadIncomeDetails();
    });
    $("#ddlFamilyList").change(function () {
        $("#hideshow").hide();
        loadIncomeDetails();
    });
    $("#hideshow").hide();

});


//Loader
function completeajaxrequestIncome() {
    $("#waitInIncome").css("display", "none");
}
function startajaxrequestIncome() {
    $("#waitInIncome").css("display", "block");
}


function loadIncomeDetails() {
    var formdata = {
        "fin_year": $('#ddlFinyear :selected').val(),
        "fam_id": $('#ddlFamilyList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/ConsolidatedIncomeDetails";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestIncome();
        },
        success: function (data) {
            $("#IncomeDetailsAllClients").html("");
            var html = "";

            if (data.length > 0) {
                
                var groups = {};
                for (var i = 0; i < data.length; i++) {
                    var groupName = data[i].client_name;
                    if (!groups[groupName]) {
                        groups[groupName] = [];
                    }
                    groups[groupName].push(data[i]);
                }
                
                $.each(groups, function (index, value1) {
                    
                    html = html + "<p class='GrandTotalRow' style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold;'>" +
                        "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target='#div_" + value1[0].main_client_id + "' aria-expanded='false' aria-controls='collapseData'>" +
                        "<i class='far fa-plus-square fa-sm accordianButtons'></i> <i class='far fa-minus-square fa-sm accordianButtons'></i> </button> " +
                        index + "</p>" +
                        "<div class='collapse' data-parent='#IncomeDetailsAllClients' id='div_" + value1[0].main_client_id + "'> <div class='card card - body'> <table class='myCstDatatable table table-hover dt-responsive nowrap w-100'>" +
                        "<thead> <tr>" +
                        "<th class='TableHeadAlignment'>Subcategory</th>" +
                        "<th class='TableHeadAlignment'>Intraday<br />Profit /</b><b style='color: red;'>(Loss)</b></th>" +
                        "<th class='TableHeadAlignment'>Short Term<br />Profit /</b><b style='color: red;'>(Loss)</b></th>" +
                        "<th class='TableHeadAlignment'><p>Long Term<br />Profit /</b><b style='color: red;'>(Loss)</b><br>(Without Grandfathering)</p></th>" +
                        "<th class='TableHeadAlignment'><p>Long Term<br />Profit /</b><b style='color: red;'>(Loss)</b><br>(With Grandfathering)</p></th>" +
                        "<th class='TableHeadAlignment'>Dividend</th>" +
                        "</tr>" +
                        "</thead><tbody>";
                    $.each(value1, function (index1, value) {
                        
                        if (value.sub_cat_code == "") {
                            html = html + "<tr class='SubTotalRow'>" +
                                "<td class='GrandTotalRowDataWithoutBorders paraTextAlignLeft'>" + value.sub_category + "</td>" +
                                "<td class='GrandTotalRowDataWithoutBorders paraTextAlignRight" + isNegativeValue(value.int_real_profit) + "'>" + numberWithCommas(value.int_real_profit.toFixed()) + "</td>" +
                                "<td class='GrandTotalRowDataWithoutBorders paraTextAlignRight" + isNegativeValue(value.srt_real_profit) + "'>" + numberWithCommas(value.srt_real_profit.toFixed()) + "</td>" +
                                "<td class='GrandTotalRowDataWithoutBorders paraTextAlignRight" + isNegativeValue(value.lng_real_profit) + "'>" + numberWithCommas(value.lng_real_profit.toFixed()) + "</td>" +
                                "<td class='GrandTotalRowDataWithoutBorders paraTextAlignRight" + isNegativeValue(value.lng_real_profit_gf) + "'>" + numberWithCommas(value.lng_real_profit_gf.toFixed()) + "</td>" +
                                "<td class='GrandTotalRowDataWithoutBorders paraTextAlignRight" + isNegativeValue(value.dividend) + "'>" + numberWithCommas(value.dividend.toFixed()) + "</td>" +
                                "</tr>";
                        }
                        else {
                            html = html + "<tr>" +
                                "<td>" + value.sub_cat_code + "</td>" +
                                "<td class='paraTextAlignRight" + isNegativeValue(value.int_real_profit) + "'>" + numberWithCommas(value.int_real_profit.toFixed()) + "</td>" +
                                "<td class='paraTextAlignRight" + isNegativeValue(value.srt_real_profit) + "'>" + numberWithCommas(value.srt_real_profit.toFixed()) + "</td>" +
                                "<td class='paraTextAlignRight" + isNegativeValue(value.lng_real_profit) + "'>" + numberWithCommas(value.lng_real_profit.toFixed()) + "</td>" +
                                "<td class='paraTextAlignRight" + isNegativeValue(value.lng_real_profit_gf) + "'>" + numberWithCommas(value.lng_real_profit_gf.toFixed()) + "</td>" +
                                "<td class='paraTextAlignRight" + isNegativeValue(value.dividend) + "'>" + numberWithCommas(value.dividend.toFixed()) + "</td>" +
                                "</tr>";
                        }   
                    });
                    html = html + "</tbody></table></div></div>"
                });
                $("#IncomeDetailsAllClients").append(html);
            } else {
                $("#IncomeDetailsAllClients").html("No Records Found. Please select a family from above.");
            }
        },
        error: function (xhr, err) {
            // alert(xhr+"   ,  "+err);
        },
        complete: completeajaxrequestIncome()
    })
}