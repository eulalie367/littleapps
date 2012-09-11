using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions;
using System.Configuration;
using System.IO;

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
                    && c.CompanyStats.FirstOrDefault().Earnings > 0
                    && c.CompanyStats.FirstOrDefault().MarketCap > 10000000000
                    ).ToList();
            //    .OrderBy(c => c.CompanyStats.FirstOrDefault().PE)
            //    .ThenBy(
            //    c => (double)c.CompanyStats.FirstOrDefault().MarketCap / (double)c.CompanyStats.FirstOrDefault().Earnings 
            //    ).Take(25).ToList();



            List<StockMarket.Company> pe = companies.OrderBy(c => c.CompanyStats.FirstOrDefault().PE).Take(50).ToList();
            List<StockMarket.Company> cpe = companies.OrderBy(c => (double)c.CompanyStats.FirstOrDefault().MarketCap / (double)c.CompanyStats.FirstOrDefault().Earnings).Take(50).ToList();

            List<StockMarket.Company> matches = new List<StockMarket.Company>();
            foreach (StockMarket.Company c in cpe)
            {
                if (pe.Contains(c))
                    matches.Add(c);
                if (matches.Count == 25)
                    break;
            }

            RunTrial("test",DateTime.Now.AddYears(-5));

            foreach (StockMarket.Company c in matches)
                Console.WriteLine(c.Ticker);


            //matches.Serialze_XML(ConfigurationManager.AppSettings["QuoteDirectory"] + "/Picks_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xml");
        }

        private static void RunTrial(string portfolioNam, DateTime startDate)
        {
            //TODO: Need to find better historical data for this to be affective.
            List<StockMarket.HistoricalQuote> all = GetAllHistories();
            DateTime current = startDate;

            while (current <= DateTime.Now)
            {
                //all.
                //    Where(
                //        c => c.m != null && c.CompanyStats.FirstOrDefault() != null
                //        && c.CompanyStats.FirstOrDefault().Earnings > 0
                //        && c.CompanyStats.FirstOrDefault().MarketCap > 10000000000
                //        ).ToList();
   

                current.AddDays(1);
            }
        }

        private static List<StockMarket.HistoricalQuote> GetAllHistories()
        {
            List<StockMarket.HistoricalQuote> quotes = new List<StockMarket.HistoricalQuote>();
            string dir = ConfigurationManager.AppSettings["QuoteDirectory"];
            if (!string.IsNullOrEmpty(dir))
            {
                foreach (System.IO.FileInfo fi in new System.IO.DirectoryInfo(dir).GetFiles())
                {
                    if (fi.FullName.IndexOf("AllQuotes") < 0)
                    {
                        List<StockMarket.HistoricalQuote> tmp = fi.FullName.DeSerialze_Binary<List<StockMarket.HistoricalQuote>>();
                        if(tmp.Count() > 0)
                            quotes.AddRange(tmp);
                    }
                }
            }
            return quotes;
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

                return filename.DeSerialze_Binary <List<StockMarket.Company>>().ToList();
            }
            return new List<StockMarket.Company>();
        }
    }
}
