using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class Ticket
    {
        [Display(Name = "Ticket Id")]
        public int Id { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public bool IsCanceled { get; set; } = false;
        public string AgentId { get; set; }
        public virtual CustomUser Agent { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
