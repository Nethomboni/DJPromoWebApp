using System.ComponentModel.DataAnnotations;

namespace DJPromoWebApp.Models
{
    public class DJ
    {
        [Key]
        public int DJID { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Stage Name")]
        public string StageName { get; set; }

        public ICollection<Gig> Gigs { get; set; }
    }
}
