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
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace KOS
{
    public partial class DiagrammODS : System.Web.UI.Page
    {
        string fam, io, u, m;
        
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
        string _role;

        protected void Page_Load(object sender, EventArgs e)
        {
            _role = "ODS";
            

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd;
                if (_role == "ODS")
                {
                    cmd = new SqlCommand("select tt.Ttx, tt.Id from Ttx tt " +
                        "join LiftsTtx lt on lt.TtxId=tt.Id " +
                        "join ODSLifts o on o.LiftId=lt.LiftId " +
                        "join Users u on u.UserId=o.UserId " +
                        "where tt.TtxTitleId=1 and u.UserName=@userName " +
                        "group by tt.Ttx, tt.Id", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                }
                else
                {
                    cmd = new SqlCommand("select tt.Ttx, tt.Id from Ttx tt " +
                        "where tt.TtxTitleId=1 " +
                        "group by tt.Ttx, tt.Id", conn);
                }

                SqlDataReader dr = cmd.ExecuteReader();
                
                while (dr.Read())
                    addresses.Add(new ListData() { Title = dr[0].ToString(), Id = Int32.Parse(dr[1].ToString()) });
                dr.Close();
                if (!IsPostBack)
                {
                    Address.DataSource = addresses;
                    Address.DataBind();
                    Address.SelectedIndex = 0;

                    Address_TextChanged(this, EventArgs.Empty);

                    List<string> category = new List<string>() {" ", "застревание", "останов", "заявка", "плановые работы" , "внеплановые ремонты", "ПНР/РЭО" };
                    if (_role == "ODS" || _role == "Worker")
                    {
                        Category.DataSource = category;
                        Category.DataBind();
                    }
                    else
                        Cat.Visible = false;
                    //Отправил:
                    
                    cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                        "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                        Disp.Text = dr[0].ToString() + " " + dr[1].ToString();
                    dr.Close();
                   
                }
            }
        }

        protected void Address_TextChanged(object sender, EventArgs e)
        {
            if (Address.SelectedItem == null)
                return;
            string a = Address.SelectedItem.Value;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();

                SqlCommand cmd;
                if (_role == "ODS")
                {
                    cmd = new SqlCommand("select lt.LiftId from LiftsTtx lt " +
                    "join ODSLifts o on o.LiftId=lt.LiftId " +
                    "join Users u on u.UserId=o.UserId " +
                    "join Ttx tt on tt.Id=lt.TtxId " +
                    "where tt.TtxTitleId=1 and u.UserName=@userName and tt.Ttx=@t", conn);
                    cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                }
                else
                    cmd = new SqlCommand("select lt.LiftId from LiftsTtx lt " +
                    "join Ttx tt on tt.Id=lt.TtxId " +
                    "where tt.TtxTitleId=1 and tt.Ttx=@t", conn);
                cmd.Parameters.AddWithValue("t", a);
                List<string> lifts = new List<string>();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    lifts.Add(dr[0].ToString());
                dr.Close();
                Lift.DataSource = lifts;
                Lift.DataBind();
                Lift.SelectedIndex = 0;
              
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
           
            ListData ttx = addresses.Find(delegate(ListData i)
            {
                return (i.Title == Address.SelectedValue);
            });
            if (ttx == null)
                return;
            int ttxId = ttx.Id;
            if (Category.Text == " ")
            {
                Msg.Text = "Внимание! Вы забыли выбрать вид работ.";
                return;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand("insert into Zayavky " +
                    "(TtxId, LiftId, UserId, [Text], Category, [From], [Start]) " +
                    "values (@ttxId, @liftId, (select UserId from Users where UserName=@user), @text, @c, @f, @s)", conn);
                cmd.Parameters.AddWithValue("ttxId", ttxId);
                cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("text", Text.Text);
                if (_role == "ODS")
                    cmd.Parameters.AddWithValue("c", Category.SelectedValue);
                else
                    cmd.Parameters.AddWithValue("c", "заявка");
                if (_role == "ODS")
                    cmd.Parameters.AddWithValue("f", "ОДС");
                else
                    cmd.Parameters.AddWithValue("f", "менеджер");
                cmd.Parameters.AddWithValue("s", date);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("select z.Id from Zayavky z " +
                    "join Users u on z.UserId=u.UserId " +
                    "where u.UserName=@user and z.LiftId=@liftId and [Start]=@s", conn);
                cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                cmd.Parameters.AddWithValue("s", date);
                int id = int.Parse(cmd.ExecuteScalar().ToString());
                //Блок записи в базу событий

                cmd = new SqlCommand("select ui.Family, ui.IO from UserInfo ui " +
                         "join Users u on u.UserId=ui.UserId where u.UserName=@userName", conn);
                cmd.Parameters.AddWithValue("userName", User.Identity.Name);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    fam = dr[0].ToString(); io = dr[1].ToString();
                dr.Close();
                cmd = new SqlCommand("select LiftId, IdU, IdM from Lifts " +
                   "where LiftId=@lift", conn);
                cmd.Parameters.AddWithValue("lift", Lift.SelectedValue);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { u = dr[1].ToString(); m = dr[2].ToString(); }
                dr.Close();
                cmd = new SqlCommand("insert into Events" +
                 "(EventId, RegistrId, DataId, ZayavId, Sourse, Family, IO, TypeId, IdU, IdM, LiftId, Address) " +
                 "values (@text, @reg, @s, @id, @f, @fam, @io, @c, @u, @m, @liftid, @adr)", conn);
                cmd.Parameters.AddWithValue("text", Text.Text);
                cmd.Parameters.AddWithValue("reg", "Эксплуатация лифтов");
                cmd.Parameters.AddWithValue("s", date);
                cmd.Parameters.AddWithValue("id", id);
                if (_role == "ODS")
                    cmd.Parameters.AddWithValue("f", "оператор ОДС");
                else
                    cmd.Parameters.AddWithValue("f", "менеджер");
                cmd.Parameters.AddWithValue("fam", fam);
                cmd.Parameters.AddWithValue("io", io);
                cmd.Parameters.AddWithValue("c", Category.SelectedValue);
                cmd.Parameters.AddWithValue("u", u);
                cmd.Parameters.AddWithValue("m", m);
                cmd.Parameters.AddWithValue("liftId", Lift.SelectedValue);
                cmd.Parameters.AddWithValue("adr", Address.SelectedValue);
                cmd.ExecuteNonQuery();
               
               
                KOS.App_Code.Mail mail = new Mail(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                mail.SendMsg(Lift.SelectedValue, int.Parse(id.ToString()));
          //     SMail.SendMail("smtp.office365.ru","office@emicatech.com", "pass","avikulov@emicatech.com", "фото", "Фото в присоединенном файле.", "C:\\temp\\uploads\\1.jpg");
       //sms response
                string wn = "";
                string str = User.Identity.Name;
                if (str == "ODS11" || str == "ODS12" || str == "ODS15") wn = "9253135718";
                else if (str == "ODS21" || str == "ODS22" || str == "ODS23" || str == "ODS31" || str == "ODS32") wn = "9264610904";
                else if (str == "ODS13") wn = "9268976775";
                else if (str == "ODS14") wn = "9253135718";
                else if (str == "ODS41" || str == "ODS42") wn = "9629908871";
                //else if (str == "Cadry" || str == "Emica") wn = "9624062614";
                string nomer = wn;
                string TextSms = Category.SelectedValue + "-адрес:" + Address.SelectedItem.Value + "-" + "лифт№:" + Lift.SelectedValue + "-" + Text.Text + "-отправил: " + User.Identity.Name;
                string myApiKey = "27B482E1-14AE-ACFB-C500-CCEC9C763C99"; //Ваш API ключ
                SmsRu.SmsRu sms = new SmsRu.SmsRu(myApiKey);// Основная рассылка
                var response = sms.Send(nomer, TextSms);
 /*             string wn2 = "";
                if (str == "ODS11" || str == "ODS12" || str == "ODS15") wn2 = "9684495099";
                else if (str == "ODS21" || str == "ODS22" || str == "ODS23" || str == "ODS24" || str == "ODS31" || str == "ODS32") wn2 = "9296758809";
                else if (str == "ODS13") wn2 = "9689534417";
                else if (str == "ODS14") wn2 = "9684495099";
                else if (str == "ODS41" || str == "ODS42") wn2 = "9267270995";
                string nomer2 = wn2;
                string TextSms2 = "#" + Category.SelectedValue + "-адрес:" + Address.SelectedItem.Value + "-" + "лифт№:" + Lift.SelectedValue + "-" + Text.Text + "-отправил: " + User.Identity.Name;
                SmsRu.SmsRu sms2 = new SmsRu.SmsRu(myApiKey);// Дублирующая рассылка
                var response2 = sms2.Send(nomer2, TextSms2);
*/
                      Msg.Text = "Регистрация выполнена, отправлено сообщение на e-mail менеджеру и СМС Дежурному механику. Для связи с Дежурной службой нажмите кнопку сайтофона.";

                      Response.Redirect("~/Reg_ods.aspx");
            }
        } // конец блока регистрации *****
       
    }
}


