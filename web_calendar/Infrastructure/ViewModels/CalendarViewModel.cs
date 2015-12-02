using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.Mvc;
using web_calendar.BL.CustomClasses;

namespace web_calendar.BL.ViewModels
{
    public class CalendarViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [MaxLength(50, ErrorMessage = "Description is too long (50 characters max).")]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Color")]
        public Color calendarColor { get; set; }

        [Display(Name = "Share")]
        public string visibility { get; set; }

        [Display(Name = "Time zone")]
        public string timeZone { get; set; }

        // Notification Settings
        public NotificationSettingsViewModel notificationSettings { get; set; }

        public string CSSMainColor { get; set; }

        public string CSSHeadColor { get; set; }

        public CalendarDateTime calendarDateTime { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string userId { get; set; }
    }
}
