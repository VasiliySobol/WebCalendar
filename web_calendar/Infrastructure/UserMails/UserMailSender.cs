using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.Infrastructure.Services;

namespace web_calendar.BL.UserMails
{
    public static class UserMailSender
    {
        public static void SendInvitations(List<string> guests, string userEmail, Invitation invitation)
        {
            foreach (string guest in guests)
            {
                EmailModel email = new EmailModel { To = guest, From = userEmail, Subject = "Invitation", 
                    Body = invitation.GetInvitation() };
                EmailSender.SendMail(email);
            }
        }
        public static void SendNotification(string userEmail, ReminderMail mail)
        {
            EmailModel email = new EmailModel
                {
                    To = userEmail,
                    From = "webcalendarteam@gmail.com",
                    Subject = "Notification",
                    Body = mail.GetNotification()
                };
            EmailSender.SendMail(email);
        }
    }
}
