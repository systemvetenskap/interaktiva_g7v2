using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Npgsql;

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
 
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string sql = @"select date, grade, name, points,   leader.firstname, leader.lastname , testid from license_test
                            inner join users on license_test.user_id = users.userid
                            inner join leader on users.leader_id = leader.leader_id
                            where users.userid = '" + userid+"'";
            conn.Open();

            sql += "order by date desc";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
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
            GridViewTests.DataSource = dt2;
            GridViewTests.DataBind();
            conn.Close();
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