using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GBC_Travel_Group_170.Models;

namespace GBC_Travel_Group_170
{
    public class CarRental
    {
        [Key]
        public int CarRentalId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Brand name cannot exceed 50 characters.")]
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Model name cannot exceed 50 characters.")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Please enter a valid year.")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Availability Start Date")]
        [DataType(DataType.Date)]
        public DateTime AvailabilityStartDate { get; set; } // Start date of availability

        [Display(Name = "Availability End Date")]
        [DataType(DataType.Date)]
        public DateTime AvailabilityEndDate { get; set; } // End date of availability

        [Required]
        [StringLength(100, ErrorMessage = "Location name cannot exceed 100 characters.")]
        [Display(Name = "Location")]
        public string Location { get; set; } // Location where the car is available
        public ICollection<CarRentalReservation> CarRentalReservations { get; set; } = new List<CarRentalReservation>();

    }
}
