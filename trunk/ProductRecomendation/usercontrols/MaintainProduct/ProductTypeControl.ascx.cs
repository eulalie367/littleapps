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

namespace ProductRecomendation.usercontrols.MaintainProduct
{
    public partial class ProductTypeControl : System.Web.UI.UserControl
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

        protected override void OnInit(EventArgs e)
        {
            selParentType.FillSelect(ProductType.GetAll(), "Name", "ProductTypeId", "No Parent");
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.ServerClick += new EventHandler(btnSubmit_ServerClick);

            if (!IsPostBack)
                LoadValues();
        }

        protected void LoadValues()
        {
            if (ProductTypeID.HasValue)
            {
                ProductType pt = new ProductType(ProductTypeID.Value);
                if (pt != null)
                {
                    btnSubmit.Value = "Save Product Type";
                    tbName.Value = pt.Name;
                    selParentType.SelectedValue = pt.ParentProductTypeID.HasValue ? pt.ParentProductTypeID.ToString() : "";
                }
            }
        }

        private void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            SaveForm();
        }

        protected void SaveForm()
        {
            ProductType pt = new ProductType
            {
                Name = tbName.Value,
                ParentProductTypeID = selParentType.SelectedValue.ToInt()
            };
            if (ProductTypeID.HasValue)
            {
                pt.ProductTypeID = ProductTypeID.Value;
                pt.Save();
            }
            else
            {
                pt.Insert();
            }

            Response.Redirect(Request.Url.GetModifiedUrlWithParams("pt", pt.ProductTypeID.ToString()));
        }
    }
}