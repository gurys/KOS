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
using KOS.Controls;

namespace KOS
{
    public partial class Planning : System.Web.UI.Page
    {
        class LiftTp
        {
            public string LiftId { get; set; }
            public string Address { get; set; }
            public string M01 { get; set; }
            public string M02 { get; set; }
            public string M03 { get; set; }
            public string M04 { get; set; }
            public string M05 { get; set; }
            public string M06 { get; set; }
            public string M07 { get; set; }
            public string M08 { get; set; }
            public string M09 { get; set; }
            public string M10 { get; set; }
            public string M11 { get; set; }
            public string M12 { get; set; }
            public string M01url { get; set; }
            public string M02url { get; set; }
            public string M03url { get; set; }
            public string M04url { get; set; }
            public string M05url { get; set; }
            public string M06url { get; set; }
            public string M07url { get; set; }
            public string M08url { get; set; }
            public string M09url { get; set; }
            public string M10url { get; set; }
            public string M11url { get; set; }
            public string M12url { get; set; }
        }

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

        class Lift
        {
            public string Title { get; set; }
        }

        public class Works : Object
        {
            public string Title { get; set; }
            public int Id { get; set; }
            public override string ToString()
            {
                return Title;
            }
        }

        List<Base.PersonData> _workers = new List<Base.PersonData>();
        List<Works> _works = new List<Works>();
        DataTable _monthPlan = new DataTable();
        string[] _soc = new string[] { "установить", "очистить" };
        string[] _sel = new string[] { "на число", "на день недели" };
        List<TpData> _ltd = new List<TpData>();
        //string[] _saveSelDays;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _workers = db.GetWorkers();
            _works.Add(new Works() { Title = "ОС", Id = -1 }); //"ОС"
            _works.Add(new Works() { Title = "ТР", Id = -1 });
            _works.Add(new Works() { Title = "ВР", Id = -1 });
            if (!IsPostBack)
            {
                Worker.DataSource = _workers;
                Worker.DataBind();
                if (!string.IsNullOrEmpty(Request["user"]))
                {
                    string user = HttpUtility.HtmlDecode(Request["user"]);
                    Worker.SelectedValue = user;
                }
                else
                    Worker.SelectedIndex = 0;
                WorkType.DataSource = _works;
                WorkType.DataBind();
                if (!string.IsNullOrEmpty(Request["tpId"]))
                {
                    string tpId = HttpUtility.HtmlDecode(Request["tpId"]);
                    WorkType.SelectedValue = tpId;
                }
                else
                    WorkType.SelectedIndex = 0;
                SaveOrClear.DataSource = _soc;
                SaveOrClear.DataBind();
                SaveOrClear.SelectedIndex = 0;
                AddWorkSelector.DataSource = _sel;
                AddWorkSelector.DataBind();
                AddWorkSelector.SelectedIndex = 0;
                WorkPeriod.SelectedDate = DateTime.Now.Date;
                WorkPeriodEnd.SelectedDate = DateTime.Now.Date.AddMonths(1);
                WeekEndStart.SelectedDate = DateTime.Now.Date;
                WeekEndEnd.SelectedDate = DateTime.Now.Date.AddMonths(1);
                ClearStart.SelectedDate = DateTime.Now.Date;
                ClearEnd.SelectedDate = DateTime.Now.Date.AddMonths(1);
                LiftsPlan.DataSource = GetPlan();
                LiftsPlan.DataBind();
                Year.Text = DateTime.Now.Year.ToString();
                Month.DataSource = KOS.App_Code.Base.months;
                Month.DataBind();
                Month.SelectedIndex = DateTime.Now.Month - 1;
                WorkType_SelectedIndexChanged(sender, e);
                List<string> days = new List<string>();
                for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Year, Month.SelectedIndex + 1); i++)
                    days.Add((i + 1).ToString());
                ddlDate.DataSource = days;
                ddlDate.DataBind();
            }
            else
                SaveTp();
            if (SaveOrClear.SelectedIndex == 1)
                _works.Add(new Works() { Title = "всё", Id = 0 });
        }

        void SaveTp()
        {
            if (string.IsNullOrEmpty(Request["date"]) || string.IsNullOrEmpty(Request["liftId"]) ||
                string.IsNullOrEmpty(Request["tpId"]) || string.IsNullOrEmpty(Request["user"]))
                return;
            DateTime d1 = DateTime.Parse(HttpUtility.HtmlDecode(Request["date"]));
            DateTime d4 = d1.AddHours(1);
            string liftId = HttpUtility.HtmlDecode(Request["liftId"]);
            string tpId = HttpUtility.HtmlDecode(Request["tpId"]);
            string userName = HttpUtility.HtmlDecode(Request["user"]);

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            DataTable dt = db.GetPlan(userName, d1, d1);
            if (dt.Rows.Count > 0)
            {
                Msg.Text = "Это время занято";
                return;
            }
            dt = db.GetPlan(userName, new DateTime(d1.Year, d1.Month, d1.Day),
                (new DateTime(d1.Year, d1.Month, d1.Day)).AddDays(1));
            int planId = -1;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["TpId"].ToString() == tpId && dr["LiftId"].ToString() == liftId)
                {
                    DateTime d2 = (DateTime)dr["Date"];
                    DateTime d3 = (DateTime)dr["DateEnd"];
                    if (d1 >= d3)
                    {
                        DataTable dt2 = db.GetPlan(userName, d3.AddHours(1), d1);
                        if (dt2.Rows.Count < 1)
                        {
                            planId = (int)dr["PlanId"];
                            d4 = d1.AddHours(1);
                            d1 = d2;
                        }
                        break;
                    }
                    else if (d1 < d2)
                    {
                        DataTable dt2 = db.GetPlan(userName, d1, d2.AddHours(-1));
                        if (dt2.Rows.Count < 1)
                        {
                            planId = (int)dr["PlanId"];
                            d4 = d3;
                        }
                        break;
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("", conn);
                if (planId < 1)
                {
                    cmd = new SqlCommand("insert into [Plan] " +
                        "(UserId, LiftId, TpId, [Date], DateEnd) " +
                        "values ((select UserId from Users where UserName=@userName), @liftId, @tpId, @date, @dateEnd)", conn);
                    cmd.Parameters.AddWithValue("userName", userName);
                    cmd.Parameters.AddWithValue("liftId", liftId);                    
                    cmd.Parameters.AddWithValue("tpId", tpId);
                    cmd.Parameters.AddWithValue("date", d1);
                    cmd.Parameters.AddWithValue("dateEnd", d4);
                }
                else
                {
                    cmd = new SqlCommand("update [Plan] " +
                        "set UserId=(select UserId from Users where UserName=@userName), LiftId=@liftId, TpId=@tpId, [Date]=@date, DateEnd=@dateEnd " +
                        "where Id=@planId", conn);
                    cmd.Parameters.AddWithValue("userName", userName);
                    cmd.Parameters.AddWithValue("liftId", liftId);                   
                    cmd.Parameters.AddWithValue("tpId", tpId); //tpId
                    cmd.Parameters.AddWithValue("date", d1);
                    cmd.Parameters.AddWithValue("dateEnd", d4);
                    cmd.Parameters.AddWithValue("planId", planId);
                }
                cmd.ExecuteNonQuery();
                Msg.Text = string.Empty;
                Month_SelectedIndexChanged(this, EventArgs.Empty);
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.UserName == userName;
                });
                Response.Redirect("~/Planning.aspx?user=" + HttpUtility.HtmlEncode(pd.Title) + "&tpId=" +
                    HttpUtility.HtmlEncode(tpId));
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            MonthPlan1.SetMonth(new DateTime(DateTime.Now.Year, 1, 1), pd.UserName);
            MonthPlan2.SetMonth(new DateTime(DateTime.Now.Year, 2, 1), pd.UserName);
            MonthPlan3.SetMonth(new DateTime(DateTime.Now.Year, 3, 1), pd.UserName);
            MonthPlan4.SetMonth(new DateTime(DateTime.Now.Year, 4, 1), pd.UserName);
            MonthPlan5.SetMonth(new DateTime(DateTime.Now.Year, 5, 1), pd.UserName);
            MonthPlan6.SetMonth(new DateTime(DateTime.Now.Year, 6, 1), pd.UserName);
            MonthPlan7.SetMonth(new DateTime(DateTime.Now.Year, 7, 1), pd.UserName);
            MonthPlan8.SetMonth(new DateTime(DateTime.Now.Year, 8, 1), pd.UserName);
            MonthPlan9.SetMonth(new DateTime(DateTime.Now.Year, 9, 1), pd.UserName);
            MonthPlan10.SetMonth(new DateTime(DateTime.Now.Year, 10, 1), pd.UserName);
            MonthPlan11.SetMonth(new DateTime(DateTime.Now.Year, 11, 1), pd.UserName);
            MonthPlan12.SetMonth(new DateTime(DateTime.Now.Year, 12, 1), pd.UserName);
            MonthPlan13.SetMonth(new DateTime(DateTime.Now.Year, Month.SelectedIndex + 1, 1), pd.UserName);
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Planning.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "ODS" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void WorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Works w = _works.Find(delegate(Works i)
            {
                return i.Title == WorkType.SelectedValue;
            });
            if (w != null)
            {
                if (w.Id < 0)
                {
                    if (w.Title != "ОС")
                    {
                        if (SaveOrClear.SelectedIndex == 0)
                        {
                            phSave.Visible = true;
                            phClear.Visible = false;
                        }
                        else
                        {
                            phSave.Visible = false;
                            phClear.Visible = true;
                        }
                        phTp.Visible = true;
                        phWork.Visible = false;
                        phHollidayDaysOfWeek.Visible = false;
                        phHollidayPeriod.Visible = false;
                        Month_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        if (SaveOrClear.SelectedIndex == 0)
                        {
                            phSave.Visible = true;
                            phClear.Visible = false;
                        }
                        else
                        {
                            phSave.Visible = false;
                            phClear.Visible = true;
                        }
                        phWork.Visible = true;
                        phTp.Visible = false;
                        phHollidayDaysOfWeek.Visible = false;
                        phHollidayPeriod.Visible = false;
                    }
                    Worker_SelectedIndexChanged(sender, e);
                }
                else if (w.Id == 1)
                {
                    if (SaveOrClear.SelectedIndex == 0)
                    {
                        phSave.Visible = true;
                        phClear.Visible = false;
                    }
                    else
                    {
                        phSave.Visible = false;
                        phClear.Visible = true;
                    }
                    phWork.Visible = false;
                    phTp.Visible = false;
                    phHollidayDaysOfWeek.Visible = true;
                    phHollidayPeriod.Visible = false;
                }
                else if (w.Id == 0)
                {
                    phClear.Visible = true;
                    phSave.Visible = false;
                    phWork.Visible = false;
                    phTp.Visible = false;
                    phHollidayPeriod.Visible = false;
                    phHollidayDaysOfWeek.Visible = false;
                }
                else
                {
                    if (SaveOrClear.SelectedIndex == 0)
                    {
                        phSave.Visible = true;
                        phClear.Visible = false;
                    }
                    else
                    {
                        phSave.Visible = false;
                        phClear.Visible = true;
                    }
                    phWork.Visible = false;
                    phTp.Visible = false;
                    phHollidayDaysOfWeek.Visible = false;
                    phHollidayPeriod.Visible = true;
                }
            }
            Msg.Text = "";
        }

        protected void IdM_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<Base.CheckedLift> data = db.GetCheckedLiftId(pd.Id, IdU.SelectedValue, IdM.SelectedValue);
            IdL.DataSource = data;
            IdL.DataBind();
        }

        protected void IdU_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd != null)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> ls = db.GetIdM(IdU.SelectedValue);
                IdM.DataSource = ls;
                IdM.DataBind();
                if (ls.Count > 0)
                    IdM.SelectedIndex = 0;
                IdM_SelectedIndexChanged(sender, e);
            }
        }

        protected void Worker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd != null)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> ls = db.GetIdU();
                IdU.DataSource = ls;
                IdU.DataBind();
                if (ls.Count > 0)
                    IdU.SelectedIndex = 0;
                IdU_SelectedIndexChanged(sender, e);
                IdU2.DataSource = ls;
                IdU2.DataBind();
                if (ls.Count > 0)
                    IdU2.SelectedIndex = 0;
                IdU2_SelectedIndexChanged(sender, e);
                Fio.Text = pd.Title;
                ls = db.GetIdUM(pd.Id);
                IdUM.DataSource = ls;
                IdUM.DataBind();
                if (ls.Count > 0)
                    IdUM.SelectedIndex = 0;
                /*     ls = db.GetIdUM();//все лифты с закрепленными
                      IdUM.DataSource = ls;
                      IdUM.DataBind();
                      if (ls.Count > 0)
                      {
                          ls = db.GetIdUM(pd.Id);
                          if (ls.Count > 0)
                              IdUM.SelectedValue = ls[0];
                          else
                              IdUM.SelectedIndex = 0;
                      } */
               
                Month_SelectedIndexChanged(sender, e);
                BindLiftId();
                Base.UserInfo ui = db.GetUserInfo(pd.UserName);
                WorkFrom.Text = ui.HourBeg.ToString();
                WorkTo.Text = ui.HourEnd.ToString();
                WorkLunch.Text = ui.Lunch.ToString();
            }
            LiftsPlan.DataSource = GetPlan();
            LiftsPlan.DataBind();
            Msg.Text = "";
        }

        protected void SaveOrClear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveOrClear.SelectedIndex == 0)
            {
                phSave.Visible = true;
                phClear.Visible = false;
            }
            else
            {
                phSave.Visible = false;
                phClear.Visible = true;
                //_works.Add(new Base.Works() { Title = "всё", Id = 0 });
            }
            WorkType.DataSource = _works;
            WorkType.DataBind();
            Month_SelectedIndexChanged(sender, e);
            WorkType_SelectedIndexChanged(sender, e);
        }

        List<string> GetSelectedTitles(ListView lv)
        {
            List<string> sel = new List<string>();
            foreach (ListViewItem item in lv.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("Select");
                if (cb.Checked)
                {
                    Label lb = (Label)item.FindControl("Title");
                    sel.Add(lb.Text);
                }
            }
            return sel;
        }

        protected void SaveWork_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения!";
                return;
            }
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Base.UserInfo ui = db.GetUserInfo(pd.UserName);

            int t = 0, d = 0;
            if (!int.TryParse(WorkTime.Text, out t) || !int.TryParse(WorkDuration.Text, out d))
            {
                Msg.Text = "Укажите время начала и/или конца работ!";
                return;
            }
            DateTime date = WorkDay.SelectedDate;
            if (date == DateTime.MinValue)
            {
                Msg.Text = "Укажите дату работ!";
                return;
            }
            if (db.IsHolliday(pd.UserName, date))
            {
                Msg.Text = "Это не рабочий день!";
                return;
            }
            List<MonthPlan.Data> data = new List<MonthPlan.Data>();
            switch (date.Month)
            {
                case 1: MonthPlan1.SetMonth(new DateTime(DateTime.Now.Year, 1, 1), pd.UserName);
                    data = MonthPlan1.GetDayPlan(date.Day); break;
                case 2: MonthPlan2.SetMonth(new DateTime(DateTime.Now.Year, 2, 1), pd.UserName);
                    data = MonthPlan2.GetDayPlan(date.Day); break;
                case 3: MonthPlan3.SetMonth(new DateTime(DateTime.Now.Year, 3, 1), pd.UserName);
                    data = MonthPlan3.GetDayPlan(date.Day); break;
                case 4: MonthPlan4.SetMonth(new DateTime(DateTime.Now.Year, 4, 1), pd.UserName);
                    data = MonthPlan4.GetDayPlan(date.Day); break;
                case 5: MonthPlan5.SetMonth(new DateTime(DateTime.Now.Year, 5, 1), pd.UserName);
                    data = MonthPlan5.GetDayPlan(date.Day); break;
                case 6: MonthPlan6.SetMonth(new DateTime(DateTime.Now.Year, 6, 1), pd.UserName);
                    data = MonthPlan6.GetDayPlan(date.Day); break;
                case 7: MonthPlan7.SetMonth(new DateTime(DateTime.Now.Year, 7, 1), pd.UserName);
                    data = MonthPlan7.GetDayPlan(date.Day); break;
                case 8: MonthPlan8.SetMonth(new DateTime(DateTime.Now.Year, 8, 1), pd.UserName);
                    data = MonthPlan8.GetDayPlan(date.Day); break;
                case 9: MonthPlan9.SetMonth(new DateTime(DateTime.Now.Year, 9, 1), pd.UserName);
                    data = MonthPlan9.GetDayPlan(date.Day); break;
                case 10: MonthPlan10.SetMonth(new DateTime(DateTime.Now.Year, 10, 1), pd.UserName);
                    data = MonthPlan10.GetDayPlan(date.Day); break;
                case 11: MonthPlan11.SetMonth(new DateTime(DateTime.Now.Year, 11, 1), pd.UserName);
                    data = MonthPlan11.GetDayPlan(date.Day); break;
                case 12: MonthPlan12.SetMonth(new DateTime(DateTime.Now.Year, 12, 1), pd.UserName);
                    data = MonthPlan12.GetDayPlan(date.Day); break;
            }
            List<string> lifts = GetSelectedTitles(IdL);
            bool IsOk = true;
            foreach (string lift in lifts)
            {
                foreach (MonthPlan.Data i in data)
                {
                    if (t > 0 && !(WorkType.SelectedValue == "ОС" && i.TpId == WorkType.SelectedValue && i.LiftId != lift))
                    {
                        if (t >= i.DateBeg.Hour && t <= i.DateEnd.Hour)
                        {
                            IsOk = false;
                            break;
                        }
                        if (d > 0)
                        {
                            if (d > i.DateBeg.Hour && d <= i.DateEnd.Hour)
                            {
                                IsOk = false;
                                break;
                            }
                            if (t <= i.DateBeg.Hour && d >= i.DateEnd.Hour)
                            {
                                IsOk = false;
                                break;
                            }
                        }
                    }
                    if (i.LiftId == lift && WorkType.SelectedValue != "ВР") // для ВР
                    {
                        Msg.Text = "Для лифта " + lift + " в этот день уже проводятся работы!";
                        return;
                    }
                }
                if (!IsOk) break;
            }
            if (!IsOk)
            {
                Msg.Text = "Это время занято!";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                foreach (string lift in lifts)
                {
                    SqlCommand cmd = new SqlCommand("insert into [Plan] " +
                        "(UserId, LiftId, TpId, [Date], DateEnd) " +
                        "values (@userId, @liftId, @tpId, @date, @dateEnd)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("liftId", lift);
                    cmd.Parameters.AddWithValue("tpId", WorkType.SelectedValue);
                    date = new DateTime(date.Year, date.Month, date.Day, t, 0, 0);
                    cmd.Parameters.AddWithValue("date", date);
                    date = new DateTime(date.Year, date.Month, date.Day, d, 0, 0);
                    cmd.Parameters.AddWithValue("dateEnd", date);
                    cmd.ExecuteNonQuery();
                }
                Msg.Text = string.Empty;
            }
        }

        protected void SaveWeekEnd_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения SaveWeekEnd!";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                for (DateTime date = WeekEndStart.SelectedDate; date < WeekEndEnd.SelectedDate.AddDays(1);
                    date = date.AddDays(1))
                {
                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Monday: if (!HollidayDaysOfWeek.Items[0].Selected) continue; break;
                        case DayOfWeek.Tuesday: if (!HollidayDaysOfWeek.Items[1].Selected) continue; break;
                        case DayOfWeek.Wednesday: if (!HollidayDaysOfWeek.Items[2].Selected) continue; break;
                        case DayOfWeek.Thursday: if (!HollidayDaysOfWeek.Items[3].Selected) continue; break;
                        case DayOfWeek.Friday: if (!HollidayDaysOfWeek.Items[4].Selected) continue; break;
                        case DayOfWeek.Saturday: if (!HollidayDaysOfWeek.Items[5].Selected) continue; break;
                        case DayOfWeek.Sunday: if (!HollidayDaysOfWeek.Items[6].Selected) continue; break;
                    }
                    SqlCommand cmd = new SqlCommand("select * from Holidays where UserId=@userId and [Date]=@date", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("date", date);
                    object o = cmd.ExecuteScalar();
                    if (o == null)
                    {
                        cmd = new SqlCommand("insert into Holidays " +
                            "(UserId, [Date], HollidaysTypeId) " +
                            "values (@userId, @date, 1) ", conn);
                        cmd.Parameters.AddWithValue("userId", pd.Id);
                        cmd.Parameters.AddWithValue("date", date);
                        cmd.ExecuteNonQuery();
                    }
                    Msg.Text = string.Empty;
                }
            }
        }

        protected void HollidayPeriodSave_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения HollidayPeriodSave!";
                return;
            }
            Works bw = _works.Find(delegate(Works i)
            {
                return i.Title == WorkType.SelectedValue;
            });
            if (bw == null || bw.Id < 1)
            {
                Msg.Text = "Ошибка сохранения HollidayPeriodSave !!";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from Holidays where " +
                    "UserId=@userId and [Date] between @d1 and @d2", conn);
                cmd.Parameters.AddWithValue("userId", pd.Id);
                cmd.Parameters.AddWithValue("d1", HollidayStart.SelectedDate);
                cmd.Parameters.AddWithValue("d2", HollidayEnd.SelectedDate.AddDays(1).AddSeconds(-1));
                cmd.ExecuteNonQuery();
                for (DateTime date = HollidayStart.SelectedDate; date < HollidayEnd.SelectedDate.AddDays(1);
                    date = date.AddDays(1))
                {
                    cmd = new SqlCommand("insert into Holidays " +
                        "(UserId, [Date], HollidaysTypeId) " +
                        "values (@userId, @date, @type)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("type", bw.Id);
                    cmd.ExecuteNonQuery();
                    Msg.Text = string.Empty;
                }
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения Clear!";
                return;
            }
            Works bw = _works.Find(delegate(Works i)
            {
                if (WorkType.SelectedValue != "ТР")
                    return i.Title == WorkType.SelectedValue;
                else
                    return false;
            });
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                if (bw != null && bw.Id < 0)
                {
                    SqlCommand cmd = new SqlCommand("delete from [ReglamentWorks] " +
                        "where PlanId in (select Id from [Plan] where [Date] between @d1 and @d2 and UserId=@userId and TpId=@tpId)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("tpId", bw.Title);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("delete from [Plan] " +
                        "where UserId=@userId and TpId=@tpId and [Date] between @d1 and @d2 " +
                        "and not exists(select PlanId from ReglamentWorks where PlanId=[Plan].Id)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("tpId", bw.Title);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    int i = cmd.ExecuteNonQuery();
                }
                else if (bw != null && bw.Id == 0)
                {
                    SqlCommand cmd = new SqlCommand("delete from [ReglamentWorks] " +
                        "where PlanId in (select Id from [Plan] where [Date] between @d1 and @d2 and UserId=@userId)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("delete from [Plan] " +
                        "where UserId=@userId and [Date] between @d1 and @d2 " +
                        "and not exists(select PlanId from ReglamentWorks where PlanId=[Plan].Id)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    int i = cmd.ExecuteNonQuery();
                }
                else if (bw != null)
                {
                    SqlCommand cmd = new SqlCommand("delete from Holidays " +
                        "where UserId=@userId and HollidaysTypeId=@type and " +
                        "[Date] between @d1 and @d2", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("type", bw.Id);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("delete from [ReglamentWorks] " +
                        "where PlanId in (select Id from [Plan] where [Date] between @d1 and @d2 and UserId=@userId and TpId<>N'ОС')", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("delete from [Plan] " +
                        "where UserId=@userId and TpId<>N'ОС' and [Date] between @d1 and @d2 " +
                        "and not exists(select PlanId from ReglamentWorks where PlanId=[Plan].Id)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                    cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                    int i = cmd.ExecuteNonQuery();
                }
                Msg.Text = string.Empty;
            }
        }

        protected void AddWorkSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddWorkSelector.SelectedIndex == 1)
            {
                phWork1.Visible = false;
                phWork2.Visible = true;
            }
            else
            {
                phWork1.Visible = true;
                phWork2.Visible = false;
            }
        }

        protected void SaveWorkPeriod_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения SaveWorkPeriod!";
                return;
            }
            int t = 0, d = 0;
            if (!int.TryParse(WorkTime2.Text, out t) || !int.TryParse(WorkDuration2.Text, out d))
            {
                Msg.Text = "Укажите время начала и/или конца работ!";
                return;
            }
            // работы сохраняются без проверки на занятость планируемого времени!!!
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [Plan] " +
                    "(UserId, LiftId, TpId, [Date], DateEnd) " +
                    "values (@userId, @liftId, @tpId, @date, @dateEnd)", conn);
                for (DateTime date = WorkPeriod.SelectedDate; date < WorkPeriodEnd.SelectedDate; date = date.AddDays(1))
                {
                    List<string> liftIds = GetSelectedTitles(IdL2);
                    foreach (string liftId in liftIds)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("userId", pd.Id);
                        cmd.Parameters.AddWithValue("liftId", liftId);
                        cmd.Parameters.AddWithValue("tpId", WorkType.SelectedValue); // WorkType.SelectedValue
                        switch (date.DayOfWeek)
                        {
                            case DayOfWeek.Monday: if (!WorkDayOfWeek.Items[0].Selected) continue; break;
                            case DayOfWeek.Tuesday: if (!WorkDayOfWeek.Items[1].Selected) continue; break;
                            case DayOfWeek.Wednesday: if (!WorkDayOfWeek.Items[2].Selected) continue; break;
                            case DayOfWeek.Thursday: if (!WorkDayOfWeek.Items[3].Selected) continue; break;
                            case DayOfWeek.Friday: if (!WorkDayOfWeek.Items[4].Selected) continue; break;
                            case DayOfWeek.Saturday: if (!WorkDayOfWeek.Items[5].Selected) continue; break;
                            case DayOfWeek.Sunday: if (!WorkDayOfWeek.Items[6].Selected) continue; break;
                        }
                        cmd.Parameters.AddWithValue("date", new DateTime(date.Year, date.Month, date.Day, t, 0, 0));
                        cmd.Parameters.AddWithValue("dateEnd", new DateTime(date.Year, date.Month, date.Day, d, 0, 0));
                        cmd.ExecuteNonQuery();
                    }
                }
                Msg.Text = string.Empty;
            }
        }

        protected void IdU2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd != null)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> ls = db.GetIdM(pd.Id, IdU2.SelectedValue);
                IdM2.DataSource = ls;
                IdM2.DataBind();
                if (ls.Count > 0)
                    IdM2.SelectedIndex = 0;
                IdM2_SelectedIndexChanged(sender, e);
            }
        }

        protected void IdM2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd != null)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> ls = db.GetLiftId(pd.Id, IdU2.SelectedValue, IdM2.SelectedValue);
                List<Lift> data = new List<Lift>();
                foreach (string s in ls)
                    data.Add(new Lift()
                    {
                        Title = s
                    });
                IdL2.DataSource = data;
                IdL2.DataBind();
            }
        }

        List<LiftTp> GetPlan()
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
                return null;
            List<LiftTp> data = new List<LiftTp>();
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            DataTable dt = db.GetWorks(pd.Id);
            string liftId = string.Empty, m01 = string.Empty, m02 = string.Empty, m03 = string.Empty, m04 = string.Empty, m05 = string.Empty, m06 = string.Empty, m07 = string.Empty, m08 = string.Empty, m09 = string.Empty, m10 = string.Empty, m11 = string.Empty, m12 = string.Empty;
            string m01url = string.Empty, m02url = string.Empty, m03url = string.Empty, m04url = string.Empty, m05url = string.Empty, m06url = string.Empty, m07url = string.Empty, m08url = string.Empty, m09url = string.Empty, m10url = string.Empty, m11url = string.Empty, m12url = string.Empty;
            string address = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["LiftId"].ToString() != liftId)
                {
                    if (!string.IsNullOrEmpty(liftId))
                        data.Add(new LiftTp()
                        {
                            LiftId = liftId,
                            Address = address,
                            M01 = m01,
                            M02 = m02,
                            M03 = m03,
                            M04 = m04,
                            M05 = m05,
                            M06 = m06,
                            M07 = m07,
                            M08 = m08,
                            M09 = m09,
                            M10 = m10,
                            M11 = m11,
                            M12 = m12,
                            M01url = m01url,
                            M02url = m02url,
                            M03url = m03url,
                            M04url = m04url,
                            M05url = m05url,
                            M06url = m06url,
                            M07url = m07url,
                            M08url = m08url,
                            M09url = m09url,
                            M10url = m10url,
                            M11url = m11url,
                            M12url = m12url
                        });
                    liftId = dr["LiftId"].ToString();
                    address = dr["Ttx"].ToString();
                    m01 = string.Empty; m02 = string.Empty; m03 = string.Empty; m04 = string.Empty;
                    m05 = string.Empty; m06 = string.Empty; m07 = string.Empty; m08 = string.Empty; m09 = string.Empty;
                    m10 = string.Empty; m11 = string.Empty; m12 = string.Empty;
                    m01url = string.Empty; m02url = string.Empty; m03url = string.Empty; m04url = string.Empty;
                    m05url = string.Empty; m06url = string.Empty; m07url = string.Empty; m08url = string.Empty; m09url = string.Empty;
                    m10url = string.Empty; m11url = string.Empty; m12url = string.Empty;
                }
                if (((DateTime)dr["Date"]).Year == DateTime.Now.Year) switch (((DateTime)dr["Date"]).Month)
                    {
                        case 1: if (string.IsNullOrEmpty(m01)) m01 = dr["TpId"].ToString(); else m01 += ", " + dr["TpId"].ToString();
                            m01url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 2: if (string.IsNullOrEmpty(m02)) m02 = dr["TpId"].ToString(); else m02 += ", " + dr["TpId"].ToString();
                            m02url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 3: if (string.IsNullOrEmpty(m03)) m03 = dr["TpId"].ToString(); else m03 += ", " + dr["TpId"].ToString();
                            m03url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 4: if (string.IsNullOrEmpty(m04)) m04 = dr["TpId"].ToString(); else m04 += ", " + dr["TpId"].ToString();
                            m04url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 5: if (string.IsNullOrEmpty(m05)) m05 = dr["TpId"].ToString(); else m05 += ", " + dr["TpId"].ToString();
                            m05url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 6: if (string.IsNullOrEmpty(m06)) m06 = dr["TpId"].ToString(); else m06 += ", " + dr["TpId"].ToString();
                            m06url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 7: if (string.IsNullOrEmpty(m07)) m07 = dr["TpId"].ToString(); else m07 += ", " + dr["TpId"].ToString();
                            m07url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 8: if (string.IsNullOrEmpty(m08)) m08 = dr["TpId"].ToString(); else m08 += ", " + dr["TpId"].ToString();
                            m08url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 9: if (string.IsNullOrEmpty(m09)) m09 = dr["TpId"].ToString(); else m09 += ", " + dr["TpId"].ToString();
                            m09url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 10: if (string.IsNullOrEmpty(m10)) m10 = dr["TpId"].ToString(); else m10 += ", " + dr["TpId"].ToString();
                            m10url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 11: if (string.IsNullOrEmpty(m11)) m11 = dr["TpId"].ToString(); else m11 += ", " + dr["TpId"].ToString();
                            m11url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                        case 12: if (string.IsNullOrEmpty(m12)) m12 = dr["TpId"].ToString(); else m12 += ", " + dr["TpId"].ToString();
                            m12url = "~/DayPlan.aspx?user=" + HttpUtility.HtmlEncode(pd.UserName) + "&day=" +
                                HttpUtility.HtmlEncode(((DateTime)dr["Date"]).ToShortDateString());
                            break;
                    }
            }
            if (!string.IsNullOrEmpty(liftId))
                data.Add(new LiftTp()
                {
                    LiftId = liftId,
                    Address = address,
                    M01 = m01,
                    M02 = m02,
                    M03 = m03,
                    M04 = m04,
                    M05 = m05,
                    M06 = m06,
                    M07 = m07,
                    M08 = m08,
                    M09 = m09,
                    M10 = m10,
                    M11 = m11,
                    M12 = m12,
                    M01url = m01url,
                    M02url = m02url,
                    M03url = m03url,
                    M04url = m04url,
                    M05url = m05url,
                    M06url = m06url,
                    M07url = m07url,
                    M08url = m08url,
                    M09url = m09url,
                    M10url = m10url,
                    M11url = m11url,
                    M12url = m12url
                });
            return data;
        }

        protected void IdUM_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTpPlan();
            TpPlan.DataSource = _ltd;
            TpPlan.DataBind();
            BindDays();
            BindLiftId();
        }

        void BindLiftId()
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string[] id = IdUM.SelectedValue.Split('/');
            List<string> liftId = db.GetLiftId(id[0], id[1]);
            ddlLiftId.DataSource = liftId;
            ddlLiftId.DataBind();
            if (liftId.Count > 0)
                ddlLiftId.SelectedIndex = 0;
        }

        class DayPlan
        {
            public DateTime Date { get; set; }
            public DateTime DateEnd { get; set; }
            public string TpId { get; set; }
            public string LiftId { get; set; }
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

        void BindDays()
        {/*
            List<string> days = new List<string>();
            days.Add("");
            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Year, Month.SelectedIndex + 1); i++)
                days.Add((i + 1).ToString());
            for (int i = 0; i < TpPlan.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)TpPlan.Items[i].FindControl("Day");
                ddl.DataSource = days;
                ddl.DataBind();
                if (_ltd[i].Day != DateTime.MinValue)
                    ddl.SelectedIndex = _ltd[i].Day.Day;
                else if (_saveSelDays!=null && _saveSelDays.Length == TpPlan.Items.Count)
                    ddl.SelectedValue = _saveSelDays[i];
                else
                    ddl.SelectedIndex = 0;
            }*/
        }

        void BindTpPlan()
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (Month.SelectedIndex < 0)
            {
                Month.DataSource = KOS.App_Code.Base.months;
                Month.DataBind();
                Month.SelectedIndex = DateTime.Now.Month - 1;
            }
            DateTime date = new DateTime(DateTime.Now.Year, Month.SelectedIndex + 1, 1);
            MonthPlan13.SetMonth(date, pd.UserName);

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _monthPlan = db.GetPlan(pd.UserName, date, date.AddMonths(1).AddSeconds(-1));
            DataTable dt = db.GetTpPlan(pd.UserName, IdUM.SelectedValue, date, date.AddMonths(1).AddSeconds(-1));
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
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 8, 0, 0).ToString()));
                    }
                    else
                        td.H08 = tpId;
                    tpId = GetTp(9, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H09 = "";
                        td.H09url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 9, 0, 0).ToString()));
                    }
                    else
                        td.H09 = tpId;
                    tpId = GetTp(10, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H10 = "";
                        td.H10url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 10, 0, 0).ToString()));
                    }
                    else
                        td.H10 = tpId;
                    tpId = GetTp(11, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H11 = "";
                        td.H11url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 11, 0, 0).ToString()));
                    }
                    else
                        td.H11 = tpId;
                    tpId = GetTp(12, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H12 = "";
                        td.H12url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 12, 0, 0).ToString()));
                    }
                    else
                        td.H12 = tpId;
                    tpId = GetTp(13, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H13 = "";
                        td.H13url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 13, 0, 0).ToString()));
                    }
                    else
                        td.H13 = tpId;
                    tpId = GetTp(14, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H14 = "";
                        td.H14url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 14, 0, 0).ToString()));
                    }
                    else
                        td.H14 = tpId;
                    tpId = GetTp(15, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H15 = "";
                        td.H15url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 15, 0, 0).ToString()));
                    }
                    else
                        td.H15 = tpId;
                    tpId = GetTp(16, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H16 = "";
                        td.H16url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 16, 0, 0).ToString()));
                    }
                    else
                        td.H16 = tpId;
                    tpId = GetTp(17, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H17 = "";
                        td.H17url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 17, 0, 0).ToString()));
                    }
                    else
                        td.H17 = tpId;
                    tpId = GetTp(18, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H18 = "";
                        td.H18url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 18, 0, 0).ToString()));
                    }
                    else
                        td.H18 = tpId;
                    tpId = GetTp(19, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H19 = "";
                        td.H19url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 19, 0, 0).ToString()));
                    }
                    else
                        td.H19 = tpId;
                    tpId = GetTp(20, td.LiftId, ldp);
                    if (string.IsNullOrEmpty(tpId))
                    {
                        td.H20 = "";
                        td.H20url = "~/Planning.aspx?liftId=" + HttpUtility.HtmlEncode(td.LiftId) +
                            "&user=" + HttpUtility.HtmlEncode(pd.UserName) + "&tpId=" + HttpUtility.HtmlEncode(td.TpId) + "&date=" +
                            HttpUtility.HtmlEncode((new DateTime(td.Day.Year, td.Day.Month, td.Day.Day, 20, 0, 0).ToString()));
                    }
                    else
                        td.H20 = tpId;
                    #endregion
                }
                _ltd.Add(td);
            }
        }

        protected void Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTpPlan();
            TpPlan.DataSource = _ltd;
            TpPlan.DataBind();
            BindDays();
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

        void SaveSelDays()
        {/*
            _saveSelDays = new string[TpPlan.Items.Count];
            for (int i = 0; i < TpPlan.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)TpPlan.Items[i].FindControl("Day");
                _saveSelDays[i] = ddl.SelectedValue;
            }*/
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == Worker.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения btnSave!";
                return;
            }
            int t = 0, d = 0;
            if (!int.TryParse(t1.Text, out t) || !int.TryParse(t2.Text, out d))
            {
                Msg.Text = "Укажите время начала и/или конца работ!";
                return;
            }
            BindTpPlan();
            TpData tpData = _ltd.Find(delegate(TpData m)
            {
                return m.LiftId == ddlLiftId.SelectedValue;
            });
            if (tpData == null)
            {
                Msg.Text = "Для этого лифта нет работы!";
                return;
            }
            DateTime d1 = new DateTime(DateTime.Now.Year, Month.SelectedIndex + 1, int.Parse(ddlDate.Text), t, 0, 0);
            DateTime d2 = new DateTime(DateTime.Now.Year, Month.SelectedIndex + 1, int.Parse(ddlDate.Text), d, 0, 0);
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            DateTime moon = new DateTime(DateTime.Now.Year, Month.SelectedIndex + 1, 1);
            if (db.IsPlanned(pd.UserName, ddlLiftId.SelectedValue, tpData.TpId, moon, moon.AddMonths(1).AddSeconds(-1)) && WorkType.SelectedValue != "ВР" ) // ВР
            {
                Msg.Text = "Для лифта " + ddlLiftId.SelectedValue + " " + tpData.TpId + " уже спланировано!";
                return;
            }
            if (db.IsHolliday(pd.UserName, d1))
            {
                Msg.Text = "Это не рабочий день!";
                return;
            }
            /*
            Base.UserInfo ui = db.GetUserInfo(pd.UserName);
            if (ui.Lunch >= d1.Hour && ui.Lunch < d2.Hour)
            {
                Msg.Text = "Это время перекрывает обед";
                return;
            }*/
            DataTable dt = db.GetPlan(pd.UserName, d1, d2.AddHours(-1));
            if (dt.Rows.Count > 0 && WorkType.SelectedValue != "ВР") // для ВР
            {
                Msg.Text = "Это время занято!";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [Plan] " +
                    "(UserId, LiftId, TpId, [Date], DateEnd) " +
                    "values (@userId, @liftId, @tpId, @date, @dateEnd)", conn);
                cmd.Parameters.AddWithValue("userId", pd.Id);
                cmd.Parameters.AddWithValue("liftId", ddlLiftId.SelectedValue);
                if (WorkType.SelectedValue == "ВР") cmd.Parameters.AddWithValue("tpId", "ВР"); // заменить ТРы 
                else cmd.Parameters.AddWithValue("tpId", tpData.TpId); //   tpData.TpId
                cmd.Parameters.AddWithValue("date", d1);
                cmd.Parameters.AddWithValue("dateEnd", d2);
                cmd.ExecuteNonQuery();
                Msg.Text = string.Empty;
                Month_SelectedIndexChanged(sender, e);
                if (WorkType.SelectedValue == "ВР")
                { tpData.TpId = "ВР"; Msg.Text = "Для лифта № " + ddlLiftId.Text + " назначен внеплановый ремонт!"; return; }
            }
        }

        protected void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is CheckBox)) return;
            CheckBox SelectAll = (CheckBox)sender;
            if (!(SelectAll.NamingContainer is ListView)) return;
            ListView lv = (ListView)SelectAll.NamingContainer;
            foreach (ListViewItem item in lv.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("Select");
                cb.Checked = SelectAll.Checked;
            }
            SelectAll.Focus();
        }

        protected void Zayavka_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Zayavka.aspx");
        }
    }
}