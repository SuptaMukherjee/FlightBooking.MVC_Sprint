using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class Flight
    {
        [Display(Name = "Flight ID")]
        public int Id { get; set; }
        [Display(Name = "Flight Name")]
        public string FlightName { get; set; }
        [Required]
        [Display(Name = "From Location")]
        public FromLocation FromLocation { get; set; }
        [Required]
        [Display(Name = "Destination")]
        public ToLocation ToLocation { get; set; }
        [Required]
        [Display(Name = "Arrival Date")]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }
        [Required]
        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]

        public DateTime DepartureDate { get; set; }
        [Required]
        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        public DateTime ArrivalTime { get; set; }
        [Display(Name = "Flight Status")]
        public FlightStatus FlightStatus { get; set; }
        public bool IsActive { get; set; } = true;
        [Display(Name = "Business Class Seat Capacity")]
        public int BusinessSeatCapacity { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Econmic Class should be there")]
        [Display(Name = "Economic Class Seat Capacity")]
        public int EconomicSeatCapacity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        [Display(Name = "Business Class Price")]
        public double BusinessClassPrice { get; set; }
        [Range(100, double.MaxValue, ErrorMessage = "Amount should be greater than 99")]
        [Display(Name = "Economic Class Price")]
        public double EconomicClassPrice { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArrivalDate < DateTime.Today)
            {
                yield return new ValidationResult("Arrival Date must not be  the past", new string[] { nameof(ArrivalDate) });
            }

        }
        public IEnumerable<ValidationResult> Validate1(ValidationContext validationContext)
        {
            if (DepartureDate > DateTime.Today)
            {
                yield return new ValidationResult("Departure Date must not be  the past", new string[] { nameof(DepartureDate) });
            }
        }
        public IEnumerable<ValidationResult> Validate2(ValidationContext validationContext)
        {
            if (ArrivalTime > DateTime.Today)
            {
                yield return new ValidationResult("Arrival Time must not be  the past", new string[] { nameof(ArrivalTime) });
            }
        }
        public IEnumerable<ValidationResult> Validate3(ValidationContext validationContext)
        {
            if (DepartureTime > DateTime.Today)
            {
                yield return new ValidationResult("Departure Date must not be  the past", new string[] { nameof(DepartureTime) });
            }
        }
    }
}
namespace FlightBooking.MVC_Sprint2
{
    public enum FromLocation
    {
        Kolkata = 1,
        Bangalore,
        Chennai,
        Delhi,
        Hyderbad,
        Pune,
        Coimbatore,
        Mumbai,
        Jaipur,
        Raipur,
        kashmir,
        Assam,
        Goa,
        Vissakahpattanam,
        Trivandrum,
        Patna,
        Ranchi
    }
}

namespace FlightBooking.MVC_Sprint2
{
    public enum ToLocation
    {
        Kolkata = 1,
        Bangalore,
        Chennai,
        Delhi,
        Hyderbad,
        Pune,
        Coimbatore,
        Mumbai,
        Jaipur,
        Raipur,
        kashmir,
        Assam,
        Goa,
        Vissakahpattanam,
        Trivandrum,
        Patna,
        Ranchi
    }
}
namespace FlightBooking.MVC_Sprint2
{
    public enum FlightStatus
    {
        Delayed = 1,
        Cancelled,
        Ontime,
        Boarding,
    }
}
