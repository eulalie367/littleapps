using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using StandardExtensions;

namespace DivedendFinder
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private TextBox textBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(848, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Quote";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(-2, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(759, 459);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Get History";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(653, 458);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Update Sectors";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(-2, 31);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(258, 262);
            this.textBox2.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(928, 484);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
        private string connString = @"Data Source=DELLLAPTOP; Initial Catalog=Stocks; Integrated Security=SSPI;";
        private string screener = @"http://finance.google.com/finance/stockscreener#c0=MarketCap&min0=137860000&max0=470620000000&c1=PE&min1=0.25&max1=124100&c2=Price52WeekPercChange&min2=-98.13&max2=866&c3=IAD&min3=0&max3=16.82&c4=ForwardPE1Year&min4=-3.45&max4=747&c5=CurrentRatioYear&min5=1.65&max5=432&c6=DividendPerShare&min6=0.02&max6=15.27&c7=QuoteLast&min7=0.04&max7=127100&c8=High52Week&min8=0.26&max8=199000&c9=Low52Week&min9=0.01&max9=107200&c10=EPSGrowthRate5Years&min10=-69.88&max10=288&exchange=AllExchanges&sector=AllSectors&sort=&sortOrder=";
        //private void button1_Click(object sender, System.EventArgs e)
        //{
        //    //GetPortfolioQuote();
        //    YahooQuote y = new YahooQuote(textBox1.Text);
        //    List<StockMarket.HistoricalQuote> q = new List<StockMarket.HistoricalQuote>();
        //    q.Add(y.Quote);
        //    dataGridView1.DataSource = q;
        //}
        private void button2_Click_1(object sender, EventArgs e)
        {
            GetAllHistory(50);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            GetAllQuotes(500);
        }

        private void GetAllQuotes(int UpdateCount)
        {
            Google g = new Google();
            using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
            {
                IEnumerable<string> tickers = g.Quotes.Select(q => q.Ticker).Distinct();

                List<StockMarket.Company> updates = dc.Company.Where(c => tickers.Contains(c.Ticker)).ToList();
                IEnumerable<StockMarket.Company> inserts = g.Quotes.Where(t => !updates.Select(u => u.Ticker).Contains(t.Ticker));
                for (int i = 0; i < updates.Count(); i++)
                {
                    StockMarket.Company c = updates[i];
                    IEnumerable<StockMarket.Company> updateTo = g.Quotes.Where(q => q.Ticker == c.Ticker);
                    if (updateTo.Count() > 0)
                    {
                        c = updates.FirstOrDefault();
                    }
                }
                dc.Company.InsertAllOnSubmit(inserts);


                IEnumerable<StockMarket.CompanyStats> stats = g.Quotes.Select(q => q.CompanyStats.FirstOrDefault());

                List<StockMarket.CompanyStats> up = dc.CompanyStats.Where(c => stats.Select(s => s.Company.Ticker).Contains(c.Company.Ticker)).ToList();
                IEnumerable<StockMarket.CompanyStats> ins = stats.Where(s => !up.Select(u => u.Company.Ticker).Contains(s.Company.Ticker));
                for (int i = 0; i < up.Count(); i++)
                {
                    StockMarket.CompanyStats c = up[i];
                    IEnumerable<StockMarket.CompanyStats> updateTo = g.Quotes.Select(q => q.CompanyStats).FirstOrDefault().Where(q => q.CompanyID == c.CompanyID);
                    if (updateTo.Count() > 0)
                    {
                        c = up.FirstOrDefault();
                    }
                }
                dc.CompanyStats.InsertAllOnSubmit(ins);

                
                
                
                
                dc.SubmitChanges();

                string a = "";
            }
        }

        private void GetAllHistory(int Count)
        {
            List<StockMarket.Company> comps = new List<StockMarket.Company>();
            using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
            {

                comps.AddRange(dc.Company);
            }

            foreach (StockMarket.Company comp in comps)
            {
                QuoteHistroyEventArgs e = new QuoteHistroyEventArgs { ticker = comp.Ticker, count = Count };
                System.Threading.ThreadPool.SetMaxThreads(10, 2);
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(GetQuoteHistory), e);
            }
                
        }
        private class QuoteHistroyEventArgs
        {
            public string ticker { get; set; }
            public int count { get; set; }
        }
        private void GetQuoteHistory(object quoteHistroyEventArgs)
        {
            DateTime start = DateTime.Now;
            QuoteHistroyEventArgs e = (QuoteHistroyEventArgs)quoteHistroyEventArgs;

            YahooQuote y = new YahooQuote(e.ticker);

            using (System.Data.SqlClient.SqlBulkCopy bc = new System.Data.SqlClient.SqlBulkCopy(connString))
            {
                bc.BatchSize = 1000;
                bc.DestinationTableName = "dbo.HistoricalQuote";
                bc.WriteToServer(y.GetHistory().AsDataReader());
            }
            //using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
            //{
            //    IEnumerable<StockMarket.HistoricalQuote> quotes = y.GetHistory().Where(yahoo =>
            //        dc.HistoricalQuote.Where(h => h.LastTime == yahoo.LastTime
            //            && h.Ticker == yahoo.Ticker
            //        ).Count() == 0
            //        );//.Take(e.count);//limit to #count at a time

            //    ;
            //}

            double finishedOn = DateTime.Now.Subtract(start).TotalSeconds;
            this.SetTextThreadSafe(textBox2, string.Format("Obtained History for {0} in {1} seconds\n\r", new string[] { e.ticker, finishedOn.ToString() }));
        }

        //private void GetPortfolioQuote()
        //{
        //    using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
        //    {
        //        IEnumerable<StockMarket.PortFolioEntry> portfolio = dc.PortFolioEntry.Where(p => (p.PurchaseDate ?? new DateTime(1900, 1, 1)) != new DateTime(1900, 1, 1));

        //        foreach (StockMarket.PortFolioEntry entry in portfolio)
        //        {
        //            YahooQuote y = new YahooQuote(entry.Ticker);
        //            dc.HistoricalQuote.InsertOnSubmit(y.Quote);
        //        }
        //        dc.SubmitChanges();

        //        IEnumerable<StockMarket.HistoricalQuote> quotes = dc.HistoricalQuote.OrderByDescending(q => q.CreatedTime);
        //        dataGridView1.DataSource = quotes;
        //    }
        //}

        //private void UpdateIndustries()
        //{
        //    using (StockMarket.Stocks dc = new StockMarket.Stocks(connString))
        //    {
        //        foreach (StockMarket.Sector sec in dc.Sector)
        //        {
        //            YahooQuote y = new YahooQuote();
        //            for (int i = 10; i < 1000; i++)//i=10//starts at 110 now changed i=0 to allow variance
        //            {
        //                try
        //                {
        //                    y.IndustryID = (sec.SectorID * 100 + i).ToString();
        //                    StockMarket.Sector ind = y.UpdateIndustry();
        //                    IEnumerable<StockMarket.Industry> industry = ind.Industry.Where(indus => !dc.Industry.Select(indust => indust.Name).Contains(indus.Name));
        //                    IEnumerable<StockMarket.Company> insertCompanies = industry.FirstOrDefault().Company.Where(comp => !dc.Company.Select(c => c.Name).Contains(comp.Name));
        //                    if (industry.Count() > 0)
        //                    {
        //                        sec.Industry.AddRange(industry);
        //                    }
        //                    dc.Company.InsertAllOnSubmit(insertCompanies);
        //                }
        //                catch (Exception e)
        //                {
        //                    if(i > 15)// allow for variance in the first url
        //                        break;
        //                }
        //            }
        //        }
        //        dc.SubmitChanges();
        //    }
        //}

	}
}
 