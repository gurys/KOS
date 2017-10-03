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
    public partial class PrimEditTsg : System.Web.UI.Page
    {
       
        int _wz = 0;
        class aType
        {
            public aType(string s) { Email = s; }
            public string Email { get; set; }
        }
       
        string _role = string.Empty;      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]); 
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetEvent(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                Id.Text = _wz.ToString();
                Sourse.Text = data.Rows[0]["Sourse"].ToString();               
                IO.Text = data.Rows[0]["IO"].ToString();
                DataId.Text = data.Rows[0]["DataId"].ToString();
                RegistrId.Text = data.Rows[0]["RegistrId"].ToString();                
                LiftId.Text = data.Rows[0]["LiftId"].ToString();
                TypeId.Text = data.Rows[0]["TypeId"].ToString();
                EventId.Text = data.Rows[0]["EventId"].ToString();               
                ToApp.Text = data.Rows[0]["ToApp"].ToString();               
                DateToApp.Text = data.Rows[0]["DateToApp"].ToString();
                Who.Text = data.Rows[0]["Who"].ToString();
                Comment.Text = data.Rows[0]["Comment"].ToString();
                DateWho.Text = data.Rows[0]["DateWho"].ToString();
                Text.Text = data.Rows[0]["Prim"].ToString();
                Prim.Text = data.Rows[0]["Prim"].ToString();               
                string d = string.Empty;
                string d2 = string.Empty, t2 = string.Empty;
                TimeSpan pr = DateTime.Now - ((DateTime)data.Rows[0]["DataId"]);
                if (!(data.Rows[0]["DateWho"] is DBNull))
                {
                    d2 = ((DateTime)data.Rows[0]["DateWho"]).Date.ToString();
                    t2 = ((DateTime)data.Rows[0]["DateWho"]).TimeOfDay.ToString();
                    pr = ((DateTime)data.Rows[0]["DateWho"]) - ((DateTime)data.Rows[0]["DataId"]);
                }
                if (!(data.Rows[0]["DateToApp"] is DBNull))
                {
                    d = ((DateTime)data.Rows[0]["DateToApp"]).ToString();
                }
                string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                    pr.Minutes.ToString();
                Timing.Text = prostoy; 
          
            }

            PIN.Visible = false;
           
        }  
        
        //редактирование
        protected void Edit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
           
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                string s = "update Events set Prim=@p, Cansel=@c where Id=@i";
                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.Parameters.AddWithValue("p", Text.Text);
                cmd.Parameters.AddWithValue("i", _wz);
                cmd.Parameters.AddWithValue("c", false);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/PrimTSG.aspx");
                 
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
         
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetPin(Pn.Text, User.Identity.Name);
                try
                {
                    Text.Text = "[" + data.Rows[0]["surname"].ToString() + " " + data.Rows[0]["name"].ToString() + data.Rows[0]["midlename"].ToString() + "]";
                    PIN.Visible = true;
                    Vpin.Visible = false;
                }
                catch { Msg.Text = "Неверный пин-код!"; return; }
          
        }
    }
}