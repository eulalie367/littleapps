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

namespace ProductReomendation.usercontrols.MaintainProduct
{
    public partial class AttributeListControl : System.Web.UI.UserControl
    {
        private int? _productTypeID;
        public int? ProductTypeID
        {
            get
            {
                if (_productTypeID.HasValue)
                    return _productTypeID.Value;

                _productTypeID = Request["pt"].ToInt();
                return _productTypeID;
            }
            set
            {
                _productTypeID = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}