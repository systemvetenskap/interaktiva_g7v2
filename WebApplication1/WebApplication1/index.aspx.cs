using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Npgsql;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Timers;

namespace WebApplication1
{
    public partial class index : System.Web.UI.Page
    {

        public string fname, lname, auth;
        NpgsqlConnection conn = new NpgsqlConnection("Server=webblabb.miun.se;Port=5432; User Id=pgmvaru_g7;Password=akrobatik;Database=pgmvaru_g7;SSL=true;");
        protected void Page_Load(object sender, EventArgs e)
        {
            //LabelStatusLogin.Visible = false;
        }
        protected void btn1_Click(object sender, EventArgs e)
        {
            //string username = textboxusername.Text;
            //string password = textboxpassword.Text;

            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(@"select first_name, last_name, licensed, username, password, auth 
                                                            from USERS where username = @username and password = @password;", conn);
                //command.Parameters.AddWithValue("@username", username);
                //command.Parameters.AddWithValue("@password", password);

                NpgsqlDataReader read;
                read = command.ExecuteReader();
                read.Read();

                fname = read[0].ToString();
                lname = read[1].ToString();
                auth = read[5].ToString();

                conn.Close();
                //LabelStatusLogin.Visible = true;
                //LabelStatusLogin.ForeColor = System.Drawing.Color.Green;
                //LabelStatusLogin.Text = "Du är nu inloggad som " +fname+" "+lname;
                return;
            }
            catch (InvalidOperationException)
            {
                //LabelStatusLogin.Visible = true;
                //LabelStatusLogin.ForeColor = System.Drawing.Color.Red;
                //LabelStatusLogin.Text = "Felaktigt användarnamn eller lösenord";
                return;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}