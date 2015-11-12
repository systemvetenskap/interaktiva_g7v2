using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Npgsql;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Timers;

namespace WebApplication1
{
    public partial class index : System.Web.UI.Page
    {
        public string fname, lname, auth, test;
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");

        protected void btnemp_Click(object sender, EventArgs e)
        {
            string s = TextBox1.Text;
            Application["user"] = s;
            Application["role"] = "member";
            Response.Redirect("mytests.aspx");
        }

        protected void btnlead_Click(object sender, EventArgs e)
        {
            Application["role"] = "leader";
            string s = TextBox1.Text;
            Application["user"] = s;
            Response.Redirect("testres.aspx?id=9");
            //kommentar
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        
        }
    }
}