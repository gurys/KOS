using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KosMail
{
    public partial class Pop3set : Form
    {
        Config _config;
        List<Config.Server> _servers = new List<Config.Server>();

        public Pop3set(Config config)
        {
            InitializeComponent();

            _config = config;
            _servers = config.GetPop3();
            foreach (Config.Server srv in _servers)
                Servers.Items.Add(srv.Address);
            Servers.ItemCheck += Servers_ItemCheck;
            Servers.SelectedIndexChanged += Servers_SelectedIndexChanged;
        }

        void Servers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Servers.SelectedIndex >= 0)
                Show.Enabled = true;
            else
                Show.Enabled = false;
        }

        void Servers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Servers.CheckedItems.Count > 0 || e.NewValue == CheckState.Checked)
                Delete.Enabled = true;
            else
                Delete.Enabled = false;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Servers.CheckedIndices.Count; i++)
            {
                _servers.RemoveAt(Servers.CheckedIndices[i]);
                Servers.Items.RemoveAt(Servers.CheckedIndices[i]);
            }
            _config.SetPop3(_servers);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            AddServer form = new AddServer(_servers);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Servers.Items.Clear();
                foreach (Config.Server srv in _servers)
                    Servers.Items.Add(srv.Address);
                _config.SetPop3(_servers);
            }
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (Servers.SelectedIndex >= 0)
            {
                AddServer form = new AddServer(_servers, Servers.SelectedIndex);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Servers.Items.Clear();
                    foreach (Config.Server srv in _servers)
                        Servers.Items.Add(srv.Address);
                    _config.SetPop3(_servers);
                }
            }
        }
    }
}
