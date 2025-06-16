using Microsoft.EntityFrameworkCore;
using Projet_Backend.Entities;

namespace Projet_Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Assure> Assures { get; set; }
        public DbSet<Assureur> Assureurs { get; set; }
        public DbSet<Courtier> Courtiers { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Attestation> Attestations { get; set; }
        public DbSet<HistoriqueAttestation> HistoriqueAttestations { get; set; }

    }
}
