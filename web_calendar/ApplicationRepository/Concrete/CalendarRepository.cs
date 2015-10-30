﻿using ApplicationRepository.Interface;
using ApplicationRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRepository.Concrete
{
    public sealed class CalendarRepository : GenericRepository<web_calendarEntities, Calendar>, ICalendarRepository
    {
        public User GetUser(int Id)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.UserId != null))
            {
                return calendar.User;
            }
            return null;
        }
        public NotificationType GetStandartNotificationType(int Id)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.NotificationTypeId != null))
                return calendar.NotificationType;
            return null;
        }
        public CalendarEvent FindFirstEvent(int Id, Func<CalendarEvent, bool> filter)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.CalendarEvents != null))
                return calendar.CalendarEventsCollection.FirstOrDefault(filter);
            return null;
        }
        public IEnumerable<CalendarEvent> FindAllEvents(int Id, Func<CalendarEvent, bool> filter)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.CalendarEvents != null))
                return calendar.CalendarEventsCollection.Where(filter).ToList();
            return null; 
        }
        public IEnumerable<CalendarEvent> GetAllEvents(int Id)
        {
            Calendar calendar = FindFirst(x => x.Id == Id);
            if ((calendar != null) && (calendar.CalendarEvents != null))
                return calendar.CalendarEventsCollection.ToList();
            return null;
        }
    }
}
