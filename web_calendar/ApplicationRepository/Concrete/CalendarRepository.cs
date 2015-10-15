using ApplicationRepository.Interface;
using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRepository.Concrete
{
    public sealed class CalendarRepository : GenericRepository<web_calendarEntities, Calendar>, ICalendarRepository
    {
        public Calendar GetById(int Id)
        {
            return FindFirst(x => x.id == Id);
        }
        public User GetUser(int Id)
        {
            Calendar calendar = FindFirst(x => x.id == Id);
            if ((calendar != null) && (calendar.user_id != null))
            {
                return calendar.user;
            }
            return null;
        }
        public NotificationType GetStandartNotificationType(int Id)
        {
            Calendar calendar = FindFirst(x => x.id == Id);
            if ((calendar != null) && (calendar.notification_type_id != null))
                return calendar.notificationType;
            return null;
        }
        public Event FindFirstEvent(int Id, Func<Event, bool> filter)
        {
            Calendar calendar = FindFirst(x => x.id == Id);
            if ((calendar != null) && (calendar.events != null))
                return calendar.events_collection.FirstOrDefault(filter);
            return null;
        }
        public IEnumerable<Event> FindAllEvents(int Id, Func<Event, bool> filter)
        {
            Calendar calendar = FindFirst(x => x.id == Id);
            if ((calendar != null) && (calendar.events != null))
                return calendar.events_collection.Where(filter);
            return null; 
        }
    }
}
