using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace PotBroker.Listings
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptListing.ItemCreated += new EventHandler<ListViewItemEventArgs>(rptListing_ItemCreated);
            FillMeUp();
        }

        void rptListing_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem currentItem = (ListViewDataItem)e.Item;

            DataKey currentDataKey = this.rptListing.DataKeys[currentItem.DataItemIndex];

            HtmlGenericControl name = (HtmlGenericControl)currentItem.FindControl("name");
            name.InnerText = currentDataKey["Name"] as string ?? "Unknown";

            HtmlGenericControl overallRating = (HtmlGenericControl)currentItem.FindControl("overallRating");
            overallRating.InnerText = (currentDataKey["OverAllRating"] as double? ?? 0).ToString("0") + "% Overall";

            HtmlGenericControl vendorRating = (HtmlGenericControl)currentItem.FindControl("vendorRating");
            vendorRating.InnerText = (currentDataKey["VendorRating"] as double? ?? 0).ToString("0") + "% Vendor";

            HtmlGenericControl unitType = (HtmlGenericControl)currentItem.FindControl("unitType");
            unitType.InnerText = currentDataKey["UnitType"] as string ?? "1 gram";

            HtmlGenericControl unitPrice = (HtmlGenericControl)currentItem.FindControl("unitPrice");
            unitPrice.InnerText = (currentDataKey["UnitPrice"] as double? ?? 0).ToString("c");
        }

        private void FillMeUp()
        {
            string conString = ConfigurationManager.ConnectionStrings["base"].ConnectionString;


            if (!string.IsNullOrEmpty(conString))
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("Sproc_Products_Get", con))
                    {
                        com.Connection.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            rptListing.DataSource = dr;
                            rptListing.DataBind();
                        }
                        com.Connection.Close();
                    }
                }
            }
        }
    }
}
