using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using KOS.App_Code;

namespace KOS
{
    public partial class Worker : System.Web.UI.Page
    {
        class Data
        {
            public string Name { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Lunch { get; set; }
        }

        List<Base.PersonData> _workers = new List<Base.PersonData>();
        List<Base.HoolidaysType> _hollidaysType = new List<Base.HoolidaysType>();
        string[] _soc = new string[] { "установить", "очистить" };

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _workers = db.GetWorkers();
            _hollidaysType = db.GetWorkTypes();
            if (!IsPostBack)
            {
                SaveOrClear.DataSource = _soc;
                SaveOrClear.DataBind();
                SaveOrClear.SelectedIndex = 0;
                HollidaysType.DataSource = _hollidaysType;
                HollidaysType.DataBind();
                HollidaysType.SelectedIndex = 0;
                Base.UserInfo ui = db.GetUserInfo(_workers[0].UserName);
                From.Text = ui.HourBeg.ToString();
                To.Text = ui.HourEnd.ToString();
                Lunch.Text = ui.Lunch.ToString();
                DataTable data = db.GetWorkTime();
                List<Data> ls = new List<Data>();
                foreach (DataRow dr in data.Rows)
                    ls.Add(new Data()
                    {
                        Name = dr["Family"].ToString() + " " + dr["IO"].ToString(),
                        From = dr["HourBeg"].ToString(),
                        To = dr["HourEnd"].ToString(),
                        Lunch = dr["Lunch"].ToString()
                    });
                Graf.DataSource = ls;
                Graf.DataBind();

                WorkerName.DataSource = _workers;
                WorkerName.DataBind();
                WorkerName.SelectedIndex = 0;
                List<string> s = db.GetIdU();
                IdU.DataSource = s;
                IdU.DataBind();
                if (s.Count > 0)
                    IdU.SelectedIndex = 0;
                IdU_SelectedIndexChanged(sender, e);
                //Beg.SelectedDate = DateTime.Now.Date;
                //End.SelectedDate = DateTime.Now.Date.AddMonths(1);
                Calendar1.SelectedDate = DateTime.Now.Date;
                Calendar2.SelectedDate = DateTime.Now.Date.AddMonths(1);
                Calendar3.SelectedDate = DateTime.Now.Date;
                DHStart.SelectedDate = DateTime.Now.Date;
                DHEnd.SelectedDate = DateTime.Now.Date.AddMonths(1);
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.Title == WorkerName.SelectedValue;
                });
                List<Base.UserLift> ul = db.GetUserLift(pd.Id);
                AllList.DataSource = ul;
                AllList.DataBind();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //TheWorks.SetPeriod(Beg.SelectedDate, End.SelectedDate);

            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
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
        }

        protected void IdU_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> ls = db.GetIdM(IdU.SelectedValue);
            IdM.DataSource = ls;
            IdM.DataBind();
            if (ls.Count > 0)
                IdM.SelectedIndex = 0;
            IdM_SelectedIndexChanged(sender, e);
        }

        protected void IdM_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            List<Base.UserLift> data = db.GetUserLift(pd.UserName, IdU.SelectedValue, IdM.SelectedValue);
            IdL.DataSource = data;
            IdL.DataBind();

            List<Base.UserLift> ul = db.GetUserLift(pd.Id);
            AllList.DataSource = ul;
            AllList.DataBind();
        }

        protected void Worker_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdM_SelectedIndexChanged(sender, e);

            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Base.UserInfo ui = db.GetUserInfo(pd.UserName);
            From.Text = ui.HourBeg.ToString();
            To.Text = ui.HourEnd.ToString();
            Lunch.Text = ui.Lunch.ToString();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            int from, to, lunch;
            if (!int.TryParse(From.Text, out from))
            {
                Msg.Text = "Укажите начало рабочего дня";
                return;
            }
            if (!int.TryParse(To.Text, out to))
            {
                Msg.Text = "Укажите конец рабочего дня";
                return;
            }
            if (!int.TryParse(Lunch.Text, out lunch))
            {
                Msg.Text = "Укажите перерыв на обед";
                return;
            }
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update UserInfo " +
                    "set HourBeg=@t1, HourEnd=@t2, Lunch=@t3 " +
                    "where UserId=@userId", conn);
                cmd.Parameters.AddWithValue("userId", pd.Id);
                cmd.Parameters.AddWithValue("t1", from);
                cmd.Parameters.AddWithValue("t2", to);
                cmd.Parameters.AddWithValue("t3", lunch);
                cmd.ExecuteNonQuery();
                Msg.Text = string.Empty;
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Worker.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
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

        protected void SaveConnections_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from WorkerLifts " +
                    "where UserId=@userId and LiftId in " +
                    "(select LiftId from Lifts where IdU=@idU and IdM=@idM)", conn);
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.Title == WorkerName.SelectedValue;
                });
                cmd.Parameters.AddWithValue("userId", pd.Id);
                cmd.Parameters.AddWithValue("idU", IdU.SelectedValue);
                cmd.Parameters.AddWithValue("idM", IdM.SelectedValue);
                cmd.ExecuteNonQuery();
                List<string> liftIds = GetSelectedTitles(IdL);
                foreach (string liftId in liftIds)
                {
                    cmd = new SqlCommand("insert into WorkerLifts (UserId, LiftId) values (@userId, @liftId)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("liftId", liftId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void ShowWorkTime_Click(object sender, EventArgs e)
        {
            if (!WorkTime.Visible)
            {
                WorkTime.Visible = true;
                Connections.Visible = false;
                phHolliday.Visible = false;
                //YearWorks.Visible = false;
            }
            else
            {
                WorkTime.Visible = false;
                //YearWorks.Visible = true;
            }
        }

        protected void ShowConnections_Click(object sender, EventArgs e)
        {
            if (!Connections.Visible)
            {
                WorkTime.Visible = false;
                Connections.Visible = true;
                phHolliday.Visible = false;
                //YearWorks.Visible = true;
            }
            else
                Connections.Visible = false;
        }

        protected void ShowWorks_Click(object sender, EventArgs e)
        {
            //TheWorks.Visible = true;
            //YearWorks.Visible = true;
        }

        protected void ShowHollidays_Click(object sender, EventArgs e)
        {
            if (!phHolliday.Visible)
            {
                WorkTime.Visible = false;
                Connections.Visible = false;
                phHolliday.Visible = true;
                //YearWorks.Visible = true;
            }
            else
                phHolliday.Visible = false;
            SaveOrClear_SelectedIndexChanged(sender, e);
        }

        protected void SaveOrClear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaveOrClear.SelectedIndex == 0)
            {
                phClear.Visible = false;
                HollidaysType.Visible = true;
                HollidaysType_SelectedIndexChanged(sender, e);
            }
            else
            {
                phHollidayDaysOfWeek.Visible = false;
                phClear.Visible = true;
                HollidaysType.Visible = false;
                phHollidayPeriod.Visible = false;
                phHollidaysPeriod2.Visible = false;
                Connections.Visible = false;
                WorkTime.Visible = false;
                phDaysHolliday.Visible = false;
            }
        }

        protected void HollidaysType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base.HoolidaysType type = _hollidaysType.Find(delegate (Base.HoolidaysType ht)
            {
                return HollidaysType.SelectedValue == ht.Title;
            });
            if (type == null) return;
            label1.Text = "выходной";
            if (type.Id == 1)
            {
                phHollidayDaysOfWeek.Visible = true;
                phHollidayPeriod.Visible = false;
                phHollidaysPeriod2.Visible = false;
                Connections.Visible = false;
                WorkTime.Visible = false;
                phClear.Visible = false;
                phDaysHolliday.Visible = false;
            }
            else if (type.Id < 5)
            {
                phHollidayDaysOfWeek.Visible = false;
                phHollidayPeriod.Visible = true;
                phHollidaysPeriod2.Visible = false;
                Connections.Visible = false;
                WorkTime.Visible = false;
                phClear.Visible = false;
                phDaysHolliday.Visible = false;
            }
            else if (type.Id == 9) // конкретное число
            {
                phHollidayDaysOfWeek.Visible = false;
                phHollidayPeriod.Visible = false;
                phHollidaysPeriod2.Visible = false;
                Connections.Visible = false;
                WorkTime.Visible = false;
                phClear.Visible = false;
                phDaysHolliday.Visible = true;
            }
            else
            {
                if (type.Id == 5)
                    label1.Text = "рабочий день";
                phHollidayDaysOfWeek.Visible = false;
                phHollidayPeriod.Visible = false;
                phHollidaysPeriod2.Visible = true;
                Connections.Visible = false;
                WorkTime.Visible = false;
                phClear.Visible = false;
                phDaysHolliday.Visible = false;
            }
        }

        void SetHolliday(DateTime date, string userId)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Holidays where UserId=@userId and [Date]=@date", conn);
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("date", date);
                object o = cmd.ExecuteScalar();
                if (o == null)
                {
                    cmd = new SqlCommand("insert into Holidays " +
                        "(UserId, [Date], HollidaysTypeId) " +
                        "values (@userId, @date, 1) ", conn);
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void H2_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения";
                return;
            }
            Base.HoolidaysType type = _hollidaysType.Find(delegate(Base.HoolidaysType ht)
            {
                return HollidaysType.SelectedValue == ht.Title;
            });
            if (type == null)
            {
                Msg.Text = "Ошибка сохранения";
                return;
            }
            if (Calendar3.SelectedDate < Calendar1.SelectedDate || Calendar3.SelectedDate > Calendar2.SelectedDate)
            {
                Msg.Text = "Ошибка сохранения";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                switch (type.Id)
                {
                    case 5: // сутки через трое
                        for (DateTime date = Calendar3.SelectedDate.AddDays(1); date < Calendar2.SelectedDate.AddDays(1); date = date.AddDays(1))
                        {
                            for (int i = 0; date < Calendar2.SelectedDate.AddDays(1) && i < 3; date = date.AddDays(1))
                            {
                                SetHolliday(date, pd.Id);
                                ++i;
                            }
                        }
                        break;
                    case 6: // два через два
                        for (DateTime date = Calendar3.SelectedDate; date < Calendar2.SelectedDate.AddDays(1); date = date.AddDays(3))
                        {
                            SetHolliday(date, pd.Id);
                            date = date.AddDays(1);
                            if (date > Calendar2.SelectedDate)
                                break;
                            SetHolliday(date, pd.Id);
                        }
                        break;
                    case 7: // неделя через неделю
                        for (DateTime date = Calendar3.SelectedDate; date < Calendar2.SelectedDate.AddDays(1); date = date.AddDays(7))
                        {
                            for (int i = 0; date < Calendar2.SelectedDate.AddDays(1) && i < 7; date = date.AddDays(1))
                            {
                                SetHolliday(date, pd.Id);
                                ++i;
                            }
                        }
                        break;
                    case 8: // две недели через две недели
                        for (DateTime date = Calendar3.SelectedDate; date < Calendar2.SelectedDate.AddDays(1); date = date.AddDays(14))
                        {
                            for (int i = 0; date < Calendar2.SelectedDate.AddDays(1) && i < 14; date = date.AddDays(1))
                            {
                                SetHolliday(date, pd.Id);
                                ++i;
                            }
                        }
                        break;
                }
                Msg.Text = string.Empty;
            }
        }

        protected void SaveWeekEnd_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения";
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
                    SetHolliday(date, pd.Id);
                    Msg.Text = string.Empty;
                }
            }
        }

        protected void HollidayPeriodSave_Click(object sender, EventArgs e)
        {
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения";
                return;
            }
            Base.HoolidaysType bw = _hollidaysType.Find(delegate(Base.HoolidaysType i)
            {
                return i.Title == HollidaysType.SelectedValue;
            });
            if (bw == null || bw.Id < 1)
            {
                Msg.Text = "Ошибка сохранения";
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
                return i.Title == WorkerName.SelectedValue;
            });
            if (pd == null)
            {
                Msg.Text = "Ошибка сохранения";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from Holidays " +
                    "where UserId=@userId and " +
                    "[Date] between @d1 and @d2", conn);
                cmd.Parameters.AddWithValue("userId", pd.Id);
                cmd.Parameters.AddWithValue("d1", ClearStart.SelectedDate);
                cmd.Parameters.AddWithValue("d2", ClearEnd.SelectedDate);
                cmd.ExecuteNonQuery();
                Msg.Text = string.Empty;
            }
        }

        protected void ShowAllConnections_Click(object sender, EventArgs e)
        {
            AllConnections.Visible = !AllConnections.Visible;
            if (AllConnections.Visible)
                AllList.FindControl("SelectAll").Focus();
        }

        protected void SaveAllConnections_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from WorkerLifts " +
                    "where UserId=@userId", conn);
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.Title == WorkerName.SelectedValue;
                });
                cmd.Parameters.AddWithValue("userId", pd.Id);
                cmd.ExecuteNonQuery();
                List<string> liftIds = GetSelectedTitles(AllList);
                foreach (string liftId in liftIds)
                {
                    cmd = new SqlCommand("insert into WorkerLifts (UserId, LiftId) values (@userId, @liftId)", conn);
                    cmd.Parameters.AddWithValue("userId", pd.Id);
                    cmd.Parameters.AddWithValue("liftId", liftId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void DaysHollidayPeriodSet_Click(object sender, EventArgs e)
        {
            DaysHollidayPeriodSetting.Visible = false;

            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            List<App_Code.Base.CheckedLift> list = new List<Base.CheckedLift>();
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<DateTime> d = db.GetDaysHolliday(pd.Id, DHStart.SelectedDate, DHEnd.SelectedDate);
            for (DateTime date = DHStart.SelectedDate; date <= DHEnd.SelectedDate; date = date.AddDays(1))
            {
                App_Code.Base.CheckedLift cl = new Base.CheckedLift() { Title=date.ToShortDateString() };
                if (d.FindIndex(delegate(DateTime i)
                {
                    return i == date;
                }) >= 0)
                    cl.Checked = true;
                list.Add(cl);
            }
            DaysHolliday.DataSource = list;
            DaysHolliday.DataBind();
            SetDaysHolliday.Visible = true;
        }

        protected void SaveDaysHolliday_Click(object sender, EventArgs e)
        {
            List<string> list = GetSelectedTitles(DaysHolliday);
            DateTime date = DHStart.SelectedDate;
            Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
            {
                return i.Title == WorkerName.SelectedValue;
            });
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                foreach (string i in list)
                {
                    DateTime h = DateTime.Parse(i);
                    for (; date <= h; date = date.AddDays(1))
                    {
                        SqlCommand cmd = new SqlCommand("delete from Holidays where UserId=@id and [Date]=@d", conn);
                        cmd.Parameters.AddWithValue("id", pd.Id);
                        cmd.Parameters.AddWithValue("d", date);
                        cmd.ExecuteNonQuery();
                    }
                    SqlCommand cmd2 = new SqlCommand("insert into Holidays " +
                        "([Date], UserId, HollidaysTypeId) values (@d, @id, 9)", conn);
                    cmd2.Parameters.AddWithValue("id", pd.Id);
                    cmd2.Parameters.AddWithValue("d", h);
                    cmd2.ExecuteNonQuery();
                }
                for (; date <= DHEnd.SelectedDate; date = date.AddDays(1))
                {
                    SqlCommand cmd = new SqlCommand("delete from Holidays where UserId=@id and [Date]=@d", conn);
                    cmd.Parameters.AddWithValue("id", pd.Id);
                    cmd.Parameters.AddWithValue("d", date);
                    cmd.ExecuteNonQuery();
                }
            }
            DaysHollidayPeriodSetting.Visible = true;
            SetDaysHolliday.Visible = false;
        }
    }
}