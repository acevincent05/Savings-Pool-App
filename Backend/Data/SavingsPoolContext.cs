using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class SavingsPoolContext : DbContext
    {
        public SavingsPoolContext(DbContextOptions<SavingsPoolContext> options) : base(options) { }
        public DbSet<PoolContributors> PoolContributor  { get; set; }
        public DbSet<SavingsPool> SavingsPool  { get; set; }
        public DbSet<SchedTypes> SchedType  { get; set; }
        public DbSet<StatusContribution> StatusContribution { get; set; }
        public DbSet<Users> User { get; set; }

    }

}
