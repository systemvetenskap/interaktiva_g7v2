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

namespace WebApplication1

{
    public partial class quest : System.Web.UI.Page
    {
        //NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        
        {
            //Skapar två nya xmldokument
            XmlDocument xmldoc = new XmlDocument();
            XmlDocument xmldoc2 = new XmlDocument();

            //Laddar in vårat xmldokument i xmldoc
            xmldoc.Load(Server.MapPath("XmlQuestions.xml"));

            //Laddar endast in taggar i xmldoc2 som är identiska med XmlQuestions.xml
            xmldoc2.LoadXml("<categories><products></products><economy></economy><ethics></ethics></categories>");

            //Skapar ny array och stoppar in 25 st variabler av typen int
            int[] m = RandomNumbers(1, 25, 4);

            //Hämtar frågor och stoppar in det i arrayen m från varje kategori av frågor
            foreach (int i in m)
            {                           
                if (i <= 8)
                {   //Om variablen i är mindre än 8 så hämtas frågorna i kategorin products     
                    XmlNode newnode = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/products/question[@id='" + i + "']"), true);
                    XmlNode parent = xmldoc2.SelectSingleNode("categories/products");
                    parent.AppendChild(newnode);
                    xmldoc2.Save(Server.MapPath("usertest.xml"));

                }

                else if (i > 8 && i <= 16)
                {   //Om variablen i är större än 8 och mindre än 16 så hämtas frågorna i kategorin ecomony                 
                    XmlNode newnode = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/economy/question[@id='"+i+"']"), true);
                    XmlNode parent = xmldoc2.SelectSingleNode("categories/economy");
                    parent.AppendChild(newnode);
                    xmldoc2.Save(Server.MapPath("usertest.xml"));
                }

                else if (i > 16)
                {   //Om variablen i är större än 16 så hämtas frågorna i kategorin ethics                
                    XmlNode newnode = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/ethics/question[@id='" + i + "']"), true);
                    XmlNode parent = xmldoc2.SelectSingleNode("categories/ethics");
                    parent.AppendChild(newnode);
                    xmldoc2.Save(Server.MapPath("usertest.xml"));
                }
            }
            XmlNodeList lst  = xmldoc2.SelectNodes("categories/products/question");
       
            foreach (XmlNode nd in lst)
            {      
                string s = nd.Attributes["id"].Value;
                int i = Convert.ToInt16(s);
                                  
                TableRow rw = new TableRow();
                TableRow rw2 = new TableRow();
                TableRow rw3 = new TableRow();
                TableRow rw4 = new TableRow();
                TableRow rw5 = new TableRow();
                TableRow rw6 = new TableRow();

                //Skapar nya celler i raderna ovan
                TableCell cl = new TableCell();
                TableCell cl2 = new TableCell();
                TableCell cl3 = new TableCell();
                TableCell cl4 = new TableCell();
                TableCell cl5 = new TableCell();
                TableCell cl6 = new TableCell();

                //Skapar nya radiobuttons
                RadioButton rai = new RadioButton();
                RadioButton rai2 = new RadioButton();
                RadioButton rai3 = new RadioButton();
                RadioButton rai4 = new RadioButton();

                //Ger ett gruppnamn till radiobuttons 
                rai.GroupName = "gr" + i.ToString();
                rai2.GroupName = "gr" + i.ToString();
                rai3.GroupName = "gr" + i.ToString();
                rai4.GroupName = "gr" + i.ToString();

                //Sätter ett unikt namn till varje radiobutton
                rai.ID = "raid" + i;

                //Skapar ny label för varje fråga
                Label lbl = new Label();

                if (i <= 8)
                {
                    lbl.Text = xmldoc2.SelectSingleNode("/categories/products/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc2.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc2.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc2.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc2.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;


                }
                else if (i > 8 && i <= 16)
                {
                    lbl.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;

                }
                else if (i > 16)
                {
                    lbl.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;

                }



                cl.Controls.Add(lbl);
                cl2.Controls.Add(rai);
                cl3.Controls.Add(rai2);
                cl4.Controls.Add(rai3);
                cl5.Controls.Add(rai4);

                rw.Controls.Add(cl);
                rw2.Controls.Add(cl2);
                rw3.Controls.Add(cl3);
                rw4.Controls.Add(cl4);
                rw5.Controls.Add(cl5);
                rw6.Controls.Add(cl6);

                table1.Controls.Add(rw);
                table1.Controls.Add(rw2);
                table1.Controls.Add(rw3);
                table1.Controls.Add(rw4);
                table1.Controls.Add(rw5);
                table1.Controls.Add(rw6);

                }
            XmlNodeList lst2 = xmldoc2.SelectNodes("categories/economy/question");
            foreach(XmlNode nd in lst2)
            {
                string s = nd.Attributes["id"].Value;
                int i = Convert.ToInt16(s);
                TableRow rw = new TableRow();
                TableRow rw2 = new TableRow();
                TableRow rw3 = new TableRow();
                TableRow rw4 = new TableRow();
                TableRow rw5 = new TableRow();
                TableRow rw6 = new TableRow();

                TableCell cl = new TableCell();
                TableCell cl2 = new TableCell();
                TableCell cl3 = new TableCell();
                TableCell cl4 = new TableCell();
                TableCell cl5 = new TableCell();
                TableCell cl6 = new TableCell();

                RadioButton rai = new RadioButton();
                RadioButton rai2 = new RadioButton();
                RadioButton rai3 = new RadioButton();
                RadioButton rai4 = new RadioButton();
                rai.GroupName = "gr" + i.ToString();
                rai2.GroupName = "gr" + i.ToString();
                rai3.GroupName = "gr" + i.ToString();
                rai4.GroupName = "gr" + i.ToString();
                rai.ID = "raid" + i;
                Label lbl = new Label();
                if (i > 8 && i <= 16)
                {
                    lbl.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc2.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;

                }
                cl.Controls.Add(lbl);
                //Lägger till radiobuttons i cellen
                cl2.Controls.Add(rai);
                cl3.Controls.Add(rai2);
                cl4.Controls.Add(rai3);
                cl5.Controls.Add(rai4);

                //Lägger till cellerna i varje rad
                rw.Controls.Add(cl);
                rw2.Controls.Add(cl2);
                rw3.Controls.Add(cl3);
                rw4.Controls.Add(cl4);
                rw5.Controls.Add(cl5);
                rw6.Controls.Add(cl6);

                //Lägger till raderna i table1 
                table1.Controls.Add(rw);
                table1.Controls.Add(rw2);
                table1.Controls.Add(rw3);
                table1.Controls.Add(rw4);
                table1.Controls.Add(rw5);
                table1.Controls.Add(rw6);


            }
            XmlNodeList lst3 = xmldoc2.SelectNodes("categories/ethics/question");
            foreach(XmlNode nd in lst3)
            {
                string s = nd.Attributes["id"].Value;
                int i = Convert.ToInt16(s);
                TableRow rw = new TableRow();
                TableRow rw2 = new TableRow();
                TableRow rw3 = new TableRow();
                TableRow rw4 = new TableRow();
                TableRow rw5 = new TableRow();
                TableRow rw6 = new TableRow();

                TableCell cl = new TableCell();
                TableCell cl2 = new TableCell();
                TableCell cl3 = new TableCell();
                TableCell cl4 = new TableCell();
                TableCell cl5 = new TableCell();
                TableCell cl6 = new TableCell();

                RadioButton rai = new RadioButton();
                RadioButton rai2 = new RadioButton();
                RadioButton rai3 = new RadioButton();
                RadioButton rai4 = new RadioButton();
                rai.GroupName = "gr" + i.ToString();
                rai2.GroupName = "gr" + i.ToString();
                rai3.GroupName = "gr" + i.ToString();
                rai4.GroupName = "gr" + i.ToString();
                rai.ID = "raid" + i;
                Label lbl = new Label();
                if (i > 16)
                {
                    lbl.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc2.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;

                }
                cl.Controls.Add(lbl);
                cl2.Controls.Add(rai);
                cl3.Controls.Add(rai2);
                cl4.Controls.Add(rai3);
                cl5.Controls.Add(rai4);

                rw.Controls.Add(cl);
                rw2.Controls.Add(cl2);
                rw3.Controls.Add(cl3);
                rw4.Controls.Add(cl4);
                rw5.Controls.Add(cl5);
                rw6.Controls.Add(cl6);

                table1.Controls.Add(rw);
                table1.Controls.Add(rw2);
                table1.Controls.Add(rw3);
                table1.Controls.Add(rw4);
                table1.Controls.Add(rw5);
                table1.Controls.Add(rw6);
            }


        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Write("Test");
        }

        public static int[] RandomNumbers(int min, int max, int derangement)//Metod för att slumpa fram frågorna
        {
            if (min > max)
            {
                throw new Exception("Första parametern måste vara mindre eller lika med andra.");
            }
            Random random = new Random();
            int count = max - min; ;
            int[] tempList = new int[count + 1];
            int counter = 0;
            for (int i = min; i <= max; i++)
            {
                tempList[counter] = i;
                counter++;
            }
            for (int i = 0; i < derangement; i++)
                for (int j = 0; j < count; j++)
                {
                    int k = random.Next(0, count + 1);
                    int l = random.Next(0, count + 1);
                    if (k != l)
                    {
                        tempList[k] += tempList[l];
                        tempList[l] = tempList[k] - tempList[l];
                        tempList[k] = tempList[k] - tempList[l];
                    }
                }
            return tempList;
        }
 

        protected void Button1_Click1(object sender, EventArgs e)
        {
       
            
            

        }
    }
}
