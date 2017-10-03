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
    public partial class adminUM : System.Web.UI.Page
    {
        class Data
        {
            public string LiftId { get; set; }
            public string IdL { get; set; }
            public string TpId { get; set; }            
            public string Date { get; set; }
            public string DateEnd { get; set; }
            public string Vyp { get; set; }
            public string Prn { get; set; }
        }
        List<Base.PersonData> _workers = new List<Base.PersonData>();
        DateTime dateOn, dateEn;
        DateTime date1, date2;
        string userName, userNameA, userNameY, userNameG, MonthY, Sh, ShP;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
           App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                _workers = db.GetWorkers();
                if (!IsPostBack)
                {
                    List<string> year = new List<string>() { "2016", "2017", "2018", "2019", "2020" };
                //    for (int i = 2015; i <= DateTime.Now.Year; i++)
                //        year.Add(i.ToString());
                    Year.DataSource = year;
                    Year.DataBind();
                    Year.SelectedValue = DateTime.Now.Year.ToString();
                    Month.DataSource = KOS.App_Code.Base.months;
                    Month.DataBind();
                    Month.SelectedIndex = DateTime.Now.Month - 1;
                    Month_SelectedIndexChanged(sender, e);
                    BindDay();

                    Day.SelectedIndex = DateTime.Now.Day - 1;
                    List<string> ls = db.GetIdU();
                    IdU.DataSource = ls;
                    IdU.DataBind();
                    if (ls.Count > 0)
                        IdU.SelectedIndex = 0;
                    IdU_SelectedIndexChanged(sender, e);
                    List<string> prorab = new List<string>() { "Володин А.В.", "Пузин А.В." };
                    ddProrab.DataSource = prorab;
                    ddProrab.DataBind();
                    ddProrab.SelectedIndex = 0;
                    List<string> gling = new List<string>() {"Биктимиров М.Ф.", "Володин А.В."};
                    ddGlIng.DataSource = gling;
                    ddGlIng.DataBind();
                    ddGlIng.SelectedIndex = 0;
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
        protected void Button2_Click(object sender, EventArgs e) 
        {
            phOut.Visible = false;
            phOut1.Visible = true;
            if (IsPostBack)
            {
                
              //  Month_SelectedIndexChanged(sender, e);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                if (To.Text == "Беляков Д.В.") userName = "Belyakov";
                if (To.Text == "Бурханов А.А.") userName = "Burhanov";
                if (To.Text == "Кулахметов Х.И. ") userName = "Kulahmetov";
                if (To.Text == "Михеенко В.А.") userName = "Miheenko";
                if (To.Text == "Вергейчик А.") userName = "Vergeychik";
                if (To.Text == "Агинский В.Д.") userName = "Aginsky";
                if (To.Text == "Володин А.В") userName = "Volodin";
                if (To.Text == "Пузин А.В.") userName = "Puzin";
                if (To.Text == "Волченко Р.") userName = "Vol4enko";
                if (To.Text == "Саргамонов А.В.") userName = "Sargamonov";
                if (To.Text == "Андрейчук В.В.") userName = "Andreychuk";
                if (To.Text == "Барабанов Ю.М.") userName = "Barabanov";
                string IdUM = IdU.Text + "/" + IdM.Text;
                if (Year.Text == "2016") { date1 = Convert.ToDateTime("01.01.2016"); date2 = Convert.ToDateTime("01.12.2016"); }
                if (Year.Text == "2017") { date1 = Convert.ToDateTime("01.01.2017"); date2 = Convert.ToDateTime("01.12.2017"); }
                if (Year.Text == "2018") { date1 = Convert.ToDateTime("01.01.2018"); date2 = Convert.ToDateTime("01.12.2018"); }
                if (Year.Text == "2019") { date1 = Convert.ToDateTime("01.01.2019"); date2 = Convert.ToDateTime("01.12.2019"); }
                if (Year.Text == "2020") { date1 = Convert.ToDateTime("01.01.2020"); date2 = Convert.ToDateTime("01.12.2020"); }
                DataTable data = db.GetTpPlan(userName, IdUM, date1, date2); //для годового плана
                List<Data> dt1 = new List<Data>();
                foreach (DataRow dr in data.Rows)
                {
                    dt1.Add(new Data()
                    {
                        Date = Convert.ToString(dr["Date"]).ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        TpId = dr["TpId"].ToString()
                     
                    });
                }
               
                Out1.DataSource = dt1;
                Out1.DataBind();

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            phOut.Visible = true;
            phOut1.Visible = false;
            if (IsPostBack)
            {
                phOut.Visible = true;
                Month_SelectedIndexChanged(sender, e);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                if (To.Text == "Беляков Д.В.") userName = "Belyakov";
                if (To.Text == "Бурханов А.А.") userName = "Burhanov";
                if (To.Text == "Кулахметов Х.И. ") userName = "Kulahmetov";
                if (To.Text == "Михеенко В.А.") userName = "Miheenko";
                if (To.Text == "Вергейчик А.") userName = "Vergeychik";
                if (To.Text == "Агинский В.Д.") userName = "Aginsky";
                if (To.Text == "Володин А.В") userName = "Volodin";
                if (To.Text == "Пузин А.В.") userName = "Puzin";
                if (To.Text == "Волченко Р.") userName = "Vol4enko";
                if (To.Text == "Саргамонов А.В.") userName = "Sargamonov";
                if (To.Text == "Андрейчук В.В.") userName = "Andreychuk";
                if (To.Text == "Барабанов Ю.М.") userName = "Barabanov";
                string IdUM = IdU.Text + "/" + IdM.Text;
                DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
             //   DataTable data = db.GetTpPlan(userName, IdUM, dateOn, dateEn); //для годового плана
                List<Data> dt = new List<Data>();
                foreach (DataRow dr in data.Rows)
                {
                    dt.Add(new Data()
                    {
                        LiftId = dr["LiftId"].ToString(),
                        TpId = dr["TpId"].ToString(),
                        Date = Convert.ToString(dr["Date"]).ToString(),
                        DateEnd = Convert.ToString(dr["DateEnd"]).ToString(),
                        Vyp = (bool.Parse(dr["Done"].ToString()) ? "выполнено" : ""),
                        Prn = ((dr["Prn"].ToString()) == "True" ? "принято" : "")
                    });
                }
             //   List<Data> dt = GetData();
                Out.DataSource = dt;
                Out.DataBind(); 
              
            }
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
        protected void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }

        protected void Month_SelectedIndexChanged1(object sender, EventArgs e) 
        {
            BindDay();
        }

        void BindDay()
        {
            List<string> day = new List<string>();
            for (int i = 1; i <= DateTime.DaysInMonth(int.Parse(Year.SelectedValue), Month.SelectedIndex + 1); i++)
                day.Add(i.ToString());
            Day.DataSource = day;
            Day.DataBind();
            Day.SelectedIndex = 0;
        }
        protected void Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
            if (Year.Text == "2016")
            {
                if (Month.Text == "январь") { dateOn = Convert.ToDateTime("01.01.2016 08:00"); dateEn = Convert.ToDateTime("31.01.2016 21:00"); }
                if (Month.Text == "февраль") { dateOn = Convert.ToDateTime("01.02.2016 08:00"); dateEn = Convert.ToDateTime("28.02.2016 21:00"); }
                if (Month.Text == "март") { dateOn = Convert.ToDateTime("01.03.2016 08:00"); dateEn = Convert.ToDateTime("31.03.2016 21:00"); }
                if (Month.Text == "апрель") { dateOn = Convert.ToDateTime("01.04.2016 08:00"); dateEn = Convert.ToDateTime("30.04.2016 21:00"); }
                if (Month.Text == "май") { dateOn = Convert.ToDateTime("01.05.2016 08:00"); dateEn = Convert.ToDateTime("31.05.2016 21:00"); }
                if (Month.Text == "июнь") { dateOn = Convert.ToDateTime("01.06.2016 08:00"); dateEn = Convert.ToDateTime("30.06.2016 21:00"); }
                if (Month.Text == "июль") { dateOn = Convert.ToDateTime("01.07.2016 08:00"); dateEn = Convert.ToDateTime("31.07.2016 21:00"); }
                if (Month.Text == "август") { dateOn = Convert.ToDateTime("01.08.2016 08:00"); dateEn = Convert.ToDateTime("31.08.2016 21:00"); }
                if (Month.Text == "сентябрь") { dateOn = Convert.ToDateTime("01.09.2016 08:00"); dateEn = Convert.ToDateTime("30.09.2016 21:00"); }
                if (Month.Text == "октябрь") { dateOn = Convert.ToDateTime("01.10.2016 08:00"); dateEn = Convert.ToDateTime("31.10.2016 21:00"); }
                if (Month.Text == "ноябрь") { dateOn = Convert.ToDateTime("01.11.2016 08:00"); dateEn = Convert.ToDateTime("30.11.2016 21:00"); }
                if (Month.Text == "декабрь") { dateOn = Convert.ToDateTime("01.12.2016 08:00"); dateEn = Convert.ToDateTime("31.12.2016 21:00"); }
            }
            if (Year.Text == "2017")
            {
                if (Month.Text == "январь") { dateOn = Convert.ToDateTime("01.01.2017 08:00"); dateEn = Convert.ToDateTime("31.01.2017 21:00"); }
                if (Month.Text == "февраль") { dateOn = Convert.ToDateTime("01.02.2017 08:00"); dateEn = Convert.ToDateTime("28.02.2017 21:00"); }
                if (Month.Text == "март") { dateOn = Convert.ToDateTime("01.03.2017 08:00"); dateEn = Convert.ToDateTime("31.03.2017 21:00"); }
                if (Month.Text == "апрель") { dateOn = Convert.ToDateTime("01.04.2017 08:00"); dateEn = Convert.ToDateTime("30.04.2017 21:00"); }
                if (Month.Text == "май") { dateOn = Convert.ToDateTime("01.05.2017 08:00"); dateEn = Convert.ToDateTime("31.05.2017 21:00"); }
                if (Month.Text == "июнь") { dateOn = Convert.ToDateTime("01.06.2017 08:00"); dateEn = Convert.ToDateTime("30.06.2017 21:00"); }
                if (Month.Text == "июль") { dateOn = Convert.ToDateTime("01.07.2017 08:00"); dateEn = Convert.ToDateTime("31.07.2017 21:00"); }
                if (Month.Text == "август") { dateOn = Convert.ToDateTime("01.08.2017 08:00"); dateEn = Convert.ToDateTime("31.08.2017 21:00"); }
                if (Month.Text == "сентябрь") { dateOn = Convert.ToDateTime("01.09.2017 08:00"); dateEn = Convert.ToDateTime("30.09.2017 21:00"); }
                if (Month.Text == "октябрь") { dateOn = Convert.ToDateTime("01.10.2017 08:00"); dateEn = Convert.ToDateTime("31.10.2017 21:00"); }
                if (Month.Text == "ноябрь") { dateOn = Convert.ToDateTime("01.11.2017 08:00"); dateEn = Convert.ToDateTime("30.11.2017 21:00"); }
                if (Month.Text == "декабрь") { dateOn = Convert.ToDateTime("01.12.2017 08:00"); dateEn = Convert.ToDateTime("31.12.2017 21:00"); }
            }
            if (Year.Text == "2018")
            {
                if (Month.Text == "январь") { dateOn = Convert.ToDateTime("01.01.2018 08:00"); dateEn = Convert.ToDateTime("31.01.2018 21:00"); }
                if (Month.Text == "февраль") { dateOn = Convert.ToDateTime("01.02.2018 08:00"); dateEn = Convert.ToDateTime("28.02.2018 21:00"); }
                if (Month.Text == "март") { dateOn = Convert.ToDateTime("01.03.2018 08:00"); dateEn = Convert.ToDateTime("31.03.2018 21:00"); }
                if (Month.Text == "апрель") { dateOn = Convert.ToDateTime("01.04.2018 08:00"); dateEn = Convert.ToDateTime("30.04.2018 21:00"); }
                if (Month.Text == "май") { dateOn = Convert.ToDateTime("01.05.2018 08:00"); dateEn = Convert.ToDateTime("31.05.2018 21:00"); }
                if (Month.Text == "июнь") { dateOn = Convert.ToDateTime("01.06.2018 08:00"); dateEn = Convert.ToDateTime("30.06.2018 21:00"); }
                if (Month.Text == "июль") { dateOn = Convert.ToDateTime("01.07.2018 08:00"); dateEn = Convert.ToDateTime("31.07.2018 21:00"); }
                if (Month.Text == "август") { dateOn = Convert.ToDateTime("01.08.2018 08:00"); dateEn = Convert.ToDateTime("31.08.2018 21:00"); }
                if (Month.Text == "сентябрь") { dateOn = Convert.ToDateTime("01.09.2018 08:00"); dateEn = Convert.ToDateTime("30.09.2018 21:00"); }
                if (Month.Text == "октябрь") { dateOn = Convert.ToDateTime("01.10.2018 08:00"); dateEn = Convert.ToDateTime("31.10.2018 21:00"); }
                if (Month.Text == "ноябрь") { dateOn = Convert.ToDateTime("01.11.2018 08:00"); dateEn = Convert.ToDateTime("30.11.2018 21:00"); }
                if (Month.Text == "декабрь") { dateOn = Convert.ToDateTime("01.12.2018 08:00"); dateEn = Convert.ToDateTime("31.12.2018 21:00"); }
            }
            if (Year.Text == "2019")
            {
                if (Month.Text == "январь") { dateOn = Convert.ToDateTime("01.01.2019 08:00"); dateEn = Convert.ToDateTime("31.01.2019 21:00"); }
                if (Month.Text == "февраль") { dateOn = Convert.ToDateTime("01.02.2019 08:00"); dateEn = Convert.ToDateTime("28.02.2019 21:00"); }
                if (Month.Text == "март") { dateOn = Convert.ToDateTime("01.03.2019 08:00"); dateEn = Convert.ToDateTime("31.03.2019 21:00"); }
                if (Month.Text == "апрель") { dateOn = Convert.ToDateTime("01.04.2019 08:00"); dateEn = Convert.ToDateTime("30.04.2019 21:00"); }
                if (Month.Text == "май") { dateOn = Convert.ToDateTime("01.05.2019 08:00"); dateEn = Convert.ToDateTime("31.05.2019 21:00"); }
                if (Month.Text == "июнь") { dateOn = Convert.ToDateTime("01.06.2019 08:00"); dateEn = Convert.ToDateTime("30.06.2019 21:00"); }
                if (Month.Text == "июль") { dateOn = Convert.ToDateTime("01.07.2019 08:00"); dateEn = Convert.ToDateTime("31.07.2019 21:00"); }
                if (Month.Text == "август") { dateOn = Convert.ToDateTime("01.08.2019 08:00"); dateEn = Convert.ToDateTime("31.08.2019 21:00"); }
                if (Month.Text == "сентябрь") { dateOn = Convert.ToDateTime("01.09.2019 08:00"); dateEn = Convert.ToDateTime("30.09.2019 21:00"); }
                if (Month.Text == "октябрь") { dateOn = Convert.ToDateTime("01.10.2019 08:00"); dateEn = Convert.ToDateTime("31.10.2019 21:00"); }
                if (Month.Text == "ноябрь") { dateOn = Convert.ToDateTime("01.11.2019 08:00"); dateEn = Convert.ToDateTime("30.11.2019 21:00"); }
                if (Month.Text == "декабрь") { dateOn = Convert.ToDateTime("01.12.2019 08:00"); dateEn = Convert.ToDateTime("31.12.2019 21:00"); }
            }
            if (Year.Text == "2020")
            {
                if (Month.Text == "январь") { dateOn = Convert.ToDateTime("01.01.2020 08:00"); dateEn = Convert.ToDateTime("31.01.2020 21:00"); }
                if (Month.Text == "февраль") { dateOn = Convert.ToDateTime("01.02.2020 08:00"); dateEn = Convert.ToDateTime("28.02.2020 21:00"); }
                if (Month.Text == "март") { dateOn = Convert.ToDateTime("01.03.2020 08:00"); dateEn = Convert.ToDateTime("31.03.2020 21:00"); }
                if (Month.Text == "апрель") { dateOn = Convert.ToDateTime("01.04.2020 08:00"); dateEn = Convert.ToDateTime("30.04.2020 21:00"); }
                if (Month.Text == "май") { dateOn = Convert.ToDateTime("01.05.2020 08:00"); dateEn = Convert.ToDateTime("31.05.2020 21:00"); }
                if (Month.Text == "июнь") { dateOn = Convert.ToDateTime("01.06.2020 08:00"); dateEn = Convert.ToDateTime("30.06.2020 21:00"); }
                if (Month.Text == "июль") { dateOn = Convert.ToDateTime("01.07.2020 08:00"); dateEn = Convert.ToDateTime("31.07.2020 21:00"); }
                if (Month.Text == "август") { dateOn = Convert.ToDateTime("01.08.2020 08:00"); dateEn = Convert.ToDateTime("31.08.2020 21:00"); }
                if (Month.Text == "сентябрь") { dateOn = Convert.ToDateTime("01.09.2020 08:00"); dateEn = Convert.ToDateTime("30.09.2020 21:00"); }
                if (Month.Text == "октябрь") { dateOn = Convert.ToDateTime("01.10.2020 08:00"); dateEn = Convert.ToDateTime("31.10.2020 21:00"); }
                if (Month.Text == "ноябрь") { dateOn = Convert.ToDateTime("01.11.2020 08:00"); dateEn = Convert.ToDateTime("30.11.2020 21:00"); }
                if (Month.Text == "декабрь") { dateOn = Convert.ToDateTime("01.12.2020 08:00"); dateEn = Convert.ToDateTime("31.12.2020 21:00"); }
            }
            
        }

        protected void MonthSchedule_Click(object sender, EventArgs e)
        {
            //блок формирования месячного графика
                string dat = DateTime.Now.Date.ToLongDateString();
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
                string ho = DateTime.Now.Hour.ToString();
                string mi = DateTime.Now.Minute.ToString();
                string se = DateTime.Now.Second.ToString(); 
                Month_SelectedIndexChanged(sender, e);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                if (To.Text == "Беляков Д.В.") userName = "Belyakov";
                if (To.Text == "Бурханов А.А.") userName = "Burhanov";
                if (To.Text == "Кулахметов Х.И. ") userName = "Kulahmetov";
                if (To.Text == "Михеенко В.А.") userName = "Miheenko";
                if (To.Text == "Вергейчик А.") userName = "Vergeychik";
                if (To.Text == "Агинский В.Д.") userName = "Aginsky";
                if (To.Text == "Володин А.В") userName = "Volodin";
                if (To.Text == "Пузин А.В.") userName = "Puzin";
                if (To.Text == "Волченко Р.") userName = "Vol4enko";
                if (To.Text == "Саргамонов А.В.") userName = "Sargamonov";
                if (To.Text == "Андрейчук В.В.") userName = "Andreychuk";
                if (To.Text == "Барабанов Ю.М.") userName = "Barabanov";
                string IdUM = IdU.Text + "/" + IdM.Text; 
              //  DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select p.TpId, p.[Date], p.DateEnd, p.LiftId, t.Ttx, p.Id as PlanId, p.Done, p.Prn from [Plan] p " +
                    "join Lifts l on l.LiftId=p.LiftId " +
                    "join LiftsTtx lt on lt.LiftId=p.LiftId " +
                  //  "join Users u on u.UserId=p.UserId " +
                    "join Ttx t on t.Id=lt.TtxId and t.TtxTitleId=1 " +
                    "where (p.[Date] between @d1 and @d2) and p.TpId<>N'ОС' and p.TpId<>N'ВР' " + //p.UserId=@userId and
                    "and l.IdU=@U and l.IdM=@M order by p.LiftId, p.[Date]", conn);
                cmd.Parameters.AddWithValue("userId", To.Text);
                cmd.Parameters.AddWithValue("d1", dateOn);
                cmd.Parameters.AddWithValue("d2", dateEn);
                cmd.Parameters.AddWithValue("U", IdU.Text);
                cmd.Parameters.AddWithValue("M", IdM.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                    BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    if (IdU.SelectedValue == "1" && IdM.SelectedValue == "1")
                    {
                        /*  if (IdU.SelectedValue == "1" && IdM.SelectedValue == "1") Sh = "mg11";
                          if (IdU.SelectedValue == "1" && IdM.SelectedValue == "3") Sh = "mg13";
                          if (IdU.SelectedValue == "1" && IdM.SelectedValue == "4") Sh = "mg14";
                          if (IdU.SelectedValue == "2" && IdM.SelectedValue == "1") Sh = "mg21";
                          if (IdU.SelectedValue == "2" && IdM.SelectedValue == "2") Sh = "mg22";
                          if (IdU.SelectedValue == "2" && IdM.SelectedValue == "3") Sh = "mg23";
                          if (IdU.SelectedValue == "2" && IdM.SelectedValue == "4") Sh = "mg24";
                          if (IdU.SelectedValue == "2" && IdM.SelectedValue == "5") Sh = "mg25";
                          if (IdU.SelectedValue == "2" && IdM.SelectedValue == "6") Sh = "mg26";
                          if (IdU.SelectedValue == "4" && IdM.SelectedValue == "1") Sh = "mg41";
                          if (IdU.SelectedValue == "4" && IdM.SelectedValue == "2") Sh = "mg42"; */
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg11.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", data.Rows[8][1].ToString().Substring(10, 5) + "-" + data.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", data.Rows[9][1].ToString().Substring(10, 5) + "-" + data.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", data.Rows[10][1].ToString().Substring(10, 5) + "-" + data.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            
                        }
                        catch { Msg.Text = "Для участка 1/1 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "1" && IdM.SelectedValue == "3")
                    {
                        DataTable da = db.GetPlanUM13(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg13.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", da.Rows[0][0].ToString()); fields.SetField("d_1", da.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", da.Rows[1][0].ToString()); fields.SetField("d_2", da.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", da.Rows[2][0].ToString()); fields.SetField("d_3", da.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", da.Rows[3][0].ToString()); fields.SetField("d_4", da.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", da.Rows[4][0].ToString()); fields.SetField("d_5", da.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", da.Rows[5][0].ToString()); fields.SetField("d_6", da.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", da.Rows[6][0].ToString()); fields.SetField("d_7", da.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", da.Rows[7][0].ToString()); fields.SetField("d_8", da.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", da.Rows[8][0].ToString()); fields.SetField("d_9", da.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", da.Rows[9][0].ToString()); fields.SetField("d_10", da.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", da.Rows[10][0].ToString()); fields.SetField("d_11", da.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", da.Rows[11][0].ToString()); fields.SetField("d_12", da.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", da.Rows[12][0].ToString()); fields.SetField("d_13", da.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", da.Rows[13][0].ToString()); fields.SetField("d_14", da.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", da.Rows[14][0].ToString()); fields.SetField("d_15", da.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", da.Rows[15][0].ToString()); fields.SetField("d_16", da.Rows[15][1].ToString().Remove(10));
                            fields.SetField("tr_17", da.Rows[16][0].ToString()); fields.SetField("d_17", da.Rows[16][1].ToString().Remove(10));
                            fields.SetField("tr_18", da.Rows[17][0].ToString()); fields.SetField("d_18", da.Rows[17][1].ToString().Remove(10));
                            fields.SetField("tr_19", da.Rows[18][0].ToString()); fields.SetField("d_19", da.Rows[18][1].ToString().Remove(10));
                            fields.SetField("tr_20", da.Rows[19][0].ToString()); fields.SetField("d_20", da.Rows[19][1].ToString().Remove(10));
                            fields.SetField("tr_21", da.Rows[20][0].ToString()); fields.SetField("d_21", da.Rows[20][1].ToString().Remove(10));
                            fields.SetField("tr_22", da.Rows[21][0].ToString()); fields.SetField("d_22", da.Rows[21][1].ToString().Remove(10));
                            fields.SetField("tr_23", da.Rows[22][0].ToString()); fields.SetField("d_23", da.Rows[22][1].ToString().Remove(10));
                            fields.SetField("tr_24", da.Rows[23][0].ToString()); fields.SetField("d_24", da.Rows[23][1].ToString().Remove(10));
                            fields.SetField("tr_25", da.Rows[24][0].ToString()); fields.SetField("d_25", da.Rows[24][1].ToString().Remove(10));
                            fields.SetField("tr_26", da.Rows[25][0].ToString()); fields.SetField("d_26", da.Rows[25][1].ToString().Remove(10));
                            fields.SetField("tr_27", da.Rows[26][0].ToString()); fields.SetField("d_27", da.Rows[26][1].ToString().Remove(10));
                            fields.SetField("tr_28", da.Rows[27][0].ToString()); fields.SetField("d_28", da.Rows[27][1].ToString().Remove(10));
                            fields.SetField("tr_29", da.Rows[28][0].ToString()); fields.SetField("d_29", da.Rows[28][1].ToString().Remove(10));
                            fields.SetField("tr_30", da.Rows[29][0].ToString()); fields.SetField("d_30", da.Rows[29][1].ToString().Remove(10));

                            fields.SetField("ti_1", da.Rows[0][1].ToString().Substring(10, 5) + "-" + da.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (da.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", da.Rows[1][1].ToString().Substring(10, 5) + "-" + da.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (da.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", da.Rows[2][1].ToString().Substring(10, 5) + "-" + da.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (da.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", da.Rows[3][1].ToString().Substring(10, 5) + "-" + da.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (da.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", da.Rows[4][1].ToString().Substring(10, 5) + "-" + da.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (da.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", da.Rows[5][1].ToString().Substring(10, 5) + "-" + da.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (da.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", da.Rows[6][1].ToString().Substring(10, 5) + "-" + da.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (da.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", da.Rows[7][1].ToString().Substring(10, 5) + "-" + da.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (da.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", da.Rows[8][1].ToString().Substring(10, 5) + "-" + da.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (da.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", da.Rows[9][1].ToString().Substring(10, 5) + "-" + da.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (da.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", da.Rows[10][1].ToString().Substring(10, 5) + "-" + da.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (da.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_12", da.Rows[11][1].ToString().Substring(10, 5) + "-" + da.Rows[11][2].ToString().Substring(11, 5)); fields.SetField("vp_12", (da.Rows[11][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_13", da.Rows[12][1].ToString().Substring(10, 5) + "-" + da.Rows[12][2].ToString().Substring(11, 5)); fields.SetField("vp_13", (da.Rows[12][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_14", da.Rows[13][1].ToString().Substring(10, 5) + "-" + da.Rows[13][2].ToString().Substring(11, 5)); fields.SetField("vp_14", (da.Rows[13][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_15", da.Rows[14][1].ToString().Substring(10, 5) + "-" + da.Rows[14][2].ToString().Substring(11, 5)); fields.SetField("vp_15", (da.Rows[14][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_16", da.Rows[15][1].ToString().Substring(10, 5) + "-" + da.Rows[15][2].ToString().Substring(11, 5)); fields.SetField("vp_16", (da.Rows[15][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_17", da.Rows[16][1].ToString().Substring(10, 5) + "-" + da.Rows[16][2].ToString().Substring(11, 5)); fields.SetField("vp_17", (da.Rows[16][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_18", da.Rows[17][1].ToString().Substring(10, 5) + "-" + da.Rows[17][2].ToString().Substring(11, 5)); fields.SetField("vp_18", (da.Rows[17][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_19", da.Rows[18][1].ToString().Substring(10, 5) + "-" + da.Rows[18][2].ToString().Substring(11, 5)); fields.SetField("vp_19", (da.Rows[18][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_20", da.Rows[19][1].ToString().Substring(10, 5) + "-" + da.Rows[19][2].ToString().Substring(11, 5)); fields.SetField("vp_20", (da.Rows[19][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_21", da.Rows[20][1].ToString().Substring(10, 5) + "-" + da.Rows[20][2].ToString().Substring(11, 5)); fields.SetField("vp_21", (da.Rows[20][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_22", da.Rows[21][1].ToString().Substring(10, 5) + "-" + da.Rows[21][2].ToString().Substring(11, 5)); fields.SetField("vp_22", (da.Rows[21][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_23", da.Rows[22][1].ToString().Substring(10, 5) + "-" + da.Rows[22][2].ToString().Substring(11, 5)); fields.SetField("vp_23", (da.Rows[22][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_24", da.Rows[23][1].ToString().Substring(10, 5) + "-" + da.Rows[23][2].ToString().Substring(11, 5)); fields.SetField("vp_24", (da.Rows[23][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_25", da.Rows[24][1].ToString().Substring(10, 5) + "-" + da.Rows[24][2].ToString().Substring(11, 5)); fields.SetField("vp_25", (da.Rows[24][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_26", da.Rows[25][1].ToString().Substring(10, 5) + "-" + da.Rows[25][2].ToString().Substring(11, 5)); fields.SetField("vp_26", (da.Rows[25][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_27", da.Rows[26][1].ToString().Substring(10, 5) + "-" + da.Rows[26][2].ToString().Substring(11, 5)); fields.SetField("vp_27", (da.Rows[26][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_28", da.Rows[27][1].ToString().Substring(10, 5) + "-" + da.Rows[27][2].ToString().Substring(11, 5)); fields.SetField("vp_28", (da.Rows[27][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_29", da.Rows[28][1].ToString().Substring(10, 5) + "-" + da.Rows[28][2].ToString().Substring(11, 5)); fields.SetField("vp_29", (da.Rows[28][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_30", da.Rows[29][1].ToString().Substring(10, 5) + "-" + da.Rows[29][2].ToString().Substring(11, 5)); fields.SetField("vp_30", (da.Rows[29][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 1/3 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "1" && IdM.SelectedValue == "4")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg14.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", data.Rows[11][0].ToString()); fields.SetField("d_12", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", data.Rows[12][0].ToString()); fields.SetField("d_13", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", data.Rows[13][0].ToString()); fields.SetField("d_14", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", data.Rows[14][0].ToString()); fields.SetField("d_15", data.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", data.Rows[15][0].ToString()); fields.SetField("d_16", data.Rows[15][1].ToString().Remove(10));
                            fields.SetField("tr_17", data.Rows[16][0].ToString()); fields.SetField("d_17", data.Rows[16][1].ToString().Remove(10));
                            fields.SetField("tr_18", data.Rows[17][0].ToString()); fields.SetField("d_18", data.Rows[17][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", data.Rows[8][1].ToString().Substring(10, 5) + "-" + data.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", data.Rows[9][1].ToString().Substring(10, 5) + "-" + data.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", data.Rows[10][1].ToString().Substring(10, 5) + "-" + data.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_12", data.Rows[11][1].ToString().Substring(10, 5) + "-" + data.Rows[11][2].ToString().Substring(11, 5)); fields.SetField("vp_12", (data.Rows[11][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_13", data.Rows[12][1].ToString().Substring(10, 5) + "-" + data.Rows[12][2].ToString().Substring(11, 5)); fields.SetField("vp_13", (data.Rows[12][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_14", data.Rows[13][1].ToString().Substring(10, 5) + "-" + data.Rows[13][2].ToString().Substring(11, 5)); fields.SetField("vp_14", (data.Rows[13][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_15", data.Rows[14][1].ToString().Substring(10, 5) + "-" + data.Rows[14][2].ToString().Substring(11, 5)); fields.SetField("vp_15", (data.Rows[14][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_16", data.Rows[15][1].ToString().Substring(10, 5) + "-" + data.Rows[15][2].ToString().Substring(11, 5)); fields.SetField("vp_16", (data.Rows[15][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_17", data.Rows[16][1].ToString().Substring(10, 5) + "-" + data.Rows[16][2].ToString().Substring(11, 5)); fields.SetField("vp_17", (data.Rows[16][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_18", data.Rows[17][1].ToString().Substring(10, 5) + "-" + data.Rows[17][2].ToString().Substring(11, 5)); fields.SetField("vp_18", (data.Rows[17][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 1/4 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "1" && IdM.SelectedValue == "5")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg15.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                                
                        }
                        catch { Msg.Text = "Для участка 1/5 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "1")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg21.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", data.Rows[11][0].ToString()); fields.SetField("d_12", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", data.Rows[12][0].ToString()); fields.SetField("d_13", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", data.Rows[13][0].ToString()); fields.SetField("d_14", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", data.Rows[14][0].ToString()); fields.SetField("d_15", data.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", data.Rows[15][0].ToString()); fields.SetField("d_16", data.Rows[15][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", data.Rows[8][1].ToString().Substring(10, 5) + "-" + data.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", data.Rows[9][1].ToString().Substring(10, 5) + "-" + data.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", data.Rows[10][1].ToString().Substring(10, 5) + "-" + data.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_12", data.Rows[11][1].ToString().Substring(10, 5) + "-" + data.Rows[11][2].ToString().Substring(11, 5)); fields.SetField("vp_12", (data.Rows[11][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_13", data.Rows[12][1].ToString().Substring(10, 5) + "-" + data.Rows[12][2].ToString().Substring(11, 5)); fields.SetField("vp_13", (data.Rows[12][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_14", data.Rows[13][1].ToString().Substring(10, 5) + "-" + data.Rows[13][2].ToString().Substring(11, 5)); fields.SetField("vp_14", (data.Rows[13][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_15", data.Rows[14][1].ToString().Substring(10, 5) + "-" + data.Rows[14][2].ToString().Substring(11, 5)); fields.SetField("vp_15", (data.Rows[14][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_16", data.Rows[15][1].ToString().Substring(10, 5) + "-" + data.Rows[15][2].ToString().Substring(11, 5)); fields.SetField("vp_16", (data.Rows[15][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 2/1 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "2" && CheckBox1.Checked == false)
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg22.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", data.Rows[8][1].ToString().Substring(10, 5) + "-" + data.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", data.Rows[9][1].ToString().Substring(10, 5) + "-" + data.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", data.Rows[10][1].ToString().Substring(10, 5) + "-" + data.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 2/2 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "3")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg23.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 2/3 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "4")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg24.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));


                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 2/4 не на все эсквлаторы ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "2" && CheckBox1.Checked == true)
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg22e.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[11][0].ToString()); fields.SetField("d_1", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[12][0].ToString()); fields.SetField("d_2", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[13][0].ToString()); fields.SetField("d_3", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[14][0].ToString()); fields.SetField("d_4", data.Rows[14][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[11][1].ToString().Substring(10, 5) + "-" + data.Rows[11][2].ToString().Substring(11, 5)); fields.SetField("vp_1", data.Rows[11][7].ToString());
                            fields.SetField("ti_2", data.Rows[12][1].ToString().Substring(10, 5) + "-" + data.Rows[12][2].ToString().Substring(11, 5)); fields.SetField("vp_2", data.Rows[12][7].ToString());
                            fields.SetField("ti_3", data.Rows[13][1].ToString().Substring(10, 5) + "-" + data.Rows[13][2].ToString().Substring(11, 5)); fields.SetField("vp_3", data.Rows[13][7].ToString());
                            fields.SetField("ti_4", data.Rows[14][1].ToString().Substring(10, 5) + "-" + data.Rows[14][2].ToString().Substring(11, 5)); fields.SetField("vp_4", data.Rows[14][7].ToString());
                        }
                        catch { Msg.Text = "Для участка 2/2 не на все эсквлаторы ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "6")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg26.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 2/6 не на все эсквлаторы ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "5")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg25.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", data.Rows[11][0].ToString()); fields.SetField("d_12", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", data.Rows[12][0].ToString()); fields.SetField("d_13", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", data.Rows[13][0].ToString()); fields.SetField("d_14", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", data.Rows[14][0].ToString()); fields.SetField("d_15", data.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", data.Rows[15][0].ToString()); fields.SetField("d_16", data.Rows[15][1].ToString().Remove(10));
                            fields.SetField("tr_17", data.Rows[16][0].ToString()); fields.SetField("d_17", data.Rows[16][1].ToString().Remove(10));
                            fields.SetField("tr_18", data.Rows[17][0].ToString()); fields.SetField("d_18", data.Rows[17][1].ToString().Remove(10));
                            fields.SetField("tr_19", data.Rows[18][0].ToString()); fields.SetField("d_19", data.Rows[18][1].ToString().Remove(10));
                            fields.SetField("tr_20", data.Rows[19][0].ToString()); fields.SetField("d_20", data.Rows[19][1].ToString().Remove(10));
                            fields.SetField("tr_21", data.Rows[20][0].ToString()); fields.SetField("d_21", data.Rows[20][1].ToString().Remove(10));

                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", data.Rows[8][1].ToString().Substring(10, 5) + "-" + data.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", data.Rows[9][1].ToString().Substring(10, 5) + "-" + data.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", data.Rows[10][1].ToString().Substring(10, 5) + "-" + data.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_12", data.Rows[11][1].ToString().Substring(10, 5) + "-" + data.Rows[11][2].ToString().Substring(11, 5)); fields.SetField("vp_12", (data.Rows[11][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_13", data.Rows[12][1].ToString().Substring(10, 5) + "-" + data.Rows[12][2].ToString().Substring(11, 5)); fields.SetField("vp_13", (data.Rows[12][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_14", data.Rows[13][1].ToString().Substring(10, 5) + "-" + data.Rows[13][2].ToString().Substring(11, 5)); fields.SetField("vp_14", (data.Rows[13][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_15", data.Rows[14][1].ToString().Substring(10, 5) + "-" + data.Rows[14][2].ToString().Substring(11, 5)); fields.SetField("vp_15", (data.Rows[14][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_16", data.Rows[15][1].ToString().Substring(10, 5) + "-" + data.Rows[15][2].ToString().Substring(11, 5)); fields.SetField("vp_16", (data.Rows[15][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_17", data.Rows[16][1].ToString().Substring(10, 5) + "-" + data.Rows[16][2].ToString().Substring(11, 5)); fields.SetField("vp_17", (data.Rows[16][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_18", data.Rows[17][1].ToString().Substring(10, 5) + "-" + data.Rows[17][2].ToString().Substring(11, 5)); fields.SetField("vp_18", (data.Rows[17][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_19", data.Rows[18][1].ToString().Substring(10, 5) + "-" + data.Rows[18][2].ToString().Substring(11, 5)); fields.SetField("vp_19", (data.Rows[18][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_20", data.Rows[19][1].ToString().Substring(10, 5) + "-" + data.Rows[19][2].ToString().Substring(11, 5)); fields.SetField("vp_20", (data.Rows[19][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_21", data.Rows[20][1].ToString().Substring(10, 5) + "-" + data.Rows[20][2].ToString().Substring(11, 5)); fields.SetField("vp_21", (data.Rows[20][6].ToString() == "True" ? "вып." : ""));
                         
                            }
                        catch { Msg.Text = "Для участка 2/5 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "2" && IdM.SelectedValue == "7")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg27.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", data.Rows[11][0].ToString()); fields.SetField("d_12", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", data.Rows[12][0].ToString()); fields.SetField("d_13", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", data.Rows[13][0].ToString()); fields.SetField("d_14", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", data.Rows[14][0].ToString()); fields.SetField("d_15", data.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", data.Rows[15][0].ToString()); fields.SetField("d_16", data.Rows[15][1].ToString().Remove(10));
                            fields.SetField("tr_17", data.Rows[16][0].ToString()); fields.SetField("d_17", data.Rows[16][1].ToString().Remove(10));
                            fields.SetField("tr_18", data.Rows[17][0].ToString()); fields.SetField("d_18", data.Rows[17][1].ToString().Remove(10));
                            fields.SetField("tr_19", data.Rows[18][0].ToString()); fields.SetField("d_19", data.Rows[18][1].ToString().Remove(10));
                            fields.SetField("tr_20", data.Rows[19][0].ToString()); fields.SetField("d_20", data.Rows[19][1].ToString().Remove(10));
                            fields.SetField("tr_21", data.Rows[20][0].ToString()); fields.SetField("d_21", data.Rows[20][1].ToString().Remove(10));
                            fields.SetField("tr_22", data.Rows[21][0].ToString()); fields.SetField("d_22", data.Rows[21][1].ToString().Remove(10));
                            fields.SetField("tr_23", data.Rows[22][0].ToString()); fields.SetField("d_23", data.Rows[22][1].ToString().Remove(10));

                            fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_12", (data.Rows[11][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_13", (data.Rows[12][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_14", (data.Rows[13][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_15", (data.Rows[14][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_16", (data.Rows[15][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_17", (data.Rows[16][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_18", (data.Rows[17][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_19", (data.Rows[18][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_20", (data.Rows[19][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_21", (data.Rows[20][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_22", (data.Rows[21][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("vp_23", (data.Rows[22][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 2/7 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                    if (IdU.SelectedValue == "4" && IdM.SelectedValue == "1")
                    {

                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mgr41.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", data.Rows[11][0].ToString()); fields.SetField("d_12", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", data.Rows[12][0].ToString()); fields.SetField("d_13", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", data.Rows[13][0].ToString()); fields.SetField("d_14", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", data.Rows[14][0].ToString()); fields.SetField("d_15", data.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", data.Rows[15][0].ToString()); fields.SetField("d_16", data.Rows[15][1].ToString().Remove(10));
                            fields.SetField("tr_17", data.Rows[16][0].ToString()); fields.SetField("d_17", data.Rows[16][1].ToString().Remove(10));
                            fields.SetField("tr_18", data.Rows[17][0].ToString()); fields.SetField("d_18", data.Rows[17][1].ToString().Remove(10));
                            fields.SetField("tr_19", data.Rows[18][0].ToString()); fields.SetField("d_19", data.Rows[18][1].ToString().Remove(10));
                            fields.SetField("tr_20", data.Rows[19][0].ToString()); fields.SetField("d_20", data.Rows[19][1].ToString().Remove(10));
                            fields.SetField("tr_21", data.Rows[20][0].ToString()); fields.SetField("d_21", data.Rows[20][1].ToString().Remove(10));
                            fields.SetField("tr_22", data.Rows[21][0].ToString()); fields.SetField("d_22", data.Rows[21][1].ToString().Remove(10));
                            fields.SetField("tr_23", data.Rows[22][0].ToString()); fields.SetField("d_23", data.Rows[22][1].ToString().Remove(10));
                            fields.SetField("tr_24", data.Rows[23][0].ToString()); fields.SetField("d_24", data.Rows[23][1].ToString().Remove(10));
                            fields.SetField("tr_25", data.Rows[24][0].ToString()); fields.SetField("d_25", data.Rows[24][1].ToString().Remove(10));
                            fields.SetField("tr_26", data.Rows[25][0].ToString()); fields.SetField("d_26", data.Rows[25][1].ToString().Remove(10));
                            fields.SetField("tr_27", data.Rows[26][0].ToString()); fields.SetField("d_27", data.Rows[26][1].ToString().Remove(10));
                            fields.SetField("tr_28", data.Rows[27][0].ToString()); fields.SetField("d_28", data.Rows[27][1].ToString().Remove(10));
                            fields.SetField("tr_29", data.Rows[28][0].ToString()); fields.SetField("d_29", data.Rows[28][1].ToString().Remove(10));
                            fields.SetField("tr_30", data.Rows[29][0].ToString()); fields.SetField("d_30", data.Rows[29][1].ToString().Remove(10));
                            fields.SetField("tr_31", data.Rows[30][0].ToString()); fields.SetField("d_31", data.Rows[30][1].ToString().Remove(10));
                            fields.SetField("tr_32", data.Rows[31][0].ToString()); fields.SetField("d_32", data.Rows[31][1].ToString().Remove(10));
                            fields.SetField("tr_33", data.Rows[32][0].ToString()); fields.SetField("d_33", data.Rows[32][1].ToString().Remove(10));
                            fields.SetField("tr_34", data.Rows[33][0].ToString()); fields.SetField("d_34", data.Rows[33][1].ToString().Remove(10));
                            fields.SetField("tr_35", data.Rows[34][0].ToString()); fields.SetField("d_35", data.Rows[34][1].ToString().Remove(10));
                            fields.SetField("tr_36", data.Rows[35][0].ToString()); fields.SetField("d_36", data.Rows[35][1].ToString().Remove(10));
                            fields.SetField("tr_37", data.Rows[36][0].ToString()); fields.SetField("d_37", data.Rows[36][1].ToString().Remove(10));
                            fields.SetField("tr_38", data.Rows[37][0].ToString()); fields.SetField("d_38", data.Rows[37][1].ToString().Remove(10));
                            fields.SetField("tr_39", data.Rows[38][0].ToString()); fields.SetField("d_39", data.Rows[38][1].ToString().Remove(10));
                            fields.SetField("tr_40", data.Rows[39][0].ToString()); fields.SetField("d_40", data.Rows[39][1].ToString().Remove(10));
                            fields.SetField("tr_41", data.Rows[40][0].ToString()); fields.SetField("d_41", data.Rows[40][1].ToString().Remove(10));
                            fields.SetField("tr_42", data.Rows[41][0].ToString()); fields.SetField("d_42", data.Rows[41][1].ToString().Remove(10));
                            fields.SetField("tr_43", data.Rows[42][0].ToString()); fields.SetField("d_43", data.Rows[42][1].ToString().Remove(10));
                            fields.SetField("tr_44", data.Rows[43][0].ToString()); fields.SetField("d_44", data.Rows[43][1].ToString().Remove(10));
                            fields.SetField("tr_45", data.Rows[44][0].ToString()); fields.SetField("d_45", data.Rows[44][1].ToString().Remove(10));
                            fields.SetField("tr_46", data.Rows[45][0].ToString()); fields.SetField("d_46", data.Rows[45][1].ToString().Remove(10));
                            fields.SetField("tr_47", data.Rows[46][0].ToString()); fields.SetField("d_47", data.Rows[46][1].ToString().Remove(10));
                            fields.SetField("tr_48", data.Rows[47][0].ToString()); fields.SetField("d_48", data.Rows[47][1].ToString().Remove(10));
                            fields.SetField("tr_49", data.Rows[48][0].ToString()); fields.SetField("d_49", data.Rows[48][1].ToString().Remove(10));
                            fields.SetField("tr_50", data.Rows[49][0].ToString()); fields.SetField("d_50", data.Rows[49][1].ToString().Remove(10));

                             fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_12", (data.Rows[11][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_13", (data.Rows[12][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_14", (data.Rows[13][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_15", (data.Rows[14][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_16", (data.Rows[15][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_17", (data.Rows[16][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_18", (data.Rows[17][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_19", (data.Rows[18][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_20", (data.Rows[19][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_21", (data.Rows[20][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_22", (data.Rows[21][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_23", (data.Rows[22][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_24", (data.Rows[23][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_25", (data.Rows[24][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_26", (data.Rows[25][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_27", (data.Rows[26][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_28", (data.Rows[27][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_29", (data.Rows[28][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_30", (data.Rows[29][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_31", (data.Rows[30][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_32", (data.Rows[31][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_33", (data.Rows[32][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_34", (data.Rows[33][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_35", (data.Rows[34][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_36", (data.Rows[35][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_37", (data.Rows[36][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_38", (data.Rows[37][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_39", (data.Rows[38][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_40", (data.Rows[39][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_41", (data.Rows[40][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_42", (data.Rows[41][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_43", (data.Rows[42][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_44", (data.Rows[43][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_45", (data.Rows[44][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_46", (data.Rows[45][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_47", (data.Rows[46][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_48", (data.Rows[47][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_49", (data.Rows[48][6].ToString() == "True" ? "вып." : ""));
                             fields.SetField("vp_50", (data.Rows[49][6].ToString() == "True" ? "вып." : ""));
                        }
                        catch { Msg.Text = "Для участка 4/1 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                if (IdU.SelectedValue == "4" && IdM.SelectedValue == "2")
                    {
                        //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
                        PdfReader template = new PdfReader(@"C:\temp\mg42.pdf"); // файл шаблона
                        PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\mgm.pdf", FileMode.Create));
                        AcroFields fields = stamper.AcroFields;
                        fields.AddSubstitutionFont(baseFont);
                        fields.SetField("pr_1", ddProrab.Text); //прораб
                        fields.SetField("el_1", To.Text);//механик
                        fields.SetField("m_1", Month.Text);
                        fields.SetField("g_1", Year.Text);
                        try
                        {
                            fields.SetField("tr_1", data.Rows[0][0].ToString()); fields.SetField("d_1", data.Rows[0][1].ToString().Remove(10));
                            fields.SetField("tr_2", data.Rows[1][0].ToString()); fields.SetField("d_2", data.Rows[1][1].ToString().Remove(10));
                            fields.SetField("tr_3", data.Rows[2][0].ToString()); fields.SetField("d_3", data.Rows[2][1].ToString().Remove(10));
                            fields.SetField("tr_4", data.Rows[3][0].ToString()); fields.SetField("d_4", data.Rows[3][1].ToString().Remove(10));
                            fields.SetField("tr_5", data.Rows[4][0].ToString()); fields.SetField("d_5", data.Rows[4][1].ToString().Remove(10));
                            fields.SetField("tr_6", data.Rows[5][0].ToString()); fields.SetField("d_6", data.Rows[5][1].ToString().Remove(10));
                            fields.SetField("tr_7", data.Rows[6][0].ToString()); fields.SetField("d_7", data.Rows[6][1].ToString().Remove(10));
                            fields.SetField("tr_8", data.Rows[7][0].ToString()); fields.SetField("d_8", data.Rows[7][1].ToString().Remove(10));
                            fields.SetField("tr_9", data.Rows[8][0].ToString()); fields.SetField("d_9", data.Rows[8][1].ToString().Remove(10));
                            fields.SetField("tr_10", data.Rows[9][0].ToString()); fields.SetField("d_10", data.Rows[9][1].ToString().Remove(10));
                            fields.SetField("tr_11", data.Rows[10][0].ToString()); fields.SetField("d_11", data.Rows[10][1].ToString().Remove(10));
                            fields.SetField("tr_12", data.Rows[11][0].ToString()); fields.SetField("d_12", data.Rows[11][1].ToString().Remove(10));
                            fields.SetField("tr_13", data.Rows[12][0].ToString()); fields.SetField("d_13", data.Rows[12][1].ToString().Remove(10));
                            fields.SetField("tr_14", data.Rows[13][0].ToString()); fields.SetField("d_14", data.Rows[13][1].ToString().Remove(10));
                            fields.SetField("tr_15", data.Rows[14][0].ToString()); fields.SetField("d_15", data.Rows[14][1].ToString().Remove(10));
                            fields.SetField("tr_16", data.Rows[15][0].ToString()); fields.SetField("d_16", data.Rows[15][1].ToString().Remove(10));
                            fields.SetField("tr_17", data.Rows[16][0].ToString()); fields.SetField("d_17", data.Rows[16][1].ToString().Remove(10));
                            fields.SetField("tr_18", data.Rows[17][0].ToString()); fields.SetField("d_18", data.Rows[17][1].ToString().Remove(10));
                            fields.SetField("tr_19", data.Rows[18][0].ToString()); fields.SetField("d_19", data.Rows[18][1].ToString().Remove(10));
                            fields.SetField("tr_20", data.Rows[19][0].ToString()); fields.SetField("d_20", data.Rows[19][1].ToString().Remove(10));
                            fields.SetField("tr_21", data.Rows[20][0].ToString()); fields.SetField("d_21", data.Rows[20][1].ToString().Remove(10));
                            fields.SetField("tr_22", data.Rows[21][0].ToString()); fields.SetField("d_22", data.Rows[21][1].ToString().Remove(10));
                            fields.SetField("tr_23", data.Rows[22][0].ToString()); fields.SetField("d_23", data.Rows[22][1].ToString().Remove(10));
                            fields.SetField("tr_24", data.Rows[23][0].ToString()); fields.SetField("d_24", data.Rows[23][1].ToString().Remove(10));
                            fields.SetField("tr_25", data.Rows[24][0].ToString()); fields.SetField("d_25", data.Rows[24][1].ToString().Remove(10));
                            fields.SetField("tr_26", data.Rows[25][0].ToString()); fields.SetField("d_26", data.Rows[25][1].ToString().Remove(10));
                            fields.SetField("tr_27", data.Rows[26][0].ToString()); fields.SetField("d_27", data.Rows[26][1].ToString().Remove(10));


                            fields.SetField("ti_1", data.Rows[0][1].ToString().Substring(10, 5) + "-" + data.Rows[0][2].ToString().Substring(11, 5)); fields.SetField("vp_1", (data.Rows[0][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_2", data.Rows[1][1].ToString().Substring(10, 5) + "-" + data.Rows[1][2].ToString().Substring(11, 5)); fields.SetField("vp_2", (data.Rows[1][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_3", data.Rows[2][1].ToString().Substring(10, 5) + "-" + data.Rows[2][2].ToString().Substring(11, 5)); fields.SetField("vp_3", (data.Rows[2][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_4", data.Rows[3][1].ToString().Substring(10, 5) + "-" + data.Rows[3][2].ToString().Substring(11, 5)); fields.SetField("vp_4", (data.Rows[3][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_5", data.Rows[4][1].ToString().Substring(10, 5) + "-" + data.Rows[4][2].ToString().Substring(11, 5)); fields.SetField("vp_5", (data.Rows[4][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_6", data.Rows[5][1].ToString().Substring(10, 5) + "-" + data.Rows[5][2].ToString().Substring(11, 5)); fields.SetField("vp_6", (data.Rows[5][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_7", data.Rows[6][1].ToString().Substring(10, 5) + "-" + data.Rows[6][2].ToString().Substring(11, 5)); fields.SetField("vp_7", (data.Rows[6][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_8", data.Rows[7][1].ToString().Substring(10, 5) + "-" + data.Rows[7][2].ToString().Substring(11, 5)); fields.SetField("vp_8", (data.Rows[7][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_9", data.Rows[8][1].ToString().Substring(10, 5) + "-" + data.Rows[8][2].ToString().Substring(11, 5)); fields.SetField("vp_9", (data.Rows[8][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_10", data.Rows[9][1].ToString().Substring(10, 5) + "-" + data.Rows[9][2].ToString().Substring(11, 5)); fields.SetField("vp_10", (data.Rows[9][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_11", data.Rows[10][1].ToString().Substring(10, 5) + "-" + data.Rows[10][2].ToString().Substring(11, 5)); fields.SetField("vp_11", (data.Rows[10][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_12", data.Rows[11][1].ToString().Substring(10, 5) + "-" + data.Rows[11][2].ToString().Substring(11, 5)); fields.SetField("vp_12", (data.Rows[11][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_13", data.Rows[12][1].ToString().Substring(10, 5) + "-" + data.Rows[12][2].ToString().Substring(11, 5)); fields.SetField("vp_13", (data.Rows[12][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_14", data.Rows[13][1].ToString().Substring(10, 5) + "-" + data.Rows[13][2].ToString().Substring(11, 5)); fields.SetField("vp_14", (data.Rows[13][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_15", data.Rows[14][1].ToString().Substring(10, 5) + "-" + data.Rows[14][2].ToString().Substring(11, 5)); fields.SetField("vp_15", (data.Rows[14][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_16", data.Rows[15][1].ToString().Substring(10, 5) + "-" + data.Rows[15][2].ToString().Substring(11, 5)); fields.SetField("vp_16", (data.Rows[15][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_17", data.Rows[16][1].ToString().Substring(10, 5) + "-" + data.Rows[16][2].ToString().Substring(11, 5)); fields.SetField("vp_17", (data.Rows[16][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_18", data.Rows[17][1].ToString().Substring(10, 5) + "-" + data.Rows[17][2].ToString().Substring(11, 5)); fields.SetField("vp_18", (data.Rows[17][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_19", data.Rows[18][1].ToString().Substring(10, 5) + "-" + data.Rows[18][2].ToString().Substring(11, 5)); fields.SetField("vp_19", (data.Rows[18][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_20", data.Rows[19][1].ToString().Substring(10, 5) + "-" + data.Rows[19][2].ToString().Substring(11, 5)); fields.SetField("vp_20", (data.Rows[19][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_21", data.Rows[20][1].ToString().Substring(10, 5) + "-" + data.Rows[20][2].ToString().Substring(11, 5)); fields.SetField("vp_21", (data.Rows[20][6].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_22", data.Rows[21][1].ToString().Substring(10, 5) + "-" + data.Rows[21][2].ToString().Substring(11, 5)); fields.SetField("vp_22", (data.Rows[21][7].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_23", data.Rows[22][1].ToString().Substring(10, 5) + "-" + data.Rows[22][2].ToString().Substring(11, 5)); fields.SetField("vp_23", (data.Rows[22][7].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_24", data.Rows[23][1].ToString().Substring(10, 5) + "-" + data.Rows[23][2].ToString().Substring(11, 5)); fields.SetField("vp_24", (data.Rows[23][7].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_25", data.Rows[24][1].ToString().Substring(10, 5) + "-" + data.Rows[24][2].ToString().Substring(11, 5)); fields.SetField("vp_25", (data.Rows[24][7].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_26", data.Rows[25][1].ToString().Substring(10, 5) + "-" + data.Rows[25][2].ToString().Substring(11, 5)); fields.SetField("vp_26", (data.Rows[25][7].ToString() == "True" ? "вып." : ""));
                            fields.SetField("ti_27", data.Rows[26][1].ToString().Substring(10, 5) + "-" + data.Rows[26][2].ToString().Substring(11, 5)); fields.SetField("vp_27", (data.Rows[26][7].ToString() == "True" ? "вып." : ""));

                        }
                        catch { Msg.Text = "Для участка 4/2 не все ТР запланированы!"; stamper.Close(); return; }
                        stamper.FormFlattening = false;  // открыт на запись для возможности электронной подписи
                        stamper.Close();
                    }
                } 
                    // запись в БД
                    FileStream fs = new FileStream(@"C:\temp\mgm.pdf", FileMode.Open);
                    Byte[] pdf = new byte[fs.Length];
                    fs.Read(pdf, 0, pdf.Length);
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("insert into DocUm (name, numdoc, img, namefile, status, primm, usr, IdU, IdM) values (@name, @nd, @img, @namefile, @st, @pr, @usr, @idu, @idm )", conn);
                        cmd.Parameters.AddWithValue("name", "план-график");
                        cmd.Parameters.AddWithValue("nd", "Гр-"+ IdU.Text + IdM.Text + "-" + Month.Text + Year.Text );
                        cmd.Parameters.AddWithValue("usr", User.Identity.Name);
                        cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                        cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = "план-график-уч." + IdU.Text + IdM.Text + ".pdf";
                        cmd.Parameters.AddWithValue("pr", "создан-" + dd + "." + mm + "." + yy + " г.");
                        cmd.Parameters.AddWithValue("st", "не подписан заказчиком");
                        cmd.Parameters.AddWithValue("idu", IdU.Text);
                        cmd.Parameters.AddWithValue("idm", IdM.Text);
                        cmd.ExecuteNonQuery();
                    }
                    fs.Close();
                    //  просмотр в браузере
                    Response.ContentType = "image"; //image/Pdf 
                    Response.BinaryWrite(pdf);
                    string pdfFileName = Request.PhysicalApplicationPath;
                    Response.ContentType = "application/x-download";
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", "план-график-уч." + IdU.Text + IdM.Text + ".pdf"));
                    //    Response.ContentEncoding = Encoding.UTF8;
                    //  Response.BinaryWrite(bBuffer);
                    //   Response.WriteFile(pdfFileName);
                    //    Response.HeaderEncoding = Encoding.UTF8;
                    Response.Flush();
                    Response.End();
                 
        }

        protected void YearSchedule_Click(object sender, EventArgs e)
        {

        }

        protected void PrWork_Click(object sender, EventArgs e)
        {
            //блок формирования приказа о закреплении механика
                string dat = DateTime.Now.Date.ToLongDateString();
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
                string ho = DateTime.Now.Hour.ToString();
                string mi = DateTime.Now.Minute.ToString();
                string se = DateTime.Now.Second.ToString();
                string IdUM = IdU.Text + IdM.Text;
                int npr;
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
               
                      if (IdU.SelectedValue == "1" && IdM.SelectedValue == "1") Sh = "pr11";
                      if (IdU.SelectedValue == "1" && IdM.SelectedValue == "3") Sh = "pr13";
                      if (IdU.SelectedValue == "1" && IdM.SelectedValue == "4") Sh = "pr14";
                      if (IdU.SelectedValue == "1" && IdM.SelectedValue == "5") Sh = "pr15";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "1") Sh = "pr21";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "2") Sh = "pr22";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "3") Sh = "pr23";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "4") Sh = "pr24";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "5") Sh = "pr25";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "6") Sh = "pr26";
                      if (IdU.SelectedValue == "4" && IdM.SelectedValue == "1") Sh = "pr41";
                      if (IdU.SelectedValue == "2" && IdM.SelectedValue == "7") Sh = "pr27";

                      if (Month.SelectedValue == "январь") MonthY = "января";
                      if (Month.SelectedValue == "февраль") MonthY = "февраля";
                      if (Month.SelectedValue == "март") MonthY = "марта";
                      if (Month.SelectedValue == "апрель") MonthY = "апреля";
                      if (Month.SelectedValue == "май") MonthY = "мая";
                      if (Month.SelectedValue == "июнь") MonthY = "июня";
                      if (Month.SelectedValue == "июль") MonthY = "июля";
                      if (Month.SelectedValue == "август") MonthY = "августа";
                      if (Month.SelectedValue == "сентябрь") MonthY = "сентября";
                      if (Month.SelectedValue == "октябрь") MonthY = "октября";
                      if (Month.SelectedValue == "ноябрь") MonthY = "ноября";
                      if (Month.SelectedValue == "декабрь") MonthY = "декабря";
                     
                      if (To.Text == "Беляков Д.В.") userNameA = "Белякова Д.В.";
                      if (To.Text == "Бурханов А.А.") userNameA = "Бурханова А.А.";
                      if (To.Text == "Кулахметов Х.И. ") userNameA = "Кулахметова Х.И.";
                      if (To.Text == "Михеенко В.А.") userNameA = "Михеенко В.А.";
                      if (To.Text == "Волченко Р.") userNameA = "Волченко Р.";
                      if (To.Text == "Агинский В.Д.") userNameA = "Агинского В.Д.";
                      if (To.Text == "Володин А.В.") userNameA = "Володина А.В";
                      if (To.Text == "Пузин А.В.") userNameA = "Пузина А.В.";
                      if (To.Text == "Вергейчик А.") userNameA = "Вергейчика А.";
                      if (To.Text == "Саргамонов А.В.") userNameA = "Саргамонова А.В.";
                      if (To.Text == "Андрейчук В.В.") userNameA = "Андрейчука В.В.";
                      if (To.Text == "Барабанов Ю.М.") userNameA = "Барабанова Ю.М.";

                      if (ddProrab.Text == "Беляков Д.В.") userNameY = " Белякову Д.В.";
                      if (ddProrab.Text == "Бурханов А.А.") userNameY = "Бурханову А.А.";
                      if (ddProrab.Text == "Кулахметов Х.И. ") userNameY = "Кулахметову Х.И.";
                      if (ddProrab.Text == "Михеенко В.А.") userNameY = "Михеенко В.А.";
                      if (ddProrab.Text == "Волченко Р.") userNameY = "Волченко Р.";
                      if (ddProrab.Text == "Агинский В.Д.") userNameY = "Агинскому В.Д.";
                      if (ddProrab.Text == "Володин А.В.") userNameY = "Володину А.В.";
                      if (ddProrab.Text == "Пузин А.В.") userNameY = "    Пузину А.В.";
                      if (ddProrab.Text == "Вергейчик А.") userNameY = "Вергейчику А.";
                      if (ddProrab.Text == "Саргамонов А.В.") userNameY = "Саргамонову А.В.";
                      if (ddProrab.Text == "Андрейчук В.В.") userNameY = "Андрейчуку В.В.";
                      if (ddProrab.Text == "Барабанов Ю.М.") userNameY = "Барабанову Ю.М.";

                      if (ddGlIng.Text == "Володин А.В.") userNameG = "Володина А.В";
                      if (ddGlIng.Text == "Биктимиров М.Ф.") userNameG = "Биктимирова М.Ф.";

                 //   DataTable data = db.GetPlanUM(userName, IdUM, dateOn, dateEn);
            App_Code.Base db1 = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
              npr = db1.GetTotalDocUM("приказ о закреплении") +1;

                    PdfReader template = new PdfReader(@"C:\temp\uploads\"+Sh+".pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\pre.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("Text1", "З-0" + IdU.Text + "/0" + IdM.Text + "-0" + npr.ToString()); //№ приказа
                    fields.SetField("Text2", Day.Text + " " + MonthY + " " + Year.Text + " г."); //длинная дата
                    fields.SetField("Text3", Year.Text);//год
                    fields.SetField("Text4", userNameA);//механика
                    fields.SetField("Text5", userNameY); //прорабу
                    fields.SetField("Text6", userNameA); //механика
                    fields.SetField("Text7", userNameG); //гл. инженера
                    fields.SetField("Text8", To.Text); //ознакомлен механик
                    fields.SetField("Text9", ddProrab.Text); //ознакомлен прораб
                    fields.SetField("Text10", ddGlIng.Text); //ознакомлен гл. инженер
                    fields.SetField("Text14", "З-0" + IdU.Text + "/0" + IdM.Text + "-0" + npr.ToString()); //№ приказа прилож.
                    fields.SetField("Text15", Day.Text + " " + MonthY + " " + Year.Text + " г."); //короткая дата
                    fields.SetField("Text16", ddProrab.Text); //прораб
                    fields.SetField("Text17", To.Text);//механик

                  stamper.FormFlattening = true;  // 
                  stamper.Close();
                 
                    // запись в БД
                    FileStream fs = new FileStream(@"C:\temp\pre.pdf", FileMode.Open);
                    Byte[] pdf = new byte[fs.Length];
                    fs.Read(pdf, 0, pdf.Length);
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("insert into DocUm (name, numdoc, img, namefile, status, primm, usr, IdU, IdM) values (@name, @nd, @img, @namefile, @st, @pr, @usr, @idu, @idm )", conn);
                        cmd.Parameters.AddWithValue("name", "приказ о закреплении");
                        cmd.Parameters.AddWithValue("nd", "З-0" + IdU.Text+"/0" + IdM.Text + "-0" + Month.Text + Year.Text);
                        cmd.Parameters.AddWithValue("usr", User.Identity.Name);
                        cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                        cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = "приказ о закреплении уч." + IdU.Text + IdM.Text + ".pdf";
                        cmd.Parameters.AddWithValue("pr", "создан-" + dd + "." + mm + "." + yy + " г.");
                        cmd.Parameters.AddWithValue("st", "не подписан");
                        cmd.Parameters.AddWithValue("idu", IdU.Text);
                        cmd.Parameters.AddWithValue("idm", IdM.Text);
                        cmd.ExecuteNonQuery();
                    }
                    fs.Close();
                      //  просмотр в браузере
                    Response.ContentType = "image"; //image/Pdf 
                    Response.BinaryWrite(pdf);
                    string pdfFileName = Request.PhysicalApplicationPath;
                    Response.ContentType = "application/x-download";
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", "приказ о закреплении-уч." + IdU.Text + IdM.Text + ".pdf"));
                    //    Response.ContentEncoding = Encoding.UTF8;
                    //  Response.BinaryWrite(bBuffer);
                    //   Response.WriteFile(pdfFileName);
                    //    Response.HeaderEncoding = Encoding.UTF8;
                    Response.Flush();
                    Response.End();
        }

        protected void PrProrab_Click(object sender, EventArgs e)
        {
            //блок формирования приказа о закреплении механика
            string dat = DateTime.Now.Date.ToLongDateString();
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
            string ho = DateTime.Now.Hour.ToString();
            string mi = DateTime.Now.Minute.ToString();
            string se = DateTime.Now.Second.ToString();
            string IdUM = IdU.Text + IdM.Text;
            int npr;
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            if (IdU.SelectedValue == "1" && IdM.SelectedValue == "1") ShP = "prp11";
            if (IdU.SelectedValue == "1" && IdM.SelectedValue == "3") ShP = "prp13";
            if (IdU.SelectedValue == "1" && IdM.SelectedValue == "4") ShP = "prp14";
            if (IdU.SelectedValue == "1" && IdM.SelectedValue == "5") ShP = "prp15";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "1") ShP = "prp21";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "2") ShP = "prp22";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "3") ShP = "prp23";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "4") ShP = "prp24";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "5") ShP = "prp25";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "6") ShP = "prp26";
            if (IdU.SelectedValue == "4" && IdM.SelectedValue == "1") ShP = "prp41";
            if (IdU.SelectedValue == "2" && IdM.SelectedValue == "7") ShP = "prp27";

            if (Month.SelectedValue == "январь") MonthY = "января";
            if (Month.SelectedValue == "февраль") MonthY = "февраля";
            if (Month.SelectedValue == "март") MonthY = "марта";
            if (Month.SelectedValue == "апрель") MonthY = "апреля";
            if (Month.SelectedValue == "май") MonthY = "мая";
            if (Month.SelectedValue == "июнь") MonthY = "июня";
            if (Month.SelectedValue == "июль") MonthY = "июля";
            if (Month.SelectedValue == "август") MonthY = "августа";
            if (Month.SelectedValue == "сентябрь") MonthY = "сентября";
            if (Month.SelectedValue == "октябрь") MonthY = "октября";
            if (Month.SelectedValue == "ноябрь") MonthY = "ноября";
            if (Month.SelectedValue == "декабрь") MonthY = "декабря";

            if (To.Text == "Беляков Д.В.") userNameA = "Белякова Д.В.";
            if (To.Text == "Бурханов А.А.") userNameA = "Бурханова А.А.";
            if (To.Text == "Кулахметов Х.И. ") userNameA = "Кулахметова Х.И.";
            if (To.Text == "Михеенко В.А.") userNameA = "Михеенко В.А.";
            if (To.Text == "Волченко Р.") userNameA = "Волченко Р.";
            if (To.Text == "Агинский В.Д.") userNameA = "Агинского В.Д.";
            if (To.Text == "Володин А.В.") userNameA = "Володина А.В.";
            if (To.Text == "Пузин А.В.") userNameA = "Пузина А.В.";
            if (To.Text == "Вергейчик А.") userNameA = "Вергейчика А.";
            if (To.Text == "Саргамонов А.В.") userNameA = "Саргамонова А.В.";
            if (To.Text == "Андрейчук В.В.") userNameA = "Андрейчука В.В.";
            if (To.Text == "Барабанов Ю.М.") userNameA = "Барабанова Ю.М.";

            if (ddProrab.Text == "Беляков Д.В.") userNameY = " Белякова Д.В.";
            if (ddProrab.Text == "Бурханов А.А.") userNameY = "Бурханова А.А.";
            if (ddProrab.Text == "Кулахметов Х. И. ") userNameY = "Кулахметова Х.И.";
            if (ddProrab.Text == "Михеенко В.А.") userNameY = "Михеенко В.А.";
            if (ddProrab.Text == "Волченко Р.") userNameY = "Волченко Р.";
            if (ddProrab.Text == "Агинский В.Д.") userNameY = "Агинского В.Д.";
            if (ddProrab.Text == "Володин А.В.") userNameY = "Володина А.В.";
            if (ddProrab.Text == "Пузин А.В.") userNameY = "    Пузина А.В.";
            if (ddProrab.Text == "Вергейчик А.") userNameY = "Вергейчика А.";
            if (ddProrab.Text == "Саргамонов А.В.") userNameY = "Саргамонова А.В.";
            if (ddProrab.Text == "Андрейчук В.В.") userNameY = "Андрейчука В.В.";
            if (ddProrab.Text == "Барабанов Ю.М.") userNameY = "Барабанова Ю.М.";

            if (ddGlIng.Text == "Володин А.В.") userNameG = "Володина А.В.";
            if (ddGlIng.Text == "Биктимиров М.Ф.") userNameG = "Биктимирова М.Ф.";
            App_Code.Base db1 = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            npr = db1.GetTotalDocUM("приказ о ТО") + 1;// присвоение номера документу
            PdfReader template = new PdfReader(@"C:\temp\uploads\" + ShP + ".pdf"); // файл шаблона
            PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\prp.pdf", FileMode.Create));
            AcroFields fields = stamper.AcroFields;
            fields.AddSubstitutionFont(baseFont);
            fields.SetField("Text1", "ТО-0" + IdU.Text + "/0" + IdM.Text + "-0" + npr.ToString()); //№ приказа
            fields.SetField("Text2", Day.Text + " " + MonthY + " " + Year.Text + " г."); //длинная дата
            fields.SetField("Text3", userNameY);//a
            fields.SetField("Text4", userNameY);//a
            fields.SetField("Text5", userNameG); //
            fields.SetField("Text6", userNameG); //
            fields.SetField("Text7", ddProrab.Text); //
            fields.SetField("Text8", ddGlIng.Text); //

            stamper.FormFlattening = true;  // 
            stamper.Close();

            // запись в БД
            FileStream fs = new FileStream(@"C:\temp\prp.pdf", FileMode.Open);
            Byte[] pdf = new byte[fs.Length];
            fs.Read(pdf, 0, pdf.Length);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into DocUm (name, numdoc, img, namefile, status, primm, usr, IdU, IdM) values (@name, @nd, @img, @namefile, @st, @pr, @usr, @idu, @idm )", conn);
                cmd.Parameters.AddWithValue("name", "приказ о ТО");
                cmd.Parameters.AddWithValue("nd", "ТО-0" + IdU.Text + "/0" + IdM.Text + "-0" + Month.Text + Year.Text);
                cmd.Parameters.AddWithValue("usr", User.Identity.Name);
                cmd.Parameters.Add("img", SqlDbType.Image).Value = pdf;
                cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = "приказ о ТО уч." + IdU.Text + IdM.Text + ".pdf";
                cmd.Parameters.AddWithValue("pr", "создан-" + dd +"." + mm +"." + yy +" г.");
                cmd.Parameters.AddWithValue("st", "не подписан");
                cmd.Parameters.AddWithValue("idu", IdU.Text);
                cmd.Parameters.AddWithValue("idm", IdM.Text);
                cmd.ExecuteNonQuery();
            }
            fs.Close();
            //  просмотр в браузере
            Response.ContentType = "image"; //image/Pdf 
            Response.BinaryWrite(pdf);
            string pdfFileName = Request.PhysicalApplicationPath;
            Response.ContentType = "application/x-download";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", "приказ об организации ТО-уч." + IdUM + ".pdf"));
            //    Response.ContentEncoding = Encoding.UTF8;
            //  Response.BinaryWrite(bBuffer);
            //   Response.WriteFile(pdfFileName);
            //    Response.HeaderEncoding = Encoding.UTF8;
            Response.Flush();
            Response.End();
        }
    }
     
}