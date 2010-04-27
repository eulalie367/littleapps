using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaintainStocks
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            try
            {
                string command = args.Length > 0 ? args[0] : Console.ReadLine().ToLower();
                Console.WriteLine(string.Format("Running command {0}()", new string[] { command.ToUpperInvariant() }));
                switch (command)
                {
                    case "update":
                        StockMaintainer.GetAllQuotes();
                        break;
                    case "history":
                        StockMaintainer.GetAllHistory();
                        break;
                }
                Console.WriteLine(string.Format("Completed in {0} seconds", DateTime.Now.Subtract(start).TotalSeconds.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occured");
                Console.WriteLine(e.Message + "\n\n" + e.StackTrace);
                
            }
        }
    }
}
