using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace KOS.Controls
{
    public partial class Works : System.Web.UI.UserControl
    {
        List<App_Code.Base.Work> _data = new List<App_Code.Base.Work>();
        DateTime _beg, _end;
        List<string> _IdU = new List<string>();
        string[] _colors = { "red", "orange", "yellow", "green", "blue", "violet", "olive", "navy" };

        protected void Page_Load(object sender, EventArgs e)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _IdU = db.GetIdU();
        }

        public void SetPeriod(DateTime beg, DateTime end)
        {
            App_Code.Base db = new App_Code.Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _data = db.GetWorks(beg, end, Page.Request.RawUrl);
            _beg = beg;
            _end = end;
        }

        string GetIdU(List<App_Code.Base.Work> list)
        {
            string s = string.Empty;
            foreach (App_Code.Base.Work w in list)
                if (!string.IsNullOrEmpty(s) && w.IdU != s)
                    return string.Empty;
                else
                    s = w.IdU;
            return s;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<table style=\"border:solid\"><tr><td rowspan=\"2\">Механик</td>");
            DateTime d = _beg;
            do
            {
                if (d.Year == _beg.Year && d.Month == _beg.Month)
                    output.Write("<td colspan=\"{0}\" style=\"text-align:center\">{1} {2}</td>", DateTime.DaysInMonth(_beg.Year, _beg.Month) - d.Day + 1,
                        App_Code.Base.months[_beg.Month - 1], _beg.Year.ToString());
                else if (d.Year == _end.Year && d.Month == _end.Month)
                    output.Write("<td colspan=\"{0}\" style=\"text-align:center\">{1} {2}</td>", _end.Day, App_Code.Base.months[_end.Month - 1], d.Year.ToString());
                else
                    output.Write("<td colspan=\"{0}\" style=\"text-align:center\">{1} {2}</td>", DateTime.DaysInMonth(d.Year, d.Month),
                        App_Code.Base.months[d.Month - 1], d.Year.ToString());
                d = d.AddMonths(1);
            } while (d.Year < _end.Year || (d.Year == _end.Year && d.Month <= _end.Month));
            output.Write("</tr><tr>");
            for (d = _beg; d <= _end; d = d.AddDays(1))
                output.Write("<td>{0}</td>", d.Day);
            output.Write("</tr>");
            string worker = string.Empty;
            foreach (App_Code.Base.Work w in _data)
            {
                if (worker != w.Worker)
                {
                    output.Write("<tr><td>{0}</td>", w.Worker);
                    worker = w.Worker;
                }
                else continue;
                for (d = _beg; d <= _end; d = d.AddDays(1))
                {
                    List<App_Code.Base.Work> list = _data.FindAll(delegate(App_Code.Base.Work w2)
                    {
                        return w2.Worker == w.Worker && w2.Date.Date == d.Date;
                    });
                    string IdU = GetIdU(list);
                    if (!string.IsNullOrEmpty(IdU))
                    {
                        int j = _IdU.FindIndex(delegate(string s)
                        {
                            return s == IdU;
                        });
                        output.Write("<td  style=\"background-color:{0}\">", _colors[j % _colors.Length]);
                    }
                    else
                        output.Write("<td>");
                    foreach (App_Code.Base.Work wo in list)
                        output.Write("<a href=\"{0}\">{1}</a> ", wo.Url, wo.LiftId);
                    output.Write("</td>");
                }
                output.Write("</tr>");
            }
            output.Write("</table>");
        }
    }
}