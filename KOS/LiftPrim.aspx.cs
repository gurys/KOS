using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace KOS
{
    public partial class LiftPrim : System.Web.UI.Page
    {
        string _lift = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["lift"]))
                _lift = HttpUtility.HtmlDecode(Request["lift"]);

            if (!IsPostBack && !string.IsNullOrEmpty(_lift))
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<App_Code.Base.PrimView> data = db.GetPrimView(_lift, Request.RawUrl);
                Prim.DataSource = data;
                Prim.DataBind();
            }
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
    }
}