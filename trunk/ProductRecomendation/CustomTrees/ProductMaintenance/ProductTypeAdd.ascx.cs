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
using ProductRecomendation.ProductMaintenance;
using umbraco.BasePages;

namespace ProductRecomendation.CustomTrees.ProductMaintenance
{
    public partial class ProductTypeAdd : System.Web.UI.UserControl
    {
        private int? _parentProductTypeID;
        public int? ParentProductTypeID
        {
            get
            {
                if (_parentProductTypeID.HasValue)
                    return _parentProductTypeID.Value;

                _parentProductTypeID = Request["nodeID"].ToInt();
                return _parentProductTypeID;
            }
            set
            {
                _parentProductTypeID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.ServerClick += new EventHandler(btnSubmit_ServerClick);
        }

        private void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            SaveForm();
            BasePage.Current.ClientTools
                .ChildNodeCreated()
                .CloseModalWindow()
                .ReloadActionNode(true, true)
                .SetActiveTreeType(umbraco.helper.Request("nodeType"));
        }

        protected void SaveForm()
        {
            ProductType pt = new ProductType
            {
                Name = tbName.Value,
                ParentProductTypeID = ParentProductTypeID
            };
            pt.Insert();
        }
    }
}