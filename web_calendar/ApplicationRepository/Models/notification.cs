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
    
    public partial class Notification
    {
        public int Id { get; set; }
        public Nullable<int> NotificationType { get; set; }
        public Nullable<int> EventId { get; set; }
    
        public virtual CalendarEvent CalendarEvent { get; set; }
        public virtual NotificationType NotificationTypeReference { get; set; }
    }
}
