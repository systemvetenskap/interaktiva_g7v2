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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonSearchTest.Click += new EventHandler(this.ListShows_Click);
            DropDownListGrade.SelectedValue = "Godkänd";
            //ListShows_Click(ButtonSearchTest.Click(EventArgs.Empty);

        }

        void ListShows_Click(Object sender, EventArgs e)
        {
            string dropdownGrade = "AND t.grade =  '" + DropDownListGrade.SelectedValue + "'";
            Label1.Text = dropdownGrade;

            if (DropDownListGrade.SelectedValue == "Alla")
            {
                dropdownGrade = "";
            }
                Label1.Text = dropdownGrade;

            NpgsqlDataAdapter da;
            DataTable dt = new DataTable();
            GridViewMyTests.DataSource = null;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(@" SELECT t.name, t.grade, t.points, t.date, u.first_name, u.last_name, u.licensed, l.firstname, l.lastname
                                FROM users u

                                FULL JOIN license_test t
                                ON u.userid = t.user_id 

                                FULL JOIN leader l
                                ON l.leader_id = u.leader_id   WHERE t.date = (SELECT t.date FROM license_test t WHERE u.userid = t.user_id  ORDER BY t.date DESC LIMIT 1) OR u.userid NOT IN (SELECT u.userid FROM users u FULL JOIN license_test t ON u.userid = t.user_id WHERE t.date = (SELECT t.date FROM license_test t WHERE u.userid = t.user_id  ORDER BY t.date DESC LIMIT 1));", conn);

                
            da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            GridViewMyTests.DataSource = dt;
            GridViewMyTests.DataBind();
            
            conn.Close();
            
        }

        
    }
}