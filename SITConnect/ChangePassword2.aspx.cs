using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SITConnect
{
    public partial class ChangePassword2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_changePassword_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("Password changed successfully");


            Response.Redirect("Default.aspx");
        }

    }
}