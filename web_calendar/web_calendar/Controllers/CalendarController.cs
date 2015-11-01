using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationRepository.Interface;
using Newtonsoft.Json;  

namespace web_calendar.Controllers
{
    public class CalendarController : Controller
    {
        public ICalendarRepository calRepo;

        public CalendarController(ICalendarRepository _calRepo)  
        {  
            this.calRepo = _calRepo;  
        }  

        public ActionResult Index()
        {
            return View();
        }

        public string JSONIndex()
        {
            var data = calRepo.GetAll();

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