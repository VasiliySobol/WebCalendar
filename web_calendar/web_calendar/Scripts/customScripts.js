arrayOfCalendars = new Array();

function GetCalendarId(id)
{
    return arrayOfCalendars[id];
}

function GetCalendarColor(id)
{
    alert(arrayOfCalendars[id].CSSColor);
}

function Add(data)
{
    arrayOfCalendars.push(data);
}

function SetsColor(idName)
{
    document.getElementById(idName).style.backgroundColor = "#AA0000";
    alert("fdsa");
}

function SetHeadColor(color)
{
    for (var i = 1; i <= 7; i++) {
        document.getElementById("day" + i).style.backgroundColor = color;
    }
}

function SetMainColor(color, calendarLength)
{
    for (var i = 1; i < calendarLength; i++) {
        document.getElementById("cellId" + i).style.backgroundColor = color;
    }
}