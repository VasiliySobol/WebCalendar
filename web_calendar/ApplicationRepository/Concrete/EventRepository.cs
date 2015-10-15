using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Interface;
using ApplicationRepository.Models;

namespace ApplicationRepository.Concrete
{
    /*public sealed class EventRepository: GenericRepository<web_calendarEntities, Event>, IEventRepository
    {
        public Event FindById(int id)
        {
            return FindFirst(x => x.id == id);
        }

        /*public Notification FindFirstNotification(int id, Func<Notification, bool> filter)
        {
            //Notification n = FindFirst(x => x.id == id);

        }
    }*/
}
