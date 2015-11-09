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
        protected void Page_Load(object sender, EventArgs e)
        {
            string role = Application["role"].ToString();
            if (role=="member")
            {
                provresultat.Visible = false;
            }
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string sql = @"select date, grade, points, name, leader.firstname, leader.lastname from license_test
                            inner join users on license_test.user_id = users.userid
                            inner join leader on users.leader_id = leader.leader_id
                            where users.userid = 1";
            conn.Open();

            sql += "order by date desc";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            dt2.Columns.Add("date");
            dt2.Columns.Add("grade");
            dt2.Columns.Add("points");
            dt2.Columns.Add("name");
            dt2.Columns.Add("leader");
            dt2.Columns.Add("oldtest");
            string format = "yyyy-MM-dd";
          

           // Use current time.
           // Use this format.
        

            foreach (DataRow r in dt.Rows)
            {
                DataRow row = dt2.NewRow();
                DateTime date = Convert.ToDateTime(r[0]);
                row[0] = date.ToString(format);
                row[1] = r[1];
                row[2] = r[2];
                row[3] = r[3];
                row[4] = r[4].ToString() + " " + r[5].ToString();
                dt2.Rows.Add(row);
            }

            GridViewTests.DataSource = dt2;
            GridViewTests.DataBind();
            conn.Close();

        }

        protected void btnLicenseTest_Click(object sender, EventArgs e)
        {
            Application["type"] = "a";
            Response.Redirect("dotest.aspx");

        }

        protected void btnUpdateTest_Click(object sender, EventArgs e)
        {
            Application["type"] = "b";
            Response.Redirect("dotest.aspx");
        }
    }
}