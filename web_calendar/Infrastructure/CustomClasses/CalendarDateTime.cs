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

        public int GetYear()
        {
            return dateTime.Year;
        }

        public string GetMonth()
        {
            string monthName = "";

            switch (dateTime.Month)
            {
                case 1: { monthName = "January"; break; }
                case 2: { monthName = "February"; break; }
                case 3: { monthName = "March"; break; }
                case 4: { monthName = "April"; break; }
                case 5: { monthName = "May"; break; }
                case 6: { monthName = "June"; break; }
                case 7: { monthName = "July"; break; }
                case 8: { monthName = "August"; break; }
                case 9: { monthName = "September"; break; }
                case 10: { monthName = "October"; break; }
                case 11: { monthName = "November"; break; }
                case 12: { monthName = "December"; break; }
            }
            return monthName;
        }

        public void SetDateTime(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.', '/');
            string[] timeData = dateAndTime[1].Split(':');

            this.dateTime = new DateTime(int.Parse(dateData[2]), int.Parse(dateData[1]), int.Parse(dateData[0]));
        }

        public void SetPrevDateTime(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.', '/');
            string[] timeData = dateAndTime[1].Split(':');

            this.dateTime = new DateTime(int.Parse(dateData[2]), int.Parse(dateData[1]) - 1, int.Parse(dateData[0]));
        }

        public void SetNextDateTime(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.','/');
            string[] timeData = dateAndTime[1].Split(':');

            this.dateTime = new DateTime(int.Parse(dateData[2]), int.Parse(dateData[1]) + 1, int.Parse(dateData[0]));
        }

        public DateTime GetDateTime()
        {
            return dateTime;
        }

        public int GetAmountOfDays()
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        public CalendarDateTime(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }
            
            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.', '/');
            string[] timeData = dateAndTime[1].Split(':');

            this.dateTime = new DateTime(int.Parse(dateData[2]), int.Parse(dateData[1]), int.Parse(dateData[0]));
        }
    }
}
