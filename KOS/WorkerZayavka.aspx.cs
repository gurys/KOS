using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using KOS.App_Code;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace KOS
{
    public partial class WorkerZayavka : System.Web.UI.Page
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
        string fam, io, nzay, u, m, adr, adrs, numlift;
        string _role = "Worker";
        string _liftId = string.Empty;
        string _url = string.Empty;
        int evid;
        string WorkPin = "-";
        string Spec = "";
        List<Base.PersonData> _workers = new List<Base.PersonData>();
        protected void Page_Load(object sender, EventArgs e)
        {

            CheckAccount();
            if (!string.IsNullOrEmpty(Request["lift"]))
                _liftId = HttpUtility.HtmlDecode(Request["lift"]);
            if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
                _url = HttpUtility.HtmlDecode(Request["ReturnUrl"]);
            
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                           "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        fam = dr[0].ToString(); io = dr[1].ToString();
                    dr.Close();                   
                   
                } 
   
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                _workers = db.GetWorkers();
                if (!IsPostBack)
                {
                  
                    List<string> ls = db.GetIdU();
                    IdU.DataSource = ls;
                    IdU.DataBind();
                    if (ls.Count > 0)
                        IdU.SelectedIndex = 0;
                    IdU_SelectedIndexChanged(sender, e);
              
                string[] ss = { "1", "2" };
                Category.DataSource = ss;
                Category.DataBind();
                Category.SelectedIndex = 0;
                db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> types = db.GetWZTypes();
                Type.DataSource = types;
                Type.DataBind();
                Type.SelectedIndex = 0;
                PrimLift_TextChanged(this, EventArgs.Empty);
                LiftId_SelectedIndexChanged(this, EventArgs.Empty);

                To.DataSource = _workers;
                To.DataBind();
                To.SelectedIndex = 0;
                }
                List<string> typeakt = new List<string>() { "Акт дефектовки", "Акт повреждения оборудования" };
                List<string> chel = new List<string>() { "", "Да", "Нет" };
                List<string> chas = new List<string>() { "", "да", "Нет" };
                if (!IsPostBack)
                {
                    TypeAkt.DataSource = typeakt;
                    TypeAkt.DataBind();
                    TypeAkt.SelectedIndex = 0;

                    Chel.DataSource = chel;
                    Chel.DataBind();
                    Chel.SelectedIndex = 0;

                    Chas.DataSource = chas;
                    Chas.DataBind();
                    Chas.SelectedIndex = 0;

                } 
        }

      protected void IdU_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> ls = db.GetIdM(IdU.SelectedValue);
            IdM.DataSource = ls;
            IdM.DataBind();
            if (ls.Count > 0)
                IdM.SelectedIndex = 0;
            IdM_SelectedIndexChanged(sender, e);
            LiftId_SelectedIndexChanged(this, EventArgs.Empty);
         
        }
        protected void IdM_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> liftId = db.GetLiftId(IdU.SelectedValue, IdM.SelectedValue);
            LiftId.DataSource = liftId;
            LiftId.DataBind();
            LiftId.SelectedIndex = 0;
            LiftId_SelectedIndexChanged(this, EventArgs.Empty);
         
        }
  
        protected void LiftId_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {               
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> works = db.GetWorkers(LiftId.SelectedValue);
                To.DataSource = works;
                To.DataBind();
                To.SelectedIndex = 0;
            }
            catch { return; }
        }
          protected void Address_TextChanged(object sender, EventArgs e)
        {
            if (Address.SelectedItem == null)
                return;
            string a = Address.SelectedItem.Value;
            adr = a;
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
                LiftId.DataSource = lifts;
                LiftId.DataBind();
                LiftId.SelectedIndex = 0;
            }
        }  
            
        protected void Button1_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void Pinn_Click(object sender, EventArgs e)
        {
            //Ввод пин-кода
            DataTable data = new DataTable();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.surname, p.name, p.midlename, p.specialty, p.education from People p " +
                        "where p.comments=@user  and p.education=@pin", conn); //and p.specialty=N'Электромеханик'
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("pin", Pin.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                try
                {
                    WorkPin = "" + data.Rows[0]["surname"].ToString() + " " + data.Rows[0]["name"].ToString() + data.Rows[0]["midlename"].ToString() + "";
                    Spec = data.Rows[0]["specialty"].ToString();
                    Label1.Text = Spec + "  " + WorkPin;
                    /*  */
                    //   CloseEv.Visible = true;
                    phPinClose.Visible = false;
                }
                catch { Msg.Text = "Неверный пин-код!"; return; }
                //  Response.Redirect("~/Default.aspx");
            }

        }
        protected void Save_Click(object sender, EventArgs e)
        {
            /*   if (LiftId.SelectedIndex == 0)
               {
                   Msg.Text = "Внимание! Вы забыли выбрать номер лифта.";
                   return;
               }*/
            // добавлено поле для фото и его имени файла
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into WorkerZayavky " +
                    "(UserId, [Date], [Text], LiftId, [Type], [Foto], nameFoto, [Name], [NumID], [Kol]) " +
                    "values ((select UserId from Users where UserName=@user), @date, @text, @lift, @type, @foto, @namefoto, @name, @numid, @kol)", conn);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("text", Text.Text);
                cmd.Parameters.AddWithValue("name", Text1.Text);
                cmd.Parameters.AddWithValue("numid", Text2.Text);
                cmd.Parameters.AddWithValue("kol", Text3.Text);
                DateTime date; date = DateTime.Now;
                cmd.Parameters.AddWithValue("date", date);
                cmd.Parameters.AddWithValue("lift", string.IsNullOrEmpty(LiftId.SelectedValue) ? null : LiftId.SelectedValue);
                cmd.Parameters.AddWithValue("type", Type.SelectedValue);
                //выбор фото из устройства
                string namePhoto = FileUpload.FileName;
                //преобразование в двоичный код
                byte[] photo = FileUpload.FileBytes;
                //Запись фото и его имени файла в БД
                cmd.Parameters.Add("foto", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefoto", SqlDbType.NVarChar).Value = namePhoto;
                cmd.ExecuteNonQuery();
                //выбор номера
                cmd = new SqlCommand("select wz.Id from WorkerZayavky wz " +
                    "join Users u on wz.UserId=u.UserId " +
                    "where u.UserName=@user and wz.LiftId=@liftId and [Date]=@s", conn);
                cmd.Parameters.AddWithValue("liftId", LiftId.SelectedValue);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("s", date);
                int id = int.Parse(cmd.ExecuteScalar().ToString());
                nzay = Convert.ToString(id);//номер заявки
                cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    fam = dr[0].ToString(); io = dr[1].ToString();
                dr.Close();
                cmd = new SqlCommand("select LiftId, IdU, IdM from Lifts " +
                  "where LiftId=@lift", conn);
                cmd.Parameters.AddWithValue("lift", LiftId.SelectedValue);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { u = dr[1].ToString(); m = dr[2].ToString(); }
                dr.Close();
                cmd = new SqlCommand("select t.Ttx from Ttx t " +
                    "join LiftsTtx l on l.TtxId=t.Id " +
                 //   "join Ttx tt on tt.Id=t.Id " +
                    "where t.TtxTitleId=1 and l.LiftId=@t", conn);
                cmd.Parameters.AddWithValue("t", LiftId.SelectedValue);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { adrs = dr[0].ToString(); }
                dr.Close();
        //запись события в БД
                cmd = new SqlCommand("insert into Events" +
                  "(EventId, RegistrId, DataId, WZayavId, Sourse, Family, IO, TypeId, IdU, IdM, LiftId, Foto, namefoto, Name, NumID, Kol, Address, Obz ) " +
                  "values (@text, @reg, @s, @id, @f, @fam, @io, @c, @u, @m, @liftid, @foto, @namefoto, @name, @numid, @kol, @adr, @obz )", conn);
                cmd.Parameters.AddWithValue("text", Text.Text);
                cmd.Parameters.AddWithValue("reg", "Эксплуатация лифтов");
                cmd.Parameters.AddWithValue("s", date);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("f", "Эмика Техник");
                cmd.Parameters.AddWithValue("fam", fam);
                cmd.Parameters.AddWithValue("io", io);
                cmd.Parameters.AddWithValue("c", Type.SelectedValue);
                cmd.Parameters.AddWithValue("u", u);
                cmd.Parameters.AddWithValue("m", m);
                cmd.Parameters.AddWithValue("liftId", LiftId.SelectedValue);
                cmd.Parameters.Add("foto", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefoto", SqlDbType.NVarChar).Value = namePhoto;
                cmd.Parameters.AddWithValue("name", Text1.Text);
                cmd.Parameters.AddWithValue("obz", TextBox1.Text);
                cmd.Parameters.AddWithValue("numid", Text2.Text);
                cmd.Parameters.AddWithValue("kol", Text3.Text);
                cmd.Parameters.AddWithValue("adr", adrs);
                cmd.ExecuteNonQuery();
        // получаем номер события
                cmd = new SqlCommand("select Id from Events where DataId=@s and WZayavId=@id", conn);
                cmd.Parameters.AddWithValue("s", date);
                cmd.Parameters.AddWithValue("id", id);
                evid = int.Parse(cmd.ExecuteScalar().ToString());
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Msg.Text = "Заявка зарегистрирована! При необходимости, добавьте дополнительные запчасти в список. ";
                    NumEvent.Text = dr[0].ToString(); 
                }
                dr.Close();
                Parts_Click(sender, e);
            }
          
            if (Type.Text == "замечания по лифтам")
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into ZPrim (LiftId, [Date], WhoWrote, Responce, Category, [To], WzId) " +
                    "values (@liftId, @d, (select UserId from Users where UserName=@userName), @r, @c, @t, @id)", conn);
                cmd.Parameters.AddWithValue("liftId", LiftId.SelectedValue);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                cmd.Parameters.AddWithValue("r", Text.Text);
                cmd.Parameters.AddWithValue("id", nzay);
                cmd.Parameters.AddWithValue("c", int.Parse(Category.SelectedValue));
                Base.PersonData pd = _workers.Find(delegate(Base.PersonData i)
                {
                    return i.Title == To.SelectedValue;
                });
                cmd.Parameters.AddWithValue("t", pd.Id);
                cmd.ExecuteNonQuery();
                Msg.Text = "Заявка зарегистрирована! Замечание добавлено в базу!";
                if (!string.IsNullOrEmpty(_url))
                    Response.Redirect(_url);
            }
            Text1.Text = ""; TextBox1.Text = ""; Text2.Text = ""; Text3.Text = "";
        }
        protected void PrimLift_TextChanged(object sender, EventArgs e)
        {
            if (Type.Text != "замечания по лифтам")
                PrimLift.Visible = false;
            if (Type.Text == "замечания по лифтам")
            PrimLift.Visible = true;
        }
        protected void Akt_Click(object sender, EventArgs e)
        {
            //блок формирования акта 
            if (Label1.Text == "") { Msg.Text = "Для формирования актов требуется ПИН-код!"; return; }
            string PodpEmic = Spec + " " + WorkPin;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select t.Ttx from Ttx t " +
                    "join LiftsTtx l on l.TtxId=t.Id " +
                    //   "join Ttx tt on tt.Id=t.Id " +
                    "where t.TtxTitleId=1 and l.LiftId=@t", conn);
                cmd.Parameters.AddWithValue("t", LiftId.SelectedValue);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { adrs = dr[0].ToString(); }
                dr.Close();
                cmd = new SqlCommand("select PlantNum from PhisicalAddr where LiftId=@lift", conn);
                cmd.Parameters.AddWithValue("lift", LiftId.Text);
                SqlDataReader dn = cmd.ExecuteReader();
                if (dn.Read())
                { numlift = dn[0].ToString(); }
                dn.Close();
                int _wz = 0; 
                string dat = DateTime.Now.Date.ToLongDateString();
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
                _wz = Int32.Parse(NumEvent.Text);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetEvent(_wz);
                string worker = data.Rows[0]["Family"].ToString() + " " + data.Rows[0]["IO"].ToString();
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                if (TypeAkt.SelectedValue == "Акт дефектовки")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktDef.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);              
                    fields.SetField("NaktDef", NumEvent.Text + "-А1.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", LiftId.SelectedValue + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", LiftId.SelectedValue);
                    fields.SetField("opis1", data.Rows[0]["EventId"].ToString());
                    fields.SetField("fill_6", Chel.SelectedValue);
                    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("name1", Name1.Text);
                    if (Name1.Text != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name2", Name2.Text);
                    if (Name2.Text != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name3", Name3.Text);
                    if (Name3.Text != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name4", Name4.Text);
                    if (Name4.Text != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name5", Name5.Text);
                    if (Name5.Text != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("obz1", Obz1.Text);
                    fields.SetField("obz2", Obz2.Text);
                    fields.SetField("obz3", Obz3.Text);
                    fields.SetField("obz4", Obz4.Text);
                    fields.SetField("obz5", Obz5.Text);
                    fields.SetField("ID1", ID1.Text);
                    fields.SetField("ID2", ID2.Text);
                    fields.SetField("ID3", ID3.Text);
                    fields.SetField("ID4", ID4.Text);
                    fields.SetField("ID5", ID5.Text);
                    fields.SetField("kol1", Kol1.Text);
                    fields.SetField("kol2", Kol2.Text);
                    fields.SetField("kol3", Kol3.Text);
                    fields.SetField("kol4", Kol4.Text);
                    fields.SetField("kol5", Kol5.Text);
                    stamper.FormFlattening = false;// ложь - открыт для записи, истина - закрыт
                    stamper.Close();                    
                    // запись в БД
                    FileStream fs = new FileStream(@"C:\temp\akt.pdf", FileMode.Open);
                    Byte[] pdf = new byte[fs.Length];
                    fs.Read(pdf, 0, pdf.Length);                    
                     cmd = new SqlCommand("insert into Documents (Name, NumEvent, Image, NameFile, Status, Usr) values (@name, @nev, @img, @namefile, @st, @usr )", conn);
                    cmd.Parameters.AddWithValue("name", "акт дефектовки");
                    cmd.Parameters.AddWithValue("nev", NumEvent.Text);
                    cmd.Parameters.AddWithValue("usr", Label1.Text);
                    cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text + "-A1_1.pdf";
                    cmd.Parameters.AddWithValue("st", "не подписан заказчиком");
                    cmd.ExecuteNonQuery();
                    fs.Close();
               /*     string commandText = @"C:\temp\akt.pdf";
                    var proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = commandText;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
              */
                    Response.ContentType = "image"; //image/Jpeg
                    Response.BinaryWrite(pdf);
                }
                else if (TypeAkt.SelectedValue == "Акт повреждения оборудования")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktPovr.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("NaktPovr", NumEvent.Text + "-А3.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", LiftId.SelectedValue + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", LiftId.SelectedValue);
                    fields.SetField("opis1", data.Rows[0]["EventId"].ToString());
                    fields.SetField("fill_6", Chel.SelectedValue);
                    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("name1", Name1.Text);
                    if (Name1.Text != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name2", Name2.Text);
                    if (Name2.Text != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name3", Name3.Text);
                    if (Name3.Text != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name4", Name4.Text);
                    if (Name4.Text != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name5", Name5.Text);
                    if (Name5.Text != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("obz1", Obz1.Text);
                    fields.SetField("obz2", Obz2.Text);
                    fields.SetField("obz3", Obz3.Text);
                    fields.SetField("obz4", Obz4.Text);
                    fields.SetField("obz5", Obz5.Text);
                    fields.SetField("ID1", ID1.Text);
                    fields.SetField("ID2", ID2.Text);
                    fields.SetField("ID3", ID3.Text);
                    fields.SetField("ID4", ID4.Text);
                    fields.SetField("ID5", ID5.Text);
                    fields.SetField("kol1", Kol1.Text);
                    fields.SetField("kol2", Kol2.Text);
                    fields.SetField("kol3", Kol3.Text);
                    fields.SetField("kol4", Kol4.Text);
                    fields.SetField("kol5", Kol5.Text);
                    stamper.FormFlattening = false;
                    stamper.Close();                   
                    // запись в БД
                    FileStream fs = new FileStream(@"C:\temp\akt.pdf", FileMode.Open);
                    Byte[] pdf = new byte[fs.Length];
                    fs.Read(pdf, 0, pdf.Length);                   
                     cmd = new SqlCommand("insert into Documents (Name, NumEvent, Image, NameFile, Status, Usr) values (@name, @nev, @img, @namefile, @st, @usr )", conn);
                    cmd.Parameters.AddWithValue("name", "акт повреждения оборудования");
                    cmd.Parameters.AddWithValue("nev", NumEvent.Text);
                    cmd.Parameters.AddWithValue("usr", Label1.Text);
                    cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text + "-A3_1.pdf";
                    cmd.Parameters.AddWithValue("st", "не подписан заказчиком");
                    cmd.ExecuteNonQuery();
                    fs.Close();
                /*   string commandText = @"C:\temp\akt.pdf";
                    var proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = commandText;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                */
                    Response.ContentType = "image"; //image/Jpeg
                    Response.BinaryWrite(pdf);
                }
            }
        }
        protected void Parts_Click(object sender, EventArgs e) 
        {           
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into PartsList (NumEvent, Foto, namefoto, Name, NumID, Kol, Obz) values (@nev, @foto, @namefoto, @name, @numid, @kol, @obz) ", conn);
                //выбор фото из устройства
                string namePhoto = FileUpload.FileName;
                //преобразование в двоичный код
                byte[] photo = FileUpload.FileBytes; 
                cmd.Parameters.AddWithValue("nev", NumEvent.Text); 
                cmd.Parameters.Add("foto", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefoto", SqlDbType.NVarChar).Value = namePhoto;
                cmd.Parameters.AddWithValue("name", Text1.Text);
                cmd.Parameters.AddWithValue("obz", TextBox1.Text);
                cmd.Parameters.AddWithValue("numid", Text2.Text);
                cmd.Parameters.AddWithValue("kol", Text3.Text);
                cmd.ExecuteNonQuery();
                int ei = int.Parse(NumEvent.Text);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable dt = db.GetPartsList(ei);
                try
                {
                    Name1.Text = dt.Rows[0]["Name"] is DBNull ? "" : dt.Rows[0]["Name"].ToString();
                    Obz1.Text = dt.Rows[0]["Obz"] is DBNull ? "" : dt.Rows[0]["Obz"].ToString();
                    ID1.Text = dt.Rows[0]["NumID"] is DBNull ? "" : dt.Rows[0]["NumID"].ToString();
                    Kol1.Text = dt.Rows[0]["Kol"] is DBNull ? "" : dt.Rows[0]["Kol"].ToString();
                }

                catch { }
                try
                {
                    Name2.Text = dt.Rows[1]["Name"] is DBNull ? "" : dt.Rows[1]["Name"].ToString();
                    Obz2.Text = dt.Rows[1]["Obz"] is DBNull ? "" : dt.Rows[1]["Obz"].ToString();
                    ID2.Text = dt.Rows[1]["NumID"] is DBNull ? "" : dt.Rows[1]["NumID"].ToString();
                    Kol2.Text = dt.Rows[1]["Kol"] is DBNull ? "" : dt.Rows[1]["Kol"].ToString();
                }
                catch { }
                try
                {
                    Name3.Text = dt.Rows[2]["Name"] is DBNull ? "" : dt.Rows[2]["Name"].ToString();
                    Obz3.Text = dt.Rows[2]["Obz"] is DBNull ? "" : dt.Rows[2]["Obz"].ToString();
                    ID3.Text = dt.Rows[2]["NumID"] is DBNull ? "" : dt.Rows[2]["NumID"].ToString();
                    Kol3.Text = dt.Rows[2]["Kol"] is DBNull ? "" : dt.Rows[2]["Kol"].ToString();
                }
                catch { }
                try
                {
                    Name4.Text = dt.Rows[3]["Name"] is DBNull ? "" : dt.Rows[3]["Name"].ToString();
                    Obz4.Text = dt.Rows[3]["Obz"] is DBNull ? "" : dt.Rows[3]["Obz"].ToString();
                    ID4.Text = dt.Rows[3]["NumID"] is DBNull ? "" : dt.Rows[3]["NumID"].ToString();
                    Kol4.Text = dt.Rows[3]["Kol"] is DBNull ? "" : dt.Rows[3]["Kol"].ToString();
                }
                catch { }
                try
                {
                    Name5.Text = dt.Rows[4]["Name"].ToString();
                    Obz5.Text = dt.Rows[4]["Obz"].ToString();
                    ID5.Text = dt.Rows[4]["NumID"].ToString();
                    Kol5.Text = dt.Rows[4]["Kol"].ToString();
                }
                catch { }
            }
            Text1.Text = ""; Text2.Text = ""; Text3.Text = ""; TextBox1.Text = "";
        }
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/WorkerZayavka.aspx");
            List<string> roles = new List<string>() { "Worker", "Electronick", "Manager", "Administrator", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
    }
}
