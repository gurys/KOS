using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KOS.Controls
{
    public partial class SelectMinutes : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            List<int> a = GetSelectedTitles();
            if (!(sender is CheckBox)) return;
            CheckBox Select = (CheckBox)sender;
            Control sc = Select.Parent;
            for (int i = 1; i <= 60; i++)
            {
                CheckBox cb = (CheckBox)sc.FindControl("Checkbox" + i);
                cb.Checked = Select.Checked;
            }
            SelectAll.Focus();
        }

        public List<int> GetSelectedTitles()
        {
            List<int> sel = new List<int>();
            for (int i = 1; i <= 60; i++)
            {
                CheckBox cb = (CheckBox)FindControl("Checkbox" + i);
                if (cb.Checked)
                    if (i==60)
                        sel.Add(0);
                    else
                        sel.Add(i);
            }
            return sel;
        }

        public bool IsAllSelected()
        {
            for (int i = 1; i <= 60; i++)
            {
                CheckBox cb = (CheckBox)FindControl("Checkbox" + i);
                if (!cb.Checked)
                    return false;
            }
            return true;
        }
    }
}