
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
    
public partial class Calendar
{

    public Calendar()
    {

        this.CalendarEventsCollection = new HashSet<CalendarEvent>();

    }


    public int Id { get; set; }

    public string Name { get; set; }

    public string Text { get; set; }

    public Nullable<int> CalendarColor { get; set; }

    public Nullable<int> CalendarEvents { get; set; }

    public string Visibility { get; set; }

    public Nullable<byte> TimeZone { get; set; }

    public string UserId { get; set; }

    public Nullable<int> NotificationTypeId { get; set; }



    public virtual NotificationType NotificationType { get; set; }

    public virtual ICollection<CalendarEvent> CalendarEventsCollection { get; set; }

}

}
