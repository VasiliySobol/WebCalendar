using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationRepository.Models;

namespace ApplicationRepository.Interface
{
    interface IEventRepository: IGenericRepository<CalendarEvent>
    {
        Notification FindFirstNotification(int id, Func<Notification, bool> filter);
        IEnumerable<Notification> FindAllNotifications(int id, Func<Notification, bool> filter);
        bool IsRepeatable(CalendarEvent calendarEvent);
        Repeatable GetRepeatableSettings(int id);
        Calendar GetCalendar(int id);
        IEnumerable<Guest> FindAllGuests(int id, Func<Guest, bool> filter);
    }
}
