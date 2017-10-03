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
    public partial class Ods_Doc : System.Web.UI.Page
    {
        class Data
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string Name { get; set; }
            public string Nev { get; set; }             
            public string NameFile { get; set; }
            public string Status { get; set; }
            public string Prim { get; set; }
            public string Usr { get; set; }
        }
        string _role = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Data> datdoc = GetData1();
            Out1.DataSource = datdoc;
            Out1.DataBind();

         //   List<Data> datdo = GetData();
          //  Out.DataSource = datdo;
         //   Out.DataBind();
        }
         /*   List<Data> GetData() 
            {
                List<Data> datdo = new List<Data>();
                string url = "~/DocumView.aspx?zId=";
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.Image, d.NameFile, d.Status, d.Prim, d.Usr from Documents d " +
                        "join Events e on e.Id=d.NumEvent " +
                        "where d.Status=N'не подписан заказчиком' and (select l.LiftId, u.UserName from Lifts l " +
                        "join LiftsTtx lt on lt.LiftId=l.LiftId " +
                        "left join WorkerLifts wl on wl.LiftId=l.LiftId " +
                        "left join Users u on u.UserId=wl.UserId and u.UserName=@user )", conn);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    cmd.Parameters.AddWithValue("idU", "1");
                    cmd.Parameters.AddWithValue("idM", "4");
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        datdo.Add(new Data()
                        {
                            Id = dr["Id"].ToString(),
                            Url = (_role == "ODS" ? "#" : url + dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            Nev = dr["NumEvent"].ToString(),
                            NameFile = dr["NameFile"].ToString(),
                            Status = dr["Status"].ToString(),
                            Prim = dr["Prim"].ToString(),
                            Usr = dr["Usr"].ToString()
                        });
                    }
                    dr.Close();
                }
                return datdo;
            } */
        List<Data> GetData1()
        {
            List<Data> datdoc = new List<Data>();
            string url = "~/DocumView.aspx?zId=";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select d.Id, d.Name, d.NumEvent, d.Image, d.NameFile, d.Status, d.Prim, d.Usr from Documents d " +
                           "join Events e on e.Id=d.NumEvent " +
                           "where d.Status=N'не подписан заказчиком' and e.Family=@user", conn);
                cmd.Parameters.AddWithValue("user", User.Identity.Name);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    datdoc.Add(new Data()
                    {
                        Id = dr["Id"].ToString(),
                        Url = (_role == "ODS" ? "#" : url + dr["Id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Nev = dr["NumEvent"].ToString(),
                        NameFile = dr["NameFile"].ToString(),
                        Status = dr["Status"].ToString(),
                        Prim = dr["Prim"].ToString(),
                        Usr = dr["Usr"].ToString()
                    });
                }
                dr.Close();
            }
            return datdoc;
        }
    }
}