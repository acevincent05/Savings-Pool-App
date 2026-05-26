namespace Backend.DTOs
{
    public class UserCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UserUpdateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalContributions { get; set; }
        public int TotalAmountContributed { get; set; }
    }

    public class UserDetailDto : UserResponseDto
    {
        public List<PoolContributorResponseDto> Contributions { get; set; } = new();
    }
}
