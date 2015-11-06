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
using web_calendar.Mappers;
using web_calendar.DAL.Interface;

namespace web_calendar.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        public IEventRepository eventRepository;
        public INotificationRepository notificationRepository;

        public EventController(IEventRepository _eventRepository, INotificationRepository _notificationRepository)  
        {
            this.eventRepository = _eventRepository;
            this.notificationRepository = _notificationRepository;
        }
        // GET: Event/Schedule
        public ActionResult Schedule()
        {
            string userId = User.Identity.GetUserId();
            List<DisplayEventViewModel> list = new List<DisplayEventViewModel>();
            foreach (CalendarEvent item in eventRepository.GetAllUserEvents(userId))
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
            List<NotificationType> notifications = notificationRepository.GetAllEventNotificationTypes(id);
            List<Repeatable> repeatables = calendarEvent.Repeatables.ToList();
            return View(Mapper.MapToDetailsEventVM(calendarEvent, notifications, repeatables));
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Text,Place,TimeBegin,TimeEnd,Visibility,AllDay,RepetitionCount,Interval,TimeBefore,KindOfNotification,IfRepeatable,Period,RepeatCount,EndDate,CalendarName")] CreateEventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                CalendarEvent calendarEvent = Mapper.MapToEvent(eventViewModel);
                //TODO add logic
                eventRepository.Add(calendarEvent);
                eventRepository.SaveChanges();
                return RedirectToAction("Index");
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
            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Text,Place,TimeBegin,TimeEnd,Visibility,AllDay,RepetitionCount,Interval,TimeBefore,KindOfNotification,IfRepeatable,Period,RepeatCount,EndDate,CalendarName")] CreateEventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                CalendarEvent calendarEvent = Mapper.MapToEvent(eventViewModel);
                //TODO: add logic
                eventRepository.Modify(calendarEvent);
                eventRepository.SaveChanges();
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}
