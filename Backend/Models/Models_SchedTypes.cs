using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class SchedTypes
    {
        [Key]
        public int SchedTypeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<SavingsPool> SavingsPools { get; set; } = new List<SavingsPool>();
    }
}
