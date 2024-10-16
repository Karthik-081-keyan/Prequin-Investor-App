using Investor.Domain.DTO;

namespace Investor.Application.Services
{
    public interface ICommitmentService
    {
        CommitmentsDto GetCommitments(string investor);

    }
}
