using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using System.Drawing; //to change color

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Timers;

namespace SITConnect
{
    public partial class Registration : System.Web.UI.Page
    {


        string SITCONNECTDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SITCONNECTDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;


        static System.Timers.Timer _timer;
        static List<DateTime> _results = new List<DateTime>();


        protected void Page_Load(object sender, EventArgs e)
        {
            //Console.WriteLine("hihere");



            //this works fine
            //tb_password.Attributes.Add("onKeyUp", "alert('test');");

            //tb_firstName.Attributes.Add(required);

            tb_password.Attributes.Add("onKeyUp", "validate();");

            tb_password.Attributes.Add("type", "password");

            tb_confirmPassword.Attributes.Add("type", "password");

            //hide_show_password.Attributes.Add("ImageUrl", "Images/hide_password.svg");

            //tb_password.Attributes.Add("onKeyUp", "btn_checkPassword_Click(object sender, EventArgs e)");

            //tb_password.Attributes.Add("onKeyUp", btn_checkPassword_Click(object sender, EventArgs e));

            //tb_password.Attributes.Add("onKeyUp", Registration.btn_checkPassword_Click(object sender, EventArgs e));


            //tb_password.Attributes.Add("onKeyUp", "btn_checkPassword_Click;");

        }

        public void validatePassword(object sender, EventArgs e)
        {

            string password = tb_password.Text;

            //lbl_test.Text = "yo";

            
            
            
            //return "y";
            //    

            //    if (str.length < 8)
            //    {
            //        document.getElementById("lbl_pwdchecker").innerHTML = "Password Length Must be at least 8 Characters";
            //        document.getElementById("lbl_pwdchecker").style.color = "Red";
            //        return ("too_short");
            //    }

            //    else if (str.search(/[0 - 9] /) == -1)
            //    {
            //        document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 number";
            //        document.getElementById("lbl_pwdchecker").style.color = "Red";
            //        return ("no_number");
            //    }

            //    else if (str.search(/[a - z] /) == -1)
            //    {
            //        document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 lowercase letter";
            //        document.getElementById("lbl_pwdchecker").style.color = "Red";
            //        return ("no_lowercase_letter");
            //    }

            //    else if (str.search(/[A - Z] /) == -1)
            //    {
            //        document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 uppercase letter";
            //        document.getElementById("lbl_pwdchecker").style.color = "Red";
            //        return ("no_uppercase_letter");
            //    }

            //    // do the special character one here
            //    else if (str.search(/[^a - zA - Z0 - 9] /) == -1)
            //    {
            //        document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 special character";
            //        document.getElementById("lbl_pwdchecker").style.color = "Red";
            //        return ("no_uppercase_letter");
            //    }

            //    document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!"
            //document.getElementById("lbl_pwdchecker").style.color = "Blue";
        }

        private int checkPassword(string password)
        {
            int score = 0;

            


            //Include your implementation here
            //Score 1 Very Weak
            if (password.Length < 12)
            {
                return 1;
            }
            else
            {
                score = 1;
            }

            //Score 2 Weak
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score = 2;
            }

            //Score 3 Medium
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score = 3;
            }

            //Score 4 Strong
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score = 4;
            }

            //Score 5 Excellent
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                score = 5;
            }

            Console.WriteLine(score);
            return score;
        }

        protected void btn_checkPassword_Click(object sender, EventArgs e)
        {
            // implement codes for the button event
            // Extract data from textbox
            int scores = checkPassword(tb_password.Text);

            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Very Strong";
                    break;
                default:
                    break;
            }
            lbl_pwdchecker.Text = "Status : " + status;
            if (scores < 5)
            {
                lbl_pwdchecker.ForeColor = Color.Red;
                return;
            }
            lbl_pwdchecker.ForeColor = Color.Green;
        }



        protected void btn_Register_Click(object sender, EventArgs e)
        {
            
            //string password = tb_password.Text.ToString().Trim();

            string password = HttpUtility.HtmlEncode(tb_password.Text.ToString().Trim());

            //Server Side Check making sure password does indeed meet all the complexity requirements stated in "Password Complexity Requirements"
            if ((password.Length >= 12) && (Regex.IsMatch(password, "[a-z]")) && (Regex.IsMatch(password, "[A-Z]")) && (Regex.IsMatch(password, "[0-9]")) && (Regex.IsMatch(password, "[^a-zA-Z0-9]")))
            {
                lbl_testing.Text = "success";


                //Generate random "salt"
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                //Fills array of bytes with a cryptographically strong sequence of random values.
                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string pwdWithSalt = password + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                finalHash = Convert.ToBase64String(hashWithSalt);


                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;

                createAccount();



                System.Timers.Timer timer = new System.Timers.Timer(10000);

                //var timer = new Timer(3000);

                //timer.Elapsed += new ElapsedEventHandler(passwordExpired());

                timer.Enabled = true;
                _timer = timer;


                Response.Redirect("Login.aspx");


                //MyDBServiceReference.Service1Client client = new MyDBServiceReference.Service1Client();
                //int result = client.CreateEmployee(tbNric.Text, tbName.Text, dob, ddlDept.Text, wage);

            }
            else
            {
                
                //
                
                if (password.Length < 12)
                {
                    //lbl_testing.Text = "fail";
                    System.Diagnostics.Debug.WriteLine("fail");

                    //errorMsg.Text

                    // 

                }

                    //lbl_testing.Text = "fail";
                    System.Diagnostics.Debug.WriteLine("fail");
                // 

            }

           
        }


        static void passwordExpired()
        {
            System.Diagnostics.Debug.WriteLine("Your password has expired. Please change it now");
        }


        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SITCONNECTDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName, @LastName, @DOB, @CardNumber, @CardExpiry, @CVCorCVV, @Email, @EmailVerified, @PasswordHash, @PasswordSalt, @PasswordHash2, @PasswordSalt2, @DateTimeRegistered, @IV, @Key, @LoginFailCount, @LastPasswordChange)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlEncode(tb_firstName.Text.Trim()));
                            cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlEncode(tb_lastName.Text.Trim()));
                            cmd.Parameters.AddWithValue("@DOB", tb_DOB.Text.Trim());

                            cmd.Parameters.AddWithValue("@CardNumber", Convert.ToBase64String(encryptData(tb_creditCardNum.Text.Trim())));
                            cmd.Parameters.AddWithValue("@CardExpiry", tb_cardExpiry.Text.Trim());
                            cmd.Parameters.AddWithValue("@CVCorCVV", tb_CVCorCVV.Text.Trim());

                            cmd.Parameters.AddWithValue("@Email", HttpUtility.HtmlEncode(tb_email.Text.Trim()));
                            cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);

                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);

                            cmd.Parameters.AddWithValue("@PasswordHash2", "y");
                            cmd.Parameters.AddWithValue("@PasswordSalt2", "y");

                            cmd.Parameters.AddWithValue("@DateTimeRegistered", DateTime.Now);

                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));

                            cmd.Parameters.AddWithValue("@LoginFailCount", 0);

                            cmd.Parameters.AddWithValue("@LastPasswordChange", DateTime.Now);

                            cmd.Connection = con;

                            System.Diagnostics.Debug.WriteLine("this is con: " + con);
                            // con is System.Data.SqlClient.SqlConnection


                            System.Diagnostics.Debug.WriteLine("here is connection string: " + SITCONNECTDBConnectionString);


                            //using (SqlConnection con = new SqlConnection(SITCONNECTDBConnectionString)


                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }



    }
}


