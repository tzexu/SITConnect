using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SITConnect.CustomError
{
    public partial class HTTP500Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_returnToHomePage(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("but the code is executed");
            
            Response.Redirect("Default.aspx");
        }
    }
}