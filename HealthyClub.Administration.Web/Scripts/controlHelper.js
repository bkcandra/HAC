function validateLimit(obj, divID, maxchar) {

    objDiv = get_object(divID);

    if (this.id) obj = this;

    var remaningChar = maxchar - trimEnter(obj.value).length;

    if (objDiv.id) {
        objDiv.innerHTML = remaningChar + " characters left";
    }
    if (remaningChar <= 0) {
        obj.value = obj.value.substring(maxchar, 0);
        if (objDiv.id) {s
            objDiv.innerHTML = "0 characters left";
        }
        return false;
    }
    else
    { return true; }
}

function get_object(id) {
    var object = null;
    if (document.layers) {
        object = document.layers[id];
    } else if (document.all) {
        object = document.all[id];
    } else if (document.getElementById) {
        object = document.getElementById(id);
    }
    return object;
}

function trimEnter(dataStr) {
    return dataStr.replace(/(\r\n|\r|\n)/g, "");
}
