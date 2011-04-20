using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;

namespace SBWS.masterpages
{
    public partial class CorporateSite_Interior : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Node current = Node.GetCurrent();

            while (Menu1.ParentNodeID < 1 && current != null && current.Level > 0)
            {
                Property p = current.GetProperty("parentNodeID") as Property;
                if (p != null && !string.IsNullOrEmpty(p.Value))
                {
                    Menu1.ParentNodeID = p.Value.ToInt() ?? 0;
                }
                current = current.Parent as Node;
            }
        }
    }
}