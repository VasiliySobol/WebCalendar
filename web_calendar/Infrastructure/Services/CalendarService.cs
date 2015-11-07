﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.Mappers;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Interface;
using web_calendar.DAL.Models;

namespace web_calendar.BL.Services
{
    public static class CalendarService
    {
        public static ICalendarRepository calendarRepository;
        public static Dictionary<int, TimeZoneInfo> timeZoneList = new Dictionary<int, TimeZoneInfo>();

        static CalendarService()
        {
            ReadOnlyCollection<TimeZoneInfo> timeZoneInfoList = TimeZoneInfo.GetSystemTimeZones();
            
            for (int i = 0; i < timeZoneInfoList.Count; i++)
            {
                timeZoneList.Add(i, timeZoneInfoList[i]);
            }
        }

        public static IEnumerable<CalendarViewModel> GetCalendarViewModels(string _userId)
        {
            IEnumerable<Calendar> listOfCalendars = calendarRepository.GetUserCalendars(_userId);
            List<CalendarViewModel> listOfCalendarViews = new List<CalendarViewModel>();

            foreach (Calendar calendar in listOfCalendars)
            {
                listOfCalendarViews.Add(Mapper.MapToCalendarViewModel(calendar));
            }

            return listOfCalendarViews;
        }

        public static CalendarViewModel GetDetails(int _id)
        {
            return Mapper.MapToCalendarViewModel(calendarRepository.FindById(_id));
        }
    }
}
