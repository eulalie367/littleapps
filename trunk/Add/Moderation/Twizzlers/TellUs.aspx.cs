#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hershey.DataLayer;
using umbraco.BasePages;

#endregion

namespace Hershey.Web.UmbracoAddons.Moderation.Twizzlers
{
    public partial class TellUs : UmbracoEnsuredPage
    {
        #region Page_Load, OnInit, OnPreRender
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Bind();
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            stateFilter.OnFilterChanged += stateFilter_OnFilterChanged;

            gridView.RowEditing += gridView_RowEditing;
            gridView.RowCommand += gridView_RowCommand;
            gridView.RowCancelingEdit += gridView_RowCancelingEdit;
            gridView.RowUpdating += gridView_RowUpdating;
            gridView.PageIndexChanging += gridView_PageIndexChanging;
        }

        const int APPROVE_COLUMN_INDEX = 5;
        const int REJECT_COLUMN_INDEX = 6;

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

        #region Bind()
        protected void Bind()
        {
            using (var db = new ContentDB(ConfigurationManager.ConnectionStrings["ContentDB"].ConnectionString))
            {
                var query = from t in db.Twizzlers_SuggestedUses
                            select new { t.Id, t.InsertedDate, t.BirthDate, t.Suggestion, t.FirstName, t.State, t.IsApproved};

                switch (stateFilter.CurrentStateFilter)
                {
                    case ModerationState.Pending:
                        query = query.Where(x => x.IsApproved == null);
                        break;
                    case ModerationState.Accepted:
                        query = query.Where(x => x.IsApproved == true);
                        break;
                    case ModerationState.Rejected:
                        query = query.Where(x => x.IsApproved == false);
                        break;
                }

                gridView.DataSource = query.OrderByDescending(x => x.InsertedDate);
                gridView.DataBind();
            }
        }
        #endregion
        #region gridView event handlers
        void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid id = (Guid)gridView.DataKeys[e.RowIndex].Value;
            string name = ((TextBox)gridView.Rows[e.RowIndex].FindControl("txtName")).Text;
            string suggestion = ((TextBox)gridView.Rows[e.RowIndex].FindControl("txtSuggestion")).Text;

            ContentService.UpdateTwizzlersTellUsDetails(id, name, suggestion);

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
                Guid id = (Guid)gridView.DataKeys[idx].Value;

                switch (e.CommandName.ToLower())
                {
                    case "approve":
                        ContentService.UpdateTwizzlersTellUsApproval(id, true);
                        Bind();
                        break;
                    case "reject":
                        ContentService.UpdateTwizzlersTellUsApproval(id, false);
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