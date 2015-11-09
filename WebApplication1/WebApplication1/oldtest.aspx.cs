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
using System.Diagnostics;


namespace WebApplication1
{
    public partial class oldtest : System.Web.UI.Page
    {
    
        XmlDocument xmldoc = new XmlDocument();
        List<Test> testlist = new List<Test>();
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
            XmlNodeList nodes = xmldoc.SelectNodes("categories/question");
            
            foreach (XmlNode node in nodes)
            {
                Test t = new Test();

                //Debug.WriteLine(node.FirstChild.InnerText);

                string q = node.FirstChild.InnerText;
                string id = node.Attributes["id"].Value;
                //Debug.WriteLine(node["answer"].Attributes["id"].ToString());
                for(int i = 0; i <4; i++)
                {
                    string a = node["answer"].ChildNodes[i].InnerText;
                    string b = node["useranswer"].ChildNodes[i].InnerText;
                    t.setAnswers(a);
                    t.setYouranwser(b);
                }



                t.setId(id);
                t.setQuestion(q);
                //t.setAnswers(a);
               
            }
            
            //Response.Redirect("oldtest.xml");
            
        }
    }
}