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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // FK assignment
        modelBuilder.Entity<PoolContr>()
            .HasOne(s => s.StudentPrograms)
            .WithMany(p => p.Students)
            .HasForeignKey(s => s.StudentProgramsId);

        modelBuilder.Entity<Section>().HasData(
            new Section { Id = 1, SectionCode = "M001", StudentProgramsId = 1 },
            new Section { Id = 2, SectionCode = "M002", StudentProgramsId = 1 },
            new Section { Id = 3, SectionCode = "M003", StudentProgramsId = 2 },
            new Section { Id = 4, SectionCode = "M004", StudentProgramsId = 2 },
            new Section { Id = 5, SectionCode = "M005", StudentProgramsId = 3 }
        );


    }

}
}
