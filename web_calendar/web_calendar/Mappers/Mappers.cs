﻿using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_calendar.Models;

namespace web_calendar.Mappers
{
    public static class Mapper
    {
        // -------- Events --------
        public static EventViewModel Map(CalendarEvent calendarEvent, 
            ICollection<NotificationType> notificationType,
            ICollection<Repeatable> repeatable, List<string> emails)
        {
            EventViewModel eventVM = new EventViewModel();

            eventVM.Id = calendarEvent.Id;
            eventVM.Name = calendarEvent.Name;
            eventVM.Text = calendarEvent.Text;
            eventVM.Place = calendarEvent.Place;
            eventVM.Visibility = calendarEvent.Visibility;
            eventVM.TimeBegin = calendarEvent.TimeBegin;
            eventVM.AllDay = calendarEvent.AllDay;

            // Notification Settings
            List<NotificationSettingsViewModel> list = new List<NotificationSettingsViewModel>();
            foreach (NotificationType item in notificationType)
            {
                list.Add(MapToNotificationSettingsViewModel(item));
            }
            eventVM.notificationSettings = list;

            // Repeatable Settings
            List<RepeatableSettingsViewModel> listr = new List<RepeatableSettingsViewModel>();
            foreach (Repeatable item in repeatable)
            {
                listr.Add(MapToRepeatableSettingsViewModel(item));
            }
            eventVM.notificationSettings = list;

            eventVM.GuestsEmails = emails;

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

        public static RepeatableSettingsViewModel MapToRepeatableSettingsViewModel(Repeatable repeatable)
        {
            RepeatableSettingsViewModel repeatableSettingsVM = new RepeatableSettingsViewModel();
            repeatableSettingsVM.Id = repeatable.Id;
            repeatableSettingsVM.Period = repeatable.Period;
            repeatableSettingsVM.RepeatCount = repeatable.RepeatCount;
            return repeatableSettingsVM;
        }

        public static CalendarEvent MapToEvent(EventViewModel eventVM)
        {
            CalendarEvent calendarEvent = new CalendarEvent();

            calendarEvent.Name = eventVM.Name;
            calendarEvent.Text = eventVM.Text;
            calendarEvent.Place = eventVM.Place;
            calendarEvent.TimeBegin = eventVM.TimeBegin;
            calendarEvent.TimeEnd = eventVM.TimeEnd;
            calendarEvent.Visibility = eventVM.Visibility;
            calendarEvent.AllDay = eventVM.AllDay;

            return calendarEvent;
        }

        public static ICollection<NotificationType> MapToNotificationTypes(EventViewModel eventVM)
        {
            List<NotificationType> list = new List<NotificationType>();
            foreach (NotificationSettingsViewModel item in eventVM.notificationSettings)
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

        public static ICollection<Repeatable> MapToRepeatables(EventViewModel eventVM)
        {
            List<Repeatable> list = new List<Repeatable>();
            foreach (RepeatableSettingsViewModel item in eventVM.repeatableSettings)
            {
                list.Add(MapToRepeatable(item));
            }
            return list;
        }

        public static Repeatable MapToRepeatable(RepeatableSettingsViewModel repeatableSettingsVM)
        {
            Repeatable repeatable = new Repeatable();
            repeatable.Period = repeatableSettingsVM.Period;
            if (repeatableSettingsVM.RepeatCount != null)
            {
                repeatable.RepeatCount = repeatableSettingsVM.RepeatCount;
            }
            else
            {
                //TODO: calculate repeat count using date
                throw new NotImplementedException();
            }
            return repeatable;
        }

        // -------- Calendars --------
        // -------- Users --------
    }
}