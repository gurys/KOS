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
    public partial class Enviroment : System.Web.UI.Page
    {
        int _id = -1;
        List<App_Code.Base.EnviromentAddress> _data;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["id"]))
            {
                _id = Int32.Parse(Request["id"]);
                phAddRecord.Visible = false;
                phEditRecord.Visible = true;
            }
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _data = db.GetEnviromentAddresses();
            if (!IsPostBack)
            {
                AddAddress.DataSource = _data;
                AddAddress.DataBind();
                EditAddress.DataSource = _data;
                EditAddress.DataBind();
                if (_data.Count > 0)
                {
                    AddAddress.SelectedIndex = 0;
                    EditAddress.SelectedIndex = 0;
                }
                List<string> list = db.GetEnviromentType();
                AddType.DataSource = list;
                AddType.DataBind();
                AddType.SelectedIndex = 0;
                EditType.DataSource = list;
                EditType.DataBind();
                EditType.SelectedIndex = 0;
                if (_id > 0)
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("e.[Name], e.Type, ea.Address, e.[Description] from [Enviroment] e " +
                            "join EnviromentAddresses ea on ea.Id=e.AddressId where e.Id=@id", conn);
                        cmd.Parameters.AddWithValue("id", _id);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count>0)
                        {
                            EditType.SelectedValue = dt.Rows[0]["Type"].ToString();
                            EditName.Text = dt.Rows[0]["Name"].ToString();
                            EditAddress.SelectedValue = dt.Rows[0]["Address"].ToString();
                            EditDescription.Text = dt.Rows[0]["Description"].ToString();
                        }
                    }
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            phAddRecord.Visible = true;
            phEditRecord.Visible = false;
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [Enviroment] " +
                    "([Name], [Type], AddressId, [Description]) " +
                    "values (@n, @t, @ai, @d)", conn);
                cmd.Parameters.AddWithValue("n", AddName.Text);
                cmd.Parameters.AddWithValue("t", AddType.SelectedValue);
                App_Code.Base.EnviromentAddress ea = _data.Find(delegate(App_Code.Base.EnviromentAddress i)
                {
                    return i.Address == AddAddress.SelectedValue;
                });
                cmd.Parameters.AddWithValue("ai", ea.Id);
                cmd.Parameters.AddWithValue("d", AddDescription.Text);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Enviroment.aspx");
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (_id < 1) return;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update [Enviroment] set [Name]=@n, [Type]=@t, [Description]=@d, AddressId=@ai where Id=@id", conn);
                cmd.Parameters.AddWithValue("n", EditName.Text);
                cmd.Parameters.AddWithValue("t", EditType.SelectedValue);
                cmd.Parameters.AddWithValue("d", EditDescription.Text);
                cmd.Parameters.AddWithValue("e", DateTime.Now);
                App_Code.Base.EnviromentAddress ea = _data.Find(delegate(App_Code.Base.EnviromentAddress i)
                {
                    return i.Address == AddAddress.SelectedValue;
                });
                if (ea != null) cmd.Parameters.AddWithValue("ai", ea.Id);
                else return;
                cmd.Parameters.AddWithValue("id", _id);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Journal.aspx");
            }

        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (_id < 1) return;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from [Enviroment] where Id=@id", conn);
                cmd.Parameters.AddWithValue("id", _id);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Enviroment.aspx");
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Enviroment.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Electronick", "Cadry" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
    }
}