using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Models;

namespace ApplicationRepository.Interface
{
    public interface INotificationRepository: IGenericRepository<Notification>
    {
        NotificationType GetNotificationType(int id);
    }
}
