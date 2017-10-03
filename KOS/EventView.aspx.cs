using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KOS.App_Code;

namespace KOS
{
    public partial class EventView : System.Web.UI.Page
    {
        int _wz = 0;
        class aType
        {
            public aType(string s) { Email = s; }
            public string Email { get; set; }
        }
        List<aType> _type = new List<aType>()
        {
            new aType(""), new aType("заявки механику")
        };
        string _role = string.Empty;
        string _zayav = string.Empty;
         class Data 
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Idz { get; set; }
            public string Idw { get; set; } 
            public string Name { get; set; }
            public string namefoto { get; set; }
            public string NumID { get; set; }
            public string Kol { get; set; }  
            public string NameFile { get; set; }
            public string Status { get; set; }
            public string Prim { get; set; }  
            public string Usr { get; set; }
            public string Date { get; set; }
            public string Text { get; set; }
            public string Category { get; set; }
            public string UserName { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Comment { get; set; }
            public string PrimHist { get; set; }
   
        }
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
                EventId.Text = data.Rows[0]["EventId"].ToString(); ZaprosMng.Checked = Boolean.Parse(data.Rows[0]["ZaprosMng"].ToString());
                DataId.Text = data.Rows[0]["DataId"].ToString();   ZaprosKP.Checked = Boolean.Parse(data.Rows[0]["ZaprosKP"].ToString());
                                                                   KP.Checked = Boolean.Parse(data.Rows[0]["KP"].ToString());
                Comment.Text = data.Rows[0]["Comment"].ToString(); ZapBill.Checked = Boolean.Parse(data.Rows[0]["ZapBill"].ToString());
                Prim.Text = data.Rows[0]["Prim"].ToString();       Bill.Checked = Boolean.Parse(data.Rows[0]["Bill"].ToString());
                Idz.Text = data.Rows[0]["ZayavId"].ToString();     Payment.Checked = Boolean.Parse(data.Rows[0]["Payment"].ToString());
                Idwz.Text = data.Rows[0]["WZayavId"].ToString();   Dostavka.Checked = Boolean.Parse(data.Rows[0]["Dostavka"].ToString());
                LiftId.Text = data.Rows[0]["LiftId"].ToString();   Prihod.Checked = Boolean.Parse(data.Rows[0]["Prihod"].ToString());
                Sourse.Text = data.Rows[0]["Sourse"].ToString();   Peremeshenie.Checked = Boolean.Parse(data.Rows[0]["Peremeshenie"].ToString());
                Family.Text = data.Rows[0]["Family"].ToString();   AktVR.Checked = Boolean.Parse(data.Rows[0]["AktVR"].ToString());
                TypeId.Text = data.Rows[0]["TypeId"].ToString();   Spisanie.Checked = Boolean.Parse(data.Rows[0]["Spisanie"].ToString());
                ToApp.Text = data.Rows[0]["ToApp"].ToString();     Cansel.Checked = Boolean.Parse(data.Rows[0]["Cansel"].ToString());
                DateToApp.Text = data.Rows[0]["DateToApp"].ToString();
                Who.Text = data.Rows[0]["Who"].ToString();
                DateWho.Text = data.Rows[0]["DateWho"].ToString();
                TimeSpan pr = DateTime.Now - ((DateTime)data.Rows[0]["DataId"]);
                if (!(data.Rows[0]["DateWho"] is DBNull))
                    pr = ((DateTime)data.Rows[0]["DateWho"]) - ((DateTime)data.Rows[0]["DataId"]);
                string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                    pr.Minutes.ToString();
                Timing.Text = prostoy;
                

            }
        }
           //запрос к базе для конвертации и просмотра Foto
         protected void Foto_Click(object sender, EventArgs e)
         { 
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ev.[Foto], ev.nameFoto from Events ev " +
                    "where ev.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _wz);
             //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(0, 0, bBuffer, 0, bLength);
             //  просмотр в браузере
                /*    Response.ContentType = "image"; //image/Jpeg
                   Response.BinaryWrite(bBuffer);
                /*   string commandText = @"mailto:gurus@emicatech.com";// открыть почту по умолчанию
                   var proc = new System.Diagnostics.Process();
                   proc.StartInfo.FileName = commandText;
                   proc.StartInfo.UseShellExecute = true;
                   proc.Start();
                */ 
             //просмотр фото на машине пользователя
               File.WriteAllBytes(@"C:\temp\pic.jpg", bBuffer);
               string commandText = @"C:\temp\pic.jpg";// открыть файл
               var proc = new System.Diagnostics.Process();
               proc.StartInfo.FileName = commandText;
               proc.StartInfo.UseShellExecute = true;
               proc.Start();
            }
    
        }
         protected void ZayEdit_Click(object sender, EventArgs e)
         {
             if (Idz.Text != "") Response.Redirect("~/ZayavkaEdit.aspx?zId=" + Idz.Text);
             else if (Idwz.Text != "") Response.Redirect("~/WZClose.aspx?wz=" + Idwz.Text);
             else return;
         }
         protected void Edit_Click(object sender, EventArgs e)
         {
             if (string.IsNullOrEmpty(Request["zId"]))
                 // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                 Response.Redirect("~/About.aspx");
             _wz = Int32.Parse(Request["zId"]);
             try
             {
               
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        DateTime date = DateTime.Now;
                        SqlCommand cmd = new SqlCommand();
                        string s = "update Events set EventId=@e, Sourse=@s, DataId=@de, ToApp=@toapp, DateToApp=@da, Who=@who, DateWho=@dw, Comment=@comm, Prim=@pr where Id=@i";
                        if (DateToApp.Text != "" && DateWho.Text != "")
                        {
                            cmd = new SqlCommand(s, conn);
                            cmd.Parameters.AddWithValue("s", Sourse.Text);
                            cmd.Parameters.AddWithValue("e", EventId.Text);
                            cmd.Parameters.AddWithValue("i", _wz);
                            //    cmd.Parameters.AddWithValue("fio", IO.Text);
                            cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                            //    cmd.Parameters.AddWithValue("z", LiftId.Text);
                            cmd.Parameters.AddWithValue("toapp", ToApp.Text);
                            cmd.Parameters.AddWithValue("who", Who.Text);
                            cmd.Parameters.AddWithValue("pr", Prim.Text);
                            cmd.Parameters.AddWithValue("comm", Comment.Text);
                            cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                            cmd.Parameters.AddWithValue("dw", Convert.ToDateTime(DateWho.Text));
                            cmd.ExecuteNonQuery();
                        }
                        if (DateToApp.Text == "" && DateWho.Text == "")
                        {
                            s = "update Events set EventId=@e, Sourse=@s, DataId=@de, ToApp=@toapp, Who=@who, Comment=@comm, Prim=@pr where Id=@i";

                            cmd = new SqlCommand(s, conn);
                            cmd.Parameters.AddWithValue("s", Sourse.Text);
                            cmd.Parameters.AddWithValue("e", EventId.Text);
                            cmd.Parameters.AddWithValue("i", _wz);
                            //    cmd.Parameters.AddWithValue("fio", IO.Text);
                            cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                            //    cmd.Parameters.AddWithValue("z", LiftId.Text);
                            cmd.Parameters.AddWithValue("toapp", ToApp.Text);
                            cmd.Parameters.AddWithValue("who", Who.Text);
                            cmd.Parameters.AddWithValue("comm", Comment.Text);
                            cmd.Parameters.AddWithValue("pr", Prim.Text);
                         //   cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                         //   cmd.Parameters.AddWithValue("dw", Convert.ToDateTime(DateWho.Text));
                            cmd.ExecuteNonQuery();
                        }
                        if (DateToApp.Text != "" && DateWho.Text == "")
                        {
                            s = "update Events set EventId=@e, Sourse=@s, DataId=@de, ToApp=@toapp, DateToApp=@da, Who=@who, Comment=@comm, Prim=@pr where Id=@i";

                            cmd = new SqlCommand(s, conn);
                            cmd.Parameters.AddWithValue("s", Sourse.Text);
                            cmd.Parameters.AddWithValue("e", EventId.Text);
                            cmd.Parameters.AddWithValue("i", _wz);
                            //    cmd.Parameters.AddWithValue("fio", IO.Text);
                            cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                            //    cmd.Parameters.AddWithValue("z", LiftId.Text);
                            cmd.Parameters.AddWithValue("toapp", ToApp.Text);
                            cmd.Parameters.AddWithValue("who", Who.Text);
                            cmd.Parameters.AddWithValue("comm", Comment.Text);
                            cmd.Parameters.AddWithValue("pr", Prim.Text);
                               cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                            //   cmd.Parameters.AddWithValue("dw", Convert.ToDateTime(DateWho.Text));
                            cmd.ExecuteNonQuery();
                        }
                        if (DateToApp.Text == "" && DateWho.Text != "")
                        {
                            s = "update Events set EventId=@e, Sourse=@s, DataId=@de, ToApp=@toapp, Who=@who, DateWho=@dw, Comment=@comm, Prim=@pr where Id=@i";

                            cmd = new SqlCommand(s, conn);
                            cmd.Parameters.AddWithValue("s", Sourse.Text);
                            cmd.Parameters.AddWithValue("e", EventId.Text);
                            cmd.Parameters.AddWithValue("i", _wz);
                            //    cmd.Parameters.AddWithValue("fio", IO.Text);
                            cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                            //    cmd.Parameters.AddWithValue("z", LiftId.Text);
                            cmd.Parameters.AddWithValue("toapp", ToApp.Text);
                            cmd.Parameters.AddWithValue("who", Who.Text);
                            cmd.Parameters.AddWithValue("comm", Comment.Text);
                            cmd.Parameters.AddWithValue("pr", Prim.Text);
                            //   cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                            cmd.Parameters.AddWithValue("dw", Convert.ToDateTime(DateWho.Text));
                            cmd.ExecuteNonQuery();
                        }

                    }  
                     if (Idz.Text != "")
                     {
                         using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                         {
                             conn.Open();
                             SqlCommand cmd = new SqlCommand();
                             string s = "update Zayavky set Start=@de, PrinyalDate=@da where Id=@zid";
                             if (DataId.Text != "" && DateToApp.Text != "")
                             {
                                 cmd = new SqlCommand(s, conn);
                                 cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                                 cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                                 cmd.Parameters.AddWithValue("zid", Idz.Text);
                                 cmd.ExecuteNonQuery();
                             }
                             if (DataId.Text != "" && DateToApp.Text == "")
                             {
                                s = "update Zayavky set Start=@de where Id=@zid";
                                 cmd = new SqlCommand(s, conn);
                                 cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                             //    cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                                 cmd.Parameters.AddWithValue("zid", Idz.Text);
                                 cmd.ExecuteNonQuery();
                             }
                            
                             cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @to, @cm, @ph )", conn);
                             cmd.Parameters.AddWithValue("ne", _wz);
                             cmd.Parameters.AddWithValue("d", DateTime.Now);
                             cmd.Parameters.AddWithValue("u", User.Identity.Name);
                             //  cmd.Parameters.AddWithValue("f", IO.Text);
                             cmd.Parameters.AddWithValue("txt", EventId.Text);
                             cmd.Parameters.AddWithValue("cat", "редактирование");
                             cmd.Parameters.AddWithValue("ph", "из интерфейса менеджера");
                             cmd.Parameters.AddWithValue("to", "manager");
                             cmd.Parameters.AddWithValue("cm", "редактирование события");
                             cmd.ExecuteNonQuery();
                         }
                     }
                     if (Idwz.Text != "")
                     {
                         using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                         {
                             conn.Open();
                             SqlCommand cmd = new SqlCommand();
                             string s ="update WorkerZayavky set Date=@de, DoneDate=@da where Id=@zid";
                             if (DataId.Text != "" && DateToApp.Text != "")
                             {
                                 cmd = new SqlCommand(s, conn);
                                 cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                                 cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                                 cmd.Parameters.AddWithValue("zid", Idwz.Text);
                                 cmd.ExecuteNonQuery();
                             }
                             if (DataId.Text != "" && DateToApp.Text == "")
                             {
                                 s = "update WorkerZayavky set Date=@de where Id=@zid";
                                 cmd = new SqlCommand(s, conn);
                                 cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                            //     cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                                 cmd.Parameters.AddWithValue("zid", Idwz.Text);
                                 cmd.ExecuteNonQuery();
                             }
                             cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @to, @cm, @ph )", conn);
                             cmd.Parameters.AddWithValue("ne", _wz);
                             cmd.Parameters.AddWithValue("d", DateTime.Now);
                             cmd.Parameters.AddWithValue("u", User.Identity.Name);
                             //   cmd.Parameters.AddWithValue("f", IO.Text);
                             cmd.Parameters.AddWithValue("txt", EventId.Text);
                             cmd.Parameters.AddWithValue("cat", "редактирование");
                             cmd.Parameters.AddWithValue("ph", "из интерфейса менеджера");
                             cmd.Parameters.AddWithValue("to", "manager");
                             cmd.Parameters.AddWithValue("cm", "редактирование события");
                             cmd.ExecuteNonQuery();
                         }
                     }  
                 
                 Msg.Text = "Изменения записаны!";
             }
             catch { Msg.Text = "Поля Дата/время не корректны! Исравьте формат, например (01.01.2017 00:01) "; }
         }

         //закрытие
         protected void Close_Click(object sender, EventArgs e)
         {
             if (string.IsNullOrEmpty(Request["zId"]))

                 Response.Redirect("~/About.aspx");
             _wz = Int32.Parse(Request["zId"]);
             try
             {

                 using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                 {
                     conn.Open();
                     DateTime date = DateTime.Now;
                     DateTime cl = Convert.ToDateTime(DateWho.Text);
                     TimeSpan pr = Convert.ToDateTime(DateWho.Text) - Convert.ToDateTime(DataId.Text);
                     SqlCommand cmd = new SqlCommand("update Events " +
                      "set Cansel=@c, DateCansel=@d, Who=@w, DateWho=@dw, Comment=@com, Timing=@t where Id=@i", conn);

                     string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" + pr.Minutes.ToString();
                     if ((int)pr.TotalDays < 0 || ((int)pr.Hours) < 0 || ((int)pr.Minutes) < 0)
                     {
                         Msg.Text = "Дата/время Исполнения не может быть раньше даты/времени Регистрации!";
                         return;
                     }

                     cmd.Parameters.AddWithValue("d", date);
                     cmd.Parameters.AddWithValue("dw", cl);
                     cmd.Parameters.AddWithValue("c", true);
                     cmd.Parameters.AddWithValue("com", Comment.Text);
                     cmd.Parameters.AddWithValue("t", prostoy);
                     cmd.Parameters.AddWithValue("w", Who.Text);
                     cmd.Parameters.AddWithValue("i", _wz);
                     cmd.ExecuteNonQuery();

                     cmd = new SqlCommand("update Zayavky " +
                    "set Finish=@f, [Status]=@s, Couse=@c where Id=@i", conn);
                     cmd.Parameters.AddWithValue("i", _zayav);
                     //  cmd.Parameters.AddWithValue("w", _worker);
                     cmd.Parameters.AddWithValue("f", DateTime.Now);
                     cmd.Parameters.AddWithValue("s", true);
                     cmd.Parameters.AddWithValue("c", Comment.Text);
                     cmd.ExecuteNonQuery();
                     Response.Redirect("~/Reg_tsg.aspx");
                 }
             }
             catch { Msg.Text = "Дата/время Закрытия введена не корректно! Исравьте формат, например (01.01.2017 00:01)"; }
         }
         protected void Hist_Click(object sender, EventArgs e)
         {
             List<Data> datahist = GetData2();
             ListView2.DataSource = datahist;
             ListView2.DataBind();
             PartL.Visible = false;
             DocEv.Visible = false;
             HistL.Visible = true;
         }
         List<Data> GetData2()
         {
             List<Data> datahist = new List<Data>();
             //  string url = "~/DocumView.aspx?zId=";
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("select he.NumEvent, he.Date, he.Text, he.Category, he.UserName, he.[From], he.[To], he.Comment, he.PrimHist from HistEv he " +
                 "where he.NumEvent=@id", conn);
                 cmd.Parameters.AddWithValue("id", _wz);
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     datahist.Add(new Data()
                     {
                         Date = (dr["Date"].ToString()),
                         Text = dr["Text"].ToString(),
                         Category = dr["Category"].ToString(),
                         UserName = dr["UserName"].ToString(),
                         From = dr["From"].ToString(),
                         To = dr["To"].ToString(),
                         Comment = dr["Comment"].ToString(),
                         PrimHist = dr["PrimHist"].ToString()
                     });
                 }
                 dr.Close();
             }
             return datahist;
         }
         protected void PartList_Click(object sender, EventArgs e)
         {
             List<Data> datapart = GetData1();
             ListView1.DataSource = datapart;
             ListView1.DataBind();
             PartL.Visible = true;
             DocEv.Visible = false;
             HistL.Visible = false;
         }
         List<Data> GetData1()
         {
             List<Data> datapart = new List<Data>();
             string url = "~/PartView.aspx?zId=";
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("select Id, namefoto, Name, NumID, Kol from PartsList " +
                 "where NumEvent=@id", conn);
                 cmd.Parameters.AddWithValue("id", _wz);
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     datapart.Add(new Data()
                     {
                         Id = (dr["Id"].ToString()),
                         Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                         Name = dr["Name"].ToString(),
                         namefoto = dr["namefoto"].ToString(),
                         NumID = dr["NumID"].ToString(),
                         Kol = dr["Kol"].ToString()

                     });
                 }
                 dr.Close();
             }
             return datapart;
         }
         protected void DocE_Click(object sender, EventArgs e)
         {
             List<Data> datadoc = GetData();
             Out.DataSource = datadoc;
             Out.DataBind();
             PartL.Visible = false;
             HistL.Visible = false;
             DocEv.Visible = true;
         }
         List<Data> GetData()
         {
             List<Data> datadoc = new List<Data>();
             string url = "~/DocumView.aspx?zId=";
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.Prim, d.Usr from Documents d " +
                 "where d.NumEvent=@id", conn);
                 cmd.Parameters.AddWithValue("id", _wz);
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     datadoc.Add(new Data()
                     {
                         Id = (dr["Id"].ToString()),
                         Url = (url + dr["Id"].ToString()),
                         Name = dr["Name"].ToString(),
                         NameFile = dr["NameFile"].ToString(),
                         Status = dr["Status"].ToString(),
                         Prim = dr["Prim"].ToString(),
                         Usr = dr["Usr"].ToString()
                     });
                 }
                 dr.Close();
             }
             return datadoc;
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

         bool IsAllSelected(ListView lv)
         {
             CheckBox cbAll = (CheckBox)lv.FindControl("SelectAll");
             if (cbAll == null) return true;
             bool selAll = true;
             foreach (ListViewItem item in lv.Items)
             {
                 CheckBox cb = (CheckBox)item.FindControl("Select");
                 if (!cb.Checked)
                 {
                     selAll = false;
                     break;
                 }
             }
             cbAll.Checked = selAll;
             return selAll;
         }

         List<string> GetSelectedEmails(ListView lv) 
         {
             List<string> sel = new List<string>();
             foreach (ListViewItem item in lv.Items)
             {
                 CheckBox cb = (CheckBox)item.FindControl("Select");
                 if (cb.Checked)
                 {
                     Label lb = (Label)item.FindControl("Label2");
                     sel.Add(lb.Text);
                 }
             }
             return sel;
         }
         protected void Arbitrary_Query_Click(object sender, EventArgs e)
         {
             //запрос на КП запчастей с фото не внесенных в событие
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("insert into PartsList (NumEvent, Foto, namefoto, Name, NumID, Kol, Obz) values (@nev, @foto, @namefoto, @name, @numid, @kol, @obz) ", conn);
                 //выбор фото из устройства
                 string namePhoto = FileUpload.FileName;
                 //преобразование в двоичный код
                 byte[] photo = FileUpload.FileBytes;
                 cmd.Parameters.AddWithValue("nev", Id.Text);
                 cmd.Parameters.Add("foto", SqlDbType.Image).Value = photo;
                 cmd.Parameters.Add("namefoto", SqlDbType.NVarChar).Value = namePhoto;
                 cmd.Parameters.AddWithValue("name", Text1.Text);
                 cmd.Parameters.AddWithValue("obz", TextBox1.Text);
                 cmd.Parameters.AddWithValue("numid", Text2.Text);
                 cmd.Parameters.AddWithValue("kol", Text3.Text);
                 cmd.ExecuteNonQuery();
                 File.WriteAllBytes(@"C:\temp\pic1.jpg", photo);
             }
             string text = Message.Text + " - " + Text1.Text + " - " + TextBox1.Text + " - " + Text2.Text + " - " + Text3.Text + ", фото в прикрепленном файле.";
             MailMessage message = new MailMessage("office@emicatech.com", Email2.Text, "запрос КП", text);
             message.Attachments.Add(new Attachment("C://temp//pic1.jpg"));
             SmtpClient client = new SmtpClient("127.0.0.1");
             client.Credentials = CredentialCache.DefaultNetworkCredentials;
             //    client.Credentials = new System.Net.NetworkCredential("gurus@emicatech.com", "pass"); 
             client.Send(message);
             Msg.Text = "Запрос отправлен на email: " + Email2.Text;
         }
        protected void Email_Click(object sender, EventArgs e)
         {
           
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                string name ="", id = "", kol ="";
                string name1 = "", id1 = "", kol1 = "";
                string name2 = "", id2 = "", kol2 = "";
                string name3 = "", id3 = "", kol3 = "";
                string name4 = "", id4 = "", kol4 = "";
                string dat = DateTime.Now.Date.ToLongDateString();
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
             //   _wz = Int32.Parse(EventId.Text);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
             //   DataTable data = db.GetEvent(_wz);
                DataTable dt = db.GetPartsList(_wz);
                try
                {
                    name = dt.Rows[0]["Name"].ToString();
                    id = dt.Rows[0]["NumID"].ToString();
                    kol = dt.Rows[0]["Kol"].ToString();
                }
                catch { }
                try
                {
                 name1 = dt.Rows[1]["Name"].ToString();
                 id1 = dt.Rows[1]["NumID"].ToString();
                 kol1 = dt.Rows[1]["Kol"].ToString(); 
                }
                catch { }
                try
                {
                    name2 = dt.Rows[2]["Name"].ToString();
                    id2 = dt.Rows[2]["NumID"].ToString();
                    kol2 = dt.Rows[2]["Kol"].ToString();
                }
                catch { }
                try
                {
                    name3 = dt.Rows[3]["Name"].ToString();
                    id3 = dt.Rows[3]["NumID"].ToString();
                    kol3 = dt.Rows[3]["Kol"].ToString();
                }
                catch { }
                try
                {
                    name4 = dt.Rows[4]["Name"].ToString();
                    id4 = dt.Rows[4]["NumID"].ToString();
                    kol4 = dt.Rows[4]["Kol"].ToString();
                }

                catch { }

                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
              
                    PdfReader template = new PdfReader(@"C:\temp\kp.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\zkp.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("dd", dd);
                    fields.SetField("mm", mm);
                    fields.SetField("yy", yy);
                    if (name != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name", name);
                    if (name1 != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name1", name1);
                    if (name2 != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name2", name2);
                    if (name3 != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name3", name3);
                    if (name4 != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("name4", name4);
                    fields.SetField("id", id);
                    fields.SetField("id1", id1);
                    fields.SetField("id2", id2);
                    fields.SetField("id3", id3);
                    fields.SetField("id4", id4);
                    fields.SetField("kol", kol);
                    fields.SetField("kol1", kol1);
                    fields.SetField("kol2", kol2);
                    fields.SetField("kol3", kol3);
                    fields.SetField("kol4", kol4);
                   // fields.SetField("kol1", kol1);
                    fields.SetField("dni", "один");
                    stamper.FormFlattening = false;// ложь - открыт для записи, истина - закрыт
                    stamper.Close();                    
                    // запись в БД
                    FileStream fs = new FileStream(@"C:\temp\zkp.pdf", FileMode.Open);
                    Byte[] pdf = new byte[fs.Length];
                    fs.Read(pdf, 0, pdf.Length);                    
                    SqlCommand cmd = new SqlCommand("insert into Documents (Name, NumEvent, Image, NameFile, Status) values (@name, @nev, @img, @namefile, @st )", conn);
                    cmd.Parameters.AddWithValue("name", "запрос КП");
                    cmd.Parameters.AddWithValue("nev", Id.Text);
                    cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = Id.Text + "/ЗКП.pdf";
                    cmd.Parameters.AddWithValue("st", "отправлен");
                    cmd.ExecuteNonQuery();
                    fs.Close();

                  //  Response.ContentType = "image"; //image/Jpeg
                  //  Response.BinaryWrite(pdf);
                }

             string text = "Здравствуйте! Вышлите пожалуйста коммерческое предложение на перечень запчастей во вложенном файле";
          if (!IsAllSelected(Post))
          {

              List<string> sel = GetSelectedEmails(Post);
               for (int i = 0; i < sel.Count; i++)
               Send(sel[i], text);
          }
          Msg.Text = "Запрос нв КП разослан по выбранным адресам!";
         }
        void Send(string mail, string text)
        {
            MailMessage message = new MailMessage("office@emicatech.com", mail, "запрос КП", text);
            message.Attachments.Add(new Attachment("C://temp//zkp.pdf"));
            SmtpClient client = new SmtpClient("127.0.0.1");
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
        //    client.Credentials = new System.Net.NetworkCredential("gurus@emicatech.com", "pass"); 
            client.Send(message);
        }
        protected void ZapKP_Click(object sender, EventArgs e)
        {
            zKP.Visible = true;
        }

        protected void ZaprosMng_CheckedChanged(object sender, EventArgs e)
        {
              using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        DateTime date = DateTime.Now;
                        SqlCommand cmd = new SqlCommand();
                        string s = "update Events set ZaprosMng=1 where Id=@i";
                        if (ZaprosMng.Checked == true)
                        {
                            cmd = new SqlCommand(s, conn);
                            cmd.Parameters.AddWithValue("i", _wz);
                            cmd.ExecuteNonQuery();
                        }
                        if (ZaprosMng.Checked == false)
                        {
                            s = "update Events set ZaprosMng=0 where Id=@i";
                            cmd = new SqlCommand(s, conn);
                            cmd.Parameters.AddWithValue("i", _wz);
                            cmd.ExecuteNonQuery();
                        }
                    }
        }

        protected void ZaprosKP_CheckedChanged(object sender, EventArgs e)
        {
            zKP.Visible = true;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set ZaprosKP=1 where Id=@i";
                if (ZaprosKP.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (ZaprosKP.Checked == false)
                {
                    s = "update Events set ZaprosKP=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void KP_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set KP=1 where Id=@i";
                if (KP.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (KP.Checked == false)
                {
                    s = "update Events set KP=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void ZapBill_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set ZapBill=1 where Id=@i";
                if (ZapBill.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (ZapBill.Checked == false)
                {
                    s = "update Events set ZapBill=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Bill_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Bill=1 where Id=@i";
                if (Bill.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (Bill.Checked == false)
                {
                    s = "update Events set Bill=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Payment_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Payment=1 where Id=@i";
                if (Payment.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (Payment.Checked == false)
                {
                    s = "update Events set Payment=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Dostavka_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Dostavka=1 where Id=@i";
                if (Dostavka.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (Dostavka.Checked == false)
                {
                    s = "update Events set Dostavka=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Prihod_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Prihod=1 where Id=@i";
                if (Prihod.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (Prihod.Checked == false)
                {
                    s = "update Events set Prihod=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Peremeshenie_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Peremeshenie=1 where Id=@i";
                if (Peremeshenie.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (Peremeshenie.Checked == false)
                {
                    s = "update Events set Peremeshenie=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void AktVR_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set AktVR=1 where Id=@i";
                if (AktVR.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (AktVR.Checked == false)
                {
                    s = "update Events set AktVR=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Spisanie_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Spisanie=1 where Id=@i";
                if (Spisanie.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
                if (Spisanie.Checked == false)
                {
                    s = "update Events set Spisanie=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void Cansel_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                string s = "update Events set Cansel=1 where Id=@i";
                if (Cansel.Checked == true)
                {
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                    Msg.Text = "Событие переведено в статус ЗАКРЫТО, чтобы убрать из списка механика закройте из обработчика! ";
                }
                if (Cansel.Checked == false)
                {
                    s = "update Events set Cansel=0 where Id=@i";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                    Msg.Text = "Событие переведено в статус АКТИВНО, чтобы добавить в список механика активируйте из обработчика! "; 
                }
            }
        }

        protected void AddDoc_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Documents (Name, NumEvent, Image, NameFile, Status, Prim, Usr) values (@name, @nev, @foto, @namefoto, @st, @pr, @usr) ", conn);
                //выбор фото из устройства
                string namePhoto = FileUpload1.FileName;
                //преобразование в двоичный код
                byte[] photo = FileUpload1.FileBytes;
                cmd.Parameters.AddWithValue("name", TextBox2.Text);
                cmd.Parameters.AddWithValue("nev", Id.Text);
                cmd.Parameters.Add("foto", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefoto", SqlDbType.NVarChar).Value = namePhoto;
                cmd.Parameters.AddWithValue("st", "добавлено");
                cmd.Parameters.AddWithValue("pr", "вручную");
                cmd.Parameters.AddWithValue("usr", User.Identity.Name);

                cmd.ExecuteNonQuery();
            }
            DocE_Click(sender, e);
        }
    }
}