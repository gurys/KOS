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
    public partial class ZaprosKP : System.Web.UI.Page
    {
        int _type = 1;
        string _role;

        class Data
        {
           
            public string Url { get; set; }
            public string RegistrId { get; set; }
            public string DataId { get; set; }
            public string Akt { get; set; } 
            public string zId { get; set; }
            public string Sourse { get; set; }
            public string IdU { get; set; }
            public string IdM { get; set; }
            public string LiftId { get; set; }
            public string Foto { get; set; }
            public string namefoto { get; set; }
            public string Name { get; set; }
            public string NumId { get; set; }
            public string Kol { get; set; }
            public string KP { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            _role = CheckAccount();
            //   _role = "ODS";
            if (!string.IsNullOrEmpty(Request["t"]))
                _type = int.Parse(Request["t"]);

            if (!IsPostBack)
            {
                KOS.App_Code.ClearTemp clear = new App_Code.ClearTemp(Request);
                clear.DeleteOld();

                List<Data> data = GetData();
                Out.DataSource = data;
                Out.DataBind();
            }
        }

       
        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
          //  List<Data> data1 = new List<Data>();
         //   List<Data> data2 = new List<Data>(); 
            string url = "~/EventView.aspx?zId=";
            

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                   SqlCommand cmd = new SqlCommand("select Id, EventId, RegistrId, DataId, ZayavId, WZayavId, Sourse, Family, IO, " +
                     "TypeId, IdU, IdM, LiftId, Akt, KP, Foto, namefoto, Name, NumId, Kol from Events e " +
                     "where e.ZaprosKp=N'true' and e.Cansel=N'false'", conn);
                cmd.Parameters.AddWithValue("Akt", true);
                SqlDataReader dr = cmd.ExecuteReader();
            
                while (dr.Read())
                {
                    data.Add(new Data()
                    {
                        zId = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        RegistrId = dr["RegistrId"].ToString(),
                        DataId = dr["DataId"].ToString(),
                        Akt = dr["Akt"].ToString(),
                        KP = dr["KP"].ToString(),
                        Sourse = dr["Family"].ToString() + dr["IO"].ToString(), 
                        IdU = dr["IdU"].ToString(),
                        IdM = dr["IdM"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Name = dr["Name"].ToString(),
                        NumId = dr["NumId"].ToString(),
                        Kol = dr["Kol"].ToString(),
                        namefoto = dr["namefoto"].ToString(),
                        Foto = dr["Foto"].ToString()
                    });
                }
                dr.Close();
                
                
            }
           return data;
            
        }

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "ODS", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            Response.Redirect("~/About.aspx");
            return "";
        }

        protected void DoIt_Click(object sender, EventArgs e)
        {
           
            List<Data> data = GetData();
            Out.DataSource = data;
            Out.DataBind();
            // Period.Visible = false;
            //  phReport.Visible = true;
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
                    Label lb = (Label)item.FindControl("zId");
                    sel.Add(lb.Text);
                }
            }
            return sel;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
        
    }

}