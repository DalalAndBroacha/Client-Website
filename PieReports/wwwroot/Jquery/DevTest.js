$(document).ready(function () {

    CollapseSideMenu();
    HideFinYearList();

    //textAreas.forEach(textarea => {
    //    console.log(textarea)
    //    textarea.addEventListener('keyup', updateCounter);
    //    textarea.addEventListener('input', updateCounter);
    //    textarea.addEventListener('paste', updateCounter);

    //});
    const inputElement = document.getElementById('textAreaContent');

    // 2. Add the event listener
    inputElement.addEventListener('input', function (event) {

        console.log('Current value:', event);
        console.log('Current value:', event.target.value);
        console.log('Current Lenght:', event.target.value.length);
        //updateCounter(event.target.value.length); 
    });

});

const countDisplay = document.getElementById('charCount');
let currentLength = 0;
const maxLength = 500;

function updateCounter(newLen) {

    console.log(currentLength)

    currentLength += newLen ?? 0;
        
    const remaining = maxLength - currentLength;
    countDisplay.textContent = `${remaining} characters remaining`;

    // Optional: Add visual cues when the limit is reached or almost reached
    if (remaining <= 0) {
        countDisplay.style.color = 'red';
    } else {
        countDisplay.style.color = 'black';
    }
}

//    // Initial count display
//    updateCounter();


