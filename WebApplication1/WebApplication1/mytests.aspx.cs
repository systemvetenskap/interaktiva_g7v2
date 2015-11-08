using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class mytests : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLicenseTest_Click(object sender, EventArgs e)
        {
            Application["type"] = "a";
            Response.Redirect("dotest.aspx");

        }

        protected void btnUpdateTest_Click(object sender, EventArgs e)
        {
            Application["type"] = "b";
            Response.Redirect("dotest.aspx");
        }
    }
}