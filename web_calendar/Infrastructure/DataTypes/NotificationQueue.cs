using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.UserMails;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Models;

namespace web_calendar.BL.DataTypes
{
    public static class NotificationQueue
    {
        public static List<Notification> Queue = new List<Notification>();
        public static List<EmailReminder> EmailQueue = new List<EmailReminder>();

        public static Reminder GetClosest()
        {
            if (Queue == null || Queue.Count == 0) return null;
            DateTime notificationTime = Queue[0].CalendarEvent.TimeBegin.AddMinutes(-Queue[0].TimeBefore.Value);
            if (notificationTime.CompareTo(DateTime.Now) < 0)
            {
                Queue.RemoveAt(0);
                if (Queue == null || Queue.Count == 0) return null;
                notificationTime = Queue[0].CalendarEvent.TimeBegin.AddMinutes(-Queue[0].TimeBefore.Value);
            }
            TimeSpan span = notificationTime.Subtract(DateTime.Now);
            int timeTo = (int)span.TotalMilliseconds;
            int _eventId = Queue[0].CalendarEvent.Id;
            string eventName = Queue[0].CalendarEvent.Name;
            return new Reminder() { Name = eventName, Time = timeTo };
        }

        public static void AppendQueue(List<Notification> newNotifications)
        {
            Queue.AddRange(newNotifications);
            Queue = Queue.OrderBy(x => x.CalendarEvent.TimeBegin.AddMinutes(-x.TimeBefore.Value)).ToList();
        }

        public static void AppendQueueIfNeeded(List<Notification> newNotifications)
        {
            if (!(Queue != null && Queue.Count > 0)) return;
            foreach (Notification newNotification in newNotifications)
            {
                // check if this notification is later then now but ealier then last notification in the queue
                if (newNotification.CalendarEvent.TimeBegin.AddMinutes(-newNotification.TimeBefore.Value).CompareTo(
                    DateTime.Now) > 0
                    &&
                    newNotification.CalendarEvent.TimeBegin.AddMinutes(-newNotification.TimeBefore.Value).CompareTo(
                    Queue.LastOrDefault().CalendarEvent.TimeBegin.AddMinutes(-Queue.LastOrDefault().TimeBefore.Value)) < 0)
                {
                    if (!Queue.Contains(newNotification))
                        Queue.Add(newNotification);
                }
            }
            Queue = Queue.OrderBy(x => x.CalendarEvent.TimeBegin.AddMinutes(-x.TimeBefore.Value)).ToList();
        }

        public static EmailReminder GetClosestEmailReminder()
        {
            if (EmailQueue == null || EmailQueue.Count == 0) return null;
            return EmailQueue[0];
        }

        public static void RemovePrevious()
        {
            EmailQueue.RemoveAt(0);
        }

        public static void AppendEmailQueue(List<Notification> newNotifications, string _userId)
        {
            foreach (Notification item in newNotifications)
            {
                TimeSpan span = item.CalendarEvent.TimeBegin.AddMinutes(-item.TimeBefore.Value).Subtract(DateTime.Now);
                int timeTo = (int)span.TotalMilliseconds;
                EmailQueue.Add(new EmailReminder() { eventId = item.EventId.Value, EventName = item.CalendarEvent.Name, 
                    EventTime = item.CalendarEvent.TimeBegin, EventText = item.CalendarEvent.Text, 
                    NotificationTime = item.CalendarEvent.TimeBegin.AddMinutes(-item.TimeBefore.Value), 
                    Time = timeTo, userId = _userId });
            }
            EmailQueue = EmailQueue.OrderBy(x => x.NotificationTime).ToList();
        }

        public static void AppendEmailQueueIfNeeded(List<Notification> newNotifications, string _userId)
        {
            foreach (Notification item in newNotifications)
            {
                TimeSpan span = item.CalendarEvent.TimeBegin.AddMinutes(-item.TimeBefore.Value).Subtract(DateTime.Now);
                int timeTo = (int)span.TotalMilliseconds;
                EmailReminder reminder = new EmailReminder()
                {
                    eventId = item.EventId.Value,
                    EventName = item.CalendarEvent.Name,
                    EventTime = item.CalendarEvent.TimeBegin,
                    EventText = item.CalendarEvent.Text,
                    NotificationTime = item.CalendarEvent.TimeBegin.AddMinutes(-item.TimeBefore.Value),
                    Time = timeTo,
                    userId = _userId,
                };                    
                if (!EmailQueue.Contains(reminder))
                    EmailQueue.Add(reminder);
            }
            EmailQueue = EmailQueue.OrderBy(x => x.NotificationTime).ToList();
        }
    }
}
