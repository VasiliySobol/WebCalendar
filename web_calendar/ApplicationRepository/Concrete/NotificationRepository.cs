using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Models;
using ApplicationRepository.Interface;

namespace ApplicationRepository.Concrete
{
    public sealed class NotificationRepository: GenericRepository<web_calendarEntities, Notification>, INotificationRepository
    {
        public Notification FindById(int id)
        {
            return FindFirst(x => x.id == id);
        }

        public NotificationType GetNotificationType(int id)
        {
            Notification n = FindFirst(x => x.id == id);
            if (n!=null)
            {
                return (n.notification_type1);
            }
            return null;
        }
    }
}
