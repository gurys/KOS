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
    public partial class Arc : System.Web.UI.Page
    {
        int _type = 0;
        string _role;
        
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string From { get; set; }
            public string Date1 { get; set; }
            public string Time1 { get; set; }
            public string LiftId { get; set; }
            public string Category { get; set; }
            public string Prinyal { get; set; }
            public string StartPrinyal { get; set; } //дата/время приема заявки
            public string Vypolnil { get; set; }
            public string Date2 { get; set; }
            public string Time2 { get; set; }
            public string Prostoy { get; set; }
            public string Text { get; set; }
            public string Couse { get; set; }
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
                    What.Text = "Архив с " + Beg.SelectedDate.Date.ToShortDateString() +
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
            string url = "~/ZayavkaEdit.aspx?zId=";
            if (_role == "ODS")
                url = "~/ZayavkaView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string s = "select z.Id, z.LiftId, ui.Family as FromFamily, ui.[IO] as FromIO, " +
                    "z.Category, ui2.Family, ui2.[IO], z.Start, ui3.Family as WorkerFamily, " +
                    "ui3.[IO] as WorkerIO, z.Finish, z.[Text], z.Couse, z.PrinyalDate from Zayavky z " +
                    "join Users u on z.UserId=u.UserId " +
                    "join UserInfo ui on ui.UserId=z.UserId " +
                    "left join UserInfo ui2 on ui2.UserId=z.Prinyal " +
                    "left join UserInfo ui3 on ui3.UserId=z.Worker ";
                if (_type == 0) // архив
                {
                    s += "where z.[Start] between @beg and @end";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("beg", Beg.SelectedDate);
                    cmd.Parameters.AddWithValue("end", End.SelectedDate);
                          
                            if (_role == "ODS")
                            {
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //pdf
                            //    adapter.Fill(MyDataSet);//pdf
                            //    Out.DataSource = MyDataSet.Tables[0]; //pdf
                            }
                }
                else if (_type == 1) // не закрытые все
                {
                    s += "where z.Finish is null";
                    if (_role == "ODS") s += " and u.UserName=@user";
                    cmd = new SqlCommand(s, conn);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
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
                    string p = string.Empty;
                    if (!(dr["Family"] is DBNull))
                        p = dr["Family"].ToString() + " " + dr["IO"].ToString();
                    string d = string.Empty;
                    string w = string.Empty;
                    if (!(dr["WorkerFamily"] is DBNull))
                        w = dr["WorkerFamily"].ToString() + " " + dr["WorkerIO"].ToString();
                    string d2 = string.Empty, t2 = string.Empty;
                    TimeSpan pr = DateTime.Now - ((DateTime)dr["Start"]);
                    if (!(dr["Finish"] is DBNull))
                    {
                      //  d = ((DateTime)dr["Finish"]).Date.ToString();
                        d2 = ((DateTime)dr["Finish"]).ToString();
                        t2 = ((DateTime)dr["Finish"]).ToShortTimeString();//TimeOfDay.ToString();
                        pr = ((DateTime)dr["Finish"]) - ((DateTime)dr["Start"]);
                    }
                    if (!(dr["PrinyalDate"] is DBNull))
                    {
                        d = ((DateTime)dr["PrinyalDate"]).ToString();
                    }
                    string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                        pr.Minutes.ToString();
                    data.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role=="ODS" ? "#" : url + dr["Id"].ToString()),
                        From = dr["FromFamily"].ToString() + " " + dr["FromIO"].ToString(),
                        Date1 = ((DateTime)dr["Start"]).ToString(),//Date.ToShortDateString(),
                        Time1 = ((DateTime)dr["Start"]).ToShortTimeString(),
                        Category = dr["Category"].ToString(),
                        LiftId = " " + dr["LiftId"].ToString(),
                        Prinyal = p,
                        StartPrinyal = d,
                        Vypolnil = w,
                        Date2 = d2,
                        Time2 = t2,
                        Prostoy = prostoy,
                        Text = dr["Text"].ToString(),
                        Couse = dr["Couse"].ToString()
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

            List<string> roles = new List<string>() { "ODS", "Cadry", "ODS_tsg" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
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

            string[] cols = { "       №", "ОТПРАВИЛ", "ДАТА", "ВРЕМЯ", "ЛИФТ", "СОБЫТИЕ", "ОПИСАНИЕ", "ПРИНЯЛ", "ДАТА/ВРЕМЯ", "ВЫПОЛНИЛ", 
                                "КОММЕНТАРИЙ", "ДАТА", "ВРЕМЯ", "ПРОСТОЙ" };
            for (int i = 0; i < cols.Length; i++)
                xlsSheet.Cells[1, i + 1] = cols[i];
            for (int i = 0; i < data.Count; i++)
            {
                xlsSheet.Cells[i + 2, 1] = data[i].Id;
                xlsSheet.Cells[i + 2, 2] = data[i].From;
                xlsSheet.Cells[i + 2, 3] = data[i].Date1;
                xlsSheet.Cells[i + 2, 4] = data[i].Time1;
                xlsSheet.Cells[i + 2, 5] = data[i].LiftId;
                xlsSheet.Cells[i + 2, 6] = data[i].Category;
                xlsSheet.Cells[i + 2, 7] = data[i].Text;
                xlsSheet.Cells[i + 2, 8] = data[i].Prinyal;
                xlsSheet.Cells[i + 2, 9] = data[i].StartPrinyal; //время принятия заявки
                xlsSheet.Cells[i + 2, 10] = data[i].Vypolnil;
                xlsSheet.Cells[i + 2, 11] = data[i].Couse;
                xlsSheet.Cells[i + 2, 12] = data[i].Date2;
                xlsSheet.Cells[i + 2, 13] = data[i].Time2;
                xlsSheet.Cells[i + 2, 14] = data[i].Prostoy;
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