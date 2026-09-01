$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();
    GetExistingTopPicksData();
});

function GetExistingTopPicksData() {
    $('#display_table').DataTable({
        "columnDefs": [
            { "width": "10%", "targets": 0 },
            { "width": "10%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "10%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "10%", "targets": 5, orderable: false },
            { "width": "10%", "targets": 6, orderable: false },
            { "width": "10%", "targets": 7, orderable: false }
        ],
        "order": []
    });
}

$(document).on("click", ".update_rec", function () {
    $('#current_price').show();
    $('.current_price').show();
    $('#stock_action').show();
    var abc = new Array();
    $(this).parents("tr").find("td:not(:last-child)").each(function () {
        abc.push($(this).text());

    });

    var in_rec_id = $(this).parents('tr').find('input:hidden[name=rec_id]').val()
    var wk_low = $(this).parents('tr').find('input:hidden[name=wk_low]').val()
    var wk_high = $(this).parents('tr').find('input:hidden[name=wk_high]').val()
    var eps1 = $(this).parents('tr').find('input:hidden[name=eps1]').val()
    var eps2 = $(this).parents('tr').find('input:hidden[name=eps2]').val()
    var range_start = $(this).parents('tr').find('input:hidden[name=range_start]').val()
    var range_end = $(this).parents('tr').find('input:hidden[name=range_end]').val()
    var reco_price = $(this).parents('tr').find('input:hidden[name=reco_price]').val()
    var stock_action = $(this).parents('tr').find('input:hidden[name=stock_action]').val()
    var risk_profile = $(this).parents('tr').find('input:hidden[name=risk_profile]').val()
    var portfolio_type = $(this).parents('tr').find('input:hidden[name=portfolio_type]').val()
    var inv_rationale = $(this).parents('tr').find('input:hidden[name=inv_rationale]').val()
    var active = $(this).parents('tr').find('input:hidden[name=active]').val()
    var actionable = $(this).parents('tr').find('input:hidden[name=actionable]').val()

    var date = abc[0];
    $("#reco_date").prop("readonly", true);

    date = invertDate(formatDateIndianStandard(invertDate(date)))

    var isin = abc[1]
    var script_name = abc[2]
    var current_price = abc[3]
    var price_target = abc[4]

    $("#inp_rec_id").val(in_rec_id);
    $("#reco_date").val(date.trim());
    $("#isin").val(isin.trim());
    $("#Script_Name").val(script_name.trim());
    $("#portfolio_type").val(portfolio_type);
    $("#wk_high").val(wk_high.trim());
    $("#wk_low").val(wk_low.trim());
    $("#eps1").val(eps1);
    $("#eps2").val(eps2);
    $("#range_start").val(range_start.trim());
    $("#range_end").val(range_end.trim());
    $("#reco_price").val(reco_price);
    $("#current_price").val(current_price.trim());
    $("#price_target").val(price_target.trim());
    $("#stock_action").val(stock_action.trim());
    $("#inv_rationable").val(inv_rationale);

    switch (risk_profile) {
        case (risk_profile = "High"):
            risk_profile = 1;
            break;
        case (risk_profile = "Low"):
            risk_profile = 2;
            break;
        case (risk_profile = "Medium"):
            risk_profile = 3;
            break;
        case (risk_profile = "Moderate"):
            risk_profile = 4;
            break;
        default:
            risk_profile = 0;
    }

    switch (portfolio_type) {
        case portfolio_type = "Core Portfolio":
            portfolio_type = 1;
            break;
        case portfolio_type = "Satellite Portfolio":
            portfolio_type = 2;
            break;
        default:
            portfolio_type = 0;
    }

    $("#risk_profile").val(risk_profile);
    $("#portfolio_type").val(portfolio_type);

    if (active == 'Y') {
        $("#chk_active").bootstrapToggle('on');
    }
    else {
        $("#chk_active").bootstrapToggle('off');
    }
    if (actionable == 'Y') {
        $("#chk_actionable").bootstrapToggle('on');
    }
    else {
        $("#chk_actionable").bootstrapToggle('off');
    }
    $('#dsp_form').modal('show')
});

$("#price_target").change(function () {
    var price_target = $('#price_target').val();

    var range_start = Number(price_target / 1.2).toFixed(2)
    var range_end = Number(price_target / 1.1).toFixed(2)
    $('#range_start').val(range_start);
    $('#range_end').val(range_end);
});


//function closebtn() {
//    var modal = $('#dsp_form');
//    modal.hide();
//    $('#display_table').show();
//}

$("#isin").change(function () {

    var isin = $('#isin').val();
    var formdata = {
        "Isin_Code": isin,
    }
    var posturl = "/ClientPortal/Reports/psp_dsp_script_isin_cp";

    $.ajax({
        url: posturl,
        type: "post",
        contentType: "application/json",
        data: JSON.stringify(formdata),
        success: function (data) {
            if (data == null) {
                $("#Script_Name").val("Security name not found");
            }
            else {
                $("#Script_Name").val(data.script_Name);
            }

        },
        error: function (xhr, err) {
            alert(err)
        }

    })

});

$(document).on("click", "#btn_Add_New", function () {
    $("#dsp_form form")[0].reset();

    $("#lblhighlow").css("margin-top", "10px")
    $("#lblpricetarget").css("margin-top", "10px")
    $("#lblrange").css("margin-top", "10px")
    $("#lbleps1").css("margin-top", "10px")
    $("#lbleps2").css("margin-top", "10px")

    $('#dsp_form').modal('show')
    $("#chk_actionable").bootstrapToggle('off');
    $("#chk_active").bootstrapToggle('on');
    $("#reco_date").prop("readonly", false);
    $('#current_price').hide();
    $('.current_price').hide();
    $('#stock_action').hide();

});

function ADDUpdate_topPicks() {
    var date = $('#reco_date').val();
    var loginid = $('#LoggedInUID').val();
    var rec_id = $('#inp_rec_id').val();
    var isin = $('#isin').val();
    var wk_low = $('#wk_low').val();
    var wk_high = $('#wk_high').val();
    var eps1 = $('#eps1').val();
    var eps2 = $('#eps2').val();
    var price_target = $('#price_target').val();
    var risk_profile = $('#risk_profile :selected').text();
    var portfolio_type = $('#portfolio_type :selected').text();
    var inv_rationale = $('#inv_rationable').val();
    var script_name = $('#Script_Name').val();
    var risk_profile_val = $('#risk_profile :selected').val();
    var portfolio_type_val = $('#portfolio_type :selected').val();


    var rec_id = rec_id.toString();
    var isin = isin.toString();
    var wk_low = wk_low.toString();
    var wk_high = wk_high.toString();
    var eps1 = eps1.toString();
    var eps2 = eps2.toString();
    var price_target = price_target.toString();
    var inv_rationale = inv_rationale.toString();

    if ($("#chk_actionable").is(":checked")) {
        actionable = "Y";
    }
    else {
        actionable = "N"
    }
    if ($("#chk_active").is(":checked")) {
        active = "Y";
    }
    else {
        active = "N"
    }
    var amd_flag;

    if (rec_id == 0) {
        amd_flag = "I"
        var formdata = {
            "reco_date": date,
            "rec_id": rec_id,
            "isin": isin,
            "portfolio_type": portfolio_type,
            "wk_high": wk_high,
            "wk_low": wk_low,
            "eps1": eps1,
            "eps2": eps2,
            "price_target": price_target,
            "risk_profile": risk_profile,
            "inv_rationale": inv_rationale,
            "active": active,
            "actionable": actionable,
            "user": loginid,
            "action": amd_flag,
            "rec_id": rec_id
        }
        var posturl = "/ClientPortal/Reports/psp_amd_model_portfolio_cp";
        if (date != '') {
            if (isin != '') {
                if (script_name != "Security name not found") {
                    if (wk_high != '' && wk_low != '' && eps1 != '' && eps2 != '' && price_target != '' && risk_profile != '' && portfolio_type_val != 0 && risk_profile_val != 0 && inv_rationale != '') {
                        if (wk_high > 0 && wk_low > 0 && eps1 > 0 && eps2 > 0 && price_target > 0) {
                            $.ajax({
                                url: posturl,
                                type: "post",
                                contentType: "application/json",
                                data: JSON.stringify(formdata),
                                success: function (data) {
                                    $("#dsp_form form")[0].reset();
                                    alert("Added Successfully");
                                    location.reload();
                                },
                                error: function (xhr, err) {
                                    alert(err)
                                }
                            })
                        }
                        else {
                            alert("Value cannot be 0.");
                        }
                    }

                    else {
                        alert("Please enter all fields");
                    }
                }
                else {
                    alert("Invalid ISIN");
                }
            }
            else {
                alert("Please enter ISIN");
            }
        }
        else {
            alert("Please select date");
        }
    }
    else {
        amd_flag = "U"
        var formdata = {
            "reco_date": date,
            "rec_id": rec_id,
            "isin": isin,
            "portfolio_type": portfolio_type,
            "wk_high": wk_high,
            "wk_low": wk_low,
            "eps1": eps1,
            "eps2": eps2,
            "price_target": price_target,
            "risk_profile": risk_profile,
            "inv_rationale": inv_rationale,
            "active": active,
            "actionable": actionable,
            "user": loginid,
            "action": amd_flag,
            "rec_id": rec_id
        }

        var posturl = "/ClientPortal/Reports/psp_amd_model_portfolio_cp";

        if (date != '') {
            if (isin != '') {
                if (script_name != "Security name not found") {
                    if (wk_high != '' && wk_low != '' && eps1 != '' && eps2 != '' && price_target != '' && risk_profile != '' && portfolio_type_val != 0 && risk_profile_val != 0 && inv_rationale != '') {
                        if (wk_high > 0 && wk_low > 0 && eps1 > 0 && eps2 > 0 && price_target > 0) {
                            $.ajax({
                                url: posturl,
                                type: "post",
                                contentType: "application/json",
                                data: JSON.stringify(formdata),
                                success: function (data) {
                                    $("#dsp_form form")[0].reset();
                                    alert("Updated Successfully");
                                    location.reload();
                                },
                                error: function (xhr, err) {
                                    alert(err)
                                }
                            })
                        }
                        else {
                            alert("Value cannot be 0.");
                        }
                    }

                    else {
                        alert("Please enter all fields");
                    }
                }
                else {
                    alert("Invalid ISIN");
                }
            }
            else {
                alert("Please enter ISIN");
            }
        }
        else {
            alert("Please select date");
        }
    }
}