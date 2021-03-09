window.onload = function () {
    var addButtons = this.document.getElementsByClassName("btn-add-bookmark");
    if (addButtons !== null && addButtons != 'undefined') {
        for (var i = 0; i < addButtons.length; ++i) {
            var item = addButtons[i];
            item.style.visibility = "visible";
        }
    }
}