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
   
    public partial class OdsHome : System.Web.UI.Page
    {
 
        DateTime _from;
        string Text2;
        string Text3;
        string Text4;
        string Text6;
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
                    /*SqlCommand cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join Users u on z.UserId=u.UserId " +
                        "where z.Category=N'застревание' and z.Start>@date and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("date", _from);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    HyperLink1.Text = cmd.ExecuteScalar().ToString();
                    */
                    SqlCommand cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
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
                 //   HyperLink7.Text = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "join Users u on z.UserId=u.UserId " +
                        "where z.Category=N'заявка' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text6 = cmd.ExecuteScalar().ToString();
                 //   HyperLink6.Text = cmd.ExecuteScalar().ToString();
                    // подсчет активных событий
                    //   int all = int.Parse(HyperLink2.Text) + int.Parse(HyperLink3.Text) + int.Parse(HyperLink4.Text) + int.Parse(HyperLink6.Text)+ int.Parse(HyperLink8.Text);
                    int all = int.Parse(Text2) + int.Parse(Text3) + int.Parse(Text4) + int.Parse(Text6)+ int.Parse(Text8);
                   HyperLink1.Text = all.ToString();
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

            List<string> roles = new List<string>() { "ODS", "Administrator" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

   /*     protected void Zayvka_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Zayavka.aspx");
        }
   */
        protected void ArcButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Arc.aspx");
        }   

        protected void ZakrytieODS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ZakrytieODS.aspx");
        }
        protected void Lifts1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reg_ods.aspx");
        }
    }
}