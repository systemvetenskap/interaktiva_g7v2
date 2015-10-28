using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebApplication1
{
    public partial class quest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(Server.MapPath("XmlQuestions.xml"));



            XmlNodeList nodeListAnswer = xmldoc.SelectNodes("/categories/ethicandrules/question[@id='2']/answer/answer");
            XmlNodeList nodeListCorrectAnswer = xmldoc.SelectNodes("/categories/ethicandrules/question[@id='1']/correctanswer");

            //lblQuestion.Text = xmldoc.SelectSingleNode("/categories/ethicandrules/question[@id='1']").FirstChild.InnerText;

            //RadioButton1.Text = nodeListAnswer[0].FirstChild.InnerText;
            //RadioButton2.Text = nodeListAnswer[1].FirstChild.InnerText;
            //RadioButton3.Text = nodeListAnswer[2].InnerText;
            //RadioButton4.Text = nodeListAnswer[3].FirstChild.InnerText;
            //RadioButton5.Text = nodeListCorrectAnswer[0].FirstChild.InnerText;
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
    }
}
