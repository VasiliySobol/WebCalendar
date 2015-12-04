function SetHeadColor(color) {
    for (var i = 1; i <= 7; i++) {
        document.getElementById("day" + i).style.backgroundColor = color;
    }
}

function SetMainColor(color) {
    for (var i = 1; i <= 31; i++) {
        document.getElementById("cellId" + i).style.backgroundColor = color;
    }
}