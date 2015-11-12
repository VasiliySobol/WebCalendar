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
        public CalendarController(ICalendarRepository _calendarRepository)  
        {
            CalendarService.calendarRepository = _calendarRepository;  
        }

        public ActionResult Index(string RenderPart = "_CalendarMonthPartial")
        {
            ViewBag.RenderPart = RenderPart;			
            return View(CalendarService.GetCalendarViewModels(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult Create(CalendarViewModel calendar)
        {
            calendar.userId = User.Identity.GetUserId();
            CalendarService.calendarRepository.Add(Mapper.MapToCalendarFromCalendarVM(calendar));
            CalendarService.calendarRepository.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel calendar)
        {
            CalendarService.calendarRepository.Modify(Mapper.MapToCalendarFromCalendarVM(calendar));
            CalendarService.calendarRepository.SaveChanges();
            return View("Index");
        }

        public ViewResult Edit(int id)
        {
            var calendar = CalendarService.calendarRepository.FindById(id);
            return View(Mapper.MapToCalendarViewModel(calendar));
        }

        public ActionResult Delete(int id)
        {
            var calendar = CalendarService.calendarRepository.FindById(id);
            return View(Mapper.MapToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Delete(CalendarViewModel calendar)
        {
            CalendarService.calendarRepository.Delete(Mapper.MapToCalendarFromCalendarVM(calendar));
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult CalendarDayPartial()
        {
            return PartialView("_CalendarDayPartial");
        }

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
            var data = CalendarService.calendarRepository.GetAll();

            return JsonConvert.SerializeObject(data);
        }

        public ActionResult Create()
        {
            return View();
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