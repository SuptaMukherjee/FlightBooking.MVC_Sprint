using FlightBooking.MVC_Sprint2.Data;
using FlightBooking.MVC_Sprint2.Dtos;
using FlightBooking.MVC_Sprint2.Helpers;
using FlightBooking.MVC_Sprint2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext context;
        private IConfiguration config;
        private ILogger<Payment> logger;

        public PaymentsController(ApplicationDbContext context, IConfiguration config, ILogger<Payment> logger)
        {
            this.context = context;
            this.config = config;
            this.logger = logger;
        }
        // GET: Payments
        public ActionResult Index()
        {
            var list = context.Payments.Where(p => p.IsActive == true).ToList();
            return View(list);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int id)
        {
            var payment = context.Payments.Include(o => o.Agent).FirstOrDefault(o => o.Id == id);
            //var payment = context.Payments.FirstOrDefault(o=>o.Id == id);
            //var payment=context.Payments
            if (payment == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(payment); 
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment payment)
        {
            try
            {
                var booking = SessionHelper.GetObjectFromJson<List<TicketBooking>>
                    (HttpContext.Session, SessionHelper.TicketKey);
                
                foreach (var item in booking)
                {

                    double price = item.Flight.BusinessClassPrice;
                    if (item.SeatType == "Economic Class")
                    {
                        price = item.Flight.EconomicClassPrice;
                    }
                }
                double total = 0;
                foreach (var item in booking)
                {

                
                    if (item.SeatType == "Economic Class")
                    {
                        total += item.Flight.EconomicClassPrice;
                    }
                    else
                    {
                        total += item.Flight.BusinessClassPrice;
                    }

                    var pay = new Payment
                    {
                        PaymentDate = payment.PaymentDate,
                        BankName = payment.BankName,
                        CardNo = payment.CardNo,
                        Cvv = payment.Cvv,
                        AccountNumber = payment.AccountNumber,
                        IFSCcode = payment.IFSCcode,
                       // Ticket =payment.Ticket,
                        TotalAmount = total,
                        AgentId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                    };
                    context.Payments.Add(pay);
                    
                }
                context.SaveChanges();
                SessionHelper.ClearSession(HttpContext.Session, SessionHelper.TicketKey);
                //var ticketDto = new TicketDto
                //{
                    
                //    Bookings = booking,
                    
                //};
                //try
                //{
                //    BlobHelper.UploadBlob(config, ticketDto);
                //}
                //catch (Exception ex)
                //{
                //    logger.LogInformation(JsonConvert.SerializeObject(ex));
                //}
                //return RedirectToAction(nameof(Create), "Payments");
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int id)
        {
            var passenger = context.Payments.Find(id);
            if (passenger == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        // POST: Payments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var passenger = context.Payments.Find(id);
            try
            {

                if (passenger == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    context.Payments.Remove(passenger);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {

                ModelState.AddModelError("", "Delete payment failed");
                return View(passenger);
            }
            ModelState.AddModelError("", "Delete payment failed");
            return View(passenger);
        }
    }
}
