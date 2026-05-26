using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PoolContributors
    {
        [Key]
        public int ContributorId { get; set; }

        public int SavingsPoolId { get; set; }
        public SavingsPool SavingsPool { get; set; } = null!;

        public int UserId { get; set; }
        public Users User { get; set; } = null!;

        public int StatusId { get; set; }
        public StatusContribution StatusContribution { get; set; } = null!;

        public int Amount { get; set; }

        public DateTime ContributionDate { get; set; } = DateTime.UtcNow;
    }
}
