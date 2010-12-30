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

namespace ProductRecomendation.CustomTrees.ProductMaintenance
{
    public partial class ProductAdd : System.Web.UI.UserControl
    {
        private int? _productID;
        public int? ProductID
        {
            get
            {
                if (_productID.HasValue)
                    return _productID.Value;

                _productID = Request["nodeID"].ToInt();
                return _productID;
            }
            set
            {
                _productID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.ServerClick += new EventHandler(btnSubmit_ServerClick);
        }

        void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            SaveForm();
            umbraco.BasePages.BasePage.Current.ClientTools
                .ChildNodeCreated()
                .CloseModalWindow()
                .ReloadActionNode(true, true)
                .SetActiveTreeType(umbraco.helper.Request("nodeType"));
        }

        private void SaveForm()
        {
            if (ProductID.HasValue && !string.IsNullOrEmpty(tbName.Value))
            {
                Product p = new Product();
                p.ProductTypeID = ProductID.Value;
                p.ProductName = tbName.Value;
                p.Add();
            }
        }
    }
}