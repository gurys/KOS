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
    public partial class Reg_wz : System.Web.UI.Page
    {
        int _type = 0;
        string _role;

        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string EventId { get; set; }
            public string RegistrId { get; set; }
            public string DataId { get; set; }
            public string ZayavId { get; set; }
            public string Url1 { get; set; }
            public string WZayavId { get; set; }
            public string Url2 { get; set; }
            public string Sourse { get; set; }
            public string Family { get; set; }
            public string IO { get; set; } 
            public string TypeId { get; set; }
            public string IdU { get; set; }
            public string IdM { get; set; }
            public string LiftId { get; set; }
            public string Address { get; set; } 
           
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

                UpdateLabel();

                List<Data> data = GetData();
                Out.DataSource = data;
                Out.DataBind();
            }
        }

        void UpdateLabel()
        {
            switch (_type)
            {
                case 0:
                    What.Text = "Активные события на " + DateTime.Now.Date.ToShortDateString();
                    break;
                case 1:
                    What.Text = "Закрытые события на " + DateTime.Now.Date.ToShortDateString();
                    break;
               
            }
        }

        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
            string url = "~/EventView.aspx?zId=";
            string url1 = "~/ZayavkaEditODS.aspx?zId=";
            string url2 = "~/WZView.aspx?zId=";
            
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string s = "select Id, EventId, RegistrId, DataId, ZayavId, WZayavId, Sourse, Family, IO, " +
                    "TypeId, IdU, IdM, LiftId, Address from Events " +
                    "where RegistrId=N'Эксплуатация лифтов'";
                if (_type == 0) // Активные события
                {
                    s += "and Cansel=N'false'";
                    cmd = new SqlCommand(s, conn);
                }
                else if (_type == 1) // Закрытые все
                {
                    s += "and Cansel=N'true'";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("rg", 0); 
                }
                else return data;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                  
                    data.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        EventId = dr["EventId"].ToString(),
                        RegistrId = dr["RegistrId"].ToString(),
                        DataId = dr["DataId"].ToString(),
                        ZayavId = dr["ZayavId"].ToString(),
                        Url1 = (_role == "worker" ? "#" : url1 + dr["ZayavId"].ToString()),
                        WZayavId = dr["WZayavId"].ToString(),
                        Url2 = (_role == "worker" ? "#" : url2 + dr["WZayavId"].ToString()),
                        Sourse = dr["Sourse"].ToString(),
                        Family = dr["Family"].ToString(),
                        IO = dr["IO"].ToString(),
                        TypeId = dr["TypeId"].ToString(),
                        IdU = dr["IdU"].ToString(),
                        IdM = dr["IdM"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Address = dr["Address"].ToString()
                        
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
            UpdateLabel();
            List<Data> data = GetData();
            Out.DataSource = data;
            Out.DataBind();
            // Period.Visible = false;
            //  phReport.Visible = true;
        }
        protected void DiagrammODS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DiagrammODS.aspx");
        }
        protected void ZakrytieODS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ZakrytieODS.aspx");
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
        protected void OdsHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OdsHome.aspx");
        }
    }

}