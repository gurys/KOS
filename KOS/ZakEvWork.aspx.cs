using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KOS.App_Code;

namespace KOS
{
    public partial class ZakEvWork : System.Web.UI.Page
    {
        int _wz = 0;
        string _role = string.Empty;
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string namefoto { get; set; }
            public string NumID { get; set; }
            public string Kol { get; set; }
            public string NameFile { get; set; }
            public string Status { get; set; }
            public string Prim { get; set; }
            public string Usr { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetEvent(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                Id.Text = _wz.ToString();
                EventId.Text = data.Rows[0]["EventId"].ToString();
                DataId.Text = data.Rows[0]["DataId"].ToString();
                //  IdU.Text = data.Rows[0]["IdU"].ToString();
                //  IdM.Text = data.Rows[0]["IdM"].ToString();
                LiftId.Text = data.Rows[0]["LiftId"].ToString();
               //   Sourse.Text = data.Rows[0]["Sourse"].ToString();
               //   ToApp.Text = data.Rows[0]["ToApp"].ToString();
                TypeId.Text = data.Rows[0]["TypeId"].ToString();
               // DateToApp.Text = data.Rows[0]["DateToApp"].ToString();
                DateWho.Text = data.Rows[0]["DateWho"].ToString();
                Who.Text = data.Rows[0]["Who"].ToString();
                Comment.Text = data.Rows[0]["Comment"].ToString();
                Prim.Text = data.Rows[0]["Prim"].ToString();
                //   ZayavId.Text = data.Rows[0]["ZayavId"].ToString();
                Idw.Text = data.Rows[0]["WZayavId"].ToString();
                NameFoto.Text = data.Rows[0]["namefoto"].ToString();
                Name.Text = data.Rows[0]["Name"].ToString();
                NumID.Text = data.Rows[0]["NumID"].ToString();
                //тайминг  
                TimeSpan pr = DateTime.Now - ((DateTime)data.Rows[0]["DataId"]);
                if (!(data.Rows[0]["DateWho"] is DBNull))
                    pr = ((DateTime)data.Rows[0]["DateWho"]) - ((DateTime)data.Rows[0]["DataId"]);
                string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                    pr.Minutes.ToString();
                Timing.Text = prostoy;
                //   DocBase.Text = "Click";
                Sklad.Text = "подр.";
                Spis.Text = "подр.";
                //   DopZap.Text = "Click";
                //   Status.Text = "подр.";

            }
        }
        
        //закрытие
        protected void Close_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);           
            Response.Redirect("~/WZClose.aspx?wz=" + Idw.Text);
       /*     using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DateTime date = DateTime.Now;
                SqlCommand cmd = new SqlCommand("update Events " +
                 "set Cansel=@c, DateCansel=@d where Id=@i", conn);
                cmd.Parameters.AddWithValue("d", date);
                cmd.Parameters.AddWithValue("c", true);
                cmd.Parameters.AddWithValue("i", _wz);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Akt.aspx");
            }
        */
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Akt.aspx");
        }
        protected void Edit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
                // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            try
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    DateTime date = DateTime.Now;
                    string s = "";

                    s = "update Events set EventId=@e, Comment=@c, Prim=@p where Id=@i";

                    SqlCommand cmd = new SqlCommand(s, conn);

                 //   cmd.Parameters.AddWithValue("s", Sourse.Text);
                    cmd.Parameters.AddWithValue("e", EventId.Text);
                    cmd.Parameters.AddWithValue("i", _wz);
                    cmd.Parameters.AddWithValue("c", Comment.Text);
                //    cmd.Parameters.AddWithValue("de", Convert.ToDateTime(DataId.Text));
                    cmd.Parameters.AddWithValue("p", Prim.Text);
                
                    cmd.ExecuteNonQuery();
                    //   Response.Redirect("~/ZakTSG.aspx");
                }
                Msg.Text = "Изменения записаны! Вернуться назад - нажмите на логотип [EMICATECH]";
            }

            catch { Msg.Text = "Поля Дата/время не корректны! Исравьте формат, например (01.01.2017 00:01) "; }
        }

        protected void PartList_Click(object sender, EventArgs e)
        {
            List<Data> datapart = GetData1();
            ListView1.DataSource = datapart;
            ListView1.DataBind();
            PartL.Visible = true;
            DocEv.Visible = false;
        }
        List<Data> GetData1()
        {
            List<Data> datapart = new List<Data>();
            string url = "~/PartView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Id, namefoto, Name, NumID, Kol from PartsList " +
                "where NumEvent=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datapart.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        namefoto = dr["namefoto"].ToString(),
                        NumID = dr["NumID"].ToString(),
                        Kol = dr["Kol"].ToString()

                    });
                }
                dr.Close();
            }
            return datapart;
        }
        protected void DocE_Click(object sender, EventArgs e)
        {
            List<Data> datadoc = GetData();
            Out.DataSource = datadoc;
            Out.DataBind();
            PartL.Visible = false;
            DocEv.Visible = true;
        }
        List<Data> GetData()
        {
            List<Data> datadoc = new List<Data>();
            string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Id, Name, NameFile, Status, Prim, Usr from Documents " +
                "where NumEvent=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datadoc.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "worker" ? "#" : url + dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        NameFile = dr["NameFile"].ToString(),
                        Status = dr["Status"].ToString(),
                        Prim = dr["Prim"].ToString(),
                        Usr = dr["Usr"].ToString()
                    });
                }
                dr.Close();
            }
            return datadoc;
        }

    }
}