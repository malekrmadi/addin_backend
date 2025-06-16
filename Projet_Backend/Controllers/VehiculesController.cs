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
    public class VehiculesController : ControllerBase
    {
        private readonly DataContext _context;

        public VehiculesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/vehicules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicule>>> GetVehicules()
        {
            return await _context.Vehicules.ToListAsync();
        }

        // GET: api/vehicules/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicule>> GetVehicule(int id)
        {
            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule == null)
                return NotFound();

            return vehicule;
        }

        // POST: api/vehicules
        [HttpPost]
        public async Task<ActionResult<Vehicule>> CreateVehicule(Vehicule vehicule)
        {
            _context.Vehicules.Add(vehicule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicule), new { id = vehicule.IdVehicule }, vehicule);
        }

        // PUT: api/vehicules/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicule(int id, Vehicule vehicule)
        {
            if (id != vehicule.IdVehicule)
                return BadRequest();

            _context.Entry(vehicule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/vehicules/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicule(int id)
        {
            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule == null)
                return NotFound();

            _context.Vehicules.Remove(vehicule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/vehicules/assure/{id_assure}
        [HttpGet("assure/{id_assure}")]
        public async Task<ActionResult<IEnumerable<Vehicule>>> GetVehiculesByAssure(int id_assure)
        {
            var vehicules = await _context.Vehicules
                .Where(v => v.IdAssure == id_assure)
                .ToListAsync();

            return vehicules;
        }

        private bool VehiculeExists(int id)
        {
            return _context.Vehicules.Any(e => e.IdVehicule == id);
        }
    }
}
