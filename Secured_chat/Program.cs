using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            _clientsocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            LoopConnect();
            //Console.ReadLine();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }

        private static void LoopConnect()
        {
            int attempts = 0;


            while (!_clientsocket.Connected) {
                try {
                    attempts++;
                    _clientsocket.Connect(System.Net.IPAddress.Loopback, 5000);
                } catch (SocketException)
                {

              //      Console.Clear();
                    //Console.WriteLine("connexion.....: "+attempts.ToString());
                }
            }
      //      Console.Clear();
            //Console.WriteLine("connected !!!!!");
            
        }
    }
}
