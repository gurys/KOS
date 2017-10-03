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
    public partial class ODS_TSG : System.Web.UI.Page
    {
        DateTime _from;
        string _wz;
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
          
                CheckAccount();
                _from = DateTime.Now.AddDays(-2);
                if (!IsPostBack)
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        /*
                        cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                            "join UsersInRoles uir on uir.UserId=z.UserId " +
                            "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                            "join Users u on z.UserId=u.UserId " +
                            "where z.Category=N'заявка' and z.Finish is null and u.UserName=@user", conn);
                        cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        Text6 = cmd.ExecuteScalar().ToString();
                        //   HyperLink6.Text = cmd.ExecuteScalar().ToString();
                         */
                        // Активные события
                        SqlCommand cmd = new SqlCommand("select count(e.Id) from Events e " +
                            "where e.Cansel=N'false' and e.Family=@user", conn);
                        cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        //   Text2 = cmd.ExecuteScalar().ToString();
                        HyperLink2.Text = cmd.ExecuteScalar().ToString();
                        // HyperLink3.Text = cmd.ExecuteScalar().ToString();
                        // Не назначенные события
                        cmd = new SqlCommand("select count(e.Id) from Events e " +
                           "where e.Cansel=N'false' and e.Family=@user and ToApp=N' '", conn);
                        cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        //   Text2 = cmd.ExecuteScalar().ToString();
                        HyperLink1.Text = cmd.ExecuteScalar().ToString();
                        cmd = new SqlCommand("select count(d.Id) from Documents d " +
                           "join Events e on e.Id=d.NumEvent " +
                           "where d.Status=N'не подписан заказчиком' and e.Family=@user", conn);
                        cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        //   Text2 = cmd.ExecuteScalar().ToString();
                        HyperLink3.Text = cmd.ExecuteScalar().ToString();
                        // подсчет активных событий
                        //   int all = int.Parse(HyperLink2.Text) + int.Parse(HyperLink3.Text) + int.Parse(HyperLink4.Text) + int.Parse(HyperLink6.Text)+ int.Parse(HyperLink8.Text);
                        //  int all = int.Parse(Text2) + int.Parse(Text3) + int.Parse(Text4) + int.Parse(Text6) + int.Parse(Text8);
                        //  HyperLink1.Text = all.ToString();
                        //App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                        //List<App_Code.Base.StoppedLift> list = db.GetStoppedODSLifts(User.Identity.Name);
                        //int total = db.GetODSLifts(User.Identity.Name);
                        //Worked.Text = (total - list.Count).ToString();
                        //Stopped.Text = list.Count.ToString();
                    }
                }

             //   InSmsText_Click(sender, e);
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
            catch { Response.Redirect("~/"); }
            Response.Redirect("~/");
        } 
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "ODS_tsg", "Administrator" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
               protected void ArcButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ArcTsg.aspx");
        }

        protected void DiagrammTSG_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/DiagrammTSG.aspx");
        }
        protected void RegTSG_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/Reg_tsg.aspx");
        }
        protected void RegNZ_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RegNZ_tsg.aspx"); 
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Ods_Doc.aspx"); 
        }
    }
}