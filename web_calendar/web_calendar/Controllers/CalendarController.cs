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
        public CalendarController(ICalendarRepository _calendarRepository)
        {
            CalendarDomainModel.calendarRepository = _calendarRepository;
        }
        
        [HttpPost]
        public ActionResult CalendarList()
        {
            var allCalendars = CalendarDomainModel.calendarRepository.GetAll();
            List<CalendarViewModel> list = new List<CalendarViewModel>();
            foreach (var item in allCalendars)
            {
                list.Add(CalendarMapper.ToCalendarViewModel(item));
            }
            return PartialView(list);
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

        public ViewResult Edit(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            return View(CalendarMapper.ToCalendarViewModel(calendar));
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

        public ViewResult Delete(int id)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(id);
            return View(CalendarMapper.ToCalendarViewModel(calendar));
        }

        [HttpPost]
        public ActionResult Delete(CalendarViewModel calendarViewModel)
        {
            Calendar calendar = CalendarDomainModel.calendarRepository.FindById(calendarViewModel.id);
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

            CalendarViewModel activeCalendar = CalendarMapper.ToCalendarViewModel(CalendarDomainModel.calendarRepository.FindById(id));

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