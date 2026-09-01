$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    
    var d = new Date();
    d.setDate(d.getDate() - 1);

    var Tdaydate = d.toISOString().split('T')[0];

    $("#inpDate").val(Tdaydate);
    $('#inpDate').prop('disabled', true)

    $('#btnBulkEmailOutDebi').prop('disabled', true);

});
function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}
function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}

$("#inpDate").change(function () {
    $('#divOutDebi').hide();
});
function ViewDebiData() {

    let parameterObj = {
        "login_id": $("#LoggedInUID").val(),
        "trdate": $('#inpDate').val()
    }

    let PnLurl = "/ClientPortal/Pie/psp_dsp_client_coms_outstanding_debit_send_mail";
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
                let html = "";
                $("#tbodyOutDebi").html("");
                $('#tblData').DataTable().clear();
                $('#tblData').DataTable().destroy();
                
                //$('#tblData').DataTable().draw();

                console.log(data.length);

                //html = html + " <thead><tr><th><button id='selectAllDor' class='btn btn-sm DnB-RedLabel'>Select All</button></th><th>Name</th><th>Amount</th>" +
                //                "<th></th><th>History</th></tr></thead><tbody>"
                $.each(data, function (index, value) {
                    html = html + "<tr>" + 
                           "<td></td>" + //@* Required for Checkbox *@
                           "<td class='paraTextAlignCenter align-middle'>" + value.client_name + "</td>" +
                           "<td class='paraTextAlignCenter align-middle'>" + value.dsp_amount + "</td>" +
                            "<td class='paraTextAlignCenter align-middle'>" + value.account_code + "</td>" +
                           //"<td class='text-center'>" +
                           // "<div class='row justify-content-center'>" +
                           // "<button class='btn' type='button' onclick=\"FetchHistoryData(\'" + value.account_code + "\')\" title='History'>" +
                           //         "<i class='fas fa-history fa-lg White_Red_Icon'></i>" +
                           //     "</button>" +
                           // "</div>" +
                           //"</td>" +
                        "</tr>"
                });
                /*html = html + "</tbody>"*/
                $("#tbodyOutDebi").append(html);
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
    let example = $('#tblData').DataTable({
        autoWidth: false,
        //destroy:true,
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
            width: "10%",
            targets: 2
        },
        {
            visible: false,
            searchable: false,
            targets: 3
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
            [2, 'desc']
        ],
        "initComplete": function (settings, json) {
            $('#tblData').DataTable().columns.adjust().draw();
            $('#divOutDebi').show();
            $('#btnBulkEmailOutDebi').prop('disabled', false);
            $('#btnBulkEmailOutDebi').show();

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
        if (example.rows({
            selected: true
        }).count() !== example.rows().count()) {
            $("th.select-checkbox").removeClass("selected");
        } else {
            $("th.select-checkbox").addClass("selected");
        }
    });
}

$(document).on("click", "#btnBulkEmailOutDebi", function () {
    $('#btnBulkEmailOutDebi').prop('disabled', true);

    let table = $('#tblData').DataTable();
    let selectedData = $.map(table.rows('.selected').data(), function (item) {
        let dataObj = {
            "login_id": $("#LoggedInUID").val(),
            "account_code": item[3],
            "amount": item[2],
            "as_on_date": $('#inpDate').val()
        };
        return dataObj;
    });
    let rowsSelected = table.rows('.selected').data().length;

    if (rowsSelected > 0) {
        alert(rowsSelected + ' row(s) selected.');
        SendEmail(selectedData);
        location.reload();
    }
    else {
        alert(rowsSelected + ' row(s) selected. Please select clients.');
    }
});
function SendEmail(parameterObj) {

    const posturl = "/ClientPortal/Pie/OutDebitEmail";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(parameterObj),
        beforeSend: function (xhr) {
            startajaxrequestGlobal();
        },
        success: function (data) {
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

function FetchHistoryData(cli) {

    let formdata = {
        "client_code": cli,
        "content_id": "3"
    }

    let posturl = "/ClientPortal/Pie/psp_dsp_client_coms_history";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        beforeSend: function (xhr) {
            startajaxrequestGlobal();
        },
        success: function (data) {
            if (data.length > 0) {
                let html = "";
                $("#tbodyHistoryData").html("");
                $.each(data, function (index, value) {

                    html = html + "<tr>" +
                        "<td>" + value.login_Name + "</td>" +
                        "<td>" + formatDate(value.prev_comunicated_on) + "</td>" +
                        /*"<td>" + value.sms_status + "</td>" +*/
                        "<td>" + value.email_status + "</td>" +
                        "</tr>"
                });
                $("#tbodyHistoryData").append(html);
                completeajaxrequestGlobal();
                $('#historyModal').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                alert("No Data found.")
                completeajaxrequestGlobal();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })


}



