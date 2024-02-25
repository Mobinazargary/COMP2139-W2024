using GBC_Travel_Group_170.Data;
using GBC_Travel_Group_170.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GBC_Travel_Group_170.Controllers
{
    public class FlightReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for ApplicationDbContext
        public FlightReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Reserve(int reserveId)
        {
            // Initialize a new FlightReservation with the FlightID to ensure it's correctly passed to the form
            var flightReservation = new FlightReservation { FlightId = reserveId };
            return View(flightReservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(FlightReservation flightReservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the reservation to the database context
                    _context.FlightReservations.Add(flightReservation);
                    await _context.SaveChangesAsync();

                    // Redirect to the ReservationDetails action with the reservation ID
                    return RedirectToAction("ReservationDetails", new { id = flightReservation.ReservationID });
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                    // Optionally, redirect to an error page
                    return RedirectToAction("Error");
                }
            }
            // If the model state is invalid, return the view with validation errors
            return View(flightReservation);
        }

        public IActionResult ReservationDetails(int id)
        {
            // Retrieve the FlightReservation with eager loading of the associated Flight
            var reservation = _context.FlightReservations
                .Include(r => r.Flight) // Eagerly load the Flight entity
                .FirstOrDefault(r => r.ReservationID == id);

            if (reservation == null)
            {
                return NotFound(); // Handle case where reservation is not found
            }

            return View(reservation);
        }
    }
}
