namespace Investor.Infrastructure.Utility
{
    public static class NumberFormatter
    {
        public static string FormatToMillionAndBillion(this decimal number)
        { 
            // Check if the number can be formatted as billion or million
            if (number >= 1_000_000_000)
            {
                decimal inBillions = number / 1_000_000_000m;
                return inBillions.ToString("N2") + " B"; 
            }
            else if (number >= 1_000_000)
            {
                decimal inMillions = number / 1_000_000m;
                return inMillions.ToString("N0") + " M"; 
            }
            else
            {
                return number.ToString();
            }

        }
    }

}
