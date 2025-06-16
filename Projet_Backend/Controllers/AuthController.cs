using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_Backend.Entities;
using Projet_Backend.Data;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto.Role?.ToLower() != "assure")
                return BadRequest("Seuls les assurés peuvent s'inscrire.");

            // Vérifier unicité email dans toutes les tables utilisateurs
            bool emailExists = await _context.Assureurs.AnyAsync(a => a.Email == registerDto.Email)
                || await _context.Courtiers.AnyAsync(c => c.Email == registerDto.Email)
                || await _context.Assures.AnyAsync(a => a.Email == registerDto.Email);

            if (emailExists)
                return BadRequest("Email déjà utilisé.");

            string hashedPassword = HashPassword(registerDto.MotDePasse);

            // Conversion de Statut string en enum StatutAssure
            StatutAssure statutEnum;
            if (!Enum.TryParse<StatutAssure>(registerDto.Statut!, true, out statutEnum))
            {
                return BadRequest("Statut assuré invalide. Valeurs acceptées : Physique, Morale.");
            }

            var assure = new Assure
            {
                Nom = registerDto.Nom,
                Prenom = registerDto.Prenom,
                Email = registerDto.Email,
                Telephone = registerDto.Telephone,
                MotDePasse = hashedPassword,
                Adresse = registerDto.Adresse!,
                Statut = statutEnum,
                IdCourtier = registerDto.IdCourtier ?? 0
            };

            _context.Assures.Add(assure);
            await _context.SaveChangesAsync();

            return Ok("Assuré enregistré avec succès.");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            string hashedPassword = HashPassword(loginDto.MotDePasse);

            // Chercher dans Assureurs
            var assureur = await _context.Assureurs.FirstOrDefaultAsync(a => a.Email == loginDto.Email && a.MotDePasse == hashedPassword);
            if (assureur != null)
                return Ok(new { Role = "assureur", Id = assureur.IdAssureur, Nom = assureur.Nom, Prenom = assureur.Prenom });

            // Chercher dans Courtiers
            var courtier = await _context.Courtiers.FirstOrDefaultAsync(c => c.Email == loginDto.Email && c.MotDePasse == hashedPassword);
            if (courtier != null)
                return Ok(new { Role = "courtier", Id = courtier.IdCourtier, Nom = courtier.Nom, Prenom = courtier.Prenom });

            // Chercher dans Assures
            var assure = await _context.Assures.FirstOrDefaultAsync(a => a.Email == loginDto.Email && a.MotDePasse == hashedPassword);
            if (assure != null)
                return Ok(new { Role = "assure", Id = assure.IdAssure, Nom = assure.Nom, Prenom = assure.Prenom });

            return Unauthorized("Email ou mot de passe incorrect.");
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        public ActionResult Logout()
        {
            // À adapter selon la gestion de token/session
            return Ok("Déconnexion réussie.");
        }

        // Méthode simple pour hasher le mot de passe (à remplacer par algo sécurisé comme BCrypt en prod)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    // DTO pour l'inscription
    public class RegisterDto
    {
        public string? Role { get; set; } // Doit être "assure"
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string MotDePasse { get; set; } = null!;
        public string? Adresse { get; set; }
        public string? Statut { get; set; }  // 'physique' ou 'morale'
        public int? IdCourtier { get; set; }
    }

    // DTO pour la connexion
    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string MotDePasse { get; set; } = null!;
    }
}
