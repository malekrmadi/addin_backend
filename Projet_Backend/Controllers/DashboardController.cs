using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Backend.Data;
using Projet_Backend.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Backend.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext _context;

        public DashboardController(DataContext context)
        {
            _context = context;
        }

        // GET /api/dashboard/total-assures
        [HttpGet("total-assures")]
        public async Task<ActionResult<int>> GetTotalAssures()
        {
            int count = await _context.Assures.CountAsync();
            return Ok(count);
        }

        // GET /api/dashboard/assures-by-courtier/:id_courtier
        [HttpGet("assures-by-courtier/{idCourtier}")]
        public async Task<ActionResult<int>> GetAssuresByCourtier(int idCourtier)
        {
            int count = await _context.Assures.CountAsync(a => a.IdCourtier == idCourtier);
            return Ok(count);
        }

        // GET /api/dashboard/total-vehicules
        [HttpGet("total-vehicules")]
        public async Task<ActionResult<int>> GetTotalVehicules()
        {
            int count = await _context.Vehicules.CountAsync();
            return Ok(count);
        }

        // GET /api/dashboard/vehicules-by-assure/:id_assure
        [HttpGet("vehicules-by-assure/{idAssure}")]
        public async Task<ActionResult<int>> GetVehiculesByAssure(int idAssure)
        {
            int count = await _context.Vehicules.CountAsync(v => v.IdAssure == idAssure);
            return Ok(count);
        }

        // GET /api/dashboard/total-attestations
        [HttpGet("total-attestations")]
        public async Task<ActionResult<int>> GetTotalAttestations()
        {
            int count = await _context.Attestations.CountAsync();
            return Ok(count);
        }

        // GET /api/dashboard/attestations-en-attente
        [HttpGet("attestations-en-attente")]
        public async Task<ActionResult<int>> GetAttestationsEnAttente()
        {
            int count = await _context.Attestations.CountAsync(a => a.Statut == StatutAttestation.EnAttente);
            return Ok(count);
        }

        // GET /api/dashboard/attestations-acceptees
        [HttpGet("attestations-acceptees")]
        public async Task<ActionResult<int>> GetAttestationsAcceptees()
        {
            int count = await _context.Attestations.CountAsync(a => a.Statut == StatutAttestation.Accepte);
            return Ok(count);
        }

        // GET /api/dashboard/attestations-rejetees
        [HttpGet("attestations-rejetees")]
        public async Task<ActionResult<int>> GetAttestationsRejetees()
        {
            int count = await _context.Attestations.CountAsync(a => a.Statut == StatutAttestation.Rejete);
            return Ok(count);
        }


        // GET /api/dashboard/attestations-actives
        [HttpGet("attestations-actives")]
        public async Task<ActionResult<int>> GetAttestationsActives()
        {
            DateTime now = DateTime.UtcNow.Date;
            int count = await _context.Attestations.CountAsync(a =>
                a.Statut == StatutAttestation.Accepte &&
                a.DateDebut <= now &&
                a.DateFin >= now);
            return Ok(count);
        }


        // GET /api/dashboard/total-courtiers
        [HttpGet("total-courtiers")]
        public async Task<ActionResult<int>> GetTotalCourtiers()
        {
            int count = await _context.Courtiers.CountAsync();
            return Ok(count);
        }

        // GET /api/dashboard/total-assureurs
        [HttpGet("total-assureurs")]
        public async Task<ActionResult<int>> GetTotalAssureurs()
        {
            int count = await _context.Assureurs.CountAsync();
            return Ok(count);
        }
    }
}
