using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EventEase.Models
{
    public class Venue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueID { get; set; } //Primary key
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ImageURL { get; set; }

        // Navigation Property for bookings
        public List<Booking> Bookings { get; set; } = new();
    }
}
