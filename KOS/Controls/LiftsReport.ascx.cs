using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using KOS.App_Code;

namespace KOS.Controls
{
    public partial class LiftsReport : System.Web.UI.UserControl
    {
        DateTime _beg = DateTime.Now.AddMonths(-1), _end = DateTime.Now;
        List<string> _lifts = new List<string>();
        public static List<string> _types = new List<string>(){ "Всё", "Инциденты", "Замечания", "ТР", "ОС", "ВР" };
        string _type = _types[0];
        List<Base.LiftRep> _data = new List<Base.LiftRep>();
        string _role = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Set(List<string> lifts, DateTime beg, DateTime end, string type, string role)
        {
            _role = role;
            _beg = beg;
            _end = end;
            _lifts = lifts;
            _type = type;
            _data.Clear();
            Base db = new Base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            switch (_type)
            {
                case "Инциденты": db.GetIncidents(_beg, _end, ref _data); break;
                case "Замечания": db.GetPrim(_beg, _end, ref _data); break;
                case "ТР": db.GetTP(_beg, _end, ref _data, Page.Request.RawUrl); break;
                case "ОС": db.GetOC(_beg, _end, ref _data, Page.Request.RawUrl); break;
                case "ВР": db.GetBP(_beg, _end, ref _data, Page.Request.RawUrl); break;
                default:
                    db.GetIncidents(_beg, _end, ref _data);
                    db.GetPrim(_beg, _end, ref _data);
                    db.GetTP(_beg, _end, ref _data, Page.Request.RawUrl);
                    db.GetOC(_beg, _end, ref _data, Page.Request.RawUrl);
                    db.GetBP(_beg, _end, ref _data, Page.Request.RawUrl);
                    break;
            }
            for (int i = 0; i < _data.Count; i++)
                if (string.IsNullOrEmpty(_lifts.Find(delegate(string s)
                {
                    return s == _data[i].LiftId;
                }))) 
                    _data.RemoveAt(i--);
            _data.Sort(delegate(Base.LiftRep lr1, Base.LiftRep lr2)
            {
                int c = string.Compare(lr1.LiftId, lr2.LiftId);
                if (c != 0) return c;
                if (lr1.Date < lr2.Date) return -1;
                if (lr1.Date > lr2.Date) return 1;
                return 0;
            });
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<table class=\"tabdate\" style=\"border:solid\"><tr><td class=\"tddate\" rowspan=\"2\">Лифт:</td>");
            DateTime d = _beg;
            do
            {
                if (d.Year == _beg.Year && d.Month == _beg.Month)
                    output.Write("<td class=\"tddate\" colspan=\"{0}\" style=\"text-align:center\">{1} {2}</td>", DateTime.DaysInMonth(_beg.Year, _beg.Month) - d.Day + 1,
                        Base.months[_beg.Month - 1], _beg.Year.ToString());
                else if (d.Year == _end.Year && d.Month == _end.Month)
                    output.Write("<td class=\"tddate\" colspan=\"{0}\" style=\"text-align:center\">{1} {2}</td>", _end.Day, Base.months[_end.Month - 1], d.Year.ToString());
                else
                    output.Write("<td class=\"tddate\" colspan=\"{0}\" style=\"text-align:center\">{1} {2}</td>", DateTime.DaysInMonth(d.Year, d.Month),
                        Base.months[d.Month - 1], d.Year.ToString());
                d = d.AddMonths(1);
            } while (d.Year < _end.Year || (d.Year == _end.Year && d.Month <= _end.Month));
            output.Write("</tr><tr>");
            for (d = _beg; d <= _end; d = d.AddDays(1))
                output.Write("<td bgcolor=\"WhiteSmoke\">{0}</td>", d.Day);
            output.Write("</tr>");
            foreach (string liftId in _lifts)
            {
                output.Write("<tr><td bgcolor=\"WhiteSmoke\">{0}</td>", liftId);
                for (d = _beg; d <= _end; d = d.AddDays(1))
                {
                    List<Base.LiftRep> list = _data.FindAll(delegate(Base.LiftRep lr)
                    {
                        return lr.LiftId == liftId && lr.Date.Date == d.Date.Date;
                    });
                    if (list.Count == 1)
                    {
                        if (list[0].Done)
                            output.Write("<td bgcolor=\"dodgerblue\">");
                        else
                            output.Write("<td bgcolor=\"tomato\">");
                    }
                    else
                        output.Write("<td class=\"tddate2\">"); // td class=\"tddate2\
                    foreach (Base.LiftRep lr in list)
                        output.Write("<a class=\"adate\" href=\"{0}\">{1}</a> ", _role == "ODS" ? "#" : lr.Url, lr.Title);
                    output.Write("</td>");
                }
                output.Write("</tr>");
            }
            output.Write("</table>");
        }
    }
}