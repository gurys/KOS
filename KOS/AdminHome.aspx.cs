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
    public partial class AdminHome : System.Web.UI.Page
    {
         DateTime _from;
        string Text2;
        string Text3;
        string Text4;
        string Text5;
        string Text6;
        string Text7;
        string Text8;


        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            _from = DateTime.Now.AddDays(-2);
         //   Label1.Text = "С " + _from.ToString();
      //      if (!IsPostBack)
      //      CheckAccount();   

     //       _from = DateTime.Now.AddDays(-2);
     //                   Label1.Text = "С " + _from.ToString();
         /*    CurDate.Text = DateTime.Now.Date.ToLongDateString();
               CurHour.Text = DateTime.Now.Hour.ToString();
               CurMinute.Text = DateTime.Now.Minute.ToString(); */

            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                   // все события
                    SqlCommand cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                 //   Text2 = cmd.ExecuteScalar().ToString();
                    HyperLink2.Text = cmd.ExecuteScalar().ToString();
                    // срочные события - 2 дня
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'false' and e.DataId<=@date and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    Text2 = cmd.ExecuteScalar().ToString();
                    HyperLink3.Text = cmd.ExecuteScalar().ToString();
                    // все события без Акта
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.ZaprosMng=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                 //   Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink5.Text = cmd.ExecuteScalar().ToString();
                    // срочные события без Акта
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.ZaprosMng=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    Text3 = cmd.ExecuteScalar().ToString();
                    HyperLink6.Text = cmd.ExecuteScalar().ToString();
                    // все события с запросом manager
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.ZaprosKp=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    // Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink8.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с запросом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.ZaprosKp=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink9.Text = cmd.ExecuteScalar().ToString();
                    // все события с ответом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.KP=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    // Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink11.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ответом КП
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.KP=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    Text5 = cmd.ExecuteScalar().ToString();
                    HyperLink12.Text = cmd.ExecuteScalar().ToString();
                    // все события с запросом счета
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.ZapBill=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    // Text4 = cmd.ExecuteScalar().ToString();
                    HyperLink14.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с запросом счета
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.ZapBill=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                 //   Text6 = cmd.ExecuteScalar().ToString();
                    HyperLink15.Text = cmd.ExecuteScalar().ToString();
                    // все события со  счетами
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Bill=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink17.Text = cmd.ExecuteScalar().ToString();
                    // срочные события со  счетами
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Bill=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink18.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием оплаты
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Payment=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink20.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием оплаты
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Payment=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink21.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием доставки
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Dostavka=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink35.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием доставки
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Dostavka=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink36.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием прихода
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Prihod=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink38.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием прихода
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Prihod=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink39.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием акта ВР
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.AktVR=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink41.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием акта ВР
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.AktVR=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink42.Text = cmd.ExecuteScalar().ToString();
                    // все события с ожиданием списания
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Spisanie=N'true' and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    HyperLink44.Text = cmd.ExecuteScalar().ToString();
                    // срочные события с ожиданием списания
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Spisanie=N'true' and e.DataId<=@date and e.Cansel=N'false' and RegistrId=N'Эксплуатация лифтов'", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    HyperLink45.Text = cmd.ExecuteScalar().ToString();
                 //Блок ОДС ----------------------------------------------------------
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                       "join Users u on z.UserId=u.UserId " +
                       "where z.Category=N'застревание' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text2 = cmd.ExecuteScalar().ToString();
                    //   HyperLink2.Text = cmd.ExecuteScalar().ToString();
                    // плановые работы
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join Users u on z.UserId=u.UserId " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Category=N'плановые работы' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text3 = cmd.ExecuteScalar().ToString();
                    //   HyperLink3.Text = cmd.ExecuteScalar().ToString();
                    // внеплановые ремонты
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join Users u on z.UserId=u.UserId " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "where z.Category=N'внеплановые ремонты' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text8 = cmd.ExecuteScalar().ToString();
                    //   HyperLink8.Text = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join Users u on z.UserId=u.UserId " +
                        "where z.Category=N'останов' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text4 = cmd.ExecuteScalar().ToString();
                    //   HyperLink4.Text = cmd.ExecuteScalar().ToString();
                    //заявки от заказчика
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "join Users u on z.UserId=u.UserId " +
                        "where z.Finish is null and z.Category=N'заявка'and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    //   HyperLink5.Text = cmd.ExecuteScalar().ToString();
                    //отправленные заявки
                    cmd = new SqlCommand("select  count(z.Id) from Zayavky z " +
                               "join UserInfo ui on ui.UserId=z.UserId " +
                               "join UsersInRoles uir on uir.UserId=z.UserId " +
                               "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                               "join Users u on z.UserId=u.UserId " +
                               "where z.Category=N'заявка' and z.Start>@date and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text7 = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='Cadry' " +
                        "join Users u on z.UserId=u.UserId " +
                        "where z.Category=N'заявка' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text6 = cmd.ExecuteScalar().ToString();
                    //   HyperLink6.Text = cmd.ExecuteScalar().ToString();
                    // подсчет активных событий
                    //   int all = int.Parse(HyperLink2.Text) + int.Parse(HyperLink3.Text) + int.Parse(HyperLink4.Text) + int.Parse(HyperLink6.Text)+ int.Parse(HyperLink8.Text);
                    int all = int.Parse(Text2) + int.Parse(Text3) + int.Parse(Text4) + int.Parse(Text6) + int.Parse(Text7) + int.Parse(Text8);
                    HyperLink29.Text = all.ToString();
                    App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    List<App_Code.Base.StoppedLift> list = db.GetStoppedODSLifts(User.Identity.Name);
                    int total = db.GetODSLifts(User.Identity.Name);
                    Worked.Text = (total - list.Count).ToString();
                    Stopped.Text = list.Count.ToString();
                }
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Cadry", "Administrator" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
        protected void ArcButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Arc.aspx");
        }
        protected void WorkerZayavka_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WorkerZayavka.aspx");
        }

        protected void Akt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Akt.aspx");
        }
        protected void ZakrytieODS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ZakrytieODS.aspx");
        }
        protected void Lifts1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reg_tsg.aspx");
        }
        protected void Lifts_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Lifts.aspx");
        }
        protected void Reg_wz_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reg_wz.aspx");
        }
        protected void WebPhone_Click(object sender, EventArgs e)
        {
            phWebPhone.Visible = true;
        }
        
    }
}