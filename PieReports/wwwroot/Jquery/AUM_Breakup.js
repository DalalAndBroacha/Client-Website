$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    FetchAUMBrkUpList();

});
function completeajaxrequestwaitIn() {
    $("#waitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#waitIn").css("display", "block");
}

    
function FetchAUMBrkUpList() {
    var posturl = "/ClientPortal/Reports/psp_dsp_aum_break_up";
    $.ajax({
        url: posturl,
        type: "get",
        contentType: "application/json",
        data: JSON.stringify(),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {
            if (data.length > 0) {

                $('#tableAUM_Brkup').DataTable({
                    "initComplete": function (settings, json) {
                        completeajaxrequestwaitIn();
                        $("#report_header_and_buttons").show();
                        $("#tableAUM_Brkup").show();
                        
                    },
                    "destroy": true,
                    "pagingType": "full_numbers",
                    "data": data,
                    "columns": [
                        {
                            data: "branch",
                            render: function (data, type, src) {
                                return "<p>" + src.branch + "</p>";
                            }
                        },
                        {
                            data: "family_name",
                            render: function (data, type, src) {
                                return "<p>" + src.family_name + "</p>";
                            }
                        },
                        {
                            data: "client_anme",
                            render: function (data, type, src) {
                                return "<p>" + src.client_anme + "</p>";
                            }
                        },
                        {
                            data: "rm",
                            render: function (data, type, src) {
                                return "<p>" + src.rm + "</p>";
                            }
                        },
                        {
                            data: "total",
                            render: function (data, type, src) {
                                if (src.total < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.total.toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas(src.total.toFixed(2)) + "</p>";
                            },
                        },
                        {
                            data: "direct_Equity",
                            render: function (data, type, src) {
                                if (src.direct_Equity < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.direct_Equity.toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas(src.direct_Equity.toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: null,
                            render: function (data, type, src) {
                                if (src.direct_Equity < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas((src.direct_Equity / src.total * 100).toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas((src.direct_Equity / src.total * 100).toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: "equity_PMS",
                            render: function (data, type, src) {
                                if (src.direct_Equity < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.equity_PMS.toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas(src.equity_PMS.toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: null,
                            render: function (data, type, src) {
                                if (src.direct_Equity < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas((src.equity_PMS / src.total * 100).toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas((src.equity_PMS / src.total * 100).toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: "equity_MF",
                            render: function (data, type, src) {
                                if (src.direct_Equity < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.equity_MF.toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas(src.equity_MF.toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: null,
                            render: function (data, type, src) {
                                if (src.equity_MF < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas((src.equity_MF / src.total * 100).toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas((src.equity_MF / src.total * 100).toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: "debt_MF",
                            render: function (data, type, src) {
                                if (src.debt_MF < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.debt_MF.toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas(src.debt_MF.toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: null,
                            render: function (data, type, src) {
                                if (src.debt_MF < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas((src.debt_MF / src.total * 100).toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas((src.debt_MF / src.total * 100).toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: "bonds",
                            render: function (data, type, src) {
                                if (src.bonds < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas(src.bonds.toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas(src.bonds.toFixed(2)) + "</p>";
                            }
                        },
                        {
                            data: null,
                            render: function (data, type, src) {
                                if (src.bonds < 0) {
                                    return "<p class='negavtiveValueField paraTextAlignRight'>" + numberWithCommas((src.bonds / src.total * 100).toFixed(2)) + "</p>";
                                }
                                return "<p class='paraTextAlignRight'>" + numberWithCommas((src.bonds / src.total * 100).toFixed(2)) + "</p>";
                            }
                        }
                    ]

                });
            }
            else {
                $("#norecordsMISFamDeatils").append("No Record Found");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

//**** Button Clicks ****
$("#btnEmailAUMBrkUp").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();

    var strPara = LoginId;

    var strDescrip = "AUM Breakup";

    PDFandExcelExport("#btnEmailAUMBrkUp", "21", LoginId, famId, "E", "PDF", strPara, strDescrip);

})

$("#btnPDFAUMBrkUp").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();

    var strPara = LoginId;

    var strDescrip = "AUM Breakup";

    PDFandExcelExport("#btnPDFAUMBrkUp", "21", LoginId, famId, "X", "PDF", strPara, strDescrip);

})

$("#btnExportAUMBrkUp").click(function () {

    var famId = "0";
    var LoginId = $("#LoggedInUID").val();

    var strPara = LoginId;

    var strDescrip = "AUM Breakup";

    PDFandExcelExport("#btnExportAUMBrkUp", "21", LoginId, famId, "X", "EXCEL", strPara, strDescrip);

})
//**** Button Clicks ****