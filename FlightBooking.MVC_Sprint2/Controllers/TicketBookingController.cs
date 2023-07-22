using FlightBooking.MVC_Sprint2.Data;
using FlightBooking.MVC_Sprint2.Helpers;
using FlightBooking.MVC_Sprint2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Controllers
{
    public class TicketBookingController : Controller
    {
        private ApplicationDbContext context;

        public TicketBookingController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: TicketBookingController
        public ActionResult Index()
        {
            TempData["FlightId"] = TempData["FlightId"];
            var ticketbook = SessionHelper.GetObjectFromJson<List<TicketBooking>>
                (HttpContext.Session, SessionHelper.TicketKey);

            /*  ViewBag.Total = ticketbook == null ? 0 :
                  ticketbook.Sum(i => i.Flight.BusinessClassPrice * i.HeadCount); */
            return View(ticketbook);

        }

        // GET: TicketBookingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketBookingController/Create
        //public ActionResult Create()
        //{

        //}

        // POST: TicketBookingController/Create
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int id, string seatType)
        {
            if (ModelState.IsValid)
            {
                var sessionObject = SessionHelper.GetObjectFromJson<List<TicketBooking>>
                   (HttpContext.Session, SessionHelper.TicketKey);
                TempData["FlightId"] = TempData["FlightId"];
                int flightId = (int)TempData["FlightId"];
                if (sessionObject == null)
                {


                    var list = new List<TicketBooking>
                    {  


                    new TicketBooking
                    {
                        Flight = context.Flights.Find(flightId),
                        Passenger = context.Passengers.Find(id),
                        SeatType = seatType,
                    }

                    };
                    SessionHelper.SetObjectAsJson(HttpContext.Session, SessionHelper.TicketKey, list);


                }
                else
                {
                    var booking = sessionObject.Find(i => i.Passenger.Id == id);
                    //var flight= sessionObject.Find(i => i.Flight.Id == flightId);
                    if (booking != null)
                    {

                        sessionObject[sessionObject.IndexOf(booking)].SeatType = seatType;


                    }
                    else
                    {
                        sessionObject.Add(new TicketBooking
                        {
                            //  Flight = context.Flights.Find(id),
                            Flight = context.Flights.Find(flightId),
                            Passenger = context.Passengers.Find(id),
                            SeatType = seatType,

                        });

                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, SessionHelper.TicketKey, sessionObject);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Invalid Input or Blank Input");
                return View();
            }
        }

        // GET: TicketBookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TicketBookingController/Edit/5
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

        // GET: TicketBookingController/Delete/5
        public ActionResult Delete(int id)
        {
            List<TicketBooking> book = SessionHelper.GetObjectFromJson<List<TicketBooking>>
                 (HttpContext.Session, SessionHelper.TicketKey);
            var item = book.Find(i => i.Passenger.Id == id);
            book.Remove(item);
            SessionHelper.SetObjectAsJson(HttpContext.Session, SessionHelper.TicketKey, book);
            return RedirectToAction(nameof(Index));
        }

        // POST: TicketBookingController/Delete/5
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
