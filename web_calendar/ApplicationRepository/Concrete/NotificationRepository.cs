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
        public NotificationType GetNotificationType(int id)
        {
            Notification notification = FindFirst(x => x.Id == id);
            if (notification != null)
            {
                return (notification.NotificationTypeReference);
            }
            return null;
        }

        public List<NotificationType> GetAllEventNotificationTypes(int eventId)
        {
            List<Notification> notifications = FindAll(x => x.EventId == eventId).ToList();
            if (notifications != null)
                return notifications.Select(x => x.NotificationTypeReference).ToList();
            return null;
        }
    }
}
