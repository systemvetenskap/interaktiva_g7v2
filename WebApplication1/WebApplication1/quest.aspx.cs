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
    public partial class quest : System.Web.UI.Page
    {
        //NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Timer

            

            //Skapar två nya xmldokument
            XmlDocument xmldoc = new XmlDocument();
            XmlDocument xmldoc2 = new XmlDocument();

            //Laddar in vårat xmldokument i xmldoc
            xmldoc.Load(Server.MapPath("XmlQuestions.xml"));

            //Laddar endast in taggar i xmldoc2 som är identiska med XmlQuestions.xml
            xmldoc2.LoadXml("<categories></categories>");

            //Skapar ny array och stoppar in 25 st variabler av typen int
            int[] m = RandomNumbers(1, 25, 4);

            //Hämtar frågor från orginaldokumentet och stoppar in detta i det nya
            foreach (int i in m)
            {
                    XmlNode newnode = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/question[@id='" + i + "']"), true);
                    XmlNode parent = xmldoc2.SelectSingleNode("categories");
                    parent.AppendChild(newnode);
                    xmldoc2.Save(Server.MapPath("usertest.xml"));
            }
            //Lägger in alla question nodes i en XML lista
            XmlNodeList lst  = xmldoc2.SelectNodes("categories/question");
            //Loopar igenom xml listan 1st
            foreach (XmlNode nd in lst)
            {      
                    string s = nd.Attributes["id"].Value;
                    int i = Convert.ToInt16(s);
                    //Skapar nya rader                  
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


                
                lbl.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']").FirstChild.InnerText;
                rai.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                rai2.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                rai3.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                rai4.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;
                //Lägger in label i cellen
                cl.Attributes.Add("class", "questionCell");
                cl.Controls.Add(lbl);
                    //Lägger in radiobutton i cellerna
                cl2.Controls.Add(rai);
                cl3.Controls.Add(rai2);
                cl4.Controls.Add(rai3);
                cl5.Controls.Add(rai4);
                    //Lägger in cellen på raden
                rw.Controls.Add(cl);
                rw2.Controls.Add(cl2);
                rw3.Controls.Add(cl3);
                rw4.Controls.Add(cl4);
                rw5.Controls.Add(cl5);
                rw6.Controls.Add(cl6);
                //Lägger in raderna i tabellen som heter table1

                rw.Attributes.Add("class", "question");
                table1.Controls.Add(rw);
                rw2.Attributes.Add("class", "answers answer1");
                table1.Controls.Add(rw2);
                rw3.Attributes.Add("class", "answers answer2");
                table1.Controls.Add(rw3);
                rw4.Attributes.Add("class", "answers answer3");
                table1.Controls.Add(rw4);
                rw5.Attributes.Add("class", "answers answer4");
                table1.Controls.Add(rw5);
                rw6.Attributes.Add("class", "empty");
                table1.Controls.Add(rw6);
            }
        }
        protected void btnSubmint_Click(object sender, EventArgs e)
        {

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

    }
}
