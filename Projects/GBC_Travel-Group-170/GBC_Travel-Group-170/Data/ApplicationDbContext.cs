using GBC_Travel_Group_170.Models;

using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_170.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        public DbSet<Hotel> Hotels { get; set; }
        
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Flight> Flights { get; set; } // Use Room class here
       
        public DbSet<HotelReservation> HotelReservations { get; set; }
        public DbSet<FlightReservation> FlightReservations { get; set; }
        public DbSet<CarRentalReservation> CarRentalReservations { get; set; }
        

        internal object Search(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }

}
