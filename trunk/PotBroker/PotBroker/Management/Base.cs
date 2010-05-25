using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Web.Caching;

namespace PotBroker.Management
{
    public class Base
    {
        internal static string encrypt(string password)
        {
            string rethash = "";
            try
            {

                System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
                System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
                byte[] combined = encoder.GetBytes(password);
                hash.ComputeHash(combined);
                rethash = Convert.ToBase64String(hash.Hash);
            }
            catch (Exception ex)
            {
                string strerr = "Error in HashCode : " + ex.Message;
            }
            return rethash;
        }
        internal static string encrypt(string str, string key)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(str);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        internal static string decrypt(string str, string key)
        {
            byte[] toEncryptArray = Convert.FromBase64String(str);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        internal static bool DoLogin(string userName, string password)
        {
            string conString = ConfigurationManager.ConnectionStrings["base"].ConnectionString;

            if (!string.IsNullOrEmpty(conString))
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("Sproc_Login", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;

                        password = PotBroker.Management.Base.encrypt(password);

                        com.Parameters.Add(new SqlParameter("@UserName", userName));
                        com.Parameters.Add(new SqlParameter("@Password", password));
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
                                User u = new User { AccountID = a.Value, LoggedIn = true, Password = password };
                                u.SaveAccountState();
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

        internal static bool Validate(string validationKey, string encryptedPasswordUserName)
        {
            string conString = ConfigurationManager.ConnectionStrings["base"].ConnectionString;

            string key = encryptedPasswordUserName.ToLower();
            validationKey = decrypt(validationKey, key);

            if (!string.IsNullOrEmpty(conString))
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("Sproc_ValidateAccount", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;

                        int vKey;
                        if (int.TryParse(validationKey, out vKey))
                        {
                            com.Parameters.Add(new SqlParameter("@Key", vKey));
                            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar, 100);
                            SqlParameter Password = new SqlParameter("@Password", SqlDbType.VarChar, 100);
                            UserName.Direction = ParameterDirection.Output;
                            com.Parameters.Add(UserName);
                            Password.Direction = ParameterDirection.Output;
                            com.Parameters.Add(Password);

                            com.Connection.Open();
                            com.ExecuteNonQuery();
                            com.Connection.Close();

                            if (UserName.Value != null && Password.Value != null)
                            {
                                string sUserName = UserName.Value as string;
                                string sPassword = Password.Value as string;
                                if (!string.IsNullOrEmpty(sUserName) && !string.IsNullOrEmpty(sPassword))
                                    return DoLogin(sUserName, sPassword);
                            }
                        }
                    }
                }
            }
            else
                throw new Exception("The connection string is invalid");
            return false;
        }
        [Serializable]
        internal class User
        {
            public User()
            {
                this.LoggedIn = false;
                this.Password = "";
                this.AccountID = -1;
            }
            public bool LoggedIn { get; set; }
            public string Password { get; set; }
            public int AccountID { get; set; }
            public string LastURL { get; set; }

            internal void SaveAccountState()
            {
                HttpContext context = HttpContext.Current;

                if (context != null)
                {
                    string accountid = encrypt(AccountID.ToString());
                    context.Response.AddHeader("pbaccount", accountid);
                    context.Response.AppendCookie(new HttpCookie("pbaccount", accountid));
                    context.Cache.Add("pbaccount" + accountid, this, null, Cache.NoAbsoluteExpiration, new TimeSpan(3, 0, 0), CacheItemPriority.Normal, null);
                }
                else
                    throw new Exception("WTF:  There is no HTTP Context Here...");
            }
            internal static User LoadAccountState()
            {
                HttpContext context = HttpContext.Current;
                User v = new User();
                if (context != null)
                {
                    string accountid = context.Request.Headers["pbaccount"];
                    if (string.IsNullOrEmpty(accountid))
                    {
                        HttpCookie c = context.Request.Cookies["pbaccount"];
                        if (c != null && !string.IsNullOrEmpty(c.Value))
                        {
                            accountid = c.Value;
                        }
                    }
                    if (!string.IsNullOrEmpty(accountid))
                    {
                        object acc = context.Cache["pbaccount" + accountid];
                        if (acc != null)
                        {
                            try
                            {
                                v = acc as User;
                                v.SaveAccountState();
                            }
                            catch
                            {
                            }
                        }
                    }
                    v.LoggedIn = !string.IsNullOrEmpty(accountid);
                }
                else
                    throw new Exception("WTF:  There is no HTTP Context Here...");

                return v;
            }
        }
    }
}
