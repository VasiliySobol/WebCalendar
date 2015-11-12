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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CalendarViewModel calendar)
        {
            calendar.userId = User.Identity.GetUserId();
            CalendarService.calendarRepository.Add(Mapper.MapToCalendarFromCalendarVM(calendar));
            CalendarService.calendarRepository.SaveChanges();
            return View("Index");
        }

        public ViewResult Edit(int id)
        {
            Calendar calendar = CalendarService.calendarRepository.FindById(id);
            CalendarViewModel calendarVM = Mapper.MapToCalendarViewModel(calendar);
            string userId = User.Identity.GetUserId();
            calendarVM.userId = userId;
            return View(calendarVM);
        }

        [HttpPost]
        public ActionResult Edit(CalendarViewModel calendarVM)
        {
            Calendar calendar = Mapper.MapToCalendarFromCalendarVM(calendarVM);
            CalendarService.calendarRepository.Modify(calendar);
            CalendarService.calendarRepository.SaveChanges();
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            Calendar calendar = CalendarService.calendarRepository.FindById(id);
            CalendarViewModel calendarVM = Mapper.MapToCalendarViewModel(calendar);
            string userId = User.Identity.GetUserId();
            calendarVM.userId = userId;
            return View(calendarVM);
        }

        [HttpPost]
        public ActionResult Delete(CalendarViewModel calendarVM)
        {
            Calendar calendar = Mapper.MapToCalendarFromCalendarVM(calendarVM);
            CalendarService.calendarRepository.Delete(calendar);
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