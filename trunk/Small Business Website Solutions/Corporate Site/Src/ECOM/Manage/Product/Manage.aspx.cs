using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SBWS.ECOM.Data;
using System.Data.SqlClient;

namespace SBWS.ECOM.Manage.Product
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.ServerClick += new EventHandler(btnSave_ServerClick);

            ViewProduct viewProduct = SqlHelper.FillEntity<ViewProduct>("SELECT * FROM viewProduct (NOLOCK) WHERE ProductID = @ProductID", new SqlParameter[] { new SqlParameter("@ProductID", 1) }, CommandType.Text);
            txtProductID.Text = viewProduct.ProductID.ToString();
            txtCategoryID.Text = viewProduct.CategoryID.ToString();
            txtUnitTypeID.Text = viewProduct.UnitTypeID.ToString();
            txtCostPerUnit.Text = viewProduct.CostPerUnit.ToString();
            txtPricePerUnit.Text = viewProduct.PricePerUnit.ToString();
            txtName.Text = viewProduct.Name.ToString();
            txtDescription.Text = viewProduct.Description.ToString();
            txtCategoryName.Text = viewProduct.CategoryName.ToString();
            txtUnitTypeName.Text = viewProduct.UnitTypeName.ToString();
        }

        void btnSave_ServerClick(object sender, EventArgs e)
        {
            SBWS.ECOM.Data.Product vp = new SBWS.ECOM.Data.Product();
            vp.ProductID = txtProductID.Text.ToInt() ?? -1;
            vp.CategoryID = txtCategoryID.Text.ToInt() ?? 1;
            vp.UnitTypeID = txtUnitTypeID.Text.ToInt() ?? 1;
            vp.CostPerUnit = txtCostPerUnit.Text.ToFloat();
            vp.PricePerUnit = txtPricePerUnit.Text.ToFloat();
            vp.Name =  txtName.Text;
            vp.Description = txtDescription.Text;
            SqlHelper.UpdateEntity<SBWS.ECOM.Data.Product>(vp);
        }
    }
}