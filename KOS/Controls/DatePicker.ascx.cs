using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KOS.Controls
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        public DateTime SelectedDate
        {
            get 
            {
                if (Year.SelectedIndex >= 0)
                    return new DateTime(int.Parse(Year.SelectedValue), Month.SelectedIndex + 1, int.Parse(Day.SelectedValue));
                else
                    return DateTime.Now.Date;
            }
            set
            {
                Year.SelectedValue = value.Year.ToString();
                Month.SelectedIndex = value.Month - 1;
                Day.SelectedIndex = value.Day - 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> year = new List<string>();
                for (int i = 2016; i <= DateTime.Now.Year; i++)
                    year.Add(i.ToString());
                Year.DataSource = year;
                Year.DataBind();
                Year.SelectedValue = DateTime.Now.Year.ToString();

                Month.DataSource = KOS.App_Code.Base.months;
                Month.DataBind();
                Month.SelectedIndex = DateTime.Now.Month - 1;

                BindDay();

                Day.SelectedIndex = DateTime.Now.Day - 1;
            }
        }

        protected void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }

        protected void Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDay();
        }

        void BindDay()
        {
            List<string> day = new List<string>();
            for (int i = 1; i <= DateTime.DaysInMonth(int.Parse(Year.SelectedValue), Month.SelectedIndex + 1); i++)
                day.Add(i.ToString());
            Day.DataSource = day;
            Day.DataBind();
            Day.SelectedIndex = 0;
        }
    }
}