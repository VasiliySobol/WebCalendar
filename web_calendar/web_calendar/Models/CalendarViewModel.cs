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
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Name")]
        public string Text { get; set; }

        [Display(Name = "Color")]
        public Color Color { get; set; }

        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [Display(Name = "Time zone")]
        public string TimeZone { get; set; }

        // Notification Settings
        [Display(Name = "Repetition count")]
        public Nullable<int> RepetitionCount { get; set; }

        [Display(Name = "Interval")]
        public Nullable<int> Interval { get; set; }

        [Display(Name = "Time before event")]
        public Nullable<int> TimeBefore { get; set; }

        [Display(Name = "Kind of notification")]
        public string KindOfNotification { get; set; }

        int _userId {get; set; }
    }
}