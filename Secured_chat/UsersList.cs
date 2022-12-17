using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Secured_chat
{
    public partial class UsersList : Form
    {
        public UsersList()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Chat chatBox = new Chat(this.allConnectedUsers.SelectedItem.ToString());
            ChatManager.GetInstance().AddChat(chatBox);
            chatBox.Show();
            this.Close();
        }
    }
}
