using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<PoolContributors> PoolContributors { get; set; } = new List<PoolContributors>();
    }
}
