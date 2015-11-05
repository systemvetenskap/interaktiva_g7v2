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
                string sql = @" SELECT t.name, t.grade, t.points, t.date, u.first_name, u.last_name, u.licensed, l.firstname, l.lastname
                                FROM users u

                                FULL JOIN license_test t
                                ON u.userid = t.user_id 

                                FULL JOIN leader l
                                ON l.leader_id = u.leader_id 

                                WHERE t.date = (SELECT t.date
                                                 FROM license_test t
                                                 WHERE u.userid = t.user_id           
                                                 ORDER BY t.date DESC
                                                 LIMIT 1)
                               OR u.userid NOT IN (SELECT u.userid
                                                   FROM users u
                                                   FULL JOIN license_test t
                                                   ON u.userid = t.user_id 
                                                   WHERE t.date = (SELECT t.date
                                                   FROM license_test t
                                                   WHERE u.userid = t.user_id           
                                                   ORDER BY t.date DESC
                                                   LIMIT 1));";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                conn.Open();
                da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(dt);
                GridViewMyTests.DataSource = dt;
                GridViewMyTests.DataBind();
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