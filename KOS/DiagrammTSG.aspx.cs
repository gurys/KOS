using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace KOS
{
    public partial class DiagrammTSG : System.Web.UI.Page 
    {
        string fam, io, u, m, _wz, pr;
        int id = 1;
        static string NumEv, Cr, Sn, _id, _nev;
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
        public class AsyncMethod
        {
            public async static Task<string> GetPost()
            {

                string date_from = DateTime.Now.AddDays(-1).ToString();
                string date_to = DateTime.Now.ToString();
                string url = "http://service.qtelecom.ru/public/http/";
                string post = "user=36851&pass=67448501&gzip=none&action=inbox&sib_num=1745&new_only=0&date_from=" + date_from + "&date_to=" + date_to;
                string PostSms = "";
                return await Task.Run(() =>
                {
                    while (PostSms == "")
                    {
                        Thread.Sleep(600000);
                        XmlDocument doc = POST(url, post);
                        PostSms = doc.OuterXml.ToString();
                    }               
                    return PostSms;
                });
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _role = "ODS_tsg";
            
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd;
                if (_role == "ODS_tsg")
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
                }
                List<string> uslugy = new List<string>() { "Эксплуатация лифтов", "Эксплуатация зданий", "Электроснабжение", "Отопление", "Водоснабжение", "Водоотведение", "Охрана", "Клининг" };
                    if (!IsPostBack)
                    {
                        Uslugy.DataSource = uslugy;
                        Uslugy.DataBind();
                        Uslugy.SelectedIndex = 0;
                     //   NLift.Text = "Внести!";
                        Uslugy_TextChanged(this, EventArgs.Empty);
                        
                        
                    }
             
                        //    диспетчеры ОДС
                        {
                            cmd = new SqlCommand("select p.surname, p.name, p.midlename from People p " +
                                "where p.comments=@user and p.specialty=N'диспетчер'", conn);
                            
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        }
                        List<string> iodisp = new List<string>() { " " };
                         dr = cmd.ExecuteReader();
                        while (dr.Read())                            
                            iodisp.Add(dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString());
                        dr.Close();
                        if (!IsPostBack)
                        {
                            FIO.DataSource = iodisp;
                            FIO.DataBind();
                            FIO.SelectedIndex = 0;
                            Fdr.Visible = true;
                        }


                    
                    //Механик
                    List<string> worker = new List<string>() { " " };
                   if (!IsPostBack)
                   {                       
                        Workers.DataSource = worker;
                        Workers.DataBind();
                        Workers.SelectedIndex = 0;
                        Worker_TextChanged(this, EventArgs.Empty);
                    }
               

                    //Отправил:
                    cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                        "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                        Disp.Text = dr[0].ToString() + " " + dr[1].ToString();
                    dr.Close();
                    
                
            }
        }
        protected void Uslugy_TextChanged(object sender, EventArgs e)
        {
            if (Uslugy.SelectedItem == null)
                return;            

            if (Uslugy.SelectedValue != "Эксплуатация лифтов")
            {
                List<string> category = new List<string>() { " ", "авария", "заявка", "внеплановые ремонты" };

                
                    Category.DataSource = category;
                    Category.DataBind();
                    Cat.Visible = true;
                    TA.Visible = true;
                    Text2.Visible = true;
                    Workers.Visible = false;
                    Lift.Visible = false;
                    NLift.Visible = true;
                    NLift.Text = "Внимание! Внести..";
                    
                
            }
            else
            {
                List<string> category = new List<string>() { " ", "застревание", "останов", "заявка", "плановые работы", "внеплановые ремонты", "ПНР/РЭО"};

                Category.DataSource = category;
                Category.DataBind();
                Cat.Visible = true;
                TA.Visible = false;
                Text2.Visible = false;
                Workers.Visible = true;
                Lift.Visible = true;
                NLift.Visible = true;
                NLift.Text = "Внимание! Bыбрать №.."; 
            }
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
                if (_role == "ODS_tsg")
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
                if (Uslugy.SelectedValue == "Эксплуатация лифтов")
                {
                    NLift.Visible = true; 
                    Lift.Visible = true;
                    NLift.Text = "Bыбрать №!";
                 
                }
                else
                {
                    NLift.Visible = true;
                    Lift.Visible = false;
                    NLift.Text = "Внести!";
                }
            }
        }
        protected void Worker_TextChanged(object sender, EventArgs e)
        {
            if (Workers.SelectedItem == null)
                return;           
            // закрепленные механики за ОДС
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO from WorkerLifts wl " +
                        "join UserInfo ui on wl.UserId=ui.UserId " +
                        "where wl.LiftId=@liftId", conn);
                    cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                    List<string> worker = new List<string>() {" "};
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                        worker.Add(dr[0].ToString() + " " + dr[1].ToString());
                    dr.Close();
                   {
                     Workers.DataSource = worker;
                     Workers.DataBind();
                     Workers.SelectedIndex = 0;
                     Wrk.Visible = true;
                   }
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
            if (Category.Text == " ")
            {
                Msg.Text = "Внимание! Вы забыли выбрать вид работ.";
                return;
            }           
           
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                DateTime date = DateTime.Now;

                if (Uslugy.SelectedValue == "Эксплуатация лифтов")
                {
                    cmd = new SqlCommand("insert into Zayavky " +
                        "(TtxId, LiftId, UserId, [Text], Category, [From], [Start]) " +
                        "values (@ttxId, @liftId, (select UserId from Users where UserName=@user), @text, @c, @f, @s)", conn);
                    cmd.Parameters.AddWithValue("ttxId", ttxId);
                    cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    cmd.Parameters.AddWithValue("text", Text.Text);
                    if (_role == "ODS_tsg")
                        cmd.Parameters.AddWithValue("c", Category.SelectedValue);
                    else
                        cmd.Parameters.AddWithValue("c", "заявка");
                    if (_role == "ODS_tsg")
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
                    id = int.Parse(cmd.ExecuteScalar().ToString());
                } 
                //Блок записи в базу событий
                    //подготовка
                    cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                             "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        fam = dr[0].ToString(); io = dr[1].ToString();
                    dr.Close();
                    cmd = new SqlCommand("select LiftId, IdU, IdM from Lifts " +
                       "where LiftId=@lift", conn);
                    cmd.Parameters.AddWithValue("lift", Lift.SelectedValue);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    { u = dr[1].ToString(); m = dr[2].ToString(); }
                    dr.Close();
                    // запись события в базу               
                    string s = "";
                    if (Uslugy.SelectedValue == "Эксплуатация лифтов" & Workers.SelectedValue != " ")
                        s = "insert into Events (EventId, RegistrId, DataId, ZayavId, Sourse, Family, IO, TypeId, IdU, IdM, LiftId, ToApp, DateToApp, Address) " +
                           "values (@text, @reg, @s, @id, @f, @fam, @io, @c, @u, @m, @liftid, @toapp, @ta, @adr)";
                    else if (Uslugy.SelectedValue == "Эксплуатация лифтов" & Workers.SelectedValue == " ")
                        s = "insert into Events (EventId, RegistrId, DataId, ZayavId, Sourse, Family, IO, TypeId, IdU, IdM, LiftId, ToApp, Address) " +
                           "values (@text, @reg, @s, @id, @f, @fam, @io, @c, @u, @m, @liftid, @toapp, @adr)";
                    else if (Uslugy.SelectedValue != "Эксплуатация лифтов" & (!string.IsNullOrEmpty(Text2.Text)))
                        s = "insert into Events (EventId, RegistrId, DataId, Sourse, Family, IO, TypeId, IdU, IdM, LiftId, ToApp, DateToApp, Address) " +
                            "values (@text, @reg, @s, @f, @fam, @io, @c, @u, @m, @liftid, @toapp, @ta, @adr)";
                    else if (Uslugy.SelectedValue != "Эксплуатация лифтов" & (string.IsNullOrEmpty(Text2.Text)))
                        s = "insert into Events (EventId, RegistrId, DataId, Sourse, Family, IO, TypeId, IdU, IdM, LiftId, ToApp, Address) " +
                        "values (@text, @reg, @s, @f, @fam, @io, @c, @u, @m, @liftid, @toapp, @adr)";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("text", Text.Text);
                    cmd.Parameters.AddWithValue("s", date);
                    if (Uslugy.SelectedValue == "Эксплуатация лифтов")
                        cmd.Parameters.AddWithValue("toapp", Workers.SelectedValue);
                    else
                        cmd.Parameters.AddWithValue("toapp", Text2.Text);
                    if (Text2.Text != " " || Workers.SelectedValue != " ")
                        cmd.Parameters.AddWithValue("ta", date);
                    cmd.Parameters.AddWithValue("f", Text1.Text);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("fam", User.Identity.Name);
                    cmd.Parameters.AddWithValue("reg", Uslugy.SelectedValue);
                    cmd.Parameters.AddWithValue("io", FIO.SelectedValue);
                    cmd.Parameters.AddWithValue("c", Category.SelectedValue);
                    cmd.Parameters.AddWithValue("adr", Address.SelectedValue);
                    cmd.Parameters.AddWithValue("u", u);
                    cmd.Parameters.AddWithValue("m", m);
                    if (Uslugy.SelectedValue == "Эксплуатация лифтов")
                        cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                    else
                        cmd.Parameters.AddWithValue("liftId", TextArea.Text);
                    cmd.ExecuteNonQuery();
                //  номер записанного события
                    cmd = new SqlCommand("select e.Id from Events e" +
                    " where e.ZayavId=@i", conn);
                    cmd.Parameters.AddWithValue("i", id);
                    SqlDataReader de = cmd.ExecuteReader();
                    while (de.Read())
                    {
                        _wz = de[0].ToString();
                    }
                    dr.Close();
                    _nev = _wz;
                    _id = id.ToString();
                    try
                    {
                        //Сообщения на Email и почту
                        KOS.App_Code.Mail mail = new Mail(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                        mail.SendMsg(Lift.SelectedValue, int.Parse(id.ToString()));
                    }
                    catch { Msg.Text = "Сбой почтовой службы! Регистрация услуг ТСЖ выполнена.  ";  }
                    // sms 
                    if (Category.SelectedValue == "застревание" || Category.SelectedValue == "останов" )
                    {
                        pr = "2345";
                        string wn = "9269338001";
                        string str = User.Identity.Name;
                        if (str == "ODS_Emica") wn = "9269338001";
                        else if (str == "ODS13" || Address.Text == "пр. Вернадского, 94 -1" || Address.Text == "пр. Вернадского, 94 -2" || Address.Text == "пр. Вернадского, 94 -3" || Address.Text == "пр. Вернадского, 94 -4" || Address.Text == "пр. Вернадского, 94 -5") wn = "9268976775";
                        else if (str == "ODS14" || Address.Text == "пр. Вернадского, 92") wn = "9253135718";
                        else if (str == "ODS21" || Address.Text == "ул. Никольская, д.12") wn = "9264610904";
                        else if (str == "ODS22" || Address.Text == "ул. Никольская, д.10") wn = "9296758809";
                        else if (str == "ODS25" || Address.Text == "Хилков пер. 1" ) wn = "9264610904";
                        else if (str == "ODS41" || Address.Text == "ул. Ясная, д. 7" || Address.Text == "ул. Жасминовая, д. 7") wn = "9267270995";
                        else if (str == "ODS42" || Address.Text == "ул. Ясная, д. 5" || Address.Text == "ул. Жасминовая, д. 5" || Address.Text == "ул. Ясная, д. 6" || Address.Text == "ул. Жасминовая, д. 6") wn = "9629908871";
                        else if (str == "ODS_test") wn = "9264062614";
                        string nomer = wn;
                        string TextSms = Category.SelectedValue + "-адрес:" + Address.SelectedItem.Value + "-" + "лифт №:"
                                         + Lift.SelectedValue + ", отправьте " + pr + " " + _wz + " на 89037676333 о принятии";
                        // отправка смс SMS.ru-
                        string myApiKey = "27B482E1-14AE-ACFB-C500-CCEC9C763C99"; // API ключ
                        SmsRu.SmsRu sms = new SmsRu.SmsRu(myApiKey);// Основная рассылка
                        var response = sms.Send(nomer, TextSms);

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
                        Param2.phone = "79269338001";
                        Param2.text = TextSms; 
                        Param2.allowSince = 0;
                        Param2.allowSinceSpecified = false;
                        Param2.allowTill = 0;
                        Param2.allowTillSpecified = false;
                        Param2.usePhoneTimeSpecified = false;

                        ReturnValueString rv2 = soapclient.sendSimpleSms(Param2);
                        if (rv2.errorCode != 0)
                            throw new Exception(rv2.errorDescription);
                        else
                            Msg.Text = "СМС отправлено id: " + rv2.strResult;

                        // Close session
                        soapclient.logoff();
                        // конец блока отправки смс ЛоджикТелеком
                        //    Msg.Text = AsyncMethod.GetPost().ToString();
                        //    Task.Run(() => PostRequestAsync());
                        //    Msg.Text = GetPostSms().ToString();
                    }
                        Msg.Text = "Регистрация выполнена, отправлено сообщение на e-mail Менеджеру и СМС Дежурному механику. "; // + Для связи с Дежурной службой нажмите кнопку сайтофона.
               
                        Response.Redirect("~/Reg_tsg.aspx");
            }
        }
        private static async Task PostRequestAsync()
        {
            string date_from = DateTime.Now.AddDays(-1).ToString();
            string date_to = DateTime.Now.ToString();
            WebRequest request = WebRequest.Create("http://service.qtelecom.ru/public/http/");
            request.Method = "POST"; // для отправки используется метод Post
            // данные для отправки
            string data = "user=36851&pass=67448501&gzip=none&action=inbox&sib_num=1745&new_only=0&date_from=" + date_from + "&date_to=" + date_to;
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;
            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                   //Console.WriteLine(reader.ReadToEnd());
                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);
                    
                     XmlNode txt = doc["output"]["inbox"]["MESSAGE"]["SMS_TEXT"];
                     XmlNode txt1 = doc["output"]["inbox"]["MESSAGE"]["CREATED"];
                     XmlNode txt2 = doc["output"]["inbox"]["MESSAGE"]["SMS_SENDER"];
                    reader.Close();
                  if (txt != null && txt.HasChildNodes)
                  {   
                    for (int i = 0; i < txt.ChildNodes.Count; i++)
                    {
                        NumEv = txt.ChildNodes[i].InnerText.Substring(5);
                        
                    }
                  }
                  if (txt1 != null && txt1.HasChildNodes)
                  {
                      for (int i = 0; i < txt1.ChildNodes.Count; i++)
                      {
                          Cr = txt1.ChildNodes[i].InnerText;

                      }
                  }
                  if (txt2 != null && txt2.HasChildNodes)
                  {
                      for (int i = 0; i < txt2.ChildNodes.Count; i++)
                      {
                          Sn = txt2.ChildNodes[i].InnerText;

                      }
                  }
                    
                }
            }
            if (NumEv == _nev)
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Zayavky " +
                    "set Prinyal=@w, PrinyalDate=@f, [Status]=@s where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", _id);
                cmd.Parameters.AddWithValue("w", Cr); //
                cmd.Parameters.AddWithValue("f", Sn);
                cmd.Parameters.AddWithValue("s", false);
                //  cmd.Parameters.AddWithValue("c", Text1.Text);
                cmd.ExecuteNonQuery();
               // Status.Text = "принято";
            /*    cmd = new SqlCommand("select Id from Events e" +
                " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close(); */
                // проверка истории, обновление события
              
                cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", _nev);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("u", Sn);
                cmd.Parameters.AddWithValue("f", Sn);
                cmd.Parameters.AddWithValue("txt", "ответное смс");
                cmd.Parameters.AddWithValue("cat", "");
                cmd.Parameters.AddWithValue("ph", NumEv);
                cmd.Parameters.AddWithValue("to", "ODS");
                cmd.Parameters.AddWithValue("cm", "Принято по № " + Cr);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Events " +
                 "set ToApp=@w, DateToApp=@f where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", _nev);
                cmd.Parameters.AddWithValue("w", Sn);
                cmd.Parameters.AddWithValue("f", Cr);
                cmd.ExecuteNonQuery();

            }

            response.Close(); 
            //Console.WriteLine("Ответная СМС прочитана!");
        }
        private static XmlDocument POST(string Url, string Data)
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
            XmlDocument doc = new XmlDocument();
            doc.Load(sr);            
            return doc; 
        }
        public async static Task<string> GetPostSms()
        {
            string date_from = DateTime.Now.AddDays(-1).ToString();
            string date_to = DateTime.Now.ToString();
            string url = "http://service.qtelecom.ru/public/http/";
            string post = "user=36851&pass=67448501&gzip=none&action=inbox&sib_num=1745&new_only=0&date_from=" + date_from + "&date_to=" + date_to;
            string PostSms = "";
            return await Task.Run(() =>
            {
                
                {
                    XmlDocument doc = POST(url, post);
                  //  Thread.Sleep(60000);
                }
            //    PrinyalSms(PostSms);
                return PostSms;
            });
        } 
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reg_tsg.aspx");
        } 

    }
}


