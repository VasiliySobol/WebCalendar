using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Interface;
using web_calendar.DAL.Models;

namespace web_calendar.DAL.Concrete
{
    public sealed class RepeatableRepository : GenericRepository<web_calendarEntities, Repeatable>, IRepeatableRepository
    {
    }
}
