const Tdaydate = (new Date()).toISOString().split('T')[0];
$(document).ready(function () {

    $("[data-widget='pushmenu']").PushMenu("collapse");
    HideFinYearList();
    HideFamilyList(); 

    $("#inpStartDate").attr("max", Tdaydate);
    $("#inpEndDate").attr("max", Tdaydate);

    var maxDate = formatDateold(addDays(Tdaydate, -11));

    $("#inpEndDate").val(Tdaydate);
    
    $("#inpStartDate").val(maxDate);

    FetchData();

});

function startajaxrequestIndxMst() {
    $("#CurrwaitIn").css("display", "block");
}
function completeajaxrequestIndxMst() {
    $("#CurrwaitIn").css("display", "none");
}

function FetchData() {
    $("#DataDiv").hide();
    let Inptype = $("#ddlMasterList").val();

    var posturl = "/ClientPortal/Reports/psp_dsp_mst_index_cp";

    if (Inptype == "1") { //1: Index
        var formdata = {
            "from_date": $("#inpStartDate").val(),
            "to_date": $("#inpEndDate").val(),
            "flag": "1"
        }   
    }
    else if (Inptype == "2") { //2: Currency

        var formdata = {
            "from_date": $("#inpStartDate").val(),
            "to_date": $("#inpEndDate").val(),
            "flag": "2"
        }
    }

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequestIndxMst();
        },
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                
                $("#tblDataTbody").html("");
                var html = "";

                $.each(data, function (index, value) {

                    html = html + "<tr>" +
                        
                        "<td class='paraTextAlignCenter'>" + formatDateIndianStandard(value.date) + "<input type='hidden' name='rec_id' value='" + value.rec_id + "'/></td>" +
                        "<td class='paraTextAlignCenter'>" + value.name + "<input type='hidden' name='master_id' value='" + value.master_id + "'/></td>" +
                        "<td class='paraTextAlignCenter'>" + numberWithCommas(value.rate.toFixed(2)) +  "</td>" +
                        "<td class='text-center' style='width: 175px;'>" +
                            "<button class='btn Edit_rec' type='submit' title='Edit'><i class='fas fa-pencil-alt fa-lg White_Red_Icon'></i></button>" +
                            "<button class='btn Delete_rec' type='submit' title='Delete'><i class='far fa-trash-alt fa-lg White_Red_Icon'></i></button>" +
                        "</td></tr>" ;

                });
                $("#tblDataTbody").append(html);
                $("#DataDiv").show();
                
            }
            else {
                html = "<tr><td colspan = 4 class='text-center'>No Data</td></tr>"
                $("#tblDataTbody").append(html);
                $("#DataDiv").show();
            }
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestIndxMst();
        }
    })

}

function FetchListData() {
    
    let Inptype = $("#ddlAddMstIndexDataList").val();

    var formdata = {
        "flag": Inptype
    }
    
    var posturl = "/ClientPortal/Reports/psp_dsp_master_list_cp";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlAddSelectedIndexDataList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.id + ">" + value.dsp_name + "</option>";
                });

                $("#ddlAddSelectedIndexDataList").append(html);
                $("#ddlAddSelectedIndexDataList").attr("disabled", false);
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

function FetchEditListData(master_id) {

    var formdata = {
        "flag": $("#ddlMasterList").val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_master_list_cp";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#EditddlIndexDataList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.id + ">" + value.dsp_name + "</option>";
                });

                $("#EditddlIndexDataList").append(html);
                $("#EditddlIndexDataList").attr("disabled", false);


                $("#EditddlIndexDataList").val(master_id);
                

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

function UpdateData() {
    $("#DataDiv").hide();
    
    var posturl = "/ClientPortal/Reports/psp_amd_index_currency_master_cp";

    var formdata = {
        "login_id": $('#LoggedInUID').val(),
        "rec_id": $('#EditRec').val(),
        "mod_flag": "U", //A: Add | D: Delete | U: Update
        "table_id": $("#ddlMasterList").val(), // Index|Curr
        "rate": String($("#EdittxtRate").val()),
        "tr_date": $('#EditInpTrDate').val(),
        "master_id": $("#EditddlIndexDataList").val() // USD|Nifty|Etc
    }

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequestIndxMst();
        },
        data: JSON.stringify(formdata),
        success: function (data) {

            FetchData();

            alert(data[0].sql_msg)
            $('#EditModal').modal('hide')
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestIndxMst();
        }
    })

}

function AddData() {
    $("#DataDiv").hide();
    
    var formdata = {
        "login_id": $('#LoggedInUID').val(),
        "rec_id": "0",
        "mod_flag": "A", //A: Add | D: Delete | U: Update
        "table_id": $("#ddlAddMstIndexDataList").val(), // Index|Curr
        "rate": String($("#AddtxtRate").val()),
        "tr_date": $("#inpTrDate").val(),
        "master_id": $("#ddlAddSelectedIndexDataList").val() // USD|Nifty|Etc
    }

    var posturl = "/ClientPortal/Reports/psp_amd_index_currency_master_cp";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequestIndxMst();
        },
        data: JSON.stringify(formdata),
        success: function (data) {
            
            alert(data[0].sql_msg)
            
            $('#AddModal').modal('hide')
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestIndxMst();
        }
    })

}

function DeleteData(rec_id, master_id) {

    var posturl = "/ClientPortal/Reports/psp_amd_index_currency_master_cp";

    var formdata = {
        "login_id": $('#LoggedInUID').val(),
        "rec_id": rec_id,
        "mod_flag": "D", //A: Add | D: Delete | U: Update
        "table_id": $("#ddlMasterList").val(), // Index|Curr
        "rate": "0",
        "tr_date": "1900-01-01",
        "master_id": master_id // USD|Nifty|Etc
    }

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        beforeSend: function (xhr) {
            startajaxrequestIndxMst();
        },
        data: JSON.stringify(formdata),
        success: function (data) {

            FetchData();

            alert(data[0].sql_msg)
        },
        error: function (xhr, err) {
            alert(err)
        },
        complete: function (xhr) {
            completeajaxrequestIndxMst();
        }
    })

}

$("#inpStartDate").change(function () {

    var date = $(this).val();
    var maxDate = formatDateold(addDays(date, 31));

    //$("#inpEndDate").prop("disabled", false);

    if (maxDate > Tdaydate) {
        $("#inpEndDate").val(Tdaydate);
        $("#inpEndDate").attr("min", date);
        $("#inpEndDate").attr("max", Tdaydate);
    }
    else {
        $("#inpEndDate").val(maxDate);
        $("#inpEndDate").attr("min", date);
        $("#inpEndDate").attr("max", maxDate);
    }

});

$("#ddlAddMstIndexDataList").change(function () {

    FetchListData();

});

$("#ddlMasterList").change(function () {

    $("#DataDiv").hide();

});


$("#btnAddData").click(function () {

    $("#inpTrDate").val(Tdaydate);
    FetchListData();

    $('#AddModal').modal({ backdrop: 'static', keyboard: false });

});

$("#btnViewData").click(function () {

    FetchData();

});


$(document).on("click", ".Delete_rec", function () {

    let delete_rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()
    let delete_entity_id = $(this).parents('tr').find('input:hidden[name=master_id]').val()

    DeleteData(delete_rec_id, delete_entity_id);
   
});

$(document).on("click", ".Edit_rec", function () {

    let Inptype = $("#ddlMasterList").val();


    let abc = new Array();

    $(this).parents("tr").find("td:not(:last-child)").each(function () {
        abc.push($(this).text());
    });
    const rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()
    const master_id = $(this).parents('tr').find('input:hidden[name=master_id]').val()

    $("#EditRec").val(rec_id);
    $("#EditInpTrDate").val(invertDate(abc[0]));

    let inpRate = abc[2]
    inpRate = inpRate.replace(',', '');

    
    $("#EdittxtRate").val(parseFloat(inpRate));

    FetchEditListData(master_id)

    $('#EditModal').modal({ backdrop: 'static', keyboard: false });
   

});


//psp_dsp_master_list_cp