using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Backend.Entities
{
    public class Courtier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCourtier { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(100)]
        public string Prenom { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Telephone { get; set; }

        [Required]
        [MaxLength(255)]
        public string MotDePasse { get; set; } // Ce champ est censé contenir un mot de passe hashé

        [Required]
        [MaxLength(100)]
        public string NomAgence { get; set; }

        [Required]
        public int IdAssure { get; set; } // Référence à l'assuré manager, sans navigation EF
    }
}
