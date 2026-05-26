using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class SavingsPoolContext : DbContext
    {
        public SavingsPoolContext(DbContextOptions<SavingsPoolContext> options) : base(options) { }

        public DbSet<PoolContributors> PoolContributors { get; set; }
        public DbSet<SavingsPool> SavingsPools { get; set; }
        public DbSet<SchedTypes> SchedTypes { get; set; }
        public DbSet<StatusContribution> StatusContributions { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PoolContributors -> SavingsPool
            modelBuilder.Entity<PoolContributors>()
                .HasOne(pc => pc.SavingsPool)
                .WithMany(sp => sp.Contributors)
                .HasForeignKey(pc => pc.SavingsPoolId)
                .OnDelete(DeleteBehavior.Cascade);

            // PoolContributors -> Users
            modelBuilder.Entity<PoolContributors>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.PoolContributors)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // PoolContributors -> StatusContribution
            modelBuilder.Entity<PoolContributors>()
                .HasOne(pc => pc.StatusContribution)
                .WithMany(sc => sc.PoolContributors)
                .HasForeignKey(pc => pc.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // SavingsPool -> SchedTypes
            modelBuilder.Entity<SavingsPool>()
                .HasOne(sp => sp.SchedType)
                .WithMany(st => st.SavingsPools)
                .HasForeignKey(sp => sp.SchedTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
