using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        const string tmpDir = "E:/Database/wordnet30-mysql/tmp/";
        private void button1_Click(object sender, EventArgs e)
        {
            string fileLocation = textBox1.Text;
            FileChunker fc = new FileChunker(2048000, fileLocation);
            fc.TextChunked += new FileChunker.ChunkedFile(FileChunker_TextChunked);

            Thread th = new System.Threading.Thread(new ThreadStart(fc.ChunkTextFiles));
            th.Start();

            textBox1.Text = "Started Chunking";

        }
        private void FileChunker_TextChunked(object chunkedData, int position)
        {
            //really the directory at this point
            string fileName = tmpDir + "chunked/";
            string tmp = (string)chunkedData;

            //delete old files the first time through
            if (position == 0 && Directory.Exists(fileName))
            {
                Directory.Delete(fileName, true);
                Directory.CreateDirectory(fileName);
            }

            fileName += position.ToString() + ".txt";
            
            //report to client
            SafeSetControlText(fileName, label1);

            StreamWriter wr = null;
            try
            {
                //prep string
                tmp = tmp.Replace("rn", "");
                tmp = tmp.Replace("`", "");
                tmp = tmp.Replace("\\" + (char)34, "" + (char)34);

                //save chunk
                wr = File.CreateText(fileName);
                wr.Write(tmp);
            }
            finally//Dispose everything
            {
                if (wr != null)
                    wr.Close();

                tmp = null;
                wr = null;
            }
        }

        #region Thread Safe Form Control
        delegate void SetControlTextCallback(string txt, Control cntrl);
        public void SafeSetControlText(string txt, Control cntrl)
        {
            if (cntrl.InvokeRequired)
            {
                SetControlTextCallback sctc = new SetControlTextCallback(SetText);
                this.Invoke(sctc, new object[] { txt, cntrl });
            }
            else
            {
                this.SetText(txt, cntrl);
            }
        }
        public void SetText(string txt, Control cntrl)
        {
            cntrl.Text = txt;
        }
        #endregion

        private void cleanup()
        {
            //StreamReader reader = null;
            //StreamWriter writer = null;
            //string tmpDir = TmpDir + "formated/";
            //if (!Directory.Exists(tmpDir))
            //    Directory.CreateDirectory(tmpDir);

            //string tmp = "";
            //int i = 0;
            //foreach (string file in Directory.GetFiles(TmpDir + "chunked/"))
            //{
            //    try
            //    {

            //    }
            //    catch (Exception ex)
            //    { }
            //    finally
            //    {
            //        if (reader != null)
            //            reader.Close();

            //        if (writer != null)
            //            writer.Close();

            //        tmp = "";
            //    }
            //    i++;
            //    File.Delete(file);
            //}
        }
    }
}
