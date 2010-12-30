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
using umbraco.uicontrols;
using ProductRecomendation.ProductMaintenance;

namespace ProductRecomendation.CustomTrees.ProductMaintenance
{
    public partial class ProductControl : umbraco.BasePages.UmbracoEnsuredPage
    {
        private int? _productID;
        public int? ProductID
        {
            get
            {
                if (_productID.HasValue)
                    return _productID.Value;

                _productID = Request.ToInt("p");
                return _productID;
            }
            set
            {
                _productID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TabPage InfoTabPage = TabView1.NewTabPage("Details");
            InfoTabPage.Controls.Add(pane_Details);

            ImageButton saveDetails = InfoTabPage.Menu.NewImageButton();
            saveDetails.ImageUrl = umbraco.IO.SystemDirectories.Umbraco + "/images/editor/save.gif";
            saveDetails.Click += new ImageClickEventHandler(saveDetails_Click);

            if (!IsPostBack)
                LoadValues();
        }

        void saveDetails_Click(object sender, ImageClickEventArgs e)
        {
            Product p = new Product(this.ProductID.Value);
            p.ProductName = tbName.Value;
            p.Save();

            umbraco.BasePages.BasePage.Current.ClientTools
                .ReloadActionNode(true, true)
                .SetActiveTreeType(umbraco.helper.Request("nodeType"));
        }

        private void LoadValues()
        {
            if (this.ProductID.HasValue)
            {
                Product p = new Product(this.ProductID.Value);
                tbName.Value = p.ProductName;
            }
        }
    }
}
