using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using KOS.App_Code;
using System.IO;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace KOS
{
    public partial class ZayavkaPrinyal : System.Web.UI.Page
    {
        int evid;
        // byte[] _f;
        string WorkPin = "";
        string Spec = "";
        string _role, _prinyal, _wz, adrs, numlift, famzak1; 
        class Data
        {
            public string Date { get; set; }
            public string From { get; set; }
            public string Text { get; set; }
            public string Usr { get; set; }
        }
        class Users : Object
        {
            public string Fio { get; set; }
            public string Id { get; set; }
            public override string ToString()
            {
                return Fio;
            }
        }
        List<Users> _users = new List<Users>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                Response.Redirect("~/About.aspx");
            _role = CheckAccount();
            List<string> vidakt = new List<string>() { "Акт дефектовки", "Акт повреждения оборудования", "Акт установки", "Акт выполнения внеплановых работ" };
            List<string> chel = new List<string>() { "", "Да", "Нет" };
            List<string> chas = new List<string>() { "", "Да", "Нет" };
            List<string> pnr = new List<string>() { "", "Да", "Нет" };
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

                Pnr.DataSource = pnr;
                Pnr.DataBind();
                Pnr.SelectedIndex = 0;

            }
            if (_role == "Manager") { ActZNa.Visible = true; }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select ui.Family, ui.IO, ui.UserId from UserInfo ui " +
                        "join UsersInRoles ur on ur.UserId=ui.UserId join Users u on u.UserId=ui.UserId where ur.RoleId='a51bcf45-c4cd-4a68-93e0-b699b3d47b02'", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Users user = new Users()
                        {
                            Fio = dr[0].ToString() + " " + dr[1].ToString(),
                            Id = dr[2].ToString()
                        };
                        _users.Add(user);
                    }
                    dr.Close();

                    cmd = new SqlCommand("select ui.Family, ui.IO, ui.UserId from UserInfo ui " +
                        "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Prinyal.Text = dr[0].ToString() + " " + dr[1].ToString();
                        _prinyal = dr[2].ToString();
                    }
                    dr.Close();
            }

            if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
            {
               int zId = Int32.Parse(Request["zId"]);
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select tt.Ttx, z.LiftId, z.[Text], ui.Family as FromFamily, ui.[IO] as FromIO, " +
                        "z.Category, z.[From], z.Start, ui2.Family, ui2.[IO], z.[Status], z.PrinyalDate, ui3.Family as WorkerFamily, " +
                        "ui3.[IO] as WorkerIO, z.Finish, z.Couse  from Zayavky z " +
                        "join Ttx tt on tt.Id=z.TtxId join UserInfo ui on ui.UserId=z.UserId " +
                        "left join UserInfo ui2 on ui2.UserId=z.Prinyal " +
                        "left join UserInfo ui3 on ui3.UserId=z.Worker where z.Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", zId);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Address.Text = dt.Rows[0]["Ttx"].ToString();
                        Lift.Text = dt.Rows[0]["LiftId"].ToString();
                        Text.Text = dt.Rows[0]["Text"].ToString();
                    //    Disp.Text = dt.Rows[0]["FromFamily"].ToString() + " " + dt.Rows[0]["FromIO"].ToString();
                        Category.Text = dt.Rows[0]["Category"].ToString();
                    //    From.Text = dt.Rows[0]["From"].ToString();
                   //     Start.Text = dt.Rows[0]["Start"].ToString();
                        StartPrinyal.Text = dt.Rows[0]["PrinyalDate"].ToString();
                        Status.Text = (bool.Parse(dt.Rows[0]["Status"].ToString()) == true ? "закрыто" : "активно");
                    //    if (!(dt.Rows[0]["WorkerFamily"] is DBNull))
                    //    Worker.Text = dt.Rows[0]["WorkerFamily"].ToString() + " " + dt.Rows[0]["WorkerIO"].ToString();
                   /*   TimeSpan pr = DateTime.Now - ((DateTime)dt.Rows[0]["Start"]);
                        if (Status.Text == "закрыто") { pr = ((DateTime)dt.Rows[0]["Finish"]) - ((DateTime)dt.Rows[0]["Start"]); }
                        Prostoy.Text = "простой: " + ((int)pr.TotalDays).ToString() + " дн. " + pr.Hours.ToString() + " час. " +
                        pr.Minutes.ToString() + " мин."; */
                   /*     Worker.DataSource = _users; //выбор механика из выпадающего списка
                        Worker.DataBind();
                        if (_role == "Worker")
                        {
                            int n = _users.FindIndex(delegate(Users u)
                            {
                                return u.Id == _prinyal;
                            });
                            if (n >= 0)
                                Worker.SelectedIndex = n;
                        }
                        else if (dt.Rows.Count > 0)
                        {
                            cmd = new SqlCommand("select wl.UserId from WorkerLifts wl " +
                                "join Users u on u.UserId=wl.UserId where wl.LiftId=@LiftId", conn);
                            cmd.Parameters.AddWithValue("LiftId", dt.Rows[0]["LiftId"]);
                            Object o = cmd.ExecuteScalar();
                            if (o != null)
                            {
                                int n = _users.FindIndex(delegate(Users u)
                                {
                                    return u.Id == o.ToString();
                                });
                                if (n >= 0)
                                    Worker.SelectedIndex = n;                               
                            }
                        } */
                        if (Category.Text == "застревание") ChangeCat.Visible = true;
                    }
             
                }
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("select Id from Events where ZayavId=@wz", conn);
                        cmd.Parameters.AddWithValue("wz", zId);
                        evid = int.Parse(cmd.ExecuteScalar().ToString());
                        NumEvent.Text = Convert.ToString(evid);//номер события
                        //   NumEvent0.Text = Convert.ToString(evid);
                    }
                    catch { NumEvent.Text = "не зарегестрировано"; }
                }
                int _id = Int32.Parse(NumEvent.Text);
                App_Code.Base db1 = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable da1 = db1.GetEvent(_id);
                if (da1.Rows.Count < 1) { Msg.Text = "нет события с таким №" + _id; return; }
                Address.Text = da1.Rows[0]["Address"].ToString();
                StartPrinyal.Text = da1.Rows[0]["DateToApp"].ToString();
                Prinyal.Text = da1.Rows[0]["ToApp"].ToString();
                Comm.Text = da1.Rows[0]["Comment"].ToString();
                Disp.Text = da1.Rows[0]["Family"].ToString();
                TimeSpan pr = DateTime.Now - ((DateTime)da1.Rows[0]["DataId"]);
                if (!(da1.Rows[0]["DateWho"] is DBNull)) { pr = ((DateTime)da1.Rows[0]["DateWho"]) - ((DateTime)da1.Rows[0]["DataId"]); }
                        Prostoy.Text = ((int)pr.TotalDays).ToString() + " дн. " + pr.Hours.ToString() + " час. " +
                        pr.Minutes.ToString() + " мин.";
                Ustr.Text = Text1.Text;
                if (!IsPostBack)
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("select surname, name, midlename from People where specialty=N'заказчик' and comments=@disp", conn);
                        cmd.Parameters.AddWithValue("disp", Disp.Text);
                        SqlDataReader dn = cmd.ExecuteReader();
                        if (dn.Read())
                        { famzak1 = dn[0].ToString() + " " + dn[1].ToString() + " " + dn[2].ToString(); }
                        dn.Close();
                    }
                    FamZak.Text = famzak1; 
                    //  DopZapp.Visible = true;
                    App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    DataTable dt = db.GetPartsList(evid);
                    //     if (dt.Rows.Count < 1) DopZapp.Visible = false;
                    try
                    {
                        Name.Text = dt.Rows[0]["Name"] is DBNull ? "" : dt.Rows[0]["Name"].ToString();
                        Obz.Text = dt.Rows[0]["Obz"] is DBNull ? "" : dt.Rows[0]["Obz"].ToString();
                        ID0.Text = dt.Rows[0]["NumID"] is DBNull ? "" : dt.Rows[0]["NumID"].ToString();
                        Kol.Text = dt.Rows[0]["Kol"] is DBNull ? "" : dt.Rows[0]["Kol"].ToString();
                    }

                    catch { }
                    try
                    {
                        Name1.Text = dt.Rows[1]["Name"] is DBNull ? "" : dt.Rows[1]["Name"].ToString();
                        Obz1.Text = dt.Rows[1]["Obz"] is DBNull ? "" : dt.Rows[1]["Obz"].ToString();
                        ID1.Text = dt.Rows[1]["NumID"] is DBNull ? "" : dt.Rows[1]["NumID"].ToString();
                        Kol1.Text = dt.Rows[1]["Kol"] is DBNull ? "" : dt.Rows[1]["Kol"].ToString();
                    }

                    catch { }
                    try
                    {
                        Name2.Text = dt.Rows[2]["Name"] is DBNull ? "" : dt.Rows[2]["Name"].ToString();
                        Obz2.Text = dt.Rows[2]["Obz"] is DBNull ? "" : dt.Rows[2]["Obz"].ToString();
                        ID2.Text = dt.Rows[2]["NumID"] is DBNull ? "" : dt.Rows[2]["NumID"].ToString();
                        Kol2.Text = dt.Rows[2]["Kol"] is DBNull ? "" : dt.Rows[2]["Kol"].ToString();
                    }
                    catch { }
                    try
                    {
                        Name3.Text = dt.Rows[3]["Name"] is DBNull ? "" : dt.Rows[3]["Name"].ToString();
                        Obz3.Text = dt.Rows[3]["Obz"] is DBNull ? "" : dt.Rows[3]["Obz"].ToString();
                        ID3.Text = dt.Rows[3]["NumID"] is DBNull ? "" : dt.Rows[3]["NumID"].ToString();
                        Kol3.Text = dt.Rows[3]["Kol"] is DBNull ? "" : dt.Rows[3]["Kol"].ToString();
                    }
                    catch { }
                    try
                    {
                        Name4.Text = dt.Rows[4]["Name"].ToString();
                        Obz4.Text = dt.Rows[4]["Obz"].ToString();
                        ID4.Text = dt.Rows[4]["NumID"].ToString();
                        Kol4.Text = dt.Rows[4]["Kol"].ToString();
                    }
                    catch { }
                    List<Data> dataop = GetData();
                    Out.DataSource = dataop;
                    Out.DataBind();
                }
            }
        }
        List<Data> GetData()
        {
            List<Data> dataop = new List<Data>();
          //  string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select  h.Date, h.Text, h.UserName, h.[From] from HistEv h " +
                "where h.NumEvent=@id and h.Category=N'описание неисправности'", conn);
                cmd.Parameters.AddWithValue("id", NumEvent.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    dataop.Add(new Data()
                    {
                        Date = (dr["Date"].ToString()),
                        From = (dr["From"].ToString()),
                        Text = dr["Text"].ToString(),
                        Usr = dr["UserName"].ToString()
                    });
                }
                dr.Close();
            }
            return dataop;
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
                    //    CloseEv.Visible = true;
                        phAction.Visible = true;
                        phPodp.Visible = true;
                        phPoleVvoda.Visible = true;
                        phCheckBox.Visible = true;
                        phPinClose.Visible = false;
                    }
                    else
                    {
                        Msg.Text = "Внимание! Закрытие События является особо ответственной и необратимой операцией! Eсли Вы уверены, что все работы и документы по этому Событию закрыты, то нажмите кнопку Закрыть Событие! Вернуться без закрытия - На главную.";
                    //    CloseEv.Visible = true;
                        phAction.Visible = false;
                        phPodp.Visible = true;
                        phPinClose.Visible = false;
                        Button4.Visible = true;
                    }
                       // Msg.Text = "Внимание! Закрытие События является особо ответственной операцией! Ввод пин-кода является аналогом Вашей подписи!";
                }
                catch { Msg.Text = "Неверный пин-код!"; return; }
                
            }

        }
        protected void DonePrn_Click(object sender, EventArgs e)
        {
            if (StartPrinyal.Text != "") return;
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Zayavky " +
                    "set Prinyal=@w, PrinyalDate=@f, [Status]=@s where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                cmd.Parameters.AddWithValue("w", _prinyal); //
                cmd.Parameters.AddWithValue("f", DateTime.Now);
                cmd.Parameters.AddWithValue("s", false);
              //  cmd.Parameters.AddWithValue("c", Text1.Text);
                cmd.ExecuteNonQuery();
                Status.Text = "принято";
                cmd = new SqlCommand("select Id from Events e" +
                " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                // проверка истории, обновление события
                int _id = Int32.Parse(_wz);
                cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", _wz);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", User.Identity.Name);
                cmd.Parameters.AddWithValue("txt", Text1.Text);
                cmd.Parameters.AddWithValue("cat", Category.Text);
                cmd.Parameters.AddWithValue("ph", "");
                cmd.Parameters.AddWithValue("to", "manager");
                cmd.Parameters.AddWithValue("cm", "Принято пользователем: " + User.Identity.Name);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Events " +
                 "set ToApp=@w, DateToApp=@f where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", _id);
                cmd.Parameters.AddWithValue("w", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", DateTime.Now);               
                cmd.ExecuteNonQuery();

            }
            Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
        }
        protected void Done_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            if (Label1.Text == "") { Msg.Text = "Для Закрытия События требуется ввести ПИН-код!"; phPinClose.Visible = true; return; }
            phPoleVvoda.Visible = true;
            Text1.Text = Comm.Text;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Zayavky " +
                    "set Worker=@w, Finish=@f, [Status]=@s, Couse=@c where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                cmd.Parameters.AddWithValue("w", _prinyal); //
                cmd.Parameters.AddWithValue("f", DateTime.Now);
                cmd.Parameters.AddWithValue("s", true);
                cmd.Parameters.AddWithValue("c", Text1.Text);
                cmd.ExecuteNonQuery();
                Status.Text = "закрыто";
                cmd = new SqlCommand("select Id from Events e" +
                " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                int _id = Int32.Parse(_wz);
                // запись истории события 
                cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", _wz);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", User.Identity.Name);
                cmd.Parameters.AddWithValue("txt", Text1.Text);
                cmd.Parameters.AddWithValue("cat", Category.Text);
                cmd.Parameters.AddWithValue("ph", "закрыто из обработчика события ОДС");
                cmd.Parameters.AddWithValue("to", "manager");
                cmd.Parameters.AddWithValue("cm", "Закрыто пользователем: " + User.Identity.Name);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update Events " +
                 "set Who=@w, DateWho=@f, Cansel=@c, DateCansel=@d, Comment=@cm where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", _id);
                cmd.Parameters.AddWithValue("w", Label1.Text);
                cmd.Parameters.AddWithValue("f", DateTime.Now);
                cmd.Parameters.AddWithValue("c", true);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("cm", Text1.Text);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/");
        }
        protected void Save_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
           /*     SqlCommand cmd = new SqlCommand("update Zayavky " +
             //       "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                      "set [Finish]=1 where Id=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                cmd.Parameters.AddWithValue("p", _prinyal);
                cmd.Parameters.AddWithValue("s", true);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                Users n = _users.Find(delegate(Users u)
                {
                    return u.Fio == Worker.SelectedValue;
                });
                if (n != null)
                {
                    cmd.Parameters.AddWithValue("w", n.Id);
                    cmd.ExecuteNonQuery();
                } */
                SqlCommand cmd = new SqlCommand("select e.Id from Events e" +
                 " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                int _id = Int32.Parse(_wz);
                Status.Text = "не активно"; 
                // обновление события по заявке
                if (_wz != null)
                {
                  /*  cmd = new SqlCommand("update Events " +
                     "set ToApp=@a, DateToApp=@f where Id=@i", conn);
                    cmd.Parameters.AddWithValue("f", DateTime.Now);
                    cmd.Parameters.AddWithValue("a", Worker.SelectedValue);                    
                    cmd.Parameters.AddWithValue("i", _id);
                    cmd.ExecuteNonQuery(); */
                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn); 
                    cmd.Parameters.AddWithValue("ne", _wz);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    try
                    {
                        
                        cmd.Parameters.AddWithValue("txt", Text1.Text);
                        cmd.Parameters.AddWithValue("cat", "дополнительный механик ");
                        cmd.Parameters.AddWithValue("ph", "");
                        cmd.Parameters.AddWithValue("to", "manager");
                        cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);   
                        cmd.Parameters.AddWithValue("u", User.Identity.Name);
                        cmd.Parameters.AddWithValue("f", Label1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    catch { Msg.Text = "!!!"; return; }
                    if (Text1.Text != "")
                    {
                        cmd = new SqlCommand("update Zayavky " +
                            //  Category=N'ПНР/РЭО'     "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                            "set Category=N'ПНР/РЭО' where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", zId);
                        cmd.ExecuteNonQuery();
                    }
                    if (Text1.Text == "")
                    {
                        cmd = new SqlCommand("update Zayavky " +
                            //  "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                            "set [Finish]=1 where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", zId);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (_role == "Worker")
                Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
            else
                Response.Redirect("~/");
        }
        protected void Akt_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "") { Msg.Text = "Для формирования актов требуется ввести ПИН-код!"; phPinClose.Visible = true; return; }
            //блок формирования акта 
            string PodpEmic = Spec + " " + WorkPin;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select t.Ttx from Ttx t " +
                    "join LiftsTtx l on l.TtxId=t.Id " +
                    //   "join Ttx tt on tt.Id=t.Id " +
                    "where t.TtxTitleId=1 and l.LiftId=@t", conn);
                cmd.Parameters.AddWithValue("t", Lift.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { adrs = dr[0].ToString(); }
                dr.Close();
                cmd = new SqlCommand("select PlantNum from PhisicalAddr where LiftId=@lift", conn);
                cmd.Parameters.AddWithValue("lift", Lift.Text);
                SqlDataReader dn = cmd.ExecuteReader();
                if (dn.Read())
                { numlift = dn[0].ToString(); }
                dn.Close();
                string dat = DateTime.Now.Date.ToLongDateString();
                string dd = DateTime.Now.Day.ToString();
                string mm = DateTime.Now.Month.ToString();
                string yy = DateTime.Now.Year.ToString();
                string hh = DateTime.Now.Hour.ToString() + "час." + DateTime.Now.Minute.ToString() + "мин.";
                evid = Int32.Parse(NumEvent.Text);
              //  App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
              //  DataTable data = db.GetEvent(evid);
              //  string worker = data.Rows[0]["Family"].ToString() + " " + data.Rows[0]["IO"].ToString();
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                if (VidAkta.SelectedValue == "Акт дефектовки")
                {
                    PdfReader template = new PdfReader(@"C:\temp\aktDef.pdf"); // файл шаблона
                    PdfStamper stamper = new PdfStamper(template, new FileStream(@"C:\temp\akt.pdf", FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    fields.AddSubstitutionFont(baseFont);
                    fields.SetField("NaktDef", NumEvent.Text + "-А1.1"); //номер акта
                    fields.SetField("adr", adrs);
                    fields.SetField("Nlift", Lift.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", Lift.Text);
                    fields.SetField("opis2", Comm.Text);
                    fields.SetField("fill_6", Chel.SelectedValue);
                    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("pnr", Pnr.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
                    fields.SetField("name1", Name.Text);
                    if (Name.Text != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name2", Name1.Text);
                    if (Name1.Text != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name3", Name2.Text);
                    if (Name2.Text != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name4", Name3.Text);
                    if (Name3.Text != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name5", Name4.Text);
                    if (Name4.Text != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("obz1", Obz.Text);
                    fields.SetField("obz2", Obz1.Text);
                    fields.SetField("obz3", Obz2.Text);
                    fields.SetField("obz4", Obz3.Text);
                    fields.SetField("obz5", Obz4.Text);
                    fields.SetField("ID1", ID0.Text);
                    fields.SetField("ID2", ID1.Text);
                    fields.SetField("ID3", ID2.Text);
                    fields.SetField("ID4", ID3.Text);
                    fields.SetField("ID5", ID4.Text);
                    fields.SetField("kol1", Kol.Text);
                    fields.SetField("kol2", Kol1.Text);
                    fields.SetField("kol3", Kol2.Text);
                    fields.SetField("kol4", Kol3.Text);
                    fields.SetField("kol5", Kol4.Text);
                    stamper.FormFlattening = false;  // открыт для записи для возможности электронной подписи
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
                    cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = NumEvent.Text +"_" + dd + mm + yy + "_A1_1.pdf";
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
                    fields.SetField("Nlift", Lift.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", Lift.Text);
                    fields.SetField("opis2", Comm.Text);
                    fields.SetField("fill_6", Chel.SelectedValue);
                    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
                    fields.SetField("name1", Name.Text);
                    if (Name.Text != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name2", Name1.Text);
                    if (Name1.Text != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name3", Name2.Text);
                    if (Name2.Text != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name4", Name3.Text);
                    if (Name3.Text != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name5", Name4.Text);
                    if (Name4.Text != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("obz1", Obz.Text);
                    fields.SetField("obz2", Obz1.Text);
                    fields.SetField("obz3", Obz2.Text);
                    fields.SetField("obz4", Obz3.Text);
                    fields.SetField("obz5", Obz4.Text);
                    fields.SetField("ID1", ID0.Text);
                    fields.SetField("ID2", ID1.Text);
                    fields.SetField("ID3", ID2.Text);
                    fields.SetField("ID4", ID3.Text);
                    fields.SetField("ID5", ID4.Text);
                    fields.SetField("kol1", Kol.Text);
                    fields.SetField("kol2", Kol1.Text);
                    fields.SetField("kol3", Kol2.Text);
                    fields.SetField("kol4", Kol3.Text);
                    fields.SetField("kol5", Kol4.Text);
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
                    fields.SetField("Nlift", Lift.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", Lift.Text);
                    fields.SetField("opis2", Comm.Text);
                    fields.SetField("fill_6", Chel.SelectedValue);
                    //    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
                    fields.SetField("name1", Name.Text);
                    if (Name.Text != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name2", Name1.Text);
                    if (Name1.Text != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name3", Name2.Text);
                    if (Name2.Text != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name4", Name3.Text);
                    if (Name3.Text != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name5", Name4.Text);
                    if (Name4.Text != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("obz1", Obz.Text);
                    fields.SetField("obz2", Obz1.Text);
                    fields.SetField("obz3", Obz2.Text);
                    fields.SetField("obz4", Obz3.Text);
                    fields.SetField("obz5", Obz4.Text);
                    fields.SetField("ID1", ID0.Text);
                    fields.SetField("ID2", ID1.Text);
                    fields.SetField("ID3", ID2.Text);
                    fields.SetField("ID4", ID3.Text);
                    fields.SetField("ID5", ID4.Text);
                    fields.SetField("kol1", Kol.Text);
                    fields.SetField("kol2", Kol1.Text);
                    fields.SetField("kol3", Kol2.Text);
                    fields.SetField("kol4", Kol3.Text);
                    fields.SetField("kol5", Kol4.Text);
                    stamper.FormFlattening = false;
                    stamper.Close();
                    // запись в БД
                    FileStream fs = new FileStream(@"C:\temp\akt.pdf", FileMode.Open);
                    Byte[] pdf = new byte[fs.Length];
                    fs.Read(pdf, 0, pdf.Length);
                    cmd = new SqlCommand("insert into Documents (Name, NumEvent, Image, NameFile, Status, Usr) values (@name, @nev, @img, @namefile, @st, @usr )", conn);
                    cmd.Parameters.AddWithValue("name", "акт установки");
                    cmd.Parameters.AddWithValue("nev", NumEvent.Text);
                    cmd.Parameters.AddWithValue("usr", Label1.Text);
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
                    fields.SetField("Nlift", Lift.Text + " № " + numlift);
                    fields.SetField("opis1", Text.Text);
                    fields.SetField("lift", Lift.Text);
                    fields.SetField("opis2", Comm.Text);
                    fields.SetField("fill_6", Chel.SelectedValue);
                    //    fields.SetField("fill_7", Chas.SelectedValue);
                    fields.SetField("dateS", dd + "." + mm + "." + yy + "г.");
                    fields.SetField("dateP", "");
                    fields.SetField("Podt", FamZak.Text);
                    fields.SetField("Sost", Label1.Text);
                    fields.SetField("aspS", "pin: ok");
                    fields.SetField("name1", Name.Text);
                    if (Name.Text != "")
                        fields.SetField("npp1", " 1.");
                    fields.SetField("name2", Name1.Text);
                    if (Name1.Text != "")
                        fields.SetField("npp2", " 2.");
                    fields.SetField("name3", Name2.Text);
                    if (Name2.Text != "")
                        fields.SetField("npp3", " 3.");
                    fields.SetField("name4", Name3.Text);
                    if (Name3.Text != "")
                        fields.SetField("npp4", " 4.");
                    fields.SetField("name5", Name4.Text);
                    if (Name4.Text != "")
                        fields.SetField("npp5", " 5.");
                    fields.SetField("obz1", Obz.Text);
                    fields.SetField("obz2", Obz1.Text);
                    fields.SetField("obz3", Obz2.Text);
                    fields.SetField("obz4", Obz3.Text);
                    fields.SetField("obz5", Obz4.Text);
                    fields.SetField("ID1", ID0.Text);
                    fields.SetField("ID2", ID1.Text);
                    fields.SetField("ID3", ID2.Text);
                    fields.SetField("ID4", ID3.Text);
                    fields.SetField("ID5", ID4.Text);
                    fields.SetField("kol1", Kol.Text);
                    fields.SetField("kol2", Kol1.Text);
                    fields.SetField("kol3", Kol2.Text);
                    fields.SetField("kol4", Kol3.Text);
                    fields.SetField("kol5", Kol4.Text);
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
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
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
                evid = Int32.Parse(NumEvent.Text);
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable dt = db.GetPartsList(evid);
                try
                {
                    Name.Text = dt.Rows[0]["Name"] is DBNull ? "" : dt.Rows[0]["Name"].ToString();
                    Obz.Text = dt.Rows[0]["Obz"] is DBNull ? "" : dt.Rows[0]["Obz"].ToString();
                    ID0.Text = dt.Rows[0]["NumID"] is DBNull ? "" : dt.Rows[0]["NumID"].ToString();
                    Kol.Text = dt.Rows[0]["Kol"] is DBNull ? "" : dt.Rows[0]["Kol"].ToString();
                }

                catch { }
                try
                {
                    Name1.Text = dt.Rows[1]["Name"] is DBNull ? "" : dt.Rows[1]["Name"].ToString();
                    Obz1.Text = dt.Rows[1]["Obz"] is DBNull ? "" : dt.Rows[1]["Obz"].ToString();
                    ID1.Text = dt.Rows[1]["NumID"] is DBNull ? "" : dt.Rows[1]["NumID"].ToString();
                    Kol1.Text = dt.Rows[1]["Kol"] is DBNull ? "" : dt.Rows[1]["Kol"].ToString();
                }

                catch { }
                try
                {
                    Name2.Text = dt.Rows[2]["Name"] is DBNull ? "" : dt.Rows[2]["Name"].ToString();
                    Obz2.Text = dt.Rows[2]["Obz"] is DBNull ? "" : dt.Rows[2]["Obz"].ToString();
                    ID2.Text = dt.Rows[2]["NumID"] is DBNull ? "" : dt.Rows[2]["NumID"].ToString();
                    Kol2.Text = dt.Rows[2]["Kol"] is DBNull ? "" : dt.Rows[2]["Kol"].ToString();
                }
                catch { }
                try
                {
                    Name3.Text = dt.Rows[3]["Name"] is DBNull ? "" : dt.Rows[3]["Name"].ToString();
                    Obz3.Text = dt.Rows[3]["Obz"] is DBNull ? "" : dt.Rows[3]["Obz"].ToString();
                    ID3.Text = dt.Rows[3]["NumID"] is DBNull ? "" : dt.Rows[3]["NumID"].ToString();
                    Kol3.Text = dt.Rows[3]["Kol"] is DBNull ? "" : dt.Rows[3]["Kol"].ToString();
                }
                catch { }
                try
                {
                    Name4.Text = dt.Rows[4]["Name"].ToString();
                    Obz4.Text = dt.Rows[4]["Obz"].ToString();
                    ID4.Text = dt.Rows[4]["NumID"].ToString();
                    Kol4.Text = dt.Rows[4]["Kol"].ToString();
                }
                catch { }
            }
            Text4.Text = ""; Text7.Text = ""; Text2.Text = ""; Text3.Text = "";
        }
        protected void ZakParts_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Zayavky " +
                    "set [Finish]=1 where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", zId);
                cmd.ExecuteNonQuery();

                evid = Int32.Parse(NumEvent.Text);
                cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                cmd.Parameters.AddWithValue("ne", evid);
                cmd.Parameters.AddWithValue("d", DateTime.Now);
                cmd.Parameters.AddWithValue("u", User.Identity.Name);
                cmd.Parameters.AddWithValue("f", "Заказал: " + Label1.Text);
                cmd.Parameters.AddWithValue("txt", "Заказаны запчасти");
                cmd.Parameters.AddWithValue("cat", "запчасти и расходные материалы");
                cmd.Parameters.AddWithValue("ph", "заказ");
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
           
        }
        protected void DopZap_Click(object sender, EventArgs e)
        {
            phDopZap.Visible = true;
            phAktZap.Visible = true;
           

        }

        protected void FormAkt_Click(object sender, EventArgs e)
        {
            phAktZap.Visible = true;
            phDopZap.Visible = true;
            if (Label1.Text == "") { Msg.Text = "Для формирвания актов введите ПИН - код!"; phPinClose.Visible = true; return; }

          //  phAktZap.Visible = true;
           
            
        }

        protected void DopMex_Click(object sender, EventArgs e)
        {
            phDopZap.Visible = false;
            phAktZap.Visible = false;
           
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
               
                SqlCommand cmd = new SqlCommand("select e.Id from Events e" +
                 " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                int _id = Int32.Parse(_wz);
                Status.Text = "не активно";
                // обновление события по заявке
                if (_wz != null)
                {
                   
                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", _wz);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    try
                    {

                        cmd.Parameters.AddWithValue("txt", Text1.Text);
                        cmd.Parameters.AddWithValue("cat", "дополнительный механик ");
                        cmd.Parameters.AddWithValue("ph", "обработка события ОДС");
                        cmd.Parameters.AddWithValue("to", "manager");
                        cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);
                        cmd.Parameters.AddWithValue("u", User.Identity.Name);
                        cmd.Parameters.AddWithValue("f", Label1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    catch { Msg.Text = "!!!"; return; }
                   
                        cmd = new SqlCommand("update Zayavky " +
                            //  "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                            "set [Finish]=1 where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", zId);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        cmd.ExecuteNonQuery();
                    
                }
            }
          /*  if (_role == "Worker")
                Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
            else */
                Response.Redirect("~/");
        }
        // смена категории на ПНР/РЭО
        protected void ReoPnr_Click(object sender, EventArgs e)
        {
           
           // phDopResReo.Visible = true;
           
            phDopZap.Visible = false;
            phAktZap.Visible = false;
           
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
              
                SqlCommand cmd = new SqlCommand("select e.Id from Events e" +
                 " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                int _id = Int32.Parse(_wz);
                Status.Text = "не активно";
                // обновление события по заявке
                if (_wz != null)
                {
                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", _wz);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    try
                    {

                        cmd.Parameters.AddWithValue("txt", Text1.Text);
                        cmd.Parameters.AddWithValue("cat", "ПНР/РЭО");
                        cmd.Parameters.AddWithValue("ph", "обработка события ОДС");
                        cmd.Parameters.AddWithValue("to", "manager");
                        cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);
                        cmd.Parameters.AddWithValue("u", User.Identity.Name);
                        cmd.Parameters.AddWithValue("f", Label1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    catch { Msg.Text = "!!!"; return; }
                    
                        cmd = new SqlCommand("update Zayavky " +
                            //  Category=N'ПНР/РЭО'     "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                            "set Category=N'ПНР/РЭО' where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", zId);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update Events set [ZaprosMng]=1, RegistrId=N'ПНР/РЭО' where Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        cmd.ExecuteNonQuery();
                    
                }
            }
         /*   if (_role == "Worker")
                Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
            else  */
                Response.Redirect("~/");
        }

        protected void Vnr_Click(object sender, EventArgs e)
        {
           
          //  phDopResVnr.Visible = true;
           
            phDopZap.Visible = false;
            phAktZap.Visible = false;
           
          if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
        /*    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
              
                SqlCommand cmd = new SqlCommand("select e.Id from Events e" +
                 " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                int _id = Int32.Parse(_wz);
                Status.Text = "не активно";
                // обновление события по заявке
                if (_wz != null)
                {
                   
                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", _wz);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    try
                    {

                        cmd.Parameters.AddWithValue("txt", Text1.Text);
                        cmd.Parameters.AddWithValue("cat", "ВНР");
                        cmd.Parameters.AddWithValue("ph", "обработка события ОДС");
                        cmd.Parameters.AddWithValue("to", "manager");
                        cmd.Parameters.AddWithValue("cm", "Отправлен запрос менеджеру от " + Label1.Text);
                        cmd.Parameters.AddWithValue("u", User.Identity.Name);
                        cmd.Parameters.AddWithValue("f", Label1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    catch { Msg.Text = "!!!"; return; }
                    
                        cmd = new SqlCommand("update Zayavky " +
                            //  "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                            "set [Finish]=1 where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", zId);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("update Events set [ZaprosMng]=1 where Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        cmd.ExecuteNonQuery();
                    
                }
            } */
            if (_role == "Worker")
                Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
            else
                Response.Redirect("~/");
            Response.Redirect("~/Planning.aspx");
        }
        // очистка полей ввода
        protected void Button1_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            phPinClose.Visible = false;
            phCloseEvent.Visible = false;     
            phDopZap.Visible = false;
            phPoleVvoda.Visible = false;
            phAktZap.Visible = false;
        }

        protected void ZakZap_Click(object sender, EventArgs e)
        {
           // PartsZak_Click(sender, e);
                   
            phDopZap.Visible = true;
            phAktZap.Visible = false;
            //
        }
        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Zayavka.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            roles = new List<string>() { "ODS", "Cadry"};
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            Response.Redirect("~/About.aspx");
            return null;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
        // перевод в активные у механика
        protected void Button6_Click(object sender, EventArgs e)
        {
         if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
           
                SqlCommand cmd = new SqlCommand("select e.Id from Events e" +
                 " where e.ZayavId=@i", conn);
                cmd.Parameters.AddWithValue("i", zId);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _wz = dr[0].ToString();
                }
                dr.Close();
                int _id = Int32.Parse(_wz);
                Status.Text = "не активно"; 
                // Активация заявки у механика
                   
                        cmd = new SqlCommand("update Zayavky " +
                            //  "set Prinyal=@p, PrinyalDate =@d, [Status]=@s, Worker=@w where Id=@i", conn);
                            "set [Finish]=null where Id=@i", conn);
                        cmd.Parameters.AddWithValue("i", zId);                    
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("update Events set [ZaprosMng]=0 where Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                        cmd.Parameters.AddWithValue("ne", _id);
                        cmd.Parameters.AddWithValue("d", DateTime.Now);
                        cmd.Parameters.AddWithValue("u", User.Identity.Name);
                        cmd.Parameters.AddWithValue("f", Label1.Text);
                        cmd.Parameters.AddWithValue("txt", Text1.Text);
                        cmd.Parameters.AddWithValue("cat", "активация");
                        cmd.Parameters.AddWithValue("ph", "из обработчика события ОДС");
                        cmd.Parameters.AddWithValue("to", "manager");
                        cmd.Parameters.AddWithValue("cm", "перевод в активные у механика");
                        cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/");
        }
        // зарытие через пин-код
        protected void ZkEv_Click(object sender, EventArgs e)
        {
            phPinClose.Visible = true;
            phPodp.Visible = false;
            phAction.Visible = false;
            ZkEv.Visible = false;         
              Msg.Text = "Для Закрытия События требуется ввести пин-код! ";
                   
            phCloseEvent.Visible = true;
        }
        //описание неисправности
        protected void OpisNi_Click(object sender, EventArgs e)
        {
        if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            if (Text1.Text != "")
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
              /*      SqlCommand cmd = new SqlCommand("update Zayavky " +
                        "set Text=@txt where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", zId);
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.ExecuteNonQuery();
              */
                    evid = Int32.Parse(NumEvent.Text);
                    SqlCommand cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", evid);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    cmd.Parameters.AddWithValue("u", User.Identity.Name);
                    cmd.Parameters.AddWithValue("f", Label1.Text);
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", "описание неисправности");
                    cmd.Parameters.AddWithValue("ph", "из обработчика события ОДС");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "подробное описание неисправности после осмотра");
                    cmd.ExecuteNonQuery();

                      cmd = new SqlCommand("update Events set Comment=@com where Id=@id", conn);
                      cmd.Parameters.AddWithValue("id", evid);
                      cmd.Parameters.AddWithValue("com", Text1.Text);
                      cmd.ExecuteNonQuery();
                }
            else { Msg.Text = "Поле ввода пуcтое! Или не введен ПИН-код!"; return; }
            Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
        }
        // панель чекбокса
        protected void Button7_Click(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true) Chel.Text = "Да";
            if (CheckBox2.Checked == true) Chas.Text = "Да";
            if (CheckBox5.Checked == true) Pnr.Text = "Да";
            if (CheckBox4.Checked == true) phDopZap.Visible = true;
            phAktZap.Visible = true;
        }

        protected void ChangeCat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                return;
            int zId = Int32.Parse(Request["zId"]);
            if (Label1.Text != "")
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                         SqlCommand cmd = new SqlCommand("update Zayavky " +
                              "set Category=N'останов' where Id=@id", conn);
                          cmd.Parameters.AddWithValue("id", zId);
                   //       cmd.Parameters.AddWithValue("txt", Text1.Text);
                          cmd.ExecuteNonQuery();
                   
                    evid = Int32.Parse(NumEvent.Text);
                    cmd = new SqlCommand("insert into HistEv (NumEvent, Date, Text, Category, UserName, [From], [To], Comment, PrimHist) values (@ne, @d, @txt, @cat, @u, @f, @to, @cm, @ph )", conn);
                    cmd.Parameters.AddWithValue("ne", evid);
                    cmd.Parameters.AddWithValue("d", DateTime.Now);
                    cmd.Parameters.AddWithValue("u", User.Identity.Name);
                    cmd.Parameters.AddWithValue("f", Label1.Text);
                    cmd.Parameters.AddWithValue("txt", Text1.Text);
                    cmd.Parameters.AddWithValue("cat", "смена категории");
                    cmd.Parameters.AddWithValue("ph", "из обработчика события ОДС");
                    cmd.Parameters.AddWithValue("to", "manager");
                    cmd.Parameters.AddWithValue("cm", "смена категории из застреания на останов");
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("update Events set TypeId=N'останов' where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", evid);
                   // cmd.Parameters.AddWithValue("com", Text1.Text);
                    cmd.ExecuteNonQuery();
                }
            else { Msg.Text = "Не введен ПИН-код!"; return; }
            Response.Redirect("~/ZayavkaEdit.aspx?zId=" + zId.ToString());
        }
        
    }
}