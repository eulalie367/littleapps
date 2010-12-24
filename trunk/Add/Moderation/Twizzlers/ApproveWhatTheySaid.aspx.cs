using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;
using Hershey.Web;
using umbraco.uicontrols;
using umbraco.IO;
using System.Configuration;

namespace Hershey.Web.UmbracoAddons.Moderation.Twizzlers
{
    public partial class ApproveWhatTheySaid : umbraco.BasePages.UmbracoEnsuredPage
    {
        public TabPage InfoTabPage;
        public TabPage CommentsTabPage;
        
        protected void Page_Load(object sender, EventArgs e)
        {
                        InfoTabPage = TabView1.NewTabPage("Details");
            InfoTabPage.Controls.Add(pane_Details);
            ImageButton saveDetails = InfoTabPage.Menu.NewImageButton();
            saveDetails.ImageUrl = SystemDirectories.Umbraco + "/images/editor/save.gif";
            //saveDetails.Click += new ImageClickEventHandler(saveDetails_Click);

            CommentsTabPage = TabView1.NewTabPage("Comments");
            CommentsTabPage.Controls.Add(pane_Comments);
            ImageButton saveComments = CommentsTabPage.Menu.NewImageButton();
            saveComments.ImageUrl = SystemDirectories.Umbraco + "/images/editor/save.gif";
            //saveComments.Click += new ImageClickEventHandler(saveComments_Click);
        }
    }
}