using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRepository.Interface
{
    public interface ICalendarRepository : IGenericRepository<Calendar>
    {
        User GetUser(int Id);
        NotificationType GetStandartNotificationType(int Id);
        CalendarEvent FindFirstEvent(int Id, Func<CalendarEvent, bool> filter);
        IEnumerable<CalendarEvent> FindAllEvents(int Id, Func<CalendarEvent, bool> filter);
    }
}
