using Investor.Domain.DTO;
using Investor.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace Investor.Application.Services
{
    public class InvestorService : IInvestorService
    {
        private readonly IInvestorRepository _repository;
        private readonly ILogger<InvestorService> _logger;

        public InvestorService(IInvestorRepository repository, ILogger<InvestorService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// Returns investor data
        /// </summary>
        /// <returns></returns>
        public List<InvestorDto> GetInvestors()
        {
            return _repository.GetInvestors();
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
