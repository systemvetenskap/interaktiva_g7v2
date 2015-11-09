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
        TableRow row1, row2, row3, row4, row5, row6;
        TableCell cell1, cell2, cell3, cell4, cell5, cell6, imgcell;
        Label lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7;
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {


            loadXml(1);
            loadQuest();
            loadTable();


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

                string q = node.FirstChild.InnerText;
                string id = node.Attributes["id"].Value;          
                for(int i = 0; i <4; i++)
                {
                    string a = node["answer"].ChildNodes[i].InnerText;
                    bool bb = node["useranswer"].HasChildNodes;
                    if( bb == true)
                    {
                        string b = node["useranswer"].ChildNodes[i].InnerText;
                        t.setYouranwser(b);
                    }
                   
                    t.setAnswers(a);
                    
                }



                t.setId(id);
                t.setQuestion(q);
                testlist.Add(t);
               
            }
            
            //Response.Redirect("oldtest.xml");
            
        }
        protected void loadTable()
        {
            foreach(var x in testlist)
            {
                row1 = new TableRow();
                row2 = new TableRow();
                row3 = new TableRow();
                row4 = new TableRow();
                row5 = new TableRow();
                row6 = new TableRow();

                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                cell4 = new TableCell();
                cell5 = new TableCell();
                cell6 = new TableCell();
                imgcell = new TableCell();

                lbl1 = new Label();
                lbl2 = new Label();
                lbl3 = new Label();
                lbl4 = new Label();
                lbl4 = new Label();
                lbl5 = new Label();
                lbl6 = new Label();
                lbl7 = new Label();

                lbl1.Text = x.getQuestion();
                List<string> ans = x.getAnswers();
                List<string> yans = x.getYouranswers();
                for(int i = 0; i <= ans.Count;i++)
                {
                    Label lbl = new Label();
                    lbl.Text = ans[i].ToString();

                }


            }


        }
    }
}