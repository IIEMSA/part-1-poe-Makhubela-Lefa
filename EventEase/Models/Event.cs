using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EventEase.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }         // Primary Key
        public string? EventName { get; set; }   
        public DateTime EventDate { get; set; } 
        public string? Description { get; set; } // Event description (optional)
        public int VenueID { get; set; }        // Foreign key for Venue
       
        // Navigation Property
        [ForeignKey("VenueID")]
        public required Venue Venue { get; set; }        // Reference to the Venue hosting this event
    }
}
