$(document).ready(function () {

    //window.dataLayer = window.dataLayer || [];
    //function gtag() { dataLayer.push(arguments); }
    //gtag('js', new Date());

    ////gtag('config', 'G-6FJ1RE3WC0'); 
    //gtag('config', 'G-6FJ1RE3WC0', {
    //    'page_title': 'XIRR_All_Clients',
    //    'page_path': '/Reports/SIPtable'
    //});
    //gtag('event', 'XIRR_All_Clients', {
    //    'event_category': 'XIRR_All_Clients',
    //    'event_label': 'XIRR_All_Clients'
    //});
    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    ReportList();
  
});

$("#ddlExportType").change(function () {

    $("#btnGenRepo").show();

});

$("#ddlRepot").change(function () {

    $("#btnGenRepo").show();

});

function generateReport() {

    let exportType = $('#ddlExportType :selected').val();
    let LoginId = $("#LoggedInUID").val(); 
    let report_id = $('#ddlRepot :selected').val();
    let strPara = ""; 
    let strDescrip = "";

    if (report_id == "40") {
        strPara = LoginId + "~" + "3";
        strDescrip = "Bond Cash Flow for past 3 months.";
    }
    else if (report_id == "41") {
        strPara = LoginId;
        strDescrip = "Equity all client holding.";
    }

    if (exportType == "1") {
        PDFandExcelExport("#btnGenRepo", report_id, LoginId, "0", "X", "PDF", strPara, strDescrip);
    }
    else if (exportType == "2") {
        PDFandExcelExport("#btnGenRepo", report_id, LoginId, "0", "X", "EXCEL", strPara, strDescrip);
    }
}

function ReportList() {
    var formdata = {}
    var posturl = "/ClientPortal/Pie/psp_dsp_common_report_module_list";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),

        success: function (data) {
            if (data.length > 0) {
                $("#ddlRepot").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.report_id + ">" + value.report_name + "</option>";
                });
                
                $("#ddlRepot").append(html);
                
            }
            else {
                alert("Error in rendering reports.")   
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}