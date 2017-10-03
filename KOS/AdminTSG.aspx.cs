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
    public partial class AdminTSG : System.Web.UI.Page
    {
        string use;
        protected void Page_Load(object sender, EventArgs e)
        {
          //  PinAdmin.Visible = false;
          //  PinDisp.Visible = false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (User.Identity.Name == "Миракс_Парк")
            {
                PinAdmin.Visible = false;
                PinDisp.Visible = false;                
                use = "ОДС13";
            }
            else if (User.Identity.Name == "Корона_1")
            {
                PinAdmin1.Visible = false;
                PinDisp1.Visible = false; 
                use = "ОДС14";
            }
            else return;
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            DataTable data = db.GetPin(Pn.Text, use);
            try
            {
                Text.Text = "Здравствуйте, " + data.Rows[0]["surname"].ToString() + " " + data.Rows[0]["name"].ToString() + data.Rows[0]["midlename"].ToString() + "!";
                if (User.Identity.Name == "Корона_1") { PinAdmin.Visible = true; PinDisp.Visible = true; }
                if (User.Identity.Name == "Миракс_Парк") { PinAdmin1.Visible = true; PinDisp1.Visible = true; }
                Vpin.Visible = false;
            }
            catch { Msg.Text = "Пароль не верный, введите правильный пароль или обратитесь к аминистратору КОС!"; return; }
          
           
        }
    }
}