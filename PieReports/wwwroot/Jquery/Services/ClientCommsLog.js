$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    let d = new Date();
    d.setDate(d.getDate());

    let d30 = new Date();
    d30.setDate(d.getDate() - 30);

    let d366 = new Date();
    d366.setDate(d.getDate() - 366);

    let Tdaydate = d.toISOString().split('T')[0];
    let Date30 = d30.toISOString().split('T')[0];
    let Date366 = d366.toISOString().split('T')[0];

    $("#inpFromDate").val(Date30);
    $("#inpToDate").val(Tdaydate);

    $("#inpToDate").attr("max", Tdaydate);

    $("#inpFromDate").attr("max", Tdaydate);
    $("#inpFromDate").attr("min", Date366);
    
});


function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}

$("#inpFromDate").change(function () {
    $('#divLogData').hide();
});

$("#inpToDate").change(function () {
    $('#divLogData').hide();
});

function ViewLogData() {

    let parameterObj = {
        "login_id": $("#LoggedInUID").val(),
        "from_date": $('#inpFromDate').val(),
        "to_date": $('#inpToDate').val()
    }

    let PnLurl = "/ClientPortal/Pie/psp_dsp_client_communication_log";
    $.ajax({
        url: PnLurl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(parameterObj),
        beforeSend: function (xhr) {
            startajaxrequestGlobal();
        },
        success: function (data) {
            if (data.length > 0) {

                $('#divLogData').show();

                let html = "";
                $("#tbodyLogData").html("");
                $('#tblLogData').DataTable().clear();
                $('#tblLogData').DataTable().destroy();

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td class='paraTextAlignCenter align-middle' data-sort='" + value.sortDate +"'>" + value.dsp_sent_date + "</td>" +
                        "<td class='paraTextAlignCenter align-middle'>" + value.client_name + "</td>" +
                        "<td class='paraTextAlignCenter align-middle'>" + value.account_code + "</td>" +
                        "<td class='paraTextAlignCenter align-middle'>" + value.sender + "</td>" +
                        "<td class='paraTextAlignCenter align-middle'>" + value.content_name + "</td>" +
                        "<td class='paraTextAlignCenter align-middle'>" + value.comm_type + "</td>" +
                        "<td class='paraTextAlignCenter align-middle'>" + value.status + "</td>" +
                        "</tr>"
                });
                
                $("#tbodyLogData").append(html);
                DrawDataTable();
            }
            else {
                alert("No data")
            }
        },
        complete: function (xhr) {
            completeajaxrequestGlobal();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}
function DrawDataTable() {
    $('#tblLogData').DataTable({
        autoWidth: false,
        columnDefs: [{
            searchable: false,
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
                width: "10%",
                targets: 3
            },
            {
            width: "10%",
            searchable: false,
            targets: 4
            },
            {
                width: "10%",
                searchable: false,
                targets: 5
            },
        {
                width: "10%",
                searchable: false,
                targets: 6
            }
        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        order: [
            [0, 'desc']
        ],
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries. "
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "initComplete": function (settings, json) {
            $('#tblLogData').DataTable().columns.adjust().draw();
            $('#divLogData').show();

            completeajaxrequestGlobal();
        }
    });
}

function ExportToExcel() {

    let LoginId = $("#LoggedInUID").val()
    let from_date = $('#inpFromDate').val();
    let to_date = $('#inpToDate').val();

    let dsp_from_date = from_date.split("-").reverse().join("-");
    let dsp_to_date = to_date.split("-").reverse().join("-");


    let strPara = LoginId + "~" + from_date + "~" + to_date;

    let strDescrip = "Client Communication Log for period " + dsp_from_date + " to " + dsp_to_date

    PDFandExcelExport("btnXls", "70", LoginId, "0", "X", "EXCEL", strPara, strDescrip);

}