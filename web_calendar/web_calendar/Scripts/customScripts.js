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

function SetColor(color, calendarLength)
{
    for (var i = 1; i < calendarLength; i++) {
        document.getElementById("cellId" + i).style.backgroundColor = color;
    }
}