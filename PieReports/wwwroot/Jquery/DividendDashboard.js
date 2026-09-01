$(document).ready(function () {

    //window.dataLayer = window.dataLayer || [];
    //function gtag() { dataLayer.push(arguments); }
    //gtag('js', new Date());

    ////gtag('config', 'G-6FJ1RE3WC0'); 
    //gtag('config', 'G-6FJ1RE3WC0', {
    //    'page_title': 'Dividend',
    //    'page_path': '/Reports/DividendDetails'
    //});
    //gtag('event', 'DividendDetails', {
    //    'event_category': 'DividendDetails',
    //    'event_label': 'DividendDetails'
    //});

    $("#ddlFinyear").change(function () {
        loadDividentPieChart();
    });
    $("#ddlFamilyList").change(function () {
        loadDividentPieChart();
    });
    loadDividentPieChart();
});


function completeajaxrequest() {
    $("#DiviwaitIn").css("display", "none");
}
function startajaxrequest() {
    $("#DiviwaitIn").css("display", "block");
}

//psp_dsp_client_portal_dividend_piechart

function loadDividentPieChart() {

    $("#PIE_Chart").html("");

    var formdata = {
        "fin_year": $('#ddlFinyear :selected').val(),
        "family_id": $('#ddlFamilyList :selected').val()
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_client_portal_dividend_piechart";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        success: function (data) {

            if (data.length > 0) {
                var arrLabels = [], arrSeries = [ ];
                for (var i = 0; i < data.length; i++) {

                    arrLabels.push(data[i].sub_category);

                    arrSeries.push(data[i].value);
                }
                
                Donut_Chart(arrSeries, arrLabels);
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequest();
        }
    })
}

function Donut_Chart(seriesArray, labelsArray) {
    
    var options = {
        series: seriesArray,
        labels: labelsArray,
        chart: {
            type: 'donut',
            width: 380,
            height: 380
        },
        dataLabels: {
            enabled: true,
            textAnchor: 'end'
        },
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 50,
                    height: 50
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#PIE_Chart"), options);
    chart.render();
}