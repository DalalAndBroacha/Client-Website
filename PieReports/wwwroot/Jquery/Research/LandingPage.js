$(document).ready(function () {

    $("#OldReportsPage").append('Research reports prior to 01/04/2023 click <a style="font-size:1.25rem;" href="https://www.dalal-broacha.com/Old_Research_Reports.html" target="_blank">here</a>.');

    $("#tblReportsData").DataTable({
        "initComplete": function (settings, json) {
           
            $('#tblReportsData').DataTable().columns.adjust().draw();
        },
        // For more Info on DOM https://datatables.net/reference/option/dom
        // DOM example - https://stackoverflow.com/questions/39407881/pagination-at-top-and-bottom-with-datatables#:~:text=You%20can%20put%20pagination%20at,'p%3E%3E%22%2C%20%7D)%3B
        "dom": "<'row justify-content-between'<'col-sm-3'l><'col-sm-3'f><'col-sm-3'p>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        "pageLength": globalDataGridPageLenght,
        "lengthMenu": [25, 50, 100],
        "pagingType": "full_numbers",
        "order": [],
        "aoColumns": [ //To Disable sorting. By setting Ordering: false gives a bug on first col.
            //Reference:https://stackoverflow.com/questions/39285643/datatable-jquery-how-to-remove-sort-icon-from-first-column
            { "sWidth": "10%" },
            { "sWidth": "10%" },
            { "sWidth": "25%" },
            {
                "sWidth": "10%", "mRender": function (data, type, full) {
                    if (data == "") {
                        return "<p class='paraTextAlignCenter'> - </p>"
                    }
                    else {
                        return "<p>"+data+"</p>"
                    }
                            
                }
            },
            { "sWidth": "10%" },
            {
                "sWidth": "5%", "mRender": function (data, type, full) {
                    if (data == 0) {
                        return "<p class='paraTextAlignCenter'> - </p>"
                    }
                    else {
                        return "<p class='paraTextAlignRight'>" + numberWithCommas(Number(data).toFixed(2)) + "</p>"
                    }
                }
            },
            {
                "sWidth": "5%", "mRender": function (data, type, full) {
                    if (data == 0) {
                        return "<p class='paraTextAlignCenter'> - </p>"
                    }
                    else {
                        return "<p class='paraTextAlignRight'>" + numberWithCommas(Number(data).toFixed(2)) + "</p>"
                    }
                }
            },
            {
                "sWidth": "5%", "mRender": function (data, type, full) {
                    if (data == 0) {
                        return "<p class='paraTextAlignCenter'> - </p>"
                    }
                    else {
                        return "<p class='paraTextAlignRight'>" + numberWithCommas(Number(data).toFixed(2)) + "</p>"
                    }
                }
            },
            { "sWidth": "5%", "bSortable": false, "bSearchable": false }
        ]
    });


});