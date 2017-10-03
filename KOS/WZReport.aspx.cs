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
    public partial class WZReport : System.Web.UI.Page
    {
        List<Base.PersonData> workerData = new List<Base.PersonData>();
        class aType
        {
            public aType(string s) { Title = s; }
            public string Title { get; set; }
        }
        List<aType> _type = new List<aType>()
        {
            new aType("заявки прорабу"), new aType("заявки механику")
        };
        string _role = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            workerData = db.GetWZPersons();

            if (!IsPostBack)
            {
                Calendar.SelectedDate = new DateTime(2015, 1, 1);
                CalendarEnd.SelectedDate = DateTime.Now.Date;

                Type.DataSource = _type;
                Type.DataBind();

                From.DataSource = workerData;
                From.DataBind();
                if (_role == "Worker")
                {
                    ph1.Visible = false;
                    ph2.Visible = true;
                }
                else
                {
                    ph1.Visible = true;
                    ph2.Visible = false;
                }
            }
        }

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/WZReport.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Worker", "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            roles = new List<string>() { "Administrator", "Manager", "Cadry"};
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            Response.Redirect("~/About.aspx");
            return string.Empty;
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

        string BuildWhere()
        {
            string s = string.Empty;
            s = " where wz.[Date] between @start and @end";
            if (_role == "Worker")
            {
                if (SelectDone.Checked && SelectNotDone.Checked)
                    lType.Text = "все";
                else if (SelectDone.Checked)
                {
                    s += " and wz.[Done]=1";
                    lType.Text = "прорабу";
                }
                else if (SelectNotDone.Checked)
                {
                    s += " and wz.[Done]=0";
                    lType.Text = "механик";
                }
                else
                    return null;
            }
            else
            {
                if (!IsAllSelected(Type))
                {
                    s += " and wz.[Done]=";
                    List<string> sel = GetSelectedTitles(Type);
                    if (sel.Count < 1) return null;
                    if (sel[0] == _type[0].Title)
                        s += "1";
                    else
                        s += "0";
                    lType.Text = sel[0];
                }
                else
                    lType.Text = "все";
            }

            if (_role == "Worker")
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                App_Code.Base.UserInfo ui = db.GetUserInfo(Page.User.Identity.Name);
                lFrom.Text = ui.Family + " " + ui.IO;
                Base.PersonData pd = workerData.Find(delegate(Base.PersonData p)
                {
                    return p.Title == lFrom.Text;
                });
                if (pd == null) return null;
                s += " and wz.UserId='" + pd.Id + "'";
            }
            else if (!IsAllSelected(From))
            {
                string from = string.Empty;
                List<string> sel = GetSelectedTitles(From);
                if (sel.Count < 1) return null;
                foreach (string f in sel)
                {
                    Base.PersonData pd = workerData.Find(delegate(Base.PersonData p)
                    {
                        return p.Title == f;
                    });
                    if (pd != null)
                    {
                        if (!string.IsNullOrEmpty(from))
                            from += ",";
                        from += pd.Id;
                        if (!string.IsNullOrEmpty(lFrom.Text))
                            lFrom.Text += ", ";
                        lFrom.Text += f;
                    }
                }
                if (!string.IsNullOrEmpty(from))
                    s += " and wz.UserId in (" + from + ")";
            }
            else
                lFrom.Text = "от всех";
            return s;
        }

        class Report
        {
            public string Fio { get; set; }
            public string Url { get; set; }
            public string Date { get; set; }
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            string w = BuildWhere();
            if (!string.IsNullOrEmpty(w))
            {
                List<Report> data = new List<Report>();
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    string s = "select wz.Id, wz.[Date], ui.Family, ui.[IO] from WorkerZayavky wz " +
                        "join UserInfo ui on ui.UserId=wz.UserId";
                    s += w + " order by wz.[Date]";
                    SqlCommand cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("start", Calendar.SelectedDate.Date);
                    cmd.Parameters.AddWithValue("end", CalendarEnd.SelectedDate.Date.AddDays(1));
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(new Report()
                        {
                            Fio = dr["Family"].ToString() + " " + dr["IO"].ToString(),
                            Date = ((DateTime)dr["Date"]).ToShortDateString(),
                            Url = "clickIt('WZClose.aspx?wz=" + dr["Id"].ToString() + "')"
                        });
                    }
                    dr.Close();
                }
                Out.DataSource = data;
                Out.DataBind();
                Qst.Visible = false;
                phReport.Visible = true;
            }
        }
    }
}