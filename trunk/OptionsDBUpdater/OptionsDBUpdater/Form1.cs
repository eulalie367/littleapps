using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxTWSLib;
using TWSLib;

namespace OptionsDBUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AxTws tws = new AxTws();
            IContract i = tws.createContract();
            i.symbol = "ener";
            i.expiry = DateTime.Now.AddMonths(3).ToString("mm/dd/yyyy");
            tws.reqFundamentalData(1, i, "esitmates"); 

        }
    }
}
