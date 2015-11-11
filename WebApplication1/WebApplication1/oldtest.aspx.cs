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
        TableRow row1, row2, row3, row4, row5, row6,row7;
        TableCell cell1, cell2, cell3, cell4, cell5, cell6, cell7;
        Label lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8;
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {
                     
                string id = Request.QueryString[0];
                int i = int.Parse(id);
                loadXml(i);
                loadQuest();
                loadTable();
            LoadTestInfo();

        }
        protected void loadXml(int testid)
        {
            string sql = "select testxml from license_test where testid = "+testid+"";
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
                //string cor = node.Attributes["correct"].Value;
                //string chosen = node.Attributes[""]          
                for(int i = 0; i <4; i++)
                {
                    string a = node["answer"].ChildNodes[i].InnerText;
                    
                        
                    t.setAnswers(a);                   
                }
                XmlNodeList lista = node.SelectNodes("answer/answer");
                foreach(XmlNode n in lista)
                {
                    string a = n.Attributes["id"].Value;
                    string b = n.Attributes["correct"].Value;
                    t.setAnsId(Convert.ToInt16(a));
                    t.setTor(b);    
                }

                bool bb = node["useranswer"].HasChildNodes;
                if (bb == true)
                {
                   
                    XmlNodeList list = node.SelectNodes("useranswer/question");
                    XmlNode nd = node.SelectSingleNode("useranswer/question");
                    string attr = nd.Attributes["id"].Value;
                    int c = list.Count;
                        
                    for (int z = 0; z < c; z++ )
                    {
                        string b = node["useranswer"].ChildNodes[z].InnerText;
                       
                        t.setYouranwser(b);
                    }
                    XmlNodeList list2 = node.SelectNodes("useranswer/question");
                    foreach (XmlNode n in list2)
                    {
                        string attri = n.Attributes["qid"].Value;
                        int ids = Convert.ToInt16(attri);
                        t.setAid(ids);
                    }


                }

                t.setId(id);
                t.setQuestion(q);
                testlist.Add(t);
               
            }                        
        }
        protected void loadTable()
        {
            foreach(var x in testlist)
            {
                List<string> ans = x.getAnswers();
                List<string> yans = x.getYouranswers();
                List<int> answerid = x.getAnsId();
                List<int> usera = x.getAid();
                List<string> tor = x.getTor();
                row1 = new TableRow();
                row2 = new TableRow();
                row3 = new TableRow();
                row4 = new TableRow();
                row5 = new TableRow();
                row6 = new TableRow();
                row7 = new TableRow();

                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                cell4 = new TableCell();
                cell5 = new TableCell();
                cell6 = new TableCell();
                cell7 = new TableCell();

                lbl1 = new Label();
                lbl2 = new Label();
                lbl3 = new Label();
                lbl4 = new Label();
                lbl4 = new Label();
                lbl5 = new Label();
                lbl6 = new Label();
                lbl7 = new Label();
                lbl8 = new Label();

                lbl1.Text = x.getQuestion();

                lbl2.Text = ans[0].ToString();
                lbl2.Attributes.Add("id", answerid[0].ToString());
                lbl2.Attributes.Add("correct", tor[0].ToString());

                lbl3.Text = ans[1].ToString();
                lbl3.Attributes.Add("id", answerid[1].ToString());
                lbl2.Attributes.Add("correct", tor[1].ToString());

                lbl4.Text = ans[2].ToString();
                lbl4.Attributes.Add("id", answerid[2].ToString());
                lbl2.Attributes.Add("correct", tor[2].ToString());

                lbl5.Text = ans[3].ToString();
                lbl5.Attributes.Add("id", answerid[3].ToString());
                lbl2.Attributes.Add("correct", tor[3].ToString());

        
                cell1.Controls.Add(lbl1);
                List<TableCell> clist = new List<TableCell>();
                cell2.Controls.Add(lbl2);
                cell3.Controls.Add(lbl3);
                cell4.Controls.Add(lbl4);
                cell5.Controls.Add(lbl5);
                clist.Add(cell2);
                clist.Add(cell3);
                clist.Add(cell4);
                clist.Add(cell5);
                
                
                cell6.Controls.Add(lbl6);
                cell7.Controls.Add(lbl7);
                cell1.Attributes.Add("class", "questionCell");
                //cell7.Attributes.Add("class", "answerCell");

                row1.Controls.Add(cell1);
                row2.Controls.Add(cell2);
                row3.Controls.Add(cell3);
                row4.Controls.Add(cell4);
                row5.Controls.Add(cell5);
                row6.Controls.Add(cell6);
                row7.Controls.Add(cell7);
                table1.Controls.Add(row1);
                int xy = 0;
                for (int y = 0; y < answerid.Count; y++)
                {
                    
                    TableRow r = new TableRow();

                    int ai = answerid[y];
                    int count = usera.Count;
                    if(usera.Any() == true && xy < usera.Count)
                    {
                        string c = tor[y].ToString();
                            int uai = usera[xy];
                            if (ai == uai)
                            {
                                TableCell cell = new TableCell();
                                cell = clist[y];
                            if(c == "true")
                            {
                                r.Attributes.Add("class", "green");
                                r.Controls.Add(cell);
                                table1.Controls.Add(r);
                                xy++;
                            }
                            else if(c =="false")
                            {
                                r.Attributes.Add("class", "red");
                                r.Controls.Add(cell);
                                table1.Controls.Add(r);
                                xy++;
                            }
                               
                                
                                //xy++;
                            }
                            else
                            {
                                TableCell cell = new TableCell();
                                cell = clist[y];
                                r.Attributes.Add("class", "answers");
                                r.Controls.Add(cell);
                                table1.Controls.Add(r);
                            }
                        
      
                    }
                    else
                    {
                        TableCell cell = new TableCell();
                        cell = clist[y];
                        r.Attributes.Add("class", "answers");
                        r.Controls.Add(cell);
                        table1.Controls.Add(r);
                    }
                             
    
                }     
                row6.Attributes.Add("class", "answers");
                row7.Attributes.Add("class", "answers");
                table1.Controls.Add(row6);
                table1.Controls.Add(row7);




            }
}

        private void LoadTestInfo()
        {
            string id = Request.QueryString[0];

            string sql = @"select u.first_name, u.last_name, t.name, t.grade, t.points, t.date from license_test t
                            inner join  users u
                            on u.userid = t.user_id
                            where testid = @testid";

            conn.Open();
            
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@testid", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                DateTime date = Convert.ToDateTime(dr.GetValue(5));

                LabelFullName.Text = dr.GetValue(0).ToString() + " " + dr.GetValue(1).ToString();
                LabelTestName.Text = dr.GetValue(2).ToString();
                LabelTestGrade.Text = dr.GetValue(3).ToString();
                LabelTestPoints.Text = dr.GetValue(4).ToString();
                LabelTestDate.Text = date.ToShortDateString();
            }
            conn.Close();

        }
    }
}