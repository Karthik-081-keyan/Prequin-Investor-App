namespace Investor.Domain.DTO
{
    public class Commitment
    {
        public string Type { get; set; }
        public string Total { get; set; }
    }

    public class CommitmentInfo
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string Currency { get; set; }

        public string TotalAmount { get; set; }
    }
    public class CommitmentsDto
    {
        public List<Commitment> Commitments { get; set; }
        public List<CommitmentInfo> CommitmentsInfo { get; set; }
    }
}
