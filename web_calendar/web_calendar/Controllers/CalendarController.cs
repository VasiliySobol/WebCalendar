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
using web_calendar.BL.Services;

namespace web_calendar.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        [HttpPost]
        public ActionResult GetCalendars()
        {
            var allCalendars = CalendarService.calendarRepository.GetAll();
            List<CalendarViewModel> list = new List<CalendarViewModel>();
            foreach (var item in allCalendars)
            {
                list.Add(Mapper.MapToCalendarViewModel(item));
            }
            return PartialView(list);
        }

        public CalendarController(ICalendarRepository _calendarRepository)
        {
            CalendarService.calendarRepository = _calendarRepository;
        }

        public ActionResult Index(string RenderPart = "_CalendarMonthPartial")
        {
            ViewBag.RenderPart = RenderPart;
            return View(CalendarService.GetCalendarViewModels(User.Identity.GetUserId()));
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
                Calendar calendar = Mapper.MapToCalendarFromCalendarVM(calendarVM);
                CalendarService.calendarRepository.Add(calendar);
                CalendarService.calendarRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ViewResult Edit(int id)
        {
            Calendar calendar = CalendarService.calendarRepository.FindById(id);
            return View(Mapper.MapToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel calendarVM)
        {
            if (ModelState.IsValid)
            {
                Calendar calendar = Mapper.MapToCalendarFromCalendarVM(calendarVM);
                calendar.UserId = User.Identity.GetUserId();
                CalendarService.calendarRepository.Modify(calendar);
                CalendarService.calendarRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ViewResult Delete(int id)
        {
            Calendar calendar = CalendarService.calendarRepository.FindById(id);
            return View(Mapper.MapToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Delete(CalendarViewModel calendarVM)
        {
            Calendar calendar = CalendarService.calendarRepository.FindById(calendarVM.id);
            CalendarService.calendarRepository.Delete(calendar);
            CalendarService.calendarRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public PartialViewResult CalendarDayPartial()
        {
            return PartialView("_CalendarDayPartial");
        }

        [ChildActionOnly]
        public PartialViewResult CalendarWeekPartial()
        {
            return PartialView("_CalendarWeekPartial");
        }

        [ChildActionOnly]
        public PartialViewResult CalendarMonthPartial()
        {
            return PartialView("_CalendarMonthPartial");
        }

        public string JSONIndex()
        {
            var data = CalendarService.calendarRepository.GetAll().ToList();
            return JsonConvert.SerializeObject(data);
        }

        public ActionResult Details(int id)
        {
            return View(CalendarService.GetDetails(id));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}