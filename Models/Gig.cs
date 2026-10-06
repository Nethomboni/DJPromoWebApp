using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DJPromoWebApp.Models
{
    public class Gig
    {
        [Key]
        public int GigID { get; set; }

        [ForeignKey("DJ")]
        [Display(Name = "DJ")]
        public int DJID { get; set; }

        [ForeignKey("Venue")]
        [Display(Name = "Venue")]
        public int VenueID { get; set; }

        public DJ DJ { get; set; }
        public Venue Venue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
    }
}
