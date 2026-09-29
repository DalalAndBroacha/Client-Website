$(document).ready(function () {

    HideFinYearList();
    HideFamilyList();

    FetchClientList();
    FetchCalendarYearList();
    
    //$('#table_inp').dataTable({
    //    "columnDefs": [
    //        { "width": "1%", "targets": 0 },
    //        { "width": "15%", "targets": 1 },
    //        { "width": "18%", "targets": 2 },
    //        { "width": "50%", "targets": 3 },
    //        { "width": "15%", "targets": 4 },
    //        { "width": "1%", "targets": 5 }
    //    ],
    //    "searching": false,
    //    "ordering": false,
    //    "paging": false,
    //    "lengthChange": false,
    //    "info": false
    //});
});




$("#ddlUSClientList").change(function () {
    $("#div_dsptable").hide();
    $("#add_modify_form").hide();
    FetchNRIClientBankAccountList();
});

$("#ddlBankAccountsList").change(function () {
    $("#div_dsptable").hide();
    $("#add_modify_form").hide();
    FetchNRIClientBankAccountDetails();
});

$("#ddlSegList").change(function () {
    showHideTypes();
    autoPopulateNarration();
});

$("#ddlTypeList").change(function () {

    autoPopulateNarration();
    
});

function autoPopulateNarration() {
    var rec_id = $("#inp_rec_id").val();

    if (rec_id == 0) {

        var type_id = $('#ddlTypeList :selected').val();

        if (type_id == 1) {
            $("#inp_narration").val("STCG - ");
        }
        else if (type_id == 2) {
            $("#inp_narration").val("LTCG - ");
        }
        else if (type_id == 3) {
            $("#inp_narration").val("Dividend Tax - ");
        }
        else if (type_id == 4) {
            $("#inp_narration").val("Interest Tax - ");
        }
        else if (type_id == 5) {
            $("#inp_narration").val("Bank Interest for the period - ");
        }
        else if (type_id == 6) {
            $("#inp_narration").val("Highest Balance");
        }
    }
}

function showHideTypes() {
    var selected_val = $('#ddlSegList :selected').val()

    if (selected_val == "1" || selected_val == "2" || selected_val == "3") {

        $("#ddlTypeList option[value=1]").show();
        $("#ddlTypeList").val(1);
        $("#ddlTypeList option[value=2]").show();
        $("#ddlTypeList option[value=3]").show();

        $("#ddlTypeList option[value=4]").hide();
        $("#ddlTypeList option[value=5]").hide();
        $("#ddlTypeList option[value=6]").hide();
    }
    else {

        $("#ddlTypeList option[value=4]").show();
        $('#ddlTypeList').val(4);
        $("#ddlTypeList option[value=5]").show();
        $("#ddlTypeList option[value=6]").show();

        $("#ddlTypeList option[value=1]").hide();
        $("#ddlTypeList option[value=2]").hide();
        $("#ddlTypeList option[value=3]").hide();
    }
}


function FetchCalendarYearList() {

    var posturl = "/ClientPortal/Pie/psp_dsp_calendar_year";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlCalYearList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.calendar_value + ">" + value.calendar_dsp + "</option>";
                });

                $("#ddlCalYearList").append(html);
                $("#ddlCalYearList").attr("disabled", false);

                $('#inp_date').attr("min", data[0].min_cal_date);
                $('#inp_date').attr("max", data[0].max_cal_date);
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

function FetchClientList() {

    var formdata = {
        "country": "US"
    }

    var posturl = "/ClientPortal/Pie/GetNRIClientList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlUSClientList").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.client_id + ">" + value.name + "</option>";
                });

                $("#ddlUSClientList").append(html);
                $("#ddlUSClientList").attr("disabled", false);

                //$("#ddlUSClientList").select2();

                FetchNRIClientBankAccountList();
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

function FetchNRIClientBankAccountList() {

    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val(),
        "bank_account_no": " ",
        "data_level": "1"
    }

    var posturl = "/ClientPortal/Pie/GetNRIClientBankAccountList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlBankAccountsList").html("");

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.bank_account_no + ">" + value.bank_account_no + " (" + value.bank_account_type+ ")" + "</option>";
                });

                $("#ddlBankAccountsList").append(html);
                $("#ddlBankAccountsList").attr("disabled", false);

                $("#txt_Cat").val(data[0].client_category);
                $("#txt_Cat_id").val(data[0].client_cat_id);
                $("#txt_BankName").val(data[0].bank_name);

            }
            else {
                $("#ddlBankAccountsList").attr("disabled", true);
                alert("No Bank Accounts")
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function FetchNRIClientBankAccountDetails() {
    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val(),
        "bank_account_no": $('#ddlBankAccountsList :selected').val(),
        "data_level": "2"
    }
    var posturl = "/ClientPortal/Pie/GetNRIClientBankAccountList";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {
                $("#txt_Cat").val(data[0].client_category);
                $("#txt_Cat_id").val(data[0].client_cat_id);
                $("#txt_BankName").val(data[0].bank_name);
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function GetExistingTaxData() {
    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val(),
        "bank_account": $('#ddlBankAccountsList :selected').val(),
        "cal_year": $('#ddlCalYearList :selected').val()
    }

    var posturl = "/ClientPortal/Reports/psp_dsp_nri_client_data_entry_taxwithheld_details";
    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data.length > 0) {

                $('#display_table').DataTable().destroy();
                $("#tbodydsp_table").html("");

                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<tr>" +
                        "<td data-sort='" + formatDate_yyyymmdd(value.trans_date) + "'><p class='paraTextAlignLeft'>" + formatDateIndianStandard(value.trans_date) + "</p></td>" +
                        "<td>" + value.category_name + "<input type='hidden' name='cat_id' value='" + value.category_order + "'/></td>" +
                        "<td>" + value.subcategory_name + "<input type='hidden' name='subcat_id' value='" + value.subcategory_id + "'/></td>" +
                        "<td>" + value.narration + "<input type='hidden' name='rec_id' value='" + value.rec_id + "'/></td>" +
                        "<td><p class='paraTextAlignRight'>" + numberWithCommas(value.amount.toFixed(2)) + "</p></td>" +
                        "<td class='text-center' style='width: 110px;'>" +
                                "<button class='btn Edit_rec' type='submit' title='Edit'><i class='fas fa-pencil-alt fa-lg White_Red_Icon'></i></button>" +
                                "<button class='btn Delete_rec' type='submit' title='Delete'><i class='far fa-trash-alt fa-lg White_Red_Icon'></i></button>" +
                        "</td>" +
                        "</tr>"
                });

                $("#tbodydsp_table").append(html);

                $('#display_table').DataTable({
                    "columnDefs": [
                        { "width": "2%", "targets": 0 },
                        { "width": "2%", "targets": 1 },
                        { "width": "10%", "targets": 2 },
                        { "width": "66%", "targets": 3, orderable: false},
                        { "width": "5%", "targets": 4 },
                        { "width": "10%", "targets": 5, orderable: false }
                    ],
                    "order": [] //Disable Initial sort
                });

                $("#AMD_header").text("Add Record");
                $("#add_modify_form form")[0].reset();
                $('#ddlSegList').prop('disabled', false);
                $('#ddlTypeList').prop('disabled', false);
                showHideTypes();

                $("#div_dsptable").show();
                autoPopulateNarration();
                $("#add_modify_form").show();
                $('#display_table').DataTable().columns.adjust().draw();

                   
            }
            else {
                $('#display_table').DataTable().destroy();
                alert("No Data found for selected year. Please add data or change paramenters.");
                $("#AMD_header").text("Add Record");
                $("#add_modify_form form")[0].reset();
                $('#ddlSegList').prop('disabled', false);
                $('#ddlTypeList').prop('disabled', false);
                showHideTypes();

                $("#div_dsptable").hide();
                autoPopulateNarration();
                $("#add_modify_form").show();
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function addModifyRecord() {

    var txt_narration = $('#inp_narration').val();
    var trans_date = $('#inp_date').val();
    var amount = $('#inp_amount').val();

    if (trans_date != '' && txt_narration != '' && amount != '') {

        var loginid = $('#LoggedInUID').val();
        var main_client_id = $('#ddlUSClientList :selected').val();
        var bank_account = $('#ddlBankAccountsList :selected').val();
        var client_cat = $('#txt_Cat_id').val();

        var segment = $('#ddlSegList :selected').val();
        var trans_type = $('#ddlTypeList :selected').val();

        if (trans_type == 4) { //Bank Tax is 4 1 in taxWithHeld Table
            trans_type = "1";
        }


        var amount_str = amount.toString();

        var rec_id = $('#inp_rec_id').val();
        var amd_flag;

        if (rec_id == 0) {
            amd_flag = "1"
        }
        else {
            amd_flag = "2"
        }

        var formdata = {
            "loginid": loginid,
            "rec_id": rec_id,
            "main_client_id": main_client_id,
            "bank_account": bank_account,
            "client_cat": client_cat,
            "trans_date": trans_date,
            "segment": segment,
            "trans_type": trans_type,
            "narration": txt_narration,
            "amount": amount_str,
            "flag": amd_flag
        }

        var posturl = "/ClientPortal/Pie/psp_amd_nri_client_data_entry";

        $("#add_modify_form form")[0].reset();
        $('#ddlSegList').prop('disabled', false);
        $('#ddlTypeList').prop('disabled', false);

        $.ajax({
            url: posturl,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formdata),
            success: function (data) {
                
                GetExistingTaxData();
                
            },
            error: function (xhr, err) {
                alert(err)
            }
        })

    }
    else {
        alert("Please fill all the fields");
    }
}

function ResetInputs() {
    $("#add_modify_form form")[0].reset();
    $("#AMD_header").text("Add Record");
    $('#ddlSegList').prop('disabled', false);
    $('#ddlTypeList').prop('disabled', false);
    showHideTypes();
    autoPopulateNarration();
}

$(document).on("click", ".Edit_rec", function () {
    var abc = new Array();
    $(this).parents("tr").find("td:not(:last-child)").each(function () {
        abc.push($(this).text());
    });
    var cat_id = $(this).parents('tr').find('input:hidden[name=cat_id]').val()
    var subcat_id = $(this).parents('tr').find('input:hidden[name=subcat_id]').val()
    var in_rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()

    $("#inp_rec_id").val(in_rec_id);
    $("#ddlSegList").val(cat_id);
    showHideTypes();
    $('#ddlSegList').prop('disabled', 'disabled');
    if (cat_id == 4) {
        $('#ddlTypeList').prop('disabled', 'disabled');
    }
    else {
        $('#ddlTypeList').prop('disabled', false);
    }
    $("#ddlTypeList").val(subcat_id);
    
    
    $("#inp_date").val(invertDate(abc[0]));
    $("#inp_narration").val(abc[3]);
    
    $("#inp_amount").val(abc[4].replaceAll(',', ''));
    $("#AMD_header").text("Update Record");
    
});

$(document).on("click", ".Delete_rec", function () {
    // WCAG 3.3.4 Error Prevention — deleting data needs a confirmation step,
    // with a button that names the consequence. The original body runs
    // unchanged, with `this` preserved, once the user confirms.
    var a11yTrigger = this;
    var a11yAsk = (window.a11y && window.a11y.confirm)
        ? window.a11y.confirm({ title: 'Delete this entry?', message: 'The US taxation entry will be deleted. This cannot be undone.', confirmLabel: 'Delete entry' })
        : Promise.resolve(window.confirm('Delete this entry?'));
    a11yAsk.then(function (confirmed) {
        if (!confirmed) { return; }
        (function () {

            var delete_rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()
            var delete_subcat_id = $(this).parents('tr').find('input:hidden[name=subcat_id]').val()

            if (delete_subcat_id == 4) { //Bank Tax is 4 1 in taxWithHeld Table
                delete_subcat_id = "1";
            }

            var user_input = true; // confirmed in the accessible dialog above

            if (user_input) {

                var main_client_id = $('#ddlUSClientList :selected').val();
                var bank_account = $('#ddlBankAccountsList :selected').val();

                var formdata = {
                    "loginid": "0",
                    "rec_id": delete_rec_id,
                    "main_client_id": main_client_id,
                    "bank_account": bank_account,
                    "client_cat": "0",
                    "trans_date": "01/01/2020",
                    "segment": "0",
                    "trans_type": delete_subcat_id,
                    "narration": " ",
                    "amount": "0",
                    "flag": "3"
                }

                var posturl = "/ClientPortal/Pie/psp_amd_nri_client_data_entry";

                $.ajax({
                    url: posturl,
                    type: "post",
                    contentType: "application/json",
                    data: JSON.stringify(formdata),
                    success: function (data) {

                        GetExistingTaxData();

                    },
                    error: function (xhr, err) {
                        alert(err)
                    }
                })
            }

        }).call(a11yTrigger);
    });
});


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57)))
        return false;
    return true;
}