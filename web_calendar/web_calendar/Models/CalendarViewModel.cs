using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace web_calendar.Models
{
    public class CalendarViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Text { get; set; }
        public  Color Color { get; set; }
        bool Visibility { get; set; }
        string TimeZone { get; set; }
        public Nullable<int> RepetitionCount { get; set; }
        public Nullable<int> Interval { get; set; }
        public Nullable<int> TimeBefore { get; set; }
        public string KindOfNotification { get; set; }

        int _userId {get; set; }
    }
}