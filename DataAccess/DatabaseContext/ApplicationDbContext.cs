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

            ConfigurePatientAccount(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigurePatientAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Givenname)
                .IsRequired();

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Lastname)
                .IsRequired();

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Address)
                .IsRequired();

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Location)
                .IsRequired();

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.ZipCode)
                .IsRequired();

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Givenname)
                .HasMaxLength(100);

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Lastname)
                .HasMaxLength(100);

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Address)
                .HasMaxLength(100);

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.AdditionalAddress)
                .HasMaxLength(100);

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.Location)
                .HasMaxLength(100);

            modelBuilder.Entity<PatientAccount>()
                .Property(d => d.ZipCode)
                .HasMaxLength(5);
        }

        public DbSet<DriverAccount> DriverAccounts { get; set; }

        public DbSet<PatientAccount> PatientAccounts { get; set; }
    }
}