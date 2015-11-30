using web_calendar.BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;
using System.Drawing;
using web_calendar.BL.Services;

namespace web_calendar.BL.Mappers
{
    public static class Mapper
    {
        // -------- Events --------

        public static DetailsEventViewModel MapToDetailsEventVM(CalendarEvent calendarEvent, List<string> guests)
        {
            DetailsEventViewModel eventVM = new DetailsEventViewModel();
            eventVM.Id = calendarEvent.Id;
            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.Visibility = calendarEvent.Visibility;
            eventVM.AllDay = calendarEvent.AllDay.Value.ToString();
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.TimeEnd = calendarEvent.TimeEnd;
            if (calendarEvent.CalendarId != null)
            {
                eventVM.CalendarId = (int)calendarEvent.CalendarId;
                eventVM.CalendarName = calendarEvent.Calendar.Name;
            }
            eventVM.Guests = new List<string>();
            if (guests != null)
                eventVM.Guests.AddRange(guests);
            return eventVM;
        }

        public static DisplayEventViewModel MapToDisplayEventVM(CalendarEvent calendarEvent)
        {
            DisplayEventViewModel eventVM = new DisplayEventViewModel();
            eventVM.Id = calendarEvent.Id;
            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.NotificationCount = calendarEvent.Notifications.Count;
            if (calendarEvent.CalendarId != null)
            {
                eventVM.CalendarId = (int)calendarEvent.CalendarId;
                eventVM.CalendarName = calendarEvent.Calendar.Name;
            }
            return eventVM;
        }

        public static CreateEventViewModel MapToCreateEventVM(CalendarEvent calendarEvent,
            ICollection<NotificationType> notificationType,
            ICollection<Repeatable> repeatable, List<string> emails)
        {
            CreateEventViewModel eventVM = new CreateEventViewModel();

            eventVM.Id = calendarEvent.Id;
            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.Visibility = calendarEvent.Visibility;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.AllDay = (calendarEvent.AllDay == null) ? false : calendarEvent.AllDay.GetValueOrDefault();

            // Notification Settings
            List<NotificationSettingsViewModel> list = new List<NotificationSettingsViewModel>();
            foreach (NotificationType item in notificationType)
            {
                list.Add(MapToNotificationSettingsViewModel(item));
            }
            eventVM.Notifications = list;

            // Repeatable Settings
            List<RepeatableSettingsViewModel> listr = new List<RepeatableSettingsViewModel>();
            foreach (Repeatable item in repeatable)
            {
                listr.Add(MapToRepeatableViewModel(item));
            }
            eventVM.Notifications = list;

            //eventVM.Guests = emails;

            return eventVM;
        }

        public static NotificationSettingsViewModel MapToNotificationSettingsViewModel(
            NotificationType notificationType)
        {
            NotificationSettingsViewModel notificationSettingsVM = new NotificationSettingsViewModel();
            notificationSettingsVM.Id = notificationType.Id;
            notificationSettingsVM.Interval = notificationType.Interval;
            notificationSettingsVM.KindOfNotification = notificationType.KindOfNotification;
            notificationSettingsVM.RepetitionCount = notificationType.RepetitionCount;
            notificationSettingsVM.TimeBefore = notificationType.TimeBefore;
            return notificationSettingsVM;
        }

        public static List<NotificationSettingsViewModel> MapToNotificationListViewModel(
            IEnumerable<NotificationType> notifications)
        {
            List<NotificationSettingsViewModel> list = new List<NotificationSettingsViewModel>();
            foreach (NotificationType item in notifications)
            {
                list.Add(MapToNotificationSettingsViewModel(item));
            }
            return list;
        }

        public static CalendarEvent MapToEvent(CreateEventViewModel eventVM)
        {
            CalendarEvent calendarEvent = new CalendarEvent();

            calendarEvent.Id = eventVM.Id;
            calendarEvent.Name = eventVM.Name;
            calendarEvent.Text = eventVM.Text;
            calendarEvent.Place = eventVM.Place;
            calendarEvent.TimeBegin = eventVM.TimeBegin;
            calendarEvent.TimeEnd = eventVM.TimeEnd;
            calendarEvent.Visibility = eventVM.Visibility;
            calendarEvent.AllDay = eventVM.AllDay;

            return calendarEvent;
        }

        public static void MapToEvent(CalendarEvent calendarEvent, CreateEventViewModel eventVM)
        {
            calendarEvent.Name = eventVM.Name;
            calendarEvent.Text = eventVM.Text;
            calendarEvent.Place = eventVM.Place;
            calendarEvent.TimeBegin = eventVM.TimeBegin;
            calendarEvent.TimeEnd = eventVM.TimeEnd;
            calendarEvent.Visibility = eventVM.Visibility;
            calendarEvent.AllDay = eventVM.AllDay;
        }

        public static CreateEventViewModel MapToEditEventVM(CalendarEvent calendarEvent)
        {
            CreateEventViewModel eventVM = new CreateEventViewModel();

            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.TimeEnd = calendarEvent.TimeEnd;
            eventVM.Visibility = calendarEvent.Visibility;
            if (calendarEvent.AllDay != null)
                eventVM.AllDay = (bool)calendarEvent.AllDay;

            return eventVM;
        }

        public static List<NotificationType> MapToNotificationTypes(CreateEventViewModel eventVM)
        {
            List<NotificationType> list = new List<NotificationType>();
            foreach (NotificationSettingsViewModel item in eventVM.Notifications)
            {
                list.Add(MapToNotificationType(item));
            }
            return list;
        }

        public static NotificationType MapToNotificationType(NotificationSettingsViewModel notificationSettingsVM)
        {
            NotificationType notificationType = new NotificationType();
            notificationType.Interval = notificationSettingsVM.Interval;
            notificationType.KindOfNotification = notificationSettingsVM.KindOfNotification;
            notificationType.RepetitionCount = notificationSettingsVM.RepetitionCount;
            notificationType.TimeBefore = notificationSettingsVM.TimeBefore;
            return notificationType;
        }

        public static Repeatable MapToRepeatable(CreateEventViewModel eventVM)
        {
            Repeatable repeatable = new Repeatable();
            repeatable.Period = eventVM.repeatableSettings.Period;
            //TODO: count repetition count if end date
            repeatable.RepeatCount = eventVM.repeatableSettings.RepeatCount;
            return repeatable;
        }

        public static RepeatableSettingsViewModel MapToRepeatableViewModel(Repeatable repeatable)
        {
            RepeatableSettingsViewModel repeatableVM = new RepeatableSettingsViewModel();
            repeatableVM.Period = repeatable.Period;
            repeatableVM.IfRepeatable = true;
            repeatableVM.RepeatCount = repeatable.RepeatCount;
            repeatableVM.DayOfMonth = repeatable.DayOfMonth;
            repeatableVM.DayOfYear = repeatable.DayOfYear;
            repeatableVM.DaysOfWeek = repeatable.DaysOfWeek;
            repeatableVM.TimeOfDay = repeatable.TimeOfDay;
            return repeatableVM;
        }

        public static void MapToRepeatable(RepeatableSettingsViewModel repeatableVM, ref Repeatable repeatable)
        {
            repeatable.Period = repeatableVM.Period;
            repeatable.RepeatCount = repeatableVM.RepeatCount;
            repeatable.DayOfMonth = repeatableVM.DayOfMonth;
            repeatable.DayOfYear = repeatableVM.DayOfYear;
            repeatable.DaysOfWeek = repeatableVM.DaysOfWeek;
            repeatable.TimeOfDay = repeatableVM.TimeOfDay;
        }

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
            calendarVM.CSSColor = CalendarService.ColorToCSS(calendarVM.calendarColor);
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
