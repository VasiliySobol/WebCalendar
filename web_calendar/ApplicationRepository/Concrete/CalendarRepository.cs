using web_calendar.DAL.Interface;
using web_calendar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.DAL.Concrete
{
    public sealed class CalendarRepository : GenericRepository<web_calendarEntities, Calendar>, ICalendarRepository
    {
        public IEnumerable<Calendar> GetUserCalendars(string UserId)
        {
            return FindAll(x => x.UserId == UserId).ToList();
        }

        public string GetUserId(int Id)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if (calendar != null)
            {
                return calendar.UserId;
            }
            return null;
        }

        public CalendarEvent FindFirstEvent(int Id, Func<CalendarEvent, bool> filter)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.CalendarEvents != null))
                return calendar.CalendarEvents1.FirstOrDefault(filter);
            return null;
        }

        public IEnumerable<CalendarEvent> FindAllEvents(int Id, Func<CalendarEvent, bool> filter)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.CalendarEvents != null))
                return calendar.CalendarEvents1.Where(filter).ToList();
            return null; 
        }

        public IEnumerable<CalendarEvent> GetAllEvents(int Id)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.CalendarEvents != null))
                return calendar.CalendarEvents1.ToList();
            return null;
        }
    }
}
