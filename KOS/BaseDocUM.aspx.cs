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
    public partial class BaseDocUM : System.Web.UI.Page 
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
                List<string> du = new List<string>() { "1", "2", "3", "4"};
                IdU.DataSource = du;
                IdU.DataBind();
                IdU.SelectedIndex = 0;

                List<string> dm = new List<string>() { "1", "2", "3", "4", "5", "6" };
                IdM.DataSource = dm;
                IdM.DataBind();
                IdM.SelectedIndex = 0;

                List<string> dt = new List<string>() {"", "приказ о закреплении", "приказ о ТО", "план-график" };
                Ndoc.DataSource = dt;
                Ndoc.DataBind();
                Ndoc.SelectedIndex = 0;

                List<string> dn = new List<string>() { "", "не подписан", "не подписан заказчиком", "подписан", "отправлен", "получен" };
                Sdoc.DataSource = dn;
                Sdoc.DataBind();
                Sdoc.SelectedIndex = 0;
                TextBox5.Text = "0";
            }

            DateTime date = DateTime.Now;
            Date.Text = DateTime.Now.ToLongDateString();
            Mes1.Visible = false;
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Mes1.Visible = true;
            Msg1.Text = "";
            Msg.Text = "";

            if (string.IsNullOrEmpty(TextBox2.Text))
            {
                Msg.Text = "Внимание! Вы забыли ввести наименование доумента!";
                return;
            }
            // запись
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into DocUM " +
                    "([name], [img], [nameFile], [status], [primm], [usr], [IdU], [IdM])  " +
                    "values ( @name, @img, @namefile, @st, @prim, @usr, @idu, @idm) ", conn);
                cmd.Parameters.AddWithValue("usr", User.Identity.Name);
                cmd.Parameters.AddWithValue("name", TextBox2.Text);
                cmd.Parameters.AddWithValue("st", TextBox3.Text);
                cmd.Parameters.AddWithValue("prim", TextBox4.Text);
                cmd.Parameters.AddWithValue("idu", IdU.Text);
                cmd.Parameters.AddWithValue("idm", IdM.Text);

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
            string url = "~/DocViewUM.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                if (TextBox5.Text == "") { Msg.Text = "Поле № не должно быть пустым!"; return datask; }
                
                if (Ndoc.SelectedValue != "" && Sdoc.SelectedValue != "")
                {
                    cmd = new SqlCommand("select d.Id, d.name, d.namefile, d.status, d.primm, d.usr  from DocUM d " +
                           "where d.name=@nm and d.status=@st", conn);
                    
                    cmd.Parameters.AddWithValue("nm", Ndoc.SelectedValue);
                    cmd.Parameters.AddWithValue("st", Sdoc.SelectedValue);
                }
                if (TextBox5.Text != "" )
                {
                    cmd = new SqlCommand("select d.Id, d.name, d.namefile, d.status, d.primm, d.usr  from DocUM d " +
                           "where d.Id=@ne", conn);
                    cmd.Parameters.AddWithValue("ne", TextBox5.Text);
                }
                if (Ndoc.SelectedValue != "" && Sdoc.SelectedValue == "")
                {
                    cmd = new SqlCommand("select d.Id, d.name, d.namefile, d.status, d.primm, d.usr  from DocUM d " +
                           "where d.Name=@nm", conn);
                //    cmd.Parameters.AddWithValue("ne", TextBox5.Text);
                    cmd.Parameters.AddWithValue("nm", Ndoc.SelectedValue);
                }
                if (Ndoc.SelectedValue == "" && Sdoc.SelectedValue != "")
                {
                    cmd = new SqlCommand("select d.Id, d.name, d.namefile, d.status, d.primm, d.usr  from DocUM d " +
                    "where d.status=@st", conn);                           
                 //   cmd.Parameters.AddWithValue("st", 0);
                    cmd.Parameters.AddWithValue("st", Sdoc.SelectedValue);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datask.Add(new Data()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Url = (url + dr["Id"].ToString()),
                        Name = dr["name"].ToString(),                       
                        nameFile = dr["namefile"].ToString(),
                        Status = dr["status"].ToString(),
                        PrimDoc = dr["primm"].ToString(),
                        Usr = dr["usr"].ToString()

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