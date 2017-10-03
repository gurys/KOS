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
    public partial class ZPrimAdd : System.Web.UI.Page
    {
        string _liftId = string.Empty;
        string _url = string.Empty;
        List<Base.PersonData> _workers;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["lift"]))
                _liftId = HttpUtility.HtmlDecode(Request["lift"]);
            if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
                _url = HttpUtility.HtmlDecode(Request["ReturnUrl"]);

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _workers = db.GetWorkers();
            if (!IsPostBack)
            {
                To.DataSource = _workers;
                To.DataBind();
                To.SelectedIndex = 0;
                db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> list = db.GetIdU();
                IdU.DataSource = list;
                IdU.DataBind();
                if (!string.IsNullOrEmpty(_liftId))
                {
                    string[] s = _liftId.Split('/');
                    IdU.SelectedValue = s[0];
                }
                else
                    IdU.SelectedIndex = 0;
                IdU_SelectedIndexChanged(sender, e);
                if (!string.IsNullOrEmpty(_liftId))
                {
                    string[] s = _liftId.Split('/');
                    IdM.SelectedValue = s[1];
                    IdM_SelectedIndexChanged(sender, e);
                    LiftId.SelectedValue = _liftId;
                    LiftId_SelectedIndexChanged(sender, e);
                }
                string[] ss = { "1", "2" };
                Category.DataSource = ss;
                Category.DataBind();
                Category.SelectedIndex = 0;
            }
        }

        protected void IdU_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> list = db.GetIdM(IdU.SelectedValue);
            IdM.DataSource = list;
            IdM.DataBind();
            IdM.SelectedIndex = 0;
            IdM_SelectedIndexChanged(sender, e);
        }

        protected void IdM_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> list = db.GetLiftId(IdU.SelectedValue, IdM.SelectedValue);
            LiftId.DataSource = list;
            LiftId.DataBind();
            LiftId.SelectedIndex = 0;
            LiftId_SelectedIndexChanged(sender, e);
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/ZPrimAdd.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Worker", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into ZPrim (LiftId, [Date], WhoWrote, Responce, Category, [To]) " +
                    "values (@liftId, @d, (select UserId from Users where UserName=@userName), @r, @c, @t)", conn);
                cmd.Parameters.AddWithValue("liftId", LiftId.SelectedValue);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                cmd.Parameters.AddWithValue("r", Responсe.Text);
                cmd.Parameters.AddWithValue("c", int.Parse(Category.SelectedValue));
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.Title == To.SelectedValue;
                });
                cmd.Parameters.AddWithValue("t", pd.Id);
                cmd.ExecuteNonQuery();
                Msg.Text = "Замечание добавлено";
                if (!string.IsNullOrEmpty(_url))
                    Response.Redirect(_url);
            }
        }

        protected void LiftId_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            To.SelectedValue = db.GetWorker(LiftId.SelectedValue);
        }
    }
}