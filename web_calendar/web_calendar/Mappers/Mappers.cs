using ApplicationRepository.Models;
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
        public static EventViewModel Map(Event _event, ICollection<NotificationType> notificationType,
            ICollection<Repeatable> repeatable, List<string> emails)
        {
            EventViewModel eventVM = new EventViewModel();

            eventVM.Id = _event.id;
            eventVM.Name = _event.name;
            eventVM.Text = _event.text;
            eventVM.Place = _event.place;
            eventVM.Visibility = _event.visibility;
            eventVM.TimeBegin = _event.time_begin;
            eventVM.AllDay = _event.all_day;

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

        public static NotificationSettingsViewModel MapToNotificationSettingsViewModel(NotificationType notificationType)
        {
            NotificationSettingsViewModel notificationSettingsVM = new NotificationSettingsViewModel();
            notificationSettingsVM.Id = notificationType.id;
            notificationSettingsVM.Interval = notificationType.interval;
            notificationSettingsVM.KindOfNotification = notificationType.kind_of_notification;
            notificationSettingsVM.RepetitionCount = notificationType.repetition_count;
            notificationSettingsVM.TimeBefore = notificationType.time_before;
            return notificationSettingsVM;
        }

        public static RepeatableSettingsViewModel MapToRepeatableSettingsViewModel(Repeatable repeatable)
        {
            RepeatableSettingsViewModel repeatableSettingsVM = new RepeatableSettingsViewModel();
            repeatableSettingsVM.Id = repeatable.id;
            repeatableSettingsVM.Period = repeatable.period;
            repeatableSettingsVM.RepeatCount = repeatable.repeat_count;
            return repeatableSettingsVM;
        }

        public static Event MapToEvent(EventViewModel eventVM)
        {
            Event _event = new Event();

            _event.name = eventVM.Name;
            _event.text = eventVM.Text;
            _event.place = eventVM.Place;
            _event.time_begin = eventVM.TimeBegin;
            _event.time_end = eventVM.TimeEnd;
            _event.visibility = eventVM.Visibility;
            _event.all_day = eventVM.AllDay;

            return _event;
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
            notificationType.interval = notificationSettingsVM.Interval;
            notificationType.kind_of_notification = notificationSettingsVM.KindOfNotification;
            notificationType.repetition_count = notificationSettingsVM.RepetitionCount;
            notificationType.time_before = notificationSettingsVM.TimeBefore;
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
            repeatable.period = repeatableSettingsVM.Period;
            if (repeatableSettingsVM.RepeatCount != null)
            {
                repeatable.repeat_count = repeatableSettingsVM.RepeatCount;
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