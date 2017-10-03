using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KOS
{
    public partial class ZPrimEdit : System.Web.UI.Page
    {
        int _id = 0;
        int wz =0;
        string _url = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["Id"]))
                _id = int.Parse(HttpUtility.HtmlDecode(Request["Id"]));
            if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
                _url = HttpUtility.HtmlDecode(Request["ReturnUrl"]);

            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select z.*, ui.Family, ui.IO, ui2.Family as ToFamily, ui2.IO as ToIO from ZPrim z " +
                        "join UserInfo ui on ui.UserId=z.WhoWrote " +
                        "join UserInfo ui2 on ui2.UserId=z.[To] where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", _id);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count < 1) return;
                    LiftId.Text = dt.Rows[0]["LiftId"].ToString();
                    DateAndWho.Text = ((DateTime)dt.Rows[0]["Date"]).ToShortDateString() + " " +
                        dt.Rows[0]["Family"].ToString() + " " + dt.Rows[0]["IO"].ToString();
                    To.Text = dt.Rows[0]["ToFamily"].ToString() + " " + dt.Rows[0]["ToIO"].ToString();
                    Category.Text = dt.Rows[0]["Category"].ToString();
                    Responсe.Text = dt.Rows[0]["Responce"].ToString();
                    if (!(dt.Rows[0]["Replay"] is DBNull))
                        Replay.Text = dt.Rows[0]["Replay"].ToString();
                    Done.Checked = bool.Parse(dt.Rows[0]["Done"].ToString());
                    if (!(dt.Rows[0]["WzId"] is DBNull))
                    wz = int.Parse(dt.Rows[0]["WzId"].ToString());
                    Msg.Text = string.Empty;
           //  Номер события 
                try {
                    cmd = new SqlCommand("select Id from Events where WZayavId=@wz", conn);
                    cmd.Parameters.AddWithValue("wz", wz);
                    int wzid = int.Parse(cmd.ExecuteScalar().ToString());
                    NumEvent.Text = Convert.ToString(wzid);//номер события 
                    }
                catch { NumEvent.Text = "не зарегестрировано"; } 
                }
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=" + HttpUtility.HtmlEncode(Request.Url.ToString()));
            List<string> roles = new List<string>() { "Administrator", "Manager", "Worker", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (_id>0) using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update ZPrim set Done=@done, WhoDone=(select UserId from Users where UserName=@userName), " +
                    "Replay=@t where Id=@id", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                cmd.Parameters.AddWithValue("done", Done.Checked);
                cmd.Parameters.AddWithValue("t", Replay.Text);
                cmd.Parameters.AddWithValue("id", _id);
                cmd.ExecuteNonQuery();
                Msg.Text = "Замечание добавлено";
                if (!string.IsNullOrEmpty(_url))
                    Response.Redirect(_url);
            }
            else
                Msg.Text = "Замечание не добавлено";
        }

        protected void Save_Click1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("select WZayavId from Events where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", NumEvent.Text);
                    //  int wz = int.Parse(cmd.ExecuteScalar().ToString());
                    Response.Redirect("~/WZClose.aspx?wz=" + int.Parse(cmd.ExecuteScalar().ToString()));
                }
                catch
                {
                    Msg.Text = "Нет событий для дальнейшей обработки"; 
                } 
               
            }       
        }
    }
}