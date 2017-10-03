using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KOS
{
    public partial class ZakrytieTSG : System.Web.UI.Page

    {
        int _wz = 0;
        string yyy, momo, ddd, hhh, mmm, yyy2, momo2, ddd2, hhh2, mmm2;
        string  yyy1 = DateTime.Now.Year.ToString();
        string momo1 = DateTime.Now.Month.ToString();
        string ddd1 = DateTime.Now.Day.ToString();
        string hhh1=DateTime.Now.Hour.ToString();
        string mmm1 = DateTime.Now.Minute.ToString();
        DateTime dateReg;
        class aType
        {
            public aType(string s) { Email = s; }
            public string Email { get; set; }
        }
        List<aType> _type = new List<aType>()
        {
            new aType(""), new aType("заявки механику")
        };
        string _role = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (User.Identity.Name != "ODS14") { Treg.Visible = false; Tzak.Visible = false; }
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetEvent(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                Id.Text = _wz.ToString();
                Sourse.Text = data.Rows[0]["Sourse"].ToString();
                Text1.Text = data.Rows[0]["Sourse"].ToString();
                IO.Text = data.Rows[0]["IO"].ToString();
                DataId.Text = data.Rows[0]["DataId"].ToString();
                dateReg = ((DateTime)data.Rows[0]["DataId"]);
                if (!string.IsNullOrEmpty(data.Rows[0]["DataId"].ToString()))
                {
                    yyy = ((DateTime)data.Rows[0]["DataId"]).Year.ToString();
                    ddd = ((DateTime)data.Rows[0]["DataId"]).Day.ToString();
                    momo = ((DateTime)data.Rows[0]["DataId"]).Month.ToString();
                    hhh = ((DateTime)data.Rows[0]["DataId"]).Hour.ToString();
                    mmm = ((DateTime)data.Rows[0]["DataId"]).Minute.ToString();
                }
                RegistrId.Text = data.Rows[0]["RegistrId"].ToString();                
                LiftId.Text = data.Rows[0]["LiftId"].ToString();
                TextBox4.Text = data.Rows[0]["LiftId"].ToString();
                TypeId.Text = data.Rows[0]["TypeId"].ToString();
                EventId.Text = data.Rows[0]["EventId"].ToString();
                Text.Text = data.Rows[0]["EventId"].ToString();
                ToApp.Text = data.Rows[0]["ToApp"].ToString();
                Text2.Text = data.Rows[0]["ToApp"].ToString();
                DateToApp.Text = data.Rows[0]["DateToApp"].ToString();
                if (!string.IsNullOrEmpty(data.Rows[0]["DateToApp"].ToString()))
                {
                    yyy1 = ((DateTime)data.Rows[0]["DateToApp"]).Year.ToString();
                    ddd1 = ((DateTime)data.Rows[0]["DateToApp"]).Day.ToString();
                    momo1 = ((DateTime)data.Rows[0]["DateToApp"]).Month.ToString();
                    hhh1 = ((DateTime)data.Rows[0]["DateToApp"]).Hour.ToString();
                    mmm1 = ((DateTime)data.Rows[0]["DateToApp"]).Minute.ToString();
                }                
                Who.Text = data.Rows[0]["Who"].ToString();
                Comment.Text = data.Rows[0]["Comment"].ToString();
                Text5.Text = data.Rows[0]["Prim"].ToString();
                Prim.Text = data.Rows[0]["Prim"].ToString();
                DateWho.Text = data.Rows[0]["DateWho"].ToString();
             //   TextBox3.Text = data.Rows[0]["DateWho"].ToString();
             //   if (string.IsNullOrEmpty(TextBox3.Text))
             //   TextBox3.Text = Convert.ToString(DateTime.Now);
                string d = string.Empty;
                string d2 = string.Empty, t2 = string.Empty;
                TimeSpan pr = DateTime.Now - ((DateTime)data.Rows[0]["DataId"]);
                if (!(data.Rows[0]["DateWho"] is DBNull))
                {
                    d2 = ((DateTime)data.Rows[0]["DateWho"]).Date.ToString();
                    t2 = ((DateTime)data.Rows[0]["DateWho"]).TimeOfDay.ToString();
                    pr = ((DateTime)data.Rows[0]["DateWho"]) - ((DateTime)data.Rows[0]["DataId"]);
                }
                if (!(data.Rows[0]["DateToApp"] is DBNull))
                {
                    d = ((DateTime)data.Rows[0]["DateToApp"]).ToString();
                }
                string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                    pr.Minutes.ToString();
                Timing.Text = prostoy;
                {
                    yyy2 = DateTime.Now.Year.ToString();
                    ddd2 = DateTime.Now.Day.ToString();
                    momo2 = DateTime.Now.Month.ToString();
                    hhh2 = DateTime.Now.Hour.ToString();
                    mmm2 = DateTime.Now.Minute.ToString();
                }  
            }
            //Год/месяц/день
            List<string> yy = new List<string>() { yyy, "2016", "2017", "2018", "2019", "2020" };
            if (!IsPostBack)
            {
                YY.DataSource = yy;
                YY.DataBind();
                YY.SelectedIndex = 0;
            }
            List<string> yy1 = new List<string>() { yyy1, "2016", "2017", "2018", "2019", "2020" };
            if (!IsPostBack)
            {
                YY1.DataSource = yy1;
                YY1.DataBind();
                YY1.SelectedIndex = 0;
            }
            List<string> yy2 = new List<string>() { yyy2, "2016", "2017", "2018", "2019", "2020" };
            if (!IsPostBack)
            {
                YY2.DataSource = yy2;
                YY2.DataBind();
                YY2.SelectedIndex = 0;
            }
            List<string> mo = new List<string>() { momo, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            if (!IsPostBack)
            {
                MO.DataSource = mo;
                MO.DataBind();
                MO.SelectedIndex = 0;
            }
            List<string> mo1 = new List<string>() { momo1, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            if (!IsPostBack)
            {
                MO1.DataSource = mo1;
                MO1.DataBind();
                MO1.SelectedIndex = 0;
            }
            List<string> mo2 = new List<string>() { momo2, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            if (!IsPostBack)
            {
                MO2.DataSource = mo2;
                MO2.DataBind();
                MO2.SelectedIndex = 0;
            }
            List<string> dd = new List<string>() {ddd, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

            if (!IsPostBack)
            {
                DD.DataSource = dd;
                DD.DataBind();
                DD.SelectedIndex = 0;
            }
            List<string> dd1 = new List<string>() {ddd1, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

            if (!IsPostBack)
            {
                DD1.DataSource = dd1;
                DD1.DataBind();
                DD1.SelectedIndex = 0;
            }
            List<string> dd2 = new List<string>() { ddd2, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };

            if (!IsPostBack)
            {
                DD2.DataSource = dd2;
                DD2.DataBind();
                DD2.SelectedIndex = 0;
            }
 
            //Часы/минуты
            List<string> hh = new List<string>() { hhh, "0", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
            if (!IsPostBack)
            {
                HH.DataSource = hh;
                HH.DataBind();
                HH.SelectedIndex = 0;               
            }
            List<string> hh1 = new List<string>() { hhh1, "0", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
            if (!IsPostBack)
            {
                HH1.DataSource = hh1;
                HH1.DataBind();
                HH1.SelectedIndex = 0;
            }
            List<string> hh2 = new List<string>() { hhh2, "0", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
            if (!IsPostBack)
            {
                HH2.DataSource = hh2;
                HH2.DataBind();
                HH2.SelectedIndex = 0;
            }
            List<string> mm = new List<string>() { mmm, "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55", "00" };
            if (!IsPostBack)
            {
                MM.DataSource = mm;
                MM.DataBind();
                MM.SelectedIndex = 0;
            }
            List<string> mm1 = new List<string>() { mmm1, "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55", "00" };
            if (!IsPostBack)
            {
                MM1.DataSource = mm1;
                MM1.DataBind();
                MM1.SelectedIndex = 0;
            }
            List<string> mm2 = new List<string>() { mmm2, "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55", "00" };
            if (!IsPostBack)
            {
                MM2.DataSource = mm2;
                MM2.DataBind();
                MM2.SelectedIndex = 0;
            }

         //Диспетчер            
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select p.surname, p.name, p.midlename from People p " +
                    "where p.comments=@user and p.specialty=N'диспетчер'", conn);

                cmd.Parameters.AddWithValue("user", User.Identity.Name);

                List<string> iodisp = new List<string>() { IO.Text };
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    iodisp.Add(dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString());
                dr.Close();
                if (!IsPostBack)
                {
                    FIO.DataSource = iodisp;
                    FIO.DataBind();
                    FIO.SelectedIndex = 0;
                    Fdr.Visible = false;
                    TextBox5.Text = IO.Text;
                }
              
            }
            //Механик
            List<string> worker = new List<string>() { ToApp.Text };
            if (!IsPostBack)
            {
                Workers.DataSource = worker;
                Workers0.DataSource = worker;
                Workers.DataBind();
                Workers0.DataBind();
                Workers.SelectedIndex = 0;
                Workers0.SelectedIndex = 0;
                Worker_TextChanged(this, EventArgs.Empty);
            }
   
            if (RegistrId.Text == "Эксплуатация лифтов")
            {
                Wrk.Visible = true;
                Wrk0.Visible = true;
                Text2.Visible = false;
                Text3.Visible = false;
            }
            else
            {
                Wrk.Visible = false;
                Wrk0.Visible = false;
                Text2.Visible = true;
                Text3.Visible = true;
            }
        }

        //редактирование
        protected void Edit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            try {   
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                string s = "";
                DateTime bg = dateReg;
                DateTime ap = date;
                    if (RegistrId.Text == "Эксплуатация лифтов" & Workers.SelectedValue != " ")
                        s = "update Events set EventId=@e, Sourse=@s, IO=@fio, DataId=@de, LiftId=@z, ToApp=@toapp, DateToApp=@da, Prim=@pr where Id=@i";
                    else if (RegistrId.Text == "Эксплуатация лифтов" & Workers.SelectedValue == " ")
                        s = "update Events set EventId=@e, Sourse=@s, IO=@fio, DataId=@de, LiftId=@z, Prim=@pr where Id=@i";
                    else if (RegistrId.Text != "Эксплуатация лифтов" & (!string.IsNullOrEmpty(Text2.Text)))
                        s = "update Events set EventId=@e, Sourse=@s, IO=@fio, DataId=@de, LiftId=@z, ToApp=@toapp, DateToApp=@da, Prim=@pr where Id=@i";
                    else if (RegistrId.Text != "Эксплуатация лифтов" & (string.IsNullOrEmpty(Text2.Text)))
                        s = "update Events set EventId=@e, Sourse=@s, IO=@fio, DataId=@de, LiftId=@z, Prim=@pr where Id=@i";
                    SqlCommand cmd = new SqlCommand(s, conn);
                     bg = Convert.ToDateTime(DD.SelectedValue + "." + MO.SelectedValue + "." + YY.SelectedValue + " " + HH.SelectedValue + ":" + MM.SelectedValue + ":00");
                     ap = Convert.ToDateTime(DD1.SelectedValue + "." + MO1.SelectedValue + "." + YY1.SelectedValue + " " + HH1.SelectedValue + ":" + MM1.SelectedValue + ":00");
                    cmd.Parameters.AddWithValue("de", bg);   
                    cmd.Parameters.AddWithValue("s", Text1.Text);
                    cmd.Parameters.AddWithValue("e", Text.Text);
                    cmd.Parameters.AddWithValue("pr", Text5.Text);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.Parameters.AddWithValue("fio", TextBox5.Text); //FIO.SelectedValue
                    cmd.Parameters.AddWithValue("z", TextBox4.Text);
                    if (RegistrId.Text == "Эксплуатация лифтов")
                        cmd.Parameters.AddWithValue("toapp", Workers.SelectedValue);
                    else
                        cmd.Parameters.AddWithValue("toapp", Text2.Text);
                    if (Text2.Text != " " || Workers.SelectedValue != " ")
                        cmd.Parameters.AddWithValue("da", ap);                   
                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/Reg_tsg.aspx");
                }
            } 
            catch
            {
                Msg.Text = "Формат даты/времени не верный!";
            }
            Msg.Text = "Что то не так!";
        }
        protected void Prim_Click(object sender, EventArgs e) 
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (string.IsNullOrEmpty(Prim.Text))
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    DateTime date = DateTime.Now;
                    SqlCommand cmd = new SqlCommand("update Events " +
                     "set Prim=@pr where Id=@i", conn);
                    cmd.Parameters.AddWithValue("pr", "[Диспетчер "+ IO.Text +"]:"+ Text5.Text);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/Reg_tsg.aspx");
                }
            }
            else return;
        }
        protected void Worker_TextChanged(object sender, EventArgs e)
        {
            if (Workers.SelectedItem == null)
                return;
            // закрепленные механики за ОДС
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO from WorkerLifts wl " +
                    "join UserInfo ui on wl.UserId=ui.UserId " +
                    "where wl.LiftId=@liftId", conn);
                cmd.Parameters.AddWithValue("liftId", LiftId.Text);
                List<string> worker = new List<string>() { ToApp.Text };
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    worker.Add(dr[0].ToString() + " " + dr[1].ToString());
                dr.Close();
                {
                    Workers.DataSource = worker;
                    Workers0.DataSource = worker;
                    Workers.DataBind();
                    Workers0.DataBind();
                    Workers.SelectedIndex = 0;
                    Workers0.SelectedIndex = 0;
                    Wrk.Visible = true;
                }
            }

        }
        //закрытие
        protected void Close_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
               
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            try
            {
               
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    DateTime date = DateTime.Now;
                    SqlCommand cmd = new SqlCommand("update Events " +
                     "set Cansel=@c, DateCansel=@d, Who=@w, DateWho=@dw, Comment=@com, Timing=@t where Id=@i", conn);                    
                    DateTime cl = Convert.ToDateTime(DD2.SelectedValue + "." + MO2.SelectedValue + "." + YY2.SelectedValue + " " + HH2.SelectedValue + ":" + MM2.SelectedValue + ":00");
                    TimeSpan pr = cl - Convert.ToDateTime(DataId.Text);
                 //   pr = cl - dateReg;
                    string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" + pr.Minutes.ToString();
                    if ((int)pr.TotalDays < 0 )  // || ((int)pr.Hours) < 0 )
                    {
                        Msg.Text = "Дата/время Исполнения не может быть раньше даты/времени Регистрации!";
                        return; 
                    }
                    //     if (string.IsNullOrEmpty(TextBox3.Text)) TextBox3.Text = Convert.ToString(date);
                    cmd.Parameters.AddWithValue("d", date);
                    cmd.Parameters.AddWithValue("dw", cl);
                    cmd.Parameters.AddWithValue("c", true);
                    cmd.Parameters.AddWithValue("com", Text4.Text);
                    cmd.Parameters.AddWithValue("t", prostoy);
                    if (RegistrId.Text == "Эксплуатация лифтов")
                        cmd.Parameters.AddWithValue("w", Workers0.SelectedValue);
                    else
                        cmd.Parameters.AddWithValue("w", Text3.Text);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/Reg_tsg.aspx");
                }
            }
            catch { Msg.Text = "Такой даты в этом месяце не бывает!"; }
        }

        protected void EditTime_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;            
                try
                {
                    SqlCommand cmd = new SqlCommand("update Events " +
                     "set  DataId=@ds, DateToApp=@dt where Id=@i", conn);
                       DateTime bg = Convert.ToDateTime(DD.SelectedValue + "." + MO.SelectedValue + "." + YY.SelectedValue + " " + HH.SelectedValue + ":" + MM.SelectedValue + ":00");
                       DateTime ap = Convert.ToDateTime(DD1.SelectedValue + "." + MO1.SelectedValue + "." + YY1.SelectedValue + " " + HH1.SelectedValue + ":" + MM1.SelectedValue + ":00");
                       TimeSpan pr = ap - dateReg;
                    string prosto = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" + pr.Minutes.ToString();
                      if ( (int)pr.TotalDays < 0 )  //|| ((int)pr.Hours) < 0 ) 
                    {
                          Msg.Text = "Дата/время Принятия не может быть раньше даты/времени Регистрации!";
                          return;
                    }
                    cmd.Parameters.AddWithValue("ds", bg); // Convert.ToDateTime(TextBox1.Text));
                    //   if (string.IsNullOrEmpty(bg)) bg = Convert.ToString(date);
                    cmd.Parameters.AddWithValue("dt", ap);  // Convert.ToDateTime(TextBox2.Text));               

                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("~/Reg_tsg.aspx?zId=");
                }
                catch { Msg.Text = "Дата принятия не может быть [00.0000]! или такого дня нет в этом месяце!"; return; }
            }

        }
      
    }
}