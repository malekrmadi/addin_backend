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
    public class AttestationsController : ControllerBase
    {
        private readonly DataContext _context;

        public AttestationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/attestations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestations()
        {
            return await _context.Attestations.ToListAsync();
        }

        // GET: api/attestations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Attestation>> GetAttestation(int id)
        {
            var attestation = await _context.Attestations.FindAsync(id);
            if (attestation == null)
                return NotFound();

            return attestation;
        }

        // POST: api/attestations
        [HttpPost]
        public async Task<ActionResult<Attestation>> CreateAttestation(Attestation attestation)
        {
            _context.Attestations.Add(attestation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAttestation), new { id = attestation.IdAttestation }, attestation);
        }

        // PUT: api/attestations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttestation(int id, Attestation attestation)
        {
            if (id != attestation.IdAttestation)
                return BadRequest();

            _context.Entry(attestation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttestationExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/attestations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttestation(int id)
        {
            var attestation = await _context.Attestations.FindAsync(id);
            if (attestation == null)
                return NotFound();

            _context.Attestations.Remove(attestation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/attestations/vehicule/{id_vehicule}
        [HttpGet("vehicule/{id_vehicule}")]
        public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestationsByVehicule(int id_vehicule)
        {
            var attestations = await _context.Attestations
                .Where(a => a.IdVehicule == id_vehicule)
                .ToListAsync();

            return attestations;
        }

        // GET: api/attestations/assure/{id_assure}
        [HttpGet("assure/{id_assure}")]
        public async Task<ActionResult<IEnumerable<Attestation>>> GetAttestationsByAssure(int id_assure)
        {
            // On récupère les véhicules de l'assuré
            var vehiculesIds = await _context.Vehicules
                .Where(v => v.IdAssure == id_assure)
                .Select(v => v.IdVehicule)
                .ToListAsync();

            var attestations = await _context.Attestations
                .Where(a => vehiculesIds.Contains(a.IdVehicule))
                .ToListAsync();

            return attestations;
        }

        // GET: api/attestations/{id}/is-active
        [HttpGet("{id}/is-active")]
        public async Task<ActionResult<bool>> IsAttestationActive(int id)
        {
            var attestation = await _context.Attestations.FindAsync(id);
            if (attestation == null)
                return NotFound();

            bool isActive = attestation.Statut == StatutAttestation.Accepte &&
                            DateTime.Today >= attestation.DateDebut.Date &&
                            DateTime.Today <= attestation.DateFin.Date;

            return Ok(isActive);
        }


        private bool AttestationExists(int id)
        {
            return _context.Attestations.Any(e => e.IdAttestation == id);
        }
    }
}
