namespace SahayaSetu.DTO.NGO
{
    public class DonationResponseDto
    {
        public int DonationId { get; set; }
        public string? DonorName { get; set; }
        public string? DonorEmail { get; set; }
        public decimal Amount { get; set; }
        public string? FundraiserTitle { get; set; }
        public DateTime DonationDate { get; set; }
        public bool ResponseStatus { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
