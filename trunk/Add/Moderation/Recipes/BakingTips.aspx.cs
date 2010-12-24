using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hershey.DataLayer.Recipes;
using umbraco.BasePages;

namespace Hershey.Web.UmbracoAddons.Moderation.Recipes
{
    public partial class BakingTips : UmbracoEnsuredPage
    {
        #region Page_Load, OnInit, OnPreRender
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Bind();

        }

        protected override void OnInit(EventArgs e)
        {
            //base.OnInit(e);

            if (!this.DesignMode)
            {
                stateFilter.OnFilterChanged += stateFilter_OnFilterChanged;

                gridView.RowEditing += gridView_RowEditing;
                gridView.RowCommand += gridView_RowCommand;
                gridView.RowCancelingEdit += gridView_RowCancelingEdit;
                gridView.RowUpdating += gridView_RowUpdating;
                gridView.PageIndexChanging += gridView_PageIndexChanging;
            }
        }

        const int APPROVE_COLUMN_INDEX = 6;
        const int REJECT_COLUMN_INDEX = 7;

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

        #region Bind()
        protected void Bind()
        {
            using (var db = new RecipeDB(ConfigurationManager.ConnectionStrings["KitchenDB"].ConnectionString))
            {
                var query = from t in db.BakingTips
                            join r in db.Recipes on t.RecipeId equals r.recipe_ID into joined
                            from r in joined.DefaultIfEmpty()
                            join c in db.Categories on t.CategoryId equals c.category_ID into j_categs
                            from c in j_categs.DefaultIfEmpty()
                            select new { t.SubmissionId, t.RecipeId, r.RecipeName, t.FirstName, t.Title, t.Tip, t.CreatedOn, t.IsEnabled, CategoryName = c.category_name };

                switch (stateFilter.CurrentStateFilter)
                {
                    case ModerationState.Pending:
                        query = query.Where(x => x.IsEnabled == null);
                        break;
                    case ModerationState.Accepted:
                        query = query.Where(x => x.IsEnabled == true);
                        break;
                    case ModerationState.Rejected:
                        query = query.Where(x => x.IsEnabled == false);
                        break;
                }

                //txtDebug.Text = "rowcount: " + query.Count();

                gridView.DataSource = query.OrderByDescending(x => x.CreatedOn);
                gridView.DataBind();
            }
        }
        #endregion

        #region gridView event handlers
        void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            string title = ((TextBox)gridView.Rows[e.RowIndex].FindControl("txtTitle")).Text;
            string tip = ((TextBox)gridView.Rows[e.RowIndex].FindControl("txtTip")).Text;

            MyKitchenService.BakingTipUpdate(id, title, tip);

            gridView.EditIndex = -1;
            Bind();
        }

        void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridView.EditIndex = -1;
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
                        MyKitchenService.BakingTipSetState(id, true);
                        Bind();
                        break;
                    case "reject":
                        MyKitchenService.BakingTipSetState(id, false);
                        Bind();
                        break;
                }
            }
        }

        void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridView.EditIndex = e.NewEditIndex;
            Bind();
        }

        void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            Bind();
        }
        #endregion

        #region user-control event handlers
        void stateFilter_OnFilterChanged(object sender, EventArgs e)
        {
            gridView.EditIndex = -1; //(in case we are currently in edit mode when navigating to a different 'tab'
            Bind();
        }
        #endregion
    }
}