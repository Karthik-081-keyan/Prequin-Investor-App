using Investor.Domain.DTO;

namespace Investor.Infrastructure.Repository
{
    public interface IInvestorRepository
    {
        List<InvestorDto> GetInvestors();

        CommitmentsDto GetCommitments(string investor);
    }
}
