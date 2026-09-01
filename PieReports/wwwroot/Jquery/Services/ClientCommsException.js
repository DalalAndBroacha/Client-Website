$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    DrawDataTable();
});

function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}

$('#ddlExceptionList').on('change', function () {
    let selectedValue = $(this).val();
    let oTable  = $("#tblDataInvalid").DataTable();

    if (selectedValue != "All") {

        oTable.column(6).search(selectedValue).draw();
    }
    else {
        oTable.column(6).search('').draw();
    }

    $("#btnCliComExceptionMail").show()
    $("#btnCliComExceptionXls").show()
});

function DrawDataTable() {
     $("#tblDataInvalid").DataTable({
        "initComplete": function (settings, json) {
            $('#tblDataInvalid').DataTable().columns.adjust().draw();
            $('#divInvalid').show();
            completeajaxrequestGlobal();
        },
        // For more Info on DOM https://datatables.net/reference/option/dom
        // DOM example - https://stackoverflow.com/questions/39407881/pagination-at-top-and-bottom-with-datatables#:~:text=You%20can%20put%20pagination%20at,'p%3E%3E%22%2C%20%7D)%3B
        "dom": "<'row justify-content-between'<'col-sm-3'l><'col-sm-3'f><'col-sm-3'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
         "pageLength": globalDataGridPageLenght,
         "lengthMenu": [25, 50, 100],
         "pagingType": "full_numbers",
         autoWidth: false,
        columnDefs: [{
            width: "3%",
            targets: 0
        },
        {
            width: "17%",
            targets: 1
        },
        {
            orderable: false,
            searchable: false,
            width: "10%",
            targets: 2
        },
        {
            orderable: false,
            searchable: false,
            width: "15%",
            targets: 3
        },
        {
            width: "15%",
            targets: 4
        },
        {
            width: "10%",
            targets: 5
        },
        {
            width: "10%",
            targets: 6
        }],
        order: [
            [1, 'asc']
        ]
    });
}



function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()
    let ddlSelectedVal = $("#ddlExceptionList").val()

    let strPara = LoginId + "~" + ddlSelectedVal;

    let strDescrip = "Client Communication Exception Report";

    PDFandExcelExport(evt_btn, "58", LoginId, "0", export_type, export_format, strPara, strDescrip);
}

$("#btnCliComExceptionMail").click(function () {
    GenerateExportDetails("#btnCliComExceptionMail", "E", "EXCEL")
})
$("#btnCliComExceptionXls").click(function () {
    GenerateExportDetails("#btnCliComExceptionXls", "X", "EXCEL")
})