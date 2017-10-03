using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
namespace KOS
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
        }
       
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Reports.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                WZReport.Visible = true;
                Lifts.Visible = true;
                AdminUM.Visible = true;
                DocViewUM.Visible = true;
                DocumView.Visible = true;
                ReportsTSG.Visible = true;
                PartsList.Visible = true;

                return;
            }
            roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                WZReport.Visible = true;
                return;
            }
            roles = new List<string>() { "ODS", "Electronick" };
            if (db.CheckAccount(User.Identity.Name, roles))
                return;
            Response.Redirect("~/About.aspx");
        }
    }
}