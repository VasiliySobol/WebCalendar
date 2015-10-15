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
    
    public partial class User
    {
        public User()
        {
            this.calendars_collection = new HashSet<Calendar>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Nullable<int> calendars { get; set; }
        public Nullable<int> settings { get; set; }
        public string role { get; set; }
    
        public virtual ICollection<Calendar> calendars_collection { get; set; }
        public virtual Settings settings_reference { get; set; }
    }
}
