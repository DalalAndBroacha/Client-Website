$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    FetchBranchList();

});

$("#ddlBranch").change(function () {
    FetchRMList();
});


function FetchBranchList() {

    var formdata = {
        "LoginId": $("#LoggedInUID").val(),
        "cat_type": "2"
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_branch_list";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlBranch").append("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.id + ">" + value.name + "</option>";
            });
            $("#ddlBranch").append(html);
            $('#ddlBranch').select2();
            FetchRMList();

        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchRMList() {
    var formdata = {
        "Branch": $('#ddlBranch :selected').val()
    }
    var posturl = "/ClientPortal/Pie/RMList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            $("#ddlRM").html("");
            var html = "";
            $.each(data, function (index, value) {
                html = html + "<option value=" + value.rm_id + ">" + value.rm_login_name + "</option>";
            });
            $("#ddlRM").append(html);
            $('#ddlRM').select2();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetData() {

    var formdata = {
        "Branch": $('#ddlBranch :selected').val(),
        "RM": $('#ddlRM :selected').val()
    }
    var posturl = "/ClientPortal/Reports/psp_rpt_client_bond_mismatch";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {

            if (data.length > 0) {
                var html = "";
                $("#tableBondsExcep").DataTable().destroy();
                $("#divBondExcep").html("");

                html = html + "<table id='tableBondsExcep' class='table table-hover table-bordered'><thead><tr>" +
                    "<th>Branch Name</th>" +
                    "<th>RM Name</th>" +
                    "<th>Family Name</th>" +
                    "<th>Client Code</th>" +
                    "<th>Client Name</th>" +
                    "<th>Script Name</th>" +
                    "<th>CDSL Qty</th>" +
                    "<th>Pie Qty</th>" +
                    "<th>Latest Buy</th>" +
                    "<th>Latest Sell</th>" +
                    "<th>Remarks</th>" +
                    "</tr></thead><tbody>";

                for (var i = 0; i < data.length; i++) {

                    html = html + "<tr>" +
                        "<td class='paraTextAlignLeft'>" + data[i].family_branch_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].rm_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].family_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].client_code + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].client_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].scrip_name + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].dp_holding_qty.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].holding_qty.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].latest_buy.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(data[i].latest_sell.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignLeft'>" + data[i].remarks + "</td>" +
                        "</tr>"
                }

                html = html + "</tbody></table>";

                $("#divBondExcep").append(html);

                $('#tableBondsExcep').DataTable({
                    "initComplete": function (settings, json) {
                        $("#norecordsfound").hide();
                        $("#div_data").show();
                        $("#retroHeader").hide();
                        $('#tableBondsExcep').DataTable().columns.adjust().draw();
                    },
                    "order": [],
                    "scrollX": true,
                    "pageLength": globalDataGridPageLenght,
                    "lengthMenu": [3, 5, 10, 25, 50, 100],
                });
            }

            else {
                $("#retroHeader").show();
                $("#div_data").hide();
                $("#norecordsfound").show();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

//Exports
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    var LoginId = $("#LoggedInUID").val();
    var Branch = $('#ddlBranch :selected').val();
    var RM = $('#ddlRM :selected').val();

    var strPara = LoginId + "~" + Branch + "~" + RM;

    var strDescrip = "Bonds Holding Exception Report for " + $('#ddlRM :selected').text();

    PDFandExcelExport(evt_btn, "29", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

$("#btnEMailBondCliExcep").click(function () {

    GenerateExportDetails("#btnEMailBondCliExcep", "E", "PDF")

})

$("#btnBondCliExcepExportPdf").click(function () {

    GenerateExportDetails("#btnBondCliExcepExportPdf", "X", "PDF")

})

$("#btnBondCliExcepExportExcel").click(function () {

    GenerateExportDetails("#btnBondCliExcepExportExcel", "X", "EXCEL")

})
//Exports