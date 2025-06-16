using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Backend.Data;
using Projet_Backend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Projet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssureursController : ControllerBase
    {
        private readonly DataContext _context;

        public AssureursController(DataContext context)
        {
            _context = context;
        }

        // GET: api/assureurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assureur>>> GetAssureurs()
        {
            return await _context.Assureurs.ToListAsync();
        }

        // GET: api/assureurs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Assureur>> GetAssureur(int id)
        {
            var assureur = await _context.Assureurs.FindAsync(id);

            if (assureur == null)
                return NotFound();

            return assureur;
        }

        // POST: api/assureurs
        [HttpPost]
        public async Task<ActionResult<Assureur>> CreateAssureur(Assureur assureur)
        {
            _context.Assureurs.Add(assureur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssureur), new { id = assureur.IdAssureur }, assureur);
        }

        // PUT: api/assureurs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssureur(int id, Assureur assureur)
        {
            if (id != assureur.IdAssureur)
                return BadRequest();

            _context.Entry(assureur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssureurExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/assureurs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssureur(int id)
        {
            var assureur = await _context.Assureurs.FindAsync(id);
            if (assureur == null)
                return NotFound();

            _context.Assureurs.Remove(assureur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssureurExists(int id)
        {
            return _context.Assureurs.Any(e => e.IdAssureur == id);
        }
    }
}
