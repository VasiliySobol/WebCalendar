using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Interface;
using ApplicationRepository.Models;

namespace ApplicationRepository.Concrete
{
    public sealed class EventRepository: GenericRepository<web_calendarEntities, CalendarEvent>, IEventRepository
    {
        public Notification FindFirstNotification(int id, Func<Notification, bool> filter)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.FirstOrDefault(filter);
            }
            return null;
        }

        public IEnumerable<Notification> FindAllNotifications(int id, Func<Notification, bool> filter)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.Where(filter).ToList();
            }
            return null;
        }

        public IEnumerable<Notification> GetAllNotifications(int id)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.ToList();
            }
            return null;
        }

        public bool IsRepeatable(CalendarEvent calendarEvent)
        {
            return ((calendarEvent != null) && (calendarEvent.Repeatables != null));
        }

        public Repeatable GetRepeatableSettings(int id)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (IsRepeatable(calendarEvent))
            {
                return calendarEvent.Repeatables.FirstOrDefault();
            }
            return null;
        }

        public Calendar GetCalendar(int id)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.CalendarId != null)
            {
                return calendarEvent.Calendar;
            }
            return null;
        }

        public Guest FindFirstGuest(int id, Func<Guest, bool> filter)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Guests != null)
            {
                return calendarEvent.Guests.FirstOrDefault(filter);
            }
            return null;
        }

        public IEnumerable<Guest> FindAllGuests(int id, Func<Guest, bool> filter)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Guests != null)
            {
                return calendarEvent.Guests.Where(filter).ToList();
            }
            return null;
        }

        public IEnumerable<Guest> GetAllGuests(int id)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Guests != null)
            {
                return calendarEvent.Guests.ToList();
            }
            return null;
        }
    }
}
