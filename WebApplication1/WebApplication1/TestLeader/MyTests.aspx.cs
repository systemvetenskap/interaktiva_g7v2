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
    public partial class TestResults : System.Web.UI.Page
    {
        XmlDocument right = new XmlDocument();
        XmlDocument wrong = new XmlDocument();
        XmlDocument xmldoc = new XmlDocument();
        XmlDocument xmldoc2 = new XmlDocument();
        Table table;
        TableRow row1, row2, row3, row4, row5, row6;
        TableCell cell1, cell2, cell3, cell4, cell5, cell6, imgcell;
        RadioButton radiob1, radiob2, radiob3, radiob4;
        CheckBox checkbox1, checkbox2, checkbox3, checkbox4;
        public int timerVar = 1;
        public string tpoints;
        public int gr;
        public string pPoints;
        public string ecPoints;
        public string ethPoints;
        int prod = 0;
        int eco = 0;
        int eth = 0;
        int integer = 0;
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                timerVar = 1;
                tpoints = "na";
                gr = 0;
                right.LoadXml("<test></test>");
                wrong.LoadXml("<test></test>");
                //Laddar in vårat xmldokument i xmldoc
                xmldoc.Load(Server.MapPath("/XmlLicenseTest.xml"));

                //Laddar endast in taggar i xmldoc2 som är identiska med XmlQuestions.xml
                xmldoc2.LoadXml("<categories></categories>");

                //Skapar ny array och stoppar in 25 st variabler av typen int
                int[] arrayQuestions = RandomNumbers(1, 25, 4);

                //Hämtar frågor från orginaldokumentet och stoppar in detta i det nya
                foreach (int i in arrayQuestions)
                {
                    XmlNode newnode = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/question[@id='" + i + "']"), true);
                    XmlNode parent = xmldoc2.SelectSingleNode("categories");
                    parent.AppendChild(newnode);
                    xmldoc2.Save(Server.MapPath("usertest.xml"));
                }
                //Lägger in alla question nodes i en XML lista
                XmlNodeList lst = xmldoc2.SelectNodes("categories/question");
                //Loopar igenom xml listan 1st
                int count = 1;
                foreach (XmlNode node in lst)
                {
                    string attributeID = node.Attributes["id"].Value;
                    string attributeMulti = node.Attributes["multi"].Value;
                    string img = node.Attributes["image"].Value;
                    int i = Convert.ToInt16(attributeID);
                    loadQuest(attributeID, attributeMulti, img, count);
                    count++;

                }

            }
            else if (IsPostBack)
            {

                timerVar = 2;
                btn1.Visible = false;
                xmldoc2.Load(Server.MapPath("usertest.xml"));
                XmlNodeList lst = xmldoc2.SelectNodes("categories/question");
                //Loopar igenom xml listan 1st
                int count = 1;
                foreach (XmlNode node in lst)
                {
                    string attributeID = node.Attributes["id"].Value;
                    string attributeMulti = node.Attributes["multi"].Value;
                    string img = node.Attributes["image"].Value;
                    int i = Convert.ToInt16(attributeID);
                    loadQuest(attributeID, attributeMulti, img, count);
                    count++;

                }


                if (ViewState["points"] != null)
                {
                    string s = (string)ViewState["points"];
                    tpoints = s;

                }
                if (ViewState["grade"] != null)
                {
                    int a = (int)ViewState["grade"];
                    gr = a;

                }



                //string s = (string)ViewState["grade"];
                //grade = s;

                //
                //

            }


        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState.Add("points", tpoints);
            ViewState.Add("grade", gr);

        }
        protected void btnSubmint_Click(object sender, EventArgs e)
        {

        }
        public static int[] RandomNumbers(int min, int max, int derangement)
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
        protected void btn1_Click(object sender, EventArgs e)
        {

            calcPoints();
            feedbackAnswers();
        }
        protected void loadQuest(string i, string attributeMulti, string img, int count)
        {
            table = new Table();
            //Skapar nya rader                  
            row1 = new TableRow();
            row2 = new TableRow();
            row3 = new TableRow();
            row4 = new TableRow();
            row5 = new TableRow();
            row6 = new TableRow();
            //Skapar nya celler i raderna ovan
            cell1 = new TableCell();
            cell2 = new TableCell();
            cell3 = new TableCell();
            cell4 = new TableCell();
            cell5 = new TableCell();
            cell6 = new TableCell();
            imgcell = new TableCell();
            //Skapar nya radiobuttons
            radiob1 = new RadioButton();
            radiob2 = new RadioButton();
            radiob3 = new RadioButton();
            radiob4 = new RadioButton();
            //Skapar nya checkboxes
            checkbox1 = new CheckBox();
            checkbox2 = new CheckBox();
            checkbox3 = new CheckBox();
            checkbox4 = new CheckBox();
            //Ger ett gruppnamn till radiobuttons 
            radiob1.GroupName = "gr" + i.ToString();
            radiob2.GroupName = "gr" + i.ToString();
            radiob3.GroupName = "gr" + i.ToString();
            radiob4.GroupName = "gr" + i.ToString();
            //Sätter ett unikt namn till varje radiobutton



            //Skapar ny label för varje fråga
            Label lblQuestion = new Label();

            //Om frågan bara har ett svarsalternativ som är rätt
            if (attributeMulti == "false")
            {

                radiob1.ID = i.ToString() + "r1";
                radiob2.ID = i.ToString() + "r2";
                radiob3.ID = i.ToString() + "r3";
                radiob4.ID = i.ToString() + "r4";
                lblQuestion.Text = count + ": " + (xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']").FirstChild.InnerText);
                XmlNode n1 = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                string cat1 = n1.Attributes["multi"].Value;
                lblQuestion.Attributes.Add("multi", cat1);
                string cat2 = n1.Attributes["id"].Value;
                lblQuestion.Attributes.Add("id", cat2);

                radiob1.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                XmlNode n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='1']");
                string at = n.Attributes["correct"].Value;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                string cat = n.Attributes["category"].Value;
                lblQuestion.Text += " (" + cat + ")";
                radiob1.Attributes.Add("correct", at);
                radiob1.Attributes.Add("category", cat);

                radiob2.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='2']");
                at = n.Attributes["correct"].Value;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                cat = n.Attributes["category"].Value;
                radiob2.Attributes.Add("category", cat);
                radiob2.Attributes.Add("correct", at);

                radiob3.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='3']");
                at = n.Attributes["correct"].Value;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                cat = n.Attributes["category"].Value;
                radiob3.Attributes.Add("category", cat);
                radiob3.Attributes.Add("correct", at);

                radiob4.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='4']");
                at = n.Attributes["correct"].Value;
                n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                cat = n.Attributes["category"].Value;
                radiob4.Attributes.Add("category", cat);
                radiob4.Attributes.Add("correct", at);
                //Lägger in radiobuttons i cellerna
                cell1.Controls.Add(lblQuestion);
                cell1.ColumnSpan = 2;
                cell2.Controls.Add(radiob1);
                cell3.Controls.Add(radiob2);
                cell4.Controls.Add(radiob3);
                cell5.Controls.Add(radiob4);
            }
            //Om det är en fråga med många svarsalternativ
            if (attributeMulti == "true")
            {
                checkbox1.ID = i.ToString() + "c1";
                checkbox2.ID = i.ToString() + "c2";
                checkbox3.ID = i.ToString() + "c3";
                checkbox4.ID = i.ToString() + "c4";
                checkbox1.Attributes.Add("name", "check" + count);
                checkbox1.Attributes.Add("value", "1");
                checkbox2.Attributes.Add("name", "check" + count);
                checkbox2.Attributes.Add("value", "2");
                checkbox3.Attributes.Add("name", "check" + count);
                checkbox3.Attributes.Add("value", "3");
                checkbox4.Attributes.Add("name", "check" + count);
                checkbox4.Attributes.Add("value", "4");

                lblQuestion.Text = count + ": " + (xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']").FirstChild.InnerText);
                XmlNode n1 = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                string cat1 = n1.Attributes["multi"].Value;
                lblQuestion.Attributes.Add("multi", cat1);
                string cat2 = n1.Attributes["id"].Value;
                lblQuestion.Attributes.Add("id", cat2);

                checkbox1.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '1']").InnerText;
                XmlNode usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='1']");
                string at = usernode.Attributes["correct"].Value;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                string cat = usernode.Attributes["category"].Value;
                lblQuestion.Text += " (" + cat + ")";
                checkbox1.Attributes.Add("category", cat);
                checkbox1.Attributes.Add("correct", at);

                checkbox2.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '2']").InnerText;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='2']");
                at = usernode.Attributes["correct"].Value;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                cat = usernode.Attributes["category"].Value;

                checkbox2.Attributes.Add("category", cat);
                checkbox2.Attributes.Add("correct", at);


                checkbox3.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '3']").InnerText;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='3']");
                at = usernode.Attributes["correct"].Value;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                cat = usernode.Attributes["category"].Value;

                checkbox3.Attributes.Add("category", cat);
                checkbox3.Attributes.Add("correct", at);

                checkbox4.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '4']").InnerText;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='4']");
                at = usernode.Attributes["correct"].Value;
                usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                cat = usernode.Attributes["category"].Value;

                checkbox4.Attributes.Add("category", cat);
                checkbox4.Attributes.Add("correct", at);
                //Lägger in checkboxar i cellerna
                cell1.Controls.Add(lblQuestion);
                cell1.ColumnSpan = 2;

                cell2.Controls.Add(checkbox1);
                cell3.Controls.Add(checkbox2);
                cell4.Controls.Add(checkbox3);
                cell5.Controls.Add(checkbox4);
            }
            //Lägger in label i cellen
            cell1.Attributes.Add("class", "questionCell");


            //Lägger in cellen på raden
            row1.Controls.Add(cell1);
            row2.Controls.Add(cell2);
            row3.Controls.Add(cell3);
            row4.Controls.Add(cell4);
            row5.Controls.Add(cell5);
            row6.Controls.Add(cell6);
            //Lägger till attribut till de olika radobjekten
            row1.Attributes.Add("class", "question");

            table1.Controls.Add(row1);
            //row2.Attributes.Add("class", "answers answer1");
            row2.Attributes.Add("class", "answers");
            table1.Controls.Add(row2);
            row3.Attributes.Add("class", "answers");
            table1.Controls.Add(row3);
            row4.Attributes.Add("class", "answers");
            table1.Controls.Add(row4);
            row5.Attributes.Add("class", "answers");
            table1.Controls.Add(row5);
            row6.Attributes.Add("class", "empty");
            table1.Controls.Add(row6);
            count++;


            //Om attributet image är satt till true
            if (img == "true")
            {
                Image bild = new Image();
                string imagelink = xmldoc2.SelectSingleNode("categories/question[@id='" + i + "']/image").InnerText;
                bild.ImageUrl = imagelink;
                imgcell.RowSpan = 4;
                row2.Controls.Add(imgcell);
                imgcell.Controls.Add(bild);


            }



        }
        protected void next_quest(object sender, EventArgs e)
        {


        }
        protected void feedbackAnswers()
        {
            foreach (TableRow rw in table1.Rows)
            {
                foreach (TableCell cell in rw.Cells)
                {
                    foreach (Control cl in cell.Controls)
                    {
                        if (cl is RadioButton)
                        {
                            RadioButton rad = (RadioButton)cl;
                            string cor = rad.Attributes["correct"];

                            if (rad.Checked == true)
                            {

                                if (cor == "true")
                                {
                                    rw.Attributes.Remove("class");
                                    rw.Attributes.Add("class", "green");


                                }
                                else if (cor == "false")
                                {
                                    rw.Attributes.Remove("class");
                                    rw.Attributes.Add("class", "red");

                                }
                            }
                            else if (rad.Checked == false)
                            {
                                if (cor == "true")
                                {
                                    rw.Attributes.Remove("class");
                                    rw.Attributes.Add("class", "green");

                                }

                            }

                        }
                        else if (cl is CheckBox)
                        {
                            CheckBox chk = (CheckBox)cl;
                            string cor = chk.Attributes["correct"];

                            if (chk.Checked == true)
                            {
                                if (cor == "true")
                                {
                                    rw.Attributes.Remove("class");
                                    rw.Attributes.Add("class", "green");
                                }
                                else if (cor == "false")
                                {
                                    rw.Attributes.Remove("class");
                                    rw.Attributes.Add("class", "red");
                                }
                            }
                            else if (chk.Checked == false)
                            {
                                if (cor == "true")
                                {
                                    rw.Attributes.Remove("class");
                                    rw.Attributes.Add("class", "green");
                                }
                            }

                        }


                    }

                }

            }




        }
        protected void calcPoints()
        {
            int checkcount = 0;
            int acount = 0;
            string multi = "";
            foreach (TableRow rw in table1.Rows)
            {
                foreach (TableCell cell in rw.Cells)
                {
                    foreach (Control cl in cell.Controls)
                    {
                        if (cl is Label)
                        {
                            Label lab = (Label)cl;
                            multi = lab.Attributes["multi"];

                            if (multi == "true")
                            {
                                int id = int.Parse(lab.Attributes["id"]);
                                checkcount = countCorrect(id);
                            }


                        }


                        if (cl is RadioButton)
                        {
                            RadioButton rad = (RadioButton)cl;
                            rad.Enabled = false;
                            string cor = rad.Attributes["correct"];
                            string cat = rad.Attributes["category"];
                            if (rad.Checked == true)
                            {
                                if (cor == "true")
                                {
                                    if (cat == "products")
                                    {
                                        prod++;
                                        //string s = rad.ID;
                                        //string x = rad.Text;
                                        //XmlNode nd = right.SelectSingleNode("/test");
                                        //XmlElement el = right.CreateElement("test");
                                        //el.InnerText = prod.ToString();
                                        //nd.AppendChild(el);

                                        //right.Save("C:\\Users\\Henrik\\Desktop\\count.xml");
                                    }
                                    else if (cat == "ethics")
                                    {
                                        eth++;
                                    }
                                    else if (cat == "economy")
                                    {
                                        eco++;

                                    }

                                }
                                else if (cor == "false")
                                {
                                    //string s = rad.ID;
                                    //string x = rad.Text;
                                    //XmlNode nd = wrong.SelectSingleNode("/test");
                                    //XmlElement el = wrong.CreateElement("test");
                                    //el.InnerText = x;
                                    //nd.AppendChild(el);

                                    //wrong.Save("C:\\Users\\Henrik\\Desktop\\wronganswer.xml");
                                }

                            }

                        }
                        else if (cl is CheckBox)
                        {
                            CheckBox chk = (CheckBox)cl;

                            string cor = chk.Attributes["correct"];
                            string cat = chk.Attributes["category"];
                            string chkID = chk.Attributes["value"];
                            chk.Enabled = false;
                            if (chk.Checked == true)
                            {
                                if (cor == "true")
                                {
                                    acount++;
                                    if (cat == "products" && acount == checkcount)
                                    {

                                        prod++;

                                    }
                                    else if (cat == "ethics" && acount == checkcount)
                                    {
                                        eth++;
                                    }
                                    else if (cat == "economy" && acount == checkcount)
                                    {
                                        eco++;

                                    }

                                }
                                else if (cor == "false")
                                {
                                    //string s = chk.ID;
                                    //string x = chk.Text;
                                    //XmlNode nd = wrong.SelectSingleNode("/test");
                                    //XmlElement el = wrong.CreateElement(s);
                                    //el.InnerText = x;
                                    //nd.AppendChild(el);

                                    //wrong.Save("C:\\Users\\Henrik\\Desktop\\correctanswer.xml");
                                }

                            }


                        }

                    }
                }
            }

            double prodcat = 8;
            double ecocat = 8;
            double ethcat = 9;
            double total = prod;
            total += eco;
            total += eth;
            tpoints = total.ToString();
            string gradestring = "";
            if (total / 25 >= 0.70)
            {
                gr = 1;
                gradestring = "Godkänd";
            }
            else
            {
                gr = 2;
                gradestring = "Icke Godkänd";
            }

            string savexml = xmldoc2.OuterXml;
            string tn = "Licenseringstest";
            int ln = 1;
            DateTime date = DateTime.Today;
            string sql = "insert into license_test(name, user_id, grade, points, date, testxml) values(:tname, :user, :grd, :pts, :dt, :addxml)";
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add(new NpgsqlParameter("tname", tn));
                cmd.Parameters.Add(new NpgsqlParameter("user", ln));
                cmd.Parameters.Add(new NpgsqlParameter("grd", gradestring));
                cmd.Parameters.Add(new NpgsqlParameter("pts", total));
                cmd.Parameters.Add(new NpgsqlParameter("dt", date));
                cmd.Parameters.Add(new NpgsqlParameter("addxml", savexml));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {

            }








            if (prod / prodcat >= 0.60)
            {
                //XmlNode nd = wrong.SelectSingleNode("/test");
                //XmlElement el = wrong.CreateElement("test");
                //el.InnerText = "du är godkänd i kategori products!";
                //nd.AppendChild(el);

                //wrong.Save("C:\\Users\\Henrik\\Desktop\\betygr.xml");
            }
            else if (prod / prodcat < 0.60)
            {
                //XmlNode nd = wrong.SelectSingleNode("/test");
                //XmlElement el = wrong.CreateElement("test");
                //el.InnerText = "du är inte godkänd i kategori products!";
                //nd.AppendChild(el);

                //wrong.Save("C:\\Users\\Henrik\\Desktop\\betygr.xml");
            }
            if (eco / ecocat >= 0.60)
            {
                //XmlNode nd = wrong.SelectSingleNode("/test");
                //XmlElement el = wrong.CreateElement("test");
                //el.InnerText = "du är godkänd i kategori economy!";
                //nd.AppendChild(el);

                //wrong.Save("C:\\Users\\Henrik\\Desktop\\betygr.xml");
            }
            else if (eco / ecocat < 0.60)
            {
                //XmlNode nd = wrong.SelectSingleNode("/test");
                //XmlElement el = wrong.CreateElement("test");
                //el.InnerText = "du är inte godkänd i kategori economy!";
                //nd.AppendChild(el);

                //wrong.Save("C:\\Users\\Henrik\\Desktop\\betygr.xml");
            }
            if (eth / ethcat >= 0.60)
            {
                //XmlNode nd = wrong.SelectSingleNode("/test");
                //XmlElement el = wrong.CreateElement("test");
                //el.InnerText = "du är godkänd i kategori ethics!";
                //nd.AppendChild(el);

                //wrong.Save("C:\\Users\\Henrik\\Desktop\\betygr.xml");
            }
            else if (eth / ethcat < 0.60)
            {
                //XmlNode nd = wrong.SelectSingleNode("/test");
                //XmlElement el = wrong.CreateElement("test");
                //el.InnerText = "du är inte godkänd i kategori ethics!";
                //nd.AppendChild(el);

                //wrong.Save("C:\\Users\\Henrik\\Desktop\\betygr.xml");
            }



        }
        protected int countCorrect(int i)
        {
            int count = 0;
            xmldoc2.Load(Server.MapPath("usertest.xml"));



            for (int z = 1; z < 5; z++)
            {
                XmlNode node = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + z + "']");
                string attributeID = node.Attributes["correct"].Value;
                if (attributeID == "true")
                {
                    count++;
                }
            }



            return count;




        }


    }

}
