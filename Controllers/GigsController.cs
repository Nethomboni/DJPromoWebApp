using DJPromoWebApp.Data;
using DJPromoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DJPromoWebApp.Controllers
{
    public class GigsController : Controller
    {
        private readonly DJContext _context;
        public GigsController(DJContext context) => _context = context;

        private void LoadLists(int? djId = null, int? venueId = null)
        {
            ViewData["DJID"] = new SelectList(_context.DJ.OrderBy(d => d.StageName), "DJID", "StageName", djId);
            ViewData["VenueID"] = new SelectList(_context.Venue.OrderBy(v => v.Name), "VenueID", "Name", venueId);
        }

        public async Task<IActionResult> Index()
        {
            var gigs = _context.Gig
                .Include(g => g.DJ)
                .Include(g => g.Venue)
                .OrderBy(g => g.Date).ThenBy(g => g.Time);
            return View(await gigs.ToListAsync());
        }

        public IActionResult Create()
        {
            LoadLists();
            return View(new Gig { Date = DateTime.Today });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DJID,VenueID,Date,Time")] Gig gig)
        {
            if (!ModelState.IsValid)
            {
                LoadLists(gig.DJID, gig.VenueID);
                return View(gig);
            }
            _context.Add(gig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var gig = await _context.Gig.FindAsync(id);
            if (gig == null) return NotFound();
            LoadLists(gig.DJID, gig.VenueID);
            return View(gig);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GigID,DJID,VenueID,Date,Time")] Gig gig)
        {
            if (id != gig.GigID) return NotFound();
            if (!ModelState.IsValid)
            {
                LoadLists(gig.DJID, gig.VenueID);
                return View(gig);
            }
            try
            {
                _context.Update(gig);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Gig.Any(e => e.GigID == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var gig = await _context.Gig
                .Include(g => g.DJ).Include(g => g.Venue)
                .FirstOrDefaultAsync(m => m.GigID == id);
            if (gig == null) return NotFound();
            return View(gig);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gig = await _context.Gig.FindAsync(id);
            if (gig != null)
            {
                _context.Gig.Remove(gig);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
