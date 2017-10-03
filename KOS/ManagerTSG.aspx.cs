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
    public partial class ManagerTSG : System.Web.UI.Page
    {
        DateTime _from;
        string Text2;
        string Text3;
        string Text4;
        string Text5;
        string Text6; 
       
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            _from = DateTime.Now.AddDays(-2);
              //  CheckAccount();   

            //       _from = DateTime.Now.AddDays(-2);
            

            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    /*
                    cmd = new SqlCommand("select count(z.Id) from Zayavky z " +
                        "join UsersInRoles uir on uir.UserId=z.UserId " +
                        "join Roles r on r.RoleId=uir.RoleId and r.RoleName='ODS' " +
                        "join Users u on z.UserId=u.UserId " +
                        "where z.Category=N'заявка' and z.Finish is null and u.UserName=@user", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    Text6 = cmd.ExecuteScalar().ToString();
                    //   HyperLink6.Text = cmd.ExecuteScalar().ToString();
                     */
                    // Активные события
                    SqlCommand cmd = new SqlCommand("select count(e.Id) from Events e " +
                        "where e.Cansel=N'false' and e.Family=@user", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                   // cmd.Parameters.AddWithValue("user", "ODS13");
                    //   Text2 = cmd.ExecuteScalar().ToString();
                    HyperLink2.Text = cmd.ExecuteScalar().ToString();
                  
                    // Cобытия с замечаниями
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'false' and e.Family=@user and e.Prim!=N' '", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                    HyperLink3.Text = cmd.ExecuteScalar().ToString();
                    //закрытые события с превышением нормативов
                    //Электроснабжение 'авария'
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'true' and e.Family=@user" +
                       " and e.RegistrId=N'Электроснабжение' and e.TypeId=N'авария' and e.[Timing]>=N'0 3:00'", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                    Text2 = cmd.ExecuteScalar().ToString();
                    //Эксплуатация лифтов 'застревание'
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'true' and e.Family=@user" +
                       " and e.RegistrId=N'Эксплуатация лифтов' and e.TypeId=N'застревание' and e.[Timing]>=N'0 0:30'", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                    Text3 = cmd.ExecuteScalar().ToString();
                    //Эксплуатация лифтов 'останов'
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'true' and e.Family=@user" +
                       " and e.RegistrId=N'Эксплуатация лифтов' and e.TypeId=N'останов' and e.[Timing]>=N'0 2:00'", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                    Text4 = cmd.ExecuteScalar().ToString();
                    //Эксплуатация лифтов 'внеплановые ремонты'
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'true' and e.Family=@user" +
                       " and e.RegistrId=N'Эксплуатация лифтов' and e.TypeId=N'внеплановые ремонты' and e.[Timing]>=N'3 0:00'", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                    Text5 = cmd.ExecuteScalar().ToString();
                    //Кроме Эксплуатация лифтов 
                    cmd = new SqlCommand("select count(e.Id) from Events e " +
                       "where e.Cansel=N'true' and e.Family=@user" +
                       " and e.RegistrId!=N'Эксплуатация лифтов' and e.[Timing]>=N'1 0:00'", conn);
                    if (User.Identity.Name == "Миракс_Парк")
                        cmd.Parameters.AddWithValue("user", "ODS13");
                    else if (User.Identity.Name == "Корона_1")
                        cmd.Parameters.AddWithValue("user", "ODS14");
                    else if (User.Identity.Name == "Весна")
                        cmd.Parameters.AddWithValue("user", "ODS41");
                    else if (User.Identity.Name == "TSG_test")
                        cmd.Parameters.AddWithValue("user", "ODS_test");
                    Text6 = cmd.ExecuteScalar().ToString(); 
                    // подсчет превышения нормативов событий
                    //   int all = int.Parse(HyperLink2.Text) + int.Parse(HyperLink3.Text) + int.Parse(HyperLink4.Text) + int.Parse(HyperLink6.Text)+ int.Parse(HyperLink8.Text);
                      int all = int.Parse(Text2) + int.Parse(Text3) + int.Parse(Text4) + int.Parse(Text5) + int.Parse(Text6);
                      HyperLink1.Text = all.ToString();
                    //App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    //List<App_Code.Base.StoppedLift> list = db.GetStoppedODSLifts(User.Identity.Name);
                    //int total = db.GetODSLifts(User.Identity.Name);
                    //Worked.Text = (total - list.Count).ToString();
                    //Stopped.Text = list.Count.ToString();
                }
            }
        }


        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "ManagerTSG", "ODS_tsg", "Administrator" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
       
        protected void ArcButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReportsNormTSG.aspx");
        }

        protected void PrimTSG_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/PrimTSG.aspx");
        }
        protected void EventsTSG_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/Reg_tsg.aspx");
        }
        protected void ReportsTSG_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/ReportsTSG.aspx");
        }
        protected void AdminTSG_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminTSG.aspx");
        }
    }
}