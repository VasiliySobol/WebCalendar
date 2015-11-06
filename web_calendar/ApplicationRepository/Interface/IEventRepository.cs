﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;

namespace web_calendar.DAL.Interface
{
    public interface IEventRepository: IGenericRepository<CalendarEvent>
    {
        IEnumerable<CalendarEvent> GetAllUserEvents(string userId);
        Notification FindFirstNotification(int id, Func<Notification, bool> filter);
        IEnumerable<Notification> FindAllNotifications(int id, Func<Notification, bool> filter);
        bool IsRepeatable(CalendarEvent calendarEvent);
        Repeatable GetRepeatableSettings(int id);
        Calendar GetCalendar(int id);
        IEnumerable<Guest> FindAllGuests(int id, Func<Guest, bool> filter);
    }
}
