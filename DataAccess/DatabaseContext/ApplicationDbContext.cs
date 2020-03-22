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

            modelBuilder.Entity<SwabJobMatch>()
                .HasKey(jobMatch => new { jobMatch.SwabJobId, jobMatch.DriverAccountId });
            modelBuilder.Entity<SwabJobMatch>()
                .HasOne(jobMatch => jobMatch.SwabJob)
                .WithMany(job => job.SwabJobMatches)
                .HasForeignKey(jobMatch => jobMatch.SwabJobId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SwabJobMatch>()
                .HasOne(jobMatch => jobMatch.DriverAccount)
                .WithMany(driverAccount => driverAccount.SwabJobMatches)
                .HasForeignKey(jobMatch => jobMatch.DriverAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DriverAccount> DriverAccounts { get; set; }
        public DbSet<MedicalInstituteAccount> MedicalInstituteAccounts { get; set; }
        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}