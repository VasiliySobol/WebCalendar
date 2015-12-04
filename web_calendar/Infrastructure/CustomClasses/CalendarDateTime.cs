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

        public int GetWeekDayNumberOfFirstMonthDay()
        {
            DateTime currentMonthDateTime = new DateTime(dateTime.Year, dateTime.Month, 1);
             return (int)currentMonthDateTime.DayOfWeek;
        }

        public int GetWeekDayNumberOfLastMonthDay()
        {
            DateTime currentMonthDateTime = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
            return (int)currentMonthDateTime.DayOfWeek;
        }

        public int GetAmountOfDays()
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        public void ShowNextMonth()
        {
            dateTime = new DateTime(dateTime.Year, dateTime.Month + 1, 1);
        }

        public CalendarDateTime()
        {
            dateTime = DateTime.Now;
        }
    }
}
