using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using KOS.App_Code;

namespace KOS
{
    public partial class AddPrim : System.Web.UI.Page
    {
        int _worksId = 0, _planId = 0, _reglamentId = 0;

        string[] _index = { "1", "2" };

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["WorksId"]))
                _worksId = Int32.Parse(Request["WorksId"]);
            string liftId = string.Empty;
            if (!string.IsNullOrEmpty(Request["PlanId"]))
            {
                _planId = Int32.Parse(Request["PlanId"]);
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select LiftId from [Plan] where Id=@i", conn);
                    cmd.Parameters.AddWithValue("i", _planId);
                    liftId = (string)cmd.ExecuteScalar();
                }
            }
            if (!string.IsNullOrEmpty(Request["ReglamentId"]))
                _reglamentId = Int32.Parse(Request["ReglamentId"]);

            Date.Text = DateTime.Now.ToString("dd.MM.yyyy hh:mm");
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            Address.Text = db.GetAddress(_planId);
            ReglamentTitle.Text = db.GetReglamentTitle(_reglamentId);
            Lifts.DataSource = db.GetLiftsId(_planId);
            if (!IsPostBack)
            {
                Lifts.DataBind();
                if (!string.IsNullOrEmpty(liftId))
                    Lifts.SelectedValue = liftId;
                Index.DataSource = _index;
                Index.DataBind();
            }
        }
    
        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/DayPlan.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker" };
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (Lifts.SelectedIndex < 0)
                Lifts.SelectedIndex = 0;
            string prim = "Дата/время: " + DateTime.Now.ToString() + "\r\n";
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            prim += "Вид работ " + db.GetTp(_planId) + "\r\n";
            prim += "№ лифта: " + Lifts.SelectedItem.ToString() + "\r\n";
            prim += ReglamentTitle.Text + "\r\n";
            prim += "Замечание: " + Prim.Text + "\r\n";
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select p.Id as PlanId from [Plan] p " +
                    "where p.UserId=(select UserId from Users where UserName=@UserName) and " +
                "p.[Date]=(select p2.[Date] from [Plan] p2 where p2.Id=@PlanId) and p.LiftId=@LiftId", conn);
                cmd.Parameters.AddWithValue("UserName", User.Identity.Name);
                cmd.Parameters.AddWithValue("PlanId", _planId);
                cmd.Parameters.AddWithValue("LiftId", Lifts.SelectedItem.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int planId = _planId;
                if (dt.Rows.Count > 0 && !(dt.Rows[0]["PlanId"] is DBNull))
                    planId = Int32.Parse(dt.Rows[0]["PlanId"].ToString());
                /*cmd = new SqlCommand("select w.Id as WorksId from ReglamentWorks w " +
                    "where w.PlanId=@PlanId and w.ReglamentId=@rId", conn);
                cmd.Parameters.AddWithValue("PlanId", planId);
                cmd.Parameters.AddWithValue("rId", _reglamentId);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                int worksId = 0;
                if (dt.Rows.Count > 0 && !(dt.Rows[0]["WorksId"] is DBNull))
                    worksId = Int32.Parse(dt.Rows[0]["WorksId"].ToString());
                 */
                int worksId = _worksId;
                if (_worksId < 1 || (planId != _planId && worksId < 1))
                {
                    cmd = new SqlCommand("insert into ReglamentWorks " +
                        "(ReglamentId, PlanId, [Date], UserId, [Index], [Prim]) " +
                        "values (@rId, @pId, @Date, (select UserId from Users where UserName=@user), @i, @Prim)",
                        conn);
                    cmd.Parameters.AddWithValue("rId", _reglamentId);
                    cmd.Parameters.AddWithValue("pId", planId);
                    cmd.Parameters.AddWithValue("Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    cmd.Parameters.AddWithValue("i", int.Parse(Index.SelectedValue));
                    cmd.Parameters.AddWithValue("Prim", prim);
                }
                else
                {
                    if (worksId < 1) worksId = _worksId;
                    cmd = new SqlCommand("select Prim from ReglamentWorks where Id=@rwId", conn);
                    cmd.Parameters.AddWithValue("rwId", _worksId);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0 && !(dt.Rows[0]["Prim"] is DBNull))
                        prim = dt.Rows[0]["Prim"].ToString() + prim;

                    cmd = new SqlCommand("update ReglamentWorks " +
                        "set [Prim]=@Prim, [Date]=@Date, [Done]=0, UserId=(select UserId from Users where UserName=@user), [Index]=@i " +
                        "where Id=@rwId", conn);
                    cmd.Parameters.AddWithValue("Prim", prim);
                    cmd.Parameters.AddWithValue("rwId", worksId);
                    cmd.Parameters.AddWithValue("Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("user", User.Identity.Name);
                    cmd.Parameters.AddWithValue("i", int.Parse(Index.SelectedValue));
                }
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("update [Plan] set Done=0 " +
                    "where Id=@PlanId ", conn);
                cmd.Parameters.AddWithValue("PlanId", _planId);
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("~/Reglament.aspx?PlanId=" + _planId);
        }
    }
}