using DJPromoWebApp.Data;
using DJPromoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DJPromoWebApp.Controllers
{
    public class DJsController : Controller
    {
        private readonly DJContext _context;
        public DJsController(DJContext context) => _context = context;

        public async Task<IActionResult> Index() =>
            View(await _context.DJ.OrderBy(d => d.StageName).ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageName")] DJ dj)
        {
            if (!ModelState.IsValid) return View(dj);
            _context.Add(dj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var dj = await _context.DJ.FindAsync(id);
            if (dj == null) return NotFound();
            return View(dj);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DJID,StageName")] DJ dj)
        {
            if (id != dj.DJID) return NotFound();
            if (!ModelState.IsValid) return View(dj);
            try
            {
                _context.Update(dj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DJ.Any(e => e.DJID == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var dj = await _context.DJ.FirstOrDefaultAsync(m => m.DJID == id);
            if (dj == null) return NotFound();
            return View(dj);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dj = await _context.DJ.FindAsync(id);
            if (dj != null)
            {
                _context.DJ.Remove(dj);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
