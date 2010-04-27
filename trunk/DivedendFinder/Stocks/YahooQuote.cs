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
            this.Ticker = ticker.ToUpper();
            //this.GetQuote();
        }

        public List<StockMarket.HistoricalQuote> GetHistory()
        {
            string url = @"http://ichart.finance.yahoo.com/table.csv?s=" + this.Ticker + "&ignore=.csv";
            string csv = FetchFile(url);
            return ParseRows(csv);
        }

        //public StockMarket.Sector UpdateIndustry()
        //{
        //    if (this.IndustryID == "")
        //        throw new Exception("You need an IndustryID to get a quote");

        //    string csv = FetchFile(@"http://biz.yahoo.com/p/csv/" + this.IndustryID + "conameu.csv");

        //    if (csv == "")
        //        throw new Exception("the csv file does not exist");//TODO:Make custom exception

        //    return this.ParseIndustry(csv);
        //}

        //public StockMarket.Sector UpdateIndustry(string industryID)
        //{
        //    this.IndustryID = industryID;
        //    return this.UpdateIndustry();
        //}

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
                //CreatedTime = DateTime.Now,
                //IsAfterClose = DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0)
                //                || DateTime.Now.DayOfWeek.ToString().ToLower() == "sunday"
                //                || DateTime.Now.DayOfWeek.ToString().ToLower() == "monday"
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

        //private StockMarket.Sector ParseIndustry(string csv)
        //{
        //    //first row is headers
        //    //second row is sector
        //    //third row is industry
        //    //additional rows are companies
        //    string[] rows = csv.Replace("\0", "").Replace("NA", "").Split("\n".ToCharArray());
        //    StockMarket.Sector retVal = new StockMarket.Sector();
        //    for (int i = 1; i < rows.Length; i++)
        //    {
        //        string row = rows[i].Replace("\"", "");
        //        if (row != "")
        //        {
        //            string[] cols = row.Split(",".ToCharArray());
        //            if (cols.Length > 10)
        //            {
        //                string colfix = row.Substring(new System.Text.RegularExpressions.Regex(@",\d").Match(row).Index);
        //                cols = colfix.Split(",".ToCharArray());
        //                cols[0] = row.Replace(colfix, "");
        //            }
        //            if (cols.Length == 10)
        //            {
        //                switch (i)
        //                {
        //                    case 1://sector
        //                        break;
        //                    case 2://industry
        //                        retVal.Industry.Add(new StockMarket.Industry
        //                        {
        //                            Name = cols[0]
        //                        });
        //                        break;
        //                    default://company
        //                        try
        //                        {
        //                            StockMarket.Industry ind = retVal.Industry.FirstOrDefault();
        //                            string lookup = FetchFile("http://finance.yahoo.com/lookup?s=" + cols[0]);
        //                            string ticker = new System.Text.RegularExpressions.Regex("href=\"http://finance.yahoo.com/q\\?s=[A-Za-z0-9\\.]+").Match(lookup).ToString();
        //                            StockMarket.Company comp = new StockMarket.Company
        //                            {
        //                                Ticker = cols[0],
        //                                DividendPercent = (int)(double.Parse((cols[5] ?? "") == "" ? "0" : cols[5])),
        //                                ProfitMargin = (int)(double.Parse((cols[8] ?? "") == "" ? "0" : cols[8]))
        //                            };
        //                            string mCap = (cols[2] ?? "") == "" ? "0" : cols[2];
        //                            double cap;
        //                            if (mCap.IndexOf("B") > -1)
        //                                cap = double.Parse(mCap.Replace("B", "")) * (double)1000;
        //                            else
        //                                cap = double.Parse(mCap.Replace("M", ""));
        //                            comp.MarketCapMillions = cap;

        //                            if (ticker != "")
        //                            {
        //                                ticker = ticker.Substring(ticker.IndexOf("s=") + 2);
        //                                YahooQuote quote = new YahooQuote(ticker);
        //                                comp.Name = ticker;
        //                                comp.LastClose = quote.Quote.LastPrice;
        //                                comp.Earnings = (int)(comp.LastClose / double.Parse((cols[3] ?? "") == "" ? "0" : cols[3]));
        //                                comp.BookValue = (int)(comp.LastClose / double.Parse((cols[7] ?? "") == "" ? "0" : cols[7]));
        //                                comp.CashFlow = (int)(comp.LastClose / double.Parse((cols[9] ?? "") == "" ? "0" : cols[9]));
        //                            }

        //                            ind.Company.Add(comp);
        //                        }
        //                        catch (Exception e)
        //                        {
        //                            string a = e.Message;
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    return retVal;
        //}
    }
}
