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
    public class Menu
    {
        public static string LoadMenu(Node node, int depth, Node current)
        {
            StringBuilder sbMenu = new StringBuilder("");

            if (depth >= 0)
            {
                Property title = node.GetProperty("ShortTitle");
                string sTitle = "Home", url = "/", css = "";
                if (title != null && !string.IsNullOrEmpty(title.Value))
                {
                    sTitle = title.Value;
                    url = node.NiceUrl;
                }
                if (!string.IsNullOrEmpty(node.Name))
                {
                    css = node.Name + (node.Id == current.Id ? " active" : "");

                    sbMenu.AppendFormat("<ul><li><a href=\"{2}\" title=\"{0}\" class=\"{3}\">{1}</a>", node.Name, sTitle, url, css);

                    foreach (Node c in node.Children)
                    {
                        sbMenu.AppendFormat(LoadMenu(c, depth - 1, current));
                    }

                    sbMenu.Append("</li></ul>");
                }
            }
            return sbMenu.ToString();
        }
    }
    public partial class MenuControl : System.Web.UI.UserControl
    {
        public int Depth { get; set; }
        public StartingNode Node { get; set; }
        public enum StartingNode
        {
            Home,
            Parent,
            Self
        }
        public MenuControl()
        {
            this.Node = StartingNode.Home;
            Depth = 1;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Node n = null, current = SiteBuilder.Helpers.Umbraco.LoadCurrentNode();
            string html = "";

            if (this.Node == StartingNode.Parent)
            {
                n = SiteBuilder.Helpers.Umbraco.LoadInnerParentNode();
                html = Menu.LoadMenu(n, this.Depth, current);
            }
            else if (this.Node == StartingNode.Self && current.Children != null && current.Children.Count > 0)
            {
                //make sure it doesn't include itself
                n = current;
                foreach (Node c in n.Children)
                {
                    html += Menu.LoadMenu(c, this.Depth-1, current); 
                }
            }
            else if (this.Node == StartingNode.Home)
            {
                n = SiteBuilder.Helpers.Umbraco.LoadMainNode();
                //load the main guy selected
                current = SiteBuilder.Helpers.Umbraco.LoadInnerParentNode();
                html = Menu.LoadMenu(n, this.Depth, current);
            }
            else
            {
                n = current;
                html = Menu.LoadMenu(n, this.Depth, current);
            }
            menu.InnerHtml = html;
        }
    }
}