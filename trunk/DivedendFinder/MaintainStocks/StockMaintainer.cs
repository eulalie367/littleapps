using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket;
using StandardExtensions;

namespace MaintainStocks
{
    public static class StockMaintainer
    {
        private static string connString = @"Data Source=DELLLAPTOP; Initial Catalog=Stocks; Integrated Security=SSPI;";
        private static string screener = @"http://finance.google.com/finance/stockscreener#c0=MarketCap&min0=137860000&max0=470620000000&c1=PE&min1=0.25&max1=124100&c2=Price52WeekPercChange&min2=-98.13&max2=866&c3=IAD&min3=0&max3=16.82&c4=ForwardPE1Year&min4=-3.45&max4=747&c5=CurrentRatioYear&min5=1.65&max5=432&c6=DividendPerShare&min6=0.02&max6=15.27&c7=QuoteLast&min7=0.04&max7=127100&c8=High52Week&min8=0.26&max8=199000&c9=Low52Week&min9=0.01&max9=107200&c10=EPSGrowthRate5Years&min10=-69.88&max10=288&exchange=AllExchanges&sector=AllSectors&sort=&sortOrder=";
        public static void GetAllQuotes()
        {
            Google g = new Google();
            using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
            {
                IEnumerable<string> tickers = g.Quotes.Select(q => q.Ticker).Distinct();
                Console.WriteLine(string.Format("retrieved {0} companies", new string[] { tickers.Count().ToString() }));
                dc.Company.InsertAll(g.Quotes);

                dc.CompanyStats.DeleteAll();
                IEnumerable<StockMarket.CompanyStats> stats = g.Quotes.Select(q => q.CompanyStats.FirstOrDefault());
                dc.CompanyStats.InsertAll(stats);
            }
        }


        public static void GetAllHistory()
        {
            List<StockMarket.Company> comps = new List<StockMarket.Company>();
            using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
            {
                comps.AddRange(dc.Company);
                dc.HistoricalQuote.DeleteAll();
            }

            foreach (StockMarket.Company comp in comps)
            {
                QuoteHistroyEventArgs e = new QuoteHistroyEventArgs { ticker = comp.Ticker };
                System.Threading.ThreadPool.SetMaxThreads(10, 2);
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(StockMaintainer.GetQuoteHistory), e);
            }

            System.Threading.Thread.Sleep(10000);
            int a, b;
            System.Threading.ThreadPool.GetAvailableThreads(out a, out b);
            while (a != 10)
            {
                System.Threading.Thread.Sleep(10000);
                System.Threading.ThreadPool.GetAvailableThreads(out a, out b);
            }
        }
        public class QuoteHistroyEventArgs
        {
            public string ticker { get; set; }
        }
        private static void GetQuoteHistory(object quoteHistroyEventArgs)
        {
            DateTime start = DateTime.Now;
            QuoteHistroyEventArgs e = (QuoteHistroyEventArgs)quoteHistroyEventArgs;

            YahooQuote y = new YahooQuote(e.ticker);

            using (StockMarket.Stocks dc = new Stocks(connString))
            {
                dc.HistoricalQuote.InsertAll(y.GetHistory());
            }

            Console.WriteLine(string.Format("Obtained History for {0} in {1} seconds\n\r", new string[] { e.ticker, DateTime.Now.Subtract(start).TotalSeconds.ToString() }));
        }

    }
}
