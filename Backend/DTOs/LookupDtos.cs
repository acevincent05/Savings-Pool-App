namespace Backend.DTOs
{
    public class SchedTypeCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class SchedTypeResponseDto
    {
        public int SchedTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class StatusCreateDto
    {
        public string StatusName { get; set; } = string.Empty;
    }

    public class StatusResponseDto
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
