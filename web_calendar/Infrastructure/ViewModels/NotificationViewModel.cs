using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Time before event")]
        public Nullable<int> TimeBefore { get; set; }

        [Display(Name = "Kind of notification")]
        public string KindOfNotification { get; set; }

        public string MyPrefix { get; set; }
    }

    public class Reminder
    {
        public string Name { get; set; }
        public int Time { get; set; }
    }

    public class EmailReminder
    {
        public int eventId { get; set; }
        public string userId { get; set; }
        public string EventName { get; set; }
        public string EventText { get; set; }
        public int Time { get; set; }
        public DateTime EventTime { get; set; }
        public DateTime NotificationTime { get; set; }        
    }
}
