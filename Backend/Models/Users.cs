using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<SavingsPool> SavingsPools { get; } = new List<SavingsPool>();
    }
}