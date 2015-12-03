using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using web_calendar.BL.Mappers;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Interface;
using web_calendar.DAL.Models;

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

        public List<DisplayEventViewModel> GetFollowingEvents(int? id, string userId)
        {
            List<CalendarEvent> events;
            if (id == null)
                events = eventRepository.GetAllUserEvents(userId).Where(x => x.TimeBegin.CompareTo(DateTime.Now) >= 0).
                    OrderBy(x => x.TimeBegin).ToList();
            else
                events = eventRepository.GetAllUserEvents(userId).Where(x => x.CalendarId == id &&
                    x.TimeBegin.CompareTo(DateTime.Now) >= 0).ToList();
            List<DisplayEventViewModel> list = new List<DisplayEventViewModel>();
            foreach (CalendarEvent item in events)
            {
                list.Add(EventMapper.MapToDisplayEventVM(item));
            }
            return list;
        }

        public DetailsEventViewModel GetEventDetails(CalendarEvent calendarEvent)
        {
            DetailsEventViewModel eventViewModel = EventMapper.MapToDetailsEventVM(calendarEvent,
                calendarEvent.Guests.Select(x => x.Email).ToList());
            if (calendarEvent.Repeatables != null)
                if (calendarEvent.Repeatables.Count > 0)
                    eventViewModel.repeatableSettings =
                        EventMapper.MapToRepeatableViewModel(calendarEvent.Repeatables.First());
            if (calendarEvent.Notifications != null)
                eventViewModel.Notifications = EventMapper.MapToNotificationListViewModel(
                    eventRepository.GetAllNotifications(calendarEvent.Id));
            return eventViewModel;
        }

        public void PopulateCalendarSelectList(ref CreateEventViewModel eventViewModel, string userId)
        {
            eventViewModel.CalendarItems = new SelectList(
                calendarRepository.GetUserCalendars(userId).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text",
                calendarRepository.GetUserCalendars(userId).First().Id);
            eventViewModel.CalendarItems.First().Selected = true;
            eventViewModel.SelectedCalendarId = calendarRepository.GetUserCalendars(userId).First().Id;
        }

        public void PopulateCalendarSelectList(ref CreateEventViewModel eventViewModel, string userId, int calendarId)
        {
            eventViewModel.CalendarItems = new SelectList(
                calendarRepository.GetUserCalendars(userId).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text",
                calendarId.ToString());
            eventViewModel.CalendarItems.First().Selected = true;
            eventViewModel.SelectedCalendarId = calendarId;            
        }

        public CreateEventViewModel GetEditEventViewModel(CalendarEvent calendarEvent)
        {
            CreateEventViewModel eventViewModel = EventMapper.MapToEditEventVM(calendarEvent);
            if (calendarEvent.Repeatables != null)
                if (calendarEvent.Repeatables.Count > 0)
                    eventViewModel.repeatableSettings =
                        EventMapper.MapToRepeatableViewModel(calendarEvent.Repeatables.First());
            if (calendarEvent.Notifications != null)
                eventViewModel.Notifications = EventMapper.MapToNotificationListViewModel(
                    eventRepository.GetAllNotifications(calendarEvent.Id));
            return eventViewModel;
        }

        public void DeleteEvent(int id)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            eventRepository.Delete(calendarEvent);
            eventRepository.SaveChanges();
        }

        public void CreateEvent(CreateEventViewModel eventViewModel, int calendarId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                CalendarEvent calendarEvent = EventMapper.MapToEvent(eventViewModel);
                eventRepository.Add(calendarEvent);
                eventRepository.AddCalendar(calendarEvent.Id, calendarId);
                eventRepository.SaveChanges();

                if (eventViewModel.Notifications != null && eventViewModel.Notifications.Count > 0)
                {
                    List<Notification> notifications = 
                        EventMapper.MapToNotifications(eventViewModel.Notifications);
                    eventRepository.AddNotifications(calendarEvent.Id, notifications);
                }
                eventRepository.SaveChanges();

                if (eventViewModel.Guests != null && eventViewModel.Guests.Count > 0)
                    eventRepository.AddGuests(calendarEvent.Id,
                        eventViewModel.Guests.Select(x => x.Email).ToList());
                eventRepository.SaveChanges();

                if (eventViewModel.repeatableSettings != null)
                    if (eventViewModel.repeatableSettings.IfRepeatable)
                    {
                        Repeatable repeatable = new Repeatable();
                        EventMapper.MapToRepeatable(eventViewModel.repeatableSettings, ref repeatable, calendarEvent);
                        repeatable.EventId = calendarEvent.Id;
                        repeatable.CalendarEvent = calendarEvent;
                        eventRepository.AddRepeatableSettings(calendarEvent.Id, repeatable);

                        int[] steps = new int[0];
                        int nextStep = 0;
                        if (repeatable.Period == "week")
                        {
                            steps = new int[repeatable.DaysOfWeek.Length];
                            //day = 1 if monday and so on
                            int day = ((int)calendarEvent.TimeBegin.DayOfWeek == 0) ? 7 : (int)calendarEvent.TimeBegin.DayOfWeek;
                            int stepIndex = 0, step = 1; char dayChar = (char)((int)'1' + day);
                            do
                            {
                                if (repeatable.DaysOfWeek.Contains(dayChar))
                                {
                                    steps[stepIndex++] = step;
                                    step = 1;
                                }
                                else
                                {
                                    step++;
                                }
                                dayChar++;
                                if (dayChar == '8') dayChar = '1';
                            } while (stepIndex < repeatable.DaysOfWeek.Length);
                        }
                        for (int i = 0; i < repeatable.RepeatCount; i++)
                        {
                            CalendarEvent revent = new CalendarEvent();
                            EventMapper.MapToEvent(ref revent, eventViewModel);
                            revent.ParentEvent = calendarEvent.Id;
                            switch (repeatable.Period)
                            {
                                case "day":
                                    revent.TimeBegin = calendarEvent.TimeBegin.AddDays(i + 1);
                                    break;
                                case "week":
                                    nextStep += steps[i % steps.Length];
                                    revent.TimeBegin = calendarEvent.TimeBegin.AddDays(nextStep);
                                    break;
                                case "month":
                                    revent.TimeBegin = calendarEvent.TimeBegin.AddMonths(i + 1);
                                    break;
                                case "year":
                                    revent.TimeBegin = calendarEvent.TimeBegin.AddYears(i + 1);
                                    break;
                            }
                            eventRepository.Add(revent);
                            eventRepository.AddCalendar(revent.Id, calendarId);
                            eventRepository.SaveChanges();
                            if (calendarEvent.Notifications != null && calendarEvent.Notifications.Count > 0)
                            {
                                List<Notification> notifications = 
                                    EventMapper.MapToNotifications(eventViewModel.Notifications);
                                eventRepository.AddNotifications(revent.Id, notifications);
                            }
                            if (calendarEvent.Guests != null && calendarEvent.Guests.Count > 0)
                                eventRepository.AddGuests(revent.Id,
                                    eventViewModel.Guests.Select(x => x.Email).ToList());
                        }
                    }
                eventRepository.SaveChanges();
                scope.Complete();
            }
        }

        public void EditEvent(int id, CreateEventViewModel eventViewModel, int calendarId)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            EventMapper.MapToEvent(ref calendarEvent, eventViewModel);
            if (calendarId != eventRepository.GetCalendar(id).Id)
            {
                Calendar calendar = eventRepository.FindOtherById<Calendar>(calendarId);
                if (calendar != null)
                {
                    calendarEvent.Calendar.CalendarEvents1.Remove(calendarEvent);
                    calendarEvent.CalendarId = calendarId;
                    calendarEvent.Calendar = calendar;
                }
            }
            eventRepository.Modify(calendarEvent);
            eventRepository.SaveChanges();
            //if (eventViewModel.repeatableSettings.IfRepeatable)
            //{
            //    Repeatable repeatable = eventRepository.GetRepeatableSettings(id);
            //    if (repeatable != null)
            //    {
            //        eventRepository.DeleteAllChildrenEvents(id);
            //        EventMapper.MapToRepeatable(eventViewModel.repeatableSettings, ref repeatable, calendarEvent);                                 
            //        //add logic
            //    }
            //    else
            //    {
            //        repeatable = new Repeatable();
            //        EventMapper.MapToRepeatable(eventViewModel.repeatableSettings, ref repeatable, calendarEvent);
            //        repeatable.EventId = calendarEvent.Id;
            //        repeatable.CalendarEvent = calendarEvent;
            //        //add logic
            //        eventRepository.AddRepeatableSettings(id, repeatable);
            //    }
            //}
            eventRepository.SaveChanges();
        }
    }
}
