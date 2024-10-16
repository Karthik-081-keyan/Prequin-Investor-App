using Investor.Application.Services;
using Investor.Domain.DTO;
using Investor.Infrastructure.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investor.Application.Tests.Services
{
    public class CommitmentServiceTest
    {
        private CommitmentService _service;
        private Mock<IInvestorRepository> _repo;
        public CommitmentServiceTest()
        {
            _repo = new Mock<IInvestorRepository>();
            _service = new CommitmentService(_repo.Object);
        }
        [Test]
        public void GetCommitments_ReturnsInvestors()
        {
            //arrange
            string investor = "test";
            var data = new CommitmentsDto() { Commitments = new List<Commitment>() { new Commitment() } };
            _repo.Setup(i => i.GetCommitments(It.IsAny<string>())).Returns(data);
            //act
            var res = _service.GetCommitments(investor);
            //assert
            Assert.AreEqual(data.Commitments.Count, res.Commitments.Count);
        }
    }
}
