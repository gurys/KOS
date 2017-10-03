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
    public partial class ReglamentReport : System.Web.UI.Page
    {
        public class Data
        {
            public string Title { get; set; }
        }

        public class WorkerData
        {
            public string Title { get; set; }
            public string Id { get; set; }
        }

        class Report
        {
            public string LiftId { get; set; }
            public string Address { get; set; }
            public string TpId { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public string Url { get; set; }
            public string UrlTitle { get; set; }
            public string PlanId { get; set; }
            public string Done { get; set; }
        }

        List<WorkerData> workerData = new List<WorkerData>();
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
                    Bind(conn, "select TpId from [Plan] group by TpId order by TpId", WorkType);
                    List<Data> done = new List<Data>()
                    {
                        new Data() {Title = "выполнено"}, new Data() {Title = "не выполнено"}
                    };
                    Done.DataSource = done;
                    Done.DataBind();
                    Calendar.SelectedDate = new DateTime(2017, 1, 1);
                    CalendarEnd.SelectedDate = DateTime.Now.Date;
                    if (_role == "Worker")
                    {
                        ph1.Visible = false;
                        ph2.Visible = false;
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                BindWorker(conn);
            }
        }

        string BuildAddressWhere()
        {
            string s = string.Empty;
            if ((IsAllSelected(IdU) && IsAllSelected(IdM) && IsAllSelected(IdL)) || _role == "Worker") 
                return s;

            bool addAnd = false;
            if (!IsAllSelected(IdU))
            {
                List<string> u = GetSelectedTitles(IdU);
                string tu = string.Empty;
                foreach (string i in u)
                    tu += (string.IsNullOrEmpty(tu) ? "" : ",") + "N'" + i + "'";
                if (!string.IsNullOrEmpty(tu))
                {
                    if (!addAnd)
                        s += "where ";
                    s += "l.IdU in (" + tu + ") ";
                    addAnd = true;
                }
                else return null;
            }
            if (!IsAllSelected(IdM))
            {
                List<string> m = GetSelectedTitles(IdM);
                string tm = string.Empty;
                foreach (string i in m)
                    tm += (string.IsNullOrEmpty(tm) ? "" : ",") + "N'" + i + "'";
                if (!string.IsNullOrEmpty(tm))
                {
                    if (!addAnd)
                        s += "where ";
                    if (addAnd) s += "and ";
                    s += "l.IdM in (" + tm + ") ";
                    addAnd = true;
                }
                else return null;
            }
            if (!IsAllSelected(IdL))
            {
                List<string> l = GetSelectedTitles(IdL);
                string tl = string.Empty;
                foreach (string i in l)
                    tl += (string.IsNullOrEmpty(tl) ? "" : ",") + "N'" + i + "'";
                if (!string.IsNullOrEmpty(tl))
                {
                    if (!addAnd)
                        s += "where ";
                    if (addAnd) s += "and ";
                    s += "l.IdL in (" + tl + ") ";
                    addAnd = true;
                }
                else return null;
            }
            if (!addAnd)
                return null;
            return s;
        }

        string BuildWorkTypeWhere()
        {
            string s = BuildAddressWhere();
            if (IsAllSelected(Address) || _role == "Worker")
                return s;
            else
            {
                List<string> u = GetSelectedTitles(Address);
                string tu = string.Empty;
                foreach (string i in u)
                    tu += (string.IsNullOrEmpty(tu) ? "" : ",") + "N'" + i + "'";
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(s))
                        s = "where ";
                    else s += "and ";
                    s += "t.Ttx in (" + tu + ") ";
                }
                else return null;
            }
            return s;
        }

        string BuildWorkerWhere()
        {
            string s = BuildWorkTypeWhere();
            if (IsAllSelected(WorkType) || _role == "Worker")
                return s;
            else
            {
                List<string> u = GetSelectedTitles(WorkType);
                string tu = string.Empty;
                foreach (string i in u)
                    tu += (string.IsNullOrEmpty(tu) ? "" : ",") + "N'" + i + "'";
                if (!string.IsNullOrEmpty(tu))
                {
                    if (string.IsNullOrEmpty(s))
                        s = "where ";
                    else s += "and ";
                    s += "p.TpId in (" + tu + ") ";
                }
                else return null;
            }
            return s;
        }

        string BuildWhere()
        {
            string s = BuildWorkerWhere();
            if (s == null)
                return s;
            if (_role == "Worker")
            {
                if (string.IsNullOrEmpty(s))
                    s = "where ";
                else s += "and ";
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                App_Code.Base.UserInfo ui = db.GetUserInfo(Page.User.Identity.Name);
                WorkerData awd = workerData.Find(delegate(WorkerData wd)
                {
                    return (wd.Title == ui.Family + " " + ui.IO);
                });
                if (awd != null)
                    s += "p.UserId='" + awd.Id + "' ";
                else 
                    return null;
            }
            else if (!IsAllSelected(Worker) || workerData.Count == 1)
            {
                List<string> ls = GetSelectedTitles(Worker);
                string ss = string.Empty;
                foreach (string i in ls)
                {
                    WorkerData awd = workerData.Find(delegate(WorkerData wd)
                    {
                        return (wd.Title == i);
                    });
                    if (awd != null)
                        ss += (string.IsNullOrEmpty(ss) ? "" : ",") + "'" + awd.Id + "'";
                }
                if (!string.IsNullOrEmpty(ss))
                {
                    if (string.IsNullOrEmpty(s))
                        s = "where ";
                    else s += "and ";
                    s += "p.UserId in (" + ss + ") ";
                }
                else return null;
            }

            if (!IsAllSelected(Done))
            {
                List<string> ls = GetSelectedTitles(Done);
                int done = 0;
                if (ls.Count > 0)
                {
                    if (ls[0] == "выполнено")
                        done = 1;
                }
                else return null;
                if (string.IsNullOrEmpty(s))
                    s = "where ";
                else s += "and ";
                s += "p.[Done]=" + done.ToString() + " ";
            }

            if (s != null)
            {
                if (string.IsNullOrEmpty(s))
                    s = "where ";
                else
                    s += "and ";
                s += "p.[Date] between @DateStart and @DateEnd ";
            }

            return s;
        }

        void Bind(SqlConnection conn, string s, ListView lv)
        {
            SqlCommand cmd = new SqlCommand(s, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Data> data = new List<Data>();
            while (reader.Read())
                data.Add(new Data() { Title = reader[0].ToString() });
            reader.Close();
            lv.DataSource = data;
            lv.DataBind();
        }

        void BindWorker(SqlConnection conn)
        {
            List<string> roles = new List<string>() { "Administrator", "Manager" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                SqlCommand cmd = new SqlCommand("select Family, IO, UserId from UserInfo group by Family, IO, UserId", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    workerData.Add(new WorkerData()
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
                    workerData.Add(new WorkerData()
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
            if (cbAll == null) return false;
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

        void FillLabels()
        {
            lPrinyalDate.Text = " с " + Calendar.SelectedDate.ToLongDateString() + " по " +
                CalendarEnd.SelectedDate.ToLongDateString();
            if (IsAllSelected(IdU))
                lU.Text = " любой";
            else
            {
                List<string> ls = GetSelectedTitles(IdU);
                for (int i = 0; i < ls.Count; i++)
                    if (i == 0)
                        lU.Text = ls[i];
                    else
                        lU.Text += ", " + ls[i];
            }
            if (IsAllSelected(IdM))
                lM.Text = " любой";
            else
            {
                List<string> ls = GetSelectedTitles(IdM);
                for (int i = 0; i < ls.Count; i++)
                    if (i == 0)
                        lM.Text = ls[i];
                    else
                        lM.Text += ", " + ls[i];
            }
            if (IsAllSelected(IdL))
                lL.Text = " любой";
            else 
            {
                List<string> ls = GetSelectedTitles(IdL);
                for (int i = 0; i < ls.Count; i++)
                    if (i == 0)
                        lL.Text = ls[i];
                    else
                        lL.Text += ", " + ls[i];
            }
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
            {
                List<string> ls = GetSelectedTitles(Address);
                for (int i = 0; i < ls.Count; i++)
                    if (i == 0)
                        lAddress.Text = ls[i];
                    else
                        lAddress.Text += ", " + ls[i];
            }
            if (IsAllSelected(WorkType))
                lWorkType.Text = " любой";
            else
            {
                List<string> ls = GetSelectedTitles(WorkType);
                for (int i = 0; i < ls.Count; i++)
                    if (i == 0)
                        lWorkType.Text = ls[i];
                    else
                        lWorkType.Text += ", " + ls[i];
            }
            if (_role == "Worker")
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                App_Code.Base.UserInfo ui = db.GetUserInfo(Page.User.Identity.Name);
                lWorker.Text = ui.Family + " " + ui.IO;
            }
            else if (IsAllSelected(Worker))
                lWorker.Text = " любой";
            else
            {
                List<string> ls = GetSelectedTitles(Worker);
                for (int i = 0; i < ls.Count; i++)
                    if (i == 0)
                        lWorker.Text = ls[i];
                    else
                        lWorker.Text += ", " + ls[i];
            }
            if (IsAllSelected(Done))
                lDone.Text = " любое состояние";
            else
            {
                List<string> ls = GetSelectedTitles(Done);
                if (ls.Count > 0)
                    lDone.Text = ls[0];
            }
        }

        protected void DoIt_Click(object sender, EventArgs e)
        {
            FillLabels();

            List<Report> r = new List<Report>();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                string s = "select l.LiftId, t.Ttx, p.TpId, ui.Family, ui.IO, YEAR(rw.[Date]) as y, MONTH(rw.[Date]) as M, DAY(rw.[Date]) as d, rw.Id as rwId, p.Id as planId, p.Done from [Plan] p " +
                    "join ReglamentWorks rw on rw.PlanId=p.Id " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 " +
                    "join UserInfo ui on ui.UserId=rw.UserId ";
                string w = BuildWhere();
                if (w == null)
                {
                    // empty report
                    Out.DataSource = r;
                    Out.DataBind();
                    return;
                }
                else if (w.Length > 0)
                {
                    s += w;
                    s += " and rw.Prim is not null";
                }
                else
                    s += "where rw.Prim is not null";
                s += " group by l.LiftId, t.Ttx, p.TpId, ui.Family, ui.IO, YEAR(rw.[Date]), MONTH(rw.[Date]), DAY(rw.[Date]), rw.Id, p.Id, p.Done";
                s += " union select l.LiftId, t.Ttx, p.TpId, ui.Family, ui.IO, YEAR(ISNULL(rw.[Date],p.[Date])) as y, MONTH(ISNULL(rw.[Date],p.[Date])) as M, DAY(ISNULL(rw.[Date],p.[Date])) as d, 0 as rwId, p.Id as planId, p.Done from [Plan] p " +
                    "left join ReglamentWorks rw on rw.PlanId=p.Id " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 " +
                    "join UserInfo ui on ui.UserId=p.UserId " + w +
                    " group by l.LiftId, t.Ttx, p.TpId, ui.Family, ui.IO, YEAR(ISNULL(rw.[Date],p.[Date])), MONTH(ISNULL(rw.[Date],p.[Date])), DAY(ISNULL(rw.[Date],p.[Date])), p.Id, p.Done";

                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.Parameters.AddWithValue("DateStart", Calendar.SelectedDate.Date);
                cmd.Parameters.AddWithValue("DateEnd", CalendarEnd.SelectedDate.Date.AddDays(1));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string url = string.Empty;
                    int rwId = Int32.Parse(dr["rwId"].ToString());
                    if (rwId != 0)
                        url = "~/Prim.aspx?rwId=" + rwId;
                    string name = dr["family"].ToString() + " " + dr["IO"].ToString();
                    string done = (bool.Parse(dr["Done"].ToString()) ? "выполнено" : "не выполнено");
                    r.Add(new Report()
                    {
                        Address = dr["Ttx"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Date = dr["y"].ToString() + "." + dr["M"].ToString() + "." + dr["d"].ToString(),
                        Name = name,
                        TpId = dr["TpId"].ToString(),
                        Url = url,
                        UrlTitle = string.IsNullOrEmpty(url) ? "" : "Замечание",
                        PlanId = "~/Reglament.aspx?PlanId=" + Int32.Parse(dr["planId"].ToString()) + "&ret=" + HttpUtility.HtmlEncode(Request.Url),
                        Done = done
                    });
                }

                // удаление работ без замечаний, дублирующих работы с замечаниями
                for (int i = 0; i < r.Count; i++)
                {
                    if (string.IsNullOrEmpty(r[i].UrlTitle))
                    {
                        Report j = r.Find(delegate(Report f)
                        {
                            return (f.UrlTitle == "Замечание" && f.LiftId==r[i].LiftId && f.Date==r[i].Date && f.TpId==r[i].TpId);
                        });
                        if (j != null)
                            r.RemoveAt(i--);
                    }
                }

                Out.DataSource = r;
                Out.DataBind();

                Qst.Visible = false;
                phReport.Visible = true;
            }
        }

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/ReglamentReport.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            Response.Redirect("~/About.aspx");
            return string.Empty;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IdU.Visible = !IdU.Visible;
            if (IdU.Visible)
                IdU.FindControl("SelectAll").Focus();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            IdM.Visible = !IdM.Visible;
            if (IdM.Visible)
                IdM.FindControl("SelectAll").Focus();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IdL.Visible = !IdL.Visible;
            if (IdL.Visible)
                IdL.FindControl("SelectAll").Focus();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Address.Visible = !Address.Visible;
            if (Address.Visible)
                Address.FindControl("SelectAll").Focus();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            WorkType.Visible = !WorkType.Visible;
            if (WorkType.Visible)
                WorkType.FindControl("SelectAll").Focus();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Worker.Visible = !Worker.Visible;
            if (Worker.Visible)
                Worker.FindControl("SelectAll").Focus();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Done.Visible = !Done.Visible;
            if (Done.Visible)
                Done.FindControl("SelectAll").Focus();
        }
    }
}