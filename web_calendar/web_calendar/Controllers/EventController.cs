﻿using System;
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
using System.Transactions;
using web_calendar.BL.DomainModels;

namespace web_calendar.Controllers
{
    [Authorize]
    //[CustomHandleErrorAttribute]
    public class EventController : Controller
    {
        private IEventRepository eventRepository;
        private INotificationRepository notificationRepository;
        private ICalendarRepository calendarRepository;
        private EventDomainModel eventDomainModel;

        public EventController(IEventRepository _eventRepository, INotificationRepository _notificationRepository,
            ICalendarRepository _calendarRepository, EventDomainModel _eventDomainModel)  
        {
            this.eventRepository = _eventRepository;
            this.notificationRepository = _notificationRepository;
            this.calendarRepository = _calendarRepository;
            this.eventDomainModel = new EventDomainModel(eventRepository, notificationRepository, calendarRepository);
        }

        public JsonResult GetNextNotification()
        {
            string userId = User.Identity.GetUserId();
            return Json(eventDomainModel.GetNextReminder(userId), JsonRequestBehavior.AllowGet);
        }

        // GET: Event/Schedule/2
        public ActionResult Schedule(int? id)
        {
            string userId = User.Identity.GetUserId();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Schedule", eventDomainModel.GetFollowingEvents(id, userId));
            }
            else
            {
                return View("Schedule", eventDomainModel.GetFollowingEvents(id, userId));
            }
        }

        // GET: Event/Notify/2
        public ActionResult Notify(int id)
        {
            if (eventRepository.FindById(id) != null)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().
                    GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                eventDomainModel.SendInvitations(user.Email, user.UserName, id);
            }
            return RedirectToAction("Schedule");
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            return View(eventDomainModel.GetEventDetails(calendarEvent));
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            CreateEventViewModel eventViewModel = new CreateEventViewModel();
            eventDomainModel.PopulateCalendarSelectList(ref eventViewModel, userId);
            if (Request.IsAjaxRequest())
            {
                return PartialView(eventViewModel);
            }
            else
            {
                return View(eventViewModel);
            }
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
                            eventDomainModel.CreateEvent(eventViewModel, calendarId);
                            return RedirectToAction("Schedule");
                        }
                        break;
                    case 1:
                        ModelState.AddModelError("Name", "Name is required.");
                        break;
                    case 2:
                        ModelState.AddModelError("Begin time", "Begin time is required.");
                        break;
                    case 3:
                        ModelState.AddModelError("", "All notifications must have specified time before event.");
                        break;
                    case 4:
                        ModelState.AddModelError("", "All guests must have an email.");
                        break;
                }
            }
            eventDomainModel.PopulateCalendarSelectList(ref eventViewModel, userId);
            return View(eventViewModel);
        }

        /*
        public ActionResult CreateByRef(string eventByRef)
        {
            CreateEventViewModel eventViewModel = JsonConvert.DeserializeObject<CreateEventViewModel>(eventByRef);           
            string userId = User.Identity.GetUserId();
            eventDomainModel.PopulateCalendarSelectList(ref eventViewModel, userId);
            return View("Create",eventViewModel);
        }*/

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            string userId = User.Identity.GetUserId();
            CalendarEvent calendarEvent = eventRepository.FindById(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            CreateEventViewModel eventViewModel = eventDomainModel.GetEditEventViewModel(calendarEvent);
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
                        eventDomainModel.EditEvent(id, eventViewModel, calendarId);
                        return RedirectToAction("Schedule");                        
                    case 1:
                        ModelState.AddModelError("Name", "Name is required.");
                        break;
                    case 2:
                        ModelState.AddModelError("Begin time", "Begin time is required.");
                        break;
                    case 3:
                        ModelState.AddModelError("", "All notifications must have specified time before event.");
                        break;
                    case 4:
                        ModelState.AddModelError("", "All guests must have an email.");
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

        public ActionResult GetNotificationView(int id)
        {
            NotificationViewModel notificationVM = new NotificationViewModel();
            notificationVM.Id = id;
            notificationVM.MyPrefix = "Notifications[" + id + "]";
            return PartialView("_EditNotification", notificationVM);
        }

        public ActionResult GetGuestView(int id)
        {
            GuestsEmail guestVM = new GuestsEmail();
            guestVM.Id = id;
            guestVM.MyPrefix = "Guests[" + id + "]";
            return PartialView("_EditGuest", guestVM);
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
            eventDomainModel.DeleteEvent(id);
            return RedirectToAction("Schedule");
        }
    }
}
