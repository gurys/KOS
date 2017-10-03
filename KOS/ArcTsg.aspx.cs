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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using System.Text;

namespace KOS
{
    public partial class ArcTsg : System.Web.UI.Page 
    {
        int _type = 0;
        string _role;

        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Sourse { get; set; }
            public string IO { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }
            public string RegistrId { get; set; }
            public string LiftId { get; set; }
            public string TypeId { get; set; }
            public string EventId { get; set; }
            public string ToApp { get; set; }
            public string DateToApp { get; set; }
            public string Who { get; set; }
            public string Comment { get; set; }
            public string Date2 { get; set; }
            public string Time2 { get; set; }
            public string Timing { get; set; } 
        }
        DataSet MyDataSet = new DataSet();  //for pdf

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();

            if (!string.IsNullOrEmpty(Request["t"]))
                _type = int.Parse(Request["t"]);

            if (!IsPostBack)
            {
                KOS.App_Code.ClearTemp clear = new App_Code.ClearTemp(Request);
                clear.DeleteOld();

                if (_type == 0)
                {
                    Period.Visible = true;
                    Beg.SelectedDate = DateTime.Now.AddDays(-2);
                    End.SelectedDate = DateTime.Now;
                    phReport.Visible = false;
                }
                else
                {
                    Period.Visible = false;
                    phReport.Visible = true;
                }

                UpdateLabel();

                List<Data> data = GetData();
                Out.DataSource = data;
                Out.DataBind();
            }
        }

        void UpdateLabel()
        {
            switch (_type)
            {
                case 0:
                    What.Text = "Архив событий с " + Beg.SelectedDate.Date.ToShortDateString() +
                        " по " + End.SelectedDate.Date.ToShortDateString();
                    break;
                case 1:
                    What.Text = "Активные события (все) на " + DateTime.Now.Date.ToShortDateString();
                    break;
                case 2:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " застревания";
                    break;
                case 3:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " остановы";
                    break;
                case 4:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " заявки";
                    break;
                case 5:
                    What.Text = "Активные события (кроме заявок) на " + DateTime.Now.Date.ToShortDateString();
                    break;
                case 6:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " заявки от заказчиков";
                    break;
                case 7:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " плановые работы";
                    break;
                case 8:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " внеплановые ремонты";
                    break;
            }
        }

        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
            string url = "~/ZakrytieTSG.aspx?zId=";
            if (_role == "ODS_TSG")
                url = "~/ZakrytieTSG.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string s = "select e.Id, e.EventId, e.RegistrId, e.DataId, e.ZayavId, e.WZayavId, e.Sourse, e.Family, e.IO, " +
                    "e.TypeId, e.LiftId, e.Who, e.ToApp, e.DateWho, e.DateToApp, e.Comment from Events e ";
                if (_type == 0) // архив
                {
                    s += "where e.[Cansel]=N'true' and e.Family=@user and e.[DataId] between @beg and @end ";
                 //   if (_role == "ODS_tsg") s += " and e.Family=@user";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("beg", Beg.SelectedDate);
                    cmd.Parameters.AddWithValue("end", End.SelectedDate);                   
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        //    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //pdf
                        //    adapter.Fill(MyDataSet);//pdf
                        //    Out.DataSource = MyDataSet.Tables[0]; //pdf
                    
                }
                else if (_type == 1) // не закрытые все
                {
                    s += "where e.Cansel=N'false'";
                    if (_role == "ODS_tsg") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS_tsg") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 2) // не закрытые застревания
                {
                    s += "where z.Finish is null and z.Category=N'застревание'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 3) // не закрытые остановы
                {
                    s += "where z.Finish is null and z.Category=N'останов'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 4) // не закрытые заявки
                {
                    s += "where z.Finish is null and z.Category=N'заявка'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 5) // не закрытые кроме заявок
                {
                    s += "where z.Finish is null and z.Category<>N'заявка'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 6) // не закрытые заявки от заказчиков
                {
                    s += "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Finish is null and z.Category=N'заявка'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 7) // не закрытые плановые работы
                {
                    s += "where z.Finish is null and z.Category=N'плановые работы'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else if (_type == 8) // не закрытые внеплановые ремонты
                {
                    s += "where z.Finish is null and z.Category=N'внеплановые ремонты'";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
                }
                else return data;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   
                    string d = string.Empty;                  
                    string d2 = string.Empty, t2 = string.Empty;
                    TimeSpan pr = DateTime.Now - ((DateTime)dr["DataId"]);
                    if (!(dr["DateWho"] is DBNull))
                    {
                        //  d = ((DateTime)dr["Finish"]).Date.ToString();
                        d2 = ((DateTime)dr["DateWho"]).ToString();
                        t2 = ((DateTime)dr["DateWho"]).ToShortTimeString();//TimeOfDay.ToString();
                        pr = ((DateTime)dr["DateWho"]) - ((DateTime)dr["DataId"]);
                    }
                    if (!(dr["DateToApp"] is DBNull))
                    {
                        d = ((DateTime)dr["DateToApp"]).ToString();
                    }
                    string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                        pr.Minutes.ToString();
                    data.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "ODS" ? "#" : url + dr["Id"].ToString()),
                        Sourse = dr["Sourse"].ToString(),
                        IO = dr["IO"].ToString(),
                        Date1 = dr["DataId"].ToString(),
                        //      Date1 = ((DateTime)dr["DataId"]).Date.ToShortDateString(),
                        //      Time1 = ((DateTime)dr["DataId"]).ToShortTimeString(),                        
                        RegistrId = dr["RegistrId"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        TypeId = dr["TypeId"].ToString(),
                        EventId = dr["EventId"].ToString(),
                        //    ZayavId = dr["ZayavId"].ToString(),
                        //    Url1 = (_role == "worker" ? "#" : url1 + dr["ZayavId"].ToString()),
                        //    WZayavId = dr["WZayavId"].ToString(),
                        //    Url2 = (_role == "worker" ? "#" : url2 + dr["WZayavId"].ToString()),

                        ToApp = dr["ToApp"].ToString(),
                        DateToApp = d,
                        Who = dr["Who"].ToString(),
                        Comment = dr["Comment"].ToString(),
                        Date2 = dr["DateWho"].ToString(),
                        //    Date2 = d2,
                        //    Time2 = t2,
                        Timing = prostoy
                    });
                }
                dr.Close();
            }
            return data;
        }

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "ODS", "Cadry", "ODS_tsg", "ManagerTSG" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ManagerTSG";
            Response.Redirect("~/About.aspx");
            return "";
        }

        protected void DoIt_Click(object sender, EventArgs e)
        {
            UpdateLabel();
            List<Data> data = GetData();
            Out.DataSource = data;
            Out.DataBind();
            Period.Visible = false;
            phReport.Visible = true;
        }
        protected void btnClick_Click(object sender, EventArgs e)
        {
            DownloadAsPDF();
        }

        public void DownloadAsPDF()
        {
            try
            {
                string strHtml = string.Empty;
                string pdfFileName = Request.PhysicalApplicationPath + "\\files\\" + "архив.pdf";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                dvHtml.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                strHtml = sr.ReadToEnd();
                sr.Close();

                CreatePDFFromHTMLFile("<div style='font-family:arial unicode ms;'>" + strHtml + "</div>", pdfFileName);
                string name = DateTime.Now.ToString("ddMMyyyy-hhmm") + User.Identity.Name + ".pdf";
                Response.ContentType = "application/x-download";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", name));
                Response.ContentEncoding = Encoding.UTF8;
                Response.WriteFile(pdfFileName);
                Response.HeaderEncoding = Encoding.UTF8;
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void CreatePDFFromHTMLFile(string html, string FileName)
        {
            TextReader reader = new StringReader(html);
            // step 1: creation of a document-object 
            Document document = new Document(PageSize.A3, 30, 30, 30, 30);

            // step 2: 
            // we create a writer that listens to the document 
            // and directs a XML-stream to a file 
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FileName, FileMode.Create));

            // step 3: we create a worker parse the document 
            HTMLWorker worker = new HTMLWorker(document);

            // step 4: we open document and start the worker on the document 
            document.Open();

            // step 4.1: register a unicode font and assign it an allias 
            FontFactory.Register("C:\\Windows\\Fonts\\ARIALUNI.TTF", "arial unicode ms");

            // step 4.2: create a style sheet and set the encoding to Identity-H 
            iTextSharp.text.html.simpleparser.StyleSheet ST = new iTextSharp.text.html.simpleparser.StyleSheet();
            ST.LoadTagStyle("body", "encoding", "Identity-H");

            // step 4.3: assign the style sheet to the html parser 
            worker.Style = ST;

            worker.StartDocument();

            // step 5: parse the html into the document 
            worker.Parse(reader);

            // step 6: close the document and the worker 
            worker.EndDocument();
            worker.Close();
            document.Close();
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            List<Data> data = GetData();

            System.Reflection.Missing missingValue = System.Reflection.Missing.Value;

            //создаем и инициализируем объекты Excel
            Excel.Application App = new Microsoft.Office.Interop.Excel.Application();
            //добавляем в файл Excel книгу. Параметр в данной функции - используемый для создания книги шаблон.
            //если нас устраивает вид по умолчанию, то можно спокойно передавать пустой параметр.
            Excel.Workbooks xlsWBs = App.Workbooks;
            Excel.Workbook xlsWB = xlsWBs.Add(missingValue);
            //и использует из нее
            Excel.Sheets xlsSheets = xlsWB.Worksheets;
            Excel.Worksheet xlsSheet = (Excel.Worksheet)xlsSheets.get_Item(1);

            string[] cols = { "№ события", "Источник", "Диспетчер", "дата/время", "Вид услуги", "Зона", "Событие", "Описание", "Принял", "дата/время", 
                                "Исполнил", "Комментарий", "дата/время", "Тайминг" };
            for (int i = 0; i < cols.Length; i++)
                xlsSheet.Cells[1, i + 1] = cols[i];
            for (int i = 0; i < data.Count; i++)
            {
                xlsSheet.Cells[i + 2, 1] = data[i].Id;
                xlsSheet.Cells[i + 2, 2] = data[i].Sourse;
                xlsSheet.Cells[i + 2, 3] = data[i].IO;
                xlsSheet.Cells[i + 2, 4] = data[i].Date1;
                xlsSheet.Cells[i + 2, 5] = data[i].RegistrId;
                xlsSheet.Cells[i + 2, 6] = data[i].LiftId;
                xlsSheet.Cells[i + 2, 7] = data[i].TypeId;
                xlsSheet.Cells[i + 2, 8] = data[i].EventId;
                xlsSheet.Cells[i + 2, 9] = data[i].ToApp;
                xlsSheet.Cells[i + 2, 10] = data[i].DateToApp;
                xlsSheet.Cells[i + 2, 11] = data[i].Who;
                xlsSheet.Cells[i + 2, 12] = data[i].Comment;
                xlsSheet.Cells[i + 2, 13] = data[i].Date2;
                xlsSheet.Cells[i + 2, 14] = data[i].Timing;
            }

            string name = DateTime.Now.ToString("ddMMyyyy-hhmm") + User.Identity.Name + ".xls";
            xlsWB.SaveAs(Request.PhysicalApplicationPath + KOS.App_Code.ClearTemp._folder + "\\" + name,
             Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
             missingValue,
             missingValue,
             missingValue,
             missingValue,
             Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
             missingValue,
             missingValue,
             missingValue,
             missingValue,
             missingValue);
            //закрываем книгу                                                                        
            xlsWB.Close(false, missingValue, missingValue);
            xlsWB = null;
            xlsWBs = null;
            xlsSheet = null;
            xlsSheets = null;
            //закрываем приложение
            App.Quit();
            //уменьшаем счетчики ссылок на COM объекты, что, по идее должно их освободить.
            // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
            // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheets);
            // System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWB);
            //  System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWBs);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App);
            Download.NavigateUrl = "~/" + KOS.App_Code.ClearTemp._folder + "\\" + name;
            Download.Text = "Скачать документ";
            Download.Visible = true;
        }
    }
}