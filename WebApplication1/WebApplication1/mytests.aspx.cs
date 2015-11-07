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
        NpgsqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                GridViewMyTests.DataSource = null;

                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@grade", dropdownGrade);
                cmd.Parameters.AddWithValue("@grade2", dropdownGrade2);
                cmd.Parameters.AddWithValue("@licensed", dropdownLicens);
                cmd.Parameters.AddWithValue("@licensed2", dropdownLicens2);
                //cmd.Parameters.AddWithValue("@leader", dropdownLicens2);
                //cmd.Parameters.AddWithValue("@leader2", dropdownLicens2);

                da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                GridViewMyTests.DataSource = dt;
                GridViewMyTests.DataBind();

                conn.Close();
                conn.Close();
            }


            else
            {
                string sql2 = @"select u.first_name, u.last_name, u.licensed, t.name, t.grade, t.points, t2.maxdate, l.firstname, l.lastname
                                                       from license_test t
                                                       inner join
                                                       (
                                                       select max(date) maxdate, user_id from license_test
                                                       group by user_id) t2 on t.user_id = t2.user_id and t.date = t2.maxdate
                                                       right join users u on t.user_id = u.userid
                                                       inner join leader l on u.leader_id = l.leader_id 
                                                       Where (licensed = @licensed OR licensed = @licensed2)
                                                       ";
                //string dropdownGrade = "AND t.grade =  '" + DropDownListGrade.SelectedValue + "'";
                string
                       dropdownLicens = "Licensed",
                       dropdownLicens2 = "Icke licensed";

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
                    sql2 += addSql;

                }
                NpgsqlDataAdapter da;
                DataTable dt = new DataTable();
                GridViewMyTests.DataSource = null;

                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql2 , conn);

                cmd.Parameters.AddWithValue("@licensed", dropdownLicens);
                cmd.Parameters.AddWithValue("@licensed2", dropdownLicens2);

                da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                GridViewMyTests.DataSource = dt;
                GridViewMyTests.DataBind();

                conn.Close();
            }
        }

        
    }
}