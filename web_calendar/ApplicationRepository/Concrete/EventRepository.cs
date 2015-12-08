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
            return FindBy(x => x.Calendar.UserId == userId).ToList();
        }

        public Notification FindFirstNotification(int id, Func<Notification, bool> filter)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.FirstOrDefault(filter);
            }
            return null;
        }

        public IEnumerable<Notification> FindAllNotifications(int id, Func<Notification, bool> filter)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.Where(filter).ToList();
            }
            return null;
        }

        public IEnumerable<Notification> GetAllNotifications(int eventId)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == eventId);
            if (calendarEvent != null && calendarEvent.Notifications != null)
            {
                return calendarEvent.Notifications.ToList();
            }
            return null;
        }

        public void AddNotifications(int eventId, IEnumerable<Notification> notifications)
        {
            CalendarEvent calendarEvent = FindById(eventId);
            if (calendarEvent != null)
                foreach (Notification item in notifications)
                {
                    item.EventId = calendarEvent.Id;
                    item.CalendarEvent = calendarEvent;
                    AddOther<Notification>(item);
                }
        }
        public void DeleteNotifications(int eventId, IEnumerable<Notification> notifications)
        {
            CalendarEvent calendarEvent = FindById(eventId);
            if (calendarEvent != null)
                foreach (Notification item in notifications)
                {
                    calendarEvent.Notifications.Remove(item);
                    Delete(item);
                }
        }

        public bool IsRepeatable(CalendarEvent calendarEvent)
        {
            return ((calendarEvent != null) && (calendarEvent.Repeatables != null));
        }

        public Repeatable GetRepeatableSettings(int id)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
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
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
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

        public void AddGuests(int eventId, List<string> emails)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == eventId);
            if (calendarEvent != null)
                foreach (string email in emails)
                {
                    Guest guest = new Guest();
                    guest.Email = email;
                    guest.EventId = eventId;
                    guest.CalendarEvent = calendarEvent;
                    AddOther<Guest>(guest);
                    calendarEvent.Guests.Add(guest);
                }
        }
        public void DeleteGuests(int eventId, List<string> guests)
        {
            CalendarEvent calendarEvent = FindById(eventId);
            if (calendarEvent != null)
                foreach (string item in guests)
                {
                    Guest guest = FindFirstGuest(eventId, x => x.Email == item);
                    calendarEvent.Guests.Remove(guest);
                    Delete(guest);
                }
        }

        public Guest FindFirstGuest(int id, Func<Guest, bool> filter)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Guests != null)
            {
                return calendarEvent.Guests.FirstOrDefault(filter);
            }
            return null;
        }

        public IEnumerable<Guest> FindAllGuests(int id, Func<Guest, bool> filter)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Guests != null)
            {
                return calendarEvent.Guests.Where(filter).ToList();
            }
            return null;
        }

        public IEnumerable<Guest> GetAllGuests(int id)
        {
            CalendarEvent calendarEvent = FirstOrDefault(x => x.Id == id);
            if (calendarEvent != null && calendarEvent.Guests != null)
            {
                return calendarEvent.Guests.ToList();
            }
            return null;
        }

        public void DeleteAllChildrenEvents(int parentId)
        {
            foreach (CalendarEvent item in FindBy(x => x.ParentEvent == parentId).ToList())
            {
                Delete(item);
            }
        }
    }
}
