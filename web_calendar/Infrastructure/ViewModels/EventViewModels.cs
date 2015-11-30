using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace web_calendar.BL.ViewModels
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

        [Display(Name = "Share")]
        public string Visibility { get; set; }

        [Display(Name = "All day")]
        public string AllDay { get; set; }

        // Guests
        [Display(Name = "Guests")]
        public List<string> Guests { get; set; }

        // Notification Settings
        [Display(Name = "Notification settings")]
        public List<NotificationSettingsViewModel> Notifications { get; set; }

        // Repeatable Settings
        [Display(Name = "Make repeatable")]
        public RepeatableSettingsViewModel repeatableSettings { get; set; }

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

        [Display(Name = "Calendar name")]
        public string CalendarName { get; set; }
    }

    public class CreateEventViewModel
    {
        public CreateEventViewModel()
        {
            TimeBegin = DateTime.Now;
            DateBegin = DateTime.Now;
            repeatableSettings = new RepeatableSettingsViewModel();
            Notifications = new List<NotificationSettingsViewModel>();
            Guests = new List<GuestsEmail>();
        }

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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public System.DateTime TimeBegin { get; set; }

        [Required(ErrorMessage = "Begin date is required.")]
        [Display(Name = "Begin date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public System.DateTime DateBegin { get; set; }

        [Display(Name = "End time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [DataType(DataType.Time)]
        public System.DateTime? TimeEnd { get; set; }

        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public System.DateTime? DateEnd { get; set; }

        [Required(ErrorMessage = "Share is required.")]
        [Display(Name = "Share")]
        public string Visibility { get; set; }

        [Display(Name = "All day")]
        public bool AllDay { get; set; }

        // Guests
        [Display(Name = "Guests")]
        public List<GuestsEmail> Guests { get; set; }

        // Notification Settings
        [Display(Name = "Notification settings")]
        public List<NotificationSettingsViewModel> Notifications { get; set; }

        // Repeatable Settings
        [Display(Name = "Make repeatable")]
        public RepeatableSettingsViewModel repeatableSettings { get; set; }

        // Parent Calendar
        [Display(Name = "Calendar name")]
        public int SelectedCalendarId { get; set; }

        public SelectList CalendarItems { get; set; }
    }
    //public class NotificationsGroupViewModel
    //{
    //    public IList<NotificationSettingsViewModel> Notifications { get; set; }

    //    public IList<NotificationSettingsViewModel> Template
    //    {
    //        get
    //        {
    //            return new List<NotificationSettingsViewModel>
    //                   {
    //                       new NotificationSettingsViewModel {}
    //                   };
    //        }
    //    }

    //    public NotificationsGroupViewModel()
    //    {
    //        Notifications = new List<NotificationSettingsViewModel>();
    //    }
    //}

    public class GuestsEmail
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
