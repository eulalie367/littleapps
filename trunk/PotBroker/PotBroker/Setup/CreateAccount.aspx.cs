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
using PotBroker.Management;

namespace PotBroker
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            subCreateAccount.ServerClick += new EventHandler(subCreateAccount_ServerClick);
        }

        protected void subCreateAccount_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserName.Value))
            {
                lblUserName.Style.Add("color", "red");
                return;
            }
            if (string.IsNullOrEmpty(tbPassWord.Value) || tbPassWord.Value != tbConfirmPass.Value)
            {
                lblConfirmPassword.Style.Add("color", "red");
                return;
            }
            if (string.IsNullOrEmpty(tbEmailAddress.Value) || tbEmailAddress.Value != tbConfirmEmailAddress.Value)
            {
                lblConfirmEmail.Style.Add("color", "red");
                return;
            }

            Create(tbUserName.Value, tbPassWord.Value, tbEmailAddress.Value);
        }
        protected bool Create(string userName, string password, string email)
        {
            string conString = ConfigurationManager.ConnectionStrings["base"].ConnectionString;

            if (!string.IsNullOrEmpty(conString))
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("Sproc_CreateAccount", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;

                        password = PotBroker.Management.Base.encrypt(password);

                        com.Parameters.Add(new SqlParameter("@UserName", userName));
                        com.Parameters.Add(new SqlParameter("@Password", password));
                        com.Parameters.Add(new SqlParameter("@Email", email));
                        SqlParameter accountID = new SqlParameter("@AccountID", SqlDbType.Int);
                        accountID.Direction = ParameterDirection.Output;
                        com.Parameters.Add(accountID);

                        com.Connection.Open();
                        com.ExecuteNonQuery();
                        com.Connection.Close();

                        if (accountID.Value != null)
                        {
                            int? a = accountID.Value as int?;
                            if (a.HasValue && a.Value > 0)
                            {
                                string key = password.ToLower() + userName.ToLower();
                                Response.Write("http://potbroker/setup/validate.aspx?key=" + HttpUtility.UrlEncode(Base.encrypt(a.Value.ToString(), key)));
                                return true;
                            }
                        }
                    }
                }
            }
            else
                throw new Exception("The connection string is invalid");

            return false;
        }

    }
}
