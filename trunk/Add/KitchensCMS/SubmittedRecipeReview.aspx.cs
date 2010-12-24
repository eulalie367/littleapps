using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer;
using Hershey.DataLayer.Recipes;

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class SubmittedRecipeReview : System.Web.UI.Page
    {
        public RecipeService RecipeObject { get; set; }
        protected int RecipeId { get; set; }

        public SubmittedRecipe RecipeRecord { get; set; }

        protected List<SubmittedRecipeIngredient> Ingredients { get; set; }

        private List<SubmittedCategory> _recipeCategories;

        public List<SubmittedCategory> Categories
        {
            get { return _recipeCategories ?? (_recipeCategories = RecipeObject.GetAllSubmittedRecipeCategories()); }
            set { _recipeCategories = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RecipeObject = new RecipeService();

            RecipeId = Convert.ToInt32(Request.QueryString["Id"]);

            frmRecipeEdit.DataBound += frmRecipeEdit_DataBinding;
            frmRecipeEdit.ItemUpdating += frmRecipeEdit_ItemUpdating;
            frmRecipeEdit.ItemUpdated += frmRecipeEdit_ItemUpdated;



            if (RecipeId > 0)
            {
                RecipeRecord = RecipeObject.GetSubmittedRecipeById(RecipeId);
                Ingredients = RecipeRecord.SubmittedRecipeIngredients.ToList();
            }
        }


        protected void frmRecipeEdit_DataBinding(object sender, EventArgs e)
        {
            if (RecipeId > 0)
            {
                // Categories
                var categories = this.RecipeRecord.SubmittedCategories;
                var chklist = (CheckBoxList)frmRecipeEdit.FindControl("chkRecipeCategories");
                categories.ForEach(category =>
                {
                    ListItem item = chklist.Items.FindByValue(category.ToString());
                    if (item != null)
                        item.Selected = true;
                });

            }
        }


        private void frmRecipeEdit_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            if (RecipeId > 0)
            {
                Ingredients = GetIngredients();
                RecipeObject.UpdateSubmittedRecipeIngredients(RecipeId, Ingredients);
            }
        }

        void frmRecipeEdit_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            if (RecipeId == 0)
            {
                RecipeId = RecipeObject.GetLatestRecipeId();

                // Update Recipes
                Ingredients = GetIngredients();
                RecipeObject.UpdateSubmittedRecipeIngredients(RecipeId, Ingredients);
            }

            // Update Images

            var upload = (FileUpload)((FormView)sender).FindControl("uploadRecipe");

            if (upload.HasFile)
            {
                // Upload File to DB
                RecipeObject.UploadRecipeImage(RecipeId, upload.PostedFile.InputStream);
            }

            if (Request.QueryString["Id"] == null)
            {
                Response.Redirect("Recipe.aspx?Id=" + RecipeId);
            }
        }


        private List<SubmittedRecipeIngredient> GetIngredients()
        {
            var ingredientsList = (DataList)(frmRecipeEdit).FindControl("dlIngredients");

            List<SubmittedRecipeIngredient> ingredients = (from DataListItem item in ingredientsList.Items
                                                  let ingredient_ID =
                                                      Convert.ToInt32(
                                                          ingredientsList.DataKeys[item.ItemIndex].ToString())
                                                           select new SubmittedRecipeIngredient
                                                  {
                                                      RecipeID = RecipeId,
                                                      IngredientID = ingredient_ID,
                                                      Quantity = ((TextBox)item.FindControl("txtQty")).Text,
                                                      Unit = ((TextBox)item.FindControl("txtUnit")).Text,
                                                      Item = 
                                                          ((TextBox)item.FindControl("txtItem")).Text,
                                                  }).ToList();

            return ingredients;
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            Ingredients = GetIngredients();
            Ingredients.Add(new SubmittedRecipeIngredient { RecipeID = RecipeId });

            var ingredientsList = (DataList)(frmRecipeEdit).FindControl("dlIngredients");
            ingredientsList.DataSource = Ingredients;
            ingredientsList.DataBind();

            //frmRecipeEdit.DataBind();
        }



    }
}