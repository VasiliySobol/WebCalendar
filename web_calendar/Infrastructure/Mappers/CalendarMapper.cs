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
            return new CalendarViewModel()
            {
                calendarColor = Color.FromArgb(calendar.CalendarColor.Value),
                description = calendar.Text,
                name = calendar.Name,
                timeZone = CalendarDomainModel.GetTimeZoneNameById(calendar.TimeZone),
                id = calendar.Id,
                userId = calendar.UserId,
                CSSMainColor = CalendarDomainModel.ColorToMainCSS(Color.FromArgb(calendar.CalendarColor.Value)),
                CSSHeadColor = CalendarDomainModel.ColorToHeadCSS(Color.FromArgb(calendar.CalendarColor.Value)),
                calendarDateTime = new CalendarDateTime(),
            };
        }

        public static Calendar ToCalendar(CalendarViewModel calendarViewModel)
        {
            return new Calendar()
            {
                CalendarColor = calendarViewModel.calendarColor.ToArgb(),
                Text = calendarViewModel.description,
                Name = calendarViewModel.name,
                TimeZone = CalendarDomainModel.GetTimeZoneIdByName(calendarViewModel.timeZone),
                Id = calendarViewModel.id,
                UserId = calendarViewModel.userId,
            };
        }
    }
}
