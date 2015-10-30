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

namespace WebApplication1

{
    public partial class quest : System.Web.UI.Page
    {
        //NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        
        {


            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(Server.MapPath("XmlQuestions.xml"));



            XmlNodeList nodeListAnswer = xmldoc.SelectNodes("/categories/ethicandrules/question[@id='2']/answer/answer");
            XmlNodeList nodeListCorrectAnswer = xmldoc.SelectNodes("/categories/ethicandrules/question[@id='1']/correctanswer");
           //for(int i = 1; i <= 25; i++)
           // {
  
                // Skriv kod här ifall frågorna inte skall slumpas(Exakt samma som i foreach)







           // }
            int[] m = RandomNumbers(1, 25, 4);
            foreach (int i in m)
            {
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
                if (i <= 8)
                {
                    lbl.Text = xmldoc.SelectSingleNode("/categories/products/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc.SelectSingleNode("/categories/products/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;


                }

                else if (i > 8 && i <= 16)
                {
                    lbl.Text = xmldoc.SelectSingleNode("/categories/economy/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc.SelectSingleNode("/categories/economy/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;
                }

                else if (i > 16)
                {
                    lbl.Text = xmldoc.SelectSingleNode("/categories/ethics/question[@id='" + i + "']").FirstChild.InnerText;
                    rai.Text = xmldoc.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                    rai2.Text = xmldoc.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                    rai3.Text = xmldoc.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                    rai4.Text = xmldoc.SelectSingleNode("/categories/ethics/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;
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

            //lblQuestion.Text = xmldoc.SelectSingleNode("/categories/ethicandrules/question[@id='1']").FirstChild.InnerText;

            //RadioButton1.Text = nodeListAnswer[0].FirstChild.InnerText;
            //RadioButton2.Text = nodeListAnswer[1].FirstChild.InnerText;
            //RadioButton3.Text = nodeListAnswer[2].InnerText;
            //RadioButton4.Text = nodeListAnswer[3].FirstChild.InnerText;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Write("Test");
        }

        #region radionumbers 
        public void getNumbers()
        {
            int[] m = RandomNumbers(1, 25, 5);
            string[] questions = new string[25]; // Initialize.

            for (int i = 0; i < m.Length; i++)
            {
                questions[i] = m[i].ToString();
            }
        }

        public static int[] RandomNumbers(int min, int max)
        {
            return RandomNumbers(min, max, 2);
        }
        public static int[] RandomNumbers(int min, int max, int derangement)
        {
            if (min > max)
            {
                throw new Exception("The first parameter must be less (or equal) than the second.");
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
                        //Swap TempList[k] with TempList[l]
                        tempList[k] += tempList[l];
                        tempList[l] = tempList[k] - tempList[l];
                        tempList[k] = tempList[k] - tempList[l];
                    }
                }
            return tempList;
        }
        #endregion

        protected void Button1_Click1(object sender, EventArgs e)
        {
       
            
            

        }
    }
}
