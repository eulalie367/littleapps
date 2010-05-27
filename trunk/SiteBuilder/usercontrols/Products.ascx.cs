using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using umbraco.presentation.nodeFactory;
using System.Text;

namespace SiteBuilder.usercontrols
{
    public partial class Products : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Node n = null, current = SiteBuilder.Helpers.Umbraco.LoadCurrentNode();
            StringBuilder html = new StringBuilder();

            //make sure it doesn't include itself
            n = current;
            if (n.Children != null)
                foreach (Node c in n.Children)
                {
                    html.Append(LoadProduct(c, 1, n));
                }

            dvProducts.InnerHtml = html.ToString();
        }
        public static string LoadProduct(Node node, int depth, Node current)
        {
            StringBuilder sbMenu = new StringBuilder("");

            if (depth >= 0)
            {
                if (!string.IsNullOrEmpty(node.Name))
                {
                    Property title = node.GetProperty("ShortTitle");
                    string sTitle = "", url = "", css = "";
                    if (title != null && !string.IsNullOrEmpty(title.Value))
                    {
                        sTitle = title.Value;
                        url = node.NiceUrl;
                    }
                    css = node.Name + (node.Id == current.Id ? " active" : "");

                    sbMenu.AppendFormat("<ul><li><a href=\"{2}\" title=\"{0}\" class=\"{3}\">{1}</a>", node.Name, sTitle, url, css);

                    foreach (Node c in node.Children)
                    {
                        sbMenu.AppendFormat(LoadProduct(c, depth - 1, current));
                    }

                    sbMenu.Append("</li></ul>");
                }
            }
            return sbMenu.ToString();
        }
    }
}