using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveur
{
    /// <summary>
    /// Defines the server services
    /// </summary>
    internal class Service
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
        public Service(int backlog, int port = 5000, int bufferSize=1024)
        {
            SetUpServer(backlog, port, bufferSize);
            _address = IPAddress.Loopback;
        }
        public Service(int backlog, IPAddress address, int port = 5000, int bufferSize = 1024)
        {
            SetUpServer(backlog, port, bufferSize);
            _address = IPAddress.Loopback;
        }

        /// <summary>
        /// Basing variables initialization
        /// </summary>
        /// <param name="backlog"></param>
        /// <param name="port"></param>
        private static void SetUpServer(int backlog, int port , int bufferSize)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = backlog;
            _port = port;
            _buffer = new byte[bufferSize];
            _connectedUsers = new List<User>();
        }

        /// <summary>
        /// Set up the serveur listening configuration
        /// </summary>
        /// <param name="portNumber"></param>
        public static void InitializeServer()
        {
            Console.WriteLine("Initialisation du serveur");
          _serverSocket.Bind(new IPEndPoint(_address, _port));
           


        }

        /// <summary>
        /// Starts listenings for clients
        /// </summary>
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
