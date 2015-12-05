using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.ViewModels
{
    public class EmailModel
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
