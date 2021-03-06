﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.Mappers;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Interface;
using web_calendar.DAL.Models;

namespace web_calendar.BL.DomainModels
{
    public static class CalendarDomainModel
    {
        public static ICalendarRepository calendarRepository;
        public static Dictionary<int, TimeZoneInfo> timeZoneList = new Dictionary<int, TimeZoneInfo>();

        static CalendarDomainModel()
        {
            ReadOnlyCollection<TimeZoneInfo> timeZoneInfoList = TimeZoneInfo.GetSystemTimeZones();
            
            for (int i = 0; i < timeZoneInfoList.Count; i++)
            {
                timeZoneList.Add(i, timeZoneInfoList[i]);
            }
        }

        public static string ColorToMainCSS(Color calendarColor)
        {
            float correctionFactor = 0.8f;
            float R = (255 - calendarColor.R) * correctionFactor + calendarColor.R;
            float G = (255 - calendarColor.G) * correctionFactor + calendarColor.G;
            float B = (255 - calendarColor.B) * correctionFactor + calendarColor.B;
            Color mainCalendarColor = Color.FromArgb(calendarColor.A, (int)R, (int)G, (int)B);

            return "#" + mainCalendarColor.R.ToString("X2") + mainCalendarColor.G.ToString("X2") + mainCalendarColor.B.ToString("X2");
        }

        public static string ColorToHeadCSS(Color calendarColor)
        {
            float correctionFactor = 0.4f;
            float R = (255 - calendarColor.R) * correctionFactor + calendarColor.R;
            float G = (255 - calendarColor.G) * correctionFactor + calendarColor.G;
            float B = (255 - calendarColor.B) * correctionFactor + calendarColor.B;
            Color headCalendarColor = Color.FromArgb(calendarColor.A, (int)R, (int)G, (int)B);


            return "#" + headCalendarColor.R.ToString("X2") + headCalendarColor.G.ToString("X2") + headCalendarColor.B.ToString("X2");
        }

        public static string[] GetTimeZoneNamesList()
        {
            string[] dateTimeNamesList = new string[timeZoneList.Count];
            for (int i = 0; i < timeZoneList.Count; i++)
            {
                dateTimeNamesList[i] = timeZoneList[i].DisplayName;
            }
            return dateTimeNamesList;
        }

        public static byte GetTimeZoneIdByName(string timeZoneName)
        {
            for (byte i = 0; i < timeZoneList.Count; i++)
            {
                if ((timeZoneName == timeZoneList[i].DisplayName) || (timeZoneName == timeZoneList[i].StandardName))
                {
                    return i;
                }
            }
            return 0;
        }

        public static string GetTimeZoneNameById(byte? timeZoneId)
        {
            return timeZoneList[(int)timeZoneId].DisplayName;
        }

        public static void CreateDefaultCalendar(string userId)
        {
            Calendar calendar = new Calendar();
            
            calendar.Name = "Default calendar";
            calendar.Text = "Default calendar-description";
            calendar.UserId = userId;
            calendar.CalendarColor = 0;
            calendar.ShowedDateTime = DateTime.Now;

            calendarRepository.Add(calendar);
            calendarRepository.SaveChanges();
        }

        public static IEnumerable<CalendarViewModel> GetCalendarViewModels(string _userId)
        {
            IEnumerable<Calendar> listOfCalendars = calendarRepository.GetUserCalendars(_userId);
            List<CalendarViewModel> listOfCalendarViews = new List<CalendarViewModel>();

            foreach (Calendar calendar in listOfCalendars)
            {
                listOfCalendarViews.Add(CalendarMapper.ToCalendarViewModel(calendar));
            }

            return listOfCalendarViews;
        }

        public static CalendarViewModel GetDetails(int _id)
        {
            return CalendarMapper.ToCalendarViewModel(calendarRepository.FindById(_id));
        }

        public static List<CalendarEvent> GetCalendarEventDayList(int id, DateTime date)
        {
            Calendar activeCalendar = CalendarDomainModel.calendarRepository.FindById(id);
            List<CalendarEvent> dayEventList = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in activeCalendar.CalendarEventsCollection)
            {
                if (calendarEvent.TimeBegin.DayOfYear == date.DayOfYear)
                {
                    dayEventList.Add(calendarEvent);
                }
            }
            return dayEventList;
        }

        public static List<CalendarEvent> GetCalendarEventWeekList(int id, DateTime date)
        {
            Calendar activeCalendar = CalendarDomainModel.calendarRepository.FindById(id);
            List<CalendarEvent> dayEventList = new List<CalendarEvent>();
            int dayofweek = (int)date.DayOfWeek;
            DateTime first = date.AddDays(-dayofweek);
            DateTime seventh = date.AddDays(6 - dayofweek);
            
            foreach (CalendarEvent calendarEvent in activeCalendar.CalendarEventsCollection)
            {
                if (calendarEvent.TimeBegin.DayOfYear >= first.DayOfYear && calendarEvent.TimeBegin.DayOfYear <= seventh.DayOfYear)
                {
                    dayEventList.Add(calendarEvent);
                }
            }
            return dayEventList;
        }
    }
}
