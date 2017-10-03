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
    public partial class Journal : System.Web.UI.Page
    {
        int _id = -1;
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Begin { get; set; }
            public string Role { get; set; }
            public string Page { get; set; }
            public string Description { get; set; }
            public string End { get; set; }
            public string Prim { get; set; }
        }
        List<App_Code.Base.User> _data;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                _id = Int32.Parse(Request["id"]);
                phAddRecord.Visible = false;
                phEditRecord.Visible = true;
            }
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _data = db.GetUsers();
            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            if (!db.CheckAccount(User.Identity.Name, roles))
            {
                Select.SelectCommand = "select j.*, uf.Family+' '+uf.IO as FromFIO, ut.Family+' '+ut.IO as ToFIO, '~/Journal.aspx?id='+CAST(j.Id as nvarchar) as Url " +
                    "from [Journal] j left join UserInfo uf on uf.UserId=j.[From] left join UserInfo ut on ut.UserId=j.[To] " +
                    "join Users u1 on u1.UserId=j.[From] or j.[From] is null " +
                    "join Users u2 on u2.UserId=j.[To] or j.[To] is null " +
                    "where (j.[From] is not null and u1.UserName=N'" + User.Identity.Name +  "') " +
                    "or (j.[To] is not null and u2.UserName=N'" + User.Identity.Name + "') " +
                    "order by j.[Begin] desc";
            }
            if (!IsPostBack)
            {
                AddTo.DataSource = _data;
                AddTo.DataBind();
                AddTo.SelectedIndex = 0;
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    /*
                    SqlCommand cmd = new SqlCommand("select * from [Journal] order by [Begin] desc", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Data> data = new List<Data>();
                    while (dr.Read())
                    {
                        data.Add(new Data()
                        {
                            Id = dr["Id"].ToString(),
                            Url = "~/Journal.aspx?id=" + dr["Id"].ToString(),
                            Role = dr["Role"].ToString(),
                            Page = dr["Page"].ToString(),
                            Description = dr["Description"].ToString(),
                            Begin = ((DateTime)dr["Begin"]).Date.ToShortDateString(),
                            End = dr["End"] is DBNull ? "" : ((DateTime)dr["End"]).Date.ToShortDateString(),
                            Prim = dr["Prim"] is DBNull ? "" : dr["Prim"].ToString()
                        });
                    }
                    dr.Close();
                    Table.DataSource = data;
                    Table.DataBind();
                     */
                    if (_id > 0)
                    {
                        SqlCommand cmd = new SqlCommand("select j.*, uif.Family as FromF, uif.IO as FromIO, uit.Family as ToF, uit.IO as ToIO from [Journal] j " +
                            "left join UserInfo uit on uit.UserId=j.[To] " +
                            "left join UserInfo uif on uif.UserId=j.[From] where j.Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        EditRole.Text = dt.Rows[0]["Role"].ToString();
                        EditPage.Text = dt.Rows[0]["Page"].ToString();
                        EditDescription.Text = dt.Rows[0]["Description"].ToString();
                        if (!(dt.Rows[0]["Prim"] is DBNull))
                            EditPrim.Text = dt.Rows[0]["Prim"].ToString();
                        if (!(dt.Rows[0]["FromF"] is DBNull))
                            EditFrom.Text = dt.Rows[0]["FromF"].ToString() + " " + dt.Rows[0]["FromIO"].ToString();
                        if (!(dt.Rows[0]["ToF"] is DBNull))
                            EditTo.Text = dt.Rows[0]["ToF"].ToString() + " " + dt.Rows[0]["ToIO"].ToString();
                    }
                }
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Journal.aspx");

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry" };
            if (!db.CheckAccount(User.Identity.Name, roles))
                Delete.Visible = false;
            roles = new List<string>() { "Administrator", "Manager", "Worker", "Electronick", "Worker", "Cadry", "ODS", "ODS_tsg", "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            phAddRecord.Visible = true;
            phEditRecord.Visible = false;
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [Journal] " +
                    "([Begin], Role, Page, Description, [From], [To], Foto, NameFoto) " +
                    "values (@b, @r, @p, @d, @f, @t, @img, @namefile)", conn);
                cmd.Parameters.AddWithValue("b", DateTime.Now);
                cmd.Parameters.AddWithValue("r", AddRole.Text);               
                cmd.Parameters.AddWithValue("d", AddDescription.Text);
                App_Code.Base.User u = _data.Find(delegate(App_Code.Base.User i)
                {
                    return i.Fio == AddTo.SelectedValue;
                });
                cmd.Parameters.AddWithValue("t", u.UserId);
                u = _data.Find(delegate(App_Code.Base.User i)
                {
                    return i.UserName == User.Identity.Name;
                });
                cmd.Parameters.AddWithValue("f", u.UserId);
                //выбор из устройства
                string namePhoto = FileUpload1.FileName;
                //преобразование в двоичный код
                byte[] photo = FileUpload1.FileBytes;               
                //Запись документа и его имени файла в БД
                cmd.Parameters.Add("img", SqlDbType.Image).Value = photo;
                cmd.Parameters.Add("namefile", SqlDbType.NVarChar).Value = namePhoto;
                cmd.Parameters.AddWithValue("p", namePhoto);   //AddPage.Text
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Journal.aspx");
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (_id < 1) return;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update [Journal] set Role=@r, Page=@p, Description=@d, [End]=@e, Prim=@prim where Id=@id", conn);
                cmd.Parameters.AddWithValue("r", EditRole.Text);
                cmd.Parameters.AddWithValue("p", EditPage.Text);
                cmd.Parameters.AddWithValue("d", EditDescription.Text);
                cmd.Parameters.AddWithValue("e", DateTime.Now);
                cmd.Parameters.AddWithValue("prim", EditPrim.Text);
                cmd.Parameters.AddWithValue("id", _id);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Journal.aspx");
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (_id < 1) return;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from [Journal] where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _id);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Journal.aspx");
            }
        }
        //запрос к базе для просмотра Foto
        protected void Foto_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select j.[Foto], j.NameFoto from [Journal] j " +
                    "where j.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _id);
                //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                try
            {
                    string namefoto = datareader["NameFoto"].ToString();
                int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(0, 0, bBuffer, 0, bLength);
                //  просмотр в браузере
                Response.ContentType = "image"; //image/Pdf 
                Response.BinaryWrite(bBuffer);
                string pdfFileName = Request.PhysicalApplicationPath;
                Response.ContentType = "application/x-download";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", namefoto));                
                Response.Flush();
                Response.End();
             }
                catch { Msg.Text = "К этому сообщению файл не прикреплён!"; }
            }

        }
        //запрос для скачивания Foto
        protected void Donl_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select j.[Foto], j.NameFoto from [Journal] j " +
                    "where j.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _id);
                //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                     try
            {
                int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(0, 0, bBuffer, 0, bLength);
                //  просмотр в браузере
                Response.ContentType = "image"; //image/Pdf 
                Response.BinaryWrite(bBuffer);
                Response.Flush();
                Response.End();
            }
                     catch { Msg.Text = " К этому сообщению файл не прикреплён!"; }
            }


        }
    }
}