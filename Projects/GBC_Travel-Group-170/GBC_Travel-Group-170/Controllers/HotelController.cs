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
            var hotel = _context.Hotels.ToList();
            return View(hotel);
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
                _context.Hotels.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }




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




        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int hotelID)
        {
            var hotel= _context.Hotels.Find(hotelID);

            if (hotel != null)
            {

                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirect to the list of projects
            }

            return NotFound();
        }




        public IActionResult Search(string name, string location, string startdate, string enddate,string type)
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

