using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Repetition count")]
        public Nullable<int> RepetitionCount { get; set; }

        [Display(Name = "Interval")]
        public Nullable<int> Interval { get; set; }

        [Display(Name = "Time before event")]
        public Nullable<int> TimeBefore { get; set; }

        [Display(Name = "Kind of notification")]
        public string KindOfNotification { get; set; }
    }
}
