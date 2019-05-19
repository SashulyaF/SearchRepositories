window.onload = function () {
    var addButtons = this.document.getElementsByClassName("btn-add-bookmark");
    addButtons.forEach(function (element) {
        element.style.visibility = "visible";
    });
}