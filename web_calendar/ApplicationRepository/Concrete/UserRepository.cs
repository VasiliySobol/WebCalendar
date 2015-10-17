using ApplicationRepository.Interface;
using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRepository.Concrete
{
    public sealed class UserRepository : GenericRepository<web_calendarEntities, User>, IUserRepository
    {
        public Calendar FindFirstCalendar(int Id, Func<Calendar, bool> filter)
        {
            User user = FindFirst(x => x.id == Id);
            if ((user != null) && (user.calendars != null))
                return user.calendars_collection.First(filter);
            return null;
        }
        public IEnumerable<Calendar> FindAllCalendars(int Id, Func<Calendar, bool> filter)
        {
            if (FindFirst(x => x.id == Id) != null)
            {
                return FindFirst(x => x.id == Id).calendars_collection.Where(filter);
            }
            return null;
        }
        public Settings GetSettings(int Id)
        {
            User user = FindFirst(x => x.id == Id);
            if ((user != null) && (user.settings != null))
            {
                return user.settings_reference;
            }
            return null;
        }
    }
}
