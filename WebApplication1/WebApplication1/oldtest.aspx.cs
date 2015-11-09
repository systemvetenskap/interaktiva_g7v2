using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
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
    public partial class oldtest : System.Web.UI.Page
    {
    
        XmlDocument xmldoc = new XmlDocument();
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {


            loadXml(1);
            loadQuest();


        }
        protected void loadXml(int userid)
        {
            string sql = "select testxml from license_test where testid = 79";
            string xml = "";
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                xml = read[0].ToString();
            }
            conn.Close();
            xmldoc.LoadXml(string.Format(xml));

            xmldoc.Save(Server.MapPath("oldtest.xml"));
        }
        protected void loadQuest()
        {

            Response.Redirect("oldtest.xml");
            
        }
    }
}