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
            User user = FindFirst(x => x.Id == Id);
            if ((user != null) && (user.CalendarsCollection != null))
                return user.CalendarsCollection.First(filter);
            return null;
        }
        public IEnumerable<Calendar> FindAllCalendars(int Id, Func<Calendar, bool> filter)
        {
            User user = FindFirst(x => x.Id == Id);
            if (user != null)
            {
                return user.CalendarsCollection.Where(filter).ToList();
            }
            return null;
        }
        public UserSetting GetSettings(int Id)
        {
            User user = FindFirst(x => x.Id == Id);
            if ((user != null) && (user.UserSettingsId != null))
            {
                return user.UserSetting;
            }
            return null;
        }
    }
}
