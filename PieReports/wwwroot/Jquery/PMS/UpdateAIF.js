$(document).ready(function () {

    HideFinYearList();
    HideFamilyList();
    GetExistingSchemeData();
    GetExistingNavData();
});


function GetExistingSchemeData() {
    var date = (new Date()).toISOString().split('T')[0];
    $("#nav_date").val(date);
    $("#nav_date").attr("max", date);

    var posturl = "/ClientPortal/Pie/AIFSchemeList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {

                $('#display_table').DataTable().destroy();
                $("#tbodydsp_table").html("");

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td style='width: 200px;'>" + value.scheme_name + "<input type='hidden' name='scheme_id' value='" + value.id + "'/></td>" +
                        "<td class='text-center' style='width: 110px;'>" +
                        "<input type='number' class='form-control nav_amount' name='txt_Nav'>" +
                        "</td>" +
                        "</tr>"
                });

                $("#tbodydsp_table").append(html);

                $('#display_table').DataTable({
                    "columnDefs": [
                        { "width": "66%", "targets": 0},
                        { "width": "10%", "targets": 1, orderable: false }
                    ],
                    "searching": false,
                    "ordering": false,
                    "paging": false,
                    //"lengthChange": false,
                    "info": false,
                    "order": [] //Disable Initial sort
                });
                $("#div_dsptable").show();
            }
            else {
                $("#div_dsptable").hide();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function ADDAIFData() {
    var date = $('#nav_date').val();
    var rec_id = $('#inp_rec_id_nav').val();
    var amd_flag;

    if (rec_id == 0) {
        amd_flag = "I"

        var nav_amount = $('.nav_amount').val;
        var len = $('#display_table > tbody  > tr').length;
        if (date != '' && scheme_id != '' && amount != '') {

            $('#display_table > tbody  > tr').each(function (index, tr) {

                var scheme_id = $(this).find('input:hidden[name=scheme_id]').val()
                var amount = $(this).find("input[name^='txt_Nav']").val()

                var amount_str = amount.toString();

                var loginid = $('#LoggedInUID').val();
                var formdata = {
                    "nav_date": date,
                    "scheme_id": scheme_id,
                    "para_nav": amount_str,
                    "user": loginid,
                    "action": amd_flag,
                    "rec_id": rec_id
                }
                var posturl = "/ClientPortal/Pie/psp_amd_AIF_data_entry";

                $.ajax({
                    url: posturl,
                    type: "post",
                    contentType: "application/json",
                    data: JSON.stringify(formdata),
                    success: function (data) {
                        console.log(index);
                        if (index == len - 1) {
                            GetExistingNavData();
                            console.log('complete');
                        }
                    },
                    error: function (xhr, err) {
                        alert(err)
                    }
                })
            });
        }
        else {
            alert("Please fill all the fields");
        }
    }
    else {
        amd_flag = "U"
        var scheme_id = $('#ddlAIFSchemeList :selected').val();

        var amount = $('#inp_nav').val()

        var amount_str = amount.toString();
        var rec_id = rec_id.toString();

        var loginid = $('#LoggedInUID').val();
        var formdata = {
            "rec_id": rec_id,
            "nav_date": date,
            "scheme_id": scheme_id,
            "para_nav": amount_str,
            "user": loginid,
            "action": amd_flag,
        }
        console.log(formdata);
        var posturl = "/ClientPortal/Pie/psp_amd_AIF_data_entry";

        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {

                GetExistingNavData();

            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    $("#div_dsptable form")[0].reset();

}

function GetExistingNavData() {
  
    var posturl = "/ClientPortal/Pie/AIFNavData";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $('#display_nav_table').DataTable().destroy();
                $("#tbodynavdsp_table").html("");

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td data-sort='" + formatDate_yyyymmdd(value.nav_date) + "'><p class='paraTextAlignLeft'>" + formatDateIndianStandard(value.nav_date) + "</p></td>" +
                        "<td>" + value.scheme_name + "<input type= 'hidden' name= 'scheme_id' value= '" + value.scheme_code + "'/></td>" +
                        "<td>" + Number(value.nav).toFixed(2) + "<input type='hidden' name='id' value='" + value.id + "'></td>" +
                        "<td class='text-center' style='width: 110px;'>" +
                        "<button class='btn Edit_rec_nav' type='submit' title='Edit'><i class='fas fa-pencil-alt fa-lg White_Red_Icon'></i></button>" +
                        "</td>" +
                        "</tr>"
                });
                $("#tbodynavdsp_table").append(html);

                $('#display_nav_table').DataTable({
                    "columnDefs": [
                        { "width": "4%", "targets": 0 },
                        { "width": "40%", "targets": 1 },
                        { "width": "10%", "targets": 2 },
                        { "width": "10%", "targets": 3, orderable: false }
                    ],
                    "info": false,
                    "order": [] 
                });
                

                //$('#display_nav_table').DataTable().columns.adjust().draw();
            }
            else {
                $('#display_nav_table').DataTable().destroy();
                alert("No Data found");
                $("#div_dsptable form")[0].reset();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

$(document).on("click", ".Edit_rec_nav", function () {

    $("#btn_Add_New").show();
    $("#div_add_data").hide();
    $("#modify_nav_data").show();

    var abc = new Array();
    $(this).parents("tr").find("td:not(:last-child)").each(function () {
        abc.push($(this).text());

    });
    var in_rec_id = $(this).parents('tr').find('input:hidden[name=id]').val()
    var scheme_id = $(this).parents('tr').find('input:hidden[name=scheme_id]').val()

    $("#inp_rec_id_nav").val(in_rec_id);
    $("#nav_date").val(invertDate(abc[0]));
    
    Schemelist(scheme_id);
    $("#inp_nav").val(abc[2]);
});

function Resetdata() {
    $("#div_dsptable form")[0].reset();
}

function Schemelist(scheme_id) {
    var posturl = "/ClientPortal/Pie/AIFSchemeList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlAIFSchemeList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.id + ">" + value.scheme_name + "</option>";
                });

                $("#ddlAIFSchemeList").append(html);
                $("#ddlAIFSchemeList").attr("disabled", false);
                $("#ddlAIFSchemeList").val(scheme_id);


            }
            else {
                alert("error")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function showform()
{

    $("#btn_Add_New").hide();
    $("#div_add_data").show();
    $("#modify_nav_data").hide();

    var date = (new Date()).toISOString().split('T')[0];
    $("#nav_date").val(date);
    $("#nav_date").attr("max", date);
}