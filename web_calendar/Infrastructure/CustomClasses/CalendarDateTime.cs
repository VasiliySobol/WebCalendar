using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.CustomClasses
{
    class CalendarDateTime
    {
        DateTime dateTime { get; set; }


        public CalendarDateTime()
        {
            dateTime = DateTime.Now;
        }
    }
}
