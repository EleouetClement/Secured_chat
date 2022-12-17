using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Encryption;

namespace Serveur
{
    /// <summary>
    /// Defines a user logged in the server
    /// </summary>
    internal class User
    {
        private string _name;
        private RSASmallKey _publicKey;
        private IPAddress  _address;
        private int _messagePort;
        private int _listeningPort;
        private Socket _clientSocket;
        public User()
        { 
        }

        public User(string name, RSASmallKey publicKey)
        {
            _name = name;
            _publicKey = publicKey;
        }

        public IPAddress IP
        {
            get { return _address; }
        }

        [Obsolete]
        public Socket Socket
        {
            get
            {
                return _clientSocket;
            }
        }

        /// <summary>
        /// Port used to send message to.
        /// </summary>
        public int MessagePort
        {
            get
            {
                return _messagePort;
            }
        }

        /// <summary>
        /// Port used to communicate requests to the server
        /// </summary>
        public int ListeningPort
        {
            get { return _listeningPort; }
        }


        public void SetSocket(Socket socket)
        {
            _clientSocket = socket;
        }

        /// <summary>
        /// Set the port on which messages will be send
        /// </summary>
        /// <param name="port"></param>
        public void SetMessagePort(int port)
        {
            _messagePort = port;
        }

        /// <summary>
        /// Set ip and port used for connexion with the server
        /// </summary>
        /// <param name="address"></param>
        public void SetIp(IPAddress add)
        {
            _address = add;
        }

        public void SetListeningPort(int port)
        {
            _listeningPort = port;
        }

        public string Name
        {
            get { return _name; }
        }

        public RSASmallKey PublicKey
        {
            get { return _publicKey; }
        }

    }
}
