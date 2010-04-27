using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ComponentModel;
using System.Security.Permissions;


namespace Zoomable
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), DefaultProperty("src"), ToolboxData("<{0}:ZoomControl runat=\"server\"> </{0}:ZoomControl>")]
    public class ZoomControl : Panel 
    {
        [Bindable(false), Category("Appearance"), Description("The soruce of the image."), DefaultValue(""), Localizable(true)]
        public string src { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(src);
            base.Render(writer);
        }
    }
}
