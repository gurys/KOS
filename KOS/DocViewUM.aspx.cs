using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;
using KOS.App_Code;

namespace KOS
{
    public partial class DocViewUM : System.Web.UI.Page 
    {
        int _wz = 0;
        string _role;
        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetDocUm(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                Id.Text = _wz.ToString();
                name.Text = data.Rows[0]["name"].ToString();
                namefile.Text = data.Rows[0]["namefile"].ToString();
                status.Text = data.Rows[0]["status"].ToString();
                primm.Text = data.Rows[0]["primm"].ToString();
                if (_role == "ODS_tsg") phPodp.Visible = true;
                if (_role == "Cadry" || _role == "Manager") DelDoc.Visible = true;
            }

        }
        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "ODS_tsg" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS_tsg";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ManagerTSG";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Cadry";
            Response.Redirect("~/About.aspx");
            return "";
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
                        "where p.comments=@user and p.specialty=N'заказчик' and p.education=@pin", conn);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("pin", Pin.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                try
                {
                    string Text = "" + data.Rows[0]["surname"].ToString() + " " + data.Rows[0]["name"].ToString() + " " + data.Rows[0]["midlename"].ToString();

                    cmd = new SqlCommand("insert into HistEv ( Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values ( @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                   // cmd.Parameters.AddWithValue("ne", NumEvent.Text);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    cmd.Parameters.AddWithValue("u", User.Identity.Name);
                    cmd.Parameters.AddWithValue("f", Text);
                    cmd.Parameters.AddWithValue("txt", "заказчик: " + Text);
                    cmd.Parameters.AddWithValue("cat", "документы на подпись");
                    cmd.Parameters.AddWithValue("ph", "пин: " + Pin.Text);
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "введен пин-код");
                    cmd.ExecuteNonQuery();
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
                catch { Msg.Text = "Неверный пин-код! Попробуйте ещё раз."; return; }
            }

        }
        //запрос к базе для скачивания
        protected void Foto_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.[img], d.namefile from DocUM d " +
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
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", namefile.Text));
                //    Response.ContentEncoding = Encoding.UTF8;
                //  Response.BinaryWrite(bBuffer);
                //   Response.WriteFile(pdfFileName);
                //    Response.HeaderEncoding = Encoding.UTF8;
                Response.Flush();
                Response.End();
            }

        }
        //запрос для просмотра
        protected void Donl_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.[img], d.namefile from DocUM d " +
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
                Response.Flush();
                Response.End();
               
            }
            Response.Redirect("~/");
        }
        protected void Edit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.[img], d.namefile from DocUM d " +
                    "where d.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(0, 0, bBuffer, 0, bLength);
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                PdfReader read = new PdfReader(bBuffer);
                PdfStamper stamper = new PdfStamper(read, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                AcroFields fields = stamper.AcroFields;
                fields.AddSubstitutionFont(baseFont);
                if (TextZak.Text != "")
                    fields.SetField("Podt", TextZak.Text); //ФИО заказчика
                fields.SetField("dateP", dd + "." + mm + "." + yy + "г.");
                fields.SetField("aspP", "pin: ok"); //подпись заказчика
                stamper.FormFlattening = true;// ложь - открыт для записи, истина - закрыт
                stamper.Close();
                datareader.Close();
                // запись в БД
                FileStream fs = new FileStream(@"C:\temp\mgm.pdf", FileMode.Open);
                Byte[] pdf = new byte[fs.Length];
                fs.Read(pdf, 0, pdf.Length);
                cmd = new SqlCommand("update DocUM set img=@img, status=@st where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                cmd.Parameters.AddWithValue("st", "подписан заказчиком");
                cmd.ExecuteNonQuery();
                fs.Close();
                //  Thread.Sleep(10000);
                Response.ContentType = "image"; //image/Jpeg
                Response.BinaryWrite(pdf);
                //  просмотр в браузере

            }

        }
        // удаление документа
        protected void DelDoc_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from [DocUM] " +
                    "where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/BaseDocUM.aspx");
        }

    }
}