using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SITConnect
{
    public partial class ChangePassword : System.Web.UI.Page
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
            string old_pwd = HttpUtility.HtmlEncode(tb_old_password.Text.ToString().Trim());
            string email = Session["LoggedIn"].ToString();
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);


            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = old_pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash.Equals(dbHash))
                    {
                        System.Diagnostics.Debug.WriteLine("Old Password Matches Password in Database");

                        string new_pwd = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());

                        //Server Side Check making sure password does indeed meet all the complexity requirements stated in "Password Complexity Requirements"
                        if ((new_pwd.Length >= 12) && (Regex.IsMatch(new_pwd, "[a-z]")) && (Regex.IsMatch(new_pwd, "[A-Z]")) && (Regex.IsMatch(new_pwd, "[0-9]")) && (Regex.IsMatch(new_pwd, "[^a-zA-Z0-9]")))
                        {
                            //lbl_testing.Text = "success";
                            System.Diagnostics.Debug.WriteLine("success");


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

                            SHA512Managed hashing2 = new SHA512Managed();

                            string pwdWithSalt2 = new_pwd + salt;
                            byte[] plainHash = hashing2.ComputeHash(Encoding.UTF8.GetBytes(new_pwd));
                            byte[] hashWithSalt2 = hashing2.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt2));

                            finalHash = Convert.ToBase64String(hashWithSalt2);


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

        

            System.Diagnostics.Debug.WriteLine("Password changed successfully");

            
            Response.Redirect("Default.aspx");
        }



        public void updatePassword()
        {

            string email = Session["LoggedIn"].ToString();

            SqlConnection connection = new SqlConnection(SITCONNECTDBConnectionString);
            //string sql = "update Account SET PasswordHash=@PASSWORDHASH, PasswordSalt=@PASSWORDSALT, IV=@IV, Key=@KEY WHERE Email=@EMAIL";

            string sql2 = "update Account SET PasswordHash2=PasswordHash, PasswordSalt2=PasswordSalt WHERE Email=@EMAIL";

            string sql = "update Account SET PasswordHash=@PASSWORDHASH, PasswordSalt=@PASSWORDSALT WHERE Email=@EMAIL";

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@EMAIL", email);

            cmd.Parameters.AddWithValue("@PASSWORDHASH", finalHash);
            cmd.Parameters.AddWithValue("@PASSWORDSALT", salt);

            //cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
            //cmd.Parameters.AddWithValue("@KEY", Convert.ToBase64String(Key2));


            try
            {
                connection.Open();

                cmd.ExecuteNonQuery();

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


    }
}