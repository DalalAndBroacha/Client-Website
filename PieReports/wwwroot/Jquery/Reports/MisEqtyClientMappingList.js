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

    let parameterObj = {
        "LoginId": $("#LoggedInUID").val(),
        "rm_id": $("#ddlBHRMList").val()
    }
    const url = "/ClientPortal/Reports/psp_dsp_mapping_client_list";

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

            $("#EqMisRmName").text($('#ddlBHRMList :selected').text());

            if (data.length > 0) {
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignCenter'>" + value.account_code + "</td>" +
                        "<td class='paraTextAlignLeft'>" + value.client_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + value.family_name + "</td>" +
                        "<td class='paraTextAlignLeft'>" + value.rm_name + "</td>" +
                        "<td class='paraTextAlignCenter' data-sort='" + formatDate_yyyymmdd(value.last_trade_date) + "'>";

                    if (value.last_trade_date == "1900-01-01T00:00:00") {
                        html = html + " </td>";
                    }
                    else {
                        html = html + formatDate(value.last_trade_date) + "</td>"
                    }
                        html = html + "</tr>";
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
            width: "10%",
            targets: 0
        },
        {
            width: "20%",
            targets: 1
        },
        {
            width: "20%",
            targets: 2
        },
        {
            width: "30%",
            targets: 3
            },
            {
                width: "20%",
                targets: 4
            }
        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries."
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        order: [
            [2, 'asc']
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
    let rm_id = $("#ddlBHRMList").val()

    let strPara = LoginId + "~" + rm_id;

    let strDescrip = "Client List for " + $('#ddlBHRMList :selected').text()

    PDFandExcelExport(evt_btn, "60", LoginId, "0", export_type, export_format, strPara, strDescrip);
}

$("#btnEmail").click(function () { //Email
    GenerateExportDetails("#btnEmail", "E", "EXCEL")
})

$("#btnExcel").click(function () { //Excel
    GenerateExportDetails("#btnExcel", "X", "EXCEL")
})