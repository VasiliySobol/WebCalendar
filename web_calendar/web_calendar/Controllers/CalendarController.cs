using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationRepository.Interface;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace web_calendar.Controllers
{
    public class CalendarController : Controller
    {
        public ICalendarRepository calendarRepository;

        public CalendarController(ICalendarRepository _calendarRepository)  
        {
            this.calendarRepository = _calendarRepository;  
        }

        [Authorize]
        public ActionResult Index(string RenderPart = "_CalendarMonthPartial")
        {
            ViewBag.RenderPart = RenderPart;
            return View(calRepo.GetUserCalendars(User.Identity.GetUserId()));
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult CalendarDayPartial()
        {
            return PartialView("_CalendarDayPartial");
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult CalendarWeekPartial()
        {
            return PartialView("_CalendarWeekPartial");
        }

        [ChildActionOnly]
        public ActionResult CalendarMonthPartial()
        {
            return PartialView("_CalendarMonthPartial");
        }

        public string JSONIndex()
        {
            var data = calendarRepository.GetAll();

            return JsonConvert.SerializeObject(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}