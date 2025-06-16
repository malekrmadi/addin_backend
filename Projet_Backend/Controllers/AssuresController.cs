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
    public class AssuresController : ControllerBase
    {
        private readonly DataContext _context;

        public AssuresController(DataContext context)
        {
            _context = context;
        }

        // GET: api/assures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assure>>> GetAssures()
        {
            return await _context.Assures.ToListAsync();
        }

        // GET: api/assures/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Assure>> GetAssure(int id)
        {
            var assure = await _context.Assures.FindAsync(id);
            if (assure == null)
                return NotFound();

            return assure;
        }

        // POST: api/assures
        [HttpPost]
        public async Task<ActionResult<Assure>> CreateAssure(Assure assure)
        {
            _context.Assures.Add(assure);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssure), new { id = assure.IdAssure }, assure);
        }

        // PUT: api/assures/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssure(int id, Assure assure)
        {
            if (id != assure.IdAssure)
                return BadRequest();

            _context.Entry(assure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssureExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/assures/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssure(int id)
        {
            var assure = await _context.Assures.FindAsync(id);
            if (assure == null)
                return NotFound();

            _context.Assures.Remove(assure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/assures/courtier/{id_courtier}
        [HttpGet("courtier/{id_courtier}")]
        public async Task<ActionResult<IEnumerable<Assure>>> GetAssuresByCourtier(int id_courtier)
        {
            var assures = await _context.Assures
                .Where(a => a.IdCourtier == id_courtier)
                .ToListAsync();

            return assures;
        }

        private bool AssureExists(int id)
        {
            return _context.Assures.Any(e => e.IdAssure == id);
        }
    }
}
