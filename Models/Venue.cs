using System.ComponentModel.DataAnnotations;

namespace DJPromoWebApp.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        public ICollection<Gig> Gigs { get; set; }
    }
}
