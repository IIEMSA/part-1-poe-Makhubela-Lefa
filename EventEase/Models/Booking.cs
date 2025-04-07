using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; } // Primary Key
        public int EventID { get; set; }  // Foreign key for Event
        public int VenueID { get; set; }  // Foreign key for Venue
        public DateTime BookingDate { get; set; }
      
        

        // Navigation Properties
        public Event LinkedEvent { get; set; } // Reference to Event
        public Venue Venue { get; set; }     // Reference to Venue
        
    }
}
