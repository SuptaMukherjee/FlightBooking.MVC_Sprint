using FlightBooking.MVC_Sprint2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Dtos
{
    public class TicketDto
    {
       // public Payment Payment { get; set; }
        public Ticket Ticket { get; set; }
        public List<TicketBooking> Bookings { get; set; }
    }
}
