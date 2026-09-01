$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");

    HideFinYearList();
    HideFamilyList();
    FetchRequestList();
});


//$(document).on('shown.lte.pushmenu', handleExpandedEvent)


//function handleExpandedEvent() {
//    $("#req_table").hide()
//    $("#req_table").show()
//    $('#req_table').DataTable().columns.adjust().draw();
//}

//$("[data-widget='pushmenu']").toggle(
//    function () {
//        $('#req_table').DataTable().columns.adjust().draw();
//    }, function () {
//        $('#req_table').DataTable().columns.adjust().draw();
//    }
//);



function FetchRequestList() {

    var formdata = {
        "login_id": $("#LoggedInUID").val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_kyc_backoffice_requests";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            
            if (data.length > 0) {
                $("#req_table").DataTable().destroy();
                $("#tbodyReqTable").html("");
                var html = "";
                
                $.each(data, function (index, value) {
                   html = html + "<tr>" +
                       "<td name='req_id'>" + value.request_id +   "</td>" +
                       "<td>" + value.request_date + "</td>" +
                       "<td>" + value.client_name + "</td>" +
                       "<td name='acc_code'>" + value.account_code + "</td>" +
                       "<td><p>" + value.str_old_values + "</p></td>" +
                       "<td><p>" + value.str_new_values + "</p></td>" +
                       "<td><textarea style='resize: none;' rows='3' name='KYC_Remarks'></textarea></td>" +
                       "<td>" +
                            "<button class='btn Approve_req' type='button' title='Approve'><i style='color:forestgreen' class='fas fa-check-circle fa-lg'></i></button>" +
                            "<button class='btn Reject_req' type='button' title='Reject'><i style='color:darkred' class='fas fa-times-circle fa-lg'></i></button>" +
                       "</td>" +
                       "</tr>"
                });
                
                $("#tbodyReqTable").append(html);
                $('#req_table').DataTable({
                    "initComplete": function (settings, json) {
                        $("#noRecordsFound").hide()
                        $("#req_table").show()
                        $('#req_table').DataTable().columns.adjust().draw();
                    },
                    "order": [],
                    "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
                        //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false },
                        { "bSortable": false }
                    ],

                });
                
            }
            else {
                $("#req_table").DataTable().destroy();
                $("#req_table").hide()
                $("#noRecordsFound").show()
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}


function ApproveRejectReq(req_id, acc_code, remarks, status) {

    var formdata = {
        "request_id": req_id,
        "account_code": acc_code,
        "remarks": remarks,
        "status": status
    }

    var posturl = "/ClientPortal/Pie/psp_amd_kyc_backoffice_request_action";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            FetchRequestList();
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

$(document).on("click", ".Reject_req", function () {

    var textArea = $(this).parents('tr').find('textarea[name="KYC_Remarks"]')
    var req_id = $(this).parents('tr').find('td[name = "req_id"]').text()
    var acc_code = $(this).parents('tr').find('td[name = "acc_code"]').text()

    var remarks_value = textArea.val()
    
    if (remarks_value == "") {

        alert("Please enter the reason for rejection of request.")

        $(textArea).animate({ backgroundColor: '#FF0000' }, 'slow', function () {
            $(textArea).animate({ backgroundColor: '#FFFFFF' }, 'slow');
        });

        textArea.focus();
    }
    else {
        ApproveRejectReq(req_id, acc_code, remarks_value, "N")
    }


});

$(document).on("click", ".Approve_req", function () {
    var req_id = $(this).parents('tr').find('td[name = "req_id"]').text()
    var acc_code = $(this).parents('tr').find('td[name = "acc_code"]').text()
    var textArea = $(this).parents('tr').find('textarea[name="KYC_Remarks"]')

    var remarks_value = textArea.val()

    if (remarks_value == "") {
        alert("Please enter the reason for approval of request.")

        $(textArea).animate({ backgroundColor: '#FF0000' }, 'slow', function () {
            $(textArea).animate({ backgroundColor: '#FFFFFF' }, 'slow');
        });

        textArea.focus();
    }
    else {
        ApproveRejectReq(req_id, acc_code, remarks_value, "Y")
    }
    
});