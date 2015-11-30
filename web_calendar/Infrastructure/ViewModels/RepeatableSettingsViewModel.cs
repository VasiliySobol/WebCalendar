using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.ViewModels
{
    public class RepeatableSettingsViewModel
    {
        public RepeatableSettingsViewModel()
        {
        }

        public int Id { get; set; }

        [Display(Name = "Repeatable")]
        public bool IfRepeatable { get; set; }

        [Required(ErrorMessage = "Period is required.")]
        [Display(Name = "Period")]
        public string Period { get; set; }

        [Display(Name = "Repeat count")]
        public Nullable<int> RepeatCount { get; set; }

        [Display(Name = "Days of Week")]
        public string DaysOfWeek { get; set; }

        [Display(Name = "Time of day")]
        public string TimeOfDay { get; set; }

        [Display(Name = "Day of Year")]
        public string DayOfYear { get; set; }

        [Display(Name = "Day of Month")]
        public string DayOfMonth { get; set; }
    }
}
