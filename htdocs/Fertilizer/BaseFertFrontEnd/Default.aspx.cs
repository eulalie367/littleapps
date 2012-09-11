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
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using Calculators.Fertilizer;

namespace BaseFertFrontEnd
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnCalculate.ServerClick += new EventHandler(btnCalculate_ServerClick);
        }

        protected void btnCalculate_ServerClick(object sender, EventArgs e)
        {
            List<viewMolecularMass.mmElement> mm = new viewMolecularMass(Compound.Value).Elements;
            content.InnerHtml = "";
            foreach (viewMolecularMass.mmElement elem in mm)
            {
                content.InnerHtml += string.Format("<div><h1>{0}</h1><div>{1}</div></div>", elem.Symbol, elem.PercentWeight.ToString("N2") + " %");
            }
        }
    }
}
