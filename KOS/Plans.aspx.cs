using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace KOS
{
    public partial class Plans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Plans.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                Planning.Visible = true;
                Worker.Visible = true;
                return;
            }
       /*     List<string> _roles = new List<string>() { "ODS" };
            App_Code.Base _db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, _roles))
            {
                Planning.Visible = false;
                Worker.Visible = false;
                return;
            } */
            Response.Redirect("~/About.aspx");
        }
    }
}