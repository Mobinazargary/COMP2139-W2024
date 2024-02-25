using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_170.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Airline name cannot exceed 100 characters.")]
        public string Airline { get; set; }

        [Required]
        [Display(Name = "Departure City")]
        public string DepartureCity { get; set; }

        [Required]
        [Display(Name = "Arrival City")]
        public string ArrivalCity { get; set; }

        [Required]
        
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public ICollection<FlightReservation> FlightReservations { get; set; } = new List<FlightReservation>();

        // Optional: Relationship to bookings or users, if needed
        // public List<Booking> Bookings { get; set; }
    }
}