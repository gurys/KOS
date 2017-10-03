using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;

namespace KOS.Controls
{
    public partial class MonthPlan : System.Web.UI.UserControl
    {
        DateTime _date;
        DataTable _data;
        string _user;
        DateTime d1;
        List<KOS.App_Code.Base.Holliday> _hollidays;

        public class Data
        {
            public string Address { get; set; }
            public string TpId { get; set; }
            public DateTime DateBeg { get; set; }
            public DateTime DateEnd { get; set; }
            public string LiftId { get; set; }
            public int PlanId { get; set; }
            public bool Done { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["month"]))
                _date = DateTime.Now.Date;
            else
                _date = DateTime.Parse(HttpUtility.HtmlDecode(Request["month"]));
            if (!IsPostBack)
                SetMonth(_date, Page.User.Identity.Name);
        }

        public void SetMonth(DateTime m, string userName)
        {
            _date = m;
            _user = userName;

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            d1 = new DateTime(_date.Year, _date.Month, 1);
            DateTime d2 = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));
            d2 = d2.AddDays(1);
            _data = db.GetPlan(userName, d1, d2);
            _hollidays = db.GetHollidays(userName, d1, d2);
        }

        int IsHolliday()
        {
            KOS.App_Code.Base.Holliday h = _hollidays.Find(delegate(KOS.App_Code.Base.Holliday d)
            {
                return d.Date == d1;
            });
            if (h == null)
                return 0;
            return h.Id;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<script type=\"text/javascript\">function clickIt(r){window.location.href = r;}</script>" + output.NewLine);  //
            output.Write("<table style=\"border:solid\"><tr colspan=8>{0}</tr><tr style=\"border:solid\">", KOS.App_Code.Base.months[_date.Month - 1]);
            output.Write("<td></td><td>Пн.</td><td>Вт.</td><td>Ср.</td><td>Чт.</td><td>Пт.</td><td>Сб.</td><td>Вс.</td></tr>" + output.NewLine);
            int week = (int)(new DateTime(_date.Year, _date.Month, 1).DayOfYear / 7) + 1;
            output.Write("<tr><td>{0}</td>", week);
            switch (d1.DayOfWeek)
            {
                case DayOfWeek.Monday: break;
                case DayOfWeek.Tuesday: output.Write("<td></td>"); break;
                case DayOfWeek.Wednesday: output.Write("<td></td><td></td>"); break;
                case DayOfWeek.Thursday: output.Write("<td></td><td></td><td></td>"); break;
                case DayOfWeek.Friday: output.Write("<td></td><td></td><td></td><td></td>"); break;
                case DayOfWeek.Saturday: output.Write("<td></td><td></td><td></td><td></td><td></td>"); break;
                case DayOfWeek.Sunday: output.Write("<td></td><td></td><td></td><td></td><td></td><td></td>"); break;
            }
            while (d1.Month == _date.Month)
            {
                List<Data> dayPlan = GetDayPlan(d1.Day);
                Data notDone = dayPlan.Find(delegate(Data d)
                {
                    return d.Done == false;
                });
                if (dayPlan.Count > 0)
                {
                    if (notDone != null)
                        output.Write("<td bgcolor=\"yellow\"");
                    else
                        output.Write("<td bgcolor=\"dodgerblue\"");
                    output.Write("<td bgcolor=\"yellow\" onclick=\"clickIt('DayPlan.aspx?day="
                        + HttpUtility.HtmlEncode(d1.Date.ToShortDateString())
                        + "&user=" + HttpUtility.HtmlEncode(_user)
                        + "')\"><a href=\"DayPlan.aspx?day="
                        + HttpUtility.HtmlEncode(d1.Date.ToShortDateString())
                        + "&user=" + HttpUtility.HtmlEncode(_user) + "\">"
                        + d1.Day + "</a></td>" + output.NewLine);
                }
                else if (IsHolliday() > 0)
                    output.Write("<td bgcolor=\"tomato\">{0}</td>", d1.Day);
                else
                    output.Write("<td onclick=\"clickIt('DayPlan.aspx?day="
                        + HttpUtility.HtmlEncode(d1.Date.ToShortDateString())
                        + "&user=" + HttpUtility.HtmlEncode(_user)
                        + "')\"><a href=\"DayPlan.aspx?day="
                        + HttpUtility.HtmlEncode(d1.Date.ToShortDateString())
                        + "&user=" + HttpUtility.HtmlEncode(_user) + "\">"
                        + d1.Day + "</a></td>" + output.NewLine);
                if (d1.DayOfWeek == DayOfWeek.Sunday)
                    output.Write("</tr>" + output.NewLine);
                d1 = d1.AddDays(1);
                if (d1.DayOfWeek == DayOfWeek.Monday && d1.Month == _date.Month)
                    output.Write("<tr><td>{0}</td>", ++week);
            }
            switch (d1.DayOfWeek)
            {
                case DayOfWeek.Sunday: output.Write("<td></td></tr>"); break;
                case DayOfWeek.Saturday: output.Write("<td></td><td></td></tr>"); break;
                case DayOfWeek.Friday: output.Write("<td></td><td></td><td></td></tr>"); break;
                case DayOfWeek.Thursday: output.Write("<td></td><td></td><td></td><td></td></tr>"); break;
                case DayOfWeek.Wednesday: output.Write("<td></td><td></td><td></td><td></td><td></td></tr>"); break;
                case DayOfWeek.Tuesday: output.Write("<td></td><td></td><td></td><td></td><td></td><td></td></tr>"); break;
            }
            output.Write("</table>" + output.NewLine);
        }

        public List<Data> GetDayPlan(int day)
        {
            List<Data> data = new List<Data>();

            foreach (DataRow dr in _data.Rows)
            {
                if (((DateTime)dr["Date"]).Day == day)
                    data.Add(new Data()
                    {
                        Address = dr["Ttx"].ToString(),
                        DateBeg = (DateTime)dr["Date"],
                        DateEnd = (DateTime)dr["DateEnd"],
                        TpId = dr["TpId"].ToString(),
                        LiftId = dr["LiftId"].ToString(),
                        PlanId = Int32.Parse(dr["PlanId"].ToString()),
                        Done = (bool) dr["Done"]
                    });
            }

            return data;
        }
    }
}