using Microsoft.AspNetCore.Mvc;
using Projekt_zaliczeniowy.Data;
using Projekt_zaliczeniowy.Models;
using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZgloszeniaApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniaApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zgloszenie>>> GetZgloszenia()
        {
            return await _context.Zgloszenia.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Zgloszenie>> GetZgloszenie(int id)
        {
            var zgloszenie = await _context.Zgloszenia.FindAsync(id);

            if (zgloszenie == null)
            {
                return NotFound();
            }

            return zgloszenie;
        }
    }
}
