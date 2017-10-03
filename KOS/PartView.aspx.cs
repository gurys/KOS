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
using iTextSharp.text;
using iTextSharp.text.pdf;
using KOS.App_Code;

namespace KOS
{
    public partial class PartView : System.Web.UI.Page
    {
        int _wz = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
if (string.IsNullOrEmpty(Request["zId"]))
           // if (!string.IsNullOrEmpty(Request["zId"]) && !IsPostBack)
             Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (User.Identity.Name == "manager") Button1.Visible = true;
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetPart(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
            //    Id.Text = _wz.ToString();
                Name.Text = data.Rows[0]["Name"].ToString();
                NumEvent.Text = data.Rows[0]["NumEvent"].ToString();           
                NameFile.Text = data.Rows[0]["namefoto"].ToString();
                NumID.Text = data.Rows[0]["NumID"].ToString();
                Obz.Text = data.Rows[0]["Obz"].ToString();
                Kol.Text = data.Rows[0]["Kol"].ToString();


                
            }
        }
           //запрос к базе для конвертации и просмотра Foto
         protected void Foto_Click(object sender, EventArgs e)
         { 
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select p.[Foto], p.namefoto from PartsList p " +
                    "where p.Id=@id ", conn);
                cmd.Parameters.AddWithValue("id", _wz);
             //чтение из базы
                SqlDataReader datareader = cmd.ExecuteReader();
                datareader.Read();
                int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                byte[] bBuffer = new byte[bLength];
                datareader.GetBytes(0, 0, bBuffer, 0, bLength);
             //  просмотр в браузере
                  Response.ContentType = "image"; //image/Pdf 
                  Response.BinaryWrite(bBuffer);
                   string pdfFileName = Request.PhysicalApplicationPath;
                  Response.ContentType = "application/x-download";
                  Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", NameFile.Text));
               //    Response.ContentEncoding = Encoding.UTF8;
               //  Response.BinaryWrite(bBuffer);
               //   Response.WriteFile(pdfFileName);
               //    Response.HeaderEncoding = Encoding.UTF8;
                   Response.Flush();
                   Response.End();
                 
            }   
        
        }
         //запрос для скачивания Foto
         protected void Donl_Click(object sender, EventArgs e)
         {
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("select  p.[Foto], p.namefoto from PartsList p " +
                     "where p.Id=@id ", conn);
                 cmd.Parameters.AddWithValue("id", _wz);
                 //чтение из базы
                 SqlDataReader datareader = cmd.ExecuteReader();
                 datareader.Read();
                 int bLength = (int)datareader.GetBytes(0, 0, null, 0, int.MaxValue);
                 byte[] bBuffer = new byte[bLength];
                 datareader.GetBytes(0, 0, bBuffer, 0, bLength);
                 //  просмотр в браузере
                 Response.ContentType = "image"; //image/Pdf 
                 Response.BinaryWrite(bBuffer);
                 Response.Flush();
                 Response.End();
             }


         }
         protected void EditPart_Click(object sender, EventArgs e) 
         {
             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("update PartsList set Name=@nm, NumID=@ni, Kol=@kol, Obz=@obz " +
                     "where Id=@id", conn);
                 cmd.Parameters.AddWithValue("id", _wz);
                 cmd.Parameters.AddWithValue("nm", Name.Text);
                 cmd.Parameters.AddWithValue("ni", NumID.Text);
                 cmd.Parameters.AddWithValue("kol", Kol.Text);
                 cmd.Parameters.AddWithValue("obz", Obz.Text);
                 cmd.ExecuteNonQuery();
             }
             Msg.Text = "Изменения записаны!";
         }
         protected void DelPart_Click(object sender, EventArgs e)
         {

             using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("delete from [PartsList] " +
                     "where Id=@id", conn);
                 cmd.Parameters.AddWithValue("id", _wz);
                 cmd.ExecuteNonQuery();
             }
             Response.Redirect("~/");
         }
        
    }
}