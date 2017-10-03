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
    public partial class ZReport : System.Web.UI.Page
    {
        class Data
        {
            public string Url { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public string Id { get; set; }
        }
        int _type = 0;
        string _role = string.Empty;
        DateTime _date = DateTime.Now.AddDays(-2);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["t"]))
                _type = Int32.Parse(Request["t"]);
            _role  = CheckAccount();

            if (!IsPostBack)
            {
                List<Data> data = new List<Data>();
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd;
                    SqlDataReader dr;
                    string url = "~/ZayavkaEdit.aspx?zId=";
                    if (_role == "ODS")
                        url = "~/ZayavkaView.aspx?zId=";
                    if (_role == "Cadry")
                        url = "~/ZayavkaEdit.aspx?zId=";

                    switch (_type)
                    {
                        case 1:
                            ReportTitle.Text = "Застревания с " + _date.ToString();
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                    "join UserInfo ui on ui.UserId=z.UserId " +
                                    "where z.Category=N'застревание' and z.Start>@date", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                   "join UserInfo ui on ui.UserId=z.UserId " +
                                   "join Users u on z.UserId=u.UserId " +
                                   "where z.Category=N'застревание' and z.Start>@date and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            cmd.Parameters.AddWithValue("date", _date);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " + 
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 2:
                            ReportTitle.Text = "Не закрытые застревания";
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "where z.Category=N'застревание' and z.Finish is null", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                   "join UserInfo ui on ui.UserId=z.UserId " +
                                   "join Users u on z.UserId=u.UserId " +
                                   "where  z.Category=N'застревание' and z.Finish is null and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 3:
                            ReportTitle.Text = "Остановы с " + _date.ToString();
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "where z.Category=N'останов' and z.Start>@date", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'останов' and z.Start>@date and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            cmd.Parameters.AddWithValue("date", _date);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 4:
                            ReportTitle.Text = "Не закрытые остановы";
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "where z.Category=N'останов' and z.Finish is null", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'останов' and z.Finish is null and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 5:
                            ReportTitle.Text = "Заявки от заказчика с " + _date.ToString();
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "where z.Category=N'заявка' and z.Start>@date", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'заявка' and z.Start>@date and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            cmd.Parameters.AddWithValue("date", _date);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 6:
                            ReportTitle.Text = "Не закрытые заявки от заказчика";
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "where z.Category=N'заявка' and z.Finish is null", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'заявка' and z.Finish is null and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 7:
                            ReportTitle.Text = "Заявки от менеджера с " + _date.ToString();
                            cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='Manager' " +
                                "where z.Category=N'заявка' and z.Start>@date", conn);
                            cmd.Parameters.AddWithValue("date", _date);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 8:
                            ReportTitle.Text = "Не закрытые заявки от менеджера";
                            cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='Manager' " +
                                "where z.Category=N'заявка' and z.Finish is null", conn);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 9:
                            ReportTitle.Text = "Заявки от механика с " + _date.ToString();
                            cmd = new SqlCommand("select wz.Id, wz.[Date], ui.Family, ui.IO, wz.Done, wz.[Text], wz.LiftId  from WorkerZayavky wz " +
                                "join UserInfo ui on ui.UserId=wz.UserId " +
                                "where wz.[Date]>@date", conn);
                            cmd.Parameters.AddWithValue("date", _date);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Date"]).ToString(),
                                    Url = (bool.Parse(dr["Done"].ToString()) ? "~/WZView.aspx?zId=" : "~/WZClose.aspx?zId=") +
                                        Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["LiftId"].ToString()
                                });
                            dr.Close();
                            break;
                        case 10:
                            ReportTitle.Text = "Незакрытые заявки от механика";
                            cmd = new SqlCommand("select wz.Id, wz.[Date], ui.Family, ui.IO, wz.Done, wz.[Text], wz.LiftId from WorkerZayavky wz " +
                                "join UserInfo ui on ui.UserId=wz.UserId " +
                                "where wz.Done=0", conn);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Date"]).ToString(),
                                    Url = "~/WZClose.aspx?wz=" + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["LiftId"].ToString()
                                });
                            dr.Close();
                            break;
                        case 11:
                            ReportTitle.Text = "Не закрытые плановые работы от заказчика";
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "where z.Category=N'плановые работы' and z.Finish is null", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'плановые работы' and z.Finish is null and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 12:
                            ReportTitle.Text = "Не закрытые внеплановые ремонты от заказчика";
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "where z.Category=N'внеплановые ремонты' and z.Finish is null", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'внеплановые ремонты' and z.Finish is null and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                        case 13:
                            ReportTitle.Text = "Не закрытые ПНР/РЭО";
                            if (_role != "ODS")
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "where z.Category=N'ПНР/РЭО' and z.Finish is null", conn);
                            else
                            {
                                cmd = new SqlCommand("select z.Id, z.Start, ui.Family, ui.IO, z.[Text]  from Zayavky z " +
                                "join UserInfo ui on ui.UserId=z.UserId " +
                                "join UsersInRoles uir on uir.UserId=z.UserId " +
                                "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                                "join Users u on z.UserId=u.UserId " +
                                "where z.Category=N'ПНР/РЭО' and z.Finish is null and u.UserName=@user", conn);
                                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            }
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                                data.Add(new Data()
                                {
                                    Title = dr["Family"].ToString() + " " + dr["IO"].ToString() + " от: " +
                                        ((DateTime)dr["Start"]).ToString(),
                                    Url = url + Int32.Parse(dr["Id"].ToString()),
                                    Text = dr["Text"].ToString(),
                                    Id = dr["Id"].ToString()
                                });
                            dr.Close();
                            break;
                    }
                }
                List.DataSource = data;
                List.DataBind();
            }
        }

        string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry"};
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "ODS" };
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                if (_type < 7 || _type == 12) //_type == 11,12 добавлено для плановых работ и внеплановые ремонты ОДС
                    return "ODS";
            }
            Response.Redirect("~/About.aspx");
            return string.Empty;
        }
    }
}