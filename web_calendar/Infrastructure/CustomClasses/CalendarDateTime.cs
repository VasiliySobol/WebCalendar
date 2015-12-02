using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.CustomClasses
{
    public class CalendarDateTime
    {
        DateTime dateTime { get; set; }
        int weekDayNumberOfFirstMonthDay { get; set; }

        public int GetWeekDayNumberOfFirstMonthDay()
        {
            DateTime currentMonthDateTime = new DateTime(dateTime.Year, dateTime.Month, 1);
            return (int)currentMonthDateTime.DayOfWeek;
        }

        public int GetFirstDayNumberAtLineFromPrevMonth()
        {
            return (DateTime.DaysInMonth(dateTime.Year, dateTime.Month - 1) - GetWeekDayNumberOfFirstMonthDay());
        }

        public CalendarDateTime()
        {
            dateTime = DateTime.Now;
        }
            //firstDayNumberAtLineFromPrevMonth
    }
}
