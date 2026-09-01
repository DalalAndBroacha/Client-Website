$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();
    HideFamilyList();

    const Tdaydate = (new Date()).toISOString().split('T')[0];
    const minDate = formatDateold(addDays(Tdaydate, -366));

    $("#uploadDate").val(Tdaydate);
    $("#uploadDate").attr("min", minDate);
    $("#uploadDate").attr("max", Tdaydate);

    $("#inpFromDate").val(Tdaydate);
    $("#inpFromDate").attr("min", minDate);
    $("#inpFromDate").attr("max", Tdaydate);

    $("#inpToDate").val(Tdaydate);
    $("#inpToDate").attr("min", minDate);
    $("#inpToDate").attr("max", Tdaydate);

});
//const selectedFile = fileInput.files[0];

$("#inpFromDate").change(function () {

    let date = $(this).val();
    let Tdaydate = (new Date()).toISOString().split('T')[0];
    let maxDate = formatDateold(addDays(date, 365));

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

function startajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "block");
}

function completeajaxrequestGlobal() {
    $("#GlobalwaitIn").css("display", "none");
}


document.getElementById('formAdd').addEventListener('submit', function (event) {

    event.preventDefault();

    const dateInput = document.getElementById('uploadDate');
    const fileInput = document.getElementById('reportFile');

    if (dateInput.value == "") {
        alert('Please select date.');
        event.preventDefault(); // Prevent form submission
        return false;
    }

    if (fileInput.files.length === 0) {
        alert('Please select a file to upload.');
        event.preventDefault(); // Prevent form submission
        return false;
    }
    else {
        const selectedFile = fileInput.files[0];
        const allowedExtensions = ['xls'];
        const fileName = selectedFile.name;
        const fileExtension = fileName.split('.').pop().toLowerCase();

        if (!allowedExtensions.includes(fileExtension)) {
            alert('Invalid file type. Please upload a Excel file with xls extention.');
            event.preventDefault(); // Prevent form submission
            fileInput.value = null;
            return false;
        }

        const maxSizeInBytes = 5 * 1024 * 1024; // 5MB
        if (selectedFile.size > maxSizeInBytes) {
            alert('File size exceeds the limit of 5MB.');
            event.preventDefault(); // Prevent form submission
            fileInput.value = null;
            return false;
        }
        // Perform other file validations as needed
        // If validation fails, call event.preventDefault();
    }

    const formData = new FormData($('#formAdd')[0]);
    let url = "/ClientPortal/Pie/uploadEqSetData";
    $.ajax({
        url: url,
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        beforeSend: function (xhr) {
            $("#btnUpload").hide();
            startajaxrequestGlobal();
        },
        success: function (data) {
            alert(data.msg);
            location.reload();

        },
        complete: function (xhr) {
            completeajaxrequestGlobal();
            $("#btnUpload").show();
        },
        error: function (data) {
            // there was an error.
            alert('There was an error in the request.');
            location.reload();
        }
    });
    
});

function GenerateExportDetails(evt_btn, export_type, export_format) { //export_type = E: Email, X:PDF

    let LoginId = $("#LoggedInUID").val()

    let FromDate = $('#inpFromDate').val();
    let ToDate = $('#inpToDate').val();

    const FromDateparts = FromDate.split("-"); // ["2025", "11", "20"]
    const FromDateDsp = FromDateparts[2] + "/" + FromDateparts[1] + "/" + FromDateparts[0]; // "20/11/2025"

    const ToDateparts = ToDate.split("-"); // ["2025", "11", "20"]
    const ToDateDsp = ToDateparts[2] + "/" + ToDateparts[1] + "/" + ToDateparts[0]; // "20/11/2025"


    let strPara = LoginId + "~" + FromDate + "~" + ToDate;

    let strDescrip = "Settlement Report Log from " + FromDateDsp + " to " + ToDateDsp;

    PDFandExcelExport(evt_btn, "92", LoginId, "0", export_type, export_format, strPara, strDescrip);
}

$("#btnDownloadXls").click(function () {
    GenerateExportDetails("#btnDownloadXls", "X", "EXCEL");
    location.reload();
})