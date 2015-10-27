using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeLogging.Models
{
    public class TimeLogViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }
        public bool Billable { get; set; }
    }
}