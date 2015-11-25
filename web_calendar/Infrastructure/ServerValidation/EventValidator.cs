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
            return 0;
        }
    }
}
