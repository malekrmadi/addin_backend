using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Backend.Data;
using Projet_Backend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Projet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtiersController : ControllerBase
    {
        private readonly DataContext _context;

        public CourtiersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/courtiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courtier>>> GetCourtiers()
        {
            return await _context.Courtiers.ToListAsync();
        }

        // GET: api/courtiers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Courtier>> GetCourtier(int id)
        {
            var courtier = await _context.Courtiers.FindAsync(id);
            if (courtier == null)
                return NotFound();

            return courtier;
        }

        // POST: api/courtiers
        [HttpPost]
        public async Task<ActionResult<Courtier>> CreateCourtier(Courtier courtier)
        {
            _context.Courtiers.Add(courtier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourtier), new { id = courtier.IdCourtier }, courtier);
        }

        // PUT: api/courtiers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourtier(int id, Courtier courtier)
        {
            if (id != courtier.IdCourtier)
                return BadRequest();

            _context.Entry(courtier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourtierExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/courtiers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourtier(int id)
        {
            var courtier = await _context.Courtiers.FindAsync(id);
            if (courtier == null)
                return NotFound();

            _context.Courtiers.Remove(courtier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/courtiers/assure/{id_assure}
        [HttpGet("assure/{id_assure}")]
        public async Task<ActionResult<Courtier>> GetCourtierByAssure(int id_assure)
        {
            // On récupère le courtier qui est manager de l'assuré donné
            var courtier = await _context.Courtiers
                .Where(c => c.IdAssure == id_assure)
                .FirstOrDefaultAsync();

            if (courtier == null)
                return NotFound();

            return courtier;
        }

        private bool CourtierExists(int id)
        {
            return _context.Courtiers.Any(e => e.IdCourtier == id);
        }
    }
}
