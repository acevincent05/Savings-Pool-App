namespace Backend.DTOs
{
    public class PoolContributorCreateDto
    {
        public int SavingsPoolId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public int Amount { get; set; }
    }

    public class PoolContributorUpdateDto
    {
        public int StatusId { get; set; }
        public int Amount { get; set; }
    }

    public class PoolContributorResponseDto
    {
        public int ContributorId { get; set; }
        public int SavingsPoolId { get; set; }
        public string SavingsPoolTitle { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime ContributionDate { get; set; }
    }
}
