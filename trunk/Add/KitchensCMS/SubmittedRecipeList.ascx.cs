#region

using System;
using System.Linq;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;

#endregion

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class SubmittedRecipeList : HersheyControl
    {
        private SortDirection m_SortDirection = SortDirection.Ascending;
        private string m_strSortExp;

        protected void Page_Load(object sender, EventArgs e)
        {
            RecipeObject = new RecipeService();
            SearchObject = new DataLayer.Recipes.RecipeSearch();

            gvRecipes.PageIndexChanging += gvRecipes_PageIndexChanging;
            gvRecipes.Sorting += gvRecipes_Sorting;

            Bind();
        }

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

        public void Bind(RecipeFilter filter)
        {
            ViewState["Filter"] = filter;
            Bind();
        }

        public void Bind()
        {
            RecipeObject = new RecipeService();

            if (ViewState["Filter"] != null)
                SearchObject.Filter = (RecipeFilter)ViewState["Filter"];
            else
            {
                SearchObject.Filter = new RecipeFilter(false) { ActiveRecipesOnly = false };
            }

            #region Check querystring values

            if (Request.QueryString["active"] != null)
            {
                SearchObject.Filter.InActiveRecipesOnly = true;
            }
            if (Request.QueryString["myrecipes"] != null)
            {
                SearchObject.Filter.EditedBy = UmbracoEnsuredPage.CurrentUser.LoginName;
            }

            if (Request.QueryString["submit"] != null)
            {
                SearchObject.Filter.SubmittedRecipesOnly = true;
            }

            #endregion

            var data = SearchObject.SearchSubmittedRecipes().Select(r => new
            {
                recipe_ID = r.RecipeId,
                r.RecipeName,
                r.Submitted,
                Image = "",
                QuickAndEasy = false,
                LessFatRecipe = false,
                IsActive = r.Active,
                DateUpdated = r.Submitted
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
                        data.OrderByDescending(r => r.recipe_ID);
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
        //        RecipeObject.UpdateRecipeVisibility(recipeId, chkVisible.Checked);
        //    }
        //}
    }
}