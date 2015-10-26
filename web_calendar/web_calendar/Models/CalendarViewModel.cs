﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace web_calendar.Models
{
    public class CalendarViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Text { get; set; }

        public  Color color { get; set; }

        bool _visibility { get; set; }

        string _timezone {get; set; }

        int _userId {get; set; }
    }
}