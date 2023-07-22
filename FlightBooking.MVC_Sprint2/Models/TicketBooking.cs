using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class TicketBooking 
    {
        
        public virtual Flight Flight { get; set; }
        
        public virtual Passenger Passenger { get; set; }
        [Display(Name = "Seat Type")]
        [Required]
        public string SeatType { get; set; }
        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
    }
}
