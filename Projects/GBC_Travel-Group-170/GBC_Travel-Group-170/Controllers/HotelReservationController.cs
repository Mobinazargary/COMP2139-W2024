using GBC_Travel_Group_170.Data;
using GBC_Travel_Group_170.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GBC_Travel_Group_170.Controllers
{
    public class HotelReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for ApplicationDbContext
        public HotelReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Reserve(int reserveId)
        {
            // Initialize a new ProjectTask with the ProjectId to ensure it's correctly passed to the form
            var HotelReservation = new HotelReservation { HotelID = reserveId };
            return View(HotelReservation);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(HotelReservation hotelReservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the reservation to the database context
                    _context.HotelReservations.Add(hotelReservation);
                    await _context.SaveChangesAsync();

                    // Redirect to the ReservationDetails action with the reservation ID
                    return RedirectToAction("ReservationDetails", new { id = hotelReservation.ReservationID });
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
            return View(hotelReservation);
        }

        public IActionResult ReservationDetails(int id)
        {
            // Retrieve the HotelReservation with eager loading of the associated Hotel
            var reservation = _context.HotelReservations
                .Include(r => r.Hotel) // Eagerly load the Hotel entity
                .FirstOrDefault(r => r.ReservationID == id);

            if (reservation == null)
            {
                return NotFound(); // Handle case where reservation is not found
            }

            return View(reservation);
        }

  
    }
}