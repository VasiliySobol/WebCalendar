//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApplicationRepository.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CalendarEvent
    {
        public CalendarEvent()
        {
            this.Guests = new HashSet<Guest>();
            this.Notifications = new HashSet<Notification>();
            this.Repeatables = new HashSet<Repeatable>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public System.DateTime TimeBegin { get; set; }
        public Nullable<System.DateTime> TimeEnd { get; set; }
        public string Visibility { get; set; }
        public Nullable<bool> AllDay { get; set; }
        public Nullable<int> CalendarId { get; set; }
    
        public virtual Calendar Calendar { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Repeatable> Repeatables { get; set; }
    }
}
