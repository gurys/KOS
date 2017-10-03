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
    public partial class WZView : System.Web.UI.Page
    {
        int _wz = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["zId"]))
             //   if (!string.IsNullOrEmpty(Request["wz"]) && !IsPostBack)
                Response.Redirect("~/About.aspx");
            _wz = Int32.Parse(Request["zId"]);
            if (!IsPostBack)
            {
                App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                DataTable data = db.GetWorkerZayavka(_wz);
                if (data.Rows.Count < 1)
                    Response.Redirect("~/About.aspx");
                From.Text = data.Rows[0]["Family"].ToString() + " " + data.Rows[0]["IO"].ToString();
                Date.Text = ((DateTime)data.Rows[0]["Date"]).ToLongDateString();
                Text.Text = data.Rows[0]["Text"].ToString();
                Type.Text = data.Rows[0]["Type"] is DBNull ? "" : data.Rows[0]["Type"].ToString();
                LiftId.Text = data.Rows[0]["LiftId"] is DBNull ? "" : data.Rows[0]["LiftId"].ToString();
                if (bool.Parse(data.Rows[0]["Done"].ToString()) == false)
                    Done.Text = "не выполнено";
                else
                {
                    Done.Text = "выполнено " + data.Rows[0]["WhoFamily"].ToString() + " " +
                        data.Rows[0]["WhoIO"].ToString() + " " +
                        ((DateTime)data.Rows[0]["DoneDate"]).ToLongDateString();
                }
                Readed.Checked = (data.Rows[0]["Readed"] is DBNull ? false : bool.Parse(data.Rows[0]["Readed"].ToString()));
            }
        }
    }
}