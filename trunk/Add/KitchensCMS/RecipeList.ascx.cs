#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;

#endregion

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class RecipeList1 : HersheyControl
    {
        private SortDirection m_SortDirection = SortDirection.Ascending;
        private string m_strSortExp;

        protected void Page_Load(object sender, EventArgs e)
        {
            RecipeObject = new RecipeService();
        }

        private void gvRecipes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var isActive = (bool) e.Row.DataItem.GetType().GetProperty("IsActive").GetValue(e.Row.DataItem, null);

            e.Row.CssClass = !isActive ? "InActive" : "Active";
        }

        public void Bind(RecipeFilter filter)
        {
            ViewState["Filter"] = filter;
            Bind();
        }

        public void Bind()
        {
            RecipeObject = new RecipeService();
            gvRecipes.PageIndexChanging += gvRecipes_PageIndexChanging;
            gvRecipes.Sorting += gvRecipes_Sorting;
            gvRecipes.RowDataBound += gvRecipes_RowDataBound;


            if (ViewState["Filter"] != null)
                SearchObject.Filter = (RecipeFilter) ViewState["Filter"];
            else
            {
                SearchObject.Filter = new RecipeFilter(false) {ActiveRecipesOnly = false};
            }

            #region Check querystring values

            if (Request.QueryString["active"] != null)
            {
                SearchObject.Filter.InActiveRecipesOnly = true;
                litTitle.Text = "Active Recipes";
            }
            if (Request.QueryString["myrecipes"] != null)
            {
                SearchObject.Filter.EditedBy = UmbracoEnsuredPage.CurrentUser.LoginName;
                litTitle.Text = "Recipes edited by " + UmbracoEnsuredPage.CurrentUser.LoginName;
            }

            if (Request.QueryString["homepage"] != null)
            {
                SearchObject.Filter.FilteredOccasions.Add(159);
                litTitle.Text = "Homepage Recipes";
            }
            

            if (Request.QueryString["submit"] != null)
            {
                SearchObject.Filter.SubmittedRecipesOnly = true;
            }

            #endregion

            var data = SearchObject.SearchRecipes(false).Select(r => new
                                                                         {
                                                                             r.recipe_ID,
                                                                             r.RecipeName,
                                                                             r.DateUpdated,
                                                                             r.Image,
                                                                             r.QuickAndEasy,
                                                                             r.LessFatRecipe,
                                                                             r.IsActive
                                                                         }).ToList();

            if (!string.IsNullOrEmpty(m_strSortExp))
                switch (m_strSortExp)
                {
                    case "RecipeName":
                        data.OrderBy(r => r.RecipeName);
                        break;
                    case "recipe_ID":
                        data.OrderBy(r => r.recipe_ID);
                        break;
                    case "active":
                        data.OrderBy(r => r.IsActive);
                        break;
                    case "DateUpdated":
                        //data.OrderBy(r => r.DateUpdated);
                        data.OrderByDescending(r => r.DateUpdated);
                        break;
                }

            if (m_SortDirection == SortDirection.Descending)
                data.Reverse();

            gvRecipes.DataSource = data;
            gvRecipes.DataBind();
        }

        //protected void UpdateRecipeStatus(object sender, EventArgs e)
        //{
        //    // Update Status of Each Item in Grid

        //    RecipeObject = new RecipeService();

        //    foreach (GridViewRow row in this.gvRecipes.Rows)
        //    {
        //        int recipeId = (int)gvRecipes.DataKeys[row.DataItemIndex].Value;

        //        CheckBox chkVisible = (CheckBox)row.FindControl("chkVisible");

        //        //  Update Status:
        //        RecipeObject.UpdateRecipeVisibility(recipeId,chkVisible.Checked);
        //    }
        //}

        protected void UpdateSelectedRecipes(object sender, EventArgs e)
        {
            List<int> selectedRecipes = (from GridViewRow row in gvRecipes.Rows
                                         let recipeId = (int) gvRecipes.DataKeys[row.DataItemIndex].Value
                                         let chkVisible = (CheckBox) row.FindControl("chkVisible")
                                         where chkVisible.Checked
                                         select recipeId).ToList();


            switch (((DropDownList) (sender)).SelectedValue)
            {
                case "Active":

                    if (!RecipeObject.CanPublishRecipes)
                    {
                        ((DropDownList)(sender)).SelectedIndex = 0;
                        Alert.Show("This account doesn't have permission to make recipes active.", Page);
                        lblError.Text = "This account doesn't have permission to make recipes active.";
                        return;
                    }

                    selectedRecipes.ForEach(recipe => RecipeObject.UpdateRecipeVisibility(recipe, true));
                    break;
                case "InActive":

                    if (!RecipeObject.CanPublishRecipes)
                    {
                        ((DropDownList)(sender)).SelectedIndex = 0;
                        lblError.Text = "This account doesn't have permission to make recipes inactive.";
                        return;
                    }

                    selectedRecipes.ForEach(recipe => RecipeObject.UpdateRecipeVisibility(recipe, false));
                    break;
                case "Homepage":

                    if (!RecipeObject.CanEditRecipes)
                    {
                        ((DropDownList)(sender)).SelectedIndex = 0;
                        lblError.Text = "This account doesn't have permission to edit recipes";
                        return;
                    }

                    selectedRecipes.ForEach(recipe => RecipeObject.UpdateHomepageVisibility(recipe, true));
                    break;
                case "NoHomepage":

                    if (!RecipeObject.CanEditRecipes)
                    {
                        ((DropDownList)(sender)).SelectedIndex = 0;
                        lblError.Text = "This account doesn't have permission to edit recipes";
                        return;
                    }


                    selectedRecipes.ForEach(recipe => RecipeObject.UpdateHomepageVisibility(recipe, false));
                    break;
            }

            Bind();
            
        }
      

        #region Paging

        private void gvRecipes_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (String.Empty != m_strSortExp)
            {
                if (String.Compare(e.SortExpression, m_strSortExp, true) == 0)
                {
                    m_SortDirection =
                        (m_SortDirection == SortDirection.Ascending)
                            ? SortDirection.Descending
                            : SortDirection.Ascending;
                }
            }
            ViewState["_Direction_"] = m_SortDirection;
            ViewState["_SortExp_"] = m_strSortExp = e.SortExpression;

            Bind();
        }

        private void gvRecipes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRecipes.PageIndex = e.NewPageIndex;
            Bind();
        }

        #endregion
    }
}