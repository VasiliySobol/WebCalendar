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
        public static List<Notification> EmailQueue = new List<Notification>();

        public static Reminder GetClosest(ref List<Notification> queue)
        {
            if (queue == null || queue.Count == 0) return null;
            DateTime notificationTime = queue[0].CalendarEvent.TimeBegin.AddMinutes(-queue[0].TimeBefore.Value);
            if (notificationTime.CompareTo(DateTime.Now) < 0)
            {
                queue.RemoveAt(0);
                if (queue == null || queue.Count == 0) return null;
                notificationTime = queue[0].CalendarEvent.TimeBegin.AddMinutes(-queue[0].TimeBefore.Value);
            }
            TimeSpan span = notificationTime.Subtract(DateTime.Now);
            int timeTo = (int)span.TotalMilliseconds;
            int _eventId = queue[0].CalendarEvent.Id;
            string eventName = queue[0].CalendarEvent.Name;
            return new Reminder() { eventId = _eventId, Name = eventName, Time = timeTo };
        }

        public static void RemovePrevious(ref List<Notification> queue)
        {
            queue.RemoveAt(0);
        }

        public static void AppendQueue(ref List<Notification> queue, List<Notification> newNotifications)
        {
            queue.AddRange(newNotifications);
            queue = queue.OrderBy(x => x.CalendarEvent.TimeBegin.AddMinutes(-x.TimeBefore.Value)).ToList();
        }

        public static void AppendQueueIfNeeded(ref List<Notification> queue, List<Notification> newNotifications)
        {
            if (!(queue != null && queue.Count > 0)) return;
            foreach (Notification newNotification in newNotifications)
            {
                // check if this notification is later then now but ealier then last notification in the queue
                if (newNotification.CalendarEvent.TimeBegin.AddMinutes(-newNotification.TimeBefore.Value).CompareTo(
                    DateTime.Now) > 0
                    &&
                    newNotification.CalendarEvent.TimeBegin.AddMinutes(-newNotification.TimeBefore.Value).CompareTo(
                    queue.LastOrDefault().CalendarEvent.TimeBegin.AddMinutes(-queue.LastOrDefault().TimeBefore.Value)) < 0)
                {
                    if (!queue.Contains(newNotification))
                        queue.Add(newNotification);
                }
            }
            queue = queue.OrderBy(x => x.CalendarEvent.TimeBegin.AddMinutes(-x.TimeBefore.Value)).ToList();
        }
    }
}
