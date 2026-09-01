$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    DrawDataTable();

    $('#btnBulkEmail').hide();
});

function DrawDataTable() {

    let example = $('#tblData').DataTable({
        autoWidth: false,
        columnDefs: [{
            orderable: false,
            searchable: false,
            className: 'select-checkbox',
            width: "5%",
            targets: 0
        },
            {
                width: "15%",
                targets: 1
            },
            {
                orderable: false,
                width: "5%",
                targets: 2
            },
            {
                orderable: false,
                width: "10%",
                targets: 3
            },
            {
                orderable: false,
                width: "10%",
                targets: 4
            },
            {
                orderable: false,
                searchable: false,
                width: "20%",
                targets: 5
            }
        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        select: {
            style: 'multi',
            selector: 'td:first-child'
        },
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries. "
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        //dom: '<"top"i>rt<"bottom"flp><"clear">',
        order: [
            [1, 'asc']
        ],
        "initComplete": function (settings, json) {
            $('#tblData').DataTable().columns.adjust().draw();
            $('#divDorData').show();
            completeajaxrequestGlobal();
        }
    });
    example.on("click", "th.select-checkbox", function () {
        if ($("th.select-checkbox").hasClass("selected")) {
            example.rows().deselect();
            $("th.select-checkbox").removeClass("selected");
        } else {
            example.rows().select();
            $("th.select-checkbox").addClass("selected");
        }
    }).on("select deselect", function () {
        ("Some selection or deselection going on")
        if (example.rows({
            selected: true
        }).count() !== example.rows().count()) {
            $("th.select-checkbox").removeClass("selected");
        } else {
            $("th.select-checkbox").addClass("selected");
        }
    });

    let tblBeDor = $('#tblDataBeDor').DataTable({
        autoWidth: false,
        columnDefs: [{
            orderable: false,
            searchable: false,
            className: 'select-checkbox',
            width: "5%",
            targets: 0
        },
        {
            width: "15%",
            targets: 1
        },
        {
            orderable: false,
            width: "5%",
            targets: 2
        },
        {
            orderable: false,
            width: "10%",
            targets: 3
        },
        {
            orderable: false,
            width: "15%",
            targets: 4
        },
        {
            orderable: false,
            searchable: false,
            width: "15%",
            targets: 5
        },
        {
            orderable: false,
            searchable: false,
            width: "5%",
            targets: 6
        }
        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        select: {
            style: 'multi',
            selector: 'td:first-child'
        },
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries. "
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        //dom: '<"top"i>rt<"bottom"flp><"clear">',
        order: [
            [1, 'asc']
        ],
        "initComplete": function (settings, json) {
            $('#tblDataBeDor').DataTable().columns.adjust().draw();
            $('#divBeDorData').show();
            completeajaxrequestGlobal();
        }
    });
    tblBeDor.on("click", "th.select-checkbox", function () {
        if ($("th.select-checkbox").hasClass("selected")) {
            tblBeDor.rows().deselect();
            $("th.select-checkbox").removeClass("selected");
        } else {
            tblBeDor.rows().select();
            $("th.select-checkbox").addClass("selected");
        }
    }).on("select deselect", function () {
        ("Some selection or deselection going on")
        if (tblBeDor.rows({
            selected: true
        }).count() !== tblBeDor.rows().count()) {
            $("th.select-checkbox").removeClass("selected");
        } else {
            $("th.select-checkbox").addClass("selected");
        }
    });
}
function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}
function SendEmail(parameterObj) {

    const posturl = "/ClientPortal/Pie/DormantAccountEmail";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(parameterObj),
        beforeSend: function (xhr) {
            startajaxrequestGlobal();
        },
        success: function (data) {

            //data.sqlmsg
            alert("E-mail request processed.");
            location.reload();
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestGlobal();
        }
    })
}

$(document).on("click", "#btnBulkEmail", function () {
    let table = $('#tblData').DataTable();
    let selectedData = $.map(table.rows('.selected').data(), function (item) {
        let dataObj = {
            "login_id": $("#LoggedInUID").val(),
            "content_id": "4",
            "code": item[2],
            "client_category": item[3]
        };
        return dataObj;
    });
    let rowsSelected = table.rows('.selected').data().length;

    if (rowsSelected > 0) {
        SendEmail(selectedData);
        alert(rowsSelected + ' row(s) selected.');
    }
    else {
        alert(rowsSelected + ' row(s) selected. Please select clients.');
    }
});

//$(document).on("click", "#btnBeComDorBulkEmail", function () {
//    let table = $('#tblDataBeDor').DataTable();
//    let selectedData = $.map(table.rows('.selected').data(), function (item) {
//        let dataObj = {
//            "login_id": $("#LoggedInUID").val(),
//            "content_id": "2",
//            "name": item[1],
//            "code": item[2],
//            "mobile": item[3],
//            "email": item[4]
//        };
//        return dataObj;
//    });
//    let rowsSelected = table.rows('.selected').data().length;

//    if (rowsSelected > 0) {
//        SendEmail(selectedData);
//        alert(rowsSelected + ' row(s) selected.');
//    }
//    else {
//        alert(rowsSelected + ' row(s) selected. Please select clients.');
//    }
//});

//function FetchHistoryData(cli, content_id) {

//    let formdata = {
//        "client_code": cli,
//        "content_id": content_id
//    }

//    let posturl = "/ClientPortal/Pie/psp_dsp_client_coms_history";
//    $.ajax({
//        url: posturl,
//        type: "post",
//        contentType: "application/json",
//        data: JSON.stringify(formdata),
//        beforeSend: function (xhr) {
//            startajaxrequestGlobal();
//        },
//        success: function (data) {
//            if (data.length > 0) {
//                let html = "";
//                $("#tbodyHistoryData").html("");
//                $.each(data, function (index, value) {

//                    html = html + "<tr>" +
//                        "<td>" + value.login_Name + "</td>" +
//                        "<td>" + formatDate(value.prev_comunicated_on) + "</td>" +
//                        "<td>" + value.sms_status + "</td>" +
//                        "<td>" + value.email_status + "</td>" +
//                        "</tr>"
//                });
//                $("#tbodyHistoryData").append(html);
//                completeajaxrequestGlobal();
//                $('#historyModal').modal({ backdrop: 'static', keyboard: false });
//            }
//            else {
//                alert("No Data found.")
//                completeajaxrequestGlobal();
//            }
//        },
//        error: function (xhr, err) {
//            alert(err)
//        }
//    })


//}