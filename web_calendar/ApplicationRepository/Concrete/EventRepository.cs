using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Interface;
using web_calendar.DAL.Models;

namespace web_calendar.DAL.Concrete
{
    public sealed class EventRepository: GenericRepository<web_calendarEntities, CalendarEvent>, IEventRepository
    {
        public IEnumerable<CalendarEvent> GetAllUserEvents(string userId)
        {
            return FindAll(x => x.Calendar.UserId == userId).ToList();
        }

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

        public IEnumerable<Notification> GetAllNotifications(int eventId)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == eventId);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.ToList();
            }
            return null;
        }

        public IEnumerable<NotificationType> GetAllNotificationTypes(int id)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                List<NotificationType> list = new List<NotificationType>();
                foreach (Notification item in calendarEvent.Notifications.ToList())
                {
                    list.Add(item.NotificationTypeReference);                    
                }
                return list;
            }
            return null;
        }

        public IEnumerable<NotificationType> FindAllNotifications(int id, Func<NotificationType, bool> filter)
        {
            CalendarEvent calendarEvent = FindFirst(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                List<NotificationType> list = new List<NotificationType>();
                foreach (Notification item in calendarEvent.Notifications.Where(x => 
                    filter(x.NotificationTypeReference)).ToList())
                {
                    list.Add(item.NotificationTypeReference);
                }
                return list;
            }
            return null;
        }

        public void AddNotifications(int eventId, IEnumerable<NotificationType> notifications)
        {
            CalendarEvent calendarEvent = FindById(eventId);
            if (calendarEvent != null)
                foreach (NotificationType item in notifications)
                {
                    AddOther<NotificationType>(item);
                    Notification newNotification = new Notification();
                    newNotification.NotificationType = item.Id;
                    newNotification.NotificationTypeReference = item;
                    newNotification.EventId = calendarEvent.Id;
                    newNotification.CalendarEvent = calendarEvent;
                    AddOther<Notification>(newNotification);
                }
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

        public void AddRepeatableSettings(int eventId, Repeatable repeatable)
        {
            CalendarEvent calendarEvent = FindById(eventId);
            AddOther<Repeatable>(repeatable);
            calendarEvent.Repeatables.Add(repeatable);
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

        public void AddCalendar(int eventId, int calendarId)
        {
            Calendar calendar = FindOtherById<Calendar>(calendarId);
            if (calendar != null)
            {
                CalendarEvent calendarEvent = FindById(eventId);
                calendarEvent.CalendarId = calendarId;
                calendarEvent.Calendar = calendar;
            }
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
