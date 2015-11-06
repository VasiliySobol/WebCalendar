using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;

namespace web_calendar.DAL.Interface
{
    public interface INotificationRepository: IGenericRepository<Notification>
    {
        NotificationType GetNotificationType(int id);
        List<NotificationType> GetAllEventNotificationTypes(int eventId);
    }
}
