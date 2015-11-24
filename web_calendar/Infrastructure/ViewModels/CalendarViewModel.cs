using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.Mvc;

namespace web_calendar.BL.ViewModels
{
    public class CalendarViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Color")]
        public Color calendarColor { get; set; }

        [Display(Name = "Visibility")]
        public string visibility { get; set; }

        [Display(Name = "Time zone")]
        public string timeZone { get; set; }

        // Notification Settings
        public NotificationSettingsViewModel notificationSettings { get; set; }

        public bool isActive { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string userId { get; set; }
    }
}
