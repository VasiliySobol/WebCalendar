using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web_calendar.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Begin time is required.")]
        [Display(Name = "Begin time")]
        public System.DateTime TimeBegin { get; set; }

        [Display(Name = "End time")]
        public Nullable<System.DateTime> TimeEnd { get; set; }
        
        [Required(ErrorMessage = "Visibility is required.")]
        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [Display(Name = "All day")]
        public Nullable<bool> AllDay { get; set; }

        // Notification Settings
        [Display(Name = "Repetition count")]
        public Nullable<int> RepetitionCount { get; set; }

        [Display(Name = "Interval")]
        public Nullable<int> Interval { get; set; }

        [Display(Name = "Time before event")]
        public Nullable<int> TimeBefore { get; set; }

        [Display(Name = "Kind of notification")]
        public string KindOfNotification { get; set; }

        [Display(Name = "Guests")]
        public ICollection<string> GuestsEmails { get; set; }

        // Repeatable Settings
        [Display(Name = "If repeatable")]
        public bool IfRepeatable { get; set; }

        [Display(Name = "Period")]
        public string Period { get; set; }

        [Display(Name = "Repeat count")]
        public Nullable<int> RepeatCount { get; set; }
        // or
        [Display(Name = "End date")]
        public Nullable<System.DateTime> EndDate { get; set; }

        // Parent Calendar
        public int CalendarId { get; set; }

        [Display(Name = "Interval")]
        public string CalendarName { get; set; }
    }
}