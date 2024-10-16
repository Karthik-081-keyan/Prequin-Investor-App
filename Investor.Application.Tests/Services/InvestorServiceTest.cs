using Investor.Application.Services;
using Investor.Domain.DTO;
using Investor.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace Investor.Application.Tests.Services
{
    public class InvestorServiceTest
    {
        private IInvestorService _service;
        private Mock<IInvestorRepository> _repository;
        private Mock<ILogger<InvestorService>> _logger;

        public InvestorServiceTest()
        {
            _repository = new Mock<IInvestorRepository>();
            _logger = new Mock<ILogger<InvestorService>>();
            _service = new InvestorService(_repository.Object, _logger.Object);
        }

        [Test]

        public void GetInvestors_ReturnsInvestors()
        {
            //arrange
            var data = new List<InvestorDto> { new InvestorDto(), new InvestorDto() };
            _repository.Setup(i => i.GetInvestors()).Returns(data);
            //act
            var res = _service.GetInvestors();
            //assert
            Assert.AreEqual(data.Count, res.Count);
        }

       
    }
}