using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Secured_chat
{
    public partial class Form1 : Form
    {
        private Connexion connexion;
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initiate connexion to the server and start listening for Messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connexion_Click(object sender, EventArgs e)
        {
            int attemptsConnexion = 0;
            connexion = Secured_chat.Connexion.GetInstance();
            string ip = ipAddress.Text;
            int port = 0;
            IPAddress ipad; 
            if(!IPAddress.TryParse(ip, out ipad))
            {
                this.attempts.Text = "Mauvaise adresse ip";
                return;
            }
            connexion.SetServerIp(ipad);
            if(!int.TryParse(this.portNumber.Text, out port))
            {
                this.attempts.Text = "Mauvais numéro de port serveur";
                return;
            }
            connexion.SetServerPort(port);

            while(!connexion.IsConnected && attemptsConnexion < connexion.MaxAttempts)
            {
                attemptsConnexion++;
                this.attempts.Text = attemptsConnexion.ToString();
                connexion.TryOpenServerConnection();
            }
            if(!connexion.IsConnected)
            {
                this.attempts.Text = "Impossible de se connecter au seveur";
            }
            else
            {
                this.attempts.Text = "Connecté au serveur";
            }
            int listening = 0;
            if (!int.TryParse(this.listeningPort.Text, out listening))
            {
                this.attempts.Text = "Mauvais numéro de port d'ecoute de message";
                return;
            }
            ChatManager cm = ChatManager.GetInstance();
            cm.SetNetwotkInfo(port = listening);
            string[] userTab = connexion.SendUserData(this.pseudovalue.Text);
            UsersList connected = new UsersList();
            foreach(string user in userTab)
            {
                connected.allConnectedUsers.Items.Add(user);
            }
            connected.Text = this.pseudovalue.Text;
            connected.Show();
            //Start Listening process
            try
            {                
                cm.StartListening();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hide();//Hiding connection interface
        }
    }
}
