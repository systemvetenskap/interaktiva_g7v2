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
            //DropDownListGrade.SelectedValue = "Godkänd";
            //ListShows_Click(ButtonSearchTest.Click(EventArgs.Empty);

        }

        void ListShows_Click(Object sender, EventArgs e)
        {
            //string dropdownGrade = "AND t.grade =  '" + DropDownListGrade.SelectedValue + "'";
            string dropdownGrade = null,
                   dropdownLicens = "";
            

            if (DropDownListGrade.SelectedValue == "Godkänd")
            {
                dropdownGrade = "Godkänd";
            }

            else if (DropDownListGrade.SelectedValue == "Icke godkänd")
            {
                dropdownGrade = "Icke Godkänd";
            }

            else if (DropDownListGrade.SelectedValue == "Alla")
            {
                dropdownGrade = "NotNull";
            }

            if (DropDownListLicensed.SelectedValue == "Alla")
            {
                dropdownLicens = "Licensed AND licensed = 'Icke licensed'";
            }

            else if (DropDownListLicensed.SelectedValue == "Licensed")
            {
                dropdownLicens = "Licensed";
            }

            else if (DropDownListLicensed.SelectedValue == "Icke licensed")
            {
                dropdownLicens = "Icke licensed";
            }



            NpgsqlDataAdapter da;
            DataTable dt = new DataTable();
            GridViewMyTests.DataSource = null;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(@"select u.first_name, u.last_name, u.licensed, t.name, t.grade, t.points, t2.maxdate, l.firstname, l.lastname
                                                    from license_test t
                                                    inner join
                                                    (
                                                    select max(date) maxdate, user_id from license_test
                                                    group by user_id) t2 on t.user_id = t2.user_id and t.date = t2.maxdate
                                                    right join users u on t.user_id = u.userid
                                                    inner join leader l on u.leader_id = l.leader_id 
                                                    WHERE grade = @grade
                                                    AND licensed = @licensed"
                                                    , conn);
            cmd.Parameters.AddWithValue("@grade", dropdownGrade);
            cmd.Parameters.AddWithValue("@licensed", dropdownLicens);

            da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            GridViewMyTests.DataSource = dt;
            GridViewMyTests.DataBind();
            
            conn.Close();
            
        }

        
    }
}