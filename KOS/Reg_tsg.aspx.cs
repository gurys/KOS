using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Threading;

namespace KOS
{
    public partial class Reg_tsg : System.Web.UI.Page 
    {
        
        int _type = 0;
        string _role, _wz;

        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Sourse { get; set; } 
            public string IO { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }            
            public string RegistrId { get; set; }
            public string LiftId { get; set; }
            public string TypeId { get; set; }           
            public string EventId { get; set; }
            public string ToApp { get; set; } 
            public string DateToApp { get; set; }
            public string Who { get; set; }
            public string Comment { get; set; }
            public string Date2 { get; set; }
            public string Time2 { get; set; }
            public string Prim { get; set; }
            public string Timing { get; set; }
            public string Address { get; set; } 
           
        }
        public class ReadFromInternet
        {
            public XmlDocument MakeRequest()
            {
                string date_from = DateTime.Now.AddDays(-1).ToString();
                string date_to = DateTime.Now.ToString();
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://service.qtelecom.ru/public/http/?user=36851&pass=67448501&gzip=none&action=inbox&sib_num=1745&new_only=0&date_from=" + date_from + "&date_to=" + date_to + "&");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(new StreamReader(response.GetResponseStream(), Encoding.UTF8));
                    return doc;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();
            Date.Text = DateTime.Now.ToLongDateString();
            if (!string.IsNullOrEmpty(Request["t"]))
                _type = int.Parse(Request["t"]);

            if (!IsPostBack)
            {
                KOS.App_Code.ClearTemp clear = new App_Code.ClearTemp(Request);
                clear.DeleteOld();

                UpdateLabel();
                if (User.Identity.Name == "Миракс_Парк") ButtonReg.Visible = false;
                else if (User.Identity.Name == "Корона_1") ButtonReg.Visible = false;
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
                    What.Text = "Не закрытые события на " + DateTime.Now.Date.ToShortDateString();
                    break;
               
            }
        }
        
        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
            string url = "~/";
            if (User.Identity.Name == "Миракс_Парк" || User.Identity.Name == "Корона_1" ) url = "~/PrimEditTsg.aspx?zId=";
            else if (User.Identity.Name == "ODS14") url = "~/ZakTSG.aspx?zId=";
            else if (User.Identity.Name == "ODS_test") url = "~/ZakTSG.aspx?zId=";
            else url = "~/ZakTSG.aspx?zId=";
         //   string url1 = "~/ZayavkaEditODS.aspx?zId=";
         //   string url2 = "~/WZView.aspx?zId=";
            
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string s = "select e.Id, e.EventId, e.RegistrId, e.DataId, e.ZayavId, e.WZayavId, e.Sourse, e.Family, e.IO, " +
                    "e.TypeId, e.LiftId, e.Who, e.ToApp, e.DateWho, e.DateToApp, e.Comment, e.Prim, e.Address from Events e " +
                    "where e.Family=@user";
                if (_type == 0) // Активные события
                {
                    s += " and Cansel=N'false'";
                    cmd = new SqlCommand(s, conn);
                   
                        if ( User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                        else if (User.Identity.Name == "Корона_1")
                            cmd.Parameters.AddWithValue("user", "ODS14");
                    
                    else
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 1) // не закрытые все
                {
                    s += " and Cansel=N'false'";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name); 
                }
                else return data;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   
                    string d = string.Empty;                    
                    string d2 = string.Empty, t2 = string.Empty;
                    TimeSpan pr = DateTime.Now - ((DateTime)dr["DataId"]);
                    if (!(dr["DateWho"] is DBNull))
                    {
                        d2 = ((DateTime)dr["DateWho"]).Date.ToString();
                        t2 = ((DateTime)dr["DateWho"]).TimeOfDay.ToString();
                        pr = ((DateTime)dr["DateWho"]) - ((DateTime)dr["DataId"]);
                    }
                    if (!(dr["DateToApp"] is DBNull))
                    {
                        d = ((DateTime)dr["DateToApp"]).ToString();
                    }
                    string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                        pr.Minutes.ToString();
                    data.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        Sourse = dr["Sourse"].ToString(),
                        IO = dr["IO"].ToString(),
                        Date1 = dr["DataId"].ToString(),
                  //      Date1 = ((DateTime)dr["DataId"]).Date.ToShortDateString(),
                  //      Time1 = ((DateTime)dr["DataId"]).ToShortTimeString(),                        
                        RegistrId = dr["RegistrId"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        TypeId = dr["TypeId"].ToString(),
                        EventId = dr["EventId"].ToString(),
                    //    ZayavId = dr["ZayavId"].ToString(),
                    //    Url1 = (_role == "worker" ? "#" : url1 + dr["ZayavId"].ToString()),
                    //    WZayavId = dr["WZayavId"].ToString(),
                    //    Url2 = (_role == "worker" ? "#" : url2 + dr["WZayavId"].ToString()),
                   
                        ToApp = dr["ToApp"].ToString(),
                        DateToApp = d,
                        Who = dr["Who"].ToString(),
                        Comment = dr["Comment"].ToString(),
                        Date2 = dr["DateWho"].ToString(),
                        Prim = dr["Prim"].ToString(),
                        Address = dr["Address"].ToString(),
                    //    Date2 = d2,
                    //    Time2 = t2,
                        Timing = prostoy
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

            List<string> roles = new List<string>() { "ODS_tsg", "Administrator", "ManagerTSG", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS_tsg";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ManagerTSG";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            Response.Redirect("~/About.aspx");
            return "";
        }
        protected void InSmsText_Click(object sender, EventArgs e)
        {
            try
            {
                ReadFromInternet sms = new ReadFromInternet();
                XmlDocument doc = sms.MakeRequest();
                XmlNode icx = doc["output"]["inbox"]["MESSAGE"]["CREATED"];
                XmlNode isx = doc["output"]["inbox"]["MESSAGE"]["SMS_SENDER"];
                XmlNode itx = doc["output"]["inbox"]["MESSAGE"]["SMS_TEXT"];
                XmlNode ist = doc["output"]["inbox"]["MESSAGE"]["SMS_STATUS"];

                if (icx != null && icx.HasChildNodes)
                {
                    for (int i = 0; i < icx.ChildNodes.Count; i++)
                    {
                        Creat.Text = icx.ChildNodes[i].InnerText;

                    }
                }// когда отправлено
                if (isx != null && isx.HasChildNodes)
                {
                    for (int i = 0; i < isx.ChildNodes.Count; i++)
                    {
                        Sender.Text = isx.ChildNodes[i].InnerText;

                    }
                } // кто
                if (itx != null && itx.HasChildNodes)
                {
                    for (int i = 0; i < itx.ChildNodes.Count; i++)
                    {
                        NumEv.Text = itx.ChildNodes[i].InnerText.Substring(5);
                    }
                } // номер события
                if (ist != null && ist.HasChildNodes)
                {
                    for (int i = 0; i < ist.ChildNodes.Count; i++)
                    {
                        Status.Text = ist.ChildNodes[i].InnerText;
                    }
                } // статус
                if (Status.Text == "N")
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        string _prinyal = String.Empty;
                        string _worker = String.Empty;
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("select ZayavId from Events e" +
                         " where e.Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", NumEv.Text);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            _wz = dr[0].ToString();
                        }
                        dr.Close();
                        cmd = new SqlCommand("update Zayavky " +
                           "set Prinyal=@w, PrinyalDate=@f, [Status]=@s where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", _wz);
                        if (Sender.Text == "79264062614") { _prinyal = "d9c074a1-f0c4-4da3-afe4-0f55b364d6bc"; _worker = "Гурьянов С.П."; }
                        else if (Sender.Text == "79269338001") { _prinyal = "d9c074a1-f0c4-4da3-afe4-0f55b364d6bc"; _worker = "Викулов А.В."; }
                        else if (Sender.Text == "79268976775") { _prinyal = "d2425029-3b24-4153-8b4e-07e1e4ef0009"; _worker = "Дежурная служба уч.1,3"; }
                        else if (Sender.Text == "79253135718") { _prinyal = "d2425029-3b24-4153-8b4e-07e1e4ef0009"; _worker = "Дежурная служба уч.1,4"; }
                        else if (Sender.Text == "79264610904") { _prinyal = "d2425029-3b24-4153-8b4e-07e1e4ef0009"; _worker = "Дежурная служба уч.2,1.2,5"; }
                        else if (Sender.Text == "79296758809") { _prinyal = "d2425029-3b24-4153-8b4e-07e1e4ef0009"; _worker = "Дежурная служба уч.2,2"; }
                        else if (Sender.Text == "79267270995") { _prinyal = "d2425029-3b24-4153-8b4e-07e1e4ef0009"; _worker = "Дежурная служба уч.4,1"; }
                        else if (Sender.Text == "79629908871") { _prinyal = "d2425029-3b24-4153-8b4e-07e1e4ef0009"; _worker = "Дежурная служба уч.4,2"; }
                        else return;
                        cmd.Parameters.AddWithValue("w", _prinyal); //
                        cmd.Parameters.AddWithValue("f", Convert.ToDateTime(Creat.Text));
                        cmd.Parameters.AddWithValue("s", false);
                        cmd.ExecuteNonQuery();

                        // проверка истории, обновление события

                        cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                        cmd.Parameters.AddWithValue("ne", NumEv.Text);
                        cmd.Parameters.AddWithValue("d", Convert.ToDateTime(Creat.Text));
                        cmd.Parameters.AddWithValue("u", User.Identity.Name);
                        cmd.Parameters.AddWithValue("f", _worker);
                        cmd.Parameters.AddWithValue("txt", "ответное смс");
                        cmd.Parameters.AddWithValue("cat", "принятие события");
                        cmd.Parameters.AddWithValue("ph", NumEv.Text);
                        cmd.Parameters.AddWithValue("to", "ODS");
                        cmd.Parameters.AddWithValue("cm", "Принято по № " + Sender.Text);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update Events " +
                         "set ToApp=@w, DateToApp=@f where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", NumEv.Text);
                        cmd.Parameters.AddWithValue("f", Convert.ToDateTime(Creat.Text));
                        cmd.Parameters.AddWithValue("w", _worker);
                        cmd.ExecuteNonQuery();
                    }
            }
            catch { Response.Redirect("~/Reg_tsg.aspx"); }
            Response.Redirect("~/Reg_tsg.aspx");
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
            Response.Redirect("~/DiagrammTSG.aspx");
        }
        protected void ZakrytieODS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ZakTSG.aspx");
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
        protected void OdsHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ODS_TSG.aspx");
        }
    }

}