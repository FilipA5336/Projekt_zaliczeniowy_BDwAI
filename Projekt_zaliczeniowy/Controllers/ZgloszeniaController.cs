using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt_zaliczeniowy.Data;
using Projekt_zaliczeniowy.Models;
using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy.Controllers
{
    [Authorize] 
    public class ZgloszeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniaController(ApplicationDbContext context)
        {
            _context = context;
        }
     
        public async Task<IActionResult> Index()
        {
            var zgloszenia = _context.Zgloszenia
                .Include(z => z.Kategoria)
                .Include(z => z.Priorytet);
            return View(await zgloszenia.ToListAsync());
        }

        public IActionResult Create()
        {

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tytul,Opis,KategoriaId,PriorytetId")] Zgloszenie zgloszenie)
        {
            if (ModelState.IsValid)
            {
                zgloszenie.DataUtworzenia = DateTime.Now;
                zgloszenie.UzytkownikId = User.Identity?.Name; 
                _context.Add(zgloszenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
         
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", zgloszenie.KategoriaId);
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            return View(zgloszenie);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var zgloszenie = await _context.Zgloszenia
                .Include(z => z.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zgloszenie == null) return NotFound();

            return View(zgloszenie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zgloszenie = await _context.Zgloszenia.FindAsync(id);
            if (zgloszenie != null)
            {
                _context.Zgloszenia.Remove(zgloszenie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
