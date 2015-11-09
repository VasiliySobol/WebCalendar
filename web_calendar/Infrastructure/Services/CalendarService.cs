using System;
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

        public static string[] GetTimeZoneNamesList()
        {
            string[] dateTimeNamesList = new string[timeZoneList.Count];
            for (int i = 0; i < timeZoneList.Count; i++)
            {
                dateTimeNamesList[i] = timeZoneList[i].DisplayName;
            }
            return dateTimeNamesList;
        }

        public static int GetTimeZoneIdByName(string timeZoneName)
        {
            for (int i = 0; i < timeZoneList.Count; i++)
            {
                if (timeZoneName == timeZoneList[i].DisplayName)
                {
                    return i;
                }
            }
            return 0;
        }

        public static void CreateDefaultCalendar(string userId)
        {
            Calendar calendar = new Calendar();
            calendar.Name = "Default calendar";
            calendar.UserId = userId;
            //initialize other properties
            calendarRepository.Add(calendar);
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
