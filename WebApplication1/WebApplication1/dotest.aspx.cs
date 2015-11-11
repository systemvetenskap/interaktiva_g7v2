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

namespace WebApplication1.Employee
{
    public partial class mytestsite : System.Web.UI.Page
    {
        XmlDocument right = new XmlDocument();
        XmlDocument wrong = new XmlDocument();
        XmlDocument xmldoc = new XmlDocument();
        XmlDocument xmldoc2 = new XmlDocument();
        TableRow row1, row2, row3, row4, row5, row6;
        TableCell cell1, cell2, cell3, cell4, cell5, cell6, imgcell;
        RadioButton radiob1, radiob2, radiob3, radiob4;
        CheckBox checkbox1, checkbox2, checkbox3, checkbox4;
        public int timerVar = 1;
        public string tpoints, p, ec, et;
        public int gr;
        public string ecPoints, pPoints, ethPoints;
        double prod = 0;
        double eco = 0;
        double eth = 0;
        public int testType = 0;
        string type = "";
        public int userid = 0;
        List<answ> list = new List<answ>();
        List<questions> listq = new List<questions>();
        List<counter> checklist = new List<counter>();

        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                user();
                bool pb = true;
                if (Application["type"] != null)
                {
                    type = Application["type"].ToString();
                }
                else
                {
                    type = "a";
                }

                btn2.Visible = false;
                type = "a";
                int x = 0;
                timerVar = 1;
                tpoints = "na";
                ecPoints = "na";
                ethPoints = "na";
                pPoints = "na";

                gr = 0;
                right.LoadXml("<test></test>");
                wrong.LoadXml("<test></test>");
                //Laddar in vårat xmldokument i xmldoc
                if (type == "a")
                {
                    xmldoc.Load(Server.MapPath("/XmlLicenseTest.xml"));
                    x = 25;
                    testType = 1;
                }
                else
                {
                    xmldoc.Load(Server.MapPath("/XmlUpdateTest.xml"));
                    x = 15;
                    testType = 0;
                }

                //Laddar endast in taggar i xmldoc2 som är identiska med XmlQuestions.xml
                xmldoc2.LoadXml("<categories></categories>");

                //Skapar ny array och stoppar in variabler av typen int(minsta värde, högsta värde)
                int[] arrayQuestions = RandomNumbers(1, x, 4);

                //Hämtar frågor från orginaldokumentet och stoppar in detta i det nya
                foreach (int i in arrayQuestions)
                {
                    XmlNode newnode = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/question[@id='" + i + "']"), false);
                    string text = xmldoc.SelectSingleNode("/categories/question[@id='" + i + "']").FirstChild.InnerText;
                    XmlNode parent = xmldoc2.SelectSingleNode("categories");
                    parent.AppendChild(newnode);
                    parent = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    XmlText txt = xmldoc2.CreateTextNode(text);
                    parent.AppendChild(txt);
                    XmlElement newel = xmldoc2.CreateElement("answer");
                    parent.AppendChild(newel);

                    XmlNode tst = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    string img = tst.Attributes["image"].Value;
                    if (img == "true")
                    {
                        XmlNode newnode2 = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/question[@id='" + i + "']/image"), true);
                        parent.AppendChild(newnode2);
                    }
                    newel = xmldoc2.CreateElement("useranswer");
                    parent.AppendChild(newel);

                    //skicka arrayen vidare
                    int[] arrayAnswers = RandomNumbers(1, 4, 2);
                    questions q = new questions();
                    q.setId(i);
                    q.setArr(arrayAnswers);
                    listq.Add(q);
                    ViewState.Add("questions", listq);

                    foreach (int ix in arrayAnswers)
                    {
                        XmlNode newnode2 = xmldoc2.ImportNode(xmldoc.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + ix + "']"), true);
                        XmlNode parent2 = xmldoc2.SelectSingleNode("categories/question[@id='" + i + "']/answer");
                        parent2.AppendChild(newnode2);
                    }

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

                    loadQuest(attributeID, attributeMulti, img, count, listq, pb);
                    count++;
                    xmldoc2.Save(Server.MapPath("usertest.xml"));                   
                }

            }
            else if (IsPostBack)
            {
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
                if (ViewState["pP"] != null)
                {
                    pPoints = (string)ViewState["pP"];
                }
                if(ViewState["ecP"] != null)
                {
                    ecPoints = (string)ViewState["ecP"];
                }
                if(ViewState["ehP"] != null)
                {
                    ethPoints = (string)ViewState["ehP"];
                }
 
   

                bool pb = false;
                List<questions> lista = new List<questions>(); ;
                timerVar = 2;
                btn1.Visible = false;
                btn2.Visible = true;

                xmldoc2.Load(Server.MapPath("usertest.xml"));
                XmlNodeList lst = xmldoc2.SelectNodes("categories/question");
                //Loopar igenom xml listan 1st
                int count = 1;
                if (ViewState["questions"] != null)
                {
                    lista = (List<questions>)ViewState["questions"];
                }
                foreach (XmlNode node in lst)
                {
                    string attributeID = node.Attributes["id"].Value;
                    string attributeMulti = node.Attributes["multi"].Value;
                    string img = node.Attributes["image"].Value;
                    int i = Convert.ToInt16(attributeID);

                    loadQuest(attributeID, attributeMulti, img, count, lista, pb);
                    count++;
                }

    
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState.Add("points", tpoints);
            ViewState.Add("grade", gr);
            ViewState.Add("pP", pPoints);
            ViewState.Add("ecP", ecPoints);
            ViewState.Add("ehP", ethPoints);
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
        protected void loadQuest(string i, string attributeMulti, string img, int count, List<questions> t, bool pb)
        {
            int[] arr = new int[4];
            if (pb == true)
            {
                foreach (var x in listq)
                {
                    int id = x.getId();
                    if (id.ToString() == i)
                    {
                        arr = x.getArr();
                    }
                }

            }
            else if (pb == false)
            {
                foreach (var x in t)
                {
                    int id = x.getId();
                    if (id.ToString() == i)
                    {
                        arr = x.getArr();
                    }
                }
            }
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

                //Skapar ny label
                Label lblQuestion = new Label();

                //Om frågan bara har ett svarsalternativ som är rätt
                if (attributeMulti == "false")
                {
                    //Skapar nya radiobuttons
                    radiob1 = new RadioButton();
                    radiob2 = new RadioButton();
                    radiob3 = new RadioButton();
                    radiob4 = new RadioButton();
                    //Ger ett gruppnamn till radiobuttons 
                    radiob1.GroupName = "gr" + i.ToString();
                    radiob2.GroupName = "gr" + i.ToString();
                    radiob3.GroupName = "gr" + i.ToString();
                    radiob4.GroupName = "gr" + i.ToString();
                    //Sätter ett unikt namn till varje radiobutton
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

                    radiob1.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[0] + "']").InnerText;
                    XmlNode n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id ='" + arr[0] + "']");
                    string at = n.Attributes["correct"].Value;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    string cat = n.Attributes["category"].Value;                    
                    lblQuestion.Text += " (" + cat + ")";
                    radiob1.Attributes.Add("correct", at);
                    radiob1.Attributes.Add("category", cat);
                    string qid = n.Attributes["id"].Value;
                    radiob1.Attributes.Add("qid", arr[0].ToString());

                    radiob2.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[1] + "']").InnerText;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[1] + "']");
                    at = n.Attributes["correct"].Value;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    cat = n.Attributes["category"].Value;
                    radiob2.Attributes.Add("category", cat);
                    radiob2.Attributes.Add("correct", at);
                    qid = n.Attributes["id"].Value;
                    radiob2.Attributes.Add("qid", arr[1].ToString());

                radiob3.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[2] + "']").InnerText;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[2] + "']");
                    at = n.Attributes["correct"].Value;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    cat = n.Attributes["category"].Value;
                    radiob3.Attributes.Add("category", cat);
                    radiob3.Attributes.Add("correct", at);
                qid = n.Attributes["id"].Value;
                radiob3.Attributes.Add("qid", arr[2].ToString());

                radiob4.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[3] + "']").InnerText;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[3] + "']");
                    at = n.Attributes["correct"].Value;
                    n = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    cat = n.Attributes["category"].Value;
                    radiob4.Attributes.Add("category", cat);
                    radiob4.Attributes.Add("correct", at);
                qid = n.Attributes["id"].Value;
                radiob4.Attributes.Add("qid", arr[3].ToString());
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

                    //Skapar nya checkboxes
                    checkbox1 = new CheckBox();
                    checkbox2 = new CheckBox();
                    checkbox3 = new CheckBox();
                    checkbox4 = new CheckBox();
                    answ ch = new answ();
                    counter cnt = new counter();
                
                //checkbox1.Attributes.Add("runat", "server");
                //checkbox2.Attributes.Add("runat", "server");
                //checkbox3.Attributes.Add("runat", "server");
                //checkbox4.Attributes.Add("runat", "server");

                //checkbox1.Attributes.Add("onClick", "Check(this)");
                //checkbox2.Attributes.Add("onClick", "Check(this)");
                //checkbox3.Attributes.Add("onClick", "Check(this)");
                //checkbox4.Attributes.Add("onClick", "Check(this)");

                //checkbox1.A
                //checkbox1.ID = i.ToString() + "c1";
                //checkbox2.ID = i.ToString() + "c2";
                //checkbox3.ID = i.ToString() + "c3";
                //checkbox4.ID = i.ToString() + "c4";

             

                    checkbox1.InputAttributes.Add("name", "check" + count);
                    checkbox1.InputAttributes.Add("value", "1");


                    checkbox2.InputAttributes.Add("name", "check" + count);
                    checkbox2.InputAttributes.Add("value", "2");
            

                    checkbox3.InputAttributes.Add("name", "check" + count);
                    checkbox3.InputAttributes.Add("value", "3");
           

                    checkbox4.InputAttributes.Add("name", "check" + count);
                    checkbox4.InputAttributes.Add("value", "4");
                
                    cnt.setGroup("check" + count.ToString());
                    ch.setId(Convert.ToInt16(i));
          

                lblQuestion.Text = count + ": " + (xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']").FirstChild.InnerText);
                    XmlNode n1 = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    string cat1 = n1.Attributes["multi"].Value;
                    lblQuestion.Attributes.Add("multi", cat1);
                    string cat2 = n1.Attributes["id"].Value;
                    lblQuestion.Attributes.Add("id", cat2);

                    checkbox1.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[0] + "']").InnerText;
                    XmlNode usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[0] + "']");
                    string at = usernode.Attributes["correct"].Value;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    string cat = usernode.Attributes["category"].Value;
                    lblQuestion.Text += " (" + cat + ")";
                    checkbox1.Attributes.Add("category", cat);
                    checkbox1.Attributes.Add("correct", at);
                    checkbox1.Attributes.Add("group", i);
                    string qid = n1.Attributes["id"].Value;
                    checkbox1.Attributes.Add("qid", arr[0].ToString());
                if (at == "true")
                    {
                        ch.setCount();
                        cnt.setCount();
                    }

                    checkbox2.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[1] + "']").InnerText;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[1] + "']");
                    at = usernode.Attributes["correct"].Value;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    cat = usernode.Attributes["category"].Value;
                    qid = n1.Attributes["id"].Value;
                    checkbox2.Attributes.Add("qid", arr[1].ToString());
                if (at == "true")
                    {
                        ch.setCount();
                        cnt.setCount();
                }
                    checkbox2.Attributes.Add("category", cat);
                    checkbox2.Attributes.Add("correct", at);
                    checkbox2.Attributes.Add("group", i);


                    checkbox3.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[2] + "']").InnerText;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[2] + "']");
                    at = usernode.Attributes["correct"].Value;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    cat = usernode.Attributes["category"].Value;
                    qid = n1.Attributes["id"].Value;
                    checkbox3.Attributes.Add("qid", arr[2].ToString());
                if (at == "true")
                    {
                        ch.setCount();
                        cnt.setCount();
                }
                    checkbox3.Attributes.Add("category", cat);
                    checkbox3.Attributes.Add("correct", at);
                    checkbox3.Attributes.Add("group", i);

                    checkbox4.Text = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id = '" + arr[3] + "']").InnerText;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer[@id='" + arr[3] + "']");
                    at = usernode.Attributes["correct"].Value;
                    usernode = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']");
                    cat = usernode.Attributes["category"].Value;
                    if (at == "true")
                    {
                        ch.setCount();
                         cnt.setCount();
                }
                    checkbox4.Attributes.Add("category", cat);
                    checkbox4.Attributes.Add("correct", at);
                    checkbox4.Attributes.Add("group", i);
                    qid = n1.Attributes["id"].Value;
                    checkbox4.Attributes.Add("qid", arr[3].ToString());
                        //Lägger in checkboxar i cellerna
                    cell1.Controls.Add(lblQuestion);
                    cell1.ColumnSpan = 2;

                    cell2.Controls.Add(checkbox1);
                    cell3.Controls.Add(checkbox2);
                    cell4.Controls.Add(checkbox3);
                    cell5.Controls.Add(checkbox4);

                    ch.setCat(cat);
                    list.Add(ch);
                    checklist.Add(cnt);
               

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
            string id = "";
            string qtext = "";
            foreach (TableRow rw in table1.Rows)
            {
                foreach (TableCell cell in rw.Cells)
                {
                    foreach (Control cl in cell.Controls)
                    {
                        if(cl is Label)
                        {
                            Label label = (Label)cl;
                            id = label.Attributes["id"];                          
                        }
                        if (cl is RadioButton)
                        {
                            RadioButton rad = (RadioButton)cl;
                            rad.Enabled = false;
                            string cor = rad.Attributes["correct"];
                            string cat = rad.Attributes["category"];
                            string qid = rad.Attributes["qid"];
                            if (rad.Checked == true)
                            {
                                qtext = rad.Text;
                                writeToXml(id, qtext, qid, cor);
                                if (cor == "true")
                                {
                                    if (cat == "products")
                                    {
                                        prod++;
                                      
                
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
   
                                }

                            }

                        }
                        else if (cl is CheckBox)
                        {
                            CheckBox chk = (CheckBox)cl;

                            string cor = chk.Attributes["correct"];
                            string cat = chk.Attributes["category"];
                            string chkID = chk.Attributes["value"];
                            string qid = chk.Attributes["qid"];
                            
                            
                            if (chk.Checked == true)
                            {
                                qtext = chk.Text;
                                writeToXml(id, qtext, qid, cor);
                                chk.Enabled = false;
                            
                                    
                                    int boxid = Convert.ToInt16(chk.Attributes["group"]);
                                foreach (var x in list)
                                {
                                    int ident = x.getId();

                                    if (ident == boxid)
                                    {
                                        if (cor == "true")
                                        {
                                            x.setAnswer();
                                            x.setTAnsw();
                                        } 
                                        else if(cor == "false")
                                        {
                                            x.setTAnsw();
                                        }  
                                     }

                                    }
                              
                     
                            }
                        }
                    }
                }
            }
            countCorrect();
            saveResult();
        }
        protected void countCorrect()
        {
   
            foreach(var o in list)
            {
                if(o.getCorrect() == true)
                {
                    string cat = o.getCat();
                    if (cat == "products")
                    {
                        prod++;
                    }
                    else if(cat == "ethics")
                    {
                        eth++;
                    }
                    else
                    {
                        eco++;
                    }
                }
            } 
        }
        protected void saveResult()
        {
      
            double total = prod;
            total += eco;
            total += eth;
            tpoints = total.ToString();
            if(Application["type"] != null)
            {
                type = Application["type"].ToString();
            }
            else
            {
                type = "a";
            }

            
            if (type == "a")
            {
                int percentProducts = (int)Math.Round((double)(100 * prod) / 8);
                int percentEthics = (int)Math.Round((double)(100 * eth) / 9);
                int percentEconomy = (int)Math.Round((double)(100 * eco) / 8);
                ecPoints = percentEconomy.ToString();
                ethPoints = percentEthics.ToString();
                pPoints = percentProducts.ToString();
                string gradestring = "";
                string licens = "";
                if (total / 25 >= 0.70 && prod / 8 > 0.60 && eco / 8 > 0.60 && eth / 8 > 0.60)
                {
                    gr = 1;
                    gradestring = "Godkänd";
                    licens = "Licensierad";
                }
                else
                {
                    gr = 2;
                    gradestring = "Underkänd";
                    licens = "Ej licensierad";
                }

                string savexml = xmldoc2.OuterXml;
                string tn = "Licensieringstest";
                user();
                int ln = userid;
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
                    
                    conn.Open();
                    cmd = new NpgsqlCommand("update users set licensed = @value where users.userid = @id", conn);
                    cmd.Parameters.AddWithValue("@value", licens);
                    cmd.Parameters.AddWithValue("@id", userid);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch
                {
                    
                }

            }
            else
            {
                int percentProducts = (int)Math.Round((double)(100 * prod) / 5);
                int percentEthics = (int)Math.Round((double)(100 * eth) / 5);
                int percentEconomy = (int)Math.Round((double)(100 * eco) / 5);
                ecPoints = percentEconomy.ToString();
                ethPoints = percentEthics.ToString();
                pPoints = percentProducts.ToString();
                string gradestring = "";
            
                if (total / 15 >= 0.70 && prod / 8 > 0.60 && eco / 8 > 0.60 && eth / 8 > 0.60)
                {
                    gr = 1;
                    gradestring = "Godkänd";
                }
                else
                {
                    gr = 2;
                    gradestring = "Underkänd";
                }

                string savexml = xmldoc2.OuterXml;
                string tn = "ÅKU";
                user();
                int ln = userid;
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

            }
          }
        protected void btn2_Click(object sender, EventArgs e)
        {
            Response.Redirect("mytests.aspx");
        }
        protected void writeToXml(string i, string q, string qid, string avalue)
        {

                 
            XmlNode nd = xmldoc2.SelectSingleNode("/categories/question[@id='"+i+"']/useranswer");
            XmlNode nd2 = xmldoc2.SelectSingleNode("/categories/question[@id='" + i + "']/answer/answer");
            string answid = nd2.Attributes["id"].Value;   
            XmlElement el = xmldoc2.CreateElement("question");
            el.SetAttribute("id", i);
            el.SetAttribute("correct", avalue);
            el.SetAttribute("qid", qid);
 
  
            el.InnerText = q;       
            nd.AppendChild(el);
            xmldoc2.Save(Server.MapPath("usertest.xml"));
        }

        protected void user()
        {
            if (Application["user"] != null)
            {
                string user = Application["user"].ToString();
                if (user == "henrik")
                {

                    userid = 3;

                }
                else if (user == "michael")
                {
                    userid = 2;

                }
                else if (user == "stefan")
                {
                    userid = 1;

                }
                else if (user == "bertil")
                {
                    userid = 6;

                }
                else if (user == "nils")
                {
                    userid = 5;

                }
                else if(user == "kalle")
                {
                    userid = 7;
                }
                else if(user == "tobbe")
                {
                    userid = 8;
                }
                else
                {
                    userid = 1;

                }
            }
        }
        [WebMethod]
        public static void timeOut(int type,int id)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
            XmlDocument xmldoc2 = new XmlDocument();
            DateTime date = DateTime.Today;
            int total = 0;
            string gradestring = "Underkänd";
            string licensed = "Ej licensierad";
            
            int userid = id;
            string tn = "";

            xmldoc2.Load("usertest.xml");
            string savexml = xmldoc2.OuterXml;

            if(type == 1)
            {
                tn = "Licensieringstest";
            }
            else
            {
                tn = "ÅKU";
            }
      
           
            string sql = "insert into license_test(name, user_id, grade, points, date, testxml) values(:tname, :user, :grd, :pts, :dt, :addxml)";
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.Add(new NpgsqlParameter("tname", tn));
                cmd.Parameters.Add(new NpgsqlParameter("user", userid));
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
            try
            {
                if (type == 1)
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("update users set licensed = @value where users.userid = @id", conn);
                    cmd.Parameters.AddWithValue("@value", licensed);
                    cmd.Parameters.AddWithValue("@id", userid);
                    cmd.ExecuteNonQuery();
                    conn.Close();
            
                }
            }
            catch
            {

            }
        }
    }

}
