//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web_calendar.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Repeatable
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public Nullable<int> RepeatCount { get; set; }
        public Nullable<int> EventId { get; set; }
        public string DaysOfWeek { get; set; }
        public Nullable<System.TimeSpan> TimeOfDay { get; set; }
        public Nullable<System.DateTime> DayOfYear { get; set; }
        public string DayOfMonth { get; set; }
    
        public virtual CalendarEvent CalendarEvent { get; set; }
    }
}
