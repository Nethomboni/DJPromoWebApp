using DJPromoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DJPromoWebApp.Data
{
    public class DJContext : DbContext
    {
        public DJContext(DbContextOptions<DJContext> options) : base(options)
        {
        }

        public DbSet<Gig> Gig { get; set; }
        public DbSet<DJ> DJ { get; set; }
        public DbSet<Venue> Venue { get; set; }
    }
}
