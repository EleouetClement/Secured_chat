using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encryption;

namespace Secured_chat
{
    public partial class UsersList : Form
    {
        Thread _thread;
        public UsersList()
        {
            InitializeComponent();
            SetWindowsThread();
        }

        private void SetWindowsThread()
        {
            _thread = new Thread(CheckWindows);
            _thread.IsBackground = true;
            _thread.Name = "Windows manager";
            _thread.Start();
        }


        private void CheckWindows()
        {
            while(true)
            {
                lock(ChatManager.AllChats)
                {
                    foreach(KeyValuePair<string, Chat> c in ChatManager.AllChats)
                    {
                        if(c.Value.ToShow)
                        {
                            c.Value.Show();
                        }
                    }
                }
                Thread.Sleep(2000);
            }
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
            chatBox.SetBoxName();          
        }

        private void refreshList_Click(object sender, EventArgs e)
        {
            this.allConnectedUsers.Items.Clear();
            this.allConnectedUsers.Items.AddRange(Connexion.GetInstance().RefreshUserList());
        }
    }
}
