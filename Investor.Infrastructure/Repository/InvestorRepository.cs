using Investor.Domain.DTO;
using Investor.Domain.Model;
using Investor.Infrastructure.Utility;
using Microsoft.EntityFrameworkCore;

namespace Investor.Infrastructure.Repository
{
    public class InvestorRepository : IInvestorRepository
    {
        private readonly AppDbContext dbContext;

        public InvestorRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<InvestorDto> GetInvestors()
        {
            var records = dbContext.Investors.AsNoTracking().ToList();
            int count = 0;
            var investorList = records.GroupBy(x => x.Name).Select(r => new InvestorDto
            {
                Id=count++,
                Name = r.Key,
                DateAdded = r.Last().DateAdded,
                Type = r.First().Type,
                Address = r.First().Country,
                TotalAmount = r.Sum(p => p.CommitmentAmount).FormatToMillionAndBillion()
            }).ToList();
            return investorList;
        }

        public CommitmentsDto GetCommitments(string investor)
        {
            var records = dbContext.Investors
                      .Where(i => i.Name == investor)
                      .AsNoTracking()
                      .ToList();

            // Group commitments by asset class
            var commitmentsGroup = records.GroupBy(r => r.CommitmentAssetClass);

            var commitments = commitmentsGroup
                .Select(c => new Commitment
                {
                    Type = c.Key,
                    Total = c.Sum(p => p.CommitmentAmount).FormatToMillionAndBillion()
                })
                .ToList();

            // Add total commitments for all asset classes
            commitments.Add(new Commitment
            {
                Type = "All",
                Total = records.Sum(r => r.CommitmentAmount).FormatToMillionAndBillion()
            });

            // Create commitment info
            var info = records.Select(r => new CommitmentInfo
            {
                AssetName = r.CommitmentAssetClass,
                TotalAmount = r.CommitmentAmount.FormatToMillionAndBillion(),
                Currency = r.CommitmentCurrency,
                Id=r.Id
            }).ToList();

            // Create the DTO
            var dto = new CommitmentsDto
            {
                Commitments = commitments,
                CommitmentsInfo = info
            };

            return dto;

        }
    }
}
