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
    public partial class ReportsTSG : System.Web.UI.Page
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
            public string Prim { get; set; }
            public string Timing { get; set; }  
        }
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
                    Period.Visible = true;
                    phReport.Visible = true;
                }

                UpdateLabel();

                List<Data> data = GetData();
                Out.DataSource = data;
                Out.DataBind();



                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                //    адреса ОДС
                    if (_role == "ODS")
                    {
                        cmd = new SqlCommand("select tt.Ttx, tt.Id from Ttx tt " +
                            "join LiftsTtx lt on lt.TtxId=tt.Id " +
                            "join ODSLifts o on o.LiftId=lt.LiftId " +
                            "join Users u on u.UserId=o.UserId " +
                            "where tt.TtxTitleId=1 and u.UserName=@userName " +
                            "group by tt.Ttx, tt.Id", conn);
                        if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("userName", "ODS13");
                        else if (User.Identity.Name == "Корона_1")
                            cmd.Parameters.AddWithValue("userName", "ODS14");
                        else
                            cmd.Parameters.AddWithValue("userName", User.Identity.Name);  
                    }
             //      Все адреса!
                    if (_role == "Manager" || _role == "Cadry")
                    {
                        cmd = new SqlCommand("select tt.Ttx, tt.Id from Ttx tt " +
                            "where tt.TtxTitleId=1 " +
                            "group by tt.Ttx, tt.Id", conn);
                    }
            
                    List<string> adr = new List<string>() { "все" };
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                   while (dr.Read())
               //         addresses.Add(new ListData() { Title = dr[0].ToString(), Id = Int32.Parse(dr[1].ToString()) });
                    adr.Add(dr[0].ToString());
                    dr.Close();
                    if (!IsPostBack)
                    {
                        DropDownList3.DataSource = adr;
                        DropDownList3.DataBind();
                        DropDownList3.SelectedIndex = 0;                        
                    }

                       
                    }
                    List<string> uslugy = new List<string>() { "все", "Эксплуатация зданий", "Эксплуатация лифтов", "Электроснабжение", "Отопление", "Водоснабжение", "Водоотведение", "Охрана", "Клининг" };
                    if (!IsPostBack)
                    {
                        DropDownList1.DataSource = uslugy;
                        DropDownList1.DataBind();
                        DropDownList1.SelectedIndex = 0;
                        //   NLift.Text = "Внести!";
                        //   Uslugy_TextChanged(this, EventArgs.Empty);
                    }
                    List<string> category = new List<string>() { "все", "застревание", "останов", "заявка", "плановые работы", "внеплановые ремонты" };
                    if (!IsPostBack)
                        DropDownList2.DataSource = category;
                        DropDownList2.DataBind();
                        DropDownList2.SelectedIndex = 0;
               }
            
        }

        void UpdateLabel()
        {
            switch (_type)
            {
                case 0:
                    What.Text = "Закрытые события ТСЖ с " + Beg.SelectedDate.Date.ToShortDateString() +
                        " по " + End.SelectedDate.Date.ToShortDateString() + " , вид услуги: [" + DropDownList1.SelectedValue +
                        "] , вид работ: [" + DropDownList2.SelectedValue + "] , адрес: [" + DropDownList3.SelectedValue +"]";
                    break;
                case 1:
                    
                    What.Text = "Закрытые события с " + Beg.SelectedDate.Date.ToShortDateString() +
                        " по " + End.SelectedDate.Date.ToShortDateString();
                    break;
                case 2:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " запрос КП";
                    break;
                case 3:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " ожидание КП";
                    break;
                case 4:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + "запрос счета";
                    break;
                case 5:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " получен счет";
                    break;
                case 6:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " оплата счета";
                    break;
                case 7:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " доставка запчастей/оборудования/инструмента и расходных материалов";
                    break;
                case 8:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " приход запчастей/оборудования/инструмента и расходных материалов";
                    break;
                case 9:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " ожидание акта выполненных работ";
                    break;
                case 10:
                    What.Text = "Не закрытые на  " + DateTime.Now.Date.ToShortDateString() + " списание зачастей/оборудования";

                    break;
            }
        }

        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
            string url = "~/PrimEditTsg.aspx?zId=";
            if (_role == "ODS_TSG")
                url = "~/ZakrytieTSG.aspx?zId=";
            if (_role == "Manager")
                url = "~/EventView.aspx?zId=";
            if (_role == "Cadry")
                url = "~/EventView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string s = "select e.Id, e.EventId, e.RegistrId, e.DataId, e.ZayavId, e.WZayavId, e.Sourse, e.Family, e.IO, " +
                    "e.TypeId, e.LiftId, e.Who, e.ToApp, e.DateWho, e.DateToApp, e.Comment, e.Prim, e.Address from Events e ";
                if (_type == 0) // архив
                {
                    if (DropDownList1.SelectedValue == "все" && DropDownList2.SelectedValue == "все" && DropDownList3.SelectedValue == "все")
                    s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                    " and e.Family=@user";
                    else if (DropDownList1.SelectedValue != "все" && DropDownList2.SelectedValue == "все" && DropDownList3.SelectedValue == "все")
                        s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                        " and e.Family=@user and e.RegistrId=@vu";
                    else if (DropDownList1.SelectedValue != "все" && DropDownList2.SelectedValue != "все" && DropDownList3.SelectedValue == "все")
                        s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                        " and e.Family=@user and e.RegistrId=@vu and e.TypeId=@vr";
                    else if (DropDownList1.SelectedValue == "все" && DropDownList2.SelectedValue != "все" && DropDownList3.SelectedValue == "все")
                        s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                        " and e.Family=@user and e.TypeId=@vr";
                    else if (DropDownList1.SelectedValue == "все" && DropDownList2.SelectedValue == "все" && DropDownList3.SelectedValue != "все")
                        s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                        " and e.Family=@user and e.Address=@adr";
                    else
                    s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                    " and e.Family=@user and e.RegistrId=@vu and e.TypeId=@vr and e.Address=@adr";   // and e.TypeId=@vr and e.Address=@adr";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("beg", Beg.SelectedDate);
                    cmd.Parameters.AddWithValue("end", End.SelectedDate);
                    cmd.Parameters.AddWithValue("vu", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("vr", DropDownList2.SelectedValue);
                    cmd.Parameters.AddWithValue("adr", DropDownList3.SelectedValue);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                        else if (User.Identity.Name == "Корона_1")
                            cmd.Parameters.AddWithValue("user", "ODS14");
                    else
                        cmd.Parameters.AddWithValue("user", User.Identity.Name);                 
                    
                }
                     else if (_type == 1) //архив всех
                        {
                            DropDownList1.Visible = false;DropDownList2.Visible = false;DropDownList3.Visible = false;
                            Label1.Visible = false; Label2.Visible = false; Label3.Visible = false;
                           s += "where e.[Cansel]=N'true' and e.[DataId] between @beg and @end" +
                           " and e.RegistrId=@vu ";
                           cmd = new SqlCommand(s, conn);
                           cmd.Parameters.AddWithValue("beg", Beg.SelectedDate);
                           cmd.Parameters.AddWithValue("end", End.SelectedDate);
                           cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                           
                       }
                       else if (_type == 2) // не закрытые запрос КП
                      {
                          Period.Visible = false;
                          s += "where e.ZaprosKP=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                      else if (_type == 3) // не закрытые КП
                      {
                          Period.Visible = false;
                          s += "where e.KP=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                      else if (_type == 4) // не закрытые запрос счета
                      {
                          Period.Visible = false;
                          s += "where e.ZapBill=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                      else if (_type == 5) // не закрытые счет
                      {
                          Period.Visible = false;
                          s += "where e.Bill=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                      else if (_type == 6) // не закрытые оплата
                      {
                          Period.Visible = false;
                          s += "where e.Payment=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                      else if (_type == 7) // не закрытые доставка
                      {
                          Period.Visible = false;
                          s += "where e.Dostavka=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                      else if (_type == 8) // не закрытые приход
                      {
                          Period.Visible = false;
                          s += "where e.Prihod=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                          cmd = new SqlCommand(s, conn);
                         cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                      }
                else if (_type == 9) // не закрытые акт вып. работ
                {
                    Period.Visible = false;
                    s += "where e.AktVR=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
                }
                else if (_type == 10) // не закрытые списания
                {
                    Period.Visible = false;
                    s += "where e.Spisanie=N'true' and e.Cansel=N'false' and e.RegistrId=@vu";
                    cmd = new SqlCommand(s, conn);
                    cmd.Parameters.AddWithValue("vu", "Эксплуатация лифтов");
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
                        Url = (url + dr["Id"].ToString()), //_role == "ManagerTSG" ? "#" : 
                        Sourse = dr["Sourse"].ToString(),
                        IO = dr["IO"].ToString(),
                        Date1 = dr["DataId"].ToString(),
                        //      Date1 = ((DateTime)dr["DataId"]).Date.ToShortDateString(),
                        //      Time1 = ((DateTime)dr["DataId"]).ToShortTimeString(),                        
                        RegistrId = dr["RegistrId"].ToString(),
                        LiftId =" " + dr["LiftId"].ToString(),
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
                        Prim = dr["Prim"].ToString(),
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

            List<string> roles = new List<string>() { "ODS", "ODS_tsg", "ManagerTSG" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Cadry";
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
            Document document = new Document(PageSize.A2, 30, 30, 30, 30); //PageSize.A3, 30, 30, 30, 30

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
                                "Исполнил", "Комментарий", "дата/время", "Замечания", "Тайминг" };
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
                xlsSheet.Cells[i + 2, 14] = data[i].Prim;
                xlsSheet.Cells[i + 2, 15] = data[i].Timing;
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