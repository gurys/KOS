using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using KOS.App_Code;
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace KOS
{
    public partial class Zayavka : System.Web.UI.Page
    {
        class ListData : Object
        {
            public string Title { get; set; }
            public int Id { get; set; }
            public override string ToString()
            {
                return Title;
            }
        }
        List<ListData> addresses = new List<ListData>();
        string _role;

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd;
                if (_role == "ODS")
                {
                    cmd = new SqlCommand("select tt.Ttx, tt.Id from Ttx tt " +
                        "join LiftsTtx lt on lt.TtxId=tt.Id " +
                        "join ODSLifts o on o.LiftId=lt.LiftId " +
                        "join Users u on u.UserId=o.UserId " +
                        "where tt.TtxTitleId=1 and u.UserName=@userName " +
                        "group by tt.Ttx, tt.Id", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                }
                else
                {
                    cmd = new SqlCommand("select tt.Ttx, tt.Id from Ttx tt " +
                        "where tt.TtxTitleId=1 " +
                        "group by tt.Ttx, tt.Id", conn);
                }

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    addresses.Add(new ListData() { Title = dr[0].ToString(), Id = Int32.Parse(dr[1].ToString()) });
                dr.Close();
                if (!IsPostBack)
                {
                    Address.DataSource = addresses;
                    Address.DataBind();
                    Address.SelectedIndex = 0;

                    Address_TextChanged(this, EventArgs.Empty);

                    List<string> category = new List<string>() { "застревание", "останов", "заявка" , "плановые работы", "внеплановые ремонты"};
                    if (_role == "Cadry" || _role == "Manager" || _role == "Administrator")
                    {
                        Category.DataSource = category;
                        Category.DataBind();
                    }
                    else
                        Cat.Visible = false;

                    cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                        "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                        Disp.Text = dr[0].ToString() + " " + dr[1].ToString();
                    dr.Close();
                }
            }
        }

       const string ipConfig = "c:\\ARC\\kos.xml";

        void SaveFirst(XmlDocument doc)
        {
            doc.LoadXml("<zayavka></zayavka>");
            XmlNode root = doc.DocumentElement;
            XmlElement elem = doc.CreateElement(User.Identity.Name);
            XmlNode user = root.AppendChild(elem);
            XmlElement first = doc.CreateElement("x001");
            first.InnerText = Request.UserHostAddress;
            user.AppendChild(first);
            doc.Save(ipConfig);
        }

        void SaveNext(XmlDocument doc)
        {
            XmlNode root = doc.DocumentElement;
            XmlElement elem = doc.CreateElement(User.Identity.Name);
            XmlNode user = root.AppendChild(elem);
            XmlElement first = doc.CreateElement("x001");
            first.InnerText = Request.UserHostAddress;
            user.AppendChild(first);
            doc.Save(ipConfig);
        } 

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Zayavka.aspx");

            List<string> roles = new List<string>() { "ODS" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                XmlDocument doc = new XmlDocument();
                if (!File.Exists(ipConfig))
                    SaveFirst(doc);
                XmlTextReader reader = new XmlTextReader(ipConfig);
                doc.Load(reader);
                XmlNode ipx = doc["zayavka"][User.Identity.Name];
                reader.Close();
                if (ipx != null && ipx.HasChildNodes)
                {
                    for (int i = 0; i < ipx.ChildNodes.Count; i++)
                    {
                        string ip = ipx.ChildNodes[i].InnerText;
                        if (ip == Request.UserHostAddress || ip == "*")
                            return "ODS";
                    }
                    Response.Redirect("~/About.aspx");
                }
                else
                    SaveNext(doc);
                return "ODS";
            }
            roles = new List<string>() { "Administrator", "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            roles = new List<string>() { "Cadry " };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Cadry";

            Response.Redirect("~/About.aspx");
            return null;
        }

        protected void Address_TextChanged(object sender, EventArgs e)
        {
            if (Address.SelectedItem == null)
                return;
            string a = Address.SelectedItem.Value;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd;
                if (_role == "ODS")
                {
                    cmd = new SqlCommand("select lt.LiftId from LiftsTtx lt " +
                    "join ODSLifts o on o.LiftId=lt.LiftId " +
                    "join Users u on u.UserId=o.UserId " +
                    "join Ttx tt on tt.Id=lt.TtxId " +
                    "where tt.TtxTitleId=1 and u.UserName=@userName and tt.Ttx=@t", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                }
                else
                    cmd = new SqlCommand("select lt.LiftId from LiftsTtx lt " +
                    "join Ttx tt on tt.Id=lt.TtxId " +
                    "where tt.TtxTitleId=1 and tt.Ttx=@t", conn);
                cmd.Parameters.AddWithValue("t", a);
                List<string> lifts = new List<string>();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    lifts.Add(dr[0].ToString());
                dr.Close();
                Lift.DataSource = lifts;
                Lift.DataBind();
                Lift.SelectedIndex = 0;
            }
        }
     
        protected void Save_Click(object sender, EventArgs e)
        {
            ListData ttx = addresses.Find(delegate(ListData i)
            {
                return (i.Title == Address.SelectedValue);
            });
            if (ttx == null)
                return;
            int ttxId = ttx.Id;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand("insert into Zayavky " +
                    "(TtxId, LiftId, UserId, [Text], Category, [From], [Start]) " +
                    "values (@ttxId, @liftId, (select UserId from Users where UserName=@user), @text, @c, @f, @s)", conn);
                cmd.Parameters.AddWithValue("ttxId", ttxId);
                cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("text", Text.Text);
                if (_role == "ODS")
                    cmd.Parameters.AddWithValue("c", Category.SelectedValue);
                else
                    cmd.Parameters.AddWithValue("c", Category.SelectedValue); //Было "заявка" в начальной версии
                if (_role == "ODS")
                    cmd.Parameters.AddWithValue("f", "ОДС");
                else
                    cmd.Parameters.AddWithValue("f", "менеджер");
                cmd.Parameters.AddWithValue("s", date);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("select z.Id from Zayavky z " +
                    "join Users u on z.UserId=u.UserId " +
                    "where u.UserName=@user and z.LiftId=@liftId and [Start]=@s", conn);
                cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("s", date);
                int id = int.Parse(cmd.ExecuteScalar().ToString());
                KOS.App_Code.Mail mail = new Mail(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                mail.SendMsg(Lift.SelectedValue, int.Parse(id.ToString()));
                //sms response
                string nw = "9269308001";
                string TextSms = Category.SelectedValue + "-адрес:" + Address.SelectedItem.Value + "-" + "лифт №:" + Lift.SelectedValue + "-" + Text.Text;
                string myApiKey = "27B482E1-14AE-ACFB-C500-CCEC9C763C99"; //Ваш API ключ
                SmsRu.SmsRu sms = new SmsRu.SmsRu(myApiKey);
                var response = sms.Send(nw, TextSms);
                Msg.Text = "Ваша заявка зарегистрирована, отправлена по Email и СМС Администратору. Для звонка Администратору используйте кнопку на странице.";

             }
        }
    }
}
