using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class StatusContribution
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public string StatusName { get; set; } = string.Empty;

        public ICollection<PoolContributors> PoolContributors { get; set; } = new List<PoolContributors>();
    }
}
