using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCapt.agenceXML scraper = new WebCapt.agenceXML();
            string html = scraper.Capture("http://fullspectrumlabs.com/tested/products/");

            XmlNode node = scraper.xDoc.SelectSingleNode("//div[@style='overflow:auto;height:300px;']");

            string a = "";
        }
    }
}
