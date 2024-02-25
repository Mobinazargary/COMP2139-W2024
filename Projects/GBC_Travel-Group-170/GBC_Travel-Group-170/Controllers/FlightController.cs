using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBC_Travel_Group_170.Data;
using GBC_Travel_Group_170.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GBC_Travel_Group_170.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for ApplicationDbContext
        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpGet]
        public IActionResult Index()
        {
            var flights = _context.Flights.ToList();
            return View(flights);
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Flights.Update(flight);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }



        // Confirms the deletion of a project
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }



        // Processes the deletion of a project
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int flightId)
        {
            var flight = _context.Flights.Find(flightId);

            if (flight != null)
            {

                _context.Flights.Remove(flight);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirect to the list of projects
            }

            return NotFound();
        }

        [HttpGet]
        [HttpGet]
        public IActionResult Search(string airline, string departureCity, string arrivalCity, string departureDate, string arrivalDate)
        {
            var flights = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(airline))
            {
                flights = flights.Where(f => f.Airline.Contains(airline));
            }

            if (!string.IsNullOrEmpty(departureCity))
            {
                flights = flights.Where(f => f.DepartureCity.Contains(departureCity));
            }

            if (!string.IsNullOrEmpty(arrivalCity))
            {
                flights = flights.Where(f => f.ArrivalCity.Contains(arrivalCity));
            }

            if (!string.IsNullOrEmpty(departureDate))
            {
                DateTime departure = DateTime.Parse(departureDate);
                flights = flights.Where(f => f.DepartureDate.Date == departure.Date);
            }

            if (!string.IsNullOrEmpty(arrivalDate))
            {
                DateTime arrival = DateTime.Parse(arrivalDate);
                flights = flights.Where(f => f.ArrivalDate.Date == arrival.Date);
            }

            var searchResults = flights.ToList();
            return View("Index", searchResults);
        }
    }
}
        