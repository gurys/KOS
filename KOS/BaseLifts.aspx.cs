using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using KOS.App_Code;

namespace KOS
{
    public partial class BaseLifts : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount(); 
               
        }
           
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/BaseLifts.aspx");

            List<string> roles = new List<string>() { "Cadry", "Administrator", "Manager", "Worker" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}