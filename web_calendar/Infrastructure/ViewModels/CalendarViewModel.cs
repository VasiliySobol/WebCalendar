using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
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
        public string CalendarColor { get; set; }

        [Display(Name = "Visibility")]
        public string Visibility { get; set; }

        [Display(Name = "Time zone")]
        public byte? TimeZone { get; set; }

        // Notification Settings
        public NotificationSettingsViewModel notificationSettings { get; set; }

        string _userId { get; set; }
    }
}
