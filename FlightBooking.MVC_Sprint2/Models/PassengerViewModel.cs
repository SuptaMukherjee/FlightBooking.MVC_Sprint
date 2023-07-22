using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class PassengerViewModel : Passenger 
    {
        public int FlightId { get; set; } 
        public virtual Flight Flight { get; set; }
        [Required]
        public string SeatType { get; set; }
    }
}
