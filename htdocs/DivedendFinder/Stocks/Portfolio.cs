using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockMarket
{
    public class PortfolioEntry
    {
        public List<StockMarket.Company> Holdings { get; set; }
        public double Value{ get; set; }
        public DateTime Date { get; set; }
        public PortfolioEntry()
        {
            this.Date = DateTime.Now;
            this.Holdings = null;
            this.Value = 0;
        }
    }
}
