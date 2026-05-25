using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class StatusContribution
    {
        public int StatusId { get; set; }

        [Required]
        public string StatusName { get; set; } = string.Empty;

        public int ICollection<PoolContributors> PoolContributor { get; } = new List<PoolContributor>();
        
    }
}