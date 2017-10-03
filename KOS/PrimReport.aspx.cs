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
    public partial class PrimReport : System.Web.UI.Page
    {
        public class Data
        {
            public string Title { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();

                    Bind(conn, "select IdU from Lifts group by IdU", IdU);
                    Bind(conn, "select IdM from Lifts group by IdM", IdM);
                    Bind(conn, "select IdL from Lifts group by IdL", IdL);
                    Bind(conn, "select Ttx from Ttx where TtxTitleId=1 group by Ttx", Address);
                    List<Data> data = new List<Data>();
                    data.Add(new Data() { Title = "1" });
                    data.Add(new Data() { Title = "2" });
                    Category.DataSource = data;
                    Category.DataBind();
                    data = new List<Data>();
                    data.Add(new Data() { Title = "устранено" });
                    data.Add(new Data() { Title = "не устранено" });
                    Done.DataSource = data;
                    Done.DataBind();
                    Start.SelectedDate = new DateTime(2016, 1, 1);
                    End.SelectedDate = DateTime.Now.Date;
                }
            }
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

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry", "Worker" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
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
                Done.FindControl("SelectAll").Focus();
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
            lDate.Text = " с " + Start.SelectedDate.ToLongDateString() + " по " +
                End.SelectedDate.ToLongDateString();
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
            if (IsAllSelected(Done))
                lDone.Text = " любые";
            else
                lDone.Text = GetSelected(Done);
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

        string BuildDone(ListView lv, string w, string p)
        {
            if (w != null && !IsAllSelected(lv))
            {
                List<string> u = GetSelectedTitles(lv);
                if (u.Count < 1) return null;
                if (string.IsNullOrEmpty(w))
                    w = "where ";
                else
                    w += "and ";
                if (u[0] == "выполнено")
                    w += p + "=1";
                else
                    w += p + "=0";
            }
            return w;
        }

        string BuildCategory(ListView lv, string w, string p)
        {
            if (w != null && !IsAllSelected(lv))
            {
                List<string> u = GetSelectedTitles(lv);
                string tu = string.Empty;
                foreach (string i in u)
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

        string BuildPeriod(string w, string p)
        {
            if (w != null)
            {
                if (string.IsNullOrEmpty(w))
                    w = "where ";
                else
                    w += "and ";
                w += p + " between @Start and @End ";
            }
            return w;
        }

        class Report
        {
            public string LiftId { get; set; }
            public string Address { get; set; }
            public string Url { get; set; }
            public string Category { get; set; }
            public string Done { get; set; }
            public string Date { get; set; }
            public string UserName { get; set; }
        }

        protected void DoIt_Click(object sender, EventArgs e)
        {
            FillLabels();
            
            List<Report> r = new List<Report>();            
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                string s = "select rw.Id, rw.[Index], p.LiftId, p.[Date], rw.Done, ui.Family, ui.IO, t.Ttx  from ReglamentWorks rw " +
                    "join [Plan] p on p.Id=rw.PlanId join Lifts l on l.LiftId=p.LiftId " +
                    "join UserInfo ui on ui.UserId=rw.UserId join LiftsTtx lt on lt.LiftId=p.LiftId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 ";
                string w = string.Empty;
                w = BuildVarcharWhere(IdU, w, "l.IdU");
                w = BuildVarcharWhere(IdM, w, "l.IdM");
                w = BuildVarcharWhere(IdL, w, "l.IdL");
                w = BuildVarcharWhere(Address, w, "t.Ttx");
                w = BuildCategory(Category, w, "rw.[Index]");
                w = BuildDone(Done, w, "rw.Done");
                w = BuildPeriod(w, "p.[Date]");
                if (w == null) return;
                s += w + " and rw.Prim is not null";
                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.Parameters.AddWithValue("Start", Start.SelectedDate);
                cmd.Parameters.AddWithValue("End", End.SelectedDate);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    r.Add(new Report()
                    {
                        LiftId = dr["LiftId"].ToString(),
                        Address = dr["Ttx"].ToString(),
                        Url = "~/Prim.aspx?rwId=" + dr["Id"].ToString(),
                        Category = dr["Index"].ToString(),
                        Done = (bool.Parse(dr["Done"].ToString()) ? "выполнено" : "висит"),
                        Date = ((DateTime)dr["Date"]).ToShortDateString(),
                        UserName = dr["Family"].ToString() + " " + dr["IO"].ToString()
                    });
                }
                dr.Close();

                s = "select z.Id, z.Category, z.LiftId, z.[Date], z.Done, ui.Family, ui.IO, t.Ttx  from ZPrim z " +
                    "join Lifts l on l.LiftId=z.LiftId " +
                    "join UserInfo ui on ui.UserId=z.WhoWrote join LiftsTtx lt on lt.LiftId=z.LiftId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 ";
                w = string.Empty;
                w = BuildVarcharWhere(IdU, w, "l.IdU");
                w = BuildVarcharWhere(IdM, w, "l.IdM");
                w = BuildVarcharWhere(IdL, w, "l.IdL");
                w = BuildVarcharWhere(Address, w, "t.Ttx");
                w = BuildCategory(Category, w, "z.Category");
                w = BuildDone(Done, w, "z.Done");
                w = BuildPeriod(w, "z.[Date]");
                if (w == null) return;
                s += w;
                cmd = new SqlCommand(s, conn);
                cmd.Parameters.AddWithValue("Start", Start.SelectedDate);
                cmd.Parameters.AddWithValue("End", End.SelectedDate);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    r.Add(new Report()
                    {
                        LiftId = dr["LiftId"].ToString(),
                        Address = dr["Ttx"].ToString(),
                        Url = "~/ZPrimEdit.aspx?Id=" + dr["Id"].ToString(),
                        Category = dr["Category"].ToString(),
                        Done = (bool.Parse(dr["Done"].ToString()) ? "устранено" : "не устранено"),
                        Date = ((DateTime)dr["Date"]).ToShortDateString(),
                        UserName = dr["Family"].ToString() + " " + dr["IO"].ToString()
                    });
                }
                dr.Close();

                r.Sort(delegate(Report r1, Report r2)
                {
                    int i = DateTime.Compare(DateTime.Parse(r1.Date), DateTime.Parse(r2.Date));
                    if (i != 0) return i;
                    return string.Compare(r1.LiftId, r2.LiftId);
                });
            }            
            Out.DataSource = r;
            Out.DataBind();

            Qst.Visible = false;
            phReport.Visible = true;
        }
    }
}