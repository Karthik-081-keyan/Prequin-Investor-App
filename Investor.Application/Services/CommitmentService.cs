using Investor.Domain.DTO;
using Investor.Infrastructure.Repository;

namespace Investor.Application.Services
{
    public class CommitmentService : ICommitmentService
    {
        private IInvestorRepository _repository;

        public CommitmentService(IInvestorRepository repository)
        {
            _repository = repository;


        }

        /// <summary>
        /// Get Commitments info for investor
        /// </summary>
        /// <param name="investor"></param>
        /// <returns></returns>
        public CommitmentsDto GetCommitments(string investor)
        {
            return _repository.GetCommitments(investor);
        }
    }
}
