using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Investor.Domain.Model
{
    public class InvestorModel
    {

        [Key]

        public int Id { get; set; }

        [Name("Investor Name")]
        public string Name { get; set; } = default!;

        [Name("Investory Type")]
        public string Type { get; set; }

        [Name("Investor Country")]
        public string Country { get; set; }

        [Name("Investor Date Added")]
        public DateTime DateAdded { get; set; }

        [Name("Investor Last Updated")]
        public DateTime LastUpdated { get; set; }

        [Name("Commitment Asset Class")]
        public string CommitmentAssetClass { get; set; }

        [Name("Commitment Amount")]
        public decimal CommitmentAmount { get; set; }

        [Name("Commitment Currency")]
        public string CommitmentCurrency { get; set; }
    }
}
