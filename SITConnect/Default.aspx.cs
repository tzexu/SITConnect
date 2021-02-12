using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SITConnect
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Login is successful");

                    Session["User"] = "tom";
                }
                
                
                

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("not logged in");

                Response.Redirect("Login.aspx", false);

            }
        }
    }
}