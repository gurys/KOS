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
    public partial class Reglament : System.Web.UI.Page
    {
        int _planId = 0;
        string _retUrl = string.Empty;
        DataTable _data, _prim;
        List<string> _liftsId;
        List<int> _planIds;
        string _liftId;
        string _TpId; 
        class TheN
        {
            public int N { get; set; }
        }
        class Data
        {
            public string Title { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["PlanId"]))
                _planId = Int32.Parse(Request["PlanId"]);
            if (!string.IsNullOrEmpty(Request["ret"]))
                _retUrl = HttpUtility.HtmlDecode(Request["ret"]);
            else
                Grafik.Visible = false;

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _planIds = db.GetPlans(User.Identity.Name, _planId);
            _prim = db.GetPrim(User.Identity.Name, _planId);
            List<TheN> n = new List<TheN>();
            foreach (DataRow dr in _prim.Rows)
                n.Add(new TheN() { N = (int)dr["N"] });
            _data = db.GetReglamentWorks(_planId);
            _data.Columns.Add("bDone", Type.GetType("System.Boolean"));
            _data.Columns.Add("AddPrim", Type.GetType("System.String"));
            _data.Columns.Add("PrimExists", Type.GetType("System.String"));
            foreach (DataRow dr in _data.Rows)
            {
                if (dr["Done"] is DBNull || bool.Parse(dr["Done"].ToString()) == false)
                    dr["bDone"] = false;
                else
                    dr["bDone"] = true;
                //      if (dr["WorksId"] is DBNull) // было закомментировано
                dr["AddPrim"] = "~/AddPrim.aspx?PlanId=" + _planIds + "&ReglamentId=" + dr["ReglamentId"].ToString();
                //     else  // было закомментировано 2 стр.
                //       dr["AddPrim"] = "~/AddPrim.aspx?WorksId=" + dr["WorksId"].ToString() + "&PlanId=" + _planId + "&ReglamentId=" + dr["ReglamentId"].ToString();
                dr["PrimExists"] = "";
                if (!(dr["N"] is DBNull))
                {
                    int N = (int)dr["N"];
                    if (n.Find(delegate(TheN i)
                    {
                        return i.N == N;
                    }) != null)
                        dr["PrimExists"] = "!!!";
                }
            }
            _liftsId = db.GetLiftsId(_planId);
            _liftId = db.GetLiftId(_planId);

            /* string liftsId = string.Empty;// начало коммента
             foreach (string s in _liftsId)
                 if (string.IsNullOrEmpty(liftsId))
                     liftsId = s;
                 else
                     liftsId += "," + s; */
            // конец коммента
             
            List<Data> data = new List<Data>();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select TpId from [Plan] where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _planId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    _TpId = dr["TpId"].ToString(); 
                dr.Close();
             }
//
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                TpId.Text = _TpId;
                LiftsId.Text = _liftId;
                SqlCommand cmd = new SqlCommand("select r.[Title] from [Reglament] r " +
                      "where r.[TpId]=@rId", conn);
           /*     if (TpId.Text == "ТР")
                cmd.Parameters.AddWithValue("rId", "ТР");
                else if (TpId.Text == "ТР1")
                cmd.Parameters.AddWithValue("rId", "ТР1");
                else if (TpId.Text == "ТР2")
                cmd.Parameters.AddWithValue("rId", "ТР2");
                else if (TpId.Text == "ТР3")
                cmd.Parameters.AddWithValue("rId", "ТР3");
                else */
                cmd.Parameters.AddWithValue("rId", TpId.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Data()
                    {
                        Title = dr["Title"].ToString()
                    });
                    Out.DataSource = data;
                    Out.DataBind();
                }
                dr.Close();
            }
//
            if (!IsPostBack)
            {
                ReglamentWorks.DataSource = _data;
                ReglamentWorks.DataBind();
                Msg.Text = string.Empty;
                List<Base.ZPrim> list = db.GetNotDonePrim(_liftId, Request.Url.ToString());
                ZPrim.DataSource = list;
                ZPrim.DataBind();
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select Done from [Plan] where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", _planId);
                    object o = cmd.ExecuteScalar();
                    if (o != null)
                        Done.Checked = bool.Parse(o.ToString());
                    cmd = new SqlCommand("select Prn from [Plan] where Id=@id", conn);
                    cmd.Parameters.AddWithValue("id", _planId);
                    string op = cmd.ExecuteScalar().ToString();
                    if (op == "") Prin.Checked = false;
                    else Prin.Checked = bool.Parse(op.ToString());

                }
                if (User.Identity.Name == "Volodin" || User.Identity.Name == "Puzin" || User.Identity.Name == "manager")
                { Prin.Visible = true; btnPrin.Visible = true; }
                if (TpId.Text == "ВР" && User.Identity.Name == "manager")
                { AddBP.Visible = true; TextBox1.Visible = true; L1.Visible = true; }
            }
        } 

        protected void Save_Click(object sender, EventArgs e)
        {
            List<string> roles = new List<string>() { "Manager" };
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles))
            {
                if (!string.IsNullOrEmpty(Request["ret"]))
                    Response.Redirect(HttpUtility.HtmlDecode(Request["ret"]));
                return;
            }

            DateTime now = DateTime.Now;
            bool allDone = Done.Checked;
            bool prn = Prin.Checked;
            for (int i=0; i< ReglamentWorks.Items.Count; i++)
            {
                DataRow dr = _data.Rows[i];
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd;
                    if (dr["WorksId"] is DBNull)
                    {
                        foreach (int plan in _planIds)
                        {
                            cmd = new SqlCommand("insert into ReglamentWorks " +
                                "(ReglamentId, PlanId, Done, [Date], UserId) " +
                                "values (@rId, @pId, @Done, @Date, (select UserId from Users where UserName=@user))", conn);
                            cmd.Parameters.AddWithValue("rId", dr["ReglamentId"]); //dr["ReglamentId"]);
                            cmd.Parameters.AddWithValue("pId", plan);
                            cmd.Parameters.AddWithValue("Done", allDone);
                            cmd.Parameters.AddWithValue("Date", now);
                            cmd.Parameters.AddWithValue("user", User.Identity.Name);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("update ReglamentWorks " +
                            "set [Done]=@Done, [Date]=@Date, UserId=(select UserId from Users where UserName=@user) where Id=@rwId", conn);
                        cmd.Parameters.AddWithValue("Done", allDone);
                        cmd.Parameters.AddWithValue("rwId", dr["WorksId"]);
                        cmd.Parameters.AddWithValue("Date", now);
                        cmd.Parameters.AddWithValue("user", User.Identity.Name);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update [Plan] set Done=@done, Prn=@prn where Id=@PlanId", conn);
                cmd.Parameters.AddWithValue("PlanId", _planId);
                cmd.Parameters.AddWithValue("done", allDone);
                cmd.Parameters.AddWithValue("prn", prn);
                cmd.ExecuteNonQuery();
            
            }
        
            if (!string.IsNullOrEmpty(Request["ret"]))
                Response.Redirect(HttpUtility.HtmlDecode(Request["ret"]));
            Msg.Text = "Успешно сохранено";
        }

        void SavePlan(bool allDone)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd;
                foreach (string lift in _liftsId)
                {
                    if (!allDone || IsPrimEsists(lift))
                    {
                        cmd = new SqlCommand("update [Plan] set Done=0 " +
                            "where LiftId=@LiftId and UserId=(select UserId from [Plan] where Id=@PlanId) " +
                            "and [Date]=(select [Date] from [Plan] where Id=@PlanId)", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("update [Plan] set Done=1 " +
                            "where LiftId=@LiftId and UserId=(select UserId from [Plan] where Id=@PlanId) " +
                            "and [Date]=(select [Date] from [Plan] where Id=@PlanId)", conn);
                    }
                    cmd.Parameters.AddWithValue("LiftId", lift);
                    cmd.Parameters.AddWithValue("PlanId", _planId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
       
        protected void Prin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["PlanId"]))
                _planId = Int32.Parse(Request["PlanId"]);
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd;

                if (Done.Checked== true && Prin.Checked == true)
                {
                    cmd = new SqlCommand("update [Plan] set Prn=1 " +
                        " where Id=@PlanId ", conn);
                    cmd.Parameters.AddWithValue("PlanId", _planId);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Msg.Text = "Принятие возможно после отметки о выполнении работ или нет отметки *Принял* !"; return;
                }
              
            }
        }
        bool IsPrimEsists(string lift)
        {
            foreach (DataRow dr in _prim.Rows)
            {
                string prim = dr["Prim"].ToString();
                if (prim.IndexOf(lift) > 0)
                    return true;
            }
            return false;
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/DayPlan.aspx");

            List<string> roles = new List<string>() { "Administrator", "Worker", "Manager" };
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }

        protected void AddZPrim_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WZayavka.aspx?lift=" + HttpUtility.HtmlEncode(_liftId));
         //   Response.Redirect("~/ZPrimAdd.aspx?lift=" + HttpUtility.HtmlEncode(_liftId) + "&ReturnUrl=" +
         //       HttpUtility.HtmlEncode(Request.RawUrl));
        }

        protected void Grafik_Click(object sender, EventArgs e)
        {
            Response.Redirect(_retUrl);
        }

        protected void AddBP_Click(object sender, EventArgs e)
        {
            if (TpId.Text == "ВР")
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Reglament (TpId, LiftId, N, Title) values (@tr, @lift, @n, @ti)", conn);
                cmd.Parameters.AddWithValue("tr", "ВР");
                cmd.Parameters.AddWithValue("lift", _liftId);
                cmd.Parameters.AddWithValue("n", 1);
                cmd.Parameters.AddWithValue("ti", TextBox1.Text);
                cmd.ExecuteNonQuery();

            }
            else { Msg.Text = "Регламент ТР не корректируется!"; return; }
            Response.Redirect("~/Reglament.aspx?planId=" + HttpUtility.HtmlEncode(_planId) + "&ReturnUrl=" +
                HttpUtility.HtmlEncode(Request.RawUrl));
        }
    }
}