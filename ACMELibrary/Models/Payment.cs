using System;
using System.Collections.Generic;
using System.Text;

namespace ACMELibrary.Models
{
    public class Payment
    {
        //Do not require DataAnnotations, because this solution won't enter data
        public string Day { get; set; }
        public string Schedule { get; set; }
        public float Hours { get; set; }
        public float Amount { get; set; }
    }
}
