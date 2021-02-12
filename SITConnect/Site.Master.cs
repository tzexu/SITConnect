using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SITConnect
{
    public partial class SiteMaster : MasterPage
    {

        string SITCONNECTDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //public void LogOut(object sender, EventArgs e)

        public void LogOut(object sender, EventArgs e)
        {
            //Like removing books from a bookshelf
            Session.Clear();

            //Like throwing away the bookshelf
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-10);
            }


            System.Diagnostics.Debug.WriteLine("User has successfully logged out");

        }

        protected void btn_goChangePassword(object sender, EventArgs e)
        {

            string email = Session["LoggedIn"].ToString();

            Session["ErrorMsg"] = null;

            DateTime now = DateTime.Now;
            DateTime lastPasswordChange = getLastPasswordChange(email);

            System.Diagnostics.Debug.WriteLine("now is " + now);
            System.Diagnostics.Debug.WriteLine("last password change: " + lastPasswordChange);

            System.Diagnostics.Debug.WriteLine(now - lastPasswordChange);
            System.Diagnostics.Debug.WriteLine((now - lastPasswordChange).Minutes);

            if ((now - lastPasswordChange).Minutes >= 2)
            {
                Response.Redirect("ChangePassword.aspx", false);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("please wait 2 mins before changing password");

                Session["ErrorMsg"] = "Please wait 2 mins before changing password";

            }
            
        }


        protected DateTime getLastPasswordChange(string email)
        {
            DateTime l = DateTime.Now;

            //string SITCONNECTDBConnectionString = ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            string sql = "select LASTPASSWORDCHANGE FROM ACCOUNT WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["LASTPASSWORDCHANGE"] != null)
                        {
                            if (reader["LASTPASSWORDCHANGE"] != DBNull.Value)
                            {
                                string x = reader["LASTPASSWORDCHANGE"].ToString();
                                l = Convert.ToDateTime(x);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            //l = Int64.Parse(l);
            return l;
        }


    }
}