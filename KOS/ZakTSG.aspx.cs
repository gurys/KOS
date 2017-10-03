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
    public partial class ZakTSG : System.Web.UI.Page 
    {
        int _wz = 0;      
        DateTime dateReg = DateTime.Now;        
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string namefoto { get; set; }
            public string NumID { get; set; }
            public string Kol { get; set; }
            public string NameFile { get; set; }
            public string Status { get; set; }
            public string Prim { get; set; }
            public string Usr { get; set; }
        }
        string _role = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            //    if (User.Identity.Name != "ODS14") { Treg.Visible = false; Tzak.Visible = false; }
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetEvent(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                Id.Text = _wz.ToString();
                Sourse.Text = data.Rows[0]["Sourse"].ToString();
                //   Text1.Text = data.Rows[0]["Sourse"].ToString();
                IO.Text = data.Rows[0]["IO"].ToString();
                DataId.Text = data.Rows[0]["DataId"].ToString();
                dateReg = ((DateTime)data.Rows[0]["DataId"]);
                ZayavId.Text = data.Rows[0]["ZayavId"].ToString();
                RegistrId.Text = data.Rows[0]["RegistrId"].ToString();
                LiftId.Text = data.Rows[0]["LiftId"].ToString();
                //    TextBox4.Text = data.Rows[0]["LiftId"].ToString();
                TypeId.Text = data.Rows[0]["TypeId"].ToString();
                EventId.Text = data.Rows[0]["EventId"].ToString();
                //    Text.Text = data.Rows[0]["EventId"].ToString();
                ToApp.Text = data.Rows[0]["ToApp"].ToString();
                //    Text2.Text = data.Rows[0]["ToApp"].ToString();
                DateToApp.Text = data.Rows[0]["DateToApp"].ToString();

                Who.Text = data.Rows[0]["Who"].ToString();
                Comment.Text = data.Rows[0]["Comment"].ToString();
                //     Text5.Text = data.Rows[0]["Prim"].ToString();
                Prim.Text = data.Rows[0]["Prim"].ToString();
                DateWho.Text = data.Rows[0]["DateWho"].ToString();
               // clos = ((DateTime)data.Rows[0]["DateWho"]);
                Date.Text = DateTime.Now.ToLongDateString();
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
                string prostoy = ((int)pr.TotalDays).ToString() + " дн. " + pr.Hours.ToString() + " час. " +
                    pr.Minutes.ToString() + " мин.";
                Timing.Text = prostoy;
                DocName.Text = "документ";
                //Год/месяц/день
            /*    List<string> xx = new List<string>() {  "Акт неисправности", "Акт внеплановых работ", "Платежное поручение", "Счет" };
                if (!IsPostBack)
                {
                    DocName.DataSource = xx;
                    DocName.DataBind();
                    DocName.SelectedIndex = 0;
                } */
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select count(d.Id) from Documents d" +
                    " where d.NumEvent=@id and d.Usr=@usr" , conn);
                   cmd.Parameters.AddWithValue("id", _wz);
                   cmd.Parameters.AddWithValue("usr", User.Identity.Name);
                   UsrDoc.Text = cmd.ExecuteScalar().ToString();
                }
                List<Data> datadoc = GetData();
                Out.DataSource = datadoc;
                Out.DataBind();

                List<Data> datdoc = GetData1();
                Out1.DataSource = datdoc;
                Out1.DataBind();
              //  DocPodp.Visible = true;
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
        
            Msg.Text = "";

            if (string.IsNullOrEmpty(DocName.Text))
            {
                Msg.Text = "Внимание! Вы забыли ввести наименование доумента!";
                return;
            }
            // запись
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Documents " +
                    "([NumEvent], [Name], [Image], [NameFile], [Status], [Prim], [Usr])  " +
                    "values (@nev, @name, @img, @namefile, @st, @prim, @user) ", conn);
                cmd.Parameters.AddWithValue("nev", _wz);
                cmd.Parameters.AddWithValue("name", DocName.Text);
                cmd.Parameters.AddWithValue("st", "подписан");
                cmd.Parameters.AddWithValue("prim", " ");
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                //  DateTime date; date = DateTime.Now;
                //  cmd.Parameters.AddWithValue("date", date);
                //выбор из устройства
                string namePhoto = FileUpload1.FileName;
                //преобразование в двоичный код
                byte[] photo = FileUpload1.FileBytes;
                if (string.IsNullOrEmpty(namePhoto))
                {
                    Msg.Text = "Внимание! Документ не выбран!";
                    return;
                }
                //Запись документа и его имени файла в БД
                cmd.Parameters.Add("img", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = namePhoto;
                cmd.ExecuteNonQuery();
            }
            Msg.Text = "Документ присоединен к Событию! Добавьте ещё документ или нажмите на логотип [EMICATECH] для возврата на страницу Активных событий!";
            List<Data> datadoc = GetData();
            Out.DataSource = datadoc;
            Out.DataBind();
            DocVw.Visible = true;

          
        }
        List<Data> GetData() 
        {
            List<Data> datadoc = new List<Data>();
            string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Id, Name, Image, NameFile, Status, Prim, Usr from Documents " +
                "where NumEvent=@id and Usr=@usr", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.Parameters.AddWithValue("usr", User.Identity.Name);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datadoc.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
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
        List<Data> GetData1()
        {
            List<Data> datdoc = new List<Data>(); 
            string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Id, Name, Image, NameFile, Status, Prim, Usr from Documents " +
                "where NumEvent=@id and Status=@st", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.Parameters.AddWithValue("st", "не подписан заказчиком");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datdoc.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        NameFile = dr["NameFile"].ToString(),
                        Status = dr["Status"].ToString(),
                        Prim = dr["Prim"].ToString(),
                        Usr = dr["Usr"].ToString()
                    });
                }
                dr.Close();
            }
            return datdoc;
        }
        //редактирование
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
                 //   DateToApp.Text = DataId.Text;
                    SqlCommand cmd = new SqlCommand("update Events set EventId=@e, Sourse=@s, IO=@fio, DataId=@de, LiftId=@z, ToApp=@toapp, DateToApp=@da, Prim=@pr where Id=@i", conn);

                    cmd.Parameters.AddWithValue("s", Sourse.Text);
                    cmd.Parameters.AddWithValue("e", EventId.Text);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.Parameters.AddWithValue("fio", IO.Text);
                    cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                    cmd.Parameters.AddWithValue("z", LiftId.Text);
                    cmd.Parameters.AddWithValue("toapp", ToApp.Text);
                    cmd.Parameters.AddWithValue("pr", Prim.Text); 
                    cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("update Zayavky set Start=@de, PrinyalDate=@da where Id=@zid", conn);
                    cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                    cmd.Parameters.AddWithValue("da", Convert.ToDateTime(DateToApp.Text));
                    cmd.Parameters.AddWithValue("zid", ZayavId.Text);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", _wz);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    cmd.Parameters.AddWithValue("u", User.Identity.Name);
                    cmd.Parameters.AddWithValue("f", IO.Text);
                    cmd.Parameters.AddWithValue("txt", EventId.Text);
                    cmd.Parameters.AddWithValue("cat", "описание неисправности");
                    cmd.Parameters.AddWithValue("ph", "из интерфейса события ОДС");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "редактирование описания события");
                    cmd.ExecuteNonQuery();
                 //   Response.Redirect("~/ZakTSG.aspx");
                }
                Msg.Text = "Изменения записаны! Вернуться назад - нажмите на логотип [EMICATECH.com]";
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
                    cmd.Parameters.AddWithValue("i", ZayavId.Text);
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
        protected void Foto_Click(object sender, EventArgs e)
        {
           
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.[Image], d.NameFile from Documents d " +
                    "where d.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(0, 0, bBuffer, 0, bLength);
                //  просмотр в браузере
                Response.ContentType = "image"; //image/Pdf 
                Response.BinaryWrite(bBuffer);
                string pdfFileName = Request.PhysicalApplicationPath;
                Response.ContentType = "application/x-download";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", DocName.Text));
                //    Response.ContentEncoding = Encoding.UTF8;
                //  Response.BinaryWrite(bBuffer);
                //   Response.WriteFile(pdfFileName);
                //    Response.HeaderEncoding = Encoding.UTF8;
                Response.Flush();
                Response.End();

            }

        }
        protected void Pl_Click(object sender, EventArgs e)
        {
            PinPl.Visible = true; 
        }
        protected void Pinn_Click(object sender, EventArgs e)
        {
            //Ввод пин-кода
              DataTable data = new DataTable();  
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                              conn.Open();
                              SqlCommand cmd = new SqlCommand("select p.surname, p.name, p.midlename, p.education from People p " +
                                      "where p.comments=@user and p.specialty=N'диспетчер' and p.education=@pin", conn);
                              cmd.Parameters.AddWithValue("user", User.Identity.Name);
                              cmd.Parameters.AddWithValue("pin", Pin.Text);
                              SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                              adapter.Fill(data);
                              try
                              {
                                  string Text = "" + data.Rows[0]["surname"].ToString() + " " + data.Rows[0]["name"].ToString() + " " + data.Rows[0]["midlename"].ToString();
                                Prim.Text = "Редактировал:"+ Text;
                              /*   cmd = new SqlCommand("update Events " +
                                 "set Prim=@pr where Id=@i", conn);
                                  cmd.Parameters.AddWithValue("i", _wz);
                                  cmd.Parameters.AddWithValue("pr", Prim.Text); 
                                  cmd.ExecuteNonQuery();*/
                                  Pinn.Visible = true;
                                  PinPl.Visible = false;
                                  ActionPin.Visible = true;
                                  Msg.Text = "Здравствуйте! " + Text + "! Ввод ПИН-кода является аналогом Вашей подписи!";
                              }
                              catch { Msg.Text = "Неверный пин-код!"; return; }
             }

        }

       
    }
}