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

        private List<User> _connectedUsers;
        
        private Socket _serverSocket;


        /// <summary>
        /// Maximum number of pending connections
        /// </summary>
        private int _backlog;

        /// <summary>
        /// Port number the services needs to listen to. Default value is 5000
        /// </summary>
        private int _port;

        /// <summary>
        /// The IP address the server needs to use. default address is IPAddress.Any 
        /// </summary>
        private IPAddress _address;
        public Service(int backlog, int port = 5000)
        {
            SetUpServer(backlog, port);
            _address = IPAddress.Any;
        }
        public Service(int backlog, IPAddress address, int port = 5000)
        {
            SetUpServer(backlog, port);
            _address = address;
        }

        /// <summary>
        /// Basing variables initialization
        /// </summary>
        /// <param name="backlog"></param>
        /// <param name="port"></param>
        private void SetUpServer(int backlog, int port)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = backlog;
            _port = port;
            _connectedUsers = new List<User>();
        }

        /// <summary>
        /// Set up the serveur listening configuration
        /// </summary>
        /// <param name="portNumber"></param>
        public void InitializeServer()
        {
            Console.WriteLine("Setting up server");
            _serverSocket.Bind(new IPEndPoint(_address, _port));
        }

        /// <summary>
        /// Starts listenings for clients
        /// </summary>
        public void StartListening()
        {
            _serverSocket.Listen(_backlog);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            User newUser = new User();
            _connectedUsers.Add();
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }


    }
}
