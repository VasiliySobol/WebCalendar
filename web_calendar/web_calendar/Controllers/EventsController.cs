using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using web_calendar.BL.Mappers;
using web_calendar.BL.Services;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Interface;
using web_calendar.Handlers;
using Microsoft.AspNet.Identity;
using web_calendar.DAL.Concrete;
using web_calendar.DAL.Models;
using System.Web.Http.Cors;

namespace web_calendar.Controllers
{
    [Authorize]
    public class EventsController : ApiController
    {
        // GET: api/Events
        [HttpGet]
        public IEnumerable<DisplayEventViewModel> Get()
        {
            string userId = User.Identity.GetUserId();
            EventRepository eventRepository = new EventRepository();
            List<CalendarEvent> events = eventRepository.GetAllUserEvents(userId).ToList();
            List<DisplayEventViewModel> list = new List<DisplayEventViewModel>();
            foreach (CalendarEvent item in events)
            {
                list.Add(Mapper.MapToDisplayEventVM(item));
            }            
            return list;
        }

        // POST: api/Events
        [HttpPost]
        public void Post(string value)
        {            
            EventRepository eventRepository = new EventRepository();
            CreateEventViewModel CEVM = new CreateEventViewModel();

            string userId = User.Identity.GetUserId();
            var calendars = CalendarService.GetCalendarViewModels(userId);
            CEVM.SelectedCalendarId = calendars.ElementAt(0).id;
            CEVM.Name = value;

            var CE = Mapper.MapToEvent(CEVM);
            eventRepository.Add(CE);
            eventRepository.SaveChanges();
        }
    }
}
