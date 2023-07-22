using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class CustomUser : IdentityUser
    {
        [Display(Name = "Date of Registering")]
        public DateTime DateOfJoining { get; set; } = DateTime.Now;
    }
}
