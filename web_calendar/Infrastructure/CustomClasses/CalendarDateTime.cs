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

        public void SetDateTime(DateTime _dateTime)
        {
            this.dateTime = _dateTime;
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

        public void SetPrevMonth(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.', '/');
            string[] timeData = dateAndTime[1].Split(':');

            int prevDay = int.Parse(dateData[0]);
            int prevMonth = int.Parse(dateData[1]) - 1;
            int prevYear = int.Parse(dateData[2]);

            if (prevMonth == 0)
            {
                prevYear--;
                prevMonth = 12;
            }

            this.dateTime = new DateTime(prevYear, prevMonth, prevDay);
        }

        public void SetNextMonth(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.','/');
            string[] timeData = dateAndTime[1].Split(':');

            int nextDay = int.Parse(dateData[0]);
            int nextMonth = int.Parse(dateData[1]) + 1;
            int nextYear = int.Parse(dateData[2]);

            if (nextMonth == 13)
            {
                nextYear++;
                nextMonth = 1;
            }

            this.dateTime = new DateTime(nextYear, nextMonth, nextDay);
        }

        public void SetPrevDay(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.', '/');
            string[] timeData = dateAndTime[1].Split(':');

            int prevDay = int.Parse(dateData[0]) - 1;
            int prevMonth = int.Parse(dateData[1]);
            int prevYear = int.Parse(dateData[2]);

            if (prevDay == 0)
            {
                prevMonth--;
                if (prevMonth == 0)
                {
                    prevMonth = 12;
                    prevYear--;
                }
                prevDay = DateTime.DaysInMonth(prevYear, prevMonth);
            }

            this.dateTime = new DateTime(prevYear, prevMonth, prevDay);
        }

        public void AddDay(int offset)
        {
            this.dateTime.AddDays(offset);
        }

        public void SetNextDay(string _dateTime)
        {
            if (_dateTime == null)
            {
                _dateTime = DateTime.Now.ToString();
            }

            string[] dateAndTime = _dateTime.Split(' ');
            string[] dateData = dateAndTime[0].Split('.', '/');
            string[] timeData = dateAndTime[1].Split(':');

            int nextDay = int.Parse(dateData[0]) + 1;
            int nextMonth = int.Parse(dateData[1]);
            int nextYear = int.Parse(dateData[2]);

            if (nextDay == DateTime.DaysInMonth(nextYear, nextMonth) + 1)
            {
                nextMonth++;
                if (nextMonth == 13)
                {
                    nextMonth = 1;
                    nextYear++;
                }
                nextDay = 1;
            }

            this.dateTime = new DateTime(nextYear, nextMonth, nextDay);
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
