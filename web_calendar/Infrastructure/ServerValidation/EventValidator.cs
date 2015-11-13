using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_calendar.BL.ViewModels;

namespace web_calendar.BL.ServerValidation
{
    public static class EventValidator
    {
        public static bool Validate(CreateEventViewModel eventVM)
        {
            bool ifValid = (eventVM.Name != null);
            if (!ifValid) return false;
            ifValid &= (eventVM.TimeBegin != null);
            if (!ifValid) return false;
            ifValid &= (eventVM.TimeBegin.CompareTo(DateTime.Now) > 0);
            if (eventVM.TimeEnd != null)
                ifValid &= (eventVM.TimeBegin.CompareTo(eventVM.TimeBegin) > 0);
            // add other validation (visibility, etc.)
            return ifValid;
        }
    }
}
