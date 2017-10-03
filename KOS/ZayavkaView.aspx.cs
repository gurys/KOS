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
    public partial class ZayavkaView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
            {
                int zId = Int32.Parse(Request["zId"]);
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select tt.Ttx, z.LiftId, z.[Text], ui.Family as FromFamily, ui.[IO] as FromIO, " +
                        "z.Category, z.[From], z.Start, ui2.Family, ui2.[IO], z.[Status], ui3.Family as WorkerFamily, " +
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
                        Time.Text = dt.Rows[0]["Start"].ToString(); //время принятия заявки..
                        Status.Text = (bool.Parse(dt.Rows[0]["Status"].ToString()) == true ? "закрыто" : "висит");
                        if (!(dt.Rows[0]["WorkerFamily"] is DBNull))
                            Worker.Text = dt.Rows[0]["WorkerFamily"].ToString() + " " + dt.Rows[0]["WorkerIO"].ToString();
                        if (!(dt.Rows[0]["Finish"] is DBNull))
                        {
                            Finish.Text = dt.Rows[0]["Finish"].ToString();
                            Prostoy.Text = (((DateTime)dt.Rows[0]["Finish"]) - ((DateTime)dt.Rows[0]["Start"])).ToString();
                        }
                        else
                            Prostoy.Text = (DateTime.Now - ((DateTime)dt.Rows[0]["Start"])).ToString();
                        if (!(dt.Rows[0]["Couse"] is DBNull))
                            Couse.Text = dt.Rows[0]["Couse"].ToString();
                    }
                    cmd = new SqlCommand("select ui.Family, ui.IO from Notifications n " +
                        "join UserInfo ui on ui.UserId=n.UserId where n.ZayavkaId=@zId", conn);
                    cmd.Parameters.AddWithValue("zId", zId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    bool first = true;
                    while (dr.Read())
                    {
                        if (!first)
                            Sended.Text += ", ";
                        first = false;
                        Sended.Text += dr["Family"].ToString() + " " + dr["IO"].ToString();
                    }
                    dr.Close();
                }
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker", "ODS", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
    }
}