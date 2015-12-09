using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using web_calendar.BL.DataTypes;
using web_calendar.BL.UserMails;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Concrete;
using web_calendar.DAL.Models;
using web_calendar.Models;

namespace web_calendar.Managers
{

    public class NotificationManager
    {
        private Timer _timer;
        private NotificationRepository notificationRepository;
        private ApplicationDbContext context;

        public void SetManager()
        {
            notificationRepository = new NotificationRepository();
            context = new ApplicationDbContext();
            foreach (ApplicationUser user in context.Users)
            {
                NotificationQueue.AppendEmailQueue(notificationRepository.GetNextNotifications(user.Id, "email"), user.Id);
            }
            EmailReminder reminder = NotificationQueue.GetClosestEmailReminder();
            if (reminder == null)
                _timer = new Timer(ProcessTimerEvent, null, 50000, Timeout.Infinite);
            else
                _timer = new Timer(ProcessTimerEvent, null, reminder.Time, Timeout.Infinite);
        }

        private void ProcessTimerEvent(object obj)
        {
            // if we get here, notify
            EmailReminder reminder = NotificationQueue.GetClosestEmailReminder();
            if (reminder != null && reminder.NotificationTime.CompareTo(DateTime.Now.AddMinutes(-1)) < 0)
            {
                ApplicationUser user = context.Users.Find(reminder.userId);
                ReminderMail mail = new ReminderMail(user.UserName, user.Email, reminder.EventName,
                    reminder.EventTime, reminder.EventText);
                UserMailSender.SendNotification(user.Email, mail);
                NotificationQueue.RemovePrevious();
            }

            // set next reminder
            if (NotificationQueue.EmailQueue.Count == 0)
                foreach (ApplicationUser user in context.Users)
                {
                    NotificationQueue.AppendEmailQueue(notificationRepository.GetNextNotifications(user.Id, "email"), user.Id);
                }
            reminder = NotificationQueue.GetClosestEmailReminder();
            if (reminder == null)
                _timer.Change(50000, Timeout.Infinite);
            else
                _timer.Change(reminder.Time, Timeout.Infinite);
        }
    }
}