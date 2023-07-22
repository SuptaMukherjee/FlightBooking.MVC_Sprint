using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class Passenger : IValidatableObject
    {

        [Display(Name = "Passenger ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Moile must be 10 digits")]
        [Display(Name = "Passenger Contact Number")]
        public long ContactNo { get; set; }

        public Gender Gender { get; set; }




        //    public virtual ICollection<TicketBooking> TicketBookings { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (DateOfBirth > DateTime.Today)
                {
                    yield return new ValidationResult("Date of Birth Date must be in the past", new string[] { nameof(DateOfBirth) });
                }
            }
    }
}
namespace FlightBooking.MVC_Sprint2
{
    public enum Gender
    {
        Male = 1,
        Female,
        Others
    }
}