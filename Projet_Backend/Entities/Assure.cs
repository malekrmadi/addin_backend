using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Backend.Entities
{
    public enum StatutAssure
    {
        Physique,
        Morale
    }

    public class Assure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAssure { get; set; }

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
        public string MotDePasse { get; set; } // Hashé

        [Required]
        [MaxLength(255)]
        public string Adresse { get; set; }

        [Required]
        public StatutAssure Statut { get; set; }

        [Required]
        public int IdCourtier { get; set; } // Référence au courtier responsable (non nullable)
    }
}
