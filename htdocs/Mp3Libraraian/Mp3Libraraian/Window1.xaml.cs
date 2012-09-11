using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mp3Libraraian
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            disp.Text = "";
            foreach (XElement name in DirectoryXML(dir.Text).Descendants("dir"))
            {
                disp.Text += name.Attribute("name").Value + "\n";
            }
        }


        private XDocument DirectoryXML(string LocalPath)
        {
            XDocument directoryXML;

            System.IO.StringReader tr = new System.IO.StringReader(GetDirXML(LocalPath));
            directoryXML = XDocument.Load(tr);
            tr.Close();

            return directoryXML;
        }
        private string GetDirXML(string LocalPath)
        {
            string getXML = "";
            string name;
            getXML = "<dirs>";
            foreach(string strDir in System.IO.Directory.GetDirectories(LocalPath,"*", System.IO.SearchOption.AllDirectories))
            {
                name = strDir.Replace(LocalPath, "").Replace("\\", "") ;
                getXML += "\n\n\t<dir name=\"" + name + "\">";
                getXML += "\n\t\t" + strDir;
                getXML += "\n\t</dir>";
            }
            getXML += "</dirs>";
            return getXML;
        }

        private void label1_Click(object sender, RoutedEventArgs e)
        {
            ParseDirectories();
        }

        private void ParseDirectories()
        {
            if (newDeleteChar.Text != "")
            {
                string fileLocation = @"C:\LittleApps\Mp3Libraraian\Mp3Libraraian\MP3s.xml";
                XDocument parsers = GetParseXml(newDeleteChar.Text);
                XDocument dirs = DirectoryXML(dir.Text);

                deleteChars.Text = parsers.ToString();
                newDeleteChar.Text = "";

                foreach (XElement name in dirs.Descendants("dir"))
                {
                    foreach (XElement par in parsers.Descendants("Parser"))
                    {
                        name.Attribute("name").Value = name.Attribute("name").Value.Replace(par.Value, "");
                    }
                }
                dirs.Save(fileLocation);
                disp.Text = "";
                foreach (XElement name in dirs.Descendants("dir"))
                {
                    disp.Text += name.Attribute("name").Value + "\n";
                }
            }
        }

        private XDocument GetParseXml(string p)
        {
            string fileLocation = @"C:\LittleApps\Mp3Libraraian\Mp3Libraraian\Parse.xml";
            XDocument x;
            if (!System.IO.File.Exists(fileLocation))
            {
                x = new XDocument();
                x.Add(new XElement("Parsers"));
            }
            else
            {
                x = XDocument.Load(fileLocation);
            }
            if (p != "")
            {
                x.Descendants("Parsers").First().Add(new XElement("Parser", p));
                x.Save(fileLocation);
            }
            return x;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            XDocument parsers = GetParseXml(newDeleteChar.Text);
            XDocument dirs = DirectoryXML(dir.Text);

            deleteChars.Text = parsers.ToString();
            newDeleteChar.Text = "";
            disp.Text = "";
            foreach (XElement name in dirs.Descendants("dir"))
            {
                foreach (XElement par in parsers.Descendants("Parser"))
                {
                    name.Attribute("name").Value = name.Attribute("name").Value.Replace(par.Value, "");
                }
                foreach (XElement punc in parsers.Descendants("Punctuation"))
                {
                    name.Attribute("name").Value = name.Attribute("name").Value.Replace(punc.Value, "");
                }
                disp.Text += name.Attribute("name").Value.Replace(" ", "") +"\n";
            }
        }
    }
}
