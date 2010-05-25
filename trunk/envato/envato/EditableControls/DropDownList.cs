using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;
using Google.GData.Photos;
using System.Web.UI.HtmlControls;


namespace EditableControls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), DefaultProperty("ImageUrl"), ToolboxData("<{0}:Image runat=\"server\"> </{0}:Image>")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        [Bindable(false), Category("Behavior"), Description("Allow the control to be in edit mode."), DefaultValue(""), Localizable(true)]
        public bool EditMode { get; set; }
        public DropDownList()
        {
            this.EditMode = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.EditMode)
                base.Render(writer);
            else
                writer.Write(this.SelectedItem.Text);
        }
    }
}
