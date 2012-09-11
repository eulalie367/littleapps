using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTMLPageScan
{
    public partial class Form1 : Form
    {
        System.Timers.Timer tim;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tim = new System.Timers.Timer(25000);
            tim.Elapsed += new System.Timers.ElapsedEventHandler(CheckPageAgain);
            tim.Start();
            CheckPageAgain(null, null);
        }
        public void CheckPageAgain(object o, System.Timers.ElapsedEventArgs e)
        {
            CheckPageForPhrase();
        }

        private void CheckPageForPhrase()
        {
            string html = "";
            try
            {
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.CreateDefault(new Uri(tbAddress.Text));
                System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse();
                System.IO.StreamReader current = new System.IO.StreamReader(resp.GetResponseStream());
                html = current.ReadToEnd();
                current.Close();
                string path = @"C:\LittleApps\HTMLPageScan\lastCheck.html";

                if (System.IO.File.Exists(path))
                {
                    System.IO.StreamReader last = System.IO.File.OpenText(path);
                    string tmpHtml = last.ReadToEnd();
                    last.Close();
                    if (html != tmpHtml)
                    {
                        SetText("\n*************" + DateTime.Now.ToShortTimeString() + "***************\nNew Item Added To Page\n");
                        WriteToDisk(path, html);
                    }
                }
                else//first time app is ever ran, otherwise the file already exists.
                {
                    WriteToDisk(path, html);
                }
                CheckHTML(html);
            }
            catch (Exception ex)
            {
                SetText(ex.Message);
            }
        }

        private void WriteToDisk(string path, string html)
        {
            System.IO.StreamWriter writer = System.IO.File.CreateText(path);
            writer.Write(html);
            writer.Close();
        }
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text += text;
                this.richTextBox1.Select(richTextBox1.Text.Length, 0);
                this.richTextBox1.ScrollToCaret();
            }
        }


        private void CheckHTML(string html)
        {
            html = html.ToLower();
            int index = html.IndexOf(tbSearchText.Text);
            if (index > -1)
            {
                SetText("Match Found\n");
                tim.Stop();
            }
            else
            {
                SetText("No Matches\n");
            }
        }
    }
}
