using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using KOS.App_Code;
using KOS.Controls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using System.Text;

namespace KOS
{
    public partial class Lifts : System.Web.UI.Page
    {
        class Lift
        {
            public string Title { get; set; }
        }

        string _role;
        int _year = -1;
        int _month = -1;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["y"]) && !string.IsNullOrEmpty(Request["m"]))
            {
                _year = Int32.Parse(Request["y"]);
                _month = Int32.Parse(Request["m"]);
            }
            else
            {
                _year = DateTime.Now.Year;
                _month = DateTime.Now.Month;
            }
            _role = CheckAccount();

            if (!IsPostBack)
            {
                if (_role == "ODS")
                {
                    Qst.Visible = false;
                    Out.Visible = true;
                }
                else
                {
                    App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    List<string> ls = db.GetIdU();
                    IdU.DataSource = ls;
                    IdU.DataBind();
                    if (ls.Count > 0)
                        IdU.SelectedIndex = 0;
                    IdU_SelectedIndexChanged(sender, e);
                    PeriodBeg.SelectedDate = DateTime.Now.AddMonths(-1);
                    PeriodEnd.SelectedDate = DateTime.Now;
                    Type.DataSource = LiftsReport._types;
                    Type.DataBind();
                    Type.SelectedIndex = 0;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (_role == "ODS" || _role == "Cadry")
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> lifts = db.GetODSLiftList(User.Identity.Name);
                LiftsRep.Set(lifts, new DateTime(_year, _month, 1),
                    new DateTime(_year, _month, DateTime.DaysInMonth(_year, _month)), "Всё", _role); //"Всё"
                if (_month > 1)
                {
                    PrevMonth.Text = KOS.App_Code.Base.months[_month - 2] + " " + _year.ToString();
                    PrevMonth.NavigateUrl = "~/Lifts.aspx?y=" + _year.ToString() + "&m=" + (_month - 1).ToString();
                }
                else
                {
                    PrevMonth.Text = KOS.App_Code.Base.months[11] + " " + (_year - 1).ToString();
                    PrevMonth.NavigateUrl = "~/Lifts.aspx?y=" + (_year - 1).ToString() + "&m=12";
                }
                if (_month < 12)
                {
                    NextMonth.Text = KOS.App_Code.Base.months[_month] + " " + _year.ToString();
                    NextMonth.NavigateUrl = "~/Lifts.aspx?y=" + _year.ToString() + "&m=" + (_month + 1).ToString();
                }
                else
                {
                    NextMonth.Text = KOS.App_Code.Base.months[0] + " " + (_year + 1).ToString();
                    NextMonth.NavigateUrl = "~/Lifts.aspx?y=" + (_year + 1).ToString() + "&m=1";
                }
                phGo.Visible = true;
            }
            else
            {
                List<string> lifts = GetSelectedTitles(IdL);
                LiftsRep.Set(lifts, PeriodBeg.SelectedDate, PeriodEnd.SelectedDate.AddDays(1), Type.SelectedValue, _role);
            }
        }

        protected void Report_Click(object sender, EventArgs e)
        {
            Qst.Visible = false;
            Out.Visible = true;
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

        List<string> GetSelectedTitles(ListView lv)
        {
            List<string> sel = new List<string>();
            foreach (ListViewItem item in lv.Items)
            {
                CheckBox cb = (CheckBox)item.FindControl("Select");
                if (cb.Checked)
                {
                    Label lb = (Label)item.FindControl("Title");
                    sel.Add(lb.Text);
                }
            }
            return sel;
        }
    /*    protected void btnClick_Click(object sender, EventArgs e)
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
        } */
        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Planning.aspx");

            List<string> roles = new List<string>() { "Administrator" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Cadry";
            roles = new List<string>() { "ODS" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            Response.Redirect("~/About.aspx");
            return string.Empty;
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
        }

        protected void IdM_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> ls = db.GetLiftId(IdU.SelectedValue, IdM.SelectedValue);
            List<Lift> data = new List<Lift>();
            foreach (string s in ls)
                data.Add(new Lift()
                {
                    Title = s
                });
            IdL.DataSource = data;
            IdL.DataBind();
        }
    }
}