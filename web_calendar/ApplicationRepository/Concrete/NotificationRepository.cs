using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;
using web_calendar.DAL.Interface;

namespace web_calendar.DAL.Concrete
{
    public sealed class NotificationRepository: GenericRepository<web_calendarEntities, Notification>, INotificationRepository
    {
        public Notification GetNotification(int eventId)
        {
            return FirstOrDefault(x => x.Id == eventId);
        }

        public List<Notification> GetNextNotifications(string userId, string type)
        {
            List<Notification> notificationsOfType = FindBy(x => x.CalendarEvent.Calendar.UserId == userId
                && x.KindOfNotification == type).ToList();
            List<Notification> notificationsBefoToday = notificationsOfType.
                Where(x => x.CalendarEvent.TimeBegin.AddMinutes(-x.TimeBefore.Value).CompareTo(DateTime.Now) > 0).ToList();
            List<Notification> notifications = notificationsBefoToday.
                OrderBy(x => x.CalendarEvent.TimeBegin.AddMinutes(-x.TimeBefore.Value)).ToList();
            return notifications.Take(10).ToList();
        }

        public List<Notification> GetAllEventNotifications(int eventId)
        {
            return FindBy(x => x.EventId == eventId).ToList();
        }
    }
}
