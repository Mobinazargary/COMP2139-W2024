using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBC_Travel_Group_170.Data;
using GBC_Travel_Group_170.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_170.Controllers
{
    public class CarRentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for ApplicationDbContext
        public CarRentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var carRentals = _context.CarRentals.ToList();
            return View(carRentals);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var carRental = _context.CarRentals.FirstOrDefault(p => p.CarRentalId== id);
            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _context.CarRentals.Add(carRental);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carRental);
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var carRental = _context.CarRentals.FirstOrDefault(p => p.CarRentalId == id);
            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _context.CarRentals.Update(carRental);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carRental);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var carRental = _context.CarRentals.FirstOrDefault(p => p.CarRentalId == id);
            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int carRentalId)
        {
            var carRental = _context.CarRentals.Find(carRentalId);

            if (carRental != null)
            {
                _context.CarRentals.Remove(carRental);
                _context.SaveChanges(); // Ensure that changes are saved to the database
                return RedirectToAction(nameof(Index)); // Redirect to the list of car rentals
            }

            return NotFound(); // Handle case where car rental is not found
        }


        public IActionResult Search(string location, string startDate, string endDate, string brand,string model, string searchYear)
        {
            var cars = _context.CarRentals.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                cars = cars.Where(c => c.Location.Contains(location));
            }

            // Assuming startDate and endDate are DateTime types
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);
                cars = cars.Where(c => c.AvailabilityStartDate >= start && c.AvailabilityEndDate <= end);
            }

            if (!string.IsNullOrEmpty(searchYear))
            {
                int year = int.Parse(searchYear);
                cars = cars.Where(c => c.Year == year);
            }



            if (!string.IsNullOrEmpty(brand))
            {
                cars = cars.Where(c => c.Brand.Contains(brand));
            }


            if (!string.IsNullOrEmpty(model))
            {
                cars = cars.Where(c => c.Model.Contains(model));
            }

            var searchResults = cars.ToList();
            return View("Index", searchResults); // Return the Index view with search results
        }


    }
}
