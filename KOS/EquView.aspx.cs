using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using KOS.App_Code;

namespace KOS
{
    public partial class EquView : System.Web.UI.Page
    {
        
           int _wz = 0;
           List<Base.PersonData> _workers = new List<Base.PersonData>();
        protected void Page_Load(object sender, EventArgs e)
        {
          if (string.IsNullOrEmpty(Request["zId"]))
           // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
             Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (!IsPostBack)
            {
               
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetSklad(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
            //    Id.Text = _wz.ToString();
                 
                        DataPost.Text = data.Rows[0]["DataPost"].ToString();
                        NumDoc.Text = data.Rows[0]["NumDoc"].ToString();
                        NumSklada.Text = data.Rows[0]["NumSklada"].ToString();
                        Zakreplen.Text = data.Rows[0]["Zakreplen"].ToString();
                        Prinyal.Text = data.Rows[0]["Prinyal"].ToString();
                        Name.Text = data.Rows[0]["Name"].ToString();
                        Obz.Text = data.Rows[0]["Obz"].ToString();
                        NumID.Text = data.Rows[0]["NumID"].ToString();
                        TheNum.Text = data.Rows[0]["TheNum"].ToString();
                        Price.Text = data.Rows[0]["Price"].ToString();
                        Source.Text = data.Rows[0]["Source"].ToString();
                        DataSpisaniya.Text = data.Rows[0]["DataSpisaniya"].ToString();
                        NumDocSpisan.Text = data.Rows[0]["NumDocSpisan"].ToString();
                        TheNumSpisan.Text = data.Rows[0]["TheNumSpisan"].ToString();
                        PrimZ.Text = data.Rows[0]["Prim"].ToString();
                        Ostatok.Text = data.Rows[0]["Ostatok"].ToString();
             }
            if (!IsPostBack)
            {
                List<string> dt = new List<string>() { "", "1/1", "1/2", "1/3", "1/4", "2/1", "2/2", "2/3", "2/4", "2/5", "4/1", "4/2" };
                NumSklad.DataSource = dt;
                NumSklad.DataBind();
              //  SkladVvod.Text = DdlSklad.SelectedValue;
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                _workers = db.GetWorkers();
                if (!IsPostBack)
                {
                    WorkSklad.DataSource = _workers;
                    WorkSklad.DataBind();
                    WorkSklad.SelectedIndex = 0;
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Sklady set DataSpisaniya=@dat, NumDocSpisan=@ndoc, TheNumSpisan=@kol, Prim=@pr, Ostatok=@ost " +
                    " where Id=@wz", conn);
                cmd.Parameters.AddWithValue("dat", DateTime.Now);
                cmd.Parameters.AddWithValue("ndoc", NumDocSpisan.Text);
                cmd.Parameters.AddWithValue("pr", PrimZ.Text);
                cmd.Parameters.AddWithValue("kol", TheNumSpisan.Text);
                cmd.Parameters.AddWithValue("ost", Ostatok.Text);
                cmd.Parameters.AddWithValue("wz", _wz);
                cmd.ExecuteNonQuery();
              //  Sklsdd_Click(sender, e);
            }
            Response.Redirect("~/Storage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                if (NumSklad.SelectedValue == "") return;
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Sklady set DataSpisaniya=@dat, NumDocSpisan=@ndoc, TheNumSpisan=@kol, Prim=@pr, Ostatok=@ost " +
                    " where Id=@wz", conn);
                cmd.Parameters.AddWithValue("dat", DateTime.Now);
                cmd.Parameters.AddWithValue("ndoc", NumDocSpisan.Text);
                cmd.Parameters.AddWithValue("pr", PrimZ.Text);
                cmd.Parameters.AddWithValue("kol", TheNumSpisan.Text);
                cmd.Parameters.AddWithValue("ost", Ostatok.Text);
                cmd.Parameters.AddWithValue("wz", _wz);
                cmd.ExecuteNonQuery();
                 cmd = new SqlCommand("insert into Sklady (DataPost, NumDoc, NumSklada, Zakreplen, Name, NumID, TheNum, Price, Source, Ostatok, [Prim],  Obz) " +
                   " values (@dp, @nd, @ns, @zk, @nm, @ni, @tn, @pr, @so, @os, @pm, @obz )", conn);
                cmd.Parameters.AddWithValue("dp", DateTime.Now);
                cmd.Parameters.AddWithValue("nd", NumDocSpisan.Text);                
                cmd.Parameters.AddWithValue("ns", NumSklad.SelectedValue);
                cmd.Parameters.AddWithValue("zk", WorkSklad.SelectedValue);
                cmd.Parameters.AddWithValue("nm", Name.Text);
                cmd.Parameters.AddWithValue("ni", NumID.Text);
                cmd.Parameters.AddWithValue("tn", TheNumSpisan.Text);
                cmd.Parameters.AddWithValue("pr", Price.Text);
                cmd.Parameters.AddWithValue("so", Source.Text);
                cmd.Parameters.AddWithValue("os", TheNumSpisan.Text);
                cmd.Parameters.AddWithValue("obz", Obz.Text);
                cmd.Parameters.AddWithValue("pm", PrimZ.Text);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/Storage.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            phPeremesh.Visible = true;
        }
    }
}