using FlightBooking.MVC_Sprint2.Data;
using FlightBooking.MVC_Sprint2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Controllers
{
    public class FlightsController : Controller
    {
        private ApplicationDbContext context;

        public FlightsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: FlightsController
        //public ActionResult Index()
        //{
        //    var list = context.Flights.Where(p => p.IsActive == true).ToList();
        //    return View(list);
        //}
        public ActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var list = (from e in context.Flights
                            where e.IsActive == true && (e.FlightName.Contains(search))
                            select e).ToList();
                return View(list);
            }
            else
            {
                var list = context.Flights.Where(p => p.IsActive == true).ToList();
                return View(list);
            }
        }

        // GET: FlightsController/Details/5
        public ActionResult Details(int id)
        {
            var flight = context.Flights.Find(id);
            if (flight == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var flightVM = new FlightVIewModel
            {
                Id = flight.Id,
                FlightName = flight.FlightName,
                ToLocation = flight.ToLocation,
                FromLocation = flight.FromLocation,
                DepartureDate =flight.DepartureDate,
                DepartureTime =flight.DepartureTime,
                ArrivalDate = flight.ArrivalDate,
                ArrivalTime =flight.ArrivalTime,
                BusinessSeatCapacity = flight.BusinessSeatCapacity,
                EconomicSeatCapacity = flight.EconomicSeatCapacity,
                BusinessClassPrice = flight.BusinessClassPrice,
                EconomicClassPrice = flight.EconomicClassPrice,
                FlightStatus = flight.FlightStatus,
                IsActive = flight.IsActive
            };
            return View(flightVM);
        }

        // GET: FlightsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight flight)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingFlight = context.Flights.FirstOrDefault(p => p.FromLocation == flight.FromLocation && p.ToLocation == flight.ToLocation);
                    if (existingFlight != null)
                    {
                        ModelState.AddModelError("", "Flight already exists");
                        return View(flight);
                    }


                    context.Flights.Add(flight);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Add Flight failed");
                        return View();
                    }
                }
                return View(flight);
            }
            catch
            {
                ModelState.AddModelError("", "Add Flight failed");
                return View();
            }
        }

        // GET: FlightsController/Edit/5
        public ActionResult Edit(int id)
        {
            var flight = context.Flights.Find(id);
            if (flight == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // POST: FlightsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    context.Flights.Update(flight);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "update flight failed");
                return View(flight);

            }
            ModelState.AddModelError("", "update flight failed");
            return View(flight);
        }

        // GET: FlightsController/Delete/5
        public ActionResult Delete(int id)
        {
            var flight = context.Flights.Find(id);
            if (flight == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // POST: FlightsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var flight = context.Flights.Find(id);
            try
            {

                if (flight == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    context.Flights.Remove(flight);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {

                ModelState.AddModelError("", "Delete product failed");
                return View(flight);
            }
            ModelState.AddModelError("", "Delete product failed");
            return View(flight);
        }
    }
}

