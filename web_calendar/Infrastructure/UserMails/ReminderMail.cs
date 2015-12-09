using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.UserMails
{
    public class ReminderMail
    {
        string userName;
        string userEmail;
        string eventName;
        DateTime eventDate;
        string eventText;

        public ReminderMail(string _userName, string _userEmail, string _eventName, DateTime _eventDate, string _eventText)
        {
            userName = _userName;
            userEmail = _userEmail;
            eventName = _eventName;
            eventDate = _eventDate;
            eventText = _eventText;
        }

        public string GetNotification()
        {
            StringBuilder message = new StringBuilder();
            message.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            message.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            message.Append("<head>");
            message.Append("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />");
            message.Append("<title>Invitation</title>");
            message.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'/>");
            message.Append("</head>");
            message.Append("<body style='margin: 0; padding: 0;'>");
            message.Append("<table align='center' border='0' cellpadding='0' cellspacing='0' width='500'  style='border: 1px solid #cccccc;'x>");
            message.Append("<tr><td align='center' bgcolor='#70b45a' style='padding: 40px 0 30px 0;'>");
            message.Append("<img src='http://www.aal-europe.eu/wp-content/uploads/2013/12/events_medium.jpg' alt='Web Calendar' width='300' height='230' style='display: block;' />");
            message.Append("</td></tr>");
            message.Append("<tr><td bgcolor='#ffffff' style='padding: 40px 30px 40px 30px;'>");
            message.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");
            message.Append("<tr><td style='color: #153643; font-family: Arial, sans-serif; font-size: 24px;'>");
            message.Append("<b>" + eventName + "</b>");
            message.Append("</td></tr>");
            message.Append("<tr><td style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>");
            message.Append("At: " + eventDate.ToLongDateString());
            message.Append("    " + eventDate.ToShortTimeString());
            message.Append("</td></tr><tr>");
            message.Append("<td style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>");
            message.Append(eventText);
            message.Append("</td></tr>");
            message.Append("</table>");
            message.Append("</td></tr><tr>");
            message.Append("<td bgcolor='#70b45a' style='padding: 30px 30px 30px 30px; color: #FFF; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>");
            message.Append("TO    : " + userName + "<br/>");
            message.Append("      " + userEmail);
            message.Append("From  :  Web Calendar Team<br/>");
            message.Append("</td></tr></table>");
            message.Append("</body>");
            message.Append("</html>");
            return message.ToString();
        }
    }
}
