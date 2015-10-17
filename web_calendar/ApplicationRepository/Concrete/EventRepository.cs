using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Interface;
using ApplicationRepository.Models;

namespace ApplicationRepository.Concrete
{
    public sealed class EventRepository: GenericRepository<web_calendarEntities, Event>, IEventRepository
    {
        public Notification FindFirstNotification(int id, Func<Notification, bool> filter)
        {
            Event myEvent = FindFirst(x => x.id == id);
            if (myEvent!=null && myEvent.notifications != null)
            {
                return myEvent.notifications.FirstOrDefault(filter);
            }
            return null;
        }

        public IEnumerable<Notification> FindAllNotifications(int id, Func<Notification, bool> filter)
        {
            Event myEvent = FindFirst(x => x.id == id);
            if (myEvent != null && myEvent.notifications != null)
            {
                return myEvent.notifications.Where(filter);
            }
            return null;
        }

        public bool DoesRepeatable(Event _event)
        {
            if (_event != null && _event.repeatables != null)
            {
                return true;
            }
            return false;
        }

        public Repeatable GetRepeatableSettings(int id)
        {
            Event myEvent = FindFirst(x => x.id == id);
            if (DoesRepeatable(myEvent))
            {
                return myEvent.repeatables.FirstOrDefault();
            }
            return null;
        }

        public Calendar GetCalendar(int id)
        {
            Event myEvent = FindFirst(x => x.id == id);
            if (myEvent != null && myEvent.calendar_id != null)
            {
                return myEvent.calendar;
            }
            return null;
        }

        public IEnumerable<Guest> FindAllGuests(int id, Func<Guest, bool> filter)
        {
            Event myEvent = FindFirst(x => x.id == id);
            if (myEvent != null && myEvent.guests != null)
            {
                return myEvent.guests.Where(filter);
            }
            return null;
        }
    }
}
