$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    $('#ddlBHRMList').select2({ dropdownCssClass: 'bigdrop' });
    $('#ddlScripList').select2({ dropdownCssClass: 'bigdrop', maximumSelectionLength: 10 });
    $('#ddlDataList').select2({ dropdownCssClass: 'bigdrop' });

    let Tdate = (new Date()).toISOString().split('T')[0];
    let maxDate = formatDateold(addDays(Tdate, -366));
    $("#inpFromDate").val(maxDate);
    $("#inpFromDate").attr("max", Tdate);

    $("#inpFromDate").trigger("change");

});


$("#ddlBHRMList").change(function () {
    $('#divDataTable').hide();
});
$("#ddlScripList").change(function () {
    $('#divDataTable').hide();
});
$("#inpFromDate").change(function () {

    let date = $(this).val();

    let Tdaydate = (new Date()).toISOString().split('T')[0];

    let maxDate = formatDateold(addDays(date, 366));

    //$("#inpEndDate").prop("disabled", false);

    if (maxDate > Tdaydate) {
        $("#inpToDate").val(Tdaydate);
        $("#inpToDate").attr("min", date);
        $("#inpToDate").attr("max", Tdaydate);
    }
    else {
        $("#inpToDate").val(maxDate);
        $("#inpToDate").attr("min", date);
        $("#inpToDate").attr("max", maxDate);
    }

});

const fnHideParamsTray = function () {
    $("#paramsTray").hide();
    $("#divDataTblComponent").css("max-height", "75vh");
}

const fnShowParamsTray = function () {
    $("#paramsTray").show();
    $("#divDataTblComponent").css("max-height", "55vh");
}


function completeajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "none");
}
function startajaxrequestwaitIn() {
    $("#GlobalwaitIn").css("display", "block");
}
function ViewData() {

    const url = "/ClientPortal/Reports/psp_dsp_research_view_given_brokerage";

    let parameterObj = {
        "from_date": $("#inpFromDate").val(),
        "to_date": $("#inpToDate").val(),
        "scrip_code": $("#ddlScripList").val().join(),
        "rm_id": $("#ddlBHRMList").val()
    }

    if (parameterObj.scrip_code != "") {

        $.ajax({
            url: url,
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(parameterObj),
            beforeSend: function (xhr) {
                startajaxrequestwaitIn();
            },
            success: function (data) {

                $('#tblData').DataTable().destroy();
                $("#tbodyData").html("");
                let html = "";

                if (data.length > 0) {
                    $.each(data, function (index, value) {
                        html = html + "<tr>" +
                            "<td class='paraTextAlignLeft'>" + value.script_Name + "</td>" +
                            "<td class='paraTextAlignLeft'>" + value.branch_name + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.buy_Qty.toFixed()) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.buy_client.toFixed()) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.sell_Qty.toFixed()) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.sell_client.toFixed()) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.buy_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.sell_volume.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.brokerage.toFixed(2)) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.branch_client_cnt.toFixed()) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.client_scrip_holding.toFixed()) + "</td>" +
                            "<td class='paraTextAlignRight'>" + numberWithCommas(value.client_hld_scrip_cnt.toFixed()) + "</td>" +
                            "</tr>"
                    });

                    $("#tbodyData").append(html);

                    DrawDataTable();
                }
                else {
                    alert("No data found.");
                }
            },
            complete: function (xhr) {
                completeajaxrequestwaitIn();
            },
            error: function (xhr, err) {
                alert(err)
            }
        })
    }
    else {
        alert("Please select atleast one Script.")
    }
}
function DrawDataTable() {

    $('#tblData').DataTable({
        fixedHeader: true,
        autoWidth: false,
        columnDefs: [{
            width: "15%",
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
            width: "10%",
            targets: 3
        },
        {
            width: "10%",
            targets: 4
        },
        {
            searchable: false,
            width: "5%",
            targets: 5
        },
        {
            searchable: false,
            width: "10%",
            targets: 6
        },
        {
            searchable: false,
            width: "5%",
            targets: 7
        },
        {
            searchable: false,
            width: "5%",
            targets: 8
        },
        {
            searchable: false,
            width: "10%",
            targets: 9
        },
        {
            searchable: false,
            width: "5%",
            targets: 10
            },
            {
                searchable: false,
                width: "5%",
                targets: 11
            }
        ],
        "pageLength": globalDataGridPageLenght,
        lengthMenu: [
            [5, 50, 100],
            [5, 50, 100]
        ],
        language: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries. "
        },
        "dom": "<'row justify-content-between'<'col-lg-4'l><'col-lg-4'f>>" +
            "<'row justify-content-between'<'col-lg-4'i><'col-lg-4'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        order: [
            [0, 'asc'],
            [1, 'asc'],
            [4, 'asc']
        ],
        "initComplete": function (settings, json) {
            $('#tblData').DataTable().columns.adjust().draw();

            $('#btnEmail').show();
            $('#btnExcel').show();

            $('#divDataTable').show();
            fnHideParamsTray();
        }
    });
}
function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()
    let from_date = $("#inpFromDate").val()
    let to_date = $("#inpToDate").val()
    let scrip_code = $("#ddlScripList").val().join()
    let rm_id = $("#ddlBHRMList").val()

    let strPara = LoginId + "~" + from_date + "~" + to_date + "~" + "3" + "~" + scrip_code + "~" + rm_id;

    let strDescrip = "Volume and Brokerage Report of " + $("#ddlBHRMList :selected").text() + " for the period " + from_date + " to " + to_date;

    PDFandExcelExport(evt_btn, "51", LoginId, "0", export_type, export_format, strPara, strDescrip);

}

$("#btnEmail").click(function () { //Email
    GenerateExportDetails("#btnEmail", "E", "EXCEL")
})

$("#btnExcel").click(function () { //Excel
    GenerateExportDetails("#btnExcel", "X", "EXCEL")
})

$("#btnParams").click(function () { //Params Toggle

    if ($('#paramsTray').is(':visible')) {
        fnHideParamsTray();
    }
    else {
        fnShowParamsTray();
    }

})