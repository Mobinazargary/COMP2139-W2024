using GBC_Travel_Group_170.Data;
using GBC_Travel_Group_170.Models;
using Microsoft.AspNetCore.Mvc;

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
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for ApplicationDbContext
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpGet]
        public IActionResult Index()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var hotel = _context.Hotels
                               .FirstOrDefault(h => h.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(p => p.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the hotel entity in the database
                    _context.Hotels.Update(hotel);
                    _context.SaveChanges();

                    // Redirect to the index page after successful update
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    ModelState.AddModelError("", "An error occurred while updating the hotel.");
                    return View(hotel); // Return the view with the model to display the error
                }
            }
            else
            {
                // If ModelState is not valid, there are errors
                // You can iterate over ModelState.Errors to inspect the errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Log or handle each error as needed
                    Console.WriteLine(error.ErrorMessage);
                }

                // Return the view with the model to display validation errors
                return View(hotel);
            }
        }




        // Confirms the deletion of a project
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(p => p.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }



        // Processes the deletion of a project
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int hotelId)
        {
            var hotel = _context.Hotels.Find(hotelId);

            if (hotel != null)
            {

                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirect to the list of projects
            }

            return NotFound();
        }
        public IActionResult Search(string name, string location, string startdate, string enddate, string type)
        {
            var hotels = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                hotels = hotels.Where(f => f.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(location))
            {
                hotels = hotels.Where(f => f.Location.Contains(location));
            }

            if (!string.IsNullOrEmpty(type))
            {
                hotels = hotels.Where(f => f.Type.Contains(type));
            }

            if (!string.IsNullOrEmpty(startdate))
            {
                DateTime departure = DateTime.Parse(startdate);
                hotels = hotels.Where(f => f.StartDate == departure.Date);
            }

            if (!string.IsNullOrEmpty(enddate))
            {
                DateTime arrival = DateTime.Parse(enddate);
                hotels = hotels.Where(f => f.EndDate == arrival.Date);
            }

            var searchResults = hotels.ToList();
            return View("Index", searchResults);
        }


    }
}








