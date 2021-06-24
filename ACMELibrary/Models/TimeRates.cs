using System;
using System.Collections.Generic;
using System.Text;

namespace ACMELibrary.Models
{

    public class TimeRates
    {
        //Do not require DataAnnotations, because this solution won't enter data
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Amount { get; set; }
    }
}
