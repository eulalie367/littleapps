using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class RecipeSearch : HersheyControl
    {
        public delegate void DoSearchDelegate(object sender, RecipeFilter filter);

        public event DoSearchDelegate DoSearch;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            // Filters

            var types = RecipeObject.GetRecipeCategories();
            types.Insert(0, new Category() { category_name = "Select One" });
            ddRecipeTypeFilter.DataSource = types;
            ddRecipeTypeFilter.DataBind();

            var occasions = RecipeObject.GetRecipeOccasions(false);
            occasions.Insert(0, new Occasion() { Occasion1 = "Select One" });
            ddFilterByOccasion.DataSource = occasions;
            ddFilterByOccasion.DataBind();

            var products = RecipeObject.GetProducts();
            products.Insert(0, new Product() { ProductName = "Select One:" });
            ddFilterByProduct.DataSource = products.Select(p => new { Name = p.ProductName, Id = p.ProductID });
            ddFilterByProduct.DataBind();

            var ideas = RecipeObject.GetRecipeIdeas();
            ideas.Add(new Category() { category_name = "Select One:" });
            chkFilterByIdea.DataSource = ideas;
            chkFilterByIdea.DataBind();

            // Get Users List:

            var people = RecipeObject.GetRecipeEditorsUserList().Select(p => new { Name = p }).ToList();
            ddEditedBy.DataSource = people;
            ddEditedBy.DataBind();

            // ddCreatedBy.DataSource = RecipeObject.GetRecipeEditorsUserList();
            //  ddCreatedBy.DataBind();



        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchObject.Filter = new RecipeFilter(false);


            SearchObject.Filter.SearchField = txtRecipeSearch.Text;

            if (Convert.ToInt32(this.ddRecipeTypeFilter.SelectedValue) > 0)
            {
                SearchObject.Filter.FilteredRecipeCategories.Add(Convert.ToInt32(this.ddRecipeTypeFilter.SelectedValue));
            }

            if (Convert.ToInt32(this.ddFilterByProduct.SelectedValue) > 0)
            {
                SearchObject.Filter.ProductId = (Convert.ToInt32(this.ddFilterByProduct.SelectedValue));
            }

            if (Convert.ToInt32(this.ddFilterByOccasion.SelectedValue) > 0)
            {
                SearchObject.Filter.FilteredOccasions.Add(Convert.ToInt32(this.ddFilterByOccasion.SelectedValue));
            }

            foreach (ListItem item in chkFilterByIdea.Items.Cast<ListItem>().Where(item => item.Selected))
            {
                SearchObject.Filter.FilteredIdeas.Add(Convert.ToInt32(item.Value));
            }

            SearchObject.Filter.EditedBy = ddEditedBy.SelectedValue;


            if (chkBeginner.Checked)
                SearchObject.Filter.FilterBySkill = "Beginner";

            if (chkIntermediate.Checked)
                SearchObject.Filter.FilterBySkill = "Intermediate";

            if (chkAdvanced.Checked)
                SearchObject.Filter.FilterBySkill = "Advanced";

            if (rdoActive.Checked)
                SearchObject.Filter.ActiveRecipesOnly = true;
            if (rdoInActive.Checked)
                SearchObject.Filter.InActiveRecipesOnly = true;


            int recipeId;
            if (Int32.TryParse(txtRecipeSearch.Text, out recipeId) && recipeId > 0)
            {
                SearchObject.Filter.RecipeId = recipeId;
                SearchObject.Filter.SearchField = null;
            }
                

            DoSearch(sender, this.SearchObject.Filter);
        }
    }
}