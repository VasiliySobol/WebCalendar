using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web_calendar.BL.DataTypes;
using web_calendar.BL.UserMails;
using web_calendar.BL.ViewModels;
using web_calendar.DAL.Concrete;
using web_calendar.DAL.Models;

namespace web_calendar.BL.DomainModels
{
    public class NotificationManager
    {
        private Timer _timer;
        private string userId;
        private string userName;
        private string userEmail;
        private NotificationRepository notificationRepository;

        public void SetManager(string _userId, string _userName, string _userEmail)
        {
            userEmail = _userEmail;
            userId = _userId;
            userName = _userName;
            notificationRepository = new NotificationRepository();
            if (NotificationQueue.EmailQueue.Count == 0)
                NotificationQueue.AppendQueue(ref NotificationQueue.EmailQueue,
                    notificationRepository.GetNextNotifications(userId, "email"));
            Reminder reminder = NotificationQueue.GetClosest(ref NotificationQueue.EmailQueue);
            if (reminder == null)
                reminder = new Reminder() { eventId = -1, Name = "", Time = 50000 };
            _timer = new Timer(ProcessTimerEvent, null, reminder.Time, Timeout.Infinite);
        }

        private void ProcessTimerEvent(object obj)
        {
            // if we get here, notify
            Reminder reminder = NotificationQueue.GetClosest();
            if (reminder != null)
            {
                CalendarEvent calendarEvent = notificationRepository.FindOtherById<CalendarEvent>(reminder.eventId);
                ReminderMail mail = new ReminderMail(userName, userEmail, calendarEvent.Name,
                    calendarEvent.TimeBegin, calendarEvent.Text);
                UserMailSender.SendNotification(userEmail, mail);
                NotificationQueue.RemovePrevious(ref NotificationQueue.EmailQueue);
            }

            // set next reminder
            if (NotificationQueue.EmailQueue.Count == 0)
                NotificationQueue.AppendQueue(ref NotificationQueue.EmailQueue,
                    notificationRepository.GetNextNotifications(userId, "email"));
            reminder = NotificationQueue.GetClosest();
            if (reminder == null)
                reminder = new Reminder() { eventId = -1, Name = "", Time = 50000 };
            _timer.Change(reminder.Time, Timeout.Infinite);
        }
    }
}
