using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeVsVirus.Models.Entities;

namespace WeVsVirus.DataAccess.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseHiLo("DBSequenceHiLo");

            // set cascade behaviour to restrict
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DriverAccount> DriverAccounts { get; set; }
        public DbSet<MedicalInstituteAccount> MedicalInstituteAccounts { get; set; }
        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}