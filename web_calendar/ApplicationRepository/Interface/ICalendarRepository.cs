using web_calendar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.DAL.Interface
{
    public interface ICalendarRepository : IGenericRepository<Calendar>
    {
        IEnumerable<Calendar> GetUserCalendars(string UserId);
        string GetUserId(int Id);
        NotificationType GetStandartNotificationType(int Id);
        CalendarEvent FindFirstEvent(int Id, Func<CalendarEvent, bool> filter);
        IEnumerable<CalendarEvent> FindAllEvents(int Id, Func<CalendarEvent, bool> filter);
    }
}
