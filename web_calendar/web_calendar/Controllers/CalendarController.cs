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
        private static CalendarViewModel activeCalendar;

        public CalendarController(ICalendarRepository _calendarRepository, IEventRepository _eventRepository)
        {
            CalendarDomainModel.calendarRepository = _calendarRepository;
        }       

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(CalendarViewModel calendarViewModel)
        {
            if (ModelState.IsValid)
            {
                calendarViewModel.userId = User.Identity.GetUserId();
                Calendar calendar = CalendarMapper.ToCalendar(calendarViewModel);
                calendar.ShowedDateTime = DateTime.Now.ToString();
                CalendarDomainModel.calendarRepository.Add(calendar);
                CalendarDomainModel.calendarRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView(CalendarMapper.ToCalendarViewModel(calendar));
            }
            else
            {
                return View(CalendarMapper.ToCalendarViewModel(calendar));
            }
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel calendarViewModel)
        {
            if (ModelState.IsValid)
            {
                Calendar calendar = CalendarMapper.ToCalendar(calendarViewModel);
                calendar.UserId = User.Identity.GetUserId();
                if (calendarViewModel.calendarDateTime == null)
                {
                    calendar.ShowedDateTime = DateTime.Now.ToString();
                }
                else
                {
                    calendar.ShowedDateTime = calendarViewModel.calendarDateTime.GetDateTime().ToString();
                }
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

        public PartialViewResult CalendarDayPartial(int id)
        {
            activeCalendar = SetCalendarAsActive(id);
            return PartialView("_CalendarDayPartial", activeCalendar);
        }

        public PartialViewResult CalendarWeekPartial(int id)
        {           
            activeCalendar = SetCalendarAsActive(id);
            return PartialView("_CalendarWeekPartial", activeCalendar);
        }

        public CalendarViewModel SetCalendarAsActive(int? id)
        {
            CalendarViewModel newActiveCalendar;

            if (id.HasValue)
            {
                newActiveCalendar = CalendarMapper.
                    ToCalendarViewModel(CalendarDomainModel.calendarRepository.FindById(id.Value));
            }
            else
            {
                newActiveCalendar = CalendarMapper.ToCalendarViewModel(CalendarDomainModel.calendarRepository.
                    GetUserCalendars(User.Identity.GetUserId()).ToList().FirstOrDefault());
            }

            return newActiveCalendar;
        }

        public ActionResult ShowPreviousMonth(int? id)
        {
            activeCalendar = SetCalendarAsActive(id);

            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id.Value);

            activeCalendar.calendarDateTime.SetPrevMonth(calendar.ShowedDateTime);
            calendar.ShowedDateTime = activeCalendar.calendarDateTime.GetDateTime().ToString();
            CalendarDomainModel.calendarRepository.SaveChanges();

            return PartialView("_CalendarMonthPartial", activeCalendar);
        }

        public ActionResult ShowNextMonth(int? id)
        {
            activeCalendar = SetCalendarAsActive(id);

            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id.Value);

            activeCalendar.calendarDateTime.SetNextMonth(calendar.ShowedDateTime);
            calendar.ShowedDateTime = activeCalendar.calendarDateTime.GetDateTime().ToString();
            CalendarDomainModel.calendarRepository.SaveChanges();

            return PartialView("_CalendarMonthPartial", activeCalendar);
        }

        public ActionResult ShowPreviousDay(int? id)
        {
            activeCalendar = SetCalendarAsActive(id);
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id.Value);

            activeCalendar.calendarDateTime.SetPrevDay(calendar.ShowedDateTime);
            calendar.ShowedDateTime = activeCalendar.calendarDateTime.GetDateTime().ToString();
            CalendarDomainModel.calendarRepository.SaveChanges();

            return PartialView("_CalendarDayPartial", activeCalendar);
        }

        public ActionResult ShowNextDay(int? id)
        {
            activeCalendar = SetCalendarAsActive(id);
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id.Value);

            activeCalendar.calendarDateTime.SetNextDay(calendar.ShowedDateTime);
            calendar.ShowedDateTime = activeCalendar.calendarDateTime.GetDateTime().ToString();
            CalendarDomainModel.calendarRepository.SaveChanges();

            return PartialView("_CalendarDayPartial", activeCalendar);
        }

        public ActionResult ShowDay(string Day)
        {
            if (activeCalendar == null) activeCalendar = SetCalendarAsActive(CalendarDomainModel.calendarRepository.GetUserCalendars(User.Identity.GetUserId()).First().Id);

            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(activeCalendar.id);

            activeCalendar.calendarDateTime.SetDateTime(Convert.ToDateTime(Day));
            calendar.ShowedDateTime = activeCalendar.calendarDateTime.GetDateTime().ToString();
            CalendarDomainModel.calendarRepository.SaveChanges();

            return PartialView("_CalendarDayPartial", activeCalendar);
        }

        public ActionResult CalendarMonthPartial(int? id)
        {
            activeCalendar = SetCalendarAsActive(id);
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