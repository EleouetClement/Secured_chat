using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encryption;

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
            RSASmallKey key = null;
            try
            {
                key = Connexion.GetInstance().GetReceiverKey(this.allConnectedUsers.SelectedItem.ToString());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Chat chatBox = new Chat(this.allConnectedUsers.SelectedItem.ToString(), key);
            ChatManager.GetInstance().AddChat(chatBox);
            chatBox.Show();
            this.Close();
        }
    }
}
