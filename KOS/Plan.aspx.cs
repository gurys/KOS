using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using KOS.App_Code;

namespace KOS
{
    public partial class Plan : System.Web.UI.Page
    {
        class TpData
        {
            public string Address { get; set; }
            public string LiftId { get; set; }
            public string IdL { get; set; }
            public string TpId { get; set; }
            public DateTime Day { get; set; }
            public string Date { get; set; }
            public string H08 { get; set; }
            public string H08url { get; set; }
            public string H09 { get; set; }
            public string H09url { get; set; }
            public string H10 { get; set; }
            public string H10url { get; set; }
            public string H11 { get; set; }
            public string H11url { get; set; }
            public string H12 { get; set; }
            public string H12url { get; set; }
            public string H13 { get; set; }
            public string H13url { get; set; }
            public string H14 { get; set; }
            public string H14url { get; set; }
            public string H15 { get; set; }
            public string H15url { get; set; }
            public string H16 { get; set; }
            public string H16url { get; set; }
            public string H17 { get; set; }
            public string H17url { get; set; }
            public string H18 { get; set; }
            public string H18url { get; set; }
            public string H19 { get; set; }
            public string H19url { get; set; }
            public string H20 { get; set; }
            public string H20url { get; set; }
        }

        class DayPlan
        {
            
            public DateTime Date { get; set; }
            public DateTime DateEnd { get; set; }
            public string TpId { get; set; }
            public string LiftId { get; set; } 
        }
        class Data
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Text1 { get; set; }
            public string Idi { get; set; }
        }

        List<TpData> _ltd = new List<TpData>();
        DataTable _monthPlan = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> ls = db.GetIdUMbyName(User.Identity.Name);
                IdUM.DataSource = ls;
                IdUM.DataBind();
                if (ls.Count > 0)
                    IdUM.SelectedIndex = 0;
                IdUM_SelectedIndexChanged(sender, e);
              // показ заявок на странице:  VseRaboty(sender, e);

            }
        }
 /*      protected void VseRaboty(object sender, EventArgs e)
        {
            Zayavky.Text = "Все работы на " + DateTime.Now.ToShortDateString();
            if (!IsPostBack)
            {
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
                        cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
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
                        cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
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
                    if (User.Identity.Name == "Sargamonov")
                    {
                        List<Data> data1 = new List<Data>(); // неактивные
                        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                            "join [Events] e on e.ZayavId=z.Id " +
                            "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                            "join Users u on u.UserId=wl.UserId " +
                            "where u.UserName=@UserName and z.Category=N'ПНР/РЭО' and z.[Finish]=1 and z.Status=N'false'", conn);
                            cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
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
                        List<Data> data1 = new List<Data>(); // неактивные
                        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                           "join [Events] e on e.ZayavId=z.Id " +
                           "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                           "join Users u on u.UserId=wl.UserId " +
                           "where u.UserName=@UserName and z.Category!=N'ПНР/РЭО' and z.[Finish]=1 and z.Status=N'false' ", conn);
                            cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
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
                        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            conn.Open();
                            List<Data> dat = new List<Data>();
                            SqlCommand cmd = new SqlCommand("select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Readed from WorkerZayavky wz " +
                                "join UserInfo ui on ui.UserId=wz.UserId " +
                                //   "join WorkerLifts wl on wl.UserId=wz.UserId " +
                                "join Users u on wz.UserId=u.UserId " +
                                "join Events e on e.WZayavId=wz.Id " +
                                "where u.UserName=@user and wz.Done=0 and wz.Readed=0", conn);
                            cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                                dat.Add(new Data()
                                {
                                    Title = Title = " №" + " " + dr[5].ToString(),
                                    Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                                    Text1 = dr["Text"].ToString(),
                                    Idi = dr["LiftId"].ToString()
                                });
                            dr.Close();
                            List.DataSource = dat;
                            List.DataBind();
                        }
                        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                        {
                            conn.Open();
                            List<Data> dat = new List<Data>();
                            SqlCommand cmd = new SqlCommand("select wz.Id, wz.UserId, wz.[Date], ui.Family, ui.IO, e.Id, wz.Done, wz.[Text], wz.[LiftId], wz.Readed from WorkerZayavky wz " +
                                "join UserInfo ui on ui.UserId=wz.UserId " +
                                //   "join WorkerLifts wl on wl.UserId=wz.UserId " +
                                "join Users u on wz.UserId=u.UserId " +
                                "join Events e on e.WZayavId=wz.Id " +
                                "where u.UserName=@user and wz.Done=0 and wz.Readed=1", conn);
                            cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                                dat.Add(new Data()
                                {
                                    Title = Title = " №" + " " + dr[5].ToString(),
                                    Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                                    Text1 = dr["Text"].ToString(),
                                    Idi = dr["LiftId"].ToString()
                                });
                            dr.Close();
                            ListNA.DataSource = dat;
                            ListNA.DataBind();
                        }
                    }
                }
            }
        }
        */
        void BindTpPlan()
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _monthPlan = db.GetPlan(User.Identity.Name, date, date.AddMonths(1).AddSeconds(-1));
            DataTable dt = db.GetTpPlan(User.Identity.Name, IdUM.SelectedValue, date, date.AddMonths(1).AddSeconds(-1));
            _ltd = new List<TpData>();
            foreach (DataRow dr in dt.Rows)
            {
                DateTime d1 = new DateTime(), d2 = new DateTime();
                if (FindLiftTp(dr["LiftId"].ToString(), dr["TpId"].ToString(), out d1, out d2))
                    date = d1;
                else date = DateTime.MinValue;
                TpData td = new TpData()
                {
                    Address = dr["Ttx"].ToString(),
                    Day = date,
                    IdL = dr["IdL"].ToString(),
                    TpId = dr["TpId"].ToString(),
                    LiftId = dr["LiftId"].ToString()
                };
                if (td.Day != DateTime.MinValue)
                {
                    td.Date = td.Day.ToString().Substring(0, 16);
                    List<DayPlan> ldp = GetDayPlan(td.Day);

                    #region MyRegion
                    string tpId = GetTp(8, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H08 = "";
                        td.H08url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 8, 0, 0).ToString()));
                    }
                    else
                        td.H08 = tpId;
                    tpId = GetTp(9, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H09 = "";
                        td.H09url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 9, 0, 0).ToString()));
                    }
                    else
                        td.H09 = tpId;
                    tpId = GetTp(10, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H10 = "";
                        td.H10url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 10, 0, 0).ToString()));
                    }
                    else
                        td.H10 = tpId;
                    tpId = GetTp(11, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H11 = "";
                        td.H11url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 11, 0, 0).ToString()));
                    }
                    else
                        td.H11 = tpId;
                    tpId = GetTp(12, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H12 = "";
                        td.H12url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 12, 0, 0).ToString()));
                    }
                    else
                        td.H12 = tpId;
                    tpId = GetTp(13, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H13 = "";
                        td.H13url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 13, 0, 0).ToString()));
                    }
                    else
                        td.H13 = tpId;
                    tpId = GetTp(14, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H14 = "";
                        td.H14url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 14, 0, 0).ToString()));
                    }
                    else
                        td.H14 = tpId;
                    tpId = GetTp(15, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H15 = "";
                        td.H15url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 15, 0, 0).ToString()));
                    }
                    else
                        td.H15 = tpId;
                    tpId = GetTp(16, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H16 = "";
                        td.H16url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 16, 0, 0).ToString()));
                    }
                    else
                        td.H16 = tpId;
                    tpId = GetTp(17, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H17 = "";
                        td.H17url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 17, 0, 0).ToString()));
                    }
                    else
                        td.H17 = tpId;
                    tpId = GetTp(18, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H18 = "";
                        td.H18url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 18, 0, 0).ToString()));
                    }
                    else
                        td.H18 = tpId;
                    tpId = GetTp(19, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H19 = "";
                        td.H19url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 19, 0, 0).ToString()));
                    }
                    else
                        td.H19 = tpId;
                    tpId = GetTp(20, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H20 = "";
                        td.H20url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(User.Identity.Name) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 20, 0, 0).ToString()));
                    }
                    else
                        td.H20 = tpId;
                    #endregion
                }
                _ltd.Add(td);
            }
        }

        List<DayPlan> GetDayPlan(DateTime date)
        {
            List<DayPlan> data = new List<DayPlan>();

            foreach (DataRow dr in _monthPlan.Rows)
            {
                DateTime d = (DateTime)dr["Date"];
                if (d.Day == date.Day)
                    data.Add(new DayPlan()
                    {                       
                        Date = d,
                        DateEnd = (DateTime)dr["DateEnd"],
                        TpId = dr["TpId"].ToString(),
                        LiftId = dr["LiftId"].ToString()
                    });
            }

            return data;
        }

        string GetTp(int h, string liftId, List<DayPlan> ldp)
        {
            DayPlan dp = ldp.Find(delegate(DayPlan t)
            {
                return h >= t.Date.Hour && h < t.DateEnd.Hour && t.LiftId == liftId;
            });
            if (dp != null)
                return dp.TpId;
            return string.Empty;
        }

        bool FindLiftTp(string liftId, string tp, out DateTime d1, out DateTime d2)
        {
            d1 = DateTime.MinValue;
            d2 = DateTime.MinValue;
            foreach (DataRow dr in _monthPlan.Rows)
            {
                if (dr["TpId"].ToString() == tp && dr["LiftId"].ToString() == liftId)
                {
                    d1 = (DateTime)dr["Date"];
                    d2 = (DateTime)dr["DateEnd"];
                    return true;
                }
            }
            return false;
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Plan.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker", "Manager" };
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void IdUM_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTpPlan();
            TpPlan.DataSource = _ltd;
            TpPlan.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            MonthPlan13.SetMonth(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), User.Identity.Name);
        }
    }
}