using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Models;

namespace ApplicationRepository.Interface
{
    interface INotificationRepository: IGenericRepository<Notification>
    {
        Notification FindById(int id);
        NotificationType GetNotificationType(int id);
    }
}
