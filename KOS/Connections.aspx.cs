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
    public partial class Connections : System.Web.UI.Page
    {
        List<Base.PersonData> _workers = new List<Base.PersonData>();

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _workers = db.GetWorkers();
            if (!IsPostBack)
            {
                Worker.DataSource = _workers;
                Worker.DataBind();
                Worker.SelectedIndex = 0;
                List<string> ls = db.GetIdU();
                IdU.DataSource = ls;
                IdU.DataBind();
                if (ls.Count > 0)
                    IdU.SelectedIndex = 0;
                IdU_SelectedIndexChanged(sender, e);
            }
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
                return i.Title == Worker.SelectedValue;
            });
            List<Base.UserLift> data = db.GetUserLift(pd.UserName, IdU.SelectedValue, IdM.SelectedValue);
            IdL.DataSource = data;
            IdL.DataBind();
        }

        protected void Worker_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdM_SelectedIndexChanged(sender, e);
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Connections.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager" };
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

        protected void Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from WorkerLifts " +
                    "where UserId=@userId and LiftId in " +
                    "(select LiftId from Lifts where IdU=@idU and IdM=@idM)", conn);
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.Title == Worker.SelectedValue;
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

        protected void Do_Click(object sender, EventArgs e)
        {
            List<string> liftId = new List<string>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select LiftId from Lifts " +
                    "where IdU='2' and IdM='1' group by LiftId", conn);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                    liftId.Add(da[0].ToString());
                da.Close();
            }

            for (int i = 12; i < 13; i++)
            {
                if (i == 2) continue;
                DateTime date = new DateTime(2016, i, 1);
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    foreach (string s in liftId)
                    {
                        string tp = "ТР";
                        if (s == "2/1/06")
                            tp += "2";
                        else if (s == "2/1/03" || s == "2/1/11" || s == "2/1/17")
                            tp += "1";
                        else if (s == "2/1/14")
                            tp += "3";
                        SqlCommand cmd = new SqlCommand("update TpPlan " +
                            "set TpId=@tp where [Date]=@date and LiftId=@lift", conn);
                        cmd.Parameters.AddWithValue("date", date);
                        cmd.Parameters.AddWithValue("lift", s);
                        cmd.Parameters.AddWithValue("tp", tp);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}