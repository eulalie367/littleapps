using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;

/// <summary>
/// Summary description for WikiList
/// </summary>
public class WikiList
{
	public WikiList()
	{
    }
    public class Directories
    {
        public const string Raw = "c:/speechlists/wikiTVListRaw.xml";
        public const string XML = "c:/speechlists/wikiTVList.xml";
        public const string tmpXML = "c:/speechlists/FinishedXMLFiles/";
        public const string XSL = "/Voice/Dictionary/Builders/XSL/WikiTVList.xsl";
    }
    #region URLS
    public class URLS
    {
        public const string Top1000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/1-1000";
        public const string To2000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/1001-2000";
        public const string To3000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/2001-3000";
        public const string To4000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/3001-4000";
        public const string To5000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/4001-5000";
        public const string To6000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/5001-6000";
        public const string To7000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/6001-7000";
        public const string To8000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/7001-8000";
        public const string To9000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/8001-9000";
        public const string To10000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/9001-10000";
        public const string To12000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/10001-12000";
        public const string To14000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/12001-14000";
        public const string To16000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/14001-16000";
        public const string To18000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/16001-18000";
        public const string To20000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/18001-20000";
        public const string To22000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/20001-22000";
        public const string To24000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/22001-24000";
        public const string To26000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/24001-26000";
        public const string To28000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/26001-28000";
        public const string To30000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/28001-30000";
        public const string To32000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/30001-32000";
        public const string To34000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/32001-34000";
        public const string To36000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/34001-36000";
        public const string To38000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/36001-38000";
        public const string To40000 = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/38001-40000";
        public const string TheRest = "http://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/40001-41284";
    }
    #endregion
    public Grammar GetTVLists()
    {
        Grammar retVal = null;
        if (System.IO.File.Exists(Directories.XML))
        {
            System.IO.StreamReader sr = null;
            try
            {
                sr = System.IO.File.OpenText(Directories.XML);
                retVal = BuildGrammar(sr.ReadToEnd());
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        else
        {
            GetTVList("top1000.xml", URLS.Top1000);
            GetTVList("to2000.xml", URLS.To2000);
            GetTVList("to3000.xml", URLS.To3000);
            GetTVList("to4000.xml", URLS.To4000);
            GetTVList("to5000.xml", URLS.To5000);
            GetTVList("to6000.xml", URLS.To6000);
            GetTVList("to7000.xml", URLS.To7000);
            GetTVList("to8000.xml", URLS.To8000);
            GetTVList("to9000.xml", URLS.To9000);
            GetTVList("to10000.xml", URLS.To10000);
            GetTVList("to12000.xml", URLS.To12000);
            GetTVList("to14000.xml", URLS.To14000);
            GetTVList("to16000.xml", URLS.To16000);
            GetTVList("to18000.xml", URLS.To18000);
            GetTVList("to20000.xml", URLS.To20000);
            GetTVList("to22000.xml", URLS.To22000);
            GetTVList("to24000.xml", URLS.To24000);
            GetTVList("to26000.xml", URLS.To26000);
            GetTVList("to28000.xml", URLS.To28000);
            GetTVList("to30000.xml", URLS.To30000);
            GetTVList("to32000.xml", URLS.To32000);
            GetTVList("to34000.xml", URLS.To34000);
            GetTVList("to36000.xml", URLS.To36000);
            GetTVList("To38000.xml", URLS.To38000);
            GetTVList("To40000.xml", URLS.To40000);
            GetTVList("TheRest.xml", URLS.TheRest);

            retVal = BuildGrammar(CombineLists());
        }

        if(System.IO.File.Exists(Directories.Raw))
            System.IO.File.Delete(Directories.Raw);
        if (System.IO.Directory.Exists(Directories.tmpXML))
            System.IO.Directory.Delete(Directories.tmpXML, true);

        return retVal;
    }

    private void GetTVList(string tmpFileName, string url)
    {
        System.Net.HttpWebRequest req = null;
        System.Net.HttpWebResponse resp = null;
        System.IO.StreamReader current = null;
        System.IO.TextWriter wr = null;
        try
        {
            req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.CreateDefault(new Uri(url));
            resp = (System.Net.HttpWebResponse)req.GetResponse();
            current = new System.IO.StreamReader(resp.GetResponseStream());
            wr = System.IO.File.CreateText(Directories.Raw);

            string tmp = current.ReadToEnd();
            int start = new System.Text.RegularExpressions.Regex("<table").Matches(tmp)[0].Index;
            int stop = new System.Text.RegularExpressions.Regex("</table>").Matches(tmp)[0].Index;
            //index is set at the start of a match, so account for the match itself
            stop += 8;
            tmp = tmp.Substring(start, stop - start);
            wr.Write(tmp);
        }
        catch
        {
        }
        finally
        {
            if(wr != null)
                wr.Close();
            if (current != null)
                current.Close();
        }
        //make sure the tmpDirectory exists
        if (!System.IO.Directory.Exists(Directories.tmpXML))
            System.IO.Directory.CreateDirectory(Directories.tmpXML);
        //transform; cleanup xml;
        System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
        trans.Load(HttpContext.Current.Server.MapPath(Directories.XSL));
        trans.Transform(Directories.Raw, Directories.tmpXML + tmpFileName);

    }
    private string CombineLists()
    {
        string retVal;
        //add xml declaration
        retVal = "<words>";
        foreach (string file in System.IO.Directory.GetFiles(Directories.tmpXML, "*", System.IO.SearchOption.TopDirectoryOnly))
        {
            System.IO.StreamReader text = System.IO.File.OpenText(file);
            try
            {
                retVal += text.ReadToEnd().Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>","");
            }
            finally
            {
                text.Close();
            }
        }
        retVal += "</words>";
        System.Data.DataSet ds = new System.Data.DataSet("words");
        ds.Tables.Add("word");
        ds.Tables["Word"].Columns.Add("rank", typeof(int));
        ds.Tables["Word"].Columns.Add("link", typeof(string));
        ds.Tables["Word"].Columns.Add("text", typeof(string));
        ds.Tables["Word"].Columns.Add("uses", typeof(int));

        System.Xml.XmlDataDocument xdoc = new System.Xml.XmlDataDocument(ds);
        xdoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + retVal.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", ""));

        //System.Data.DataSet d = new System.Data.DataSet("words");
        //d.Tables.Add(ds.Tables["words"].Copy());

        System.Data.DataSet writer = ds.Clone();
        //sort and write
        foreach (System.Data.DataRow dr in ds.Tables["word"].Select("text not like('%(not yet written)')", "rank asc"))
        {
            writer.Tables["word"].Rows.Add(dr.ItemArray);
        }
        writer.WriteXml(Directories.XML);
        retVal = writer.GetXml();
        return retVal;
    }
    private Grammar BuildGrammar(string xml)
    {
        System.Data.DataSet ds = new System.Data.DataSet();
        System.Xml.XmlDataDocument xdoc = new System.Xml.XmlDataDocument(ds);
        xdoc.LoadXml(xml);
        System.Collections.ArrayList words = new System.Collections.ArrayList();
        foreach (System.Xml.XmlNode n in xdoc.SelectNodes("words/word/text"))
        {
            try
            {
                //these need to be removed from the xml, but you can do that when you make the standard dictionary xml
                words.Add(n.InnerText);
            }
            catch (Exception)
            {
            }
        }
        Choices tvList = new Choices((string[])words.ToArray(typeof(string)));
        GrammarBuilder gb = tvList.ToGrammarBuilder();
        //make it a dictation grammar
        gb.AppendDictation("Default Dictation");
        Grammar g = new Grammar(gb);
        return g;
    }
}
