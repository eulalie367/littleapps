#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;

#endregion

namespace Hershey.Web.UmbracoAddons.KitchensCMS
{
    public partial class RecipeEditForm : HersheyControl
    {

        #region Properties

        public Recipe RecipeRecord { get; set; }

        private List<Category> _recipeCategories;

        public List<Category> RecipeCategories
        {
            get { return _recipeCategories ?? (_recipeCategories = RecipeObject.GetAllRecipeCategories()); }
            set { _recipeCategories = value; }
        }

        public List<Occasion> Occasions { get; set; }
        public List<Product> Products { get; set; }
        // public List<Category> Ideas { get; set; }
        public int RecipeId { get; set; }

        protected bool Active
        {
            get { return RecipeRecord != null && RecipeRecord.active == 'Y'; }
            set
            {
                RecipeRecord.active = value ? 'Y' : 'N';
            }
        }

        protected List<RecipeIngredient> Ingredients { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            RecipeId = Convert.ToInt32(Request.QueryString["Id"]);

            frmRecipeEdit.DataBound += frmRecipeEdit_DataBinding;
            frmRecipeEdit.ItemUpdating += frmRecipeEdit_ItemUpdating;
            frmRecipeEdit.ItemUpdated += frmRecipeEdit_ItemUpdated;

            if (RecipeId > 0)
            {
                RecipeRecord = RecipeObject.GetRecipeById(RecipeId);
                Ingredients = RecipeRecord.RecipeIngredients.OrderBy(r => r.rank).ToList();
            }

            if (IsPostBack) return;

            if (RecipeId == 0)
            {
                Page.Title = "Add New Recipe";
            }

            if (RecipeRecord == null || RecipeRecord.RecipeIngredients.Count == 0)
            {

                Ingredients = new List<RecipeIngredient>
                                  {
                                      new RecipeIngredient {Recipe_ID = 0, active = 'Y'},
                                      new RecipeIngredient {Recipe_ID = 0, active = 'Y'},
                                      new RecipeIngredient {Recipe_ID = 0, active = 'Y'},
                                      new RecipeIngredient {Recipe_ID = 0, active = 'Y'}
                                  };
            }

            #region DropDownList Values

            RecipeCategories = RecipeObject.GetAllRecipeCategories();
            RecipeCategories.Insert(0, new Category { category_name = "Select One:" });

            Occasions = RecipeObject.GetRecipeOccasions(false);
            Products = RecipeObject.GetProducts();

            #endregion
        }

        protected void frmRecipeEdit_DataBinding(object sender, EventArgs e)
        {
            if (RecipeId > 0)
            {
                // Occasions
                var selectedOccasions = RecipeObject.GetRecipeOccasionsForRecipe(this.RecipeId);
                var chklist = (CheckBoxList)frmRecipeEdit.FindControl("cblOccasions");
                selectedOccasions.ForEach(occasion =>
                                              {
                                                  ListItem item = chklist.Items.FindByValue(occasion.ToString());
                                                  if (item != null)
                                                      item.Selected = true;
                                              });


                // Products

                List<int> selectedProducts = RecipeObject.GetRecipeProductsForRecipe(this.RecipeId);
                chklist = (CheckBoxList)frmRecipeEdit.FindControl("cblProducts");
                selectedProducts.ForEach(product =>
                                             {
                                                 ListItem item = chklist.Items.FindByValue(product.ToString());
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
                RecipeObject.UpdateRecipeIngredients(RecipeId, Ingredients);
            }
        }

        void frmRecipeEdit_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = e.Exception.Message;
                // e.ExceptionHandled = true;
                return;
            }
            if (RecipeId == 0)
            {
                RecipeId = RecipeObject.GetLatestRecipeId();

                // Update Recipes
                Ingredients = GetIngredients();
                RecipeObject.UpdateRecipeIngredients(RecipeId, Ingredients);
            }

            // Update Occasions
            UpdateRecipeOccasions();


            UpdateRecipeProducts();

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

        private void UpdateRecipeProducts()
        {
            var productsList = (CheckBoxList)(frmRecipeEdit).FindControl("cblProducts");

            var products = (from ListItem item in productsList.Items where item.Selected select Convert.ToInt32(item.Value)).ToList();
            RecipeObject.UpdateRecipeproducts(RecipeId, products);
        }

        private void UpdateRecipeOccasions()
        {
            var occasionsList = (CheckBoxList)(frmRecipeEdit).FindControl("cblOccasions");

            var occasions = (from ListItem item in occasionsList.Items where item.Selected select Convert.ToInt32(item.Value)).ToList();
            RecipeObject.UpdateRecipeOccasions(RecipeId, occasions);
        }

        private List<RecipeIngredient> GetIngredients()
        {
            var ingredientsList = (DataList)(frmRecipeEdit).FindControl("dlIngredients");

            List<RecipeIngredient> ingredients = (from DataListItem item in ingredientsList.Items
                                                  let ingredient_ID =
                                                      Convert.ToInt32(
                                                          ingredientsList.DataKeys[item.ItemIndex].ToString())
                                                  select new RecipeIngredient
                                                             {
                                                                 Recipe_ID = RecipeId,
                                                                 ingredient_ID = ingredient_ID,
                                                                 active = 'Y',
                                                                 quantity = ((TextBox)item.FindControl("txtQty")).Text,
                                                                 unit = ((TextBox)item.FindControl("txtUnit")).Text,
                                                                 ingredient_info =
                                                                     ((TextBox)item.FindControl("txtUnitInfo")).Text,
                                                                 Ingredient =
                                                                     ((TextBox)item.FindControl("txtName")).Text,
                                                                 ingredient_info2 =
                                                                     ((TextBox)item.FindControl("txtIngredientInfo")).
                                                                     Text,
                                                             }).ToList();

            return ingredients;
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            Ingredients = GetIngredients();
            Ingredients.Add(new RecipeIngredient { Recipe_ID = RecipeId });

            var ingredientsList = (DataList)(frmRecipeEdit).FindControl("dlIngredients");
            ingredientsList.DataSource = Ingredients;
            ingredientsList.DataBind();

            //frmRecipeEdit.DataBind();
        }
    }
}