#region

using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;
using umbraco.IO;
using umbraco.uicontrols;

#endregion

namespace Hershey.Web.UmbracoAddons.Moderation.Recipes
{
    public partial class ApproveRecipe : UmbracoEnsuredPage
    {
        public TabPage CommentsTabPage;
        public TabPage InfoTabPage;

        protected void Page_Load(object sender, EventArgs e)
        {
            InfoTabPage = TabView1.NewTabPage("Details");
            InfoTabPage.Controls.Add(pane_Details);
            ImageButton saveDetails = InfoTabPage.Menu.NewImageButton();
            saveDetails.ImageUrl = SystemDirectories.Umbraco + "/images/editor/save.gif";
            saveDetails.Click += saveDetails_Click;

            CommentsTabPage = TabView1.NewTabPage("Reviews & Ratings");
            CommentsTabPage.Controls.Add(pane_Comments);
            ImageButton saveComments = CommentsTabPage.Menu.NewImageButton();
            saveComments.ImageUrl = SystemDirectories.Umbraco + "/images/editor/save.gif";
            saveComments.Click += saveComments_Click;

            // Pending Comments:
            CommentsTabPage = TabView1.NewTabPage("Pending Comments");
            CommentsTabPage.Controls.Add(pane_Pending);
            //ImageButton saveComments = CommentsTabPage.Menu.NewImageButton();
            //saveComments.ImageUrl = SystemDirectories.Umbraco + "/images/editor/save.gif";
            //saveComments.Click += saveComments_Click;


            SetValue();
        }

        private void saveComments_Click(object sender, ImageClickEventArgs e)
        {
            int? recipeID = Request["ID"].ToInt();
            if (recipeID.HasValue)
            {
                using (var context = new RecipeDB(ConfigurationManager.ConnectionStrings["KitchenDB"].ConnectionString))
                {
                    Recipe rec = context.Recipes.Where(r => r.recipe_ID == recipeID.Value).FirstOrDefault();
                    if (rec != null)
                    {
                        foreach (RecipeRating rating in rec.RecipeRatings)
                        {
                            rating.Approved = Request["cbRating" + rating.RecipeRatingsID].ToBool();
                        }
                        context.SubmitChanges();
                    }
                }
            }
        }

        private void saveDetails_Click(object sender, ImageClickEventArgs e)
        {
            int? recipeID = Request["ID"].ToInt();
            if (recipeID.HasValue)
            {
                using (var context = new RecipeDB(ConfigurationManager.ConnectionStrings["KitchenDB"].ConnectionString))
                {
                    Recipe rec = context.Recipes.Where(r => r.recipe_ID == recipeID.Value).FirstOrDefault();
                    if (rec != null)
                    {
                        rec.active = cbRecipeApproved.Checked ? 'Y' : 'N';
                        context.SubmitChanges();
                    }
                }
            }
        }

        private void SetValue()
        {
            int? recipeID = Request["ID"].ToInt();

            if (recipeID.HasValue)
            {
                RecipeReviews1.RecipeId = recipeID.Value;
                
                var r = new HersheyRecipeDetailsControl();
                r.LoadRecipe(recipeID.Value);

                if (IsPostBack) return;

                if (r.RecipeRecord != null)
                {
                    dvTitle.InnerHtml = r.Name;
                    imgLight.Visible = r.Light;
                    dvPhotoMain.InnerHtml = r.ImageHTML;
                    imgCategories.Src = r.CategoryImagePath;
                    dvInstructions.InnerHtml = RecipeService.ReplaceNewLinesWithBr(r.Instructions);
                    cbRecipeApproved.Checked = r.RecipeRecord.active.HasValue && r.RecipeRecord.active.Value == 'Y';

                    rptIngredients.DataSource = r.Ingredients;
                    rptIngredients.DataBind();


                    if (r.RecipeRecord.active.HasValue && r.RecipeRecord.active.Value == 'Y')
                    {
                        rptReviews.DataSource = r.RecipeRatings.ToList();
                        rptReviews.DataBind();
                    }
                }
            }
        }


        public string IsChecked(RepeaterItem container)
        {
            object o = DataBinder.Eval(container.DataItem, "Approved");
            if (o == null)
                return "";

            bool? b = o.ToString().ToBool();
            return b.HasValue && b.Value ? "checked=\"checked\"" : "";
        }
    }
}