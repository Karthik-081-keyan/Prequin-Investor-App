using Investor.Domain.Model;
using Investor.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Investor.Infrastructure.Tests.Repository
{
    public class InvestorRepositoryTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private Mock<DbSet<InvestorModel>> _mockInvestorDbSet;
        private InvestorRepository _investorRepository;

        [SetUp]
        public void SetUp()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockInvestorDbSet = new Mock<DbSet<InvestorModel>>();

            // Initialize repository with the mocked DbContext
            _investorRepository = new InvestorRepository(_mockDbContext.Object);
        }

        // Helper method to create a mock DbSet from a list of entities
        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private IQueryable<InvestorModel> GetInvestorTestData()
        {
            var investors = new List<InvestorModel>
            {
                new InvestorModel { Id = 1, Name = "Investor A", DateAdded = new DateTime(2022, 1, 1), Type = "Type1", Country = "USA", CommitmentAmount = 1000000, CommitmentAssetClass = "Equity", CommitmentCurrency = "USD" },
                new InvestorModel { Id = 2, Name = "Investor A", DateAdded = new DateTime(2022, 2, 1), Type = "Type1", Country = "USA", CommitmentAmount = 2000000, CommitmentAssetClass = "Debt", CommitmentCurrency = "USD" },
                new InvestorModel { Id = 3, Name = "Investor B", DateAdded = new DateTime(2023, 1, 1), Type = "Type2", Country = "UK", CommitmentAmount = 3000000, CommitmentAssetClass = "Equity", CommitmentCurrency = "GBP" }
            };
            return investors.AsQueryable();
        }

        [Test]
        public void GetInvestors_ShouldReturnGroupedInvestorsList()
        {
            // Arrange
            var investorData = GetInvestorTestData();
            _mockInvestorDbSet = CreateMockDbSet(investorData); // Mock DbSet from test data
            _mockDbContext.Setup(db => db.Investors).Returns(_mockInvestorDbSet.Object); // Mock DbContext.Investors

            // Act
            var result = _investorRepository.GetInvestors();

            // Assert
            Assert.AreEqual(2, result.Count); // 2 grouped investors by name
            Assert.AreEqual("Investor A", result[0].Name);
            Assert.AreEqual("3M", result[0].TotalAmount); // Assuming FormatToMillionAndBillion converts to "3M"
            Assert.AreEqual("Investor B", result[1].Name);
            Assert.AreEqual("3M", result[1].TotalAmount);
        }

        [Test]
        public void GetCommitments_ShouldReturnCommitmentsDto_WithGroupedCommitments()
        {
            // Arrange
            var investorData = GetInvestorTestData();
            _mockInvestorDbSet = CreateMockDbSet(investorData); // Mock DbSet from test data
            _mockDbContext.Setup(db => db.Investors).Returns(_mockInvestorDbSet.Object); // Mock DbContext.Investors

            // Act
            var result = _investorRepository.GetCommitments("Investor A");

            // Assert
            Assert.AreEqual(3, result.Commitments.Count); // 2 asset classes + "All"
            Assert.AreEqual("Equity", result.Commitments[0].Type);
            Assert.AreEqual("1M", result.Commitments[0].Total); // Assuming FormatToMillionAndBillion converts to "1M"
            Assert.AreEqual("Debt", result.Commitments[1].Type);
            Assert.AreEqual("2M", result.Commitments[1].Total);
            Assert.AreEqual("All", result.Commitments[2].Type);
            Assert.AreEqual("3M", result.Commitments[2].Total);

            Assert.AreEqual(2, result.CommitmentsInfo.Count);
            Assert.AreEqual("Equity", result.CommitmentsInfo[0].AssetName);
            Assert.AreEqual("Debt", result.CommitmentsInfo[1].AssetName);
        }
    }
}