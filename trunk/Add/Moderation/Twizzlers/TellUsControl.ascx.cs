#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Hershey.DataLayer;

#endregion

namespace Hershey.Web.UmbracoAddons.Moderation.Twizzlers
{
    public partial class TellUsControl : UserControl
    {
        public List<Twizzlers_SuggestedUse> DataRecord { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            lvTellUs.ItemCanceling += lvTellUs_ItemCanceling;
            lvTellUs.ItemUpdating += lvTellUs_ItemUpdating;
            lvTellUs.ItemEditing += lvTellUs_ItemEditing;
            lvTellUs.ItemUpdated += lvTellUs_ItemUpdated;
            lvTellUs.ItemCommand += lvTellUs_ItemCommand;
        }

        protected override void OnLoad(EventArgs e)
        {
            lvTellUs.EditIndex = editIndex.Value.ToInt() ?? -1;

            if (lvTellUs.EditIndex > -1)
                Bind();

            base.OnLoad(e);
        }

        private void lvTellUs_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void lvTellUs_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                ListViewDataItem item = lvTellUs.Items[e.ItemIndex];
                if (item != null)
                {
                    //instantiate controls.
                    HtmlInputText dob, name, state, message;
                    dob = item.FindControl("tbDOB") as HtmlInputText;
                    name = item.FindControl("tbName") as HtmlInputText;
                    state = item.FindControl("tbState") as HtmlInputText;
                    message = item.FindControl("tbMessage") as HtmlInputText;

                    var hid = item.FindControl("hidID") as HtmlInputHidden;

                    if (dob != null && name != null && state != null && message != null && hid != null)
                    {
                        var id = new Guid(hid.Value);

                        if (id != null)
                        {
                            using (
                                var db =
                                    new ContentDB(ConfigurationManager.ConnectionStrings["ContentDB"].ConnectionString))
                            {
                                Twizzlers_SuggestedUse record =
                                    db.Twizzlers_SuggestedUses.Where(t => t.Id == id).FirstOrDefault();

                                if (record != null)
                                {
                                    record.BirthDate = dob.Value.ToDateTime();
                                    record.FirstName = name.Value;
                                    record.State = state.Value;
                                    record.Suggestion = message.Value;
                                }
                                db.SubmitChanges();
                            }
                        }
                    }

                    lvTellUs.EditIndex = -1;
                    editIndex.Value = "-1";
                }
            }
            catch
            {
            }
        }

        private void lvTellUs_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvTellUs.EditIndex = -1;
            Bind();
        }

        private void lvTellUs_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvTellUs.EditIndex = e.NewEditIndex;
            editIndex.Value = e.NewEditIndex.ToString();
            Bind();
        }

        private void lvTellUs_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e != null && !string.IsNullOrEmpty(e.CommandName))
            {
                switch (e.CommandName.ToLower())
                {
                    case "approve":
                        ApproveSuggestion(e.Item);
                        break;
                    case "reject":
                        RejectSuggestion(e.Item);
                        break;
                }
            }
        }

        private void RejectSuggestion(ListViewItem listViewItem)
        {
            if (listViewItem != null)
            {
                var hid = listViewItem.FindControl("hidID") as HtmlInputHidden;

                if (hid != null)
                {
                    var id = new Guid(hid.Value);

                    if (id != null)
                    {
                        using (
                            var db = new ContentDB(ConfigurationManager.ConnectionStrings["ContentDB"].ConnectionString)
                            )
                        {
                            Twizzlers_SuggestedUse record =
                                db.Twizzlers_SuggestedUses.Where(t => t.Id == id).FirstOrDefault();

                            if (record != null)
                            {
                                record.IsApproved = false;
                            }
                            db.SubmitChanges();
                        }
                    }
                }
                Response.Redirect(Request.Url.PathAndQuery);
            }
        }

        private void ApproveSuggestion(ListViewItem listViewItem)
        {
            if (listViewItem != null)
            {
                var hid = listViewItem.FindControl("hidID") as HtmlInputHidden;

                if (hid != null)
                {
                    var id = new Guid(hid.Value);

                    if (id != null)
                    {
                        using (
                            var db = new ContentDB(ConfigurationManager.ConnectionStrings["ContentDB"].ConnectionString)
                            )
                        {
                            Twizzlers_SuggestedUse record =
                                db.Twizzlers_SuggestedUses.Where(t => t.Id == id).FirstOrDefault();

                            if (record != null)
                            {
                                record.IsApproved = true;
                            }
                            db.SubmitChanges();
                        }
                    }
                }
                Response.Redirect(Request.Url.PathAndQuery);
            }
        }

        public void Bind()
        {
            if (DataRecord != null)
            {
                lvTellUs.DataSource = DataRecord;
                lvTellUs.DataBind();
            }
        }


        public string ParseDate(object date)
        {
            if (date != null)
            {
                DateTime? d = date.ToString().ToDateTime();

                if (d.HasValue)
                    return d.Value.ToString("MM/dd/yyyy");
            }
            return "";
        }
    }
}