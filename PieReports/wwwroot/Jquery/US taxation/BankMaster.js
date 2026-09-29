$(document).ready(function () {

    HideFinYearList();
    HideFamilyList();

    FetchClientList();
    FetchBankAccountTypeList();
    FetchNRIClientCatList();
});

function ResetInputs() {
    $("#add_modify_form form")[0].reset();
    $("#AMD_header").text("Add Record");
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

function FetchBankAccountTypeList() {

    var posturl = "/ClientPortal/Pie/psp_dsp_nri_bank_account_type_list";
    $.ajax({
        url: posturl,
        type: "get",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlBankAcType").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.rec_id + ">" + value.bank_acc_type + "</option>";
                });

                $("#ddlBankAcType").append(html);
                $("#ddlBankAcType").attr("disabled", false);

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

function FetchNRIClientCatList() {

    var posturl = "/ClientPortal/Pie/psp_dsp_nri_client_category_list";
    $.ajax({
        url: posturl,
        type: "get",
        contentType: "application/json",
        data: JSON.stringify(),
        success: function (data) {
            if (data.length > 0) {
                $("#ddlClientCat").html("");
                var html = "";
                $.each(data, function (index, value) {
                    html = html + "<option value=" + value.rec_id + ">" + value.category_name + "</option>";
                });

                $("#ddlClientCat").append(html);
                $("#ddlClientCat").attr("disabled", false);

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

function GetExistingData() {
    var formdata = {
        "main_client_id": $('#ddlUSClientList :selected').val()
    }

    var posturl = "/ClientPortal/Pie/psp_dsp_nri_bankmaster_details";
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
                        "<td>" + value.bank_account_no + "</td>" +
                        "<td>" + value.bank_name + "</td>" +
                        "<td>" + value.bank_account_type + "<input type='hidden' name='rec_id' value='" + value.rec_id + "'/></td>" +
                        "<td>" + value.category_name + "</td>" +
                        "<td class='text-center' style='width: 110px;'>" +
                        "<button class='btn Edit_rec' type='submit' title='Edit'><i class='fas fa-pencil-alt fa-lg White_Red_Icon'></i></button>" +
                        "<button class='btn Delete_rec' type='submit' title='Delete'><i class='far fa-trash-alt fa-lg White_Red_Icon'></i></button>" +
                        "</td>" +
                        "</tr>"
                });

                $("#tbodydsp_table").append(html);

                $('#display_table').DataTable({
                    "columnDefs": [
                        { "width": "25%", "targets": 0 },
                        { "width": "23%", "targets": 1 },
                        { "width": "27%", "targets": 2 },
                        { "width": "12%", "targets": 3 },
                        { "width": "13%", "targets": 4, orderable: false }
                    ],
                    "order": [] //Disable Initial sort
                });

                $("#AMD_header").text("Add Record");
                $("#add_modify_form form")[0].reset();
                $("#div_dsptable").show();

                $("#add_modify_form").show();
                $('#display_table').DataTable().columns.adjust().draw();

            }
            else {
                alert("No Data found for selected year. Please add data or change paramenters.");
            }
        },
        error: function (xhr, err) {
            alert(err)
        }
    })
}

function addModifyRecord() {

    var txt_BankAccNum = $('#input_BankAccNum').val();
    var txt_BankName = $('#input_BankName').val();
    var amount = $('#inp_amount').val();

    if (txt_BankAccNum != '' && txt_BankName != '') {

        var loginid = $('#LoggedInUID').val();
        var main_client_id = $('#ddlUSClientList :selected').val();
        var bank_account = $('#ddlBankAccountsList :selected').val();
        var client_cat = $('#txt_Cat_id').val();

        var segment = $('#ddlSegList :selected').val();
        var trans_type = $('#ddlTypeList :selected').val();

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

        //$.ajax({
        //    url: posturl,
        //    type: "post",
        //    contentType: "application/json",
        //    data: JSON.stringify(formdata),
        //    success: function (data) {

        //        GetExistingTaxData();

        //    },
        //    error: function (xhr, err) {
        //        alert(err)
        //    }
        //})

    }
    else {
        alert("Please fill all the fields");
    }
}


$(document).on("click", ".Edit_rec", function () {
    var abc = new Array();
    $(this).parents("tr").find("td:not(:last-child)").each(function () {
        abc.push($(this).text());
    });
    var in_rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()


    $("#inp_rec_id").val(in_rec_id);

    $("#input_BankAccNum").val(invertDate(abc[0]));
    $("#input_BankName").val(abc[1]);

    $("#AMD_header").text("Update Record");

});

$(document).on("click", ".Delete_rec", function () {
    // WCAG 3.3.4 Error Prevention — deleting data needs a confirmation step,
    // with a button that names the consequence. The original body runs
    // unchanged, with `this` preserved, once the user confirms.
    var a11yTrigger = this;
    var a11yAsk = (window.a11y && window.a11y.confirm)
        ? window.a11y.confirm({ title: 'Delete this bank account?', message: 'The bank account record will be deleted. This cannot be undone.', confirmLabel: 'Delete bank account' })
        : Promise.resolve(window.confirm('Delete this bank account?'));
    a11yAsk.then(function (confirmed) {
        if (!confirmed) { return; }
        (function () {

            var delete_rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()

            var user_input = true; // confirmed in the accessible dialog above

            if (user_input) {

                //var main_client_id = $('#ddlUSClientList :selected').val();
                //var bank_account = $('#ddlBankAccountsList :selected').val();

                var formdata = {
                    "loginid": "0",
                }

                var posturl = "/ClientPortal/Pie/psp_amd_nri_client_data_entry";

                //$.ajax({
                //    url: posturl,
                //    type: "post",
                //    contentType: "application/json",
                //    data: JSON.stringify(formdata),
                //    success: function (data) {

                //        GetExistingTaxData();

                //    },
                //    error: function (xhr, err) {
                //        alert(err)
                //    }
                //})
            }

        }).call(a11yTrigger);
    });
});