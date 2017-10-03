using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using KOS.App_Code;

namespace KOS
{
    public partial class ZayavkaEditODS : System.Web.UI.Page
    {
        string _role, _prinyal, _wz;        
        class Users : Object
        {
            public string Fio { get; set; }
            public string Id { get; set; }
            public override string ToString()
            {
                return Fio;
            }
        }
        List<Users> _users = new List<Users>();

        protected void Page_Load(object sender, EventArgs e)
        {
           _role = CheckAccount();
          
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO, ui.UserId from UserInfo ui " +
                    "join UsersInRoles ur on ur.UserId=ui.UserId join Users u on u.UserId=ui.UserId where ur.RoleId='a51bcf45-c4cd-4a68-93e0-b699b3d47b02'", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Users user = new Users()
                    {
                        Fio = dr[0].ToString() + " " + dr[1].ToString(),
                        Id = dr[2].ToString()
                    };
                    _users.Add(user);
                }
                dr.Close();

                cmd = new SqlCommand("select ui.Family, ui.IO, ui.UserId from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Prinyal.Text = dr[0].ToString() + " " + dr[1].ToString();
                   _prinyal = dr[2].ToString();
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
                        StartPrinyal.Text = dt.Rows[0]["PrinyalDate"].ToString();
                        Status.Text = (bool.Parse(dt.Rows[0]["Status"].ToString()) == true ? "закрыто" : "висит");
                        if (!(dt.Rows[0]["WorkerFamily"] is DBNull))
                        Worker.Text = dt.Rows[0]["WorkerFamily"].ToString() + " " + dt.Rows[0]["WorkerIO"].ToString();
                        TimeSpan pr = DateTime.Now - ((DateTime)dt.Rows[0]["Start"]);
                        string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" + pr.Minutes.ToString();
                        Prostoy.Text = prostoy;
                        Worker.DataSource = _users;
                        Worker.DataBind();
                    }
                }
            }
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
                    "set Prinyal=@p, PrinyalDate =@d, Worker=@w where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
               // cmd.Parameters.AddWithValue("p", _prinyal);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                Users n = _users.Find(delegate(Users u)
                {
                    return u.Fio == Worker.SelectedValue;
                });
                if (n != null)
                {
                    cmd.Parameters.AddWithValue("w", n.Id);
                    cmd.Parameters.AddWithValue("p", n.Id);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("select Id from Events e" +
                 " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                // обновление события по заявке
                if (_wz != null)
                {
                    cmd = new SqlCommand("update Events " +
                     "set ToApp=@a, DateToApp=@f where Id=@i", conn);
                    cmd.Parameters.AddWithValue("f", DateTime.Now);
                    cmd.Parameters.AddWithValue("a", Worker.SelectedValue);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
            if (_role == "Worker")
                Response.Redirect("~/ZayavkaClose.aspx?zId=" + zId.ToString());
            else if (_role == "ODS")
                Response.Redirect("~/Reg_ODS.aspx");
            else if (_role == "ODS_tsg")
                Response.Redirect("~/Reg_tsg.aspx");
        }

      string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Zayavka.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            roles = new List<string>() { "ODS", "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "ODS_tsg" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS_tsg";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            Response.Redirect("~/About.aspx");
            return null;
        }
     
        protected void Text_TextChanged(object sender, EventArgs e)
        {

        }
    }
}