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
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = backlog;
            _port = port;
            _address = IPAddress.Any;

        }
        public Service(int backlog, IPAddress address, int port = 5000)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = backlog;
            _port = port;
            _address = address;
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
        }


    }
}
