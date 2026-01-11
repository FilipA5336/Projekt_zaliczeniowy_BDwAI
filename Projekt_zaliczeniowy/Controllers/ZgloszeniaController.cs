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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var zgloszenie = await _context.Zgloszenia
                .Include(z => z.Kategoria)
                .Include(z => z.Priorytet)
                .Include(z => z.Komentarze)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zgloszenie == null) return NotFound();

            return View(zgloszenie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int ZgloszenieId, string Tresc)
        {
            if (!string.IsNullOrWhiteSpace(Tresc))
            {
                var komentarz = new Komentarz
                {
                    ZgloszenieId = ZgloszenieId,
                    Tresc = Tresc,
                    Autor = User.Identity?.Name,
                    DataDodania = DateTime.Now
                };
                _context.Komentarze.Add(komentarz);
                await _context.SaveChangesAsync();
            }
           
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var zgloszenie = await _context.Zgloszenia.FindAsync(id);
            if (zgloszenie == null) return NotFound();

      
            if (!User.IsInRole("Admin") && zgloszenie.UzytkownikId != User.Identity.Name)
            {
                return Forbid();
            }

            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", zgloszenie.KategoriaId);
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            return View(zgloszenie);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tytul,Opis,KategoriaId,PriorytetId,DataUtworzenia,UzytkownikId")] Zgloszenie zgloszenie)
        {
            if (id != zgloszenie.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zgloszenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Zgloszenia.Any(e => e.Id == zgloszenie.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", zgloszenie.KategoriaId);
            ViewData["PriorytetId"] = new SelectList(_context.Priorytety, "Id", "Nazwa", zgloszenie.PriorytetId);
            return View(zgloszenie);
        }
    }
}
