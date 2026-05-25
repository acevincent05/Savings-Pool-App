using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class SavingsPool
    {
        public int SavingsPoolsId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public int TargetAmount { get; set; }

        public int CurrentAmount { get; set; }

        public ICollection<Contributors> Contributor { get; } = new List<Contributor>();
        
        public int SchedTypeId { get; set; }
        public SchedTypes SchedType { get; set; }
    }
}