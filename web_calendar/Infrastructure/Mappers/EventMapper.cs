using web_calendar.BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;
using System.Drawing;
using web_calendar.BL.DomainModels;

namespace web_calendar.BL.Mappers
{
    public static class EventMapper
    {
        // -------- Events --------

        public static DetailsEventViewModel MapToDetailsEventVM(CalendarEvent calendarEvent, List<string> guests)
        {
            DetailsEventViewModel eventVM = new DetailsEventViewModel();
            eventVM.Id = calendarEvent.Id;
            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
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
            MapToEvent(ref calendarEvent, eventVM);

            return calendarEvent;
        }

        public static void MapToEvent(ref CalendarEvent calendarEvent, CreateEventViewModel eventVM)
        {
            calendarEvent.Name = eventVM.Name;
            calendarEvent.Text = eventVM.Text;
            calendarEvent.Place = eventVM.Place;
            DateTime begin = new DateTime(eventVM.DateBegin.Year, eventVM.DateBegin.Month, eventVM.DateBegin.Day, eventVM.TimeBegin.Hour, eventVM.TimeBegin.Minute, eventVM.TimeBegin.Second);
            calendarEvent.TimeBegin = begin;
            if (eventVM.TimeEnd.HasValue)
            {
                DateTime end = new DateTime(eventVM.TimeEnd.Value.Year, eventVM.TimeEnd.Value.Month, eventVM.TimeEnd.Value.Day, eventVM.TimeEnd.Value.Hour, eventVM.TimeEnd.Value.Minute, eventVM.TimeEnd.Value.Second);
                calendarEvent.TimeEnd = end;
            }
            calendarEvent.AllDay = eventVM.AllDay;
        }

        public static CreateEventViewModel MapToEditEventVM(CalendarEvent calendarEvent)
        {
            CreateEventViewModel eventVM = new CreateEventViewModel();

            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.DateBegin = calendarEvent.TimeBegin;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.DateEnd = calendarEvent.TimeEnd;
            eventVM.TimeEnd = calendarEvent.TimeEnd;
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
        
        public static RepeatableSettingsViewModel MapToRepeatableViewModel(Repeatable repeatable)
        {
            RepeatableSettingsViewModel repeatableVM = new RepeatableSettingsViewModel();
            repeatableVM.Period = repeatable.Period;
            repeatableVM.IfRepeatable = true;
            repeatableVM.RepeatCount = repeatable.RepeatCount;
            if (repeatable.MonthDay.HasValue)
                repeatableVM.DayOfMonth = repeatable.MonthDay.Value.ToString();
            if (repeatable.DayOfYear.HasValue)
                repeatableVM.DayOfYear = repeatable.DayOfYear.Value.Month + "/" + repeatable.DayOfYear.Value.Day;
            repeatableVM.DaysOfWeek = repeatable.DaysOfWeek;
            if (repeatable.TimeOfDay.HasValue)
                repeatableVM.TimeOfDay = repeatable.TimeOfDay.Value.Hours + ":" + repeatable.TimeOfDay.Value.Minutes;
            return repeatableVM;
        }

        public static void MapToRepeatable(RepeatableSettingsViewModel repeatableVM, ref Repeatable repeatable, 
            CalendarEvent calendarEvent)
        {
            repeatable.Period = repeatableVM.Period;
            repeatable.RepeatCount = repeatableVM.RepeatCount;
            repeatable.MonthDay = calendarEvent.TimeBegin.Day;
            repeatable.DayOfYear = new DateTime(calendarEvent.TimeBegin.Year, calendarEvent.TimeBegin.Month, 
                calendarEvent.TimeBegin.Day);
            repeatable.TimeOfDay = calendarEvent.TimeBegin.TimeOfDay;
            repeatable.DaysOfWeek = repeatableVM.DaysOfWeek;
        }
    }
}
