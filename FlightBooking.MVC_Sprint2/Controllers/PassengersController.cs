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
    public class PassengersController : Controller
    {
        private ApplicationDbContext context;

        public PassengersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: PassengerController
        public ActionResult Index(int id,string search)
        {
            //int flghtId = 1;
            //context.Flights.FirstOrDefault(f => f.Id == flghtId);
            //TempData["FlightId"] = id;
            if (TempData["FlightId"] != null)
            {
                TempData["FlightId"] = TempData["FlightId"];
            }
            if (id != 0)
            {
                TempData["FlightId"] = id;
            }
            if (!string.IsNullOrEmpty(search))
            {
                var list = (from e in context.Passengers
                            where e.IsActive == true && (e.FirstName.Contains(search) || e.LastName.Contains(search))
                            select e).ToList();
                return View(list);
            }
            else
            {
                var list = context.Passengers.Where(p => p.IsActive == true).ToList();
                return View(list);
            }
        }

        // GET: PassengerController/Details/5
        public ActionResult Details(int id)
        {
            TempData["FlightId"] = TempData["FlightId"];
            // var flightid = route
            var passenger = context.Passengers.Find(id);
            if (passenger == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var passengerVm = new PassengerViewModel
            {
                Id = passenger.Id,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                DateOfBirth = passenger.DateOfBirth,
                ContactNo = passenger.ContactNo,
                Email = passenger.Email,
                IsActive = passenger.IsActive,
                Gender = passenger.Gender
            };
            return View(passengerVm);
        }

        // GET: PassengerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PassengerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Passenger passenger)
        {
            TempData["FlightId"] = TempData["FlightId"];
            try
            {
                if (ModelState.IsValid)
                {
                    var existingFlight = context.Passengers.FirstOrDefault(p => p.FirstName == passenger.FirstName && p.LastName == passenger.LastName);
                    if (existingFlight != null)
                    {
                        ModelState.AddModelError("", "Flight already exists");
                        return View(passenger);
                    }


                    context.Passengers.Add(passenger);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Add Passenger failed");
                        return View();
                    }
                }
                return View(passenger);
            }
            catch
            {
                ModelState.AddModelError("", "Add Passenger failed");
                return View();
            }
        }

        // GET: PassengerController/Edit/5
        public ActionResult Edit(int id)
        {
            var passenger = context.Passengers.Find(id);
            if (passenger == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        // POST: PassengerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Passenger passenger)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    context.Passengers.Update(passenger);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "update passenger failed");
                return View(passenger);

            }
            ModelState.AddModelError("", "update passenger failed");
            return View(passenger);
        }

        // GET: PassengerController/Delete/5
        public ActionResult Delete(int id)
        {
            var passenger = context.Passengers.Find(id);
            if (passenger == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        // POST: PassengerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var passenger = context.Passengers.Find(id);
            try
            {

                if (passenger == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    context.Passengers.Remove(passenger);
                    int Affectedrecords = context.SaveChanges();
                    if (Affectedrecords > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {

                ModelState.AddModelError("", "Delete passenger failed");
                return View(passenger);
            }
            ModelState.AddModelError("", "Delete passenger failed");
            return View(passenger);
        }
    }
}
