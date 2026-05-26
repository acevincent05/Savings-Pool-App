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

            // ======================
            // SEED DATA
            // ======================

            // 1. SchedTypes
            modelBuilder.Entity<SchedTypes>().HasData(
                new SchedTypes { SchedTypeId = 1, Name = "Daily" },
                new SchedTypes { SchedTypeId = 2, Name = "Weekly" },
                new SchedTypes { SchedTypeId = 3, Name = "Bi-Weekly" },
                new SchedTypes { SchedTypeId = 4, Name = "Monthly" },
                new SchedTypes { SchedTypeId = 5, Name = "Quarterly" },
                new SchedTypes { SchedTypeId = 6, Name = "Yearly" }
            );

            // 2. StatusContribution
            modelBuilder.Entity<StatusContribution>().HasData(
                new StatusContribution { StatusId = 1, StatusName = "Pending" },
                new StatusContribution { StatusId = 2, StatusName = "Completed" },
                new StatusContribution { StatusId = 3, StatusName = "Failed" },
                new StatusContribution { StatusId = 4, StatusName = "Refunded" }
            );

            // 3. Users
            modelBuilder.Entity<Users>().HasData(
                new Users { UserId = 1, Name = "Alice Johnson" },
                new Users { UserId = 2, Name = "Bob Smith" },
                new Users { UserId = 3, Name = "Charlie Brown" },
                new Users { UserId = 4, Name = "Diana Prince" },
                new Users { UserId = 5, Name = "Evan Wright" }
            );

            // 4. SavingsPools
            modelBuilder.Entity<SavingsPool>().HasData(
                new SavingsPool 
                { 
                    SavingsPoolsId = 1, 
                    Title = "Summer Vacation Fund", 
                    TargetAmount = 5000, 
                    CurrentAmount = 2500, 
                    SchedTypeId = 4 
                },
                new SavingsPool 
                { 
                    SavingsPoolsId = 2, 
                    Title = "New Laptop Group Buy", 
                    TargetAmount = 3000, 
                    CurrentAmount = 1200, 
                    SchedTypeId = 2 
                },
                new SavingsPool 
                { 
                    SavingsPoolsId = 3, 
                    Title = "Emergency Rainy Day Fund", 
                    TargetAmount = 10000, 
                    CurrentAmount = 4500, 
                    SchedTypeId = 4 
                },
                new SavingsPool 
                { 
                    SavingsPoolsId = 4, 
                    Title = "Office Party Budget", 
                    TargetAmount = 800, 
                    CurrentAmount = 800, 
                    SchedTypeId = 1 
                },
                new SavingsPool 
                { 
                    SavingsPoolsId = 5, 
                    Title = "Quarterly Investment Pool", 
                    TargetAmount = 15000, 
                    CurrentAmount = 6000, 
                    SchedTypeId = 5 
                }
            );

            // 5. PoolContributors
            modelBuilder.Entity<PoolContributors>().HasData(
                new PoolContributors 
                { 
                    ContributorId = 1, 
                    SavingsPoolId = 1, 
                    UserId = 1, 
                    StatusId = 2, 
                    Amount = 500, 
                    ContributionDate = new DateTime(2026, 1, 15, 10, 30, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 2, 
                    SavingsPoolId = 1, 
                    UserId = 2, 
                    StatusId = 2, 
                    Amount = 500, 
                    ContributionDate = new DateTime(2026, 1, 16, 14, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 3, 
                    SavingsPoolId = 1, 
                    UserId = 3, 
                    StatusId = 1, 
                    Amount = 500, 
                    ContributionDate = new DateTime(2026, 1, 17, 9, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 4, 
                    SavingsPoolId = 1, 
                    UserId = 4, 
                    StatusId = 2, 
                    Amount = 500, 
                    ContributionDate = new DateTime(2026, 1, 18, 16, 45, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 5, 
                    SavingsPoolId = 1, 
                    UserId = 5, 
                    StatusId = 2, 
                    Amount = 500, 
                    ContributionDate = new DateTime(2026, 1, 19, 11, 20, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 6, 
                    SavingsPoolId = 2, 
                    UserId = 1, 
                    StatusId = 2, 
                    Amount = 300, 
                    ContributionDate = new DateTime(2026, 2, 1, 8, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 7, 
                    SavingsPoolId = 2, 
                    UserId = 2, 
                    StatusId = 2, 
                    Amount = 300, 
                    ContributionDate = new DateTime(2026, 2, 8, 8, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 8, 
                    SavingsPoolId = 2, 
                    UserId = 3, 
                    StatusId = 2, 
                    Amount = 300, 
                    ContributionDate = new DateTime(2026, 2, 15, 8, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 9, 
                    SavingsPoolId = 2, 
                    UserId = 4, 
                    StatusId = 1, 
                    Amount = 300, 
                    ContributionDate = new DateTime(2026, 2, 22, 8, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 10, 
                    SavingsPoolId = 3, 
                    UserId = 1, 
                    StatusId = 2, 
                    Amount = 1500, 
                    ContributionDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 11, 
                    SavingsPoolId = 3, 
                    UserId = 2, 
                    StatusId = 2, 
                    Amount = 1500, 
                    ContributionDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 12, 
                    SavingsPoolId = 3, 
                    UserId = 3, 
                    StatusId = 2, 
                    Amount = 1500, 
                    ContributionDate = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 13, 
                    SavingsPoolId = 4, 
                    UserId = 1, 
                    StatusId = 2, 
                    Amount = 200, 
                    ContributionDate = new DateTime(2026, 5, 20, 12, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 14, 
                    SavingsPoolId = 4, 
                    UserId = 2, 
                    StatusId = 2, 
                    Amount = 200, 
                    ContributionDate = new DateTime(2026, 5, 21, 12, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 15, 
                    SavingsPoolId = 4, 
                    UserId = 3, 
                    StatusId = 2, 
                    Amount = 200, 
                    ContributionDate = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 16, 
                    SavingsPoolId = 4, 
                    UserId = 4, 
                    StatusId = 2, 
                    Amount = 200, 
                    ContributionDate = new DateTime(2026, 5, 23, 12, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 17, 
                    SavingsPoolId = 5, 
                    UserId = 1, 
                    StatusId = 2, 
                    Amount = 2000, 
                    ContributionDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 18, 
                    SavingsPoolId = 5, 
                    UserId = 2, 
                    StatusId = 2, 
                    Amount = 2000, 
                    ContributionDate = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc) 
                },
                new PoolContributors 
                { 
                    ContributorId = 19, 
                    SavingsPoolId = 5, 
                    UserId = 3, 
                    StatusId = 1, 
                    Amount = 2000, 
                    ContributionDate = new DateTime(2026, 7, 1, 0, 0, 0, DateTimeKind.Utc) 
                }
            );
        }
    }
}
