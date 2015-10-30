using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationRepository.Models;
using web_calendar.Models;
using web_calendar.Mappers;

namespace web_calendar.Controllers
{
    public class EventController : Controller
    {
        private web_calendarEntities db = new web_calendarEntities();

        // GET: Event
        public ActionResult Index()
        {             
            return View(db.CalendarEvents.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarEvent calendarEvent = db.CalendarEvents.Find(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            ////TODO
            //ICollection<NotificationType> notificationTypes;
            //ICollection<Repeatable> repeatables;
            //return View(Mapper.Map(_event, notificationTypes, repeatables));
            return View(calendarEvent);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Text,Place,TimeBegin,TimeEnd,Visibility,AllDay,RepetitionCount,Interval,TimeBefore,KindOfNotification,IfRepeatable,Period,RepeatCount,EndDate,CalendarId,CalendarName")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                CalendarEvent calendarEvent = Mapper.MapToEvent(eventViewModel);
                //TODO add logic
                db.CalendarEvents.Add(calendarEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventViewModel);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarEvent calendarEvent = db.CalendarEvents.Find(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            //TODO add logic
            EventViewModel eventViewModel = new EventViewModel();
            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Text,Place,TimeBegin,TimeEnd,Visibility,AllDay,RepetitionCount,Interval,TimeBefore,KindOfNotification,IfRepeatable,Period,RepeatCount,EndDate,CalendarId,CalendarName")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarEvent calendarEvent = db.CalendarEvents.Find(id);
            if (calendarEvent == null)
            {
                return HttpNotFound();
            }
            //TODO add logic
            EventViewModel eventViewModel = new EventViewModel();
            return View(eventViewModel);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CalendarEvent calendarEvent = db.CalendarEvents.Find(id);
            db.CalendarEvents.Remove(calendarEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
