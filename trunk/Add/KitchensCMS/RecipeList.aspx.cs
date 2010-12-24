#region

using System;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;

#endregion

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class RecipeList : UmbracoEnsuredPage
    {
        //public RecipeService RecipeObject { get; set; }
        //public MyKitchenService KitchenObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["search"] != null)
                divSearchPanel.Attributes["style"] = "";

            RecipeSearch1.DoSearch += RecipeSearch1_DoSearch;

            //RecipeObject = new RecipeService();
            //SearchObject.Filter = new RecipeFilter();

            if (IsPostBack) return;
            RecipeList1.Bind();
        }

        private void RecipeSearch1_DoSearch(object sender, RecipeFilter filter)
        {
            //SearchObject.Filter = filter;

            RecipeList1.Bind(filter);
        }
    }
}