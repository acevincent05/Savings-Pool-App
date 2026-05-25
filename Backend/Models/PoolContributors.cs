using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PoolContributors
    {
        public int ContributorId { get; set; }

        public ICollection<Users> User { get; } = new List<User>();
        
        public int StatusId { get; set; }
        public StatusContribution StatusContribution { get; set; }
        
        

    }
}