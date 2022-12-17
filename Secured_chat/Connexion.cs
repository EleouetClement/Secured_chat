using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Encryption;

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
        int _maxAttempts = 5;
        int _bufferSize = 1024;
        static Connexion instance;

        public enum RequestType
        {
            userInfo,
            usersList,
            userConnexion,
            message
        }
        private Connexion()
        {
        }

        public static Connexion GetInstance()
        {
            if(instance == null)
            {
                instance = new Connexion();
            }
            return instance;
        }

        public Socket Socket
        {
            get { return _socket; }
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
                return _maxAttempts;
            }
        }

        public int BufferSize
        {
            get { return _bufferSize; }
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
        /// Send data to the server using the current connexion and waits for the server response
        /// </summary>
        /// <param name="data"></param>
        private string SendServer (byte[] data)
        {
            _socket.Send(data);
            return ReceiveFromServer();
        }

        /// <summary>
        /// Wait for the server to send data.
        /// </summary>
        private string ReceiveFromServer()
        {
            byte[] received = new byte[_bufferSize];
            int reception = _socket.Receive(received);
            byte[] data = new byte[reception];
            Array.Copy(received, data, reception);
            return Encoding.ASCII.GetString(data);
        }


        /// <summary>
        /// Send user informations to the server. the public key is retrieved dynamicaly
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string [] SendUserData(string userName)
        {
            Tuple<int, int> pubKey;
            string result = string.Empty;
            try
            {
                //pubKey = ChatManager.GetInstance().UserKey.PublicKey;
                pubKey = new Tuple<int, int>(26843, 984321);//TO CHANGE
            }catch(NullReferenceException e)
            {
                throw e;
            }
            string[] args = new string[] { userName, pubKey.Item1.ToString(), pubKey.Item2.ToString() };

            try
            {
                result = SendRequest(RequestType.userConnexion, args);
            }catch(Exception e)
            {
                throw new Exception("Erreur lors de l'envoi des informations au server : " + e.Message);
            }
            return result.Split(',');
        }

        /// <summary>
        /// Returns receiver's public key. Throw new Exception in case the request did not ends correctly
        /// </summary>
        /// <param name="receiverName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RSASmallKey GetReceiverKey(string receiverName)
        {
            RSASmallKey receiverKey = new RSASmallKey();
            string result = string.Empty;
            try
            {
                result = SendRequest(RequestType.userInfo, new string[] { receiverName });
            }catch(Exception e)
            {
                throw new Exception("Impossible de récupérer les informations de " + receiverName + " depuis le serveur : " + e.Message);
            }
            string[] key = result.Split(',');
            receiverKey.SetPublicKey(int.Parse(key[0]), int.Parse(key[1]));
            return receiverKey;
        }

        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="message"></param>
        public void SendMessage(string receiver, Message message)
        {
            string [] args  = { receiver, message.Data };
            if(SendRequest(RequestType.message, args) != "ok")
            {
                throw new Exception("Une erreur est survenue, message non distribué");
            }
            //string command = "message:" + receiver + "," + message.Encrypted;
        }

        /// <summary>
        /// Request for the connected users list
        /// </summary>
        public string [] RefreshUserList()
        {
            string response = SendRequest(RequestType.usersList, new string[] { string.Empty });
            return response.Split(',');
        }

        /// <summary>
        /// Sends a specific request to the server
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="argmuents"></param>
        private string SendRequest(RequestType rt, string[] argmuents)
        {

            string command = string.Empty;
            switch(rt)
            {
                case RequestType.userInfo:
                    command = "userInfo:";
                    break;
                case RequestType.usersList:
                    command = "refreshList:";
                    break;
                case RequestType.userConnexion:
                    command = "user:";
                    break;
                case RequestType.message:
                    command = "user:";
                    break;
                default:
                    break;
            }
            command += argmuents[0];
            if (argmuents.Length > 1)
            {               
                for(int i= 1; i < argmuents.Length; i++)
                {
                    command += "," + argmuents[i];
                }
            }
            byte[] data = Encoding.ASCII.GetBytes(command);
            return SendServer(data);
        }

        
    }
}
