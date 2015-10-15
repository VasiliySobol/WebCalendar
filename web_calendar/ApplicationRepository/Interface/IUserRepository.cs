using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRepository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetById(int Id);
        Calendar FindFirstCalendar(int Id, Func<Calendar, bool> filter);
        IEnumerable<Calendar> FindAllCalendars(int Id, Func<Calendar, bool> filter);
        Settings GetSettings(int Id);
    }
}
