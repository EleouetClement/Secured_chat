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
            get { return _address; }
        }

        public void SetAddress(Socket socket)
        {
            _address = ((IPEndPoint)socket.RemoteEndPoint).Address;
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
