using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GetAFreelancerSorter
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string a = HTMLManipulation.GetHTML("http://getafreelancer.com/projects/by-job/AJAX.html");
            System.Xml.XmlDataDocument xdoc = new System.Xml.XmlDataDocument();
            xdoc.LoadXml(a);
            System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
            trans.Load(HttpContext.Current.Server.MapPath("GetAFreeLancer.xsl"));
            System.IO.Stream s = null;
            System.IO.StreamReader reader = null;
            try
            {
               trans.Transform(xdoc,null,s);
               reader = new System.IO.StreamReader(s);
               a = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (s != null)
                    s.Close();
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
