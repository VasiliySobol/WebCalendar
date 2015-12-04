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

function setActiveNavigator(title) {
    switch (title) {
        case "day": {
            document.getElementById("navDay").style.display = "block";
            document.getElementById("navWeek").style.display = "none";
            document.getElementById("navMonth").style.display = "none";
            break;
        };
        case "week": {
            document.getElementById("navDay").style.display = "none";
            document.getElementById("navWeek").style.display = "block";
            document.getElementById("navMonth").style.display = "none";
            break;
        };
        case "month": {
            document.getElementById("navDay").style.display = "none";
            document.getElementById("navWeek").style.display = "none";
            document.getElementById("navMonth").style.display = "block";
            break;
        };
    };
};

function setColor(items, color) {
    for (var i = 0; i < items.length; i++) {
        items[i].style.backgroundColor = color;
    }
}