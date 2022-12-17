﻿using System;
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

        public IPAddress Address
        {
            get { return ((IPEndPoint)_clientSocket.RemoteEndPoint).Address; }
        }

        public Socket Socket
        {
            get
            {
                return _clientSocket;
            }
        }

        public int ListeningPort
        {
            get { return _listeningPort; }
        }

        public void SetSocket(Socket socket)
        {
            _clientSocket = socket;
        }

        public void SetPort(int port)
        {
            _listeningPort = port;
        }

        /// <summary>
        /// For test ONLY!
        /// </summary>
        /// <param name="address"></param>
        public void SetTestIp(IPAddress address)
        {
            _address = address;
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
