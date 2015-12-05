using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;

namespace web_calendar.DAL.Interface
{
    public interface IEventRepository: IGenericRepository<CalendarEvent>
    {
        IEnumerable<CalendarEvent> GetAllUserEvents(string userId);
        Notification FindFirstNotification(int id, Func<Notification, bool> filter);
        IEnumerable<Notification> FindAllNotifications(int id, Func<Notification, bool> filter);
        IEnumerable<Notification> GetAllNotifications(int eventId);
        void AddNotifications(int eventId, IEnumerable<Notification> notifications);
        void DeleteNotifications(int eventId, IEnumerable<Notification> notifications);
        bool IsRepeatable(CalendarEvent calendarEvent);
        Repeatable GetRepeatableSettings(int id);
        void AddRepeatableSettings(int eventId, Repeatable repeatable);
        Calendar GetCalendar(int id);
        void AddCalendar(int eventId, int calendarId);
        void AddGuests(int eventId, List<string> emails);
        IEnumerable<Guest> FindAllGuests(int id, Func<Guest, bool> filter);
        void DeleteAllChildrenEvents(int parentId);
    }
}
