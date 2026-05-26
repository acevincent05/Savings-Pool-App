namespace Backend.DTOs
{
    public class SavingsPoolCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int TargetAmount { get; set; }
        public int SchedTypeId { get; set; }
    }

    public class SavingsPoolUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public int TargetAmount { get; set; }
        public int CurrentAmount { get; set; }
        public int SchedTypeId { get; set; }
    }

    public class SavingsPoolResponseDto
    {
        public int SavingsPoolsId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TargetAmount { get; set; }
        public int CurrentAmount { get; set; }
        public int SchedTypeId { get; set; }
        public string SchedTypeName { get; set; } = string.Empty;
        public int ContributorCount { get; set; }
        public int TotalContributed { get; set; }
    }

    public class SavingsPoolDetailDto : SavingsPoolResponseDto
    {
        public List<PoolContributorResponseDto> Contributors { get; set; } = new();
    }
}
