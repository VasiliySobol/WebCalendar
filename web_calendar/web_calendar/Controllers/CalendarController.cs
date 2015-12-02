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
        [HttpPost]
        public ActionResult GetCalendars()
        {
            var allCalendars = CalendarDomainModel.calendarRepository.GetAll();
            List<CalendarViewModel> list = new List<CalendarViewModel>();
            foreach (var item in allCalendars)
            {
                list.Add(CalendarMapper.MapToCalendarViewModel(item));
            }
            return PartialView(list);
        }

        public CalendarController(ICalendarRepository _calendarRepository)
        {
            CalendarDomainModel.calendarRepository = _calendarRepository;
        }

        public ActionResult Index(string RenderPart = "_CalendarMonthPartial")
        {         
            ViewBag.RenderPart = RenderPart;
            return View(CalendarDomainModel.GetCalendarViewModels(User.Identity.GetUserId()));
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CalendarViewModel calendarVM)
        {
            if (ModelState.IsValid)
            {
                calendarVM.userId = User.Identity.GetUserId();
                Calendar calendar = CalendarMapper.MapToCalendarFromCalendarVM(calendarVM);
                CalendarDomainModel.calendarRepository.Add(calendar);
                CalendarDomainModel.calendarRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ViewResult Edit(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            return View(CalendarMapper.MapToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel calendarVM)
        {
            if (ModelState.IsValid)
            {
                Calendar calendar = CalendarMapper.MapToCalendarFromCalendarVM(calendarVM);
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

        public ViewResult Delete(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            return View(CalendarMapper.MapToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Delete(CalendarViewModel calendarVM)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(calendarVM.id);
            CalendarDomainModel.calendarRepository.Delete(calendar);
            CalendarDomainModel.calendarRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult CalendarDayPartial()
        {
            return PartialView("_CalendarDayPartial");
        }

        public PartialViewResult CalendarWeekPartial()
        {
            return PartialView("_CalendarWeekPartial");
        }

        public ActionResult CalendarMonthPartial(int id)
        {
            var userCalendars = CalendarDomainModel.calendarRepository.GetUserCalendars(User.Identity.GetUserId());

            CalendarViewModel activeCalendar = CalendarMapper.MapToCalendarViewModel(CalendarDomainModel.calendarRepository.FindById(id));

            return PartialView("_CalendarMonthPartial", activeCalendar);
        }



        public string JSONIndex()
        {
            var data = CalendarDomainModel.calendarRepository.GetAll().ToList();
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