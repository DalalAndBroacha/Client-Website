$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    $('#ddlBHRMList').select2({ dropdownCssClass: 'bigdrop' });
});
$("#ddlBHRMList").change(function () {
    $('#divDataTable').hide();
});
function completeajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "block");
}
function ViewData() {

    const rm_id = $("#ddlBHRMList").val()

    let parameterObj = {
        "login_id": rm_id
    }
    const url = "/ClientPortal/Reports/psp_dsp_direct_equity_top_holding";

    $.ajax({
        url: url,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(parameterObj),
        beforeSend: function (xhr) {
            startajaxrequestwaitIn();
        },
        success: function (data) {

            $('#tblData').DataTable().destroy();
            $("#tbodyData").html("");
            let html = "";

            $("#EqMisScripFor").text($('#ddlBHRMList :selected').text());

            console.log(data);

            if (data.length > 0) {
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignLeft'>" + value.scrip_industry + "</td>" +
                        "<td class='paraTextAlignLeft'><a target='_blank' style='text-decoration:underline;' href='/ClientPortal/Reports/MisEqtyScripHoldingClientList?scrip_code="
                        + value.scrip_code + "&rm_id=" + rm_id + "'>" + value.scrip_name + "</a></td>" +
                        "<td class='paraTextAlignCenter'>" + value.top_Picks + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.client_count) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_qty.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.holding_value.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.minimum_return_abs.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.maximum_return_abs.toFixed(2)) + "</td>" +
                        "<td class='paraTextAlignRight'>" + numberWithCommas(value.avg_return_abs.toFixed(2)) + "</td>" +
                        "</tr>"
                });


                $("#tbodyData").append(html);

                DrawDataTable();


            }
            else {
                alert("No data found.");
            }
        },
        complete: function (xhr) {
            completeajaxrequestwaitIn();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function DrawDataTable() {

    $('#tblData').DataTable({
        autoWidth: false,
        columnDefs: [{
            width: "15%",
            targets: 0
        },
        {
            width: "15%",
            targets: 1
        },
        {
            width: "10%",
            targets: 2
        },
        {
            searchable: false,
            width: "10%",
            targets: 3
        },
        {
            searchable: false,
            width: "10%",
            targets: 4
        },
        {
            searchable: false,
            width: "10%",
            targets: 5
        },
        {
            searchable: false,
            width: "5%",
            targets: 6
        },
        {
            searchable: false,
            width: "5%",
            targets: 7
        },
        {
            searchable: false,
            width: "5%",
            targets: 8
        }

        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries. "
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        order: [
            [5, 'desc']
        ],
        "initComplete": function (settings, json) {
            $('#tblData').DataTable().columns.adjust().draw();

            $('#btnEmail').show();
            $('#btnExcel').show();

            $('#divDataTable').show();
        }
    });
}

function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()

    let strPara = LoginId + "~0";

    let strDescrip = "Scrip Summary Holding  of " + $("#EqMisScripFor").text()

    PDFandExcelExport(evt_btn, "25", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

$("#btnEmail").click(function () { //Email
    GenerateExportDetails("#btnEmail", "E", "EXCEL")
})

$("#btnExcel").click(function () { //Excel
    GenerateExportDetails("#btnExcel", "X", "EXCEL")
})