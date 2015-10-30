using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_calendar.Models;

namespace web_calendar.Mappers
{
    public static class Mapper
    {
        // -------- Events --------
        public static EventViewModel Map(Event _event, ICollection<NotificationType> notificationType,
            ICollection<Repeatable> repeatable)
        {
            EventViewModel eventVM = new EventViewModel();

            throw new NotImplementedException();

            return eventVM;
        }

        public static Event MapToEvent(EventViewModel eventVM)
        {
            Event _event = new Event();

            _event.name = eventVM.Name;
            _event.text = eventVM.Text;
            _event.place = eventVM.Place;
            _event.time_begin = eventVM.TimeBegin;
            _event.time_end = eventVM.TimeEnd;
            _event.visibility = eventVM.Visibility;
            _event.all_day = eventVM.AllDay;

            return _event;
        }

        public static NotificationType MapToNotificationType(EventViewModel eventVM)
        {
            throw new NotImplementedException();
        }

        public static Repeatable MapToRepeatable(EventViewModel eventVM)
        {
            throw new NotImplementedException();
        }

        // -------- Calendars --------
        // -------- Users --------
    }
}