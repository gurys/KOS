using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using KOS.App_Code;
using System.Data;
using System.Data.SqlClient;

namespace KOS
{
    public partial class AktWork : System.Web.UI.Page
    {
        class Data
        {
            public string Index { get; set; }
            public string LiftId { get; set; }
            public string Title { get; set; }
            public string Idi { get; set; }
            public string Url { get; set; }
            public string Address { get; set; }
            public int Id { get; set; }
            public int PlanId { get; set; }
            public string Text { get; set; }
            public string Text1 { get; set; }
        }
        class LiftId
        {
            public string _LiftId { get; set; }
        }

        DateTime _date;
       // List<Data> _data;
        string lifft = string.Empty;
        string _user, s;  
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
            if (string.IsNullOrEmpty(Request["day"]))
                _date = DateTime.Now.Date;
            else
                _date = DateTime.Parse(HttpUtility.HtmlDecode(Request["day"]));
            DateTime d2 = _date.AddDays(1);
            _user = Page.User.Identity.Name;
            if (!string.IsNullOrEmpty(Request["user"]))
                _user = HttpUtility.HtmlDecode(Request["user"]);
            AllZayavky(sender, e);
            AllZPrim(sender, e);

        }
        protected void DpZayavky(object sender, EventArgs e)
        {
            DayZayav.Visible = true; AllZayavky(sender, e);
        }
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/DayPlan.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Worker" };
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
   
        protected void AllZPrim(object sender, EventArgs e)
        {
            DayPrim.Visible = true;
          //  ReportTitle.Text = "Cобытия по закрепленным лифтам";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                List<Data> data = new List<Data>();
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lift = db.GetLiftId(_user);
                foreach (string _lift in lift)
                {

                    SqlCommand cmd = new SqlCommand();
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=0 and wz.Type!=N'ПНР/РЭО' and wz.Type=N'замечания по лифтам' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                      // "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=0 and wz.Type=N'ПНР/РЭО' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);

                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    List.DataSource = data;
                    List.DataBind();
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                List<Data> data = new List<Data>();
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lift = db.GetLiftId(_user);
                foreach (string _lift in lift)
                {

                    SqlCommand cmd = new SqlCommand();
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type!=N'ПНР/РЭО' and wz.Type=N'замечания по лифтам' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                      // "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type=N'ПНР/РЭО' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);

                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ListNA.DataSource = data;
                    ListNA.DataBind();
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                List<Data> data = new List<Data>();
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lift = db.GetLiftId(_user);
                foreach (string _lift in lift)
                {

                    SqlCommand cmd = new SqlCommand();
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=0 and wz.Type!=N'ПНР/РЭО' and wz.Type=N'запчасти и расходные материалы' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                      // "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=0 and wz.Type=N'запчасти и расходные материалы' and ui.Family=N'Саргамонов' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);

                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ListZap.DataSource = data;
                    ListZap.DataBind();
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                List<Data> data = new List<Data>();
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lift = db.GetLiftId(_user);
                foreach (string _lift in lift)
                {

                    SqlCommand cmd = new SqlCommand();
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type!=N'ПНР/РЭО' and wz.Type=N'запчасти и расходные материалы' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                      // "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type=N'запчасти и расходные материалы' and ui.Family=N'Саргамонов' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);

                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ListZapNA.DataSource = data;
                    ListZapNA.DataBind();
                }
            } using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                List<Data> data = new List<Data>();
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lift = db.GetLiftId(_user);
                foreach (string _lift in lift)
                {

                    SqlCommand cmd = new SqlCommand();
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=0 and wz.Type!=N'ПНР/РЭО' and wz.Type=N'инструменты и оборудование' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                  
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=0 and wz.Type=N'инструменты и оборудование' and ui.Family=N'Саргамонов' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);

                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ListInst.DataSource = data;
                    ListInst.DataBind();
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                List<Data> data = new List<Data>();
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lift = db.GetLiftId(_user);
                foreach (string _lift in lift)
                {

                    SqlCommand cmd = new SqlCommand();
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type!=N'ПНР/РЭО' and wz.Type=N'инструменты и оборудование' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                      // "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type=N'инструменты и оборудование' and ui.Family=N'Саргамонов' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);

                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ListInstNA.DataSource = data;
                    ListInstNA.DataBind();
                }
            }
        }
        protected void AllZayavky(object sender, EventArgs e)
        {
            DayZayav.Visible = true;
          //  Zayavky.Text = "События ОДС на " + DateTime.Now.ToShortDateString();
            List<Data> data = new List<Data>();

            if (User.Identity.Name == "Sargamonov")
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                    "join [Events] e on e.ZayavId=z.Id " +
                    "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and z.Category=N'ПНР/РЭО' and z.[Finish] is null ", conn);
                    cmd.Parameters.AddWithValue("UserName", _user);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[3].ToString(),
                            Url = "~/ZayavkaEdit.aspx?zId=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ZayavkyList.DataSource = data;
                    ZayavkyList.DataBind();
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                   "join [Events] e on e.ZayavId=z.Id " +
                   "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                   "join Users u on u.UserId=wl.UserId " +
                   "where u.UserName=@UserName and z.Category!=N'ПНР/РЭО' and z.[Finish] is null ", conn);
                    cmd.Parameters.AddWithValue("UserName", _user);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data.Add(new Data()
                        {
                            Title = " №" + " " + dr[3].ToString(),
                            Url = "~/ZayavkaEdit.aspx?zId=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ZayavkyList.DataSource = data;
                    ZayavkyList.DataBind();
                }
            }
            List<Data> data1 = new List<Data>(); // неактивные
            if (User.Identity.Name == "Sargamonov")
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                    "join [Events] e on e.ZayavId=z.Id " +
                    "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                    "join Users u on u.UserId=wl.UserId " +
                    "where u.UserName=@UserName and z.Category=N'ПНР/РЭО' and z.[Finish]=1 and z.Status=N'false' ", conn);
                    cmd.Parameters.AddWithValue("UserName", _user);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data1.Add(new Data()
                        {
                            Title = " №" + " " + dr[3].ToString(),
                            Url = "~/ZayavkaEdit.aspx?zId=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ZayavkyNA.DataSource = data1;
                    ZayavkyNA.DataBind();
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                   "join [Events] e on e.ZayavId=z.Id " +
                   "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                   "join Users u on u.UserId=wl.UserId " +
                   "where u.UserName=@UserName and z.Category!=N'ПНР/РЭО' and z.[Finish]=1 and z.Status=N'false' ", conn);
                    cmd.Parameters.AddWithValue("UserName", _user);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data1.Add(new Data()
                        {
                            Title = " №" + " " + dr[3].ToString(),
                            Url = "~/ZayavkaEdit.aspx?zId=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ZayavkyNA.DataSource = data1;
                    ZayavkyNA.DataBind();
                }
            }
        }
    }
}