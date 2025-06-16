using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Backend.Entities
{
    public class Vehicule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVehicule { get; set; }

        [Required]
        public int IdAssure { get; set; }

        [Required]
        [MaxLength(100)]
        public string Marque { get; set; }

        [Required]
        [MaxLength(100)]
        public string Modele { get; set; }

        [Required]
        [MaxLength(50)]
        public string Categorie { get; set; }

        [Required]
        public int PuissanceFiscale { get; set; }

        [Required]
        [MaxLength(20)]
        public string Immatriculation { get; set; }
    }
}
