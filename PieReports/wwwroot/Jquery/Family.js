
var $add = $("#addfev");
var $remove = $("#removefev");

$add.on("click", function () {
    var itemId = document.getElementById("menuId_Common Documents").value;
    alert("adding item " + itemId);

    // do your AJAX stuff to add the favourite here

});

$remove.on("click", function () {
   
    var itemId = document.getElementById("menuId_Common Documents").value;
    alert("removing item " + itemId);

    // do your AJAX stuff to remove the favourite here

});