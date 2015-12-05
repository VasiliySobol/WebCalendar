using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.DAL.Models;

namespace web_calendar.BL.Services
{
    public static class ScheduleService
    {
        private static IScheduler schedule;
        private static ISchedulerFactory scheduleFactory;

        static ScheduleService()
        {
            // construct a scheduler factory
            scheduleFactory = new StdSchedulerFactory();

            // get a scheduler
            schedule = scheduleFactory.GetScheduler();
            schedule.Start();
        }

        public static void AddJob(string eventName, Notification notification, int minutes)
        {
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<NotificationJob>()
                .WithIdentity("myJob", "group1")
                .UsingJobData("eventName", eventName)
                .UsingJobData("kindOfNotification", notification.KindOfNotification)
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithRepeatCount(1)                  
                  .WithIntervalInMinutes(minutes))
              .Build();

            schedule.ScheduleJob(job, trigger);
        }
    }
}
