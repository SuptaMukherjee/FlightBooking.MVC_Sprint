using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Models
{
    public class Payment
    {
        [Display(Name = "Payment Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Bank Name")]
        public BankName BankName { get; set; }
        //[ForeignKey("TicketId")]
        //public Ticket Ticket { get; set; }
       // public int TicketId { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        [Display(Name = "Account Number")]
        public long AccountNumber { get; set; }
        [Display(Name = "IFSC Code")]
        public string IFSCcode { get; set; }
        [Display(Name = "Card Number")]
        public long CardNo { get; set; }
        [Display(Name = "CVV")]
        public int Cvv { get; set; }
        [Display(Name = "Mode of Payment")]
        public PaymentMode PaymentMode { get; set; }
        public UPI UPI { get; set; }

        [RegularExpression("^[0-9]{10}$")]
        public string UPI_ID { get; set; }
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }
        public CustomUser Agent { get; set; }
        public string AgentId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

namespace FlightBooking.MVC_Sprint2
{
    public enum PaymentMode
    {
        UPI=1,
        PayNow,
        NetBanking
    }
}

namespace FlightBooking.MVC_Sprint2
{
    public enum BankName
    {
        SBI = 1,
        ICICI,
        HDFC,
        CANARA,
        UCO,
        AXIS,

    }
}

namespace FlightBooking.MVC_Sprint2
{
    public enum UPI
    {
        Phonepay = 1,
        GPay,
        Paytm
    }
}