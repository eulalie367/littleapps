using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace StockMarket
{
    public class YahooQuote
    {
        public StockMarket.HistoricalQuote Quote { get; set; }
        public string Ticker { get; set; }
        public string IndustryID { get; set; }
        public YahooQuote()
        {
            this.Ticker = "";
            this.IndustryID = "";
        }

        public YahooQuote(string ticker)
            : base()
        {
            if (!string.IsNullOrEmpty(ticker))
                this.Ticker = ticker.ToUpper();
        }

        public void GetQuote()
        {
            if (this.Ticker == "")
                throw new Exception("You need a ticker to get a quote");

            string url = @"http://download.finance.yahoo.com/d/quotes.csv?s=" + this.Ticker + "&f=sl1d1t1c1ohgv&e=.csv";
            string csv = FetchFile(url);
            Parse(csv);
        }

        public void GetQuote(string ticker)
        {
            this.Ticker = ticker;
            GetQuote();
        }

        public List<StockMarket.HistoricalQuote> GetHistory()
        {
            string url = @"http://ichart.finance.yahoo.com/table.csv?s=" + this.Ticker + "&ignore=.csv";
            string csv = FetchFile(url);
            return ParseRows(csv);
        }

        #region Private Functions

        private string FetchFile(string url)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string csv = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    using (System.IO.Stream strm = resp.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(strm))
                        {
                            csv = sr.ReadToEnd();
                            sr.Close();
                        }
                        strm.Close();
                    }
                    resp.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return csv;
        }

        private void Parse(string csv)
        {
            string[] props = csv.Replace("\"", "").Replace("N/A", "0").Replace("\r", "").Replace("\n", "").Split(",".ToCharArray());
            this.Quote = new StockMarket.HistoricalQuote
            {
                Ticker = props[0] ?? "",
                LastPrice = double.Parse(props[1] ?? "0"),
                LastTime = DateTime.Parse((props[2] ?? "0") == "0" ? "1/1/1900" : props[2] + " " + (props[3] ?? "0") == "0" ? "12:00AM" : props[3]),
                PriceChange = double.Parse(props[4] ?? "0"),
                OpenPrice = double.Parse(props[5] ?? "0"),
                HighPrice = double.Parse(props[6] ?? "0"),
                LowPrice = double.Parse(props[7] ?? "0"),
                Volume = int.Parse(props[8] ?? "0"),
            };
        }

        private List<StockMarket.HistoricalQuote> ParseRows(string csv)
        {
            string[] rows = csv.Replace("\"", "").Split("\n".ToCharArray());
            List<StockMarket.HistoricalQuote> historicalQuotes = new List<StockMarket.HistoricalQuote>();
            for (int i = 1; i < rows.Length; i++)//first row is a header
            {
                string csvRow = rows[i];
                if (csvRow != "")
                {
                    string[] cols = csvRow.Replace("\"", "").Split(",".ToCharArray());
                    StockMarket.HistoricalQuote q = new StockMarket.HistoricalQuote
                    {
                        Ticker = this.Ticker,
                        LastTime = DateTime.Parse((cols[0] ?? "1/1/1900") + " " + ("6:00PM")),
                        OpenPrice = double.Parse(cols[1] ?? "0"),
                        HighPrice = double.Parse(cols[2] ?? "0"),
                        LowPrice = double.Parse(cols[3] ?? "0"),
                        LastPrice = double.Parse(cols[4] ?? "0"),
                        Volume = int.Parse(cols[5] ?? "0"),
                        //CreatedTime = DateTime.Now,
                        //IsAfterClose = true
                    };
                    q.PriceChange = q.LastPrice - q.OpenPrice;
                    historicalQuotes.Add(q);
                }
            }
            return historicalQuotes;
        }


        #endregion
    }
    public class HistoricalQuote
    {
        public string Ticker { get; set; }
        public double LastPrice { get; set; }
        public DateTime LastTime { get; set; }
        public double PriceChange { get; set; }
        public double OpenPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public int Volume { get; set; }
    }
}
