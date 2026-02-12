namespace SahayaSetu.DTO
{
    public class FundraiserRequestResponseDto : RequestResponseDto
    {
        public decimal TargetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public DateTime Deadline { get; set; }
    }
}
