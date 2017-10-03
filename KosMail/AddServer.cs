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
    public partial class AddServer : Form
    {
        List<Config.Server> _list;
        int _index = -1;

        public AddServer(List<Config.Server> list)
        {
            InitializeComponent();
            _list = list;
        }

        public AddServer(List<Config.Server> list, int index)
        {
            InitializeComponent();
            _list = list;
            _index = index;
            Config.Server item = _list[_index];
            Address.Text = item.Address;
            User.Text = item.User;
            Password.Text = item.Password;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Config.Server item = new Config.Server()
            {
                Address = Address.Text,
                User = User.Text,
                Password = Password.Text
            };
            if (_index < 0)
                _list.Add(item);
            else
                _list[_index] = item;
        }
    }
}
