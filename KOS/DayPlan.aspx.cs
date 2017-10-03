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
    public partial class DayPlan : System.Web.UI.Page
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
        List<Data> _data;       
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

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());           
            _data = GetList(db.GetUnplan(_user, _date, d2));
            AddList(db.GetNotDonePrim(_user, _date, d2));          
            if (_data.Count < 1)
            {
               UnplanBlock.Visible = false;               
               DpZayavky(sender, e);
               LiftZPrim(sender, e);             
            }
           
            if (!IsPostBack)
            {
                Unplan.DataSource = _data;
                Unplan.DataBind();

                DpZayavky(sender, e);
                LiftZPrim(sender, e);
                List<Data> data = GetListDp(db.GetPlan(_user, _date, d2));
                if (data.Count < 1 && _data.Count < 1) 
                { 
                    AllZayavky(sender, e);
                    AllZPrim(sender, e);
                }               
            }
        }
        protected void DpZayavky(object sender, EventArgs e)
        {
            DayZayav.Visible = true; AllZayavky(sender, e);
        }
        protected void LiftZPrim(object sender, EventArgs e) 
        {
             DayPrim.Visible = true;
             ReportTitle.Text = "События по запланированным лифтам";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime d2 = _date.AddDays(1);
                List<Data> data1 = new List<Data>();                                  
                List<string> data = new List<string>();
                SqlCommand cmd = new SqlCommand(); 
                     cmd = new SqlCommand("select p.TpId, p.[Date], p.DateEnd, p.LiftId, t.Ttx, p.Id as PlanId, p.Done from [Plan] p " +
                    "join Users u on u.UserId=p.UserId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
                    "where u.UserName=@UserName and (p.[Date] between @d1 and @d2) " +
                    "order by p.[Date], p.DateEnd", conn);
                    cmd.Parameters.AddWithValue("UserName", _user);
                    cmd.Parameters.AddWithValue("d1", _date);
                    cmd.Parameters.AddWithValue("d2", d2);
                    SqlDataReader dn = cmd.ExecuteReader();
                    while (dn.Read()) data.Add(dn[3].ToString()); //Запланированные лифты
                    dn.Close();                    
                  foreach(string _lift in data)                        
                  {
                        if (_user != "Sargamonov")
                            s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                             //  "join Events e on e.ZayavId=wz.Id " +
                                "join UserInfo ui on ui.UserId=wz.UserId " +
                                "join Events e on e.WZayavId=wz.Id " +
                                "where wz.Done=0 and wz.Readed=0 and wz.Type!=N'ПНР/РЭО' and wz.LiftId=@userlift";
                        else if (_user == "Sargamonov")
                            s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                          //  "join Events e on e.ZayavId=wz.Id " +
                                "join UserInfo ui on ui.UserId=wz.UserId " +
                                "join Events e on e.WZayavId=wz.Id " +
                                "where wz.Done=0 and wz.Readed=0 and wz.Type=N'ПНР/РЭО' and wz.LiftId=@userlift";
                        cmd = new SqlCommand(s, conn);
                        cmd.Parameters.AddWithValue("userlift", _lift); 
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                            data1.Add(new Data()
                            {
                                Title = " №" + " " + dr[5].ToString(),
                                Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                                Text1 = dr["Text"].ToString(),
                                Idi = dr["LiftId"].ToString() 
                            });
                        dr.Close();
                        List.DataSource = data1;
                        List.DataBind();
                  }
             }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime d2 = _date.AddDays(1);
                List<Data> data1 = new List<Data>();
                List<string> data = new List<string>();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select p.TpId, p.[Date], p.DateEnd, p.LiftId, t.Ttx, p.Id as PlanId, p.Done from [Plan] p " +
               "join Users u on u.UserId=p.UserId " +
               "join LiftsTtx lt on lt.LiftId=p.LiftId " +
               "join Ttx t on lt.TtxId=t.Id and t.TtxTitleId=1 " +
               "where u.UserName=@UserName and (p.[Date] between @d1 and @d2) " +
               "order by p.[Date], p.DateEnd", conn);
                cmd.Parameters.AddWithValue("UserName", _user);
                cmd.Parameters.AddWithValue("d1", _date);
                cmd.Parameters.AddWithValue("d2", d2);
                SqlDataReader dn = cmd.ExecuteReader();
                while (dn.Read()) data.Add(dn[3].ToString()); //Запланированные лифты
                dn.Close();
                foreach (string _lift in data)
                {
                    if (_user != "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                             //  "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type!=N'ПНР/РЭО' and wz.LiftId=@userlift";
                    else if (_user == "Sargamonov")
                        s = "select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Type, wz.Readed from WorkerZayavky wz " +                          //  "join Events e on e.ZayavId=wz.Id " +
                            "join UserInfo ui on ui.UserId=wz.UserId " +
                            "join Events e on e.WZayavId=wz.Id " +
                            "where wz.Done=0 and wz.Readed=1 and wz.Type=N'ПНР/РЭО' and wz.LiftId=@userlift";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("userlift", _lift);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        data1.Add(new Data()
                        {
                            Title = " №" + " " + dr[5].ToString(),
                            Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                            Text1 = dr["Text"].ToString(),
                            Idi = dr["LiftId"].ToString()
                        });
                    dr.Close();
                    ListNA.DataSource = data1;
                    ListNA.DataBind();
                }
            }
        }
        protected void AllZPrim(object sender, EventArgs e)
        {
            DayPrim.Visible = true;
            ReportTitle.Text = "Cобытия по закрепленным лифтам";
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
                            "where wz.Done=0 and wz.Readed=0 and wz.Type!=N'ПНР/РЭО' and wz.LiftId=@userlift";
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
                            "where wz.Done=0 and wz.Readed=1 and wz.Type!=N'ПНР/РЭО' and wz.LiftId=@userlift";
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
        }
        protected void AllZayavky(object sender, EventArgs e)
        {
            DayZayav.Visible = true;
            Zayavky.Text = "События ОДС на " + DateTime.Now.ToShortDateString();
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
       void AddList(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                _data.Add(new Data()
                {
                    Index = dr["Category"].ToString(),
                    LiftId = dr["LiftId"].ToString(),
                    Url = "~/ZPrimEdit.aspx?Id=" + dr["Id"].ToString() + "&ReturnUrl=" + Request.RawUrl,
                    Text = dr["Responce"].ToString()
                });
            }
            _data.Sort(delegate(Data lr1, Data lr2)
            {
                return string.Compare(lr1.LiftId, lr2.LiftId);
            });
        } 
        
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/DayPlan.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Worker", "ODS" };
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        List<Data> GetList(DataTable dt)
        {
            List<Data> data = new List<Data>();
            foreach (DataRow dr in dt.Rows)
            {
                data.Add(new Data()
                {
                    Index = dr["Index"].ToString(),
                    LiftId = dr["LiftId"].ToString(),
                    Address = dr["Ttx"].ToString(),
                    Url = "~/Prim.aspx?rwId=" + dr["Id"].ToString(),
                    Id = (int)dr["Id"],
                    PlanId = (int)dr["PlanId"],
                    Text = dr["Prim"].ToString()
                });
            }
            return data;
        }
        List<Data> GetListDp(DataTable dt)
        {
            List<Data> data = new List<Data>();
            foreach (DataRow dr in dt.Rows)
            {
                data.Add(new Data()
                {                   
                    LiftId = dr["LiftId"].ToString(),
                    Address = dr["Ttx"].ToString(),
                    PlanId = (int)dr["PlanId"]                  
                });
            }
            return data;
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Unplan.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)Unplan.Items[i].FindControl("Done");
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update ReglamentWorks " +
                        "set [Date]=@Date, [Done]=@done, UserId=(select UserId from Users where UserName=@user) " +
                        "where Id=@rwId", conn);
                    cmd.Parameters.AddWithValue("rwId", _data[i].Id);
                    cmd.Parameters.AddWithValue("Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    cmd.Parameters.AddWithValue("done", cb.Checked ? 1 : 0);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DoPlan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("PlanId", _data[i].PlanId);
                    cmd.ExecuteNonQuery();
                }
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}