using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_calendar.DAL.Interface;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using web_calendar.DAL.Models;
using web_calendar.Models;
using System.Net;
using web_calendar.BL.ViewModels;
using web_calendar.BL.Mappers;
using web_calendar.BL.DomainModels;

namespace web_calendar.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        public CalendarController(ICalendarRepository _calendarRepository, IEventRepository _eventRepository)
        {
            CalendarDomainModel.calendarRepository = _calendarRepository;
        }
        
        [HttpPost]
        public ActionResult CalendarList()
        {
            List<Calendar> allCalendars = CalendarDomainModel.calendarRepository.GetAll().ToList();
            List<CalendarViewModel> list = new List<CalendarViewModel>();
            foreach (Calendar item in allCalendars)
            {
                list.Add(CalendarMapper.ToCalendarViewModel(item));
            }
            return PartialView(list);
        }        

        public PartialViewResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public ActionResult Create(CalendarViewModel calendarViewModel)
        {
            if (ModelState.IsValid)
            {
                calendarViewModel.userId = User.Identity.GetUserId();
                Calendar calendar = CalendarMapper.ToCalendar(calendarViewModel);
                CalendarDomainModel.calendarRepository.Add(calendar);
                CalendarDomainModel.calendarRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public PartialViewResult Edit(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            return PartialView("Edit", CalendarMapper.ToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel calendarViewModel)
        {
            if (ModelState.IsValid)
            {
                Calendar calendar = CalendarMapper.ToCalendar(calendarViewModel);
                calendar.UserId = User.Identity.GetUserId();
                CalendarDomainModel.calendarRepository.Modify(calendar);
                CalendarDomainModel.calendarRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            CalendarDomainModel.calendarRepository.Delete(calendar);
            CalendarDomainModel.calendarRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult Index()
        {
            return View(CalendarDomainModel.GetCalendarViewModels(User.Identity.GetUserId()));
        }

        public PartialViewResult CalendarDayPartial(int? id, string Day)
        {
            DateTime currentDay = DateTime.Now;
            if (!string.IsNullOrEmpty(Day))
            {
                currentDay = Convert.ToDateTime(Day);
            }

            Calendar activeCalendar;
            if (id.HasValue)
            {
                activeCalendar = CalendarDomainModel.calendarRepository.FindById(id.Value);
            }
            else
            {
                activeCalendar = CalendarDomainModel.calendarRepository.
                    GetUserCalendars(User.Identity.GetUserId()).ToList().FirstOrDefault();
            }

            List<CalendarEvent> dayEventList = new List<CalendarEvent>();
            foreach(CalendarEvent calendarEvent in activeCalendar.CalendarEvents1)
            {
                if (calendarEvent.TimeBegin.DayOfYear == currentDay.DayOfYear)
                {
                    dayEventList.Add(calendarEvent);
                }
            }
            return PartialView("_CalendarDayPartial", dayEventList);
        }

        public PartialViewResult CalendarWeekPartial()
        {
            return PartialView("_CalendarWeekPartial");
        }

        public ActionResult CalendarMonthPartial(int? id)
        {
            CalendarViewModel activeCalendar;
            if (id.HasValue)
            {
                activeCalendar = CalendarMapper.
                    ToCalendarViewModel(CalendarDomainModel.calendarRepository.FindById(id.Value));
            }
            else
            {
                activeCalendar = CalendarMapper.ToCalendarViewModel(CalendarDomainModel.calendarRepository.
                    GetUserCalendars(User.Identity.GetUserId()).ToList().FirstOrDefault());
            }
            return PartialView("_CalendarMonthPartial", activeCalendar);
        }

        public string JSONIndex()
        {
            List<Calendar> data = CalendarDomainModel.calendarRepository.GetAll().ToList();
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult Details(int id)
        {
            return View(CalendarDomainModel.GetDetails(id));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}