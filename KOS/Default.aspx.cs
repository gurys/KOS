using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using KOS.App_Code;
using KOS.Controls;

namespace KOS
{
    public partial class _Default : Page
    {
        class Data
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Text1 { get; set; }
            public string Idi { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();
            List<Data> data = new List<Data>();
            Date.Text = DateTime.Now.ToShortDateString();
            Plan.Text = "График работы " + App_Code.Base.months[DateTime.Now.Month - 1];
            DayPlan.Text = "План работ на " + DateTime.Now.ToShortDateString();
            Zayavky.Text = "Заявки ОДС на " + DateTime.Now.ToShortDateString();
            ZayavkyREO.Text = "Заявки ОДС по ПНР/РЭО на " + DateTime.Now.ToShortDateString();
            if (!IsPostBack)
            {

                if (User.Identity.Name == "Sargamonov")
                {
                   
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                        "join [Events] e on e.ZayavId=z.Id " +
                        "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                        "join Users u on u.UserId=wl.UserId " +
                        "where u.UserName=@UserName and z.Category=N'ПНР/РЭО' and z.[Finish] is null ", conn);
                        cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                            data.Add(new Data()
                            {
                                Title = " №" + " " + dr[3].ToString(),
                                Url = "~/ZayavkaEdit.aspx?zId=" + Int32.Parse(dr["Id"].ToString()),
                                Text1 = dr["Text"].ToString(),
                                Idi = dr["LiftId"].ToString()
                            });
                        dr.Close();
                        ZvREO.Visible = true;
                        ZayavkyListREO.DataSource = data;
                        ZayavkyListREO.DataBind();
                    }
                   
                }

                if (User.Identity.Name != "Sargamonov") { 
                    
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("select z.Id, z.LiftId, z.[Text], e.Id, z.Category, z.Worker, z.Start, z.Status from Zayavky z " +
                       "join [Events] e on e.ZayavId=z.Id " +
                       "join WorkerLifts wl on wl.LiftId=z.LiftId " +
                       "join Users u on u.UserId=wl.UserId " +
                       "where u.UserName=@UserName and z.Category!=N'ПНР/РЭО' and z.[Finish] is null ", conn);
                        cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                            data.Add(new Data()
                            {
                                Title = " №" + " " + dr[3].ToString(),
                                Url = "~/ZayavkaEdit.aspx?zId=" + Int32.Parse(dr["Id"].ToString()),
                                Text1 = dr["Text"].ToString(),
                                Idi = dr["LiftId"].ToString()
                            });
                        dr.Close();
                        ZvODS.Visible = true;
                        ZayavkyList.DataSource = data;
                        ZayavkyList.DataBind();
                    }
                }
               
            }
        }

        protected void Plan_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plan.aspx");
        }

        protected void DayPlan_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DayPlan.aspx");
        }

        protected void Info_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Lib.aspx");
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker", "Electronick" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            roles = new List<string>() { "ODS" };
            if (db.CheckAccount(User.Identity.Name, roles)) 
                Response.Redirect("~/OdsHome.aspx");
            roles = new List<string>() { "Manager" };
            if (db.CheckAccount(User.Identity.Name, roles))
                Response.Redirect("~/ManagerHome.aspx");
            roles = new List<string>() { "ManagerTSG" };
            if (db.CheckAccount(User.Identity.Name, roles))
                Response.Redirect("~/ManagerTSG.aspx");
            roles = new List<string>() { "ODS_tsg" };
            if (db.CheckAccount(User.Identity.Name, roles))
                Response.Redirect("~/ODS_TSG.aspx");
            roles = new List<string>() { "Cadry" };
            if (db.CheckAccount(User.Identity.Name, roles))
                Response.Redirect("~/AdminHome.aspx");
            Response.Redirect("~/About.aspx");
        }

        protected void WorkerZayavka_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WZayavka.aspx");
        }

        protected void Akt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AktWork.aspx");
        }

        protected void Sklad_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Storage.aspx");
        }
    }
}