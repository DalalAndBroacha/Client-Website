$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    fetchData();

    //$('#testSelect').multiselect({
    //    buttonWidth: '200px',
    //    enableResetButton: true,
    //    enableFiltering: true,
    //    enableCaseInsensitiveFiltering: true,
    //    includeSelectAllOption: true
    //});
});

$("#ddlDBList").change(function () {
    fetchData();
});


function fetchData() {

    $("#Proc_Dsp_table").hide();

    var posturl = "/ClientPortal/Pie/psp_dsp_process_log";

    var formdata = {
        "log": $('#ddlDBList :selected').val()
    }

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {

                $("#tbodyProc_Dsp_table").html("");
                var html = "";               

                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td name='req_id'>" + value.strPortfolio_date + "</td>" +
                        "<td class='txtDes'>" + value.portfolio + "</td>" +
                        "<td class='btnCopyDes' style='text-align:center;'><a class='btn-sm btn-info btnCopyDes'>Copy</a></td>" +
                        "</tr>"
                });


                $("#tbodyProc_Dsp_table").append(html);
                $("#Proc_Dsp_table").show();
            }
            else {

                $("#Proc_Dsp_table").hide();
                console.log("Error");

            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })

}

$(document).on("click", ".btnCopyDes", function () {

    let txtDesc = $(this).closest('tr').find('.txtDes').text();

    navigator.clipboard.writeText(txtDesc);

});