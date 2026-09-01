function isNegativeValue(value) {
    if (value < 0) {
        return " negavtiveValueField";
    }
    else {
        return " ";
    }
}

function blockSpecialChar(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
}

function preventCommaAndSpace(event) {
    if (event.charCode == 44 || event.charCode == 32) {
        return false;
    } else {
        return true;
    }
}

$('.disablePaste').bind("paste", function (e) {
    e.preventDefault();
});

function ValidatePAN(enteredText) {
    if (enteredText.match(/[A-Z]{5}[0-9]{4}[A-Z]{1}$/)) {
        return true;
    }
    else {
        return false;
    }
}