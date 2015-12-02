using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.Services;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Models;

namespace web_calendar.BL.Mappers
{
    public static class CalendarMapper
    {

        // -------- Calendars --------

        public static CalendarViewModel MapToCalendarViewModel(Calendar calendar)
        {
            CalendarViewModel calendarVM = new CalendarViewModel();
            calendarVM.calendarColor = Color.FromArgb(calendar.CalendarColor.Value);
            calendarVM.description = calendar.Text;
            calendarVM.name = calendar.Name;
            calendarVM.notificationSettings = new NotificationSettingsViewModel();
            calendarVM.timeZone = CalendarService.GetTimeZoneNameById(calendar.TimeZone);
            calendarVM.visibility = calendar.Visibility;
            calendarVM.id = calendar.Id;
            calendarVM.userId = calendar.UserId;
            calendarVM.CSSMainColor = CalendarService.ColorToMainCSS(calendarVM.calendarColor);
            calendarVM.CSSHeadColor = CalendarService.ColorToHeadCSS(calendarVM.calendarColor);
            return calendarVM;
        }

        public static Calendar MapToCalendarFromCalendarVM(CalendarViewModel calendarVM)
        {
            Calendar calendar = new Calendar();
            calendar.CalendarColor = calendarVM.calendarColor.ToArgb();
            calendar.Text = calendarVM.description;
            calendar.Name = calendarVM.name;
            calendar.TimeZone = CalendarService.GetTimeZoneIdByName(calendarVM.timeZone);
            calendar.Visibility = calendarVM.visibility;
            calendar.Id = calendarVM.id;
            calendar.UserId = calendarVM.userId;
            return calendar;
        }
    }
}
