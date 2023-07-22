using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class FlightVIewModel : Flight
    {
        [Required]
        public string SeatType { get; set; }

        public Passenger passenger { get; set; }
        public int PassengerId { get; set; }
    }
}
