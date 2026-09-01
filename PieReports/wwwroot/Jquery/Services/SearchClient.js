$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

});

$("#searchTerm").keyup(function () {

    if ($(this).val().length >= 4) {

        $("#btnSubmitSearch").prop('disabled', false);
        $("#txtMinCriteria").hide();
        

    }
    else {
        $("#btnSubmitSearch").prop('disabled', true);
        $("#txtMinCriteria").show();
    }
});

function SearchTerm() {

    const url = "/ClientPortal/Pie/psp_dsp_search_client_cp";

    var formdata = {
        "login_id": $("#LoggedInUID").val(),
        "search_str": $("#searchTerm").val()
    }


    $.ajax({
        url: url,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            var html = "";
            $("#tblDspClientData").html("");

            if (data.length > 0) {

                html = html + "<thead>" +
                    "<tr>" +
                        "<th>Family</th>"+
                        "<th>Client</th>"+
                        "<th>PAN</th>"+
                        "<th>Branch</th>"+
                        "<th>RM</th>"+
                    "</tr>"+
                    "</thead><tbody>"

                $.each(data, function (index, value) {

                    if (value.mask_flag == "Y") {
                        html = html + "<tr title='You have access' class='table-success'>" +
                            "<td>" + value.family_name + "</td>" +
                            "<td>" + value.client_name + "</td>" +
                            "<td>" + value.pan + "</td>" +
                            "<td>" + value.branch + "</td>" +
                            "<td>" + value.rM_name + "</td>" +
                            "</tr>"
                    }
                    else {
                        html = html + "<tr title='You do not have access' class='table-danger'>" +
                            "<td>" + value.family_name + "</td>" +
                            "<td>" + value.client_name + "</td>" +
                            "<td>" + value.pan + "</td>" +
                            "<td>" + value.branch + "</td>" +
                            "<td>" + value.rM_name + "</td>" +
                            "</tr>"
                    }
                });

                html = html + "</tbody>"

                $("#tblDspClientData").append(html);
            }
            else {
                html = html + "<tr>" +
                    "<td colspan=5>No clients found.</td>" +
                    "</tr>"
                $("#tblDspClientData").append(html);
            }

            $("#tblDspClientData").DataTable({
                "initComplete": function (settings, json) {

                    const snackBar  = document.getElementById("snackbar");

                    //$("#snackbar").addClass('show');
                    //$("#snackbar").removeClass('show');

                    snackBar.className = "show";


                    setTimeout(function () { snackBar.className = snackBar.className.replace("show", ""); },
                        3500);//Keep 0.5 secs more than fade in and out diff in css file



                    $("#tblDspClientData").show();
                    $('#tblDspClientData').DataTable().columns.adjust().draw();
                },
                "dom": "<'row justify-content-between'<'col-sm-2'l><'col-sm-3'f><'col-sm-5'p>>" +
                    "<'row'<'col-sm-12'tr>>" +
                    "<'row'<'col-sm-5'i><'col-sm-7'p>>",
                "destroy": true,
                "pageLength": globalDataGridPageLenght,
                "lengthMenu": [25, 50, 100],
                "pagingType": "full_numbers",
                "order": [],
                "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
                    //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
                    { "sWidth": "15%" },
                    { "sWidth": "15%" },
                    { "sWidth": "5%" },
                    { "sWidth": "5%" },
                    { "sWidth": "25%" }
                ]
            });
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}