using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Browser
{
    public partial class Form1 : Form
    {
        private WebSiteThumbnail c;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c = new WebSiteThumbnail(textBox1.Text, 1275, 1650, @"C:\HTMLImages\");
            c.TimeOut = 60;
            System.IO.StreamReader sr = System.IO.File.OpenText("C:/TESTHTML.txt");
            //c.GetScreenShot("<html><body><div>Hello</div></body></html>");
            c.GetScreenShot(sr.ReadToEnd());
            sr.Close();
        }
    }
}
