using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using System.IO;
using System.Web.Configuration;
using KOS.App_Code;
using System.Data;
using System.Data.SqlClient;

namespace KOS
{
    public partial class Akt : System.Web.UI.Page
    {
         int _type = 0; // family
        string _role, fam, io;
        class Lift
        {
            public string _LiftId { get; set; } 
            public string Ttx { get; set; }
        }
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string EventId { get; set; }
            public string RegistrId { get; set; }
            public string DataId { get; set; }
            public string ZayavId { get; set; }
            public string Url1 { get; set; }
            public string WZayavId { get; set; }
            public string Url2 { get; set; }
            public string Sourse { get; set; }
            public string Family { get; set; }
            public string IO { get; set; } 
            public string TypeId { get; set; }
            public string IdU { get; set; }
            public string IdM { get; set; }
            public string LiftId { get; set; }
            public string Address { get; set; }
            public string Status { get; set; } 
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();
            //   _role = "ODS";
            if (!string.IsNullOrEmpty(Request["t"]))
                _type = int.Parse(Request["t"]);

            if (!IsPostBack)
            {
                KOS.App_Code.ClearTemp clear = new App_Code.ClearTemp(Request);
                clear.DeleteOld();

                UpdateLabel();

                List<Data> data = GetData();
                Out.DataSource = data;
                Out.DataBind();
                List<Data> datana = GetDataNa();
                OutNa.DataSource = datana;
                OutNa.DataBind();
            }
        }

        void UpdateLabel()
        {
            switch (_type)
            {
                case 0:
                    What.Text = "Мои Заявки";
                    break;
                case 1:
                    What.Text = "Все Заявки " + DateTime.Now.Date.ToShortDateString();
                    break;
               
            }
        }

        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
            string url = "~/ZakEvWork.aspx?zId=";
            List<Lift> data1 = new List<Lift>();

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
               
                conn.Open();
                SqlCommand cmd = cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataReader dt = cmd.ExecuteReader();
                if (dt.Read())
                    fam = dt[0].ToString(); io = dt[1].ToString();
                dt.Close();
                    cmd = new SqlCommand();
                    string s = "select  e.Id, e.EventId, e.RegistrId, e.DataId, e.ZayavId, e.WZayavId, e.Sourse, e.Family, e.IO, " +
                        "e.TypeId, e.IdU, e.IdM, e.LiftId, e.Akt, e.Address from Events e " +
                      //  "join HistEv h on h.NumEvent=e.Id " +
                        "join WorkerZayavky wz on wz.Id=e.WZayavId " +
                        "where Cansel=N'false' and RegistrId=N'Эксплуатация лифтов' and wz.[Readed]=0";
                if (_type == 0) // Активные события
                {
                    s += "and e.Family=@fam ";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("fam", fam);
                   
                }
                else if (_type == 1) // не закрытые все
                {
                    cmd = new SqlCommand(s, conn);
                  
                }
                else return data;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                  
                    data.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        EventId = dr["EventId"].ToString(),
                        RegistrId = dr["RegistrId"].ToString(),
                        DataId = dr["DataId"].ToString(),
                        ZayavId = dr["ZayavId"].ToString(),                       
                        WZayavId = dr["WZayavId"].ToString(),                       
                        Sourse = dr["Sourse"].ToString(),
                        Family = dr["Family"].ToString(),
                        IO = dr["IO"].ToString(),
                        TypeId = dr["TypeId"].ToString(),
                        IdU = dr["IdU"].ToString(),
                        IdM = dr["IdM"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Status = " активные ",
                        Address = dr["Address"].ToString()
                        
                    });
                }
                dr.Close();
            }
            return data;
        }
        List<Data> GetDataNa()
        {
            List<Data> datana = new List<Data>();
            string url = "~/";
            List<Lift> data1 = new List<Lift>();

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {

                conn.Open();
                SqlCommand cmd = cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                    "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataReader dt = cmd.ExecuteReader();
                if (dt.Read())
                    fam = dt[0].ToString(); io = dt[1].ToString();
                dt.Close();
                cmd = new SqlCommand();
                string s = "select e.Id, e.EventId, e.RegistrId, e.DataId, e.ZayavId, e.WZayavId, e.Sourse, e.Family, e.IO, " +
                    "e.TypeId, e.IdU, e.IdM, e.LiftId, e.Akt, e.Address from Events e " +
                   // "join HistEv h on h.NumEvent=e.Id " +
                   "join WorkerZayavky wz on wz.Id=e.WZayavId " +
                    "where Cansel=N'false' and RegistrId=N'Эксплуатация лифтов' and wz.[Readed]=1";
                if (_type == 0) // Активные события
                {
                    s += "and Family=@fam and ZaprosMng=N'true'";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("fam", fam);

                }
                else if (_type == 1) // не закрытые все
                {
                    s += "and ZaprosMng=N'true'";
                    cmd = new SqlCommand(s, conn);

                }
                else return datana;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datana.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        EventId = dr["EventId"].ToString(),
                        RegistrId = dr["RegistrId"].ToString(),
                        DataId = dr["DataId"].ToString(),
                        ZayavId = dr["ZayavId"].ToString(),
                        WZayavId = dr["WZayavId"].ToString(),
                        Sourse = dr["Sourse"].ToString(),
                        Family = dr["Family"].ToString(),
                        IO = dr["IO"].ToString(),
                        TypeId = dr["TypeId"].ToString(),
                        IdU = dr["IdU"].ToString(),
                        IdM = dr["IdM"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        Status ="в ожидании",
                        Address = dr["Address"].ToString()

                    });
                }
                dr.Close();
            }
            return datana;
        } 
        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Worker", "Cadry", "Electronick" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Electronick" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Electronick";
            roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            Response.Redirect("~/About.aspx");
            return "";
        }

        
        protected void btnClick_Click(object sender, EventArgs e)
        {

            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            PdfReader template = new PdfReader(@"C:\temp\aktr.pdf");  // файл шаблона
            PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
            AcroFields fields = stamper.AcroFields;
            fields.AddSubstitutionFont(baseFont);
            fields.SetField("dd", "10");
            fields.SetField("mm", "октября");
            fields.SetField("yy", "16");
            fields.SetField("Zakazshik", "Гл. инженер ООО Валеско Иванов И.И.");
            fields.SetField("Worker", "элекромеханик Кулахметов Х.И.");
            fields.SetField("address", "г. Апрелевка, ул.Ясная д.7");
            fields.SetField("sourse", "заявка № 10001.");
            fields.SetField("lift", "1/1/.01");
            fields.SetField("text", "замена ВЧ трансорматора ");
            fields.SetField("date", "10.10.2016 12:35:55");
            fields.SetField("name", "трансформатор ВЧ12000/1");
            fields.SetField("numID", "ID -123456");
            fields.SetField("prim", "неисправная деталь передана на склад");
            fields.SetField("FamZak", "Иванов И.И.");
            fields.SetField("Worker1", "электромеханик Пузин В.И.");
            fields.SetField("FamWorker", "Кулахметов Х.И.");
            fields.SetField("nunId", "123456");
            fields.SetField("kol", "1 шт.");
            stamper.FormFlattening = false;
            stamper.Close();
         //В разработке 

          //  using (FileStream fs = new FileStream(@"C:\temp\akt.pdf", FileMode.Open))
          //  {
          //  }
           
          }
       
        }
    }

