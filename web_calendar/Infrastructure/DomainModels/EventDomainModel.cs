using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Interface;

namespace web_calendar.BL.DomainModels
{
    public class EventDomainModel
    {
        
        public IEventRepository eventRepository;
        public INotificationRepository notificationRepository;
        public ICalendarRepository calendarRepository;

        public EventDomainModel(IEventRepository _eventRepository, INotificationRepository _notificationRepository,
            ICalendarRepository _calendarRepository)  
        {
            this.eventRepository = _eventRepository;
            this.notificationRepository = _notificationRepository;
            this.calendarRepository = _calendarRepository;
        }


    }
}
