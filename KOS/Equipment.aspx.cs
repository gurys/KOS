using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using KOS.App_Code;

namespace KOS
{
    public partial class Equipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> s = db.GetIdU();
                IdU.DataSource = s;
                IdU.DataBind();
                if (s.Count > 0)
                    IdU.SelectedIndex = 0;
                IdU_SelectedIndexChanged(sender, e);
               
            }

                 if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                List<string> areas = db.GetWZAreas();
                Area.DataSource = areas;
                Area.DataBind();
                Area.SelectedIndex = 0;
                List<string> nodes = db.GetWZNodes(Area.SelectedValue);
                Node.DataSource = nodes;
                Node.DataBind();
                Node.SelectedIndex = 0;
                List<string> poz = db.GetWZPozition(Area.SelectedValue, Node.SelectedValue);
                Pozition.DataSource = poz;
                Pozition.DataBind();
                Pozition.SelectedIndex = 0;
            }
                 if (!IsPostBack)
                 {
                     using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                     {
                         conn.Open();
                         //выбор производителя и модели
                         SqlCommand cmd;
                         cmd = new SqlCommand("select Manufacturer, Model from PhisicalAddr " +
                                    "where LiftID=@lift", conn);
                         cmd.Parameters.AddWithValue("lift", LiftId.SelectedValue);
                         SqlDataReader dr = cmd.ExecuteReader();
                         //     dr = cmd.ExecuteReader();
                         if (dr.Read())
                             Disp.Text = dr[0].ToString() + "-" + dr[1].ToString() + ":";
                         string mf = dr[0].ToString(); string md = dr[1].ToString();
                         dr.Close();

                     }
                 }
        }
        protected void IdU_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> ls = db.GetIdM(IdU.SelectedValue);
            IdM.DataSource = ls;
            IdM.DataBind();
            if (ls.Count > 0)
                IdM.SelectedIndex = 0;
            IdM_SelectedIndexChanged(sender, e);
        }
        protected void IdM_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<string> liftId = db.GetLiftId(IdU.SelectedValue, IdM.SelectedValue);
            LiftId.DataSource = liftId;
            LiftId.DataBind();
            LiftId.SelectedIndex = 0;

        }
                protected void Save_Click(object sender, EventArgs e)
        {
           
            // Запись в базу ЗИП
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd1;
                cmd1 = new SqlCommand("select Manufacturer, Model from PhisicalAddr " +
                           "where LiftID=@lift", conn);
                cmd1.Parameters.AddWithValue("lift", LiftId.SelectedValue);
                SqlDataReader dr = cmd1.ExecuteReader();
                //     dr = cmd.ExecuteReader();
                if (dr.Read())
                    Disp.Text = dr[0].ToString() + "," + dr[1].ToString();
                string mf1 = dr[0].ToString(); string md1 = dr[1].ToString();
                dr.Close();
                if (mf1 == " " )
                {
                    Msg.Text = "Внимание! Лифт не внесен в базу лифтов!";
                    return;
                }
                SqlCommand cmd = new SqlCommand("insert into EquipmentBase " +
                    "([Manufacturer], [Model], [Area], [Node], [Pozition], [Name], [NumId]) " +
                    "values (@text, @text0, @area, @node, @pozition, @text4, @text5)", conn);
                cmd.Parameters.AddWithValue("text", mf1);
                cmd.Parameters.AddWithValue("text0", md1);
                cmd.Parameters.AddWithValue("area", Area.Text);
                cmd.Parameters.AddWithValue("node", Node.Text);
                cmd.Parameters.AddWithValue("pozition", Pozition.Text);
                cmd.Parameters.AddWithValue("text4", Text4.Text);
                cmd.Parameters.AddWithValue("text5", Text5.Text);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect(Request.RawUrl);
        }
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Equipment.aspx");

            List<string> roles = new List<string>() { "Cadry", "Administrator", "Manager", "Worker" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
    }

}