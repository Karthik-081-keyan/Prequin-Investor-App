namespace Investor.Domain.DTO
{
    public class InvestorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public DateTime DateAdded { get; set; }
        public string TotalAmount { get; set; }
    }
}
