using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.CustomClasses;
using web_calendar.BL.DomainModels;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Models;

namespace web_calendar.BL.Mappers
{
    public static class CalendarMapper
    {
        public static CalendarViewModel ToCalendarViewModel(Calendar calendar)
        {
            List<DisplayEventViewModel> list = new List<DisplayEventViewModel>();

            foreach (var item in calendar.CalendarEventsCollection)
            {
                list.Add(EventMapper.MapToDisplayEventVM(item));
            }

            return new CalendarViewModel()
            {
                calendarColor = Color.FromArgb(calendar.CalendarColor.Value),
                description = calendar.Text,
                name = calendar.Name,
                id = calendar.Id,
                userId = calendar.UserId,
                CSSMainColor = CalendarDomainModel.ColorToMainCSS(Color.FromArgb(calendar.CalendarColor.Value)),
                CSSHeadColor = CalendarDomainModel.ColorToHeadCSS(Color.FromArgb(calendar.CalendarColor.Value)),
                calendarDateTime = new CalendarDateTime(calendar.ShowedDateTime.Value),
                eventList = list,
            };
        }

        public static Calendar ToCalendar(CalendarViewModel calendarViewModel)
        {
            return new Calendar()
            {
                CalendarColor = calendarViewModel.calendarColor.ToArgb(),
                Text = calendarViewModel.description,
                Name = calendarViewModel.name,
                Id = calendarViewModel.id,
                UserId = calendarViewModel.userId,
            };
        }
    }
}
