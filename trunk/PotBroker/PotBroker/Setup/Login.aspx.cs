using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PotBroker.Management;

namespace PotBroker
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            subLogin.ServerClick += new EventHandler(subLogin_ServerClick);
        }

        protected void subLogin_ServerClick(object sender, EventArgs e)
        {
            PotBroker.Management.Base.User user = PotBroker.Management.Base.User.LoadAccountState();
            if (Base.DoLogin(tbUserName.Value, tbPassWord.Value))
            {
                if (!string.IsNullOrEmpty(user.LastURL))
                    Response.Redirect(user.LastURL);
            }
        }
    }
}
