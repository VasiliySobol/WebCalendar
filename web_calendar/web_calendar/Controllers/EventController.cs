using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using web_calendar.DAL.Models;
using web_calendar.Models;
using web_calendar.DAL.Interface;
using Newtonsoft.Json;
using System.Web.Services;
using web_calendar.BL.Mappers;
using web_calendar.BL.ViewModels;
using web_calendar.BL.ServerValidation;
using web_calendar.Handlers;

namespace web_calendar.Controllers
{
    [Authorize]
    //[CustomHandleErrorAttribute]
    public class EventController : Controller
    {
        private IEventRepository eventRepository;
        private INotificationRepository notificationRepository;
        private ICalendarRepository calendarRepository;

        public EventController(IEventRepository _eventRepository, INotificationRepository _notificationRepository,
            ICalendarRepository _calendarRepository)  
        {
            this.eventRepository = _eventRepository;
            this.notificationRepository = _notificationRepository;
            this.calendarRepository = _calendarRepository;
        }

        // GET: Event/Schedule/2
        public ActionResult Schedule(int? id)
        {
            string userId = User.Identity.GetUserId();
            List<CalendarEvent> events;
            if (id == null)
                events = eventRepository.GetAllUserEvents(userId).ToList();
            else
                events = eventRepository.GetAllUserEvents(userId).Where(x => x.CalendarId == id).ToList();
            List<DisplayEventViewModel> list = new List<DisplayEventViewModel>();
            foreach (CalendarEvent item in events)
            {
                list.Add(EventMapper.MapToDisplayEventVM(item));
            }
            return View(list);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            DetailsEventViewModel eventViewModel = EventMapper.MapToDetailsEventVM(calendarEvent, 
                calendarEvent.Guests.Select(x => x.Email).ToList());
            if (calendarEvent.Repeatables != null)
                if (calendarEvent.Repeatables.Count > 0)
                    eventViewModel.repeatableSettings =
                        EventMapper.MapToRepeatableViewModel(calendarEvent.Repeatables.First());
            if (calendarEvent.Notifications != null)
                eventViewModel.Notifications = EventMapper.MapToNotificationListViewModel(
                    eventRepository.GetAllNotificationTypes(calendarEvent.Id));
            return View(eventViewModel);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            CreateEventViewModel eventViewModel = new CreateEventViewModel();
            eventViewModel.CalendarItems = new SelectList(
                calendarRepository.GetUserCalendars(userId).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text",
                calendarRepository.GetUserCalendars(userId).First().Id);
            eventViewModel.CalendarItems.First().Selected = true;
            eventViewModel.SelectedCalendarId = calendarRepository.GetUserCalendars(userId).First().Id;
            return View(eventViewModel);
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEventViewModel eventViewModel)
        {
            string userId = User.Identity.GetUserId();
            int calendarId = eventViewModel.SelectedCalendarId;
            if (ModelState.IsValid)
            {
                switch (EventValidator.Validate(eventViewModel))
                {
                    case 0:
                        if (eventRepository.FindOtherById<Calendar>(calendarId) != null)
                        {
                            CalendarEvent calendarEvent = EventMapper.MapToEvent(eventViewModel);
                            eventRepository.Add(calendarEvent);
                            eventRepository.AddCalendar(calendarEvent.Id, calendarId);
                            eventRepository.SaveChanges();

                            if (eventViewModel.Notifications != null)
                                if (eventViewModel.Notifications.Count > 0)
                                {
                                    List<NotificationType> notifications = EventMapper.MapToNotificationTypes(eventViewModel);
                                    eventRepository.AddNotifications(calendarEvent.Id, notifications);
                                }
                            eventRepository.SaveChanges();

                            if (eventViewModel.Guests != null)
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
                                    for (int i = 0; i < repeatable.RepeatCount; i++)
                                    {
                                        CalendarEvent revent = new CalendarEvent();
                                        EventMapper.MapToEvent(ref revent, eventViewModel);
                                        revent.ParentEvent = calendarEvent.Id;
                                        eventRepository.Add(revent);
                                        eventRepository.AddCalendar(revent.Id, calendarId);
                                        eventRepository.SaveChanges();
                                        if (calendarEvent.Notifications.Count > 0)
                                        {
                                            List<NotificationType> notifications = EventMapper.MapToNotificationTypes(eventViewModel);
                                            eventRepository.AddNotifications(revent.Id, notifications);
                                        }
                                        if (calendarEvent.Guests != null)
                                            eventRepository.AddGuests(revent.Id,
                                                eventViewModel.Guests.Select(x => x.Email).ToList());
                                    }
                                }
                            eventRepository.SaveChanges();

                            return RedirectToAction("Schedule");
                        }
                        break;
                    case 1:
                        ModelState.AddModelError("Name", "Name is required.");
                        break;
                    case 2:
                        ModelState.AddModelError("Begin time", "Begin time is required.");
                        break;
                }
            }
            eventViewModel.CalendarItems = new SelectList(
                calendarRepository.GetUserCalendars(userId).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text",
                calendarId);
            eventViewModel.CalendarItems.First().Selected = true;
            eventViewModel.SelectedCalendarId = calendarId;
            return View(eventViewModel);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            string userId = User.Identity.GetUserId();
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            CreateEventViewModel eventViewModel = EventMapper.MapToEditEventVM(calendarEvent);
            if (calendarEvent.Repeatables != null)
                if (calendarEvent.Repeatables.Count > 0)
                    eventViewModel.repeatableSettings =
                        EventMapper.MapToRepeatableViewModel(calendarEvent.Repeatables.First());
            if (calendarEvent.Notifications != null)
                eventViewModel.Notifications = EventMapper.MapToNotificationListViewModel(
                    eventRepository.GetAllNotificationTypes(calendarEvent.Id));
            eventViewModel.CalendarItems = new SelectList(
                calendarRepository.GetUserCalendars(userId).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text",
                calendarEvent.CalendarId.ToString());
            eventViewModel.SelectedCalendarId = calendarEvent.CalendarId.Value;
            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateEventViewModel eventViewModel)
        {
            string userId = User.Identity.GetUserId();
            int calendarId = eventViewModel.SelectedCalendarId;
            if (ModelState.IsValid)
            {
                switch (EventValidator.Validate(eventViewModel))
                {
                    case 0:
                        CalendarEvent calendarEvent = eventRepository.FindById(id);
                        EventMapper.MapToEvent(ref calendarEvent, eventViewModel);
                        if (calendarId != eventRepository.GetCalendar(id).Id)
                        {
                            Calendar calendar = eventRepository.FindOtherById<Calendar>(calendarId);
                            if (calendar != null)
                            {
                                calendarEvent.Calendar.CalendarEventsCollection.Remove(calendarEvent);
                                calendarEvent.CalendarId = calendarId;
                                calendarEvent.Calendar = calendar;
                            }
                        }
                        eventRepository.Modify(calendarEvent);
                        eventRepository.SaveChanges();
                        if (eventViewModel.repeatableSettings.IfRepeatable)
                        {
                            Repeatable repeatable = eventRepository.GetRepeatableSettings(id);
                            if (repeatable != null)
                            {
                                eventRepository.DeleteAllChildrenEvents(id);
                                EventMapper.MapToRepeatable(eventViewModel.repeatableSettings, ref repeatable, calendarEvent);                                 
                                //add logic
                            }
                            else
                            {
                                repeatable = new Repeatable();
                                EventMapper.MapToRepeatable(eventViewModel.repeatableSettings, ref repeatable, calendarEvent);
                                repeatable.EventId = calendarEvent.Id;
                                repeatable.CalendarEvent = calendarEvent;
                                //add logic
                                eventRepository.AddRepeatableSettings(id, repeatable);
                            }
                        }
                        eventRepository.SaveChanges();
                        return RedirectToAction("Schedule");
                        
                    case 1:
                        ModelState.AddModelError("Name", "Name is required.");
                        break;
                    case 2:
                        ModelState.AddModelError("Begin time", "Begin time is required.");
                        break;
                }
            }
            eventViewModel.CalendarItems = new SelectList(
                calendarRepository.GetUserCalendars(userId).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(), "Value", "Text",
                calendarId);
            eventViewModel.CalendarItems.First().Selected = true;
            eventViewModel.SelectedCalendarId = calendarId;
            return View(eventViewModel);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            DisplayEventViewModel eventViewModel = EventMapper.MapToDisplayEventVM(calendarEvent);
            return View(eventViewModel);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            eventRepository.Delete(calendarEvent);
            eventRepository.SaveChanges();
            return RedirectToAction("Schedule");
        }
    }
}
