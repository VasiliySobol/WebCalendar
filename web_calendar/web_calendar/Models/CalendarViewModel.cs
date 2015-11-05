using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace web_calendar.Models
{
    public class CalendarViewModel
    {
        public int CalendarViewId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Color")]
        public int? CalendarColor { get; set; }

        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [Display(Name = "Time zone")]
        public byte? TimeZone { get; set; }

        // Notification Settings
        public NotificationSettingsViewModel notificationSettings { get; set; }        

        string _userId {get; set; }
    }
}