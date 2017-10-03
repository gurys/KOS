using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pop3;
using System.Collections;

namespace KosMail
{
    public partial class Form1 : Form
    {
        class Letter
        {
            public string Subject { get; set; }
            public string From { get; set; }
            public string To { get; set; }
        }

        Config _config;
        List<Letter> _inbox = new List<Letter>();

        public Form1()
        {
            InitializeComponent();
            _config = new Config(Application.UserAppDataPath);
        }

        private void получитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Config.Server> list = _config.GetPop3();
            foreach (Config.Server srv in list)
            {
                try
                {
                    Pop3Client email = new Pop3Client(srv.User, srv.Password, srv.Address);
                    email.OpenInbox();

                    while (email.NextEmail())
                    {
                        _inbox.Add(new Letter()
                        {
                            Subject = email.Subject,
                            From = email.From,
                            To = email.To
                        });
                        email.DeleteEmail();
                    }

                    email.CloseConnection();
                }
                catch (Pop3LoginException)
                {
                    string msg = "Сервер " + srv.Address + " не доступен";
                    MessageBox.Show(msg);
                }
                catch (Pop3ConnectException)
                {
                    string msg = "Сервер " + srv.Address + " не доступен";
                    MessageBox.Show(msg);
                }
            }
            UpdateInbox();
        }

        void UpdateInbox()
        {
        }

        private void pOP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pop3set form = new Pop3set(_config);
            if (form.ShowDialog() == DialogResult.OK)
                _config.Save();
        }

        private void sMTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SmtpSet form = new SmtpSet(_config);
            if (form.ShowDialog() == DialogResult.OK)
                _config.Save();
        }
    }
}
