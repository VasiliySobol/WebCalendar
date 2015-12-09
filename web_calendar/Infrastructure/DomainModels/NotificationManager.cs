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

        public void SetManager()
        {
            notificationRepository = new NotificationRepository();
            if (NotificationQueue.EmailQueue.Count == 0)
                NotificationQueue.AppendQueue(ref NotificationQueue.EmailQueue,
                    notificationRepository.GetNextNotifications(userId, "email"));
            Reminder reminder = NotificationQueue.GetClosest(ref NotificationQueue.EmailQueue);
            _timer = new Timer(ProcessTimerEvent, null, reminder.Time, Timeout.Infinite);
        }

        private void ProcessTimerEvent(object obj)
        {
            if (NotificationQueue.EmailQueue.Count == 0)
                NotificationQueue.AppendQueue(ref NotificationQueue.EmailQueue,
                    notificationRepository.GetNextNotifications(userId, "email"));
            Reminder reminder = NotificationQueue.GetClosest(ref NotificationQueue.EmailQueue);
            CalendarEvent calendarEvent = notificationRepository.FindOtherById<CalendarEvent>(reminder.eventId);
            ReminderMail mail = new ReminderMail(userName, userEmail, calendarEvent.Name, 
                calendarEvent.TimeBegin, calendarEvent.Text);
            UserMailSender.SendNotification(userEmail, mail);
            NotificationQueue.RemovePrevious(ref NotificationQueue.EmailQueue);
            reminder = NotificationQueue.GetClosest(ref NotificationQueue.EmailQueue);
            _timer.Change(reminder.Time, Timeout.Infinite);
        }
    }
}
