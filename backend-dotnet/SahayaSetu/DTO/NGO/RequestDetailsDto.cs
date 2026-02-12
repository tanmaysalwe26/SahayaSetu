namespace SahayaSetu.DTO.NGO
{
    public class RequestDetailsDto
    {
        public List<ApplicantDetailDto>? Applicants { get; set; }
        public List<ContributorDetailDto>? Contributors { get; set; }
        public List<DonorDetailDto>? Donors { get; set; }
    }

    public class ApplicantDetailDto
    {
        public string? DonorName { get; set; }
        public string? DonorEmail { get; set; }
        public DateTime AppliedAt { get; set; }
        public string? Status { get; set; }
    }

    public class ContributorDetailDto
    {
        public string? DonorName { get; set; }
        public string? DonorEmail { get; set; }
        public int Quantity { get; set; }
        public DateTime ContributedAt { get; set; }
    }

    public class DonorDetailDto
    {
        public string? DonorName { get; set; }
        public string? DonorEmail { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DonatedAt { get; set; }
    }
}
