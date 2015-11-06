using System;
using System.Collections.Generic;
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
