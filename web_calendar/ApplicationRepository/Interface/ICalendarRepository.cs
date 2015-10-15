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
        Calendar GetById(int Id);
        User GetUser(int Id);
        NotificationType GetStandartNotificationType(int Id);
        Event FindFirstEvent(int Id, Func<Event, bool> filter);
        IEnumerable<Event> FindAllEvents(int Id, Func<Event, bool> filter);
    }
}
