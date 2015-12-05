using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.ViewModels;

namespace web_calendar.BL.Services
{
    public static class EmailService
    {
        public static void Email(EmailModel email, string credentialUser, string credentialPassword)
        {
            //string host = ConfigurationManager.AppSettings["SMTPHost"];
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    mail.Body = email.Message;
            //    mail.To.Add(new MailAddress(email.To));
            //    mail.From = new MailAddress(email.From, email.Name);
            //    mail.Subject = email.Subject;
            //    mail.SubjectEncoding = Encoding.UTF8;
            //    mail.Priority = MailPriority.Normal;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
            //    smtp.Host = host;
            //    smtp.Send(mail);
            //}
            //catch (Exception ex)
            //{
            //    //log error
            //}
        }
    }
}
