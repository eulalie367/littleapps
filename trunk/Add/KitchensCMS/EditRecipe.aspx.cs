using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class EditRecipe : UmbracoEnsuredPage
    {
        public RecipeService RecipeObject { get; set; }
        public MyKitchenService KitchenObject { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}