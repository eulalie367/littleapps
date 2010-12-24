using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class CategoryManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            var recipes = new RecipeService();
            recipes.AddRecipeCategory(txtCategory.Text);

        }

        protected void btnAddOccasion_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnAddProductCategory_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}