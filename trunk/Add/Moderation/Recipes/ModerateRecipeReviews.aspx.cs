#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;
using umbraco.IO;
using umbraco.uicontrols;

#endregion

namespace Hershey.Web.UmbracoAddons.Moderation.Recipes
{
    public partial class ModerateRecipeReviews : UmbracoEnsuredPage
    {
        public RecipeService RecipeObject { get; set; }
        public MyKitchenService KitchenObject { get; set; }
        public int RecipeId { get; set; }

        #region Page_Load, OnInit, OnPreRender
        protected void Page_Load(object sender, EventArgs e)
        {
            RecipeObject = new RecipeService();
            KitchenObject = new MyKitchenService { AuthObject = new AuthService() };

            if (IsPostBack) return;
            Bind();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            stateFilter.OnFilterChanged += stateFilter_OnFilterChanged;
            // Event Handlers:
            gridView.RowEditing += gridView_RowEditing;
            gridView.RowUpdating += gridView_RowUpdating;
            gridView.RowCancelingEdit += gridView_RowCancelingEdit;
            gridView.RowCommand += gridView_RowCommand;
            gridView.PageIndexChanging += gridView_PageIndexChanging;
        }

        const int APPROVE_COLUMN_INDEX = 4;
        const int REJECT_COLUMN_INDEX = 5;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ModerationState state = stateFilter.CurrentStateFilter;

            //hide the Approve column if showing accepted values
            gridView.Columns[APPROVE_COLUMN_INDEX].Visible = (state != ModerationState.Accepted);

            //hide the Reject column if showing rejected values
            gridView.Columns[REJECT_COLUMN_INDEX].Visible = (state != ModerationState.Rejected);
        }
        #endregion

        #region utility functions
        protected string GetShortenedString(object theString, int maxLength)
        {
            string s = (string)theString;

            if (string.IsNullOrEmpty(s))
                return "";

            if (s.Length <= maxLength)
                return s;

            //else:
            return s.Substring(0, maxLength) + "...";
        }
        #endregion

        #region gridView event handlers

        private void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridView.EditIndex = -1;
            Bind();
        }

        private void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridView.EditIndex = e.NewEditIndex;
            Bind();
        }

        private void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int RecipeRatingsID = (int)gridView.DataKeys[e.RowIndex].Value;
            string comment = ((TextBox)gridView.Rows[e.RowIndex].FindControl("txtComment")).Text;

            KitchenObject.UpdateRecipeReview(RecipeRatingsID, comment);

            gridView.EditIndex = -1;

            Bind();
        }

        void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            Bind();
        }

        void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e != null && !string.IsNullOrEmpty(e.CommandName))
            {
                int idx = int.Parse(e.CommandArgument.ToString());
                int id = (int)gridView.DataKeys[idx].Value;

                switch (e.CommandName.ToLower())
                {
                    case "approve":
                        KitchenObject.UpdateRecipeReviewStatus(id, true);
                        Bind();
                        break;
                    case "reject":
                        KitchenObject.UpdateRecipeReviewStatus(id, false);
                        Bind();
                        break;
                }
            }
        }
        #endregion


        #region User control event handlers
        void stateFilter_OnFilterChanged(object sender, EventArgs e)
        {
            gridView.EditIndex = -1; //(in case we are currently in edit mode when navigating to a different 'tab'
            Bind();
        }
        #endregion

        #region Bind()
        private void Bind()
        {
            using (var db = new RecipeDB(ConfigurationManager.ConnectionStrings["KitchenDB"].ConnectionString))
            {
                var query = from rr in db.RecipeRatings
                            select new {
                                rr.RecipeID,
                                rr.Recipe.RecipeName,
                                rr.Approved,
                                rr.Created,
                                rr.UserName,
                                rr.Comment,
                                rr.RecipeRatingsID,
                                rr.Rating,
                                rr.TasteRating,
                                rr.DifficultyRating,
                                rr.AppearanceRating
                            };

                switch (stateFilter.CurrentStateFilter)
                {
                    case ModerationState.Pending:
                        query = query.Where(x => x.Approved == null);
                        break;
                    case ModerationState.Accepted:
                        query = query.Where(x => x.Approved == true);
                        break;
                    case ModerationState.Rejected:
                        query = query.Where(x => x.Approved == false);
                        break;
                }

                //txtDebug.Text = "rowcount: " + query.Count();

                gridView.DataSource = query.OrderByDescending(x => x.Created);
                gridView.DataBind();
            }
        }

        #endregion
    }
}