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

namespace GLP.Products
{
    public partial class Products : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Node p = Node.GetCurrent();
            foreach (Node n in p.Children)
            {
                dvProducts.InnerText += n.Name;
                dvProducts.InnerText += n.GetProperty("ProductDescription").Value;
            }
        }
    }
}