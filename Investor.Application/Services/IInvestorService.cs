using Investor.Domain.DTO;

namespace Investor.Application.Services
{
    public interface IInvestorService
    {
        List<InvestorDto> GetInvestors();
        CommitmentsDto GetCommitments(string investor);

    }
}
