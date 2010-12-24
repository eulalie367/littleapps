#region

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Hershey.DataLayer.Recipes;

#endregion

namespace Hershey.Web.UmbracoAddons.Moderation.Recipes
{
    public partial class RecipeReviews : HersheyControl
    {
        //public List<RecipeRating> DataRecord { get; set; }

        public int RecipeId { get; set; }

        public void ModerateRecipes()
        {

            foreach (GridViewRow row in gvPendingRatings.Rows)
            {
                int recipeId = (int) gvPendingRatings.DataKeys[row.DataItemIndex].Value;

                CheckBox chkApproved = (CheckBox) row.FindControl("chkApproved");

//  Update Status:

                //KitchenObject.UpdateRecipeReviewStatus(KitchenObject.GetKitchenUserID(),recipeId,chkApproved.Checked);

            }

            




        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            gvPendingRatings.RowDeleting += gvPendingRatings_RowDeleting;
            gvPendingRatings.RowEditing += gvPendingRatings_RowEditing;
            gvPendingRatings.RowCommand += gvPendingRatings_RowCommand;
            gvPendingRatings.RowUpdating += gvPendingRatings_RowUpdating;

            //gvPendingRatings.ItemCanceling += gvPendingRatings_ItemCanceling;
            //gvPendingRatings.ItemUpdating += gvPendingRatings_ItemUpdating;
            //gvPendingRatings.ItemEditing += gvPendingRatings_ItemEditing;
            //gvPendingRatings.ItemUpdated += gvPendingRatings_ItemUpdated;
            //gvPendingRatings.ItemCommand += gvPendingRatings_ItemCommand;
        }

        #region gvPendingRatings
        

        private void gvPendingRatings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //  throw new NotImplementedException();
        }

        private void gvPendingRatings_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //     throw new NotImplementedException();
            
        }

        private void gvPendingRatings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int RecipeRatingsID = Convert.ToInt32(e.Keys[0].ToString());
            KitchenObject.DeleteRecipeReview(RecipeRatingsID);
        }

        void gvPendingRatings_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int recipeId = Convert.ToInt32(e.Keys[0].ToString());

            //KitchenObject.UpdateRecipeReview(KitchenObject.GetKitchenUserID(),recipeId,e.NewValues[1].ToString());
            
        }

      

        private void Bind()
        {
            var pendingRatings = KitchenObject.GetPendingReviewsForRecipe(this.RecipeId);

            var ratings = pendingRatings.Select(r => new
                                           {
                                               r.RecipeID,
                                               r.Recipe.RecipeName,
                                               r.Approved,
                                               r.Created,
                                               r.UserName,
                                               r.Comment
                                           });

            gvPendingRatings.DataSource = ratings;
            gvPendingRatings.DataBind();
        }

        #endregion


        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack) return;
            Bind();
        }
    }
}