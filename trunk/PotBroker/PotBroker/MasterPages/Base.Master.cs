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

namespace PotBroker.MasterPages
{
    public partial class Base : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.PathAndQuery.ToLower().Contains("setup"))
            {
                PotBroker.Management.Base.User user = PotBroker.Management.Base.User.LoadAccountState();
                if (!user.LoggedIn && Request.Url.PathAndQuery.ToLower().Contains("listings"))
                {
                    user.LastURL = Request.Url.PathAndQuery;
                    user.SaveAccountState();
                    Response.Redirect("~/Setup/CreateAccount.aspx");
                }
                else if (user.LoggedIn && !string.IsNullOrEmpty(user.LastURL))
                    Response.Redirect(user.LastURL);
            }
        }
    }
}
