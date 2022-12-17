using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Secured_chat
{

    /// <summary>
    /// Set of tools to manage network connection with the server and other users
    /// Careful, it is a singleton !
    /// </summary>
    class Connexion
    {
        Socket _socket;
        IPAddress _serverAddress;
        int _portNumber;
        int maxAttempts = 5;
        static Connexion instance;


        public static Connexion GetInstance()
        {
            if(instance == null)
            {
                instance = new Connexion();
            }
            return instance;
        }

        public void CreateSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void TryOpenServerConnection()
        {
            if(_serverAddress == null)
            {
                throw new NullReferenceException("Server ip address not set correctly");
            }
            if(_portNumber == 0)
            {
                throw new NullReferenceException("Server port numver not set correctly");
            }
            try
            {
                _socket.Connect(_serverAddress, _portNumber);
            } catch (SocketException)
            {
                
            }
        }

        public int MaxAttempts
        {
            get
            {
                return maxAttempts;
            }
        }

        public void SetServerIp(IPAddress address)
        {
            _serverAddress = address;
        }

        public void SetServerPort(int port)
        {
            _portNumber = port;
        }

        public bool IsConnected
        {
            get
            {
                return _socket.Connected;
            }
        }

        /// <summary>
        /// Send data to the server using the current connexion
        /// </summary>
        /// <param name="data"></param>
        public void SendServer (byte[] data)
        {
            _socket.Send(data);
        }

        /// <summary>
        /// Wait for the server to send data.
        /// </summary>
        public string ReceiveFromServer()
        {
            byte[] received = new byte[1024];
            int reception = _socket.Receive(received);
            byte[] data = new byte[reception];
            Array.Copy(received, data, reception);
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="message"></param>
        public void SendMessage(string receiver, Message message)
        {
            string command = "message:" + receiver + "," + message.Data;
            //string command = "message:" + receiver + "," + message.Encrypted;
            byte [] data = Encoding.ASCII.GetBytes(command);
            SendServer(data);
        }
    }
}
