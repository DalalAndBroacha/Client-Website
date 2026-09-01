$(document).ready(function () {
    CollapseSideMenu();   
    HideFinYearList();
    HideFamilyList();

    $("#repo_name").focus();
});

let subsecindex = 1

function addsection() { 

    const sec = $("#section").val();
    $("#sec_accordian").removeClass("d-none");

    html = "";

    if (sec != "") {
       
        const tblsubsecId = "tblsubsection" + subsecindex;

        const divtblsubsecId = "div" + subsecindex

      /*  console.log(tblsubsecId);*/

        //html = "<tr><td>abcd</td><td>delete</td></tr>"

        html = html + "<tr class = 'GrandTotalRow' id ='secname'>" +
            "<td class = 'GrandTotalRowData'><p style='font-size:14px;font-family:Roboto, sans-serif; font-weight:bold; text-align: left !important;'>" +
            "<button title='Expand for Details' style='border:none; padding:0; background-color: inherit;' data-toggle='collapse' data-target=\'#" + divtblsubsecId+ "\' aria-expanded='false' aria-controls='#div_" + 1 + "'>" +
            "<i class='far fa-plus-square fa-sm accordianButtons'></i><i class='far fa-minus-square fa-sm accordianButtons'></i></button>&nbsp;&nbsp;" +
            sec + "</td>"+
            "<td><button type='button' title='Add Sub Section' id='btnaddsubsection' onclick=\"addsubsection('" + sec + "','" + tblsubsecId +"')\">+</button></td></tr>" +

            "<tr><td><div class='collapse' id=\'" + divtblsubsecId +"\'data-parent='#tblsection'>" +

            "<table class='table table-hover'>" +
            //<button id="btndelsection" class="btn Del_rec" title="Delete" onclick="btn_secdelete(this)"><i class="fas fa-trash-alt fa-lg White_Red_Icon"></i></button>

            "<tr><colgroup><col style = 'width: 20%;'><col style='width: 80%;'></colgroup><th>Sub Section Name</th><th>Sub Section Detail </th>" +
            "<tbody id=\'" + tblsubsecId +"\'></tbody></tr>" +
            "</table></div></td></tr>";

        $("#tblsection").append(html);

        subsecindex++
    }
    else {
        alert("Please fill the section");
    }

    $("#section").val("");
    $("#section").focus();
}

function addsubsection(sec, tblsubsecId) {

    //var index = $("#txtSection").val(sec);

    $('#dsp_subsection').modal('show')
    $("#subsectblid").val(tblsubsecId);
    console.log(sec, tblsubsecId)
}
function ADDUpdate_SubSec() {
    const subsecname = $("#Subsectiontitle").val();
    const subsecdetail = $("#Subsectiondetail").val();
    const subsectblid = $("#subsectblid").val();

    console.log(subsecname, subsecdetail, subsectblid)

    html = ""

    html = html + "<tr><td>" + subsecname + "</td><td>" + subsecdetail +"</td></tr>"

    $("#" + subsectblid).append(html);

    $("#Subsectiontitle").val("");
    $("#Subsectiondetail").val("");
}


