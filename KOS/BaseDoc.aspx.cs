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
    public partial class BaseDoc : System.Web.UI.Page
    {
        class Data
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public string IdE { get; set; }
            public string Name { get; set; }
            public string nameFile { get; set; }
            public string Status { get; set; }
            public string PrimDoc { get; set; }
            public string Usr { get; set; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> dt = new List<string>() { "акт дефектовки", "акт установки", "акт повреждения оборудования", "акт выполненных работ", "акт списания", "счет на оплату","все" };
                Ndoc.DataSource = dt;
                Ndoc.DataBind();
                Ndoc.SelectedIndex = 0;

                List<string> dn = new List<string>() { "не подписан заказчиком", "подписан", "отправлен", "получен", "все" };
                Sdoc.DataSource = dn;
                Sdoc.DataBind();
                Sdoc.SelectedIndex = 0;
            }
              
            DateTime date = DateTime.Now;
            Date.Text = DateTime.Now.ToLongDateString();
            Mes1.Visible = false;
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Mes1.Visible = true;         
            Msg1.Text ="";
            Msg.Text = "";
           
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text))
               {
                   Msg.Text = "Внимание! Вы забыли ввести номер события или наименование доумента!";
                   return;
               }
            // запись
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Documents " +
                    "([NumEvent], [Name], [Image], [NameFile], [Status], [Prim])  " +
                    "values (@nev, @name, @img, @namefile, @st, @prim) ", conn);                
                cmd.Parameters.AddWithValue("nev", TextBox1.Text);
                cmd.Parameters.AddWithValue("name", TextBox2.Text);
                cmd.Parameters.AddWithValue("st", TextBox3.Text);
                cmd.Parameters.AddWithValue("prim", TextBox4.Text);
              //  DateTime date; date = DateTime.Now;
              //  cmd.Parameters.AddWithValue("date", date);
                //выбор из устройства
                string namePhoto = FileUpload1.FileName;
                //преобразование в двоичный код
                byte[] photo = FileUpload1.FileBytes;
                if (string.IsNullOrEmpty(namePhoto))
                {
                    Msg.Text = "Внимание! Документ не выбран!";
                    return;
                }
                //Запись документа и его имени файла в БД
                cmd.Parameters.Add("img", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = namePhoto;
                cmd.ExecuteNonQuery();
            }
            Msg1.Text = "Документ записан в БД!"; return;
        }

        protected void PoiskParam_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            phView.Visible = true;
            phPoisk.Visible = false;
            phZap.Visible = false;
            phVse.Visible = false;
            List<Data> datas = GetData1();
            Out.DataSource = datas;
            Out.DataBind();
        }
        List<Data> GetData1()
        {

            List<Data> datask = new List<Data>();
            string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                if (TextBox5.Text == "") { Msg.Text = "Введите № события"; return datask; }
               //     s = "select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.[Prim], d.Usr  from Documents d " +
               //        "where d.NumEvent=@ne and d.Name=@nm and d.Status=@st";
                if (TextBox5.Text != "" && Ndoc.SelectedValue != "все" && Sdoc.SelectedValue != "все")
                {
                    cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.[Prim], d.Usr  from Documents d " +
                           "where d.NumEvent=@ne and d.Name=@nm and d.Status=@st", conn);
                    cmd.Parameters.AddWithValue("ne", TextBox5.Text);
                    cmd.Parameters.AddWithValue("nm", Ndoc.SelectedValue);
                    cmd.Parameters.AddWithValue("st", Sdoc.SelectedValue);
                }
                if (TextBox5.Text != "" && Ndoc.SelectedValue == "все" && Sdoc.SelectedValue == "все")
                {
                    cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.[Prim], d.Usr  from Documents d " +
                           "where d.NumEvent=@ne ", conn);
                    cmd.Parameters.AddWithValue("ne", TextBox5.Text);                   
                }
                if (TextBox5.Text != "" && Ndoc.SelectedValue != "все" && Sdoc.SelectedValue == "все")
                {
                    cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.[Prim], d.Usr  from Documents d " +
                           "where d.NumEvent=@ne and d.Name=@nm", conn);
                    cmd.Parameters.AddWithValue("ne", TextBox5.Text);
                    cmd.Parameters.AddWithValue("nm", Ndoc.SelectedValue);
                }
                if (TextBox5.Text != "" && Ndoc.SelectedValue == "все" && Sdoc.SelectedValue != "все")
                {
                    cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.[Prim], d.Usr  from Documents d " +
                           "where d.NumEvent=@ne and d.Status=@st", conn);
                    cmd.Parameters.AddWithValue("ne", TextBox5.Text);
                    cmd.Parameters.AddWithValue("st", Sdoc.SelectedValue);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datask.Add(new Data()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Url = ( url + dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        IdE = dr["NumEvent"].ToString(),
                        nameFile = dr["NameFile"].ToString(),
                        Status = dr["Status"].ToString(),
                        PrimDoc = dr["Prim"].ToString(),
                        Usr = dr["Usr"].ToString()
                       
                    });
                }
                dr.Close();
            }
            return datask;
        }

        protected void Poisk_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            phPoisk.Visible = true;
            phZap.Visible = false;
            phVse.Visible = false;
        }

        protected void VseDoc_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            phVse.Visible = true;
            phPoisk.Visible = false;
            phZap.Visible = false;
            phView.Visible = false;
        }

        protected void ZapDoc_Click(object sender, EventArgs e)
        {
            Msg.Text = "";
            phZap.Visible = true;
            phVse.Visible = true;
            phPoisk.Visible = false;           
            phView.Visible = false;
        }
    }
}