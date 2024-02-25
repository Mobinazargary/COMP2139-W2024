using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group_170.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string Amenities { get; set; }

        

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PricePerNight { get; set; }

        public ICollection<HotelReservation> HotelReservations { get; set; } = new List<HotelReservation>();

    }
}   



   

