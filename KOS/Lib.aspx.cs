using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.IO;

namespace KOS
{
    public partial class Lib : System.Web.UI.Page
    {
        string _path = string.Empty;

        class Data
        {
            public string Title { get; set; }
            public string Url { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccount();

            if (!string.IsNullOrEmpty(Request["lib"]))
                _path = HttpUtility.HtmlDecode(Request["lib"]);
            else
                _path = Request.PhysicalApplicationPath + "LIB\\";

            if (!Directory.Exists(_path))
            {
                if (File.Exists(_path))
                {
                    string path = Request.Url.ToString();
                    path = path.Substring(0, path.LastIndexOf('/') + 1) + _path.Substring(Request.PhysicalApplicationPath.Length);
                    Response.Redirect(path);
                }
            }
            else
            {
                DirectoryInfo info = new DirectoryInfo(_path);
                DirectoryInfo[] dirs = info.GetDirectories();
                List<Data> data = new List<Data>();
                if (_path != Request.PhysicalApplicationPath + "LIB\\")
                {
                    string url = _path;
                    if (url[url.Length - 1] == '\\')
                        url = url.Substring(0, url.Length - 1);
                    if (_path.LastIndexOf('\\') > 0)
                        url = url.Substring(0, _path.LastIndexOf('\\') + 1);
                    data.Add(new Data()
                    {
                        Title = "..",
                        Url = "~/Lib.aspx?lib=" + HttpUtility.HtmlEncode(url)
                    });
                }
                foreach (DirectoryInfo di in dirs)
                {
                    data.Add(new Data()
                    {
                        Title = di.Name,
                        Url = "~/Lib.aspx?lib=" + HttpUtility.HtmlEncode(di.FullName)
                    });
                }
                FileInfo[] files = info.GetFiles();
                foreach (FileInfo fi in files)
                {
                    data.Add(new Data()
                    {
                        Title = fi.Name,
                        Url = "~/Lib.aspx?lib=" + HttpUtility.HtmlEncode(fi.FullName)
                    });
                }
                Dir.DataSource = data;
                Dir.DataBind();
            }
        }

        void CheckAccount()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                Response.Redirect("~/Account/Login.aspx?ReturnUrl=~/Lib.aspx");

            List<string> roles = new List<string>() { "Administrator", "Manager", "Cadry", "Worker" };
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (db.CheckAccount(User.Identity.Name, roles)) return;
            Response.Redirect("~/About.aspx");
        }
    }
}