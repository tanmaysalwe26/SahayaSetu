namespace SahayaSetu.DTO
{
    public class ContributionSummaryDto
    {
        public List<DonationContributionDto> Donations { get; set; } = new();
        public List<VolunteerContributionDto> VolunteerApplications { get; set; } = new();
        public List<ResourceContributionDto> ResourceContributions { get; set; } = new();
    }

    public class DonationContributionDto
    {
        public decimal Amount { get; set; }
        public string? FundraiserTitle { get; set; }
        public DateTime DonatedAt { get; set; }
    }

    public class VolunteerContributionDto
    {
        public string? RequestTitle { get; set; }
        public string? Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }

    public class ResourceContributionDto
    {
        public int Quantity { get; set; }
        public string? ResourceType { get; set; }
        public string? RequestTitle { get; set; }
        public DateTime ContributedAt { get; set; }
    }
}
