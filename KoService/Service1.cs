using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Collections.Specialized;
using KosGSM;

namespace KoService
{
    public partial class Service1 : ServiceBase
    {
        string _conn;
        Timer _timer;

        public Service1()
        {
            InitializeComponent();

            _conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        protected override void OnStart(string[] args)
        {
            int period = int.Parse(ConfigurationManager.AppSettings["period"]);
            _timer = new Timer(Process, null, 0, period);
        }

        public void Process(Object stateInfo)
        {
            GSM gsm = new GSM(_conn);
            gsm.SendAll();
        }

        protected override void OnStop()
        {
            _timer.Dispose();
        }
    }
}
