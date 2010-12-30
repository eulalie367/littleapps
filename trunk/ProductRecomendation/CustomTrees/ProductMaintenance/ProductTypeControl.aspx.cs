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
    public partial class ProductTypeControl : umbraco.BasePages.UmbracoEnsuredPage
    {
        private int? _productTypeID;
        public int? ProductTypeID
        {
            get
            {
                if (_productTypeID.HasValue)
                    return _productTypeID.Value;

                _productTypeID = Request.ToInt("pt");
                return _productTypeID;
            }
            set
            {
                _productTypeID = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            selParentType.FillSelect(ProductType.GetAll(), "Name", "ProductTypeId", "No Parent");
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TabPage InfoTabPage;
            InfoTabPage = TabView1.NewTabPage("Details");
            InfoTabPage.Controls.Add(pane_Details);

            ImageButton saveDetails = InfoTabPage.Menu.NewImageButton();
            saveDetails.ImageUrl = umbraco.IO.SystemDirectories.Umbraco + "/images/editor/save.gif";
            saveDetails.Click += new ImageClickEventHandler(saveDetails_Click);

            if (!IsPostBack)
                LoadValues();
        }

        void saveDetails_Click(object sender, ImageClickEventArgs e)
        {
            SaveForm();
            umbraco.BasePages.BasePage.Current.ClientTools
                .ReloadActionNode(true, true)
                .SetActiveTreeType(umbraco.helper.Request("nodeType"));
        }

        protected void LoadValues()
        {
            if (ProductTypeID.HasValue)
            {
                ProductType pt = new ProductType(ProductTypeID.Value);
                if (pt != null)
                {
                    tbName.Value = pt.Name;
                    selParentType.SelectedValue = pt.ParentProductTypeID.HasValue ? pt.ParentProductTypeID.ToString() : "";
                }
            }
        }

        protected void SaveForm()
        {
            ProductType pt = new ProductType
            {
                Name = tbName.Value,
                ParentProductTypeID = selParentType.SelectedValue.ToInt(),
                ProductTypeID = this.ProductTypeID.Value
            };

            pt.Save();
        }

    }
}
