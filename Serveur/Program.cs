using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveur
{
    internal class Program
    {

        private static List<User> _connectedUsers;

        private static byte[] _buffer;
        private static Socket _serverSocket;


        /// <summary>
        /// Maximum number of pending connections
        /// </summary>
        private static int _backlog;

        /// <summary>
        /// Port number the services needs to listen to. Default value is 5000
        /// </summary>
        private static int _port;

        /// <summary>
        /// The IP address the server needs to use. default address is IPAddress.Any 
        /// </summary>
        private static IPAddress _address;




        /// <summary>
        /// Serveur initialization and listening startup
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Service chatUserManager = new Service(5);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = 5;
            _port = 5000;
            _buffer = new byte[1024];
            _connectedUsers = new List<User>();
            _address = IPAddress.Loopback;
            _serverSocket.Bind(new IPEndPoint(_address, _port));
            _serverSocket.Listen(_backlog);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);


            Console.WriteLine("Arret du service, Pressez une touche pour continuer...");
            Console.ReadLine();
        }

        public static void StartListening()
        {
            Console.WriteLine("En Attente de connexion...");
            _serverSocket.Listen(_backlog);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Console.WriteLine("Nouvelle connexion...");
            Socket socket = _serverSocket.EndAccept(AR);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int receivedDataAmount = socket.EndReceive(AR);
            byte[] dataBuff = new byte[receivedDataAmount];
            Array.Copy(_buffer, dataBuff, receivedDataAmount);
            string text = Encoding.ASCII.GetString(dataBuff);
            User newUser = new User();
            newUser.SetSocket(socket);
            _connectedUsers.Add(newUser);
            Console.WriteLine("Text received: " + text);


            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
        }
    }
}
