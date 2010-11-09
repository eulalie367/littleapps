using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions;
using StockMarket;

namespace MaintainXML
{
    class Program
    {
        static int threads = 0;
        static void Main(string[] args)
        {
            Google g = new Google();
            g.Quotes.Serialze_Binary("Quotes/AllQuotes_" + DateTime.UtcNow.ToString("MM-dd-yyyy"));
            Console.WriteLine("Saved All of today's quotes");

            //foreach (string stock in g.Quotes.Where(q => !string.IsNullOrEmpty(q.Ticker)).Select(q => q.Ticker))
            //{
            //    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(SaveQuote), stock);
            //    threads++;
            //}
            //WaitOnThreads();
        }

        private static void WaitOnThreads()
        {
            while (threads > 0)
            {
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                Console.WriteLine(threads.ToString() + " threads remaining");
            }
        }

        static void SaveQuote(object strTicker)
        {
            if (strTicker != null)
            {
                string ticker = strTicker as string;
                if (!string.IsNullOrEmpty(ticker))
                {
                    StockMarket.YahooQuote y = new StockMarket.YahooQuote(ticker);
                    List<StockMarket.HistoricalQuote> h = y.GetHistory();
                    h.Serialze_Binary("Quotes/" + y.Ticker);
                    Console.WriteLine("Saved Historical information for " + y.Ticker);
                }
            }
            threads--;
        }
    }
}
