using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class SavingsPool
    {
        [Key]
        public int SavingsPoolsId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public int TargetAmount { get; set; }

        public int CurrentAmount { get; set; }

        public ICollection<PoolContributors> Contributors { get; set; } = new List<PoolContributors>();

        public int SchedTypeId { get; set; }
        public SchedTypes SchedType { get; set; } = null!;
    }
}
