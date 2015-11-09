﻿using System;
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
    public partial class testres1 : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListLeaders();
                DropDownListGrade.SelectedValue = "Icke godkänd";
                ShowList();
            }


            ButtonSearchTest.Click += new EventHandler(this.ListShows_Click);

        }

        void ListShows_Click(Object sender, EventArgs e)
        {
            ShowList();
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


        private void ShowList()
        {

            string sql = @"select u.first_name, u.last_name, u.licensed, t.name, t.grade, t.points, t2.maxdate, l.firstname, l.lastname
                                                       from license_test t
                                                       inner join
                                                       (
                                                       select max(date) maxdate, user_id from license_test
                                                       group by user_id) t2 on t.user_id = t2.user_id and t.date = t2.maxdate
                                                       right join users u on t.user_id = u.userid
                                                       inner join leader l on u.leader_id = l.leader_id ";

            ///// LICENS
            if (DropDownListLicensed.SelectedValue == "Licensed")
            {
                string addSql = "WHERE  licensed = 'Licensed' ";
                sql += addSql;
            }

            else if (DropDownListLicensed.SelectedValue == "Icke licensed")
            {
                string addSql1 = "WHERE  licensed = 'Icke licensed' ";
                sql += addSql1;
            }

            else if (DropDownListLicensed.SelectedValue == "Alla")
            {
                string addSql2 = "WHERE  (licensed = 'Icke licensed' OR licensed = 'Licensed') ";
                sql += addSql2;
            }

            //GRADE
            if (DropDownListGrade.SelectedValue == "Godkänd")
            {
                string addSql3 = "AND grade = 'Godkänd' ";
                sql += addSql3;
            }

            else if (DropDownListGrade.SelectedValue == "Icke godkänd")
            {
                string addSql4 = "AND grade = 'Icke Godkänd' ";
                sql += addSql4;
            }


            else if (DropDownListGrade.SelectedValue == "Inga betyg")
            {
                string addSql5 = "AND grade isNull ";
                sql += addSql5;
            }

            //LEADERS
            if (DropDownListLeader.SelectedIndex > 0)
            {
                string addSql6 = "and l.leader_id = @leader_id ";
                sql += addSql6;
            }

            sql += "order by maxdate desc";

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            GridViewMyTests.DataSource = null;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@leader_id", DropDownListLeader.SelectedValue);


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
}