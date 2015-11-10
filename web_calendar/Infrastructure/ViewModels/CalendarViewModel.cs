using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.Mvc;

namespace web_calendar.BL.ViewModels
{
    public class CalendarViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CalendarViewId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Color")]
        public Color CalendarColor { get; set; }

        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [Display(Name = "Time zone")]
        public string TimeZone { get; set; }

        // Notification Settings
        public NotificationSettingsViewModel notificationSettings { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string userId { get; set; }
    }
}
