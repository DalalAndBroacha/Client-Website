$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
   

    GetData();

    //psp_dsp_client_portfolio_interaction_history

});

function startajaxrequestCliAct() {
    $("#MISCliActwaitIn").css("display", "block");
}
function completeajaxrequestCliAct() {
    $("#MISCliActwaitIn").css("display", "none");
}

function GetData() {
    var formdata = {
        "login_id": $("#LoggedInUID").val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_client_portal_mis_recent_client_activity";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequestCliAct();
        },
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#tblMain").DataTable().destroy();
                $("#tbodyMainTbl").html("");
                var html = "";

                $.each(data, function (index, value) {

                    html = html + "<tr>" +
                        "<td>" + value.client_name + "</td>" +
                        "<td class='paraTextAlignCenter' data-sort='" + formatDate_yyyymmdd(value.eqt_last_trade_date) + "'>" + (formatDate_yyyymmdd(value.eqt_last_trade_date) == "19000101" ? "-" : formatDate(value.eqt_last_trade_date)) + "</td>" +
                        "<td class='paraTextAlignCenter' data-sort='" + formatDate_yyyymmdd(value.mf_last_trade_date) + "'>" + (formatDate_yyyymmdd(value.mf_last_trade_date) == "19000101" ? "-" : formatDate(value.mf_last_trade_date)) + "</td>" +
                        "<td class='paraTextAlignLeft' data-sort='" + formatDate_yyyymmdd(value.last_review_date) + "'><p>";
                        
                    if (value.remarks != "") {
                        html = html +
                            "<span><b>Remarks</b>: " + value.remarks + "</span><br>" +
                            "<span class='row justify-content-start' style='font-size:10px;'>" +
                            "<span class='ml-2'><b>By</b>: " + value.last_reviewed_by + "</span>" +
                            "<span class='ml-2'><b>On</b>: " + formatDate(value.last_review_date) + "</span>" +
                            "</span><br>";
                        if (value.history_flag == 'Y') {
                            html = html +
                                "<span class='row justify-content-center'><button class='btn btn-info btn-sm' onclick=\"FetchHistoryData(\'" + value.main_client_id + "\')\">History</button>" +
                                "<button class='btn btn-success btn-sm ml-3'  onclick=\"FetchAddData(\'" + value.main_client_id + "\')\">Add</button></span>" +
                                "</p></td>" +
                                "</tr>";
                        }
                        else {
                            html = html +
                                "<span class='row justify-content-center'>" +
                                "<button class='btn btn-success btn-sm ml-3'  onclick=\"FetchAddData(\'" + value.main_client_id + "\')\">Add</button></span>" +
                                "</p></td>" +
                                "</tr>";
                        }
                           
                    }
                    else {
                        html = html +
                            "<span class='row justify-content-center'>" +
                            "<button class='btn btn-success btn-sm ml-3'  onclick=\"FetchAddData(\'" + value.main_client_id + "\')\">Add</button></span>" +
                            "</p></td>" +
                            "</tr>";
                    }
                });

                $("#tbodyMainTbl").append(html);


                $('#tblMain').DataTable({
                    "initComplete": function (settings, json) {
                        $("#divMainTbl").show();
                        $('#tblMain').DataTable().columns.adjust().draw();
                    },
                    "order": [],
                    "scrollX": true,
                    "pageLength": globalDataGridPageLenght,
                    "lengthMenu": [5, 10, 25, 50],
                    "columnDefs": [
                        { "width": "30%", targets: 0, 'searchable': true, orderable: true },
                        { "width": "17%", targets: [1, 2], 'searchable': false, orderable: true },
                        { "width": "36%", targets: 3, 'searchable': true, orderable: true }
                    ]
                });
            }
            else {
                html = "<tr><td colspan = 4 class='text-center'>No Data</td></tr>"
                $("#tbodyMainTbl").append(html);
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestCliAct();
        }
    })
}

function FetchHistoryData(cli) {

    var formdata = {
        "login_id": $("#LoggedInUID").val(),
        "main_client_id": cli
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_client_portfolio_interaction_history";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequestCliAct();
        },
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                var html = "";

                $("#tbodyHistoryData").html("");
                $.each(data, function (index, value) {

                    html = html + "<tr><td><p>" +
                        "<span style='word-break:break-word;'><b>Remarks</b>: " + value.remarks + "</span><br>" +
                        "<span class='row justify-content-start' style='font-size:10px;'>" +
                        "<span class='ml-2'><b>By</b>: " + value.login_Name + "</span>" +
                        "<span class='ml-2'><b>On</b>: " + formatDate(value.review_date) + "</span>" +
                        "</span></p></td></tr>"
                });
                $("#tbodyHistoryData").append(html);
                

                $('#historyModal').modal({ backdrop: 'static', keyboard: false });
            }
            else {
                alert("No Data found.")
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestCliAct();
        }
    })

    
}

function FetchAddData(mcli_id) {

    var date = (new Date()).toISOString().split('T')[0];
    $("#inpReviewDate").val(date);
    $("#inpReviewDate").attr("max", date);

    $("#txtAdddd").val("");
    $("#mCliId").val(mcli_id);

    $('#AddModal').modal({ backdrop: 'static', keyboard: false });
}

function saveRemarksData() {
    if ($("#txtRemarks").val() != "") {
        var formdata = {
            "login_id": $("#LoggedInUID").val(),
            "main_client_id": $("#mCliId").val(),
            "review_date": $("#inpReviewDate").val(),
            "remarks": $("#txtRemarks").val()
        }

        var posturl = "/ClientPortal/Reports/psp_amd_client_portfolio_interaction_add_remarks";
        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            beforeSend: function (xhr) {
                startajaxrequestCliAct();
            },
            data: JSON.stringify(formdata),
            success: function (data) {
                if (data.length > 0) {
                    
                    if (data[0].sql_msg == "Updated Successfully!") {
                        alert("Updated")
                        GetData();
                        $('#AddModal').modal('hide')
                    }
                    else if (data[0].sql_msg == "Added Successfully!") {
                        alert("Added")
                        GetData();
                        $('#AddModal').modal('hide')

                    }
                    else {
                        alert("Error in process");
                    }

                }
            },
            error: function (xhr, err) {
                alert(err)
            },
            complete: function (xhr) {
                completeajaxrequestCliAct();
            }
        })
    }
    else {
        alert("Please enter remarks.");
    }
}


