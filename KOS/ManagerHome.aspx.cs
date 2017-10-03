using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace KOS
{
    public partial class ManagerHome : System.Web.UI.Page
    {
        DateTime _from;
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
            Label1.Text = "С " + _from.ToString();
          //  Label2.Text = "С " + _from.ToString();
            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "where z.Category=N'застревание' and z.Start>@date", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink1.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "where z.Category=N'застревание' and z.Finish is null", conn);
                    HyperLink2.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "where z.Category=N'останов' and z.Start>@date", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink3.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "where z.Category=N'останов' and z.Finish is null", conn);
                    HyperLink4.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Category=N'заявка' and z.Start>@date", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink5.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Category=N'заявка' and z.Finish is null", conn);
                    HyperLink6.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='Manager' " +
                        "where z.Category=N'заявка' and z.Start>@date", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink7.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='Manager' " +
                        "where z.Category=N'заявка' and z.Finish is null", conn);
                    HyperLink8.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(wz.Id) from WorkerZayavky wz " +
                        "where wz.[Date]>@date", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink9.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(wz.Id) from WorkerZayavky wz " +
                        "where wz.Done=0", conn);
                    HyperLink10.Text = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Category=N'плановые работы' and z.Finish is null", conn);
                    HyperLink11.Text = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Category=N'внеплановые ремонты' and z.Finish is null", conn);
                    HyperLink12.Text = cmd.ExecuteScalar().ToString();
                    // все события
                     cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    //   Text2 = cmd.ExecuteScalar().ToString();
                    HyperLink47.Text = cmd.ExecuteScalar().ToString();
                    // срочные события - 2 дня
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'false' and e.DataId<=@date and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                 //   Text2 = cmd.ExecuteScalar().ToString();
                    HyperLink48.Text = cmd.ExecuteScalar().ToString();
                    // все события без Акта
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.ZaprosMng=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    //   Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink50.Text = cmd.ExecuteScalar().ToString();
                    // срочные события без Акта
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.ZaprosMng=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                //    Text3 = cmd.ExecuteScalar().ToString();
                    HyperLink51.Text = cmd.ExecuteScalar().ToString();
                    // все события с запросом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.ZaprosKp=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    // Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink53.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с запросом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.ZaprosKp=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                //    Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink54.Text = cmd.ExecuteScalar().ToString();
                    // все события с ответом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.KP=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    // Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink56.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ответом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.KP=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                //    Text5 = cmd.ExecuteScalar().ToString();
                    HyperLink57.Text = cmd.ExecuteScalar().ToString();
                    // все события с запросом счета
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.ZapBill=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    // Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink59.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с запросом счета
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.ZapBill=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    //   Text6 = cmd.ExecuteScalar().ToString();
                    HyperLink60.Text = cmd.ExecuteScalar().ToString();
                    // все события со  счетами
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Bill=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink62.Text = cmd.ExecuteScalar().ToString();
                    // срочные события со  счетами
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Bill=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink63.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием оплаты
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Payment=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink20.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием оплаты
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Payment=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink21.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием доставки
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Dostavka=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink35.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием доставки
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Dostavka=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink36.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием прихода
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Prihod=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink38.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием прихода
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Prihod=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink39.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием акта ВР
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.AktVR=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink41.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием акта ВР
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.AktVR=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink42.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием списания
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Spisanie=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink44.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием списания
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Spisanie=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink45.Text = cmd.ExecuteScalar().ToString();
                   // ReadFromInternet sms = new ReadFromInternet();
                   // sms.MakeRequest();
                    App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    List<App_Code.Base.UM> lifts = db.GetUM();
                    Lifts.DataSource = lifts;
                    Lifts.DataBind();

                    List<App_Code.Base.LiftPrim> liftPrim = db.GetLiftPrim();
                    List<App_Code.Base.LiftPrim> liftZPrim = db.GetLiftZPrim();
                    foreach (App_Code.Base.LiftPrim i in liftZPrim)
                    {
                        int n = liftPrim.FindIndex(delegate(App_Code.Base.LiftPrim lp)
                        {
                            return lp.IdUM == i.IdUM;
                        });
                        if (n >= 0)
                            liftPrim[n].N = (int.Parse(liftPrim[n].N) + int.Parse(i.N)).ToString();
                        else
                            liftPrim.Add(i);
                    }
                    liftPrim.Sort(delegate(App_Code.Base.LiftPrim lp1, App_Code.Base.LiftPrim lp2)
                    {
                        return string.Compare(lp1.IdUM, lp2.IdUM);
                    });
                    LiftPrim.DataSource = liftPrim;
                    LiftPrim.DataBind();
                }
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
        protected void WorkerZayavka_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WorkerZayavka.aspx");
        }
        protected void InSms_Click(object sender, EventArgs e)
        {
           //
            string date_from = DateTime.Now.AddDays(-1).ToString();
            string date_to = DateTime.Now.ToString();
            string url = "http://service.qtelecom.ru/public/http/";
            string post = "user=36851&pass=67448501&gzip=none&action=inbox&sib_num=1745&new_only=1&date_from=" + date_from + "&date_to=" + date_to + "&";
          //  string post = "[user]=>36851 [pass]=>67448501 [action]=>inbox [sib_num]=>1745[new_only]=>0 [date_from]=>" + date_from + " [date_to]=>" + date_to;

            Label2.Text = POST(url, post); 
  
         //   Response.Redirect("~/");
        }
        private static string POST(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
           // UTF8Encoding encoding = new UTF8Encoding();
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            System.IO.Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str + " ";
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }

        protected void OutSms_Click(object sender, EventArgs e)
        {
            // отправка смс ЛоджикТелеком
            CookieContainer cookie = new CookieContainer();
            TelAccess soapclient = new TelAccess();

            soapclient.CookieContainer = cookie;

            // Init session
            InputLogon Param1 = new InputLogon();
            Param1.userName = "36851";
            Param1.password = "67448501";
            ReturnValueBase rv1 = soapclient.logon(Param1);

            if (rv1.errorCode != 0)
                throw new Exception(rv1.errorDescription);
            // Send simple sms
            InputSendSimpleSms Param2 = new InputSendSimpleSms();
            Param2.sender = "ClientInfo";
            Param2.phone = TextBox1.Text;
            Param2.text = TextBox2.Text;
            Param2.allowSince = 0;
            Param2.allowSinceSpecified = false;
            Param2.allowTill = 0;
            Param2.allowTillSpecified = false;
            Param2.usePhoneTimeSpecified = false;

            ReturnValueString rv2 = soapclient.sendSimpleSms(Param2);
            if (rv2.errorCode != 0)
                throw new Exception(rv2.errorDescription);
            else
                Msg.Text = "СМС отправлено, id: " + rv2.strResult;

            // Close session
            soapclient.logoff();
            // конец блока отправки смс ЛоджикТелеком
            TextBox1.Text = ""; TextBox2.Text = "";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            PhInSms.Visible = true;
            PhOutSms.Visible = true;
            Monitor.Visible = false;
            phWebPhone.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            PhInSms.Visible = false;
            PhOutSms.Visible = false;
            Monitor.Visible = false;
            phWebPhone.Visible = false;
        }

        protected void WebPhone_Click(object sender, EventArgs e)
        {
            phWebPhone.Visible = true;
        }

        protected void MonInt_Click(object sender, EventArgs e)
        {
            PhInSms.Visible = false;
            PhOutSms.Visible = false;
            Monitor.Visible = true;
            phWebPhone.Visible = false;
        }

        protected void InSmsText_Click(object sender, EventArgs e)
        {
            ReadFromInternet sms = new ReadFromInternet();
            XmlDocument doc = sms.MakeRequest();
            //    XmlNode ipx = doc["output"]["inbox"]; одно смс
            // XmlNode icx = doc["output"]["inbox"]["MESSAGE"]["SMS_TEXT"]; //часть смс по имени
            XmlNode icx = doc["output"]; //все смс  
          
                if (icx != null && icx.HasChildNodes)
                {
                    for (int i = 0; i < icx.ChildNodes.Count; i++)
                    {
                        SmsCreat.Text = icx.ChildNodes[i].InnerText; // .Substring(5);

                    }
                }
               
        } 
    }
}