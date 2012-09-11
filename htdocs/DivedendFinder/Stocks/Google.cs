using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Web;

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
                        EPS = double.Parse(company.Element("EPS").Value ?? "-1"),
                        PriceSales = double.Parse(company.Element("PriceSales").Value ?? "-1"),
                        LastPrice = double.Parse(company.Element("QuoteLast").Value ?? "-1"),
                        Volume = RawGoogle.ParseLargeInt(company.Element("Volume").Value ?? "-1"),
                        CreatedDate = DateTime.Now
                    };
                if (s.LastPrice > 0 && s.EPS > 0)
                    s.Earnings = (long)(s.EPS * ((double)s.MarketCap / s.LastPrice));

                s.BookValue = (int)(double.Parse(company.Element("BookValuePerShareYear").Value ?? "-1") / (s.Volume ?? 1));

                q.CompanyStats.Add(s);
                this.Quotes.Add(q);
            }
        }
    }
    class RawGoogle
    {
        public class Query
        {
            #region Default Constructor
            public Query()
            {
                Exchange = Exchanges.AMEX | Exchanges.NASDAQ | Exchanges.NYSE;

                MarketCap_Max = null;
                MarketCap_Min = null;

                PE_Max = null;
                PE_Min = null;

                DividendYield_Max = null;
                DividendYield_Min = null;

                PriceWeekPercChange_Max = null;
                PriceWeekPercChange_Min = null;

                ForwardPEYear_Max = null;
                ForwardPEYear_Min = null;

                QuotePercChange_Max = null;
                QuotePercChange_Min = null;

                PriceToBook_Max = null;
                PriceToBook_Min = null;

                PriceSales_Max = null;
                PriceSales_Min = null;

                QuoteLast_Max = null;
                QuoteLast_Min = null;

                EPS_Max = null;
                EPS_Min = null;

                High52Week_Max = null;
                High52Week_Min = null;

                Low52Week_Max = null;
                Low52Week_Min = null;

                Price50DayAverage_Max = null;
                Price50DayAverage_Min = null;

                Price150DayAverage_Max = null;
                Price150DayAverage_Min = null;

                Price200DayAverage_Max = null;
                Price200DayAverage_Min = null;

                Price13WeekPercChange_Max = null;
                Price13WeekPercChange_Min = null;

                Price26WeekPercChange_Max = null;
                Price26WeekPercChange_Min = null;

                DividendRecentQuarter_Max = null;
                DividendRecentQuarter_Min = null;

                DPSRecentYear_Max = null;
                DPSRecentYear_Min = null;

                IAD_Max = null;
                IAD_Min = null;

                DividendPerShare_Max = null;
                DividendPerShare_Min = null;

                Dividend_Max = null;
                Dividend_Min = null;

                BookValuePerShareYear_Max = null;
                BookValuePerShareYear_Min = null;

                CurrentRatioYear_Max = null;
                CurrentRatioYear_Min = null;

                LTDebtToAssetsYear_Max = null;
                LTDebtToAssetsYear_Min = null;

                LTDebtToAssetsQuarter_Max = null;
                LTDebtToAssetsQuarter_Min = null;

                TotalDebtToAssetsYear_Max = null;
                TotalDebtToAssetsYear_Min = null;

                TotalDebtToAssetsQuarter_Max = null;
                TotalDebtToAssetsQuarter_Min = null;

                LTDebtToEquityYear_Max = null;
                LTDebtToEquityYear_Min = null;

                LTDebtToEquityQuarter_Max = null;
                LTDebtToEquityQuarter_Min = null;

                TotalDebtToEquityYear_Max = null;
                TotalDebtToEquityYear_Min = null;

                TotalDebtToEquityQuarter_Max = null;
                TotalDebtToEquityQuarter_Min = null;

                AINTCOV_Max = null;
                AINTCOV_Min = null;

                ReturnOnInvestmentTTM_Max = null;
                ReturnOnInvestmentTTM_Min = null;

                ReturnOnInvestmentYears_Max = null;
                ReturnOnInvestmentYears_Min = null;

                ReturnOnInvestmentYear_Max = null;
                ReturnOnInvestmentYear_Min = null;

                ReturnOnAssetsTTM_Max = null;
                ReturnOnAssetsTTM_Min = null;

                ReturnOnAssetsYears_Max = null;
                ReturnOnAssetsYears_Min = null;

                ReturnOnAssetsYear_Max = null;
                ReturnOnAssetsYear_Min = null;

                ReturnOnEquityTTM_Max = null;
                ReturnOnEquityTTM_Min = null;

                ReturnOnEquityYears_Max = null;
                ReturnOnEquityYears_Min = null;

                ReturnOnEquityYear_Max = null;
                ReturnOnEquityYear_Min = null;

                Beta_Max = null;
                Beta_Min = null;

                Float_Max = null;
                Float_Min = null;

                Volume_Max = null;
                Volume_Min = null;

                AverageVolume_Max = null;
                AverageVolume_Min = null;

                GrossMargin_Max = null;
                GrossMargin_Min = null;

                EBITDMargin_Max = null;
                EBITDMargin_Min = null;

                OperatingMargin_Max = null;
                OperatingMargin_Min = null;

                NetProfitMarginPercent_Max = null;
                NetProfitMarginPercent_Min = null;

                NetIncomeGrowthRateYears_Max = null;
                NetIncomeGrowthRateYears_Min = null;

                RevenueGrowthRate5Years_Max = null;
                RevenueGrowthRate5Years_Min = null;

                RevenueGrowthRate10Years_Max = null;
                RevenueGrowthRate10Years_Min = null;

                EPSGrowthRate5Years_Max = null;
                EPSGrowthRate5Years_Min = null;

                EPSGrowthRate10Years_Max = null;
                EPSGrowthRate10Years_Min = null;

            }
            #endregion

            public enum Exchanges
            {
                NONE = 0,
                NYSE = 1,
                NASDAQ = 2,
                AMEX = 3
            }

            #region Properties
            public Exchanges Exchange { get; set; }

            public long? MarketCap_Min { get; set; }
            public long? MarketCap_Max { get; set; }

            public double? PE_Min { get; set; }
            public double? PE_Max { get; set; }

            public double? DividendYield_Min { get; set; }
            public double? DividendYield_Max { get; set; }

            public double? PriceWeekPercChange_Min { get; set; }
            public double? PriceWeekPercChange_Max { get; set; }

            public double? ForwardPEYear_Min { get; set; }
            public double? ForwardPEYear_Max { get; set; }

            public double? QuotePercChange_Min { get; set; }
            public double? QuotePercChange_Max { get; set; }

            public double? PriceToBook_Min { get; set; }
            public double? PriceToBook_Max { get; set; }

            public double? PriceSales_Min { get; set; }
            public double? PriceSales_Max { get; set; }

            public double? QuoteLast_Min { get; set; }
            public double? QuoteLast_Max { get; set; }

            public double? EPS_Min { get; set; }
            public double? EPS_Max { get; set; }

            public double? High52Week_Min { get; set; }
            public double? High52Week_Max { get; set; }

            public double? Low52Week_Min { get; set; }
            public double? Low52Week_Max { get; set; }

            public double? Price50DayAverage_Min { get; set; }
            public double? Price50DayAverage_Max { get; set; }

            public double? Price150DayAverage_Min { get; set; }
            public double? Price150DayAverage_Max { get; set; }

            public double? Price200DayAverage_Min { get; set; }
            public double? Price200DayAverage_Max { get; set; }

            public double? Price13WeekPercChange_Min { get; set; }
            public double? Price13WeekPercChange_Max { get; set; }

            public double? Price26WeekPercChange_Min { get; set; }
            public double? Price26WeekPercChange_Max { get; set; }

            public double? DividendRecentQuarter_Min { get; set; }
            public double? DividendRecentQuarter_Max { get; set; }

            public double? DPSRecentYear_Min { get; set; }
            public double? DPSRecentYear_Max { get; set; }

            public double? IAD_Min { get; set; }
            public double? IAD_Max { get; set; }

            public double? DividendPerShare_Min { get; set; }
            public double? DividendPerShare_Max { get; set; }

            public double? Dividend_Min { get; set; }
            public double? Dividend_Max { get; set; }

            public double? BookValuePerShareYear_Min { get; set; }
            public double? BookValuePerShareYear_Max { get; set; }

            public double? CurrentRatioYear_Min { get; set; }
            public double? CurrentRatioYear_Max { get; set; }

            public double? LTDebtToAssetsYear_Min { get; set; }
            public double? LTDebtToAssetsYear_Max { get; set; }

            public double? LTDebtToAssetsQuarter_Min { get; set; }
            public double? LTDebtToAssetsQuarter_Max { get; set; }

            public double? TotalDebtToAssetsYear_Min { get; set; }
            public double? TotalDebtToAssetsYear_Max { get; set; }

            public double? TotalDebtToAssetsQuarter_Min { get; set; }
            public double? TotalDebtToAssetsQuarter_Max { get; set; }

            public double? LTDebtToEquityYear_Min { get; set; }
            public double? LTDebtToEquityYear_Max { get; set; }

            public double? LTDebtToEquityQuarter_Min { get; set; }
            public double? LTDebtToEquityQuarter_Max { get; set; }

            public double? TotalDebtToEquityYear_Min { get; set; }
            public double? TotalDebtToEquityYear_Max { get; set; }

            public double? TotalDebtToEquityQuarter_Min { get; set; }
            public double? TotalDebtToEquityQuarter_Max { get; set; }

            public double? AINTCOV_Min { get; set; }
            public double? AINTCOV_Max { get; set; }

            public double? ReturnOnInvestmentTTM_Min { get; set; }
            public double? ReturnOnInvestmentTTM_Max { get; set; }

            public double? ReturnOnInvestmentYears_Min { get; set; }
            public double? ReturnOnInvestmentYears_Max { get; set; }

            public double? ReturnOnInvestmentYear_Min { get; set; }
            public double? ReturnOnInvestmentYear_Max { get; set; }

            public double? ReturnOnAssetsTTM_Min { get; set; }
            public double? ReturnOnAssetsTTM_Max { get; set; }

            public double? ReturnOnAssetsYears_Min { get; set; }
            public double? ReturnOnAssetsYears_Max { get; set; }

            public double? ReturnOnAssetsYear_Min { get; set; }
            public double? ReturnOnAssetsYear_Max { get; set; }

            public double? ReturnOnEquityTTM_Min { get; set; }
            public double? ReturnOnEquityTTM_Max { get; set; }

            public double? ReturnOnEquityYears_Min { get; set; }
            public double? ReturnOnEquityYears_Max { get; set; }

            public double? ReturnOnEquityYear_Min { get; set; }
            public double? ReturnOnEquityYear_Max { get; set; }

            public double? Beta_Min { get; set; }
            public double? Beta_Max { get; set; }

            public double? Float_Min { get; set; }
            public double? Float_Max { get; set; }

            public long? Volume_Min { get; set; }
            public long? Volume_Max { get; set; }

            public long? AverageVolume_Min { get; set; }
            public long? AverageVolume_Max { get; set; }

            public long? GrossMargin_Min { get; set; }
            public long? GrossMargin_Max { get; set; }

            public long? EBITDMargin_Min { get; set; }
            public long? EBITDMargin_Max { get; set; }

            public long? OperatingMargin_Min { get; set; }
            public long? OperatingMargin_Max { get; set; }

            public double? NetProfitMarginPercent_Min { get; set; }
            public double? NetProfitMarginPercent_Max { get; set; }

            public double? NetIncomeGrowthRateYears_Min { get; set; }
            public double? NetIncomeGrowthRateYears_Max { get; set; }

            public double? RevenueGrowthRate5Years_Min { get; set; }
            public double? RevenueGrowthRate5Years_Max { get; set; }

            public double? RevenueGrowthRate10Years_Min { get; set; }
            public double? RevenueGrowthRate10Years_Max { get; set; }

            public double? EPSGrowthRate5Years_Min { get; set; }
            public double? EPSGrowthRate5Years_Max { get; set; }

            public double? EPSGrowthRate10Years_Min { get; set; }
            public double? EPSGrowthRate10Years_Max { get; set; }

            #endregion

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("(");
                if ((this.Exchange & Exchanges.AMEX) == Exchanges.AMEX)
                    sb.Append("(exchange:AMEX) OR ");
                if ((this.Exchange & Exchanges.NASDAQ) == Exchanges.NASDAQ)
                    sb.Append("(exchange:NASDAQ) OR ");
                if ((this.Exchange & Exchanges.NYSE) == Exchanges.NYSE)
                    sb.Append("(exchange:NYSE) OR ");
                sb.Remove(sb.Length - 4, 4);
                sb.Append(")");


                sb.Append("[");
                sb.Append(RangeToGQuery(MarketCap_Min, MarketCap_Max, "MarketCap"));

                sb.Append(RangeToGQuery(PE_Min, PE_Max, "PE"));

                sb.Append(RangeToGQuery(DividendYield_Min, DividendYield_Max, "DividendYield"));

                sb.Append(RangeToGQuery(PriceWeekPercChange_Min, PriceWeekPercChange_Max, "PriceWeekPercChange"));

                sb.Append(RangeToGQuery(ForwardPEYear_Min, ForwardPEYear_Max, "ForwardPEYear"));

                sb.Append(RangeToGQuery(QuotePercChange_Min, QuotePercChange_Max, "QuotePercChange"));

                sb.Append(RangeToGQuery(PriceToBook_Min, PriceToBook_Max, "PriceToBook"));

                sb.Append(RangeToGQuery(PriceSales_Min, PriceSales_Max, "PriceSales"));

                sb.Append(RangeToGQuery(QuoteLast_Min, QuoteLast_Max, "QuoteLast"));

                sb.Append(RangeToGQuery(EPS_Min, EPS_Max, "EPS"));

                sb.Append(RangeToGQuery(High52Week_Min, High52Week_Max, "High52Week"));

                sb.Append(RangeToGQuery(Low52Week_Min, Low52Week_Max, "Low52Week"));

                sb.Append(RangeToGQuery(Price50DayAverage_Min, Price50DayAverage_Max, "Price50DayAverage"));

                sb.Append(RangeToGQuery(Price150DayAverage_Min, Price150DayAverage_Max, "Price150DayAverage"));

                sb.Append(RangeToGQuery(Price200DayAverage_Min, Price200DayAverage_Max, "Price200DayAverage"));

                sb.Append(RangeToGQuery(Price13WeekPercChange_Min, Price13WeekPercChange_Max, "Price13WeekPercChange"));

                sb.Append(RangeToGQuery(Price26WeekPercChange_Min, Price26WeekPercChange_Max, "Price26WeekPercChange"));

                sb.Append(RangeToGQuery(DividendRecentQuarter_Min, DividendRecentQuarter_Max, "DividendRecentQuarter"));

                sb.Append(RangeToGQuery(DPSRecentYear_Min, DPSRecentYear_Max, "DPSRecentYear"));

                sb.Append(RangeToGQuery(IAD_Min, IAD_Max, "IAD"));

                sb.Append(RangeToGQuery(DividendPerShare_Min, DividendPerShare_Max, "DividendPerShare"));

                sb.Append(RangeToGQuery(Dividend_Min, Dividend_Max, "Dividend"));

                sb.Append(RangeToGQuery(BookValuePerShareYear_Min, BookValuePerShareYear_Max, "BookValuePerShareYear"));

                sb.Append(RangeToGQuery(CurrentRatioYear_Min, CurrentRatioYear_Max, "CurrentRatioYear"));

                sb.Append(RangeToGQuery(LTDebtToAssetsYear_Min, LTDebtToAssetsYear_Max, "LTDebtToAssetsYear"));

                sb.Append(RangeToGQuery(LTDebtToAssetsQuarter_Min, LTDebtToAssetsQuarter_Max, "LTDebtToAssetsQuarter"));

                sb.Append(RangeToGQuery(TotalDebtToAssetsYear_Min, TotalDebtToAssetsYear_Max, "TotalDebtToAssetsYear"));

                sb.Append(RangeToGQuery(TotalDebtToAssetsQuarter_Min, TotalDebtToAssetsQuarter_Max, "TotalDebtToAssetsQuarter"));

                sb.Append(RangeToGQuery(LTDebtToEquityYear_Min, LTDebtToEquityYear_Max, "LTDebtToEquityYear"));

                sb.Append(RangeToGQuery(LTDebtToEquityQuarter_Min, LTDebtToEquityQuarter_Max, "LTDebtToEquityQuarter"));

                sb.Append(RangeToGQuery(TotalDebtToEquityYear_Min, TotalDebtToEquityYear_Max, "TotalDebtToEquityYear"));

                sb.Append(RangeToGQuery(TotalDebtToEquityQuarter_Min, TotalDebtToEquityQuarter_Max, "TotalDebtToEquityQuarter"));

                sb.Append(RangeToGQuery(AINTCOV_Min, AINTCOV_Max, "AINTCOV"));

                sb.Append(RangeToGQuery(ReturnOnInvestmentTTM_Min, ReturnOnInvestmentTTM_Max, "ReturnOnInvestmentTTM"));

                sb.Append(RangeToGQuery(ReturnOnInvestmentYears_Min, ReturnOnInvestmentYears_Max, "ReturnOnInvestmentYears"));

                sb.Append(RangeToGQuery(ReturnOnInvestmentYear_Min, ReturnOnInvestmentYear_Max, "ReturnOnInvestmentYear"));

                sb.Append(RangeToGQuery(ReturnOnAssetsTTM_Min, ReturnOnAssetsTTM_Max, "ReturnOnAssetsTTM"));

                sb.Append(RangeToGQuery(ReturnOnAssetsYears_Min, ReturnOnAssetsYears_Max, "ReturnOnAssetsYears"));

                sb.Append(RangeToGQuery(ReturnOnAssetsYear_Min, ReturnOnAssetsYear_Max, "ReturnOnAssetsYear"));

                sb.Append(RangeToGQuery(ReturnOnEquityTTM_Min, ReturnOnEquityTTM_Max, "ReturnOnEquityTTM"));

                sb.Append(RangeToGQuery(ReturnOnEquityYears_Min, ReturnOnEquityYears_Max, "ReturnOnEquityYears"));

                sb.Append(RangeToGQuery(ReturnOnEquityYear_Min, ReturnOnEquityYear_Max, "ReturnOnEquityYear"));

                sb.Append(RangeToGQuery(Beta_Min, Beta_Max, "Beta"));

                sb.Append(RangeToGQuery(Float_Min, Float_Max, "Float"));

                sb.Append(RangeToGQuery(Volume_Min, Volume_Max, "Volume"));

                sb.Append(RangeToGQuery(AverageVolume_Min, AverageVolume_Max, "AverageVolume"));

                sb.Append(RangeToGQuery(GrossMargin_Min, GrossMargin_Max, "GrossMargin"));

                sb.Append(RangeToGQuery(EBITDMargin_Min, EBITDMargin_Max, "EBITDMargin"));

                sb.Append(RangeToGQuery(OperatingMargin_Min, OperatingMargin_Max, "OperatingMargin"));

                sb.Append(RangeToGQuery(NetProfitMarginPercent_Min, NetProfitMarginPercent_Max, "NetProfitMarginPercent"));

                sb.Append(RangeToGQuery(NetIncomeGrowthRateYears_Min, NetIncomeGrowthRateYears_Max, "NetIncomeGrowthRateYears"));

                sb.Append(RangeToGQuery(RevenueGrowthRate5Years_Min, RevenueGrowthRate5Years_Max, "RevenueGrowthRate5Years"));

                sb.Append(RangeToGQuery(RevenueGrowthRate10Years_Min, RevenueGrowthRate10Years_Max, "RevenueGrowthRate10Years"));

                sb.Append(RangeToGQuery(EPSGrowthRate5Years_Min, EPSGrowthRate5Years_Max, "EPSGrowthRate5Years"));

                sb.Append(RangeToGQuery(EPSGrowthRate10Years_Min, EPSGrowthRate10Years_Max, "EPSGrowthRate10Years"));

                sb.Remove(sb.ToString().LastIndexOf(" & "), 3);

                sb.Append("]");

                return HttpUtility.UrlEncode(sb.ToString());
            }

            private string RangeToGQuery(long? min, long? max, string queryVal)
            {
                if (min.HasValue && max.HasValue && !string.IsNullOrEmpty(queryVal))
                {
                    string retval = "({2} > {0} | {2} = {0}) & ({2} < {1} | {2} = {1}) & ";

                    return string.Format(retval, min.Value, max.Value, queryVal);
                }
                return "";
            }
            private string RangeToGQuery(double? min, double? max, string queryVal)
            {
                if (min.HasValue && max.HasValue && !string.IsNullOrEmpty(queryVal))
                {
                    string retval = "({2} > {0} | {2} = {0}) & ({2} < {1} | {2} = {1}) & ";

                    return string.Format(retval, min.Value.ToString(), max.Value.ToString(), queryVal);
                }
                return "";
            }
        }
        public List<GoogleCompany> searchresults { get; set; }
        public RawGoogle()
        {
            this.searchresults = new List<GoogleCompany>();
        }
        public void Load()
        {
            Query q = new Query 
            {
                Volume_Max = long.MaxValue,
                Volume_Min = 0,
                MarketCap_Max = long.MaxValue,
                MarketCap_Min = 0,

                CurrentRatioYear_Max = 1000000,
                CurrentRatioYear_Min = 0,
                DividendRecentQuarter_Max = 1000000,
                DividendRecentQuarter_Min = 0,
                High52Week_Max = 1000000,
                High52Week_Min = 0,
                Low52Week_Max = 1000000,
                Low52Week_Min = 0,
                PE_Max = 1000000,
                PE_Min = 0,
                PriceSales_Max = 1000000,
                PriceSales_Min = 0,
                QuoteLast_Max = 1000000,
                QuoteLast_Min = 0,
                BookValuePerShareYear_Max = 1000000,
                BookValuePerShareYear_Min = 0,
                EPS_Max = 1000000,
                EPS_Min = 0
            };

            Load(q);
        }
        public void Load(Query q)
        {
            string url = "http://www.google.com/finance?&gl=us&hl=en&output=json&start=0&num=10000&noIL=1&restype=company&q=" + q.ToString();
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
        public double EPS { get; set; }
        public double PriceSales { get; set; }
        public double LastPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BookValue { get; set; }
        public long? Volume { get; set; }
        public long MarketCap { get; set; }
        public long Earnings { get; set; }
    }
}
