/* Function to keep count of the number of characters enter by the user in the text box */

function CountText(field, maxlimit) {
    if (field.disabled == false) {
        if (field.value.length < maxlimit) {
            return true;
        }
        else
            return false;
    }
}

function keepWordLenTrack(field, maxlimit, lblCharactersLeft) {

    var lblCharLeft = document.getElementById(lblCharactersLeft);
    if (field.value.length > 0 && field.value.length < maxlimit) {

        lblCharLeft.innerHTML = '<b>' + (maxlimit - field.value.length).toString() + '</b> characters remaining';
    }
    else if (field.value.length == 0) {
        lblCharLeft.innerHTML = 'Maximum size: <b>' + maxlimit + '</b> Characters';
    }
    else if (field.value.length = maxlimit) {
        field.value = field.value.substr(0, maxlimit);
        lblCharLeft.innerHTML = '<b>' + (maxlimit - field.value.length).toString() + '</b> characters remaining';
    }
}

function ShowDeleteConfirmBox(objectName, objectValue) {
    var response = confirm('Are you sure to delete ' + objectName + ' "' + objectValue + '" ?');
    return response;
}

function SetCurveTableAllCorners(class_name) {

    $('.' + class_name).corner(
            { tl: { radius: 5 },
                tr: { radius: 5 },
                bl: { radius: 5 },
                br: { radius: 5 },
                antiAlias: true
            });
}

function SetCurveTableTopCorners(class_name) {

    $('.' + class_name).corner(
{ tl: { radius: 5 },
    tr: { radius: 5 },
    bl: { radius: 0 },
    br: { radius: 0 },
    antiAlias: true
});
}

