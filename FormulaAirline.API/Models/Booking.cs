using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaAirline.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;

        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public int status { get; set; }
        
    }
}