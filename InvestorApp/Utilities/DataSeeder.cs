using CsvHelper.Configuration;
using CsvHelper;
using Investor.Application.Utilities;
using Investor.Domain.Model;
using System.Globalization;

namespace InvestorApp.Utilities
{
    public static class DataSeeder
    {
        public static void SeedData(this AppDbContext context)
        {
            if (!context.Investors.Any())
            {
                var records = GetInvestorsDataFromCsv();
                context.Investors.AddRange(records);
                context.SaveChanges();
            }
        }

        public static List<InvestorModel> GetInvestorsDataFromCsv()
        {
            List<InvestorModel> records;
            string fullPath = Path.GetFullPath(AppConstants.csvPath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(fullPath);
            }
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                MissingFieldFound=null
            };
            using (var reader = new StreamReader(fullPath))

            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Read();
                csv.ReadHeader();
                records = csv.GetRecords<InvestorModel>().ToList();
            }
            int id = 1;
            records.ForEach(x => { x.Id = id++; });
            return records;
        }

    }
}
