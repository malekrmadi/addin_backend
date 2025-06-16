using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Backend.Entities
{
    public enum Statut
    {
        EnAttente,
        Accepte,
        Rejete,
        Active,
        Expire
    }

    public enum RoleUtilisateur
    {
        Assureur,
        Courtier,
        Assure,
        Systeme
    }

    public class HistoriqueAttestation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHistorique { get; set; }

        [Required]
        public int IdAttestation { get; set; }

        [Required]
        public Statut Statut { get; set; }

        [Required]
        public DateTime DateChangement { get; set; }

        [Required]
        public int IdUtilisateur { get; set; }

        [Required]
        public RoleUtilisateur RoleUtilisateur { get; set; }

        private string _commentaire = "pas de commentaire";

        [Required]
        [MaxLength(1000)]
        public string Commentaire
        {
            get => _commentaire;
            set => _commentaire = string.IsNullOrWhiteSpace(value) ? "pas de commentaire" : value;
        }
    }
}
