using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class SchedTypes
    {
        [Key]
        public int SchedTypeId { get; set; }

        [Required]
        public string SchedType { get; set; } = string.Empty;
        
    }
}