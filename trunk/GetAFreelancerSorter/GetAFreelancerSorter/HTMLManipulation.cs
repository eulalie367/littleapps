using System;
using System.Web;
using System.Net;
using System.IO;

namespace GetAFreelancerSorter
{
    public class HTMLManipulation
    {
        public string HTML { get; set; }
        public HTMLManipulation()
        {
            this.HTML = "";
        }
        /// <summary>
        /// Runs the GetHTML Method from the specified url
        /// </summary>
        public HTMLManipulation(string url)
        {
            this.HTML = GetHTML(url);
        }
        public static string GetHTML(string url)
        {
            WebResponse resp = null;
            Stream s = null;
            StreamReader sr = null;
            string tmp = "";
            try
            {
                WebRequest req = WebRequest.Create(url);
                resp = req.GetResponse();
                s = resp.GetResponseStream();
                sr = new StreamReader(s);
                tmp = sr.ReadToEnd();
            }
            catch (Exception ex) { HttpContext.Current.Response.Write(ex.Message); }
            finally
            {
                if (resp != null)
                    resp.Close();
                if (s != null)
                    s.Close();
                if (sr != null)
                    sr.Close();
            }
            return tmp;
        }
    }
}
