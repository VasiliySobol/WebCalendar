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

function SetColorToScrollCalendar(color) {
    for (var i = 1; i <= 24; i++) {
        document.getElementById("time" + i).style.backgroundColor = color;
        document.getElementById("eve" + i).style.backgroundColor = color;
    }
}

function setColor(items, color) {
    for (var i = 0; i < items.length; i++) {
        items[i].style.backgroundColor = color;
    }
}