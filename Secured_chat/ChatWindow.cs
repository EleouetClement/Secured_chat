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
    public partial class ChatWindow : Form
    {

        RSASmallKey _receiverKey;

        public ChatWindow(RSASmallKey key)
        {
            _receiverKey = key;
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            Message m = new Message(this.messageBox.Text);
            m.Encrypt(_receiverKey);
            Connexion.GetInstance().SendMessage(this.receiverName.Text, m);
        }
    }
}
