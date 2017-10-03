using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace KOS
{
    public partial class Stopped : System.Web.UI.Page
    {
        string idU = string.Empty, idM = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["U"]))
                idU = HttpUtility.HtmlDecode(Request["U"]);
            if (!string.IsNullOrEmpty(Request["M"]))
                idM = HttpUtility.HtmlDecode(Request["M"]);

            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<App_Code.Base.StoppedLift> list = db.GetStoppedLifts(idU, idM);
                Stop.DataSource = list;
                Stop.DataBind();
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Manager", "Administrator", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
    }
}