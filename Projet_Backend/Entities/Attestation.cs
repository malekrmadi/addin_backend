using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Backend.Entities
{
    public enum StatutAttestation
    {
        EnAttente,
        Accepte,
        Rejete
    }

    public enum TypeAssurance
    {
        Tiers,
        TiersPlus,
        TousRisques
    }

    public class Attestation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAttestation { get; set; }

        [Required]
        public int IdVehicule { get; set; }

        [Required]
        public DateTime DateEnvoi { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        [Required]
        public StatutAttestation Statut { get; set; } = StatutAttestation.EnAttente;

        [Required]
        public bool EstAssure { get; set; } = false;

        [Required]
        public TypeAssurance TypeAssurance { get; set; }
    }
}
