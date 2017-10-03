using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace KOS
{
    public partial class Lik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Lik.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Administrator", "Manager", "Electronick", "Worker", "Cadry", "ODS", "ODS_tsg", "ManagerTSG" };
            if (!db.CheckAccount(User.Identity.Name, roles))
                this.Enviroment.Visible = false;
            roles = new List<string>() { "Administrator", "Manager", "Worker", "Cadry", "ODS", "ODS_tsg", "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                Journal.Visible = true;
                return;
            }
            Response.Redirect("~/About.aspx");
        }
    }
}