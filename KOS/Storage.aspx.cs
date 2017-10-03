using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using KOS.App_Code;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Net;

namespace KOS
{
    public partial class Storage : System.Web.UI.Page
    {
        class Data 
         {
            public int Id { get; set; }
            public string Url { get; set; }
            public string DataPost { get; set; }
            public string NumDoc { get; set; } 
            public string NumSklada { get; set; }
            public string Zakreplen { get; set; }
            public string Name { get; set; }
            public string Obz { get; set; }
            public string NumID { get; set; }
            public string TheNum { get; set; }
            public string Price { get; set; }
            public string Source { get; set; }
            public string DataSpisaniya { get; set; }
            public string NumDocSpisan { get; set; }
            public string TheNumSpisan { get; set; }
            public string Prim { get; set; }
            public string Ostatok { get; set; }
            public string Prinyal { get; set; }
 
        }
        string s;
        string _role;
        List<Base.PersonData> _workers = new List<Base.PersonData>();
        protected void Page_Load(object sender, EventArgs e)
        {
             _role = CheckAccount();
             if (_role == "Worker")
             {
                 phPost.Visible = false;
                 if (!IsPostBack)
                 {
                     App_Code.Base db1 = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                     List<string> _wrk = new List<string>();
                     _wrk = db1.GetIdUMbyName(User.Identity.Name);
                     DdlSklad.DataSource = _wrk;
                     DdlSklad.DataBind();
                 }
                 SkladVvod.Text = DdlSklad.SelectedValue;
                 App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                 _workers = db.GetWorkers();
                 if (!IsPostBack)
                 {
                     DdlSklad1.DataSource = _workers;
                     DdlSklad1.DataBind();
                     DdlSklad1.SelectedIndex = 0;
                 }
             }
            else
             {
                 if (!IsPostBack)
                 {
                     List<string> dt = new List<string>() { "", "1/1", "1/2", "1/3", "1/4", "2/1", "2/2", "2/3", "2/4", "2/5", "4/1", "4/2", "все" };
                     DdlSklad.DataSource = dt;
                     DdlSklad.DataBind();
                     SkladVvod.Text = DdlSklad.SelectedValue;
                 }
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                _workers = db.GetWorkers();
                if (!IsPostBack)
                {
                    DdlSklad1.DataSource = _workers;
                    DdlSklad1.DataBind();
                    DdlSklad1.SelectedIndex = 0;
                }

            }
           
        }
   
          string CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "ODS_tsg" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return "ODS_tsg";
            roles = new List<string>() { "Administrator" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Administrator";
            roles = new List<string>() { "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "ManagerTSG";
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Manager";
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Cadry";
            roles = new List<string>() { "Worker" };
            if (db.CheckAccount(User.Identity.Name, roles)) return "Worker";
            Response.Redirect("~/About.aspx");
            return "";
        }
        protected void Sklsdd_Click(object sender, EventArgs e)
        {
            phView.Visible = true;
            List<Data> datas = GetData1();
            Out.DataSource = datas;
            Out.DataBind();
        
        }
        List<Data> GetData1()
        {
            
            List<Data> datask = new List<Data>();
            string url = "~/EquView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                if (DdlSklad.SelectedValue != "все")
                    s = "select s.Id, s.DataPost, s.NumDoc, NumSklada, s.Zakreplen, s.Name, s.NumID, s.TheNum, s.Price, s.Source, s.DataSpisaniya, s.NumDocSpisan, s.TheNumSpisan, s.Prim, s.Ostatok, s.Obz, s.Prinyal from Sklady s " +
                       "where s.NumSklada=@num and s.Ostatok!='0'";
                 else if (DdlSklad.SelectedValue == "все")
                    s = "select s.Id, s.DataPost, s.NumDoc, NumSklada, s.Zakreplen, s.Name, s.NumID, s.TheNum, s.Price, s.Source, s.DataSpisaniya, s.NumDocSpisan, s.TheNumSpisan, s.Prim, s.Ostatok, s.Obz, s.Prinyal from Sklady s " +
                        "where s.Ostatok!='0'";
                SqlCommand cmd = new SqlCommand(s, conn);
                if (DdlSklad.SelectedValue != "все" )
                cmd.Parameters.AddWithValue("num", DdlSklad.SelectedValue);               
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datask.Add(new Data()
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Url = ( url + dr["Id"].ToString()),
                        DataPost = dr["DataPost"].ToString(),
                        NumDoc = dr["NumDoc"].ToString(),
                        NumSklada = dr["NumSklada"].ToString(),
                        Zakreplen = dr["Zakreplen"].ToString(),
                        Name = dr["Name"].ToString(),                       
                        NumID = dr["NumID"].ToString(),
                        Obz = dr["Obz"].ToString(),
                        TheNum = dr["TheNum"].ToString(),
                        Price = dr["Price"].ToString(),
                        Source = dr["Source"].ToString(),
                        DataSpisaniya = dr["DataSpisaniya"].ToString(),
                        NumDocSpisan = dr["NumDocSpisan"].ToString(),
                        TheNumSpisan = dr["TheNumSpisan"].ToString(),
                        Prim = dr["Prim"].ToString(),
                        Ostatok = dr["Ostatok"].ToString(),
                        Prinyal = dr["Prinyal"].ToString()
                    });
                }
                dr.Close();
            }
            return datask;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _workers = db.GetWorkers();
            if (!IsPostBack)
            {
                DdlSklad1.DataSource = _workers;
                DdlSklad1.DataBind();
                DdlSklad1.SelectedIndex = 0;
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Sklady (DataPost, NumDoc, NumSklada, Zakreplen, Name, NumID, TheNum, Price, Source, Ostatok, Obz) " +
                    " values (@dp, @nd, @ns, @zk, @nm, @ni, @tn, @pr, @so, @os, @obz )", conn);
                cmd.Parameters.AddWithValue("dp", DateTime.Now);
                cmd.Parameters.AddWithValue("nd", TextBox1.Text);
                cmd.Parameters.AddWithValue("ns", SkladVvod.Text);
                cmd.Parameters.AddWithValue("zk", DdlSklad1.SelectedValue);
                cmd.Parameters.AddWithValue("nm", TextBox2.Text);
                cmd.Parameters.AddWithValue("ni", TextBox3.Text);
                cmd.Parameters.AddWithValue("tn", TextBox4.Text);
                cmd.Parameters.AddWithValue("pr", TextBox5.Text);
                cmd.Parameters.AddWithValue("so", TextBox6.Text);
                cmd.Parameters.AddWithValue("os", TextBox4.Text);
                cmd.Parameters.AddWithValue("obz", TextBox8.Text);
                cmd.ExecuteNonQuery();
                Sklsdd_Click(sender, e);
            }
            TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; TextBox4.Text = ""; TextBox5.Text = ""; TextBox6.Text = ""; TextBox8.Text = "";
        }
        protected void Vvod_Click(object sender, EventArgs e)
        {
            phVvod.Visible = true;
            SkladVvod.Text = DdlSklad.SelectedValue;
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _workers = db.GetWorkers();
            if (!IsPostBack)
            {
                DdlSklad1.DataSource = _workers;
                DdlSklad1.DataBind();
                DdlSklad1.SelectedIndex = 0;
            }

        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            phVvod.Visible = false;
            phView.Visible = false;
        }
    }
}
        
