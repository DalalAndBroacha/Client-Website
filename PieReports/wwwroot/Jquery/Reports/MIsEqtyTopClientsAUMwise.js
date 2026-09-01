$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    $('#ddlBHRMList').select2({ dropdownCssClass: 'bigdrop' });
});

$("#ddlBHRMList").change(function () {
    $('#divDataTable').hide();
});

document.querySelector(".inpPositiveNumber").addEventListener("keypress", function (evt) {
    if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
        evt.preventDefault();
    }
});
function completeajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "block");
}
function ViewData() {

    let from_count = $("#inpFrom").val()
    let to_count = $("#inpTo").val()

    if (from_count != '' && to_count != '') {
        if (parseInt(to_count) < parseInt(from_count)) {
            alert("Invalid range selection!\nTo should be greater than From.")
        }
        else {
            let parameterObj = {
                "LoginId": $("#LoggedInUID").val(),
                "rm_id": $("#ddlBHRMList").val(),
                "from_rec": from_count,
                "to_rec": to_count
            }
            const url = "/ClientPortal/Reports/psp_dsp_top_clients_AUMwise";

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
                                "<td class='paraTextAlignLeft'>" + value.client_name + "</td>" +
                                "<td class='paraTextAlignCenter'>" + value.account_code + "</td>" +
                                "<td class='paraTextAlignCenter'>" + numberWithCommas(value.holding_value.toFixed(2)) + "</td>" +
                                "<td class='paraTextAlignCenter'>" + value.dsp_last_trade_date + "</td>" +
                                "<td class='paraTextAlignLeft'>" + value.branch + "</td>" +
                                "<td class='paraTextAlignLeft'>" + value.rm + "</td>" +
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
    }
    else {
        alert("Range cannot be empty.")
    }
}
function DrawDataTable() {

    $('#tblData').DataTable({
        autoWidth: false,
        columnDefs: [{
            width: "15%",
            targets: 0
        },
        {
            width: "5%",
            targets: 1
        },
        {
            width: "10%",
            targets: 2
        },
        {
            searchable: false,
            width: "5%",
            targets: 3
        },
        {
            width: "10%",
            targets: 4
        },
        {
            width: "20%",
            targets: 5
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
            [2, 'desc']
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
    let from_rec = $("#inpFrom").val()
    let to_rec = $("#inpTo").val()

    let strPara = from_rec + "~" + to_rec + "~" + LoginId + "~" + rm_id;

    let strDescrip = "AUMwise Client List for " +  $('#ddlBHRMList :selected').text()

    PDFandExcelExport(evt_btn, "59", LoginId, "0", export_type, export_format, strPara, strDescrip);
}
$("#btnEmail").click(function () { //Email
    GenerateExportDetails("#btnEmail", "E", "EXCEL")
})
$("#btnExcel").click(function () { //Excel
    GenerateExportDetails("#btnExcel", "X", "EXCEL")
})