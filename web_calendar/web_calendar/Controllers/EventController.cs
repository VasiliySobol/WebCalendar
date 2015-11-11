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

namespace web_calendar.Controllers
{
    [Authorize]
    //[System.Web.Script.Services.ScriptService]
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
                list.Add(Mapper.MapToDisplayEventVM(item));
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
            DetailsEventViewModel eventViewModel = Mapper.MapToDetailsEventVM(calendarEvent);
            if (calendarEvent.Repeatables != null)
                if (calendarEvent.Repeatables.Count > 0)
                    eventViewModel.repeatableSettings =
                        Mapper.MapToRepeatableViewModel(calendarEvent.Repeatables.First());
            if (calendarEvent.Notifications != null)
                eventViewModel.Notifications = Mapper.MapToNotificationListViewModel(
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
                calendarRepository.GetUserCalendars(userId).First().Id.ToString());
            eventViewModel.CalendarItems.First().Selected = true;
            eventViewModel.SelectedCalendarId = calendarRepository.GetUserCalendars(userId).First().Id.ToString();
            return View(eventViewModel);
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                int calendarId;
                if (eventViewModel.Name != "" && eventViewModel.TimeBegin != null &&
                    Int32.TryParse(eventViewModel.SelectedCalendarId, out calendarId))
                {
                    if (eventRepository.FindOtherById<Calendar>(calendarId) != null)
                    {
                        CalendarEvent calendarEvent = Mapper.MapToEvent(eventViewModel);
                        eventRepository.Add(calendarEvent);
                        eventRepository.AddCalendar(calendarEvent.Id, calendarId);

                        if (eventViewModel.repeatableSettings != null)
                            if (eventViewModel.repeatableSettings.IfRepeatable)
                            {
                                Repeatable repeatable = Mapper.MapToRepeatable(eventViewModel);
                                repeatable.EventId = calendarEvent.Id;
                                repeatable.CalendarEvent = calendarEvent;
                                eventRepository.AddRepeatableSettings(calendarEvent.Id, repeatable);
                                //TODO : add repeatedevents
                            }

                        if (eventViewModel.Notifications != null)
                            if (eventViewModel.Notifications.Count > 0)
                            {
                                List<NotificationType> notifications = Mapper.MapToNotificationTypes(eventViewModel);
                                eventRepository.AddNotifications(calendarEvent.Id, notifications);
                            }

                        eventRepository.SaveChanges();

                        return RedirectToAction("Schedule");
                    }
                }
            }
            return View(eventViewModel);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            //TODO add logic
            CreateEventViewModel eventViewModel = Mapper.MapToEditEventVM(calendarEvent);
            if (calendarEvent.Repeatables != null)
                if (calendarEvent.Repeatables.Count > 0)
                    eventViewModel.repeatableSettings = 
                        Mapper.MapToRepeatableViewModel(calendarEvent.Repeatables.First());
            if (calendarEvent.Notifications != null)
                eventViewModel.Notifications = Mapper.MapToNotificationListViewModel(
                    eventRepository.GetAllNotificationTypes(calendarEvent.Id));
            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                CalendarEvent calendarEvent = Mapper.MapToEvent(eventViewModel);
                //TODO: add logic
                eventRepository.Modify(calendarEvent);
                eventRepository.SaveChanges();
                return RedirectToAction("Schedule");
            }
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
            DisplayEventViewModel eventViewModel = Mapper.MapToDisplayEventVM(calendarEvent);
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
