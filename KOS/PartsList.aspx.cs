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
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using KOS.App_Code;

namespace KOS
{
    public partial class PartsList : System.Web.UI.Page
    {
        string _role = string.Empty;
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Url1 { get; set; }
            public string Obz { get; set; }
            public string NumEvent{ get; set; }
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
            CheckAccount();
            phReport.Visible = false;
            What.Text = "Запчасти в событиях с " + Beg.SelectedDate.Date.ToShortDateString() +
                       " по " + End.SelectedDate.Date.ToShortDateString();
        }
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/adminUM.aspx");
            List<string> roles = new List<string>() { "Manager", "Administrator", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
        protected void PartList_Click(object sender, EventArgs e)
        {
            List<Data> datapart = GetData1();
            ListView1.DataSource = datapart;
            ListView1.DataBind();
            Period.Visible = false;
            phReport.Visible = true;
          
        }
        List<Data> GetData1()
        {
            List<Data> datapart = new List<Data>();
            string url = "~/PartView.aspx?zId=";
            string url1 = "~/EventView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.Id, p.NumEvent, p.namefoto, p.Name, p.NumID, p.Kol, p.Obz from PartsList p " +
                "join Events e on e.Id=p.NumEvent where p.NumEvent=e.Id and (e.DataId between @d1 and @d2)", conn);
              //  cmd.Parameters.AddWithValue("id", _wz);
                cmd.Parameters.AddWithValue("d1", Beg.SelectedDate);
                cmd.Parameters.AddWithValue("d2", End.SelectedDate);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datapart.Add(new Data()
                    {
                        Id = (dr["Id"].ToString()),
                        Url = (_role == "Manager" ? "#" : url + dr["Id"].ToString()),
                        Url1 = (_role == "Manager" ? "#" : url1 + dr["NumEvent"].ToString()),
                        NumEvent = dr["NumEvent"].ToString(),
                        Name = dr["Name"].ToString(),
                        Obz = dr["Obz"].ToString(),
                      //  namefoto = dr["namefoto"].ToString(),
                        NumID = dr["NumID"].ToString(),
                        Kol = dr["Kol"].ToString()

                    });
                }
                dr.Close();
            }
            return datapart;
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
            Document document = new Document(PageSize.A3, 30, 30, 30, 30); //PageSize.A3, 30, 30, 30, 30

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
            List<Data> data = GetData1();

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

            string[] cols = { "№ БД", "№ события", "наименование", "обозначение", "ID номер", "Количество" };
            for (int i = 0; i < cols.Length; i++)
                xlsSheet.Cells[1, i + 1] = cols[i];
            for (int i = 0; i < data.Count; i++)
            {
                xlsSheet.Cells[i + 2, 1] = data[i].Id;
                xlsSheet.Cells[i + 2, 2] = data[i].NumEvent;
                xlsSheet.Cells[i + 2, 3] = data[i].Name;
                xlsSheet.Cells[i + 2, 4] = data[i].Obz;
                xlsSheet.Cells[i + 2, 5] = data[i].NumID;
                xlsSheet.Cells[i + 2, 6] = data[i].Kol;
               
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