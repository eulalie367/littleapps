using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions;
using System.Configuration;

namespace QueryStocks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<StockMarket.Company> companies = GetCurrentQuotes();


            companies = companies.
                Where(
                    c => c.CompanyStats != null && c.CompanyStats.FirstOrDefault() != null 
                    && c.CompanyStats.FirstOrDefault().MarketCap > 0 
                    && c.CompanyStats.FirstOrDefault().Earnings > 0
                    && c.CompanyStats.FirstOrDefault().MarketCap > 10000000000
                    )
                .OrderBy(c => c.CompanyStats.FirstOrDefault().PE)
                .ThenBy(
                c => (double)c.CompanyStats.FirstOrDefault().MarketCap / (double)c.CompanyStats.FirstOrDefault().Earnings 
                ).Take(25).ToList();

            string a = "";

            foreach (StockMarket.Company c in companies)
                Console.WriteLine(c.Ticker);
        }

        private static List<StockMarket.Company> GetCurrentQuotes()
        {
            Dictionary<string, DateTime> files = new Dictionary<string, DateTime>();

            string dir = ConfigurationManager.AppSettings["QuoteDirectory"];
            if (!string.IsNullOrEmpty(dir))
            {
                foreach (System.IO.FileInfo fi in new System.IO.DirectoryInfo(dir).GetFiles("AllQuotes_*"))
                {
                    string[] nameParts = fi.FullName.Split('_');

                    if (nameParts != null && nameParts.Length > 1)
                    {
                        DateTime d = DateTime.Parse(nameParts[1]);
                        if (d != null)
                        {
                            files.Add(fi.FullName, d);
                        }
                    }

                }
                string filename = files.OrderByDescending(f => f.Value).Select(f => f.Key).FirstOrDefault();

                return filename.DeSerialze_Binary<StockMarket.Company>().ToList();
            }
            return new List<StockMarket.Company>();
        }
    }
}
