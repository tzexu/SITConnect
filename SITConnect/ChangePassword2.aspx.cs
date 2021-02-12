using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Drawing; //to change color
using System.Text.RegularExpressions;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SITConnect
{
    public partial class ChangePassword2 : System.Web.UI.Page
    {
        string SITCONNECTDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_changePassword_Click(object sender, EventArgs e)
        {
            //string old_pwd = HttpUtility.HtmlEncode(tb_old_password.Text.ToString().Trim());

            string new_pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());

            //System.Diagnostics.Debug.WriteLine("old password: " + old_pwd);
            System.Diagnostics.Debug.WriteLine("new password: " + new_pwd);

            //steps:

            // already authenticated through email

            // 


            string email = Session["Email"].ToString();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);

            string dbHash2 = getDBHash2(email);
            string dbSalt2 = getDBSalt2(email);



            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = new_pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    System.Diagnostics.Debug.WriteLine(" is it true? " + (userHash == dbHash));



                    string pwdWithSalt2 = new_pwd + dbSalt2;
                    byte[] hashWithSalt2 = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt2));
                    string userHash2 = Convert.ToBase64String(hashWithSalt2);



                    // if user's password matches database password
                    if (userHash.Equals(dbHash))
                    {
                        System.Diagnostics.Debug.WriteLine("Entered Password Matches Password in Database");
                        ;
                        lbl_errorMsg.Text = "The password you have entered matches a recent password that you have set. Please create a different password";
                        lbl_errorMsg.ForeColor = Color.Red;

                        //string new_pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());

                    }

                    else if (userHash2.Equals(dbHash2))
                    {
                        System.Diagnostics.Debug.WriteLine("Entered Password Matches Password in Database");
                        ;
                        lbl_errorMsg.Text = "The password you have entered matches a recent password that you have set. Please create a different password";
                        lbl_errorMsg.ForeColor = Color.Red;

                        //string new_pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());

                    }

                    //else if (userHash2.Equals(dbHash2))
                    //{
                    //    lbl_errorMsg.Text = "what";
                    //}

                    else
                    {

                        //Server Side Check making sure password does indeed meet all the complexity requirements stated in "Password Complexity Requirements"
                        if ((new_pwd.Length >= 12) && (Regex.IsMatch(new_pwd, "[a-z]")) && (Regex.IsMatch(new_pwd, "[A-Z]")) && (Regex.IsMatch(new_pwd, "[0-9]")) && (Regex.IsMatch(new_pwd, "[^a-zA-Z0-9]")))
                        {
                            //lbl_testing.Text = "success";
                            System.Diagnostics.Debug.WriteLine("password change successful");


                            //SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
                            //string sql = "update Account SET PasswordHash=@PASSWORDHASH, PasswordSalt=@PASSWORDSALT, IV=@IV, Key=@KEY WHERE Email=@EMAIL";
                            //SqlCommand cmd = new SqlCommand(sql, connection);
                            //cmd.Parameters.AddWithValue("@EMAIL", email);


                            //Generate random "salt"
                            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                            byte[] saltByte = new byte[8];

                            //Fills array of bytes with a cryptographically strong sequence of random values.
                            rng.GetBytes(saltByte);
                            salt = Convert.ToBase64String(saltByte);

                            SHA512Managed hashing3 = new SHA512Managed();

                            string pwdWithSalt3 = new_pwd + salt;
                            byte[] plainHash = hashing3.ComputeHash(Encoding.UTF8.GetBytes(new_pwd));
                            byte[] hashWithSalt3 = hashing3.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt3));

                            finalHash = Convert.ToBase64String(hashWithSalt3);


                            RijndaelManaged cipher = new RijndaelManaged();
                            cipher.GenerateKey();
                            Key = cipher.Key;
                            IV = cipher.IV;

                            //SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
                            //string sql = "update Account SET PasswordHash=@PASSWORDHASH, PasswordSalt=@PASSWORDSALT, IV=@IV, Key=@KEY WHERE Email=@EMAIL";
                            //SqlCommand cmd = new SqlCommand(sql, connection);
                            //cmd.Parameters.AddWithValue("@EMAIL", email);

                            updatePassword();


                            Response.Redirect("Default.aspx", false);


                            //MyDBServiceReference.Service1Client client = new MyDBServiceReference.Service1Client();
                            //int result = client.CreateEmployee(tbNric.Text, tbName.Text, dob, ddlDept.Text, wage);

                        }
                        else
                        {
                            //lbl_testing.Text = "your new password does not meet password requirements";

                            System.Diagnostics.Debug.WriteLine("your new password does not meet password requirements");

                        }


                    }


                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }



            //System.Diagnostics.Debug.WriteLine("Password changed successfully");


            //Response.Redirect("Default.aspx");
        }
            

    

        public void updatePassword()
        {

            string email = Session["Email"].ToString();

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            //string sql = "update Account SET PasswordHash=@PASSWORDHASH, PasswordSalt=@PASSWORDSALT, IV=@IV, Key=@KEY WHERE Email=@EMAIL";

            string sql = "update Account SET PasswordHash2=PasswordHash, PasswordSalt2=PasswordSalt WHERE Email=@EMAIL";

            string sql2 = "update Account SET PasswordHash=@PASSWORDHASH, PasswordSalt=@PASSWORDSALT, LastPasswordChange=@LASTPASSWORDCHANGE WHERE Email=@EMAIL";

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@EMAIL", email);

            SqlCommand cmd2 = new SqlCommand(sql2, connection);
            cmd2.Parameters.AddWithValue("@EMAIL", email);

            cmd2.Parameters.AddWithValue("@PASSWORDHASH", finalHash);
            cmd2.Parameters.AddWithValue("@PASSWORDSALT", salt);

            cmd2.Parameters.AddWithValue("@LASTPASSWORDCHANGE", DateTime.Now);

            //cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
            //cmd.Parameters.AddWithValue("@KEY", Convert.ToBase64String(Key2));


            try
            {
                connection.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }




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









        protected string getDBHash2(string email)
        {
            string h = null;

            //string SITCONNECTDBConnectionString = ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            string sql = "select PasswordHash2 FROM Account WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);



            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash2"] != null)
                        {
                            if (reader["PasswordHash2"] != DBNull.Value)
                            {
                                h = reader["PasswordHash2"].ToString();
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



        protected string getDBSalt2(string email)
        {
            string s = null;

            //string SITCONNECTDBConnectionString = ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            string sql = "select PASSWORDSALT2 FROM ACCOUNT WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT2"] != null)
                        {
                            if (reader["PASSWORDSALT2"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT2"].ToString();
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



    }
}