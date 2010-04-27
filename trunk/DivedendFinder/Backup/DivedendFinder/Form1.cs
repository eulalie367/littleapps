using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Web;
using System.Net;

namespace DivedendFinder
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(632, 456);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(704, 456);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "richTextBox1";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(704, 484);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

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

		private void button1_Click(object sender, System.EventArgs e)
		{
			System.IO.Stream strm;
			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@"http://finance.google.com/finance/stockscreener#c0=MarketCap&min0=137860000&max0=470620000000&c1=PE&min1=0.25&max1=124100&c2=Price52WeekPercChange&min2=-98.13&max2=866&c3=IAD&min3=0&max3=16.82&c4=ForwardPE1Year&min4=-3.45&max4=747&c5=CurrentRatioYear&min5=1.65&max5=432&c6=DividendPerShare&min6=0.02&max6=15.27&c7=QuoteLast&min7=0.04&max7=127100&c8=High52Week&min8=0.26&max8=199000&c9=Low52Week&min9=0.01&max9=107200&c10=EPSGrowthRate5Years&min10=-69.88&max10=288&exchange=AllExchanges&sector=AllSectors&sort=&sortOrder=");
				HttpWebResponse resp;
				req.Method = "Post";
				strm = req.GetRequestStream();
			}
			catch(Exception ex)
			{
			}
			finally
			{
				strm.Close();
			}
		}
	}
}
