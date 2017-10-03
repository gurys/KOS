using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace KOS
{
    public partial class Prim : System.Web.UI.Page
    {
        int rwId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["rwId"]))
                rwId = Int32.Parse(Request["rwId"]);

            if (!IsPostBack && rwId > 0)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select Prim, Done from ReglamentWorks where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", rwId);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Prime.Text = dt.Rows[0]["Prim"].ToString();
                        Done.Checked = bool.Parse(dt.Rows[0]["Done"].ToString());
                    }
                }                
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (rwId > 0)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    string prim = "Дата/время: " + DateTime.Now.ToString() + "\r\n";
                    SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                        "join Users u on u.UserId=ui.UserId where u.UserName=@name", conn);
                    cmd.Parameters.AddWithValue("name", User.Identity.Name);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    prim += dt.Rows[0]["Family"].ToString() + " " + dt.Rows[0]["IO"].ToString() + "\r\n";
                    prim = Prime.Text + "\r\n" + prim + Comments.Text;
                    cmd = new SqlCommand("update ReglamentWorks set Prim=@prim, Done=@done where Id=@id", conn);
                    cmd.Parameters.AddWithValue("prim", prim);
                    cmd.Parameters.AddWithValue("done", Done.Checked);
                    cmd.Parameters.AddWithValue("id", rwId);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("select 1 from ReglamentWorks rw " +
                        "where PlanId=(select rw2.PlanId from ReglamentWorks rw2 where Id=@id)", conn);
                    cmd.Parameters.AddWithValue("id", rwId);
                    Object o = cmd.ExecuteScalar();
                    if (o == null && Done.Checked)
                    {
                        cmd = new SqlCommand("update [Plan] set Done=1 " +
                            "where Id=(select rw2.PlanId from ReglamentWorks rw2 where Id=@id)", conn);
                        cmd.Parameters.AddWithValue("id", rwId);
                        cmd.ExecuteNonQuery();
                    }
                    Response.Redirect("~/DayPlan.aspx");
                }
            }
        }
    }
}