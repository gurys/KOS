using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KOS.Controls
{
    public partial class Joystik : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.PreviousPage != null)
                Left.PostBackUrl = Page.PreviousPage.ToString();
        }
    }
}