$(window).bind("load", function () {

    //window.dataLayer = window.dataLayer || [];
    //function gtag() { dataLayer.push(arguments); }
    //gtag('js', new Date());

    ////gtag('config', 'G-6FJ1RE3WC0'); 
    //gtag('config', 'G-6FJ1RE3WC0', {
    //    'page_title': 'DownloadReports',
    //    'page_path': '/ClientPortal/Pie/Download_Reports'
    //});
    //gtag('event', 'DownloadReports', {
    //    'event_category': 'DownloadReports',
    //    'event_label': 'DownloadReports'
    //});

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    fn_get_download_destails();
    fnDisableFinyearList();
    fnDisableFamilyList();

    var time = 15
    // WCAG 2.2.1 / 2.2.2 — the automatic reload can be stopped and resumed.
    var autoRefresh = true;

    setInterval(function () {

        if (!autoRefresh) { return; }

        time--;

        $('#timer').html("Page will auto reload in " + time + " seconds.");

        if (time === 0) {

            location.reload()
        }


    }, 1000);

    $('#btnAutoRefresh').on('click', function () {
        autoRefresh = !autoRefresh;
        if (autoRefresh) {
            time = 15;
            $(this).text('Stop automatic refresh');
            if (window.a11y) { window.a11y.announce('Automatic refresh resumed.'); }
        } else {
            $('#timer').text('Automatic refresh is off.');
            $(this).text('Resume automatic refresh');
            if (window.a11y) { window.a11y.announce('Automatic refresh stopped.'); }
        }
    });

});

//Loader
function completeajaxrequest() {
    $("#DwnRepowaitIn").css("display", "none");
}
function startajaxrequest() {
    $("#DwnRepowaitIn").css("display", "block");
}
//Loader


function fn_get_download_destails() {

    var formdata = {
        "requester_id": $("#LoggedInUID").val()
    }
    
    var posturl = "/ClientPortal/Exports/psp_dsp_client_portal_downloads_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequest();
        },
        data: JSON.stringify(formdata),
        success: function (data) {
            var html = "";
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    html = html + "<tr><td class='tableDataWithRedBorders txtDes'>" + data[i].description + "</td>" +
                        "<td class='tableDataWithRedBorders' style='text-align:center;'><a class='btn-sm btn-info btnCopyDes'>Copy</a></td>" +
                        "<td class='tableDataWithRedBorders' style='text-align:center;'>" + data[i].export_format + "</td>"
                    if (data[i].status == "N") {
                        html = html + "<td class='tableDataWithRedBorders' style='text-align:center;'>Pending</td>"
                    }
                    else if (data[i].status == "Y") {
                        html = html + "<td class='tableDataWithRedBorders' style='text-align:center;'><a class='btn-sm btn-success' target='_blank' href='getDocument?FP=" + data[i].download_file + "'>Download</a></td>"
                        //html = html + "<td class='tableDataWithRedBorders' style='text-align:center;'><a class='btn-sm btn-success' target='_blank' href='" + data[i].download_file + "'>Download</a></td>"
                    }
                    else if (data[i].status == "E") {
                        html = html + "<td class='tableDataWithRedBorders' style='text-align:center;'>Error</td>"
                    }
                    html = html + "</tr>"
                }
                $("#tbodyDownloadsTable").append(html);

                $("#main_Div").show();
            }
            else {
                $("#noDwnldsFound").html("");
                $("#noDwnldsFound").append("<h3>No Downloads Found</h3>");
            }
            
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: completeajaxrequest
    })
}

$(document).on("click", ".btnCopyDes", function () {

    let txtDesc = $(this).closest('tr').find('.txtDes').text();

    navigator.clipboard.writeText(txtDesc);

});