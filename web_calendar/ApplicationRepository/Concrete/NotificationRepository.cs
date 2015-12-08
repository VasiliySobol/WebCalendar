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

        public List<Notification> GetAllEventNotifications(int eventId)
        {
            return FindBy(x => x.EventId == eventId).ToList();
        }
    }
}
