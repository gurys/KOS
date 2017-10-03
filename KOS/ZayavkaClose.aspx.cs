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
    public partial class ZayavkaClose : System.Web.UI.Page
    {
        string _worker;
        string _wz; 
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO, ui.UserId from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Worker.Text = dr[0].ToString() + " " + dr[1].ToString();
                    _worker = dr[2].ToString();
                }
                dr.Close();
            }

            if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
            {
                int zId = Int32.Parse(Request["zId"]);
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select tt.Ttx, z.LiftId, z.[Text], ui.Family as FromFamily, ui.[IO] as FromIO, " +
                        "z.Category, z.[From], z.Start, ui2.Family, ui2.[IO], z.[Status], z.PrinyalDate, ui3.Family as WorkerFamily, " +
                        "ui3.[IO] as WorkerIO, z.Finish, z.Couse  from Zayavky z " +
                        "join Ttx tt on tt.Id=z.TtxId join UserInfo ui on ui.UserId=z.UserId " +
                        "left join UserInfo ui2 on ui2.UserId=z.Prinyal " +
                        "left join UserInfo ui3 on ui3.UserId=z.Worker where z.Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", zId);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Address.Text = dt.Rows[0]["Ttx"].ToString();
                        Lift.Text = dt.Rows[0]["LiftId"].ToString();
                        Text.Text = dt.Rows[0]["Text"].ToString();
                        Disp.Text = dt.Rows[0]["FromFamily"].ToString() + " " + dt.Rows[0]["FromIO"].ToString();
                        Category.Text = dt.Rows[0]["Category"].ToString();
                        From.Text = dt.Rows[0]["From"].ToString();
                        Start.Text = dt.Rows[0]["Start"].ToString();
                        if (!(dt.Rows[0]["Family"] is DBNull))
                        Prinyal.Text = dt.Rows[0]["Family"].ToString() + " " + dt.Rows[0]["IO"].ToString();
                        StartPrinyal.Text = dt.Rows[0]["PrinyalDate"].ToString();
                        Status.Text = (bool.Parse(dt.Rows[0]["Status"].ToString()) == true ? "закрыто" : "висит");
                    }

                    DateTime now = DateTime.Now;
                    Finish.Text = now.ToString();
                    Prostoy.Text = (now - ((DateTime)dt.Rows[0]["Start"])).ToString();
                }
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker", "ODS", "Cadry", "Manager" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
          //  List<string> roles = new List<string>() { "Administrator", "Worker", "ODS", "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Zayavky " +
                    "set Worker=@w, Finish=@f, [Status]=@s, Couse=@c where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                cmd.Parameters.AddWithValue("w", _worker);
                cmd.Parameters.AddWithValue("f", DateTime.Now);
                cmd.Parameters.AddWithValue("s", true);
                cmd.Parameters.AddWithValue("c", Couse.Text);
                cmd.ExecuteNonQuery();
                Status.Text = "закрыто";
                cmd = new SqlCommand("select Id from Events e" +
                " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                // обновление события 
                if (_wz != null)
                {
                    cmd = new SqlCommand("update Events " +
                     "set Who=@w, DateWho=@f, Comment=@c where Id=@i", conn);
                    cmd.Parameters.AddWithValue("f", DateTime.Now);
                    cmd.Parameters.AddWithValue("c", Couse.Text);
                    cmd.Parameters.AddWithValue("w", _worker);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("~/");
            }
        }
    }
}