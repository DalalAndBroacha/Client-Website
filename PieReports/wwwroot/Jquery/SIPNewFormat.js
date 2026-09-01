$(document).ready(function () {

    window.dataLayer = window.dataLayer || [];
    //function gtag() { dataLayer.push(arguments); }
    //gtag('js', new Date());

    ////gtag('config', 'G-6FJ1RE3WC0');
    //gtag('config', 'G-6FJ1RE3WC0', {
    //    'page_title': 'SIPtable',
    //    'page_path': '/Reports/SIPtable'
    //});
    //gtag('event', 'SIPtable', {
    //    'event_category': 'SIPtable',
    //    'event_label': 'SIPtable'
    //});
    //startajaxrequestwaitInSIP();
    //CollapseSideMenu();
    $('#Recent_Div').hide();
    $('#Terminating_Div').hide();
    SIP_Dashboard_Details();
});


function SIP_Dashboard_Details() {

    const posturl = "/ClientPortal/Reports/TestSIPDashboard";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {

                $("#TotalSIPs").text(numberWithCommas(data[0].sip_cnt));
                $("#TotalAmount").text(numberWithCommas(data[0].sip_total));
                $("#RecentSIPs").text(numberWithCommas(data[0].recent_sip_cnt));
                $("#RecentAmount").text(numberWithCommas(data[0].recent_sip_total));
                $("#ExSIPs").text(numberWithCommas(data[0].terminating_sip_cnt));
                $("#ExAmount").text(numberWithCommas(data[0].terminating_sip_total));

                $('#txtDetailsFlag').val("A");
                $('#txtFlagDescrip').val("All");
            }
            else {
                $("#TotalSIPs").text("0");
                $("#TotalAmount").text("0");
                $("#RecentSIPs").text("0");
                $("#RecentAmount").text("0");
                $("#ExSIPs").text("0");
                $("#ExAmount").text("0");

                $('#btnSIPExportPdf').hide();
                $('#btnSIPExportXls').hide();
            }
        },
        error: function (xhr, err) {
        },
        complete: function (xhr) {
            completeajaxrequestSIP();
            $('#All_Div').show();
        }
    })
}


function ShowTableData(table) {
    if (table == "All")
    {
        $('#All_Div').show();

        $('#btnSIPExportPdf').show();
        $('#btnSIPExportXls').show();


        $('#txtDetailsFlag').val("A");
        $('#txtFlagDescrip').val(table);
        

        $('#Recent_Div').hide();
        $('#Terminating_Div').hide();

        $("#Card_All_SIP").addClass("GrandTotalRow");
        $("#Card_Recent_SIP").removeClass("GrandTotalRow");
        $("#Card_Tert_SIP").removeClass("GrandTotalRow");

    }
    else if (table == "Recent") {
        $('#Recent_Div').show();

        $('#btnSIPExportPdf').show();
        $('#btnSIPExportXls').show();

        $('#txtDetailsFlag').val("R");
        $('#txtFlagDescrip').val(table);

        $('#Terminating_Div').hide();
        $('#All_Div').hide();

        $("#Card_Recent_SIP").addClass("GrandTotalRow");
        $("#Card_All_SIP").removeClass("GrandTotalRow");
        $("#Card_Tert_SIP").removeClass("GrandTotalRow");
    }
    else if (table == "Terminating") {
        $('#Terminating_Div').show();

        $('#btnSIPExportPdf').show();
        $('#btnSIPExportXls').show();

        $('#txtDetailsFlag').val("T");
        $('#txtFlagDescrip').val(table);

        $('#All_Div').hide();
        $('#Recent_Div').hide();

        $("#Card_Tert_SIP").addClass("GrandTotalRow");
        $("#Card_Recent_SIP").removeClass("GrandTotalRow");
        $("#Card_All_SIP").removeClass("GrandTotalRow");
    }
}

$("#btnSIPExportPdf").click(function () {

    let LoginId = $("#LoggedInUID").val()
    let flag = $("#txtDetailsFlag").val();

    let strPara = LoginId + "~" + flag;

    let strDescrip = $('#txtFlagDescrip').val() + " SIP Details"

    PDFandExcelExport("#btnSIPExportPdf", "37", LoginId, "0", "X", "PDF", strPara, strDescrip);
})

$("#btnSIPExportXls").click(function () {

    let LoginId = $("#LoggedInUID").val()
    let flag = $("#txtDetailsFlag").val();

    let strPara = LoginId + "~" + flag;

    let strDescrip = $('#txtFlagDescrip').val() + " SIP Details"

    PDFandExcelExport("#btnSIPExportXls", "37", LoginId, "0", "X", "EXCEL", strPara, strDescrip);
})
