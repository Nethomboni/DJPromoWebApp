using DJPromoWebApp.Models;

namespace DJPromoWebApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(DJContext context)
        {
            if (context.DJ.Any()) return;

            var djs = new[]
            {
                new DJ { StageName = "DJ Sunset" },
                new DJ { StageName = "DJ Kgosi" },
                new DJ { StageName = "DJ Nova" }
            };
            context.DJ.AddRange(djs);

            var venues = new[]
            {
                new Venue { Name = "The Grand Hall", Address = "12 Main Street, Johannesburg" },
                new Venue { Name = "Skyline Rooftop", Address = "88 Rivonia Road, Sandton" }
            };
            context.Venue.AddRange(venues);
            context.SaveChanges();

            context.Gig.AddRange(
                new Gig { DJID = djs[0].DJID, VenueID = venues[0].VenueID, Date = DateTime.Today.AddDays(7),  Time = new TimeSpan(20, 0, 0) },
                new Gig { DJID = djs[1].DJID, VenueID = venues[1].VenueID, Date = DateTime.Today.AddDays(14), Time = new TimeSpan(21, 30, 0) }
            );
            context.SaveChanges();
        }
    }
}
