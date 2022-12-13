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
        /// Initiate connexion to the server
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
                this.attempts.Text = "Mauvais numéro de port";
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

            string command = "user:Clement,684351, 89684321";
            connexion.Send_server(Encoding.ASCII.GetBytes(command));
            byte [] users = connexion.ReceiveFromServer();
            string result = Encoding.ASCII.GetString(users);
        }
    }
}
