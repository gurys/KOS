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
    public partial class IncidentReport : System.Web.UI.Page
    {
        public class Data
        {
            public string Title { get; set; }
        }

        public class PersonData
        {
            public string Title { get; set; }
            public string Id { get; set; }
        }

        List<PersonData> workerData = new List<PersonData>();
        List<PersonData> prinyalData = new List<PersonData>();
        string _role = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();

            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();

                    Bind(conn, "select IdU from Lifts group by IdU", IdU);
                    Bind(conn, "select IdM from Lifts group by IdM", IdM);
                    Bind(conn, "select IdL from Lifts group by IdL", IdL);
                    Bind(conn, "select Ttx from Ttx where TtxTitleId=1 group by Ttx", Address);
                    Bind(conn, "select Title from ZayavkaCategory", Category);
                    Bind(conn, "select Title from [From]", From);
                    CalendarStart.SelectedDate = new DateTime(2015, 1, 1);
                    CalendarStartEnd.SelectedDate = DateTime.Now.Date;
                    CalendarFinish.SelectedDate = new DateTime(2015, 1, 1);
                    CalendarFinishEnd.SelectedDate = DateTime.Now.Date;
                    if (_role == "Worker")
                        ph1.Visible = false;
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                BindPrinyal(conn);
                BindWorker(conn);
            }
        }

        string BuildVarcharWhere(ListView lv, string w, string p)
        {
            if (w != null && !IsAllSelected(lv))
            {
                List<string> u = GetSelectedTitles(lv);
                string tu = string.Empty;
                foreach (string i in u)
                    tu += (string.IsNullOrEmpty(tu) ? "" : ",") + "N'" + i + "'";
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(w))
                        w = "where ";
                    else
                        w += "and ";
                    w += p + " in (" + tu + ") ";
                }
                else return null;
            }
            return w;
        }

        string BuildHourWhere(Controls.SelectHours sh, string w, string p)
        {
            if (w != null && !sh.IsAllSelected())
            {
                List<int> u = sh.GetSelectedTitles();
                string tu = string.Empty;
                foreach (int i in u)
                    tu += (string.IsNullOrEmpty(tu) ? "" : ",") + i;
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(w))
                        w = "where ";
                    else
                        w += "and ";
                    w += p + " in (" + tu + ") ";
                }
                else return null;
            }
            return w;
        }

        string BuildMinuteWhere(Controls.SelectMinutes sh, string w, string p)
        {
            if (w != null && !sh.IsAllSelected())
            {
                List<int> u = sh.GetSelectedTitles();
                string tu = string.Empty;
                foreach (int i in u)
                    tu += (string.IsNullOrEmpty(tu) ? "" : ",") + i;
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(w))
                        w = "where ";
                    else
                        w += "and ";
                    w += p + " in (" + tu + ") ";
                }
                else return null;
            }
            return w;
        }

        string BuildPrinyalWhere(string w)
        {
            if (w != null && !IsAllSelected(Prinyal))
            {
                List<string> u = GetSelectedTitles(Prinyal);
                string tu = string.Empty;
                foreach (string i in u)
                {
                    PersonData awd = prinyalData.Find(delegate(PersonData wd)
                    {
                        return (wd.Title == i);
                    });
                    if (awd != null)
                        tu += (string.IsNullOrEmpty(tu) ? "" : ",") + "'" + awd.Id + "'";
                }
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(w))
                        w = "where ";
                    else
                        w += "and ";
                    w += "(z.Prinyal is null or z.Prinyal in (" + tu + ")) ";
                }
                else return null;
            }
            return w;
        }

        string BuildWorkerWhere(string w)
        {
            if (w != null && _role == "Worker")
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                App_Code.Base.UserInfo ui = db.GetUserInfo(Page.User.Identity.Name);
                PersonData awd = workerData.Find(delegate(PersonData wd)
                {
                    return (wd.Title == ui.Family + " " + ui.IO);
                });
                if (awd == null) return null;
                if (string.IsNullOrEmpty(w))
                    w = "where ";
                else
                    w += "and ";
                w += "(z.Worker is null or z.Worker='" + awd.Id + "') ";
            }
            else if (w != null && (!IsAllSelected(Worker) || workerData.Count == 1))
            {
                List<string> u = GetSelectedTitles(Worker);
                string tu = string.Empty;
                foreach (string i in u)
                {
                    PersonData awd = workerData.Find(delegate(PersonData wd)
                    {
                        return (wd.Title == i);
                    });
                    if (awd != null)
                        tu += (string.IsNullOrEmpty(tu) ? "" : ",") + "'" + awd.Id + "'";
                }
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(w))
                        w = "where ";
                    else
                        w += "and ";
                    w += "(z.Worker is null or z.Worker in (" + tu + ")) ";
                }
                else return null;
            }
            return w;
        }

        string BuildStartWhere(string w)
        {
            if (w != null)
            {
                if (string.IsNullOrEmpty(w))
                    w = "where ";
                else
                    w += "and ";
                w += "z.Start between @Start and @StartEnd ";
            }
            return w;
        }

        string BuildFinishWhere(string w)
        {
            if (w != null)
            {
                if (string.IsNullOrEmpty(w))
                    w = "where ";
                else
                    w += "and ";
                w += "(z.Finish is null or (z.Finish between @Finish and @FinishEnd)) ";
            }
            return w;
        }

        string BuildWhere()
        {
            string w = string.Empty;
            w = BuildVarcharWhere(IdU, w, "l.IdU");
            w = BuildVarcharWhere(IdM, w, "l.IdM");
            w = BuildVarcharWhere(IdL, w, "l.IdL");
            w = BuildVarcharWhere(Address, w, "t.Ttx");
            w = BuildVarcharWhere(Category, w, "z.Category");
            w = BuildVarcharWhere(From, w, "z.[From]");
            w = BuildStartWhere(w);
            w = BuildHourWhere(HourStart, w, "DATEPART(hour,z.Start)");
            w = BuildMinuteWhere(MinuteStart, w, "DATEPART(n,z.Start)");
            w = BuildPrinyalWhere(w);
            if (_role != "Worker")
                w = BuildFinishWhere(w);
            w = BuildHourWhere(HourFinish, w, "DATEPART(hour,z.Start)");
            w = BuildMinuteWhere(MinuteFinish, w, "DATEPART(n,z.Start)");
            w = BuildWorkerWhere(w);
            return w;
        }

        int Bind(SqlConnection conn, string s, ListView lv)
        {
            SqlCommand cmd = new SqlCommand(s, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Data> data = new List<Data>();
            while (reader.Read())
                if (!(reader[0] is DBNull))
                    data.Add(new Data() { Title = reader[0].ToString() });
            reader.Close();
            lv.DataSource = data;
            lv.DataBind();
            return data.Count;
        }

        void BindMonths(SqlConnection conn, string s, ListView lv)
        {
            SqlCommand cmd = new SqlCommand(s, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Data> data = new List<Data>();
            while (reader.Read())
                if (!(reader[0] is DBNull))
                    data.Add(new Data() { Title = KOS.App_Code.Base.months[(int)reader[0] - 1] });
            reader.Close();
            lv.DataSource = data;
            lv.DataBind();
        }

        void BindPrinyal(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("select ui.Family, ui.[IO], ui.UserId from UserInfo ui " +
                "join Zayavky z on ui.UserId=z.Prinyal " +
                "group by ui.Family, ui.[IO], ui.UserId", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                if (!(reader[0] is DBNull))
                    prinyalData.Add(new PersonData() 
                    { 
                        Title = reader[0].ToString() + " " + reader[1].ToString(),
                        Id = reader[1].ToString()
                    });
            reader.Close();
            if (!IsPostBack)
            {
                Prinyal.DataSource = prinyalData;
                Prinyal.DataBind();
            }
        }

        void BindWorker(SqlConnection conn)
        {
            List<string> roles = new List<string>() { "Administrator", "ODS", "Manager" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.[IO], ui.UserId from UserInfo ui " +
                    "join Zayavky z on ui.UserId=z.Worker " +
                    "group by ui.Family, ui.[IO], ui.UserId", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (!(reader[0] is DBNull))
                        workerData.Add(new PersonData()
                        {
                            Title = reader[0].ToString() + " " + reader[1].ToString(),
                            Id = reader[2].ToString()
                        });
                reader.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO, ui.UserId from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId where u.UserName=@UserName " +
                    "group by Family, IO, ui.UserId", conn);
                cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (!(reader[0] is DBNull))
                        workerData.Add(new PersonData()
                        {
                            Title = reader[0].ToString() + " " + reader[1].ToString(),
                            Id = reader[2].ToString()
                        });
                reader.Close();
            }
            if (!IsPostBack)
            {
                Worker.DataSource = workerData;
                Worker.DataBind();
            }
        }

        bool IsAllSelected(ListView lv)
        {
            CheckBox cbAll = (CheckBox)lv.FindControl("SelectAll");
            if (cbAll == null) return true;
            bool selAll = true;
            foreach (ListViewItem item in lv.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("Select");
                if (!cb.Checked)
                {
                    selAll = false;
                    break;
                }
            }
            cbAll.Checked = selAll;
            return selAll;
        }

        string GetSelected(ListView lv)
        {
            string s = string.Empty;
            foreach (ListViewItem item in lv.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("Select");
                if (cb.Checked)
                {
                    Label lb = (Label)item.FindControl("Title");
                    if (string.IsNullOrEmpty(s))
                        s = lb.Text;
                    else
                        s += ", " + lb.Text;
                }
            }
            return s;
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

        protected void Select_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is CheckBox)) return;
            CheckBox Select = (CheckBox)sender;
            if (!(Select.Parent.NamingContainer.NamingContainer is ListView)) return;
            ListView lv = (ListView)Select.Parent.NamingContainer.NamingContainer;
            if (Select.Checked == false)
            {
                CheckBox selectAll = (CheckBox)lv.FindControl("SelectAll");
                selectAll.Checked = false;
            }
            IsAllSelected(lv);
        }

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/IncidentReport.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            Response.Redirect("~/About.aspx");
            return string.Empty;
        }

        class Report
        {
            public string LiftId { get; set; }
            public string Address { get; set; }
            public string Url { get; set; }
            public string Category { get; set; }
            public string From { get; set; }
            public string Start { get; set; }
            public string Prinyal { get; set; }
            public string Finish { get; set; }
            public string Worker { get; set; }
            public string Prostoy { get; set; }
        }

        void FillLabels()
        {
            lPrinyalDate.Text = " с " + CalendarStart.SelectedDate.ToLongDateString() + " по " +
                CalendarStartEnd.SelectedDate.ToLongDateString();
            if (HourStart.IsAllSelected())
                lPHours.Text = " все";
            else
            {
                List<int> sel = HourStart.GetSelectedTitles();
                lPHours.Text = string.Empty;
                foreach (int i in sel)
                    if (string.IsNullOrEmpty(lPHours.Text))
                        lPHours.Text += i.ToString();
                    else
                        lPHours.Text += ", " + i.ToString();
            }
            if (MinuteStart.IsAllSelected())
                lPMinutes.Text = " все";
            else
            {
                List<int> sel = MinuteStart.GetSelectedTitles();
                lPMinutes.Text = string.Empty;
                foreach (int i in sel)
                    if (string.IsNullOrEmpty(lPMinutes.Text))
                        lPMinutes.Text += i.ToString();
                    else
                        lPMinutes.Text += ", " + i.ToString();
            }
            if (_role != "Worker")
                lVypolnilDate.Text = " с " + CalendarFinish.SelectedDate.ToLongDateString() + " по " +
                    CalendarFinishEnd.SelectedDate.ToLongDateString();
            else
                lVypolnilDate.Text = " в любое время";
            if (HourFinish.IsAllSelected())
                lVHours.Text = " все";
            else
            {
                List<int> sel = HourFinish.GetSelectedTitles();
                lVHours.Text = string.Empty;
                foreach (int i in sel)
                    if (string.IsNullOrEmpty(lVHours.Text))
                        lVHours.Text += i.ToString();
                    else
                        lVHours.Text += ", " + i.ToString();
            }
            if (MinuteFinish.IsAllSelected())
                lVMinutes.Text = " все";
            else
            {
                List<int> sel = MinuteFinish.GetSelectedTitles();
                lVMinutes.Text = string.Empty;
                foreach (int i in sel)
                    if (string.IsNullOrEmpty(lVMinutes.Text))
                        lVMinutes.Text += i.ToString();
                    else
                        lVMinutes.Text += ", " + i.ToString();
            }
            if (IsAllSelected(IdU))
                lU.Text = " любой";
            else
                lU.Text = GetSelected(IdU);
            if (IsAllSelected(IdM))
                lM.Text = " любой";
            else
                lM.Text = GetSelected(IdM);
            if (IsAllSelected(IdL))
                lL.Text = " любой";
            else
                lL.Text = GetSelected(IdL);
            if (IsAllSelected(Address))
            {
                if (GetSelectedTitles(IdU).Count == 1 && GetSelectedTitles(IdM).Count == 1)
                {
                    KOS.App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    List<string> ls = db.GetAddress(GetSelectedTitles(IdU)[0], GetSelectedTitles(IdM)[0]);
                    lAddress.Text = string.Empty;
                    foreach (string s in ls)
                    {
                        if (!string.IsNullOrEmpty(lAddress.Text))
                            lAddress.Text += ", ";
                        lAddress.Text += s;
                    }
                }
                else lAddress.Text = " любой";
            }
            else
                lAddress.Text = GetSelected(Address);
            if (IsAllSelected(Category))
                lCategory.Text = " любая";
            else
                lCategory.Text = GetSelected(Category);
            if (IsAllSelected(From))
                lSource.Text = " любой";
            else
                lSource.Text = GetSelected(From);
            if (IsAllSelected(Prinyal))
                lPrinyal.Text = " любой";
            else
                lPrinyal.Text = GetSelected(Prinyal);
            if (_role == "Worker")
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                App_Code.Base.UserInfo ui = db.GetUserInfo(Page.User.Identity.Name);
                lWorker.Text = ui.Family + " " + ui.IO;
            }
            else if (IsAllSelected(Worker))
                lWorker.Text = " любой";
            else
                lWorker.Text = GetSelected(Worker);
        }

        protected void DoIt_Click(object sender, EventArgs e)
        {
            FillLabels();
            List<Report> r = new List<Report>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                string s = "select l.LiftId, t.Ttx, ui.Family, ui.[IO], YEAR(z.Start) as yStart, MONTH(z.Start) as MStart, " +
                    "DAY(z.Start) as dStart, DATEPART(hour,z.Start) as hStart, DATEPART(n,z.Start) as minStart, z.Id as zId, z.Category, " +
                    "z.[From], z.[Status], uip.Family as pFamily, uip.[IO] as pIO, uiw.Family as wFamily, uiw.[IO] as wIO, " +
                    "YEAR(z.Finish) as yFinish, MONTH(z.Finish) as MFinish, DAY(z.Finish) as dFinish, " +
                    "DATEPART(hour,z.Finish) as hFinish, DATEPART(n,z.Finish) as minFinish, z.Start, z.Finish from Zayavky z " +
                    "join Lifts l on l.LiftId=z.LiftId join LiftsTtx lt on lt.LiftId=z.LiftId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 join UserInfo ui on ui.UserId=z.UserId " +
                    "left join UserInfo uip on uip.UserId=z.Prinyal left join UserInfo uiw on uiw.UserId=z.Worker ";
                if (_role == "Worker")
                    s += "join WorkerLifts wl on wl.LiftId=z.LiftId join Users u on u.UserId=wl.UserId and u.UserName=@userName ";
                string w = BuildWhere();
                if (w == null)
                {
                    // empty report комментарий
                    Out.DataSource = r;
                    Out.DataBind();
                    return;
                }
                else if (w.Length > 0)
                    s += w;
                s += "group by l.LiftId, t.Ttx, ui.Family, ui.[IO], YEAR(z.Start), MONTH(z.Start), " +
                    "DAY(z.Start), DATEPART(hour,z.Start), DATEPART(n,z.Start), z.Id, z.Category, " +
                    "z.[From], z.[Status], uip.Family, uip.[IO], uiw.Family, uiw.[IO], " +
                    "YEAR(z.Finish), MONTH(z.Finish), DAY(z.Finish), DATEPART(hour,z.Finish), DATEPART(n,z.Finish), z.Start, z.Finish";

                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.Parameters.AddWithValue("Start", CalendarStart.SelectedDate.Date);
                cmd.Parameters.AddWithValue("StartEnd", CalendarStartEnd.SelectedDate.Date.AddDays(1));
                cmd.Parameters.AddWithValue("Finish", CalendarFinish.SelectedDate.Date);
                cmd.Parameters.AddWithValue("FinishEnd", CalendarFinishEnd.SelectedDate.Date.AddDays(1));
                if (_role == "Worker")
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (_role == "ODS") //для ОДС возможность редактировать заявку
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                foreach (DataRow dr in dt.Rows)
                {
                    string url = string.Empty;
                    int zId = Int32.Parse(dr["zId"].ToString());
                    if (zId != 0)
                        url = "~/ZayavkaView.aspx?zId=" + zId;
                    string prinyal = string.Empty;
                    if (!(dr["pFamily"] is DBNull))
                        prinyal = dr["pFamily"].ToString() + " " + dr["pIO"].ToString();
                    string worker = string.Empty;
                    if (!(dr["wFamily"] is DBNull))
                        worker = dr["wFamily"].ToString() + " " + dr["wIO"].ToString();
                    string finish = string.Empty;
                    string prostoy = string.Empty;
                    if (!(dr["Finish"] is DBNull))
                    {
                        finish = dr["Finish"].ToString();
                        prostoy = (((DateTime)dr["Finish"]) - ((DateTime)dr["Start"])).ToString();
                    }
                    else
                        prostoy = (DateTime.Now - ((DateTime)dr["Start"])).ToString();
                    r.Add(new Report()
                    {
                        Address = dr["Ttx"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Url = url,
                        Category = dr["Category"].ToString(),
                        Prinyal = prinyal,
                        Worker = worker,
                        From = dr["From"].ToString(),
                        Finish = finish,
                        Start = dr["Start"].ToString(),
                        Prostoy = prostoy
                    });
                }

                Out.DataSource = r;
                Out.DataBind();
            }
            Qst.Visible = false;
            phReport.Visible = true;// вывод отчета
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PlaceHolder1.Visible = !PlaceHolder1.Visible;
            if (PlaceHolder1.Visible)
                HourStart.Focus();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            PlaceHolder2.Visible = !PlaceHolder2.Visible;
            if (PlaceHolder2.Visible)
                CalendarFinish.Focus();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            PlaceHolder3.Visible = !PlaceHolder3.Visible;
            if (PlaceHolder3.Visible)
                HourFinish.Focus();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            PlaceHolder4.Visible = !PlaceHolder4.Visible;
            if (PlaceHolder4.Visible)
                IdU.FindControl("SelectAll").Focus();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            PlaceHolder5.Visible = !PlaceHolder5.Visible;
            if (PlaceHolder5.Visible)
                IdM.FindControl("SelectAll").Focus();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            PlaceHolder6.Visible = !PlaceHolder6.Visible;
            if (PlaceHolder6.Visible)
                IdL.FindControl("SelectAll").Focus();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            PlaceHolder7.Visible = !PlaceHolder7.Visible;
            if (PlaceHolder7.Visible)
                Address.FindControl("SelectAll").Focus();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            PlaceHolder8.Visible = !PlaceHolder8.Visible;
            if (PlaceHolder8.Visible)
                Category.FindControl("SelectAll").Focus();
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            PlaceHolder9.Visible = !PlaceHolder9.Visible;
            if (PlaceHolder9.Visible)
                From.FindControl("SelectAll").Focus();
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            PlaceHolder10.Visible = !PlaceHolder10.Visible;
            if (PlaceHolder10.Visible)
                Prinyal.FindControl("SelectAll").Focus();
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            PlaceHolder11.Visible = !PlaceHolder11.Visible;
            if (PlaceHolder11.Visible)
                Worker.FindControl("SelectAll").Focus();
        }
    }
}