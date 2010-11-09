using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace StockMarket
{
    public class Google
    {
        public List<StockMarket.Company> Quotes { get; set; }
        public Google()
        {
            this.Quotes = new List<StockMarket.Company>();
            RawGoogle g = new RawGoogle();
            g.Load();
            XDocument xdoc = new XDocument();
            using (System.IO.StringReader sr = new System.IO.StringReader(g.ToString()))
            {
                xdoc = XDocument.Load(sr);
            }
            foreach (XElement company in xdoc.Element("GoogleFinance").Elements().Where(e => e.Name == "Company"))
            {
                StockMarket.Company q = new StockMarket.Company
                {
                    Exchange = company.Element("Exchange").Value ?? "",
                    Ticker = company.Element("Ticker").Value ?? "",
                    Title = company.Element("Title").Value ?? "",
                };
                StockMarket.CompanyStats s = new StockMarket.CompanyStats
                    {
                        CurrentRatioYear = double.Parse(company.Element("CurrentRatioYear").Value ?? "-1"),
                        DividendRecentQuarter = double.Parse(company.Element("DividendRecentQuarter").Value ?? "-1"),
                        High52Week = double.Parse(company.Element("High52Week").Value ?? "-1"),
                        Low52Week = double.Parse(company.Element("Low52Week").Value ?? "-1"),
                        MarketCap = RawGoogle.ParseLargeInt(company.Element("MarketCap").Value ?? "-1"),
                        PE = double.Parse(company.Element("PE").Value ?? "-1"),
                        PriceSales = double.Parse(company.Element("PriceSales").Value ?? "-1"),
                        LastPrice = double.Parse(company.Element("QuoteLast").Value ?? "-1"),
                        Volume = RawGoogle.ParseLargeInt(company.Element("Volume").Value ?? "-1"),
                        CreatedDate = DateTime.Now
                    };
                if(s.LastPrice > 0 && s.PE > 0)
                    s.Earnings = (long)(((double)s.LastPrice / (double)s.PE) * (s.MarketCap / s.LastPrice));
    
                s.BookValue = (int)(double.Parse(company.Element("BookValuePerShareYear").Value ?? "-1") / (s.Volume ?? 1));

                q.CompanyStats.Add(s);
                this.Quotes.Add(q);
            }
        }
    }
    class RawGoogle
    {
        public int? Start { get; set; }
        public int? num_company_results { get; set; }
        public int? num_mf_results { get; set; }
        public int? num_all_results { get; set; }
        public string original_query { get; set; }
        public string query_for_display { get; set; }
        public string results_type { get; set; }
        public string results_display_type { get; set; }
        public List<GoogleCompany> searchresults { get; set; }
        public RawGoogle()
        {
            this.Start = -1;
            this.num_company_results = -1;
            this.num_mf_results = -1;
            this.num_all_results = -1;
            this.original_query = "";
            this.query_for_display = "";
            this.results_type = "";
            this.results_display_type = "";
            this.searchresults = new List<GoogleCompany>();
        }
        public void Load()
        {
            //string url = "http://www.google.com/finance?start=0&num=2805&q=%28%28exchange%3ANYSE%29%20OR%20%28exchange%3ANASDAQ%29%20OR%20%28exchange%3AAMEX%29%29%20%5B%28MarketCap%20%3E%209879%20%7C%20MarketCap%20%3D%209879%29%20%26%20%28MarketCap%20%3C%202270000000000%20%7C%20MarketCap%20%3D%202270000000000%29%20%26%20%28PE%20%3E%200.04%20%7C%20PE%20%3D%200.04%29%20%26%20%28PE%20%3C%208263%20%7C%20PE%20%3D%208263%29%20%26%20%28DividendYield%20%3E%200%20%7C%20DividendYield%20%3D%200%29%20%26%20%28DividendYield%20%3C%20478%20%7C%20DividendYield%20%3D%20478%29%20%26%20%28Price52WeekPercChange%20%3E%20-99.83%20%7C%20Price52WeekPercChange%20%3D%20-99.83%29%20%26%20%28Price52WeekPercChange%20%3C%205001%20%7C%20Price52WeekPercChange%20%3D%205001%29%20%26%20%28BookValuePerShareYear%20%3E%20-235%20%7C%20BookValuePerShareYear%20%3D%20-235%29%20%26%20%28BookValuePerShareYear%20%3C%203000000%20%7C%20BookValuePerShareYear%20%3D%203000000%29%20%26%20%28CashPerShareYear%20%3E%200%20%7C%20CashPerShareYear%20%3D%200%29%20%26%20%28CashPerShareYear%20%3C%2069161%20%7C%20CashPerShareYear%20%3D%2069161%29%5D&restype=company&output=json&noIL=1";
            string url = "http://www.google.com/finance?start=20&num=5000&&q=%28%28exchange%3ANYSE%29%20OR%20%28exchange%3ANASDAQ%29%20OR%20%28exchange%3AAMEX%29%29%20%5B%28PriceSales%20%3E%200%20%7C%20PriceSales%20%3D%200%29%20%26%20%28PriceSales%20%3C%2023600%20%7C%20PriceSales%20%3D%2023600%29%20%26%20%28QuoteLast%20%3E%200%20%7C%20QuoteLast%20%3D%200%29%20%26%20%28QuoteLast%20%3C%2090352%20%7C%20QuoteLast%20%3D%2090352%29%20%26%20%28High52Week%20%3E%200.24%20%7C%20High52Week%20%3D%200.24%29%20%26%20%28High52Week%20%3C%20200000%20%7C%20High52Week%20%3D%20200000%29%20%26%20%28Low52Week%20%3E%200%20%7C%20Low52Week%20%3D%200%29%20%26%20%28Low52Week%20%3C%2070051%20%7C%20Low52Week%20%3D%2070051%29%20%26%20%28PE%20%3E%200.04%20%7C%20PE%20%3D%200.04%29%20%26%20%28PE%20%3C%208238%20%7C%20PE%20%3D%208238%29%20%26%20%28MarketCap%20%3E%209939%20%7C%20MarketCap%20%3D%209939%29%20%26%20%28MarketCap%20%3C%202270000000000%20%7C%20MarketCap%20%3D%202270000000000%29%20%26%20%28DividendRecentQuarter%20%3E%200%20%7C%20DividendRecentQuarter%20%3D%200%29%20%26%20%28DividendRecentQuarter%20%3C%207.01%20%7C%20DividendRecentQuarter%20%3D%207.01%29%20%26%20%28BookValuePerShareYear%20%3E%20-235%20%7C%20BookValuePerShareYear%20%3D%20-235%29%20%26%20%28BookValuePerShareYear%20%3C%203000000%20%7C%20BookValuePerShareYear%20%3D%203000000%29%20%26%20%28Volume%20%3E%200%20%7C%20Volume%20%3D%200%29%20%26%20%28Volume%20%3C%20288270000%20%7C%20Volume%20%3D%20288270000%29%20%26%20%28CurrentRatioYear%20%3E%200.02%20%7C%20CurrentRatioYear%20%3D%200.02%29%20%26%20%28CurrentRatioYear%20%3C%201596%20%7C%20CurrentRatioYear%20%3D%201596%29%5D&restype=company&output=json&noIL=1";
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            jss.MaxJsonLength = 80000000;
            string json = FetchFile(url);

            RawGoogle g = jss.Deserialize<RawGoogle>(json.Replace("\n", "").Replace("\\", ""));
            foreach (System.Reflection.PropertyInfo p in g.GetType().GetProperties())
            {
                this.GetType().GetProperty(p.Name).SetValue(this, p.GetValue(g, null), null);
            }
        }
        public static long ParseLargeInt(string val)
        {
            long retVal = -1;
            try
            {
                if (val.IndexOf("M") > 0)
                {
                    retVal = (long)(double.Parse(val.Replace("M", "")) * 1000000);
                }
                else if (val.IndexOf("B") > 0)
                {
                    retVal = (long)(double.Parse(val.Replace("B", "")) * 1000000000);
                }
            }
            catch
            {
                retVal = -1;
            }
            return retVal;
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
        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder("<GoogleFinance");

            foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties().OrderBy(prop => prop.Name))
            {
                if (p.PropertyType != typeof(List<GoogleCompany>))
                {
                    retVal.AppendFormat(" {0}=\"{1}\"", new object[] { p.Name, (p.GetValue(this, null) ?? "").ToString() });
                }
            }
            retVal.Append(">");
            foreach (GoogleCompany comp in this.searchresults.OrderBy(sr => sr.ticker))
            {
                retVal.Append(comp.ToString());
            }
            retVal.Append("\n</GoogleFinance>");
            return retVal.ToString();
        }
        public class GoogleColumn
        {
            public string display_name { get; set; }
            public string value { get; set; }
            public string field { get; set; }
            public string sort_order { get; set; }
            public GoogleColumn()
            {
                this.display_name = "";
                this.value = "";
                this.field = "";
                this.sort_order = "";
            }
            public override string ToString()
            {
                StringBuilder retVal = new StringBuilder();

                retVal.AppendFormat("\n\t\t<{0}>{1}</{0}>", new object[] { this.field, this.value });

                return retVal.ToString();

            }
        }
        public class GoogleCompany
        {
            public string title { get; set; }
            public int id { get; set; }
            public bool? is_active { get; set; }
            public string ticker { get; set; }
            public string exchange { get; set; }
            public bool? is_supported_exchange { get; set; }
            public List<GoogleColumn> columns { get; set; }
            public GoogleCompany()
            {
                this.title = "";
                this.id = -1;
                this.is_active = false;
                this.ticker = "";
                this.exchange = "";
                this.is_supported_exchange = false;
                this.columns = new List<GoogleColumn>();
            }
            public override string ToString()
            {
                StringBuilder retVal = new StringBuilder("\n\t<Company");

                foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties().OrderBy(prop => prop.Name))
                {
                    if (p.PropertyType != typeof(List<GoogleColumn>) && p.Name.ToLower() != "ticker" && p.Name.ToLower() != "title" && p.Name.ToLower() != "exchange")
                    {
                        retVal.AppendFormat(" {0}=\"{1}\"", new object[] { p.Name, (p.GetValue(this, null) ?? "").ToString() });
                    }
                }
                retVal.Append(">");
                retVal.AppendFormat("\n\t\t<Ticker>{0}</Ticker>", new object[] { this.ticker });
                retVal.AppendFormat("\n\t\t<Title>{0}</Title>", new object[] { this.title });
                retVal.AppendFormat("\n\t\t<Exchange>{0}</Exchange>", new object[] { this.exchange });
                foreach (GoogleColumn c in this.columns.OrderBy(cols => cols.field))
                {
                    retVal.Append(c.ToString());
                }

                retVal.Append("\n\t</Company>");
                return retVal.ToString();

            }
        }
    }

    [Serializable]
    public class Company
    {
        public string Exchange { get; set; }
        public string Ticker { get; set; }
        public string Title { get; set; }
        public List<CompanyStats> CompanyStats { get; set; }
        public Company()
        {
            CompanyStats = new List<CompanyStats>();
        }
    }
    [Serializable]
    public class CompanyStats
    {
        public double CurrentRatioYear { get; set; }
        public double DividendRecentQuarter { get; set; }
        public double High52Week { get; set; }
        public double Low52Week { get; set; }
        public double PE { get; set; }
        public double PriceSales { get; set; }
        public double LastPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BookValue { get; set; }
        public long? Volume { get; set; }
        public long MarketCap { get; set; }
        public long Earnings { get; set; }
    }
}
