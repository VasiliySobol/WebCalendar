using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.Infrastructure.Services
{
    public static class EmailSender
    {
        private static AppSettingsReader config = null;

        private static AppSettingsReader Config
        {
            get
            {
                if (config == null)
                    config = new AppSettingsReader();
                return config;
            }
        }

        public static void SendMail(EmailModel email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email.To == string.Empty ? (string)Config.GetValue("EmailName", typeof(string)) : email.To);
            mail.Subject = "Invitation";
            mail.From = new MailAddress(email.From == string.Empty ? 
                (string)Config.GetValue("EmailName", typeof(string)) : email.From);
            mail.Body = email.Body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = (int)Config.GetValue("EmailPort", typeof(int)),
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential
                    ((string)Config.GetValue("EmailName", typeof(string)),
                     (string)Config.GetValue("EmailPassword", typeof(string))),
                EnableSsl = true
            };

            smtp.Send(mail);
        }
    }
}
