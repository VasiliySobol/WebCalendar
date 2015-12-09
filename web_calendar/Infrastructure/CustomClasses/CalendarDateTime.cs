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

        public CalendarDateTime(DateTime _dateTime)
        {
            this.dateTime = _dateTime;
        }

        public CalendarDateTime(string _dateTime)
        {
            this.dateTime = Convert.ToDateTime(_dateTime);
        }

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

        public int GetYear()
        {
            return dateTime.Year;
        }

        public int GetMonth()
        {
            return dateTime.Month;
        }

        public int GetDay()
        {
            return dateTime.Day;
        }

        public string GetMonthName()
        {
            string[] monthName = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return monthName[dateTime.Month - 1];
        }

        public void SetDateTime(DateTime _dateTime)
        {
            this.dateTime = _dateTime;
        }

        public DateTime GetDateTime()
        {
            return this.dateTime;
        }

        public void AddDay(int offset)
        {
            this.dateTime = this.dateTime.AddDays(offset);
        }

        public void AddMonth(int offset)
        {
            this.dateTime = this.dateTime.AddMonths(offset);
        }       

        public int GetAmountOfDays()
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }      
    }
}
