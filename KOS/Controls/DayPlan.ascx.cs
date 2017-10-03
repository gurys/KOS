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
    public partial class DayPlan : System.Web.UI.UserControl
    {
        DateTime _date;
        App_Code.Base.UserInfo _userInfo;
        DataTable _data;
        string _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["day"]))
                _date = DateTime.Now.Date;
            else
                _date = DateTime.Parse(HttpUtility.HtmlDecode(Request["day"]));
            DateTime d2 = _date.AddDays(1);
            _user = Page.User.Identity.Name;
            if (!string.IsNullOrEmpty(Request["user"]))
                _user = HttpUtility.HtmlDecode(Request["user"]);

            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _userInfo = db.GetUserInfo(_user);
            _data =  db.GetPlan(_user, _date, d2);
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(_date.ToLongDateString() +
                "<br /><table border=1><thead><tr><td style=\"width: 200px;\">Адрес</td><td>№ лифта</td>");
            for (int i = Math.Min(_userInfo.HourBeg, 8); i <= Math.Max(_userInfo.HourEnd, 20); i++)
                output.Write("<td>" + i.ToString() + "</td>");
            output.Write("</tr></thead>" + output.NewLine);

            List<MonthPlan.Data> data = GetDayPlan();
            foreach (MonthPlan.Data d in data)
            {
                output.Write("<tr>");
                output.Write("<td>" + d.Address + "</td><td ");
                output.Write("onclick=\"clickIt('Reglament.aspx?PlanId={0}&ret={2}')\"><a href=\"Reglament.aspx?PlanId={0}&ret={2}\">{1}</a></td>",
                    d.PlanId, d.LiftId, HttpUtility.HtmlEncode(Request.Url));
                for (int i = Math.Min(_userInfo.HourBeg, 8); i <= Math.Max(_userInfo.HourEnd, 20); i++)
                    if (i >= d.DateBeg.Hour && i < d.DateEnd.Hour)
                    {
                        if (i == _userInfo.Lunch)
                            output.Write("<td bgcolor=\"yellowgreen\">");
                        else if (!d.Done && d.TpId != "ВР")
                            output.Write("<td bgcolor=\"yellow\">");
                        else if (!d.Done && d.TpId == "ВР")
                            output.Write("<td bgcolor=\"orange\">");
                        else
                            output.Write("<td bgcolor=\"dodgerblue\">");
                        output.Write("{0}</td>", d.TpId);
                    }
                    else if (i == _userInfo.Lunch || i >= _userInfo.HourEnd)
                        output.Write("<td bgcolor=\"yellowgreen\"></td>");
                    else
                        output.Write("<td></td>");
                output.Write("</tr>" + output.NewLine);
            }
            output.Write("</table>");
        }

        List<MonthPlan.Data> GetDayPlan()
        {
            List<MonthPlan.Data> data = new List<MonthPlan.Data>();

            foreach (DataRow dr in _data.Rows)
            {
                data.Add(new MonthPlan.Data()
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