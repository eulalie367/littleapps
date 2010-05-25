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
    public partial class Validate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            subLogin.ServerClick += new EventHandler(subLogin_ServerClick);
        }
        protected void subLogin_ServerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["key"]) && !string.IsNullOrEmpty(tbPassWord.Value))
                if(PotBroker.Management.Base.Validate(HttpUtility.UrlDecode(Request["key"]), Base.encrypt(tbPassWord.Value) + tbUserName.Value))
                    Response.Write("success");
        }
    }
}
