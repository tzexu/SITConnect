using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing; //to change color

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;

namespace SITConnect
{
    public partial class Login : System.Web.UI.Page
    {

        string SITCONNECTDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {

            btn_recoverAccount.Style["display"] = "none";

        }

        protected void btn_recoverAccount_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("go to AccountRecovery");


            Session["Email"] = tb_email_Login.Text;


            Response.Redirect("AccountRecovery.aspx");
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {

            string pwd = HttpUtility.HtmlEncode(tb_password_Login.Text.ToString().Trim());
            string email = HttpUtility.HtmlEncode(tb_email_Login.Text.ToString().Trim());
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);

            //int loginFailCount = getLoginFailCount(email);
            //System.Diagnostics.Debug.WriteLine("here is login fail count: + ", loginFailCount.ToString());

            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash.Equals(dbHash))
                    {
                        System.Diagnostics.Debug.WriteLine("Login Successful");

                        Session["LoggedIn"] = email;

                        //loginFailCount = 0;

                        //creates a new GUID and saves it into session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        //create a new cookie with guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));


                        SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
                        string sql = "update Account SET LoginFailCount=0 WHERE Email=@EMAIL";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@EMAIL", email);
                        //command.Parameters.AddWithValue("@LOGINFAILCOUNT", loginFailCount);
                        try
                        {
                            connection.Open();

                            command.ExecuteNonQuery();

                            //using (SqlDataReader reader = command.ExecuteReader())
                            //{

                            //    //while (reader.Read())
                            //    //{
                            //    //    if (reader["PasswordHash"] != null)
                            //    //    {
                            //    //        if (reader["PasswordHash"] != DBNull.Value)
                            //    //        {
                            //    //            h = reader["PasswordHash"].ToString();
                            //    //        }
                            //    //    }
                            //    //}

                            //}
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.ToString());
                        }
                        finally { connection.Close(); }


                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {

                        //loginFailCount += 1;

                        //Boolean accountLockOut = false;

                        //int loginFailCount = -2;


                        int loginFailCount = getLoginFailCount(email);

                        System.Diagnostics.Debug.WriteLine("here is previous login fail count: " + loginFailCount.ToString());

                        // if loginFailCount is already at 2, that means that this failed login attempt is the third one
                        if (loginFailCount == 2)
                        {

                            string errorMsg = "Account Locked Out!!";
                            System.Diagnostics.Debug.WriteLine(errorMsg);

                            lbl_errorMsg.Text = "You have failed the login 3 times. Your account is now locked.";

                            lbl_errorMsg.ForeColor = Color.Red;

                            btn_recoverAccount.Style["display"] = "block";

                        }
                        else
                        {

                            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
                            string sql = "update Account SET LoginFailCount=LoginFailCount+1 WHERE Email=@EMAIL";
                            SqlCommand command = new SqlCommand(sql, connection);
                            command.Parameters.AddWithValue("@EMAIL", email);

                            //command.Parameters.AddWithValue("@LOGINFAILCOUNT", SqlDbType.Int);

                            //command.Parameters["@LOGINFAILCOUNT"].Value = 1;

                            //command.Parameters.AddWithValue("@LOGINFAILCOUNT", loginFailCount);
                            try
                            {
                                connection.Open();

                                command.ExecuteNonQuery();
                                //using (SqlDataReader reader = command.ExecuteReader())
                                //{

                                //    while (reader.Read())
                                //    {
                                //        if (reader["LoginFailCount"] != null)
                                //        {
                                //            if (reader["LoginFailCount"] != DBNull.Value)
                                //            {
                                //                string y = reader["LoginFailCount"].ToString();
                                //                loginFailCount = Int32.Parse(y);

                                //                System.Diagnostics.Debug.WriteLine("login fail count: " + loginFailCount);

                                //                if (loginFailCount >= 3)
                                //                {
                                //                    accountLockOut = true;
                                //                }

                                //            }
                                //        }
                                //    }

                                //}
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.ToString());
                            }
                            finally { connection.Close(); }


                            lbl_errorMsg.Text = "Email or password is not valid. Please try again.";
                            lbl_errorMsg.ForeColor = Color.Red;



                            System.Diagnostics.Debug.WriteLine("login fail count = " + (loginFailCount+1).ToString());


                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }

        }


        protected string getDBHash(string email)
        {
            string h = null;

            //string SITCONNECTDBConnectionString = ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);



            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
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
            return h;
        }



        protected string getDBSalt(string email)
        {
            string s = null;

            //string SITCONNECTDBConnectionString = ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
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
            return s;
        }



        protected int getLoginFailCount(string email)
        {
            int l = 0;

            //string SITCONNECTDBConnectionString = ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            string sql = "select LOGINFAILCOUNT FROM ACCOUNT WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["LOGINFAILCOUNT"] != null)
                        {
                            if (reader["LOGINFAILCOUNT"] != DBNull.Value)
                            {
                                string x = reader["LOGINFAILCOUNT"].ToString();
                                l = Int32.Parse(x);
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
            return l ;
        }



        //public class MyObject
        //{
        //    public string success { get; set; }
        //    public List<string> ErrorMessage { get; set; }
        //}


        //public bool ValidateCaptcha()
        //{
        //    bool result = true; 



        //}



    }

}
