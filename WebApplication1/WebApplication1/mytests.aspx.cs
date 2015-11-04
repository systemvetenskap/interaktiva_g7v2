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
            ListShows();
        }

        public void ListShows()
        {
            NpgsqlDataAdapter da;
            DataTable dt = new DataTable();
            GridViewMyTests.DataSource = null;
            try
            {
                string sql = "select * from users";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                conn.Open();
                da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(dt);
                GridViewMyTests.DataSource = dt;
                GridViewMyTests.DataBind();
                //GridViewMyTests.Columns[1].HeaderText = "userid";
                //GridViewMyTests.Columns[2].HeaderText = "first_name";
                //GridViewMyTests.Columns[3].HeaderText = "last_name";
            }
            catch (Exception e)
            {

                Response.Write(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}