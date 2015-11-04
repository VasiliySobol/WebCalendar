using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web_calendar.Models
{
    public class DetailsEventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Place")]
        public string Place { get; set; }

        [Display(Name = "Begin time")]
        public System.DateTime TimeBegin { get; set; }

        [Display(Name = "End time")]
        public Nullable<System.DateTime> TimeEnd { get; set; }
        
        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [Display(Name = "All day")]
        public Nullable<bool> AllDay { get; set; }

        // Guests
        [Display(Name = "Guests")]
        public ICollection<string> GuestsEmails { get; set; }

        // Notification Settings
        [Display(Name = "Notification settings")]
        public ICollection<NotificationSettingsViewModel> notificationSettings { get; set; }

        // Repeatable Settings
        [Display(Name = "Make repeatable")]
        public ICollection<RepeatableSettingsViewModel> repeatableSettings { get; set; }

        // Parent Calendar
        public int CalendarId { get; set; }

        [Display(Name = "Calendar name")]
        public string CalendarName { get; set; }
    }

    public class DisplayEventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Place")]
        public string Place { get; set; }

        [Display(Name = "Begin time")]
        public System.DateTime TimeBegin { get; set; }

        [Display(Name = "Notification's")]
        public int NotificationCount { get; set; }

        // Parent Calendar
        public int CalendarId { get; set; }

        [Display(Name = "Interval")]
        public string CalendarName { get; set; }
    }

    public class CreateEventViewModel
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
        public bool AllDay { get; set; }

        // Guests
        [Display(Name = "Guests")]
        public ICollection<string> GuestsEmails { get; set; }

        // Notification Settings
        [Display(Name = "Notification settings")]
        public ICollection<NotificationSettingsViewModel> notificationSettings { get; set; }

        // Repeatable Settings
        [Display(Name = "Make repeatable")]
        public ICollection<RepeatableSettingsViewModel> repeatableSettings { get; set; }

        // Parent Calendar
        public int CalendarId { get; set; }

        [Display(Name = "Calendar name")]
        public string CalendarName { get; set; }
    }

    public class RepeatableSettingsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "If repeatable")]
        public bool IfRepeatable { get; set; }

        [Display(Name = "Period")]
        public string Period { get; set; }

        [Display(Name = "Repeat count")]
        public Nullable<int> RepeatCount { get; set; }
        // or
        [Display(Name = "End date")]
        public Nullable<System.DateTime> EndDate { get; set; }
    }
}