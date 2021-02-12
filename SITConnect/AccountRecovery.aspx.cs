using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.Net.Mail;

//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System;
//using System.Threading.Tasks;

namespace SITConnect
{
    public partial class AccountRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //static async Task Execute()
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("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");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("test@example.com", "Example User");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress("test@example.com", "Example User");
        //    var plainTextContent = "and easy to do anywhere, even with C#";
        //    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}

        public static void Email(string htmlString, string email)
        {
            try
            {


                SmtpClient smtpClient = new SmtpClient();
                //{
                //    Port = 587,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    Credentials = new NetworkCredential("sitconnect1234@gmail.com", "SITconnect1234"),
                //    Host = "smtp.gmail.com", // for gmail host
                //    UseDefaultCredentials = false,
                //    EnableSsl = true
                //};
                

                //smtpClient.Send("sitconnect1234@gmail.com", "tzexuan.tan@gmail.com", "subject", "body");

                MailMessage message = new MailMessage()
                {
                    From = new MailAddress("sitconnect1234@gmail.com"),
                    Subject = "Recover Account",
                    Body = htmlString,
                    IsBodyHtml = true //to make message body as html  
                };

                //message.Body = htmlString;

                //message.To.Add(new MailAddress("tzexuan.tan@gmail.com"));

                message.To.Add(email);

                System.Diagnostics.Debug.WriteLine("email has been sent successfully?");

                smtpClient.Send(message);

                //smtp.Send(message);
            }
            catch (Exception) { }
        }


        //changed from private to protected
        protected void btn_sendAccountRecoveryEmail(object sender, EventArgs e)
        {

            string email = tb_email.Text.ToString().Trim();

            string greeting = "Dear " + tb_firstName.Text.ToString().Trim() + " " + tb_lastName.Text.ToString().Trim() + "," + "<br /> <br />";

            string closing = "<br /><br /> Regards, <br /> SIT Connect";

            //string htmlString = getHtml(dgStudent); //here you will be getting an html string  
            string htmlString = greeting + "Click here to reset your password: https://localhost:44381/ChangePassword2" + closing;
            Email(htmlString, email); //Pass html string to Email function.  

            //Session["Email"] = email;

            Response.Redirect("EmailSent.aspx");

        }


    }
}