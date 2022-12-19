using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encryption;


namespace Secured_chat
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        private static Socket _clientsocket;

        [STAThread]
        static void Main()
        {
            InitializeConnection();
            InitializeChatManager();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }

        /// <summary>
        /// Set up the network variables
        /// </summary>
        private static void InitializeConnection()
        {
            Connexion c = Connexion.GetInstance();
            c.CreateSocket();
        }

        private static void InitializeChatManager()
        {
            ChatManager chatManager = ChatManager.GetInstance();
            chatManager.SetNetwotkInfo();
            RSASmallKey key = new RSASmallKey();
            key.CreateKeys(23541, 35532, 15542);
            chatManager.SetKey(key);
        }
    }
}
