using GBC_Travel_Group_170.Data;
using GBC_Travel_Group_170.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GBC_Travel_Group_170.Controllers
{
    public class CarRentalReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for ApplicationDbContext
        public CarRentalReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Reserve(int reserveId)
        {
            // Initialize a new CarRentalReservation with the CarRentalId to ensure it's correctly passed to the form
            var carRentalReservation = new CarRentalReservation { CarRentalId = reserveId };
            return View(carRentalReservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(CarRentalReservation carRentalReservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the reservation to the database context
                    _context.CarRentalReservations.Add(carRentalReservation);
                    await _context.SaveChangesAsync();

                    // Redirect to the ReservationDetails action with the reservation ID
                    return RedirectToAction("ReservationDetails", new { id = carRentalReservation.ReservationID });
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
            return View(carRentalReservation);
        }

        public IActionResult ReservationDetails(int id)
        {
            // Retrieve the CarRentalReservation with eager loading of the associated CarRental
            var reservation = _context.CarRentalReservations
                .Include(r => r.CarRental) // Eagerly load the CarRental entity
                .FirstOrDefault(r => r.ReservationID == id);

            if (reservation == null)
            {
                return NotFound(); // Handle case where reservation is not found
            }

            return View(reservation);
        }
    }
}


