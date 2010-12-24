using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hershey.Web.UmbracoAddons.Moderation
{
    public enum ModerationState
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }

    public partial class ModerationStateFilter : System.Web.UI.UserControl
    {
        public event System.EventHandler OnFilterChanged;

        public ModerationState CurrentStateFilter
        {
            get
            {
                object o = ViewState["moderationState"];
                if (o == null)
                    return ModerationState.Pending;

                return (ModerationState)o;
            }
            set
            {
                ViewState["moderationState"] = value;
            }
        }

        //public void SetFilterAsString(string val)
        //{
        //    switch (val)
        //    {
        //        case "accepted":
        //            CurrentStateFilter = ModerationState.Accepted;
        //            break;

        //        case "rejected":
        //            CurrentStateFilter = ModerationState.Rejected;
        //            break;

        //        default:
        //            CurrentStateFilter = ModerationState.Pending;
        //            break;
        //    }

        //}

        protected void menuMain_MenuItemClick(Object sender, MenuEventArgs e)
        {
            int val;
            if (int.TryParse(e.Item.Value, out val))
            {
                CurrentStateFilter = (ModerationState)val;
            }

            if (OnFilterChanged != null)
                OnFilterChanged(this, new EventArgs());

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string current = ((int)CurrentStateFilter).ToString();

            foreach (MenuItem menu in menuMain.Items)
            {
                if (menu.Value == current)
                {
                    menu.Selected = true;
                    menu.Enabled = false;
                }
                else
                {
                    menu.Selected = false;
                    menu.Enabled = true;
                }
            }
        }
    }
}