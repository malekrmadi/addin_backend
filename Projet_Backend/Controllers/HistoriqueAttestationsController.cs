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
    public class HistoriqueAttestationsController : ControllerBase
    {
        private readonly DataContext _context;

        public HistoriqueAttestationsController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<HistoriqueAttestation>> CreateHistorique(HistoriqueAttestation historique)
        {
            // Si role_utilisateur = 'Systeme' => id_utilisateur = 0
            if (historique.RoleUtilisateur == RoleUtilisateur.Systeme)
            {
                historique.IdUtilisateur = 0;
            }

            // Le setter de la propriété Commentaire gère déjà la valeur par défaut
            // Donc pas besoin de la vérifier ici

            _context.HistoriqueAttestations.Add(historique);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistorique), new { id = historique.IdHistorique }, historique);
        }


        // GET: api/historique-attestations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriqueAttestation>> GetHistorique(int id)
        {
            var historique = await _context.HistoriqueAttestations.FindAsync(id);

            if (historique == null)
                return NotFound();

            return historique;
        }

        // GET: api/historique-attestations/attestation/{id_attestation}
        [HttpGet("attestation/{id_attestation}")]
        public async Task<ActionResult<IEnumerable<HistoriqueAttestation>>> GetHistoriqueByAttestation(int id_attestation)
        {
            var historiques = await _context.HistoriqueAttestations
                .Where(h => h.IdAttestation == id_attestation)
                .ToListAsync();

            return historiques;
        }

        // GET: api/historique-attestations/assure/{id_assure}
        [HttpGet("assure/{id_assure}")]
        public async Task<ActionResult<IEnumerable<HistoriqueAttestation>>> GetHistoriqueByAssure(int id_assure)
        {
            // Récupérer les véhicules de l'assuré
            var vehiculesIds = await _context.Vehicules
                .Where(v => v.IdAssure == id_assure)
                .Select(v => v.IdVehicule)
                .ToListAsync();

            // Récupérer les attestations associées à ces véhicules
            var attestationIds = await _context.Attestations
                .Where(a => vehiculesIds.Contains(a.IdVehicule))
                .Select(a => a.IdAttestation)
                .ToListAsync();

            // Récupérer l'historique correspondant
            var historiques = await _context.HistoriqueAttestations
                .Where(h => attestationIds.Contains(h.IdAttestation))
                .ToListAsync();

            return historiques;
        }
    }
}
