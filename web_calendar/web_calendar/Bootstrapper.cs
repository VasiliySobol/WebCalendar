using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using web_calendar.DAL.Interface;
using web_calendar.DAL.Concrete;
using web_calendar.Controllers;

namespace ApplicationRepository
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ICalendarRepository, CalendarRepository>();
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<INotificationRepository, NotificationRepository>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
