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
    public partial class ZakrytieODS : System.Web.UI.Page
    {
        int _type = 1;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = CheckAccount();

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
            }
        }

        void UpdateLabel()
        {
            switch (_type)
            {
                case 0:
                    What.Text = "Архив  ";
                    break;
                case 1:
                    What.Text = "Не закрытые события (все) на " + DateTime.Now.Date.ToShortDateString();
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
            }
        }

        List<Data> GetData()
        {
            List<Data> data = new List<Data>();
            string url = "~/ZayavkaCloseODS.aspx?zId=";
            if (_role == "ODS")
                url = "~/ZayavkaCloseODS.aspx?zId=";
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
                 //   cmd.Parameters.AddWithValue("beg", Beg.SelectedDate);
                 //   cmd.Parameters.AddWithValue("end", End.SelectedDate);
                    if (_role == "ODS") cmd.Parameters.AddWithValue("user", User.Identity.Name);
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
                        d2 = ((DateTime)dr["Finish"]).Date.ToString();
                        t2 = ((DateTime)dr["Finish"]).TimeOfDay.ToString();
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
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        From = dr["FromFamily"].ToString() + " " + dr["FromIO"].ToString(),
                        Date1 = ((DateTime)dr["Start"]).Date.ToShortDateString(),
                        Time1 = ((DateTime)dr["Start"]).ToShortTimeString(),
                        Category = dr["Category"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
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

            List<string> roles = new List<string>() { "ODS", "Cadry"};
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
           // Period.Visible = false;
          //  phReport.Visible = true;
        }
    }
}