using web_calendar.BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;
using System.Drawing;
using web_calendar.BL.DomainModels;
using System.Globalization;

namespace web_calendar.BL.Mappers
{
    public static class EventMapper
    {
        // ------------ Events ------------

        //map to Event from view model
        public static CalendarEvent MapToEvent(CreateEventViewModel eventVM)
        {
            CalendarEvent calendarEvent = new CalendarEvent();

            calendarEvent.Id = eventVM.Id;
            calendarEvent.CalendarId = eventVM.SelectedCalendarId;
            MapToEvent(ref calendarEvent, eventVM);

            return calendarEvent;
        }
        public static void MapToEvent(ref CalendarEvent calendarEvent, CreateEventViewModel eventVM)
        {
            calendarEvent.Name = eventVM.Name;
            calendarEvent.Text = eventVM.Text;
            calendarEvent.Place = eventVM.Place;
            string[] pattern = new string[] { "dd/MM/yyyy", "d/MM/yyyy" };
            DateTime begin;
            if (DateTime.TryParseExact(eventVM.DateBegin.ToShortDateString(), pattern, null, DateTimeStyles.None, out begin))
                begin = new DateTime(begin.Year, begin.Month, begin.Day,
                    eventVM.TimeBegin.Hour, eventVM.TimeBegin.Minute, eventVM.TimeBegin.Second);
            else
                begin = DateTime.Now;
            calendarEvent.TimeBegin = begin;
            if (eventVM.TimeEnd.HasValue)
            {
                DateTime end;
                if (DateTime.TryParseExact(eventVM.TimeEnd.Value.ToShortDateString(), pattern, null, DateTimeStyles.None, out end))
                {
                    end = new DateTime(end.Year, end.Month, end.Day,
                        eventVM.TimeEnd.Value.Hour, eventVM.TimeEnd.Value.Minute, eventVM.TimeEnd.Value.Second);
                    calendarEvent.TimeEnd = end;
                }
            }
            calendarEvent.AllDay = eventVM.AllDay;
        }

        //map to view models for view details
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
            eventVM.AllDay = calendarEvent.AllDay;
            eventVM.Id = calendarEvent.Id;
            eventVM.Name = calendarEvent.Name;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.NotificationCount = calendarEvent.Notifications.Count;
            if (calendarEvent.CalendarId != null)
            {
                eventVM.CalendarId = (int)calendarEvent.CalendarId;
                eventVM.CalendarName = calendarEvent.Calendar.Name;
            }
            return eventVM;
        }

        //map to view models for editing
        public static CreateEventViewModel MapToEditEventVM(CalendarEvent calendarEvent)
        {
            CreateEventViewModel eventVM = new CreateEventViewModel();

            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.DateBegin = calendarEvent.TimeBegin;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            if (calendarEvent.TimeEnd.HasValue)
            {
                eventVM.DateEnd = calendarEvent.TimeEnd.Value;
                eventVM.TimeEnd = calendarEvent.TimeEnd;
            }
            if (calendarEvent.AllDay != null)
                eventVM.AllDay = (bool)calendarEvent.AllDay;

            return eventVM;
        }
        public static CreateEventViewModel MapToEditEventVM(CalendarEvent calendarEvent,
            ICollection<Notification> notifications,
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
            List<NotificationViewModel> list = new List<NotificationViewModel>();
            foreach (Notification item in notifications)
            {
                list.Add(MapToNotificationViewModel(item));
            }
            eventVM.Notifications = list;

            // Repeatable Settings
            List<RepeatableSettingsViewModel> listr = new List<RepeatableSettingsViewModel>();
            foreach (Repeatable item in repeatable)
            {
                listr.Add(MapToRepeatableViewModel(item));
            }
            eventVM.Notifications = list;

            // Guests
            eventVM.Guests = new List<GuestsEmail>();
            for (int i = 0; i < emails.Count; i++)
            {
                eventVM.Guests.Add(new GuestsEmail() { Id = i, Email = emails[i] });
            }

            return eventVM;
        }

        // ------------ End of Events ------------


        // ------------ Notifications ------------

        //map to Notification
        public static Notification MapToNotification(NotificationViewModel notificationVM)
        {
            Notification notification = new Notification();
            notification.KindOfNotification = notificationVM.KindOfNotification;
            notification.TimeBefore = notificationVM.TimeBefore;
            return notification;
        }
        public static List<Notification> MapToNotifications(List<NotificationViewModel> notificationsVM)
        {
            List<Notification> notifications = new List<Notification>();
            foreach (NotificationViewModel item in notificationsVM)
            {
                notifications.Add(MapToNotification(item));
            }
            return notifications;
        }
        //map to view models
        public static List<NotificationViewModel> MapToNotificationListViewModel(
            IEnumerable<Notification> notifications)
        {
            List<NotificationViewModel> list = new List<NotificationViewModel>();
            foreach (Notification item in notifications)
            {
                list.Add(MapToNotificationViewModel(item));
            }
            return list;
        }
        public static NotificationViewModel MapToNotificationViewModel(Notification notification)
        {
            NotificationViewModel notificationVM = new NotificationViewModel();
            notificationVM.Id = notification.Id;
            notificationVM.KindOfNotification = notification.KindOfNotification;
            notificationVM.TimeBefore = notification.TimeBefore;
            return notificationVM;
        }

        // ------------ End of Notifications ------------

        // ------------ Repeatable Settings ------------

        //map to Repeatable
        public static void MapToRepeatable(RepeatableSettingsViewModel repeatableVM, ref Repeatable repeatable,
            CalendarEvent calendarEvent)
        {
            repeatable.Period = repeatableVM.Period;
            repeatable.RepeatCount = repeatableVM.RepeatCount;
            repeatable.DayOfMonth = calendarEvent.TimeBegin.Day;
            repeatable.DayOfYear = new DateTime(calendarEvent.TimeBegin.Year, calendarEvent.TimeBegin.Month,
                calendarEvent.TimeBegin.Day);
            repeatable.TimeOfDay = calendarEvent.TimeBegin.TimeOfDay;
            repeatable.DaysOfWeek = repeatableVM.DaysOfWeek;
        }
        public static Repeatable MapToRepeatable(RepeatableSettingsViewModel repeatableVM, CalendarEvent calendarEvent)
        {
            Repeatable repeatable = new Repeatable();
            repeatable.Period = repeatableVM.Period;
            repeatable.RepeatCount = repeatableVM.RepeatCount;
            repeatable.DayOfMonth = calendarEvent.TimeBegin.Day;
            repeatable.DayOfYear = new DateTime(calendarEvent.TimeBegin.Year, calendarEvent.TimeBegin.Month,
                calendarEvent.TimeBegin.Day);
            repeatable.TimeOfDay = calendarEvent.TimeBegin.TimeOfDay;
            repeatable.DaysOfWeek = repeatableVM.DaysOfWeek;
            return repeatable;
        }
        //map to view model
        public static RepeatableSettingsViewModel MapToRepeatableViewModel(Repeatable repeatable)
        {
            RepeatableSettingsViewModel repeatableVM = new RepeatableSettingsViewModel();
            repeatableVM.Period = repeatable.Period;
            repeatableVM.IfRepeatable = true;
            repeatableVM.RepeatCount = repeatable.RepeatCount;
            if (repeatable.DayOfMonth.HasValue)
                repeatableVM.DayOfMonth = repeatable.DayOfMonth.Value.ToString();
            if (repeatable.DayOfYear.HasValue)
                repeatableVM.DayOfYear = repeatable.DayOfYear.Value.Month + "/" + repeatable.DayOfYear.Value.Day;
            repeatableVM.DaysOfWeek = repeatable.DaysOfWeek;
            if (repeatable.TimeOfDay.HasValue)
                repeatableVM.TimeOfDay = repeatable.TimeOfDay.Value.Hours + ":" + repeatable.TimeOfDay.Value.Minutes;
            return repeatableVM;
        }

        // ------------ End of Repeatable Settings ------------

        // ------------ Guests ------------

        public static List<GuestsEmail> MapToGuestsViewModel(IEnumerable<Guest> guests)
        {
            List<GuestsEmail> guestList = new List<GuestsEmail>();
            foreach (Guest item in guests)
            {
                guestList.Add(new GuestsEmail { Id = item.Id, Email = item.Email });
            }
            return guestList;
        }
        public static List<Guest> MapToGuests(IEnumerable<GuestsEmail> guestList)
        {
            List<Guest> guests = new List<Guest>();
            foreach (GuestsEmail item in guestList)
            {
                guests.Add(new Guest { Email = item.Email });
            }
            return guests;
        }

        // ------------ End of Guests ------------
    }
}
