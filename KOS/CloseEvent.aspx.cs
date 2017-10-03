using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KOS.App_Code;

namespace KOS
{
    public partial class CloseEvent : System.Web.UI.Page 
    {
        int _wz = 0;       
        string _role = string.Empty;
        string _zayav = string.Empty;

        class Data 
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Idz { get; set; }
            public string Idw { get; set; } 
            public string Name { get; set; }
            public string namefoto { get; set; }
            public string NumID { get; set; }
            public string Kol { get; set; }  
            public string NameFile { get; set; }
            public string Status { get; set; }
            public string Prim { get; set; }  
            public string Usr { get; set; }
            public string Date { get; set; }
            public string Text { get; set; }
            public string Category { get; set; }
            public string UserName { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Comment { get; set; }
            public string PrimHist { get; set; }
   
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
                IdU.Text = data.Rows[0]["IdU"].ToString();
                IdM.Text = data.Rows[0]["IdM"].ToString();
                LiftId.Text = data.Rows[0]["LiftId"].ToString();
                Sourse.Text = data.Rows[0]["Sourse"].ToString();
                ToApp.Text = data.Rows[0]["ToApp"].ToString();
                TypeId.Text = data.Rows[0]["TypeId"].ToString();
                DateToApp.Text = data.Rows[0]["DateToApp"].ToString();
                DateWho.Text = data.Rows[0]["DateWho"].ToString();
                Who.Text = data.Rows[0]["Who"].ToString();
                Comment.Text = data.Rows[0]["Comment"].ToString();
                Prim.Text = data.Rows[0]["Prim"].ToString();
                Idz.Text = data.Rows[0]["ZayavId"].ToString();
                Idw.Text = data.Rows[0]["WZayavId"].ToString();
             //   NameFoto.Text = data.Rows[0]["namefoto"].ToString();
             //   Name.Text = data.Rows[0]["Name"].ToString();
             //   NumID.Text = data.Rows[0]["NumID"].ToString();
              //тайминг  
                TimeSpan pr = DateTime.Now - ((DateTime)data.Rows[0]["DataId"]);
                if (!(data.Rows[0]["DateWho"] is DBNull))
                    pr = ((DateTime)data.Rows[0]["DateWho"]) - ((DateTime)data.Rows[0]["DataId"]);
                string prostoy = ((int)pr.TotalDays).ToString() + " " + pr.Hours.ToString() + ":" +
                    pr.Minutes.ToString();
                Timing.Text = prostoy;
             //   DocBase.Text = "Click";
                Sklad.Text = "";
                Spis.Text = "";
               
            }
        }       
        //закрытие события и связанных с ним заявок
                protected void Close_Click(object sender, EventArgs e)
                {
                    if (string.IsNullOrEmpty(Request["zId"]))
                        // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
                        Response.Redirect("~/About.aspx");
                    _wz = Int32.Parse(Request["zId"]);
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        DateTime date = DateTime.Now;
                        SqlCommand cmd = new SqlCommand("update Events " +
                         "set Cansel=@c, DateCansel=@d where Id=@i", conn);
                        cmd.Parameters.AddWithValue("d", date);
                        cmd.Parameters.AddWithValue("c", true);
                        cmd.Parameters.AddWithValue("i", _wz);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        DateTime date = DateTime.Now;
                        if (Idz.Text != "")
                        {
                            int z = int.Parse(Idz.Text);
                            SqlCommand cmd = new SqlCommand("update Zayavky " +
                             "set Worker=(select UserId from Users where UserName=@user), Finish=@f, [Status]=@s, Couse=@c where Id=@i", conn);
                            cmd.Parameters.AddWithValue("i", z);
                            cmd.Parameters.AddWithValue("user", User.Identity.Name); //
                            cmd.Parameters.AddWithValue("f", date);
                            cmd.Parameters.AddWithValue("s", true);
                            cmd.Parameters.AddWithValue("c", "закрыто: " + User.Identity.Name);
                            cmd.ExecuteNonQuery();
                        }
                        if (Idw.Text != "")
                        {
                            int w = int.Parse(Idw.Text);
                            SqlCommand cmd = new SqlCommand("update WorkerZayavky " +
                            "set [Done]=1, WhoDone=(select UserId from Users where UserName=@user), DoneDate=@d, [Readed]=1 where Id=@id", conn);
                            cmd.Parameters.AddWithValue("id", w);
                            cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            cmd.Parameters.AddWithValue("d", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("~/Reg_wz.aspx");
                }
        protected void Button1_Click(object sender, EventArgs e) 
        {
            Response.Redirect("~/Reg_wz.aspx");
        }
        protected void WZClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WZClose.aspx?wz=" + Idw.Text);
        }
        protected void ZayEdit_Click(object sender, EventArgs e)
        {
            if (Idz.Text != "") Response.Redirect("~/ZayavkaEdit.aspx?zId=" + Idz.Text);
            else if (Idw.Text != "") Response.Redirect("~/WZClose.aspx?wz=" + Idw.Text);
            else return;
        }
        protected void Hist_Click(object sender, EventArgs e) 
        {
            List<Data> datahist = GetData2();
            ListView2.DataSource = datahist;
            ListView2.DataBind();
            PartL.Visible = false;
            DocEv.Visible = false;
            HistL.Visible = true;
        }
        List<Data> GetData2()
        {
            List<Data> datahist = new List<Data>(); 
          //  string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select he.NumEvent, he.Date, he.Text, he.Category, he.UserName, he.[From], he.[To], he.Comment, he.PrimHist from HistEv he " +
                "where he.NumEvent=@id", conn);
                cmd.Parameters.AddWithValue("id", _wz);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datahist.Add(new Data()
                    {
                        Date = (dr["Date"].ToString()),
                        Text = dr["Text"].ToString(),
                        Category = dr["Category"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        From = dr["From"].ToString(),
                        To = dr["To"].ToString(),
                        Comment = dr["Comment"].ToString(),
                        PrimHist = dr["PrimHist"].ToString()
                    });
                }
                dr.Close();
            }
            return datahist;
        }
        protected void PartList_Click(object sender, EventArgs e)
        {
            List<Data> datapart = GetData1();
            ListView1.DataSource = datapart;
            ListView1.DataBind();
            PartL.Visible = true;
            DocEv.Visible = false;
            HistL.Visible = false;
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
                        Id = (dr["Id"].ToString()),
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
            HistL.Visible = false;
            DocEv.Visible = true;
        }
           List<Data> GetData()
        {
            List<Data> datadoc = new List<Data>();
            string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.NameFile, d.Status, d.Prim, d.Usr from Documents d " +
                "where d.NumEvent=@id",conn);
                cmd.Parameters.AddWithValue("id", _wz);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datadoc.Add(new Data()
                    {
                        Id = (dr["Id"].ToString()),
                        Url = ( url + dr["Id"].ToString()),
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