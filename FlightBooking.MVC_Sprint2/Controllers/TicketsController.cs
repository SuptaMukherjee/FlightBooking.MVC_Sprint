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
    public class TicketsController : Controller
    {
        private ApplicationDbContext context;
        private IConfiguration config;
        private ILogger<Ticket> logger;

        public TicketsController(ApplicationDbContext context, IConfiguration config, ILogger<Ticket> logger)
        {
            this.context = context;
            this.config = config;
            this.logger = logger;   
        }
        // GET: TicketsController
        public ActionResult Index()
        {
            try
            {
                //var passenger = context.Passengers.FirstOrDefault(p => p.Id == p.Id);
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var tickets = context.Tickets.Include(o => o.Payment).Where(o => o.AgentId == userId).ToList();
                return View(tickets);
            }
            catch(SystemException ex)
            {
                throw new Exception(ex.Message);
                return View();
            }
        }

        // GET: TicketsController/Details/5
        public ActionResult Details(int id)
        {
            var ticket = context.Tickets.Include(o => o.Agent).Include(o=>o.Payment)
                .FirstOrDefault(o => o.Id == id);
            if (ticket == null)
            {
                return RedirectToAction(nameof(Index));
            }
           
           var  tickets = context.TicketDetails
                .Include(i => i.Passenger).Include(i => i.Flight)
                .Where(i => i.TicketId == id);
            ViewBag.Ticket = ticket;

            return View(tickets);
        }
        public ActionResult UPI()
        {
            return View();
        }
        public ActionResult List()
        {

            return View();
        }
        public ActionResult PayMode()
        {
            return View();
        }
        public ActionResult End()
        {
            return View();
        }
        // GET: TicketsController/Create
        public ActionResult Create()
        {
            
                var booking = SessionHelper.GetObjectFromJson<List<TicketBooking>>
                  (HttpContext.Session, SessionHelper.TicketKey);
                var pay = booking.Find(i => i.PaymentId == i.PaymentId);
                var newTicket = new Ticket
                {
                    PaymentId = pay.PaymentId,
                    AgentId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                context.Tickets.Add(newTicket);
                context.SaveChanges();

                foreach (var item in booking)
                {
                    if (item.SeatType == "Economic Class")
                    {


                        var ticketItem = new TicketDetail
                        {
                            TicketId = newTicket.Id,
                            
                            Price = item.Flight.EconomicClassPrice,
                            FlightId = item.Flight.Id,
                            PassengerId = item.Passenger.Id
                        };

                        context.TicketDetails.Add(ticketItem);
                    }
                    if (item.SeatType == "Business Class")
                    {
                        var ticketItem = new TicketDetail
                        {

                            TicketId = newTicket.Id,
                            Price = item.Flight.BusinessClassPrice, 
                            FlightId = item.Flight.Id,
                            PassengerId = item.Passenger.Id
                        };
                        context.TicketDetails.Add(ticketItem);
                    }

                    
                }
                
                context.SaveChanges();
                SessionHelper.ClearSession(HttpContext.Session, SessionHelper.TicketKey);
            var ticketDto = new TicketDto
            {
                Ticket = newTicket,
                Bookings = booking,

            };
            try
            {
                BlobHelper.UploadBlob(config, ticketDto);
            }
            catch (Exception ex)
            {
                logger.LogInformation(JsonConvert.SerializeObject(ex));
            }
            return RedirectToAction(nameof(Create), "Payments");
            
           
        }

        // POST: TicketsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayCreate(Payment payment, string paymentMode, string searchBy)
        {
            if (paymentMode == "UPI")
            {
                try
                {
                    return RedirectToAction(nameof(UPI));
                }
                catch
                {
                    return View();
                }
            }
            else if (paymentMode =="NetBanking")
            {
                return RedirectToAction(nameof(List));
            }   
            else
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
                    }
                        var pay = new Payment
                        {
                            UPI = payment.UPI,
                            UPI_ID = payment.UPI_ID,
                            PaymentDate = payment.PaymentDate,
                            BankName = payment.BankName,
                            CardNo = payment.CardNo,
                            Cvv = payment.Cvv,
                            PaymentMode = PaymentMode.NetBanking,
                            TotalAmount = total,
                            AgentId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                        };
                        context.Payments.Add(pay);
                        context.SaveChanges();
                        foreach (var items in booking)
                        {
                            items.PaymentId = pay.Id;
                        }
                    
                    SessionHelper.SetObjectAsJson(HttpContext.Session,
                            SessionHelper.TicketKey, booking);

                    return RedirectToAction(nameof(Create));
                }
                catch(Exception ex)
                {

                    return View();
                }
            }
            
        }

        // GET: TicketsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketsController/Edit/5
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

        // GET: TicketsController/Delete/5
        public ActionResult Delete(int id)
        {
            var payment = context.Tickets.Find(id);
            if (payment == null)
            {
                return RedirectToAction(nameof(Index));
            }
            payment.IsCanceled = true;
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: TicketsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
