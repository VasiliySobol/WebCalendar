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
        public static int Validate(CreateEventViewModel eventVM)
        {
            if (eventVM.Name == null) return 1;
            if (eventVM.TimeBegin == null) return 2;
            if (eventVM.Notifications != null && eventVM.Notifications.Count > 0)
            {
                foreach (NotificationViewModel item in eventVM.Notifications)
                {
                    if (!item.TimeBefore.HasValue) return 3;
                }
            }
            if (eventVM.Guests != null && eventVM.Guests.Count > 0)
            {
                foreach (GuestsEmail item in eventVM.Guests)
                {
                    if (item.Email == "") return 4;
                }
            }
            return 0;
        }
    }
}
