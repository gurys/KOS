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
using System.IO;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KOS
{
    public partial class WZClose : System.Web.UI.Page
    {
        int _wz = 0;
        int evid; 
       // byte[] _f;
        string WorkPin = "";
        string Spec = "", adrs, numlift;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["wz"]))
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["wz"]);           
            List<string> vidakt = new List<string>() { "Акт дефектовки", "Акт повреждения оборудования", "Акт установки", "Акт выполнения внеплановых работ" };
            List<string> chel = new List<string>() { "", "Да", "Нет" };
            List<string> chas = new List<string>() { "", "Да", "Нет" };
            if (!IsPostBack)
            {
                VidAkta.DataSource = vidakt;
                VidAkta.DataBind();
                VidAkta.SelectedIndex = 0;

                Chel.DataSource = chel;
                Chel.DataBind();
                Chel.SelectedIndex = 0;

                Chas.DataSource = chas;
                Chas.DataBind();
                Chas.SelectedIndex = 0;
              
            }
            if (User.Identity.Name == "manager" || User.Identity.Name == "admin")
            { ActWZNa.Visible = true; }
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetWorkerZayavka(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                From.Text = data.Rows[0]["Family"].ToString() + " " + data.Rows[0]["IO"].ToString();
                DateFrom.Text = ((DateTime)data.Rows[0]["Date"]).ToString();
                Text.Text = data.Rows[0]["Text"].ToString();
              /*  TimeSpan pr = DateTime.Now - ((DateTime)data.Rows[0]["Date"]);
                Prostoy.Text = ((int)pr.TotalDays).ToString() + " дн. " + pr.Hours.ToString() + " час. " +
                pr.Minutes.ToString() + " мин."; */
             //   Name.Text = data.Rows[0]["Name"].ToString();
             //   NumID.Text = data.Rows[0]["NumID"].ToString();
             //   if (NumID.Text =="") Zp.Visible = false;
                Type.Text = data.Rows[0]["Type"] is DBNull ? "" : data.Rows[0]["Type"].ToString();
                LiftId.Text = data.Rows[0]["LiftId"] is DBNull ? "" : data.Rows[0]["LiftId"].ToString();
             //   namefoto.Text = data.Rows[0]["nameFoto"] is DBNull ? "" : data.Rows[0]["nameFoto"].ToString();
            }
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();             
              try {
                    SqlCommand cmd = new SqlCommand("select Id from Events where WZayavId=@wz", conn);
                    cmd.Parameters.AddWithValue("wz", _wz);
                    evid = int.Parse(cmd.ExecuteScalar().ToString());
                    NumEvent.Text = Convert.ToString(evid);//номер события
                 //   NumEvent0.Text = Convert.ToString(evid);
                  }
                catch { NumEvent.Text = "не зарегестрировано"; } 
            }
             int _id = Int32.Parse(NumEvent.Text);
             App_Code.Base db1 = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());            
             DataTable da = db1.GetEvent(_id);
             if (da.Rows.Count < 1) { Msg.Text = "нет события с таким № " + _id; return; }
             Address.Text = da.Rows[0]["Address"].ToString();
             Kol.Text = da.Rows[0]["Kol"].ToString();
             Name.Text = da.Rows[0]["Name"].ToString();
             NumID.Text = da.Rows[0]["NumID"].ToString();
             Obz.Text = da.Rows[0]["Obz"].ToString();
             namefoto.Text = da.Rows[0]["namefoto"].ToString();
             Comm.Text = da.Rows[0]["Comment"].ToString();
             Status.Text = da.Rows[0]["Cansel"].ToString() == "False"  ? "активно" : "закрыто";
             TimeSpan pr = DateTime.Now - ((DateTime)da.Rows[0]["DataId"]);
             if (!(da.Rows[0]["DateWho"] is DBNull)) { pr = ((DateTime)da.Rows[0]["DateWho"]) - ((DateTime)da.Rows[0]["DataId"]); }
             Prostoy.Text = ((int)pr.TotalDays).ToString() + " дн. " + pr.Hours.ToString() + " час. " +
             pr.Minutes.ToString() + " мин.";
             if (Name.Text == "") Zp.Visible = false;
             if (!IsPostBack)
             {
                 //  DopZapp.Visible = true;
                 App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                 DataTable dt = db.GetPartsList(_id);
                 //     if (dt.Rows.Count < 1) DopZapp.Visible = false;
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
        }
        protected void DopZapp_Click(object sender, EventArgs e)
        {
            
        }
      //запрос к базе для конвертации и просмотра Foto
         protected void Foto_Click(object sender, EventArgs e)
         { using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select wz.[Text], wz.[Date], wz.[Type], wz.Readed, wz.LiftId, wz.[Foto], wz.nameFoto, ui.Family, ui.[IO], wz.Done, wz.DoneDate, ui2.Family as WhoFamily, ui2.[IO] as WhoIO from WorkerZayavky wz " +
                    "join UserInfo ui on ui.UserId=wz.UserId " +
                    "left join UserInfo ui2 on ui2.UserId=wz.WhoDone " +
                    "where wz.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _wz);
             //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                int bLength = (int)datareader.GetBytes(5, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(5, 0, bBuffer, 0, bLength);
             //  просмотр в браузере
               Response.ContentType = "image"; //image/Jpeg
               Response.BinaryWrite(bBuffer);
            /*   string commandText = @"mailto:gurus@emicatech.com";// открыть почту по умолчанию
               var proc = new System.Diagnostics.Process();
               proc.StartInfo.FileName = commandText;
               proc.StartInfo.UseShellExecute = true;
               proc.Start();
            */
            }
          
    
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
                     if (phCloseEvent.Visible == false)
                     {
                         Msg.Text = "Внимание! Ввод пин-кода является аналогом Вашей подписи!";
                         CloseEv.Visible = true;
                         ActionWZ.Visible = true;
                         phPodp.Visible = true;
                         phPinClose.Visible = false;
                     }
                     else
                     {
                         Msg.Text = "Внимание! Закрытие События является особо ответственной и необратимой операцией! Eсли Вы уверены, что все работы и документы по этому Событию закрыты, то нажмите кнопку Закрыть Событие! Вернуться без закрытия - На главную.";
                         CloseEv.Visible = true;
                         ActionWZ.Visible = false;
                         phPodp.Visible = true;
                         phPinClose.Visible = false;
                         Button4.Visible = true;
                     }
                 }
                 catch { Msg.Text = "Неверный пин-код!"; return; }
               //  Response.Redirect("~/Default.aspx");
             }
             
         }
  
        protected void Save_Click(object sender, EventArgs e)
         {
             if (Label1.Text == "") { Msg.Text = "Для выполнения этой операции введите ПИН - код!"; phPinClose.Visible = true; return; }
           
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();             
                SqlCommand cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", evid);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
           try
              {
                
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", "дополнительный механик");
                    cmd.Parameters.AddWithValue("ph", "");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);
                
           //     else { Msg.Text = "Не заполнены поля ввода!"; return; }
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", Label1.Text);
                cmd.ExecuteNonQuery();
              }
                 catch { Msg.Text = "Поля ввода пустые!"; return; }
           if (Text1.Text != "")
           {
               cmd = new SqlCommand("update WorkerZayavky " +
                   "set Type=N'ПНР/РЭО' where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", _wz);
               cmd.ExecuteNonQuery();
           }

           if (Text1.Text == "")
           {
               cmd = new SqlCommand("update WorkerZayavky " +
                   "set [Readed]=1 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", _wz);
               cmd.ExecuteNonQuery();
               cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", evid);
               cmd.ExecuteNonQuery();
           }              

            } 
            Response.Redirect("~/"); 
        }
        protected void Akt_Click(object sender, EventArgs e)
        {

            //блок формирования акта
            if (Label1.Text == "") { Msg.Text = "Для формирования актов требуется ввести ПИН-код!"; phPinClose.Visible = true; return; }
            string PodpEmic = Spec + " " + WorkPin;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select t.Ttx from Ttx t " +
                     "join LiftsTtx l on l.TtxId=t.Id " +
                    //   "join Ttx tt on tt.Id=t.Id " +
                     "where t.TtxTitleId=1 and l.LiftId=@t", conn);
                cmd.Parameters.AddWithValue("t", LiftId.Text);
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
                string dat = DateTime.Now.Date.ToLongDateString();
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
                // _wz = Int32.Parse(NumEvent.Text);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetEvent(evid);
                string worker = data.Rows[0]["Family"].ToString() + " " + data.Rows[0]["IO"].ToString();
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                if (VidAkta.SelectedValue == "Акт дефектовки")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktDef.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("NaktDef", NumEvent.Text + "-А1.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", LiftId.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", LiftId.Text);
                    fields.SetField("opis1", data.Rows[0]["EventId"].ToString());
                    fields.SetField("fill_6", Chel.SelectedValue);
                    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP",  " ");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
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
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text + "_" + dd + mm + yy + "_A1_1.pdf";
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
                else if (VidAkta.SelectedValue == "Акт повреждения оборудования")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktPovr.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("NaktPovr", NumEvent.Text + "-А3.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", LiftId.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", LiftId.Text);
                    fields.SetField("opis1", data.Rows[0]["EventId"].ToString());
                    fields.SetField("fill_6", Chel.SelectedValue);
                    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
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
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text + "_" + dd + mm + yy + "_A3_1.pdf";
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
                else if (VidAkta.SelectedValue == "Акт установки")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktUst.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("NaktUst", NumEvent.Text + "-А2.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", LiftId.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", LiftId.Text);
                    fields.SetField("opis1", data.Rows[0]["EventId"].ToString());
                    fields.SetField("fill_6", Chel.SelectedValue);
                //    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
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
                    cmd.Parameters.AddWithValue("name", "акт установки");
                    cmd.Parameters.AddWithValue("usr", Label1.Text);
                    cmd.Parameters.AddWithValue("nev", NumEvent.Text);
                    cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text + "_" + dd + mm + yy + "_A2_1.pdf";
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
                else if (VidAkta.SelectedValue == "Акт выполнения внеплановых работ")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktVypr.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("NaktVr", NumEvent.Text + "-А4.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", LiftId.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", LiftId.Text);
                    fields.SetField("opis1", data.Rows[0]["EventId"].ToString());
                    fields.SetField("fill_6", Chel.SelectedValue);
                //    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
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
                    cmd.Parameters.AddWithValue("name", "акт выполнения внеплановых работ");
                    cmd.Parameters.AddWithValue("nev", NumEvent.Text);
                    cmd.Parameters.AddWithValue("usr", Label1.Text);
                    cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text + "_" + dd + mm + yy + "_A4_1.pdf";
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
                cmd.Parameters.AddWithValue("name", Text4.Text);
                cmd.Parameters.AddWithValue("obz", Text7.Text);
                cmd.Parameters.AddWithValue("numid", Text2.Text);
                cmd.Parameters.AddWithValue("kol", Text3.Text);
                cmd.ExecuteNonQuery();
            
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable dt = db.GetPartsList(evid);
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
            Text4.Text = ""; Text7.Text = ""; Text2.Text = ""; Text3.Text = "";
        }
        protected void ZakParts_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для выполнения этой операции введите ПИН - код!"; phPinClose.Visible = true; return; }

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update WorkerZayavky " +
                    "set [Readed]=1 where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", evid);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", "Заказал: " +  Label1.Text);
                cmd.Parameters.AddWithValue("txt", "Заказаны запчасти");
                cmd.Parameters.AddWithValue("cat", "запчасти и расходные материалы");
                cmd.Parameters.AddWithValue("ph", "");
                cmd.Parameters.AddWithValue("to", "manager");
                cmd.Parameters.AddWithValue("cm", "Отправлено из интерфейса механика менеджеру");
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", evid);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/");
        }
        protected void PartsZak_Click(object sender, EventArgs e)
        {
            phDopZap.Visible = true;
           
        }
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/WZClose.aspx");

            List<string> roles = new List<string>() { "Manager", "Administrator", "Worker", "Cadry"};
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
   //выполняет закрытие  заявок от механиков
        protected void Done_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для закрытия События введите ПИН - код"; phPinClose.Visible = true; return; }
            Text1.Text = Comm.Text;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update WorkerZayavky " +
                    "set [Done]=1, WhoDone=(select UserId from Users where UserName=@user), DoneDate=@d, [Readed]=1 where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Events " +
                 "set Who=@who, DateWho=@dw, Cansel=@c, DateCansel=@d, Comment=@cm where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", evid);
                cmd.Parameters.AddWithValue("c", true);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("dw", DateTime.Now);
                cmd.Parameters.AddWithValue("who", Label1.Text);
                cmd.Parameters.AddWithValue("cm", Text1.Text);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/Default.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void DopZap_Click(object sender, EventArgs e)
        {
            phDopZap.Visible = true;
            
        }

        protected void FormAkt_Click(object sender, EventArgs e)
        {
            phAktZap.Visible = true;
            phDopZap.Visible = true;            
           
        }

        protected void DopMex_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для выполнения этой операции введите ПИН - код!"; phPinClose.Visible = true; return; }
           
            phDopZap.Visible = false;
            phAktZap.Visible = false;
           
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();             
                SqlCommand cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", evid);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
           try
              {
                
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", "дополнительный механик");
                    cmd.Parameters.AddWithValue("ph", "обработка внутреннего события");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);
                
           //     else { Msg.Text = "Не заполнены поля ввода!"; return; }
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", Label1.Text);
                cmd.ExecuteNonQuery();
              }
                 catch { Msg.Text = "Поля ввода пустые!"; return; }
          
               cmd = new SqlCommand("update WorkerZayavky " +
                   "set [Readed]=1 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", _wz);
               cmd.ExecuteNonQuery();
               cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", evid);
               cmd.ExecuteNonQuery();
            } 
            Response.Redirect("~/"); 
        }
       

        protected void ReoPnr_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для выполнения этой операции введите ПИН - код!"; phPinClose.Visible = true; return; }

            phDopZap.Visible = false;
            phAktZap.Visible = false;
           
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();             
                SqlCommand cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", evid);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
           try
              {
                
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", "ПНР/РЭО");
                    cmd.Parameters.AddWithValue("ph", "обработка внутреннего события");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);
                
           //     else { Msg.Text = "Не заполнены поля ввода!"; return; }
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", Label1.Text);
                cmd.ExecuteNonQuery();
              }
                 catch { Msg.Text = "Поля ввода пустые!"; return; }
          
               cmd = new SqlCommand("update WorkerZayavky " +
                   "set Type=N'ПНР/РЭО' where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", _wz);
               cmd.ExecuteNonQuery();      
               cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", evid);
               cmd.ExecuteNonQuery();
           }              

             
            Response.Redirect("~/"); 
        }
        
        protected void Vnr_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для выполнения этой операции введите ПИН - код!"; phPinClose.Visible = true; return; }
            phDopZap.Visible = false;
            phAktZap.Visible = false;
          
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", evid);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                try
                {

                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", "ВНР");
                    cmd.Parameters.AddWithValue("ph", "обработка внутреннего события");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);

                    //     else { Msg.Text = "Не заполнены поля ввода!"; return; }
                    cmd.Parameters.AddWithValue("u", User.Identity.Name);
                    cmd.Parameters.AddWithValue("f", Label1.Text);
                    cmd.ExecuteNonQuery();
                }
                catch { Msg.Text = "Поля ввода пустые!"; return; }

                cmd = new SqlCommand("update WorkerZayavky " +
                    "set [Readed]=1 where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", evid);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/Planning.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            phPinClose.Visible = false;                  
            phDopZap.Visible = false;
            phAktZap.Visible = false;
           
        }

        protected void ZakZap_Click(object sender, EventArgs e)
        {
            phDopZap.Visible = true;
           
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
           
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

           if (User.Identity.Name == "manager" || User.Identity.Name == "admin" )
           {
              SqlCommand cmd = new SqlCommand("update WorkerZayavky " +
                   "set [Readed]=0 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", _wz);
               cmd.ExecuteNonQuery();

               cmd = new SqlCommand("update Events set [ZaprosMng]=0 where Id=@id", conn);
               cmd.Parameters.AddWithValue("id", evid);
               cmd.ExecuteNonQuery();

               cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
               cmd.Parameters.AddWithValue("ne", evid);
               cmd.Parameters.AddWithValue("d", DateTime.Now);
               cmd.Parameters.AddWithValue("u", User.Identity.Name);
               cmd.Parameters.AddWithValue("f", Label1.Text);
               cmd.Parameters.AddWithValue("txt", Text1.Text);
               cmd.Parameters.AddWithValue("cat", "активация");
               cmd.Parameters.AddWithValue("ph", "из обработчика события");
               cmd.Parameters.AddWithValue("to", "manager");
               cmd.Parameters.AddWithValue("cm", "перевод в активные у механика");
               cmd.ExecuteNonQuery();
           }              

            } 
            Response.Redirect("~/"); 
        }
        protected void ZkEv_Click(object sender, EventArgs e)
        {
            phPinClose.Visible = true;
            phPodp.Visible = false;
            ActionWZ.Visible = false;
            ZkEv.Visible = false;
            Msg.Text = "Для Закрытия События требуется ввести пин-код! ";

            phCloseEvent.Visible = true;
        }

        protected void OpisNi_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для выполнения этой операции введите ПИН - код!"; phPinClose.Visible = true; return; }
            if (Text1.Text != "")
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update WorkerZayavky " +
                        "set Text=@txt where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", _wz);
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", evid);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    cmd.Parameters.AddWithValue("u", User.Identity.Name);
                    cmd.Parameters.AddWithValue("f", "описал: " + Label1.Text);
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", Type.Text);
                    cmd.Parameters.AddWithValue("ph", "описание неисправности");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "подробное описание неисправности после осмотра");
                    cmd.ExecuteNonQuery();

                  /*   cmd = new SqlCommand("update Events set Comment=@com where Id=@id", conn);
                       cmd.Parameters.AddWithValue("id", evid);
                       cmd.Parameters.AddWithValue("com", Text1.Text);
                       cmd.ExecuteNonQuery(); */
                }
            else { Msg.Text = "Поле ввода пуcтое! Введите подробное описание неисправности!"; return; }
            Response.Redirect("~/WZClose.aspx?wz=" +_wz.ToString());
        }
     
    }
}