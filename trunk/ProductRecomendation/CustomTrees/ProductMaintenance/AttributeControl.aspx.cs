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
    public partial class AttributeControl : umbraco.BasePages.UmbracoEnsuredPage
    {
        private int? _attributeID;
        public int? AttributeID
        {
            get
            {
                if (_attributeID.HasValue)
                    return _attributeID.Value;

                _attributeID = Request.ToInt("a");
                return _attributeID;
            }
            set
            {
                _attributeID = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            Page.EnableViewState = true;

            selAttributeType.DataSource = AttributeType.Getall();
            selAttributeType.DataTextField = "Name";
            selAttributeType.DataValueField = "AttributeTypeID";
            selAttributeType.DataBind();

            base.OnInit(e);
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
            if (this.AttributeID.HasValue)
            {
                ProductTypeAttribute pta = new ProductTypeAttribute();
                pta.AttributeID = this.AttributeID.Value;
                pta.AttributeTypeID = selAttributeType.SelectedValue.ToInt();
                pta.Name = tbName.Value;
                pta.Save();

                umbraco.BasePages.BasePage.Current.ClientTools
                    .ReloadActionNode(true, true)
                    .SetActiveTreeType(umbraco.helper.Request("nodeType"));
            }
        }

        private void LoadValues()
        {
            if (this.AttributeID.HasValue)
            {
                ProductTypeAttribute attribute = new ProductTypeAttribute(this.AttributeID.Value);
                tbName.Value = attribute.Name;
                
                if(attribute.AttributeTypeID.HasValue)
                    selAttributeType.SelectedValue = attribute.AttributeTypeID.Value.ToString();
            }
        }
    }
}
