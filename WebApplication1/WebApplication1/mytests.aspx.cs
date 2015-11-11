using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.IO;
using System.Web.Configuration;


namespace WebApplication1
{
    public partial class mytests : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        string user;
        int userid = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            loadUser();
            loadData();

            if(Application["Role"] != null)
            {
                string role = Application["role"].ToString();
                if (role == "member")
                {
                    provresultat.Visible = false;
                }

            }
 
            if(!IsPostBack)
            {
                ListLeaders();
            }
            ButtonSearchTest.Click += new EventHandler(this.ListShows_Click);
            //DropDownListGrade.SelectedValue = "Godkänd";
            //ListShows_Click(ButtonSearchTest.Click(EventArgs.Empty);

        }
        

        private void ListLeaders()
        {
            DataTable dt = new DataTable();
            DataTable ts = new DataTable();

            

            dt.Columns.Add("name");
            dt.Columns.Add("leaderid");
            DataRow row = dt.NewRow();
            row[0] = "Alla";
            row[1] = DBNull.Value;
            dt.Rows.Add(row);

            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT firstname, lastname, leader_id FROM leader", conn);
            
            conn.Close();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(ts);
            foreach (DataRow r in ts.Rows)
            {
                string name = r[0].ToString() + " " + r[1].ToString();
                string leaderid = r[2].ToString();
                row = dt.NewRow();
                row[0] = name;
                row[1] = leaderid;
                dt.Rows.Add(row);
            }
            DropDownListLeader.DataSource = dt;
            DropDownListLeader.DataTextField = "name";

            DropDownListLeader.DataValueField = "leaderid";

            DropDownListLeader.DataBind();
            conn.Close();

        }
    

        void ListShows_Click(Object sender, EventArgs e)
        {

            string sql = @"select u.first_name, u.last_name, u.licensed, t.name, t.grade, t.points, t2.maxdate, l.firstname, l.lastname
                                                       from license_test t
                                                       inner join
                                                       (
                                                       select max(date) maxdate, user_id from license_test
                                                       group by user_id) t2 on t.user_id = t2.user_id and t.date = t2.maxdate
                                                       right join users u on t.user_id = u.userid
                                                       inner join leader l on u.leader_id = l.leader_id 
                                                       WHERE (grade = @grade OR grade = @grade2)
                                                       AND (licensed = @licensed OR licensed = @licensed2)";

            Label1.Text = DropDownListLeader.SelectedValue;

            if (DropDownListGrade.SelectedValue != "Inga betyg")
            {

                //string dropdownGrade = "AND t.grade =  '" + DropDownListGrade.SelectedValue + "'";
                string dropdownGrade = "Godkänd",
                       dropdownGrade2 = "Icke Godkänd",
                       dropdownLicens = "Licensed",
                       dropdownLicens2 = "Icke licensed",
                       dropdownLeader = DropDownListLeader.SelectedValue;

     
                if (DropDownListGrade.SelectedValue == "Godkänd")
                {
                    dropdownGrade = "Godkänd";
                    dropdownGrade2 = "Godkänd";
                }

                else if (DropDownListGrade.SelectedValue == "Icke godkänd")
                {
                    dropdownGrade = "Icke Godkänd";
                    dropdownGrade2 = "Icke Godkänd";
                }



                if (DropDownListLicensed.SelectedValue == "Licensed")
                {
                    dropdownLicens = "Licensed";
                    dropdownLicens2 = "Licensed";
                }

                else if (DropDownListLicensed.SelectedValue == "Icke licensed")
                {
                    dropdownLicens = "Icke licensed";
                    dropdownLicens2 = "Icke licensed";
                }

                if (DropDownListLeader.SelectedIndex > 0)
                {
                    string id = DropDownListLeader.SelectedValue;
                    string addSql = "and l.leader_id ='" + id + "'";
                    sql += addSql;

                }

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string sql = @"select date, grade, name, points,   leader.firstname, leader.lastname , testid from license_test
                            inner join users on license_test.user_id = users.userid
                            inner join leader on users.leader_id = leader.leader_id
                            where users.userid = '" + userid+"'";
            conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@grade", dropdownGrade);
                cmd.Parameters.AddWithValue("@grade2", dropdownGrade2);
                cmd.Parameters.AddWithValue("@licensed", dropdownLicens);
                cmd.Parameters.AddWithValue("@licensed2", dropdownLicens2);


                

                dt.Columns.Add("fullname");
                dt.Columns.Add("licensed");
                dt.Columns.Add("name");
                dt.Columns.Add("grade");
                dt.Columns.Add("points");
                dt.Columns.Add("maxdate");
                dt.Columns.Add("leader");

                DataRow row = dt.NewRow();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt2);
                foreach (DataRow r in dt2.Rows)
                {
                    string fullname = r[0].ToString() + " " + r[1].ToString();
                    string licens = r[2].ToString();
                    string testname = r[3].ToString();
                    string grade = r[4].ToString();
                    string points = r[5].ToString();
                    string date = r[6].ToString();
                    string leader = r[7].ToString() +" " + r[8].ToString();
                    row = dt.NewRow();
                    row[0] = fullname;
                    row[1] = licens;
                    row[2] = testname;
                    row[3] = grade;
                    row[4] = points;
                    row[5] = date;
                    row[6] = leader;
            da.Fill(dt);

            dt2.Columns.Add("date");
            dt2.Columns.Add("grade");
            dt2.Columns.Add("name");
            dt2.Columns.Add("points");          
            dt2.Columns.Add("leader");            
            dt2.Columns.Add("testid");
   
            string format = "yyyy-MM-dd";

            foreach (DataRow r in dt.Rows)
            {

                DataRow row = dt2.NewRow();
                DateTime date = Convert.ToDateTime(r[0]);
                row[0] = date.ToString(format);
                row[1] = r[1];
                row[2] = r[2];
                row[3] = r[3];
                row[4] = r[4].ToString() + " " + r[5].ToString();
                row[5] = r[6];

                dt2.Rows.Add(row);
            }
               // NpgsqlDataAdapter da;
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                GridViewMyTests.DataSource = null;

                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql2 , conn);

                cmd.Parameters.AddWithValue("@licensed", dropdownLicens);
                cmd.Parameters.AddWithValue("@licensed2", dropdownLicens2);

                //da = new NpgsqlDataAdapter(cmd);
                //da.Fill(dt);
                //GridViewMyTests.DataSource = dt;
                //GridViewMyTests.DataBind();

                //conn.Close();

                dt.Columns.Add("fullname");
                dt.Columns.Add("licensed");
                dt.Columns.Add("name");
                dt.Columns.Add("grade");
                dt.Columns.Add("points");
                dt.Columns.Add("maxdate");
                dt.Columns.Add("leader");

                DataRow row = dt.NewRow();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt2);
                foreach (DataRow r in dt2.Rows)
                {
                    string fullname = r[0].ToString() + " " + r[1].ToString();
                    string licens = r[2].ToString();
                    string testname = r[3].ToString();
                    string grade = r[4].ToString();
                    string points = r[5].ToString();
                    string date = r[6].ToString();
                    string leader = r[7].ToString() + " " + r[8].ToString();
                    row = dt.NewRow();
                    row[0] = fullname;
                    row[1] = licens;
                    row[2] = testname;
                    row[3] = grade;
                    row[4] = points;
                    row[5] = date;
                    row[6] = leader;

                    dt.Rows.Add(row);
        }

                GridViewMyTests.DataSource = dt;
                GridViewMyTests.DataBind();

            conn.Close();
        }
        }
        protected void btnLicenseTest_Click(object sender, EventArgs e)
        {
            Application["type"] = "a";
            Response.Redirect("dotest.aspx?type=a&id="+userid);
        }

        protected void btnUpdateTest_Click(object sender, EventArgs e)
        {
            Application["type"] = "b";
            Response.Redirect("dotest.aspx?type=b&id="+userid);
        }
        protected void loadUser()
        {
            if (Application["user"] != null)
            {
                user = Application["user"].ToString();
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
        protected void loadData()
        {
            string s = "";
            lblAku.Text = "";
            lblLicens.Text = "";
            DateTime date = DateTime.Today;
            DateTime last = DateTime.Now;
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select licensed from users where users.userid = @id", conn);
            cmd.Parameters.AddWithValue("@id", userid);
            NpgsqlDataReader r = cmd.ExecuteReader();
            while(r.Read())
            {
                s = r[0].ToString();
            }
            conn.Close();
            if (s == "Ej licensierad")
            {
                lblLicens.Text = "Ej Licensierad";
                lblLicens.ForeColor = System.Drawing.Color.Tomato;
                btnUpdateTest.Enabled = false;
            }
            else
            {
                lblLicens.Text = "Licensierad";
                lblLicens.ForeColor = System.Drawing.Color.LawnGreen;
                btnLicenseTest.Enabled = false;
            }
            conn.Open();
            string sql = @"select t.name, t.grade, t2.maxdate, testid, u.licensed from license_test t
                                                       inner join
                                                       (
                                                       select max(date) maxdate, user_id from license_test
                                                       group by user_id) t2 on t.user_id = t2.user_id and t.date = t2.maxdate
                                                       right join users u on t.user_id = u.userid
                                                       inner join leader l on u.leader_id = l.leader_id where u.userid = @id";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", userid);
            r = cmd.ExecuteReader();
            
            while (r.Read())
            {
                if(!string.IsNullOrWhiteSpace(r[2].ToString()))
                {
                    last = Convert.ToDateTime(r[2].ToString());
                    if(r[0].ToString() == "Licensieringstest")
                    {
                        if(r[1].ToString() == "Underkänd")
                        {
                            btnLicenseTest.Enabled = false;
                            btnUpdateTest.Enabled = false;
                            DateTime newtry = last.AddDays(7);
                            lblLicens.Text = "Ej licensierad. Nytt försök kan göras tidigast: "+ newtry.ToShortDateString();
                            lblLicens.ForeColor = System.Drawing.Color.Tomato;
                            lblAku.Text = "";
                            if(date >= newtry)
                            {
                                btnLicenseTest.Enabled = true;
                                lblLicens.Text = "Ej licensierad. Nytt försök tillgängligt";
                                lblLicens.ForeColor = System.Drawing.Color.Orange;

                            }
                        }
                        else
                        {
                            btnLicenseTest.Enabled = false;
                            btnUpdateTest.Enabled = false;
                            DateTime newtry = last.AddYears(1);
                            lblLicens.Text = "Licensierad";
                            lblLicens.ForeColor = System.Drawing.Color.LawnGreen;
                            lblAku.Text = "Kunskapsuppdatering tidigast: " + newtry.ToShortDateString();
                            if (date >= newtry)
                            {
                                btnUpdateTest.Enabled = true;

                                lblAku.Text = "Kunskapsuppdatering tillgänglig";
                                lblAku.ForeColor = System.Drawing.Color.Orange;
                            }
                        }
                    }
                    else if(r[0].ToString() == "ÅKU")
                    {
                        if (r[1].ToString() == "Underkänd")
                        {
                            btnLicenseTest.Enabled = false;
                            btnUpdateTest.Enabled = false;
                            DateTime newtry = last.AddDays(7);
                            lblAku.Text = "Underkänd. Nytt försök kan göras tidigast: " + newtry.ToShortDateString();
                            lblAku.ForeColor = System.Drawing.Color.Tomato;
                            if (date >= newtry)
                            {
                                btnUpdateTest.Enabled = true;
                                lblAku.Text = "Kunskapsuppdatering tillgänglig";
                                lblAku.ForeColor = System.Drawing.Color.Orange;
                            }

                        }
                        else
                        {
                            btnLicenseTest.Enabled = false;
                            btnUpdateTest.Enabled = false;
                            DateTime newtry = last.AddYears(1);
                            lblLicens.Text = "Licensierad";
                            lblLicens.ForeColor = System.Drawing.Color.LawnGreen;
                            lblAku.Text = "Kunskapsuppdatering tidigast: " + newtry.ToShortDateString();
                            if (date >= newtry)
                            {
                                btnUpdateTest.Enabled = true;
                                lblAku.Text = "Kunskapsuppdatering tillgänglig";
                                lblAku.ForeColor = System.Drawing.Color.Orange;
                            }
                        }
                    }
                }
                

            }
            conn.Close();

        }
    }
}

