#region

using System;
using System.Web.UI;
using Hershey.DataLayer.Recipes;

#endregion

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class RecipeProductManagement : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            var recipes = new RecipeService();
            recipes.AddRecipeProduct(txtProductName.Text, Convert.ToInt32(ddProductCategory.SelectedValue), txtDescription.Text, txtProductImage.Text);

            GridView1.DataBind();

            txtProductName.Text = "";
            txtProductImage.Text = "";
            txtDescription.Text = "";

            lblMessage.Text = txtProductName.Text + " Added.";
        }

    }
}