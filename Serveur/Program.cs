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
            //Initalisation du serveur
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = 5;
            _port = 5000;
            _buffer = new byte[1024];
            _connectedUsers = new List<User>();
            SampleUsers();
            //_address = GetIPAddress();
            _address = IPAddress.Loopback;
            Console.WriteLine("> Serveur ecoute sur " + _address.ToString() + " : " + _port.ToString());
            _serverSocket.Bind(new IPEndPoint(_address, _port));
            _serverSocket.Listen(_backlog);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            
            Console.WriteLine("> Arret du service, Pressez une touche pour continuer...");
            Console.ReadLine();
        }


        private static void SampleUsers()
        {
            RSASmallKey key = new RSASmallKey();
            key.SetPublicKey(5424, 6554);
            User us = new User("Jean", key);
            us.SetIp(IPAddress.Parse("192.168.1.1"));
            us.SetListeningPort(8888);
            _connectedUsers.Add(us);
            User us1 = new User("toto", key);
            us1.SetIp(IPAddress.Parse("192.168.1.2"));
            us1.SetListeningPort(9999);
            _connectedUsers.Add(us1);

        }

        public static void StartListening()
        {
            Console.WriteLine("> En Attente de connexion...");
            _serverSocket.Listen(_backlog);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Console.WriteLine("> Nouvelle connexion...");
            Socket socket = _serverSocket.EndAccept(AR);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }



        /// <summary>
        /// Reads data and executes the command.
        /// Command form expected : commandName:arg1[, ...]
        /// </summary>
        /// <param name="AR"></param>
        private static void ReceiveCallBack(IAsyncResult AR)
        {
            try
            {
                Socket socket = (Socket)AR.AsyncState;
                int receivedDataAmount = socket.EndReceive(AR);
                byte[] dataBuff = new byte[receivedDataAmount];
                Array.Copy(_buffer, dataBuff, receivedDataAmount);
                //string text = Encoding.ASCII.GetString(dataBuff);
                string[] command = GetCommand(dataBuff);
                string[] arguments = GetArguments(command[1]);
                switch (command[0])
                {
                    case "user":
                        CreateUser(arguments, socket);
                        break;
                    case "message":
                        SendMessage(arguments, socket); 
                        break;
                    case "refreshList":
                        SendUsersList(socket);
                        break;
                    case "userInfo":
                        SendUserInfo(arguments, socket);
                        break;
                    default:

                        break;
                }
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            }
            catch(SocketException se)
            {
                //Unplanned connexion loss need to remove user from the _connected users list

            }

        }

        private static void UpdateUserList(User us)
        {
            lock (_connectedUsers)
            {
                _connectedUsers.Add(us);
            }
        }

        /// <summary>
        /// Returns the list of the connected user as a string without the one asking for it.
        /// </summary>
        /// <param name="userSocket"></param>
        /// <returns></returns>
        private static string GetUserList(Socket userSocket)
        {
            IPAddress userIp = ((IPEndPoint)userSocket.RemoteEndPoint).Address;

            string userList = string.Empty;
            lock (_connectedUsers)
            {
                foreach(User u in _connectedUsers)
                {
                    if(!(u.IP.Equals(((IPEndPoint)userSocket.RemoteEndPoint).Address) && u.ListeningPort.Equals(((IPEndPoint)userSocket.RemoteEndPoint).Port)))
                    {//Remove current user from the list
                        userList += u.Name + ",";
                    }
                }
            }
            return userList;
        }

        /// <summary>
        /// Send the list of the connectedUsers to the userSocket
        /// </summary>
        /// <param name="userSocket"></param>
        private static void SendUsersList(Socket userSocket)
        {
            string userList = GetUserList(userSocket);
            byte[] data = Encoding.ASCII.GetBytes(userList);
            userSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), userSocket);
        }

        /// <summary>
        /// Add a user to the list of connected clients and send this list back.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userSocket"></param>
        private static void CreateUser(string [] userInfo, Socket userSocket)
        {
            if(userInfo.Length != 4)
            {
                //TO DO...
            }
            User newUser = new User(userInfo[0], GetPublicKey(userInfo));
            newUser.SetIp(((IPEndPoint)userSocket.RemoteEndPoint).Address);
            newUser.SetListeningPort(((IPEndPoint)userSocket.RemoteEndPoint).Port);
            newUser.SetMessagePort(int.Parse(userInfo[3]));
            //Avoid multiple access on user List
            UpdateUserList(newUser);
            //Send back the list of all users
            SendUsersList(userSocket);
        }

        /// <summary>
        /// Send the given message to the receiver using its connection data 
        /// </summary>
        /// <param name="messageInfo">message and receiver's name</param>
        /// <param name="userSocket">sender's socket</param>
        private static void SendMessage(string [] messageInfo, Socket userSocket)
        {
            if(messageInfo.Length != 2)
            {
                //HANDLING TO DO...
                throw new Exception("Wrong command format");
            }
            IPAddress receiverAddress = null;
            string senderName = string.Empty;
            int receiverMessagePort = 0;
            lock(_connectedUsers)
            {
                foreach(User u in _connectedUsers)
                {
                    if(u.Name.Equals(messageInfo[0]))
                    {
                        receiverAddress = u.IP;
                        receiverMessagePort = u.MessagePort;
                        break;
                    }
                    if(u.IP.Equals(((IPEndPoint)userSocket.RemoteEndPoint).Address) || u.ListeningPort.Equals(((IPEndPoint)userSocket.RemoteEndPoint).Port))
                    {
                        senderName = u.Name;
                    }
                }
            }
            byte[] confirmation = Encoding.ASCII.GetBytes("ok");
            if (receiverAddress != null)
            {//Receiver found
                string message = senderName + "," + messageInfo[1];               
                byte [] byteMessage = Encoding.ASCII.GetBytes(message);
                //Setting temporary socket to send message to the client listening port
                Socket messageSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    messageSocket.Connect(new IPEndPoint(receiverAddress, receiverMessagePort));
                    messageSocket.Send(byteMessage, 0, byteMessage.Length, SocketFlags.None);
                    Console.WriteLine("> Envoi de " + message);
                    messageSocket.Disconnect(true);
                    messageSocket.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Impossible d'enviyer le message : " + ex.Message);
                    confirmation = Encoding.ASCII.GetBytes("ko");
                    messageSocket.Disconnect(true);
                    messageSocket.Close();
                }
                //Sends to the sender a confirmation
                userSocket.BeginSend(confirmation, 0, confirmation.Length, SocketFlags.None, new AsyncCallback(SendCallBack), userSocket);
            }
            else
            {
                confirmation = Encoding.ASCII.GetBytes("ko");
                userSocket.BeginSend(confirmation, 0, confirmation.Length, SocketFlags.None, new AsyncCallback(SendCallBack), userSocket);
            }
        }

        /// <summary>
        /// Sends the public key of the given userName, sends "ko" if no user found
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userSocket"></param>
        private static void SendUserInfo(string [] userName, Socket userSocket)
        {
            if(userName.Length != 1)
            {
                //TO DO
            }
            RSASmallKey userKey = null;
            lock(_connectedUsers)
            {
                foreach (User u in _connectedUsers)
                {
                    if (u.Name.Equals(userName[0]))
                    {
                        userKey = u.PublicKey;
                        break;
                    }
                }
            }
            string command = string.Empty;
            if (userKey != null)
            {
                Tuple<int, int> pubkey = userKey.PublicKey;
                command = pubkey.Item1.ToString() + "," + pubkey.Item2.ToString();
            }
            else
            {
                command = "ko";
            }
            byte[] byteMessage = Encoding.ASCII.GetBytes(command);
            userSocket.BeginSend(byteMessage, 0, byteMessage.Length, SocketFlags.None, new AsyncCallback(SendCallBack), userSocket);
        }

        private static void SendCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }

        private static void SendMessageCallBack(IAsyncResult AR)
        {
            //Ends temporary connection after sending a message to a client listening port
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
            socket.Disconnect(false);
        }

        private static RSASmallKey GetPublicKey(string [] data)
        {
            RSASmallKey clientKey = new RSASmallKey();
            
            int e, n;
            if(!int.TryParse(data[1], out e))
            {
                throw new Exception("Mauvais format d'exposant e");
            }
            if (!int.TryParse(data[2], out n))
            {
                throw new Exception("Mauvais format d'exposant n");
            }
            clientKey.SetPublicKey(e, n);
            return clientKey;
        }

        /// <summary>
        /// Return the command value
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string [] GetCommand(byte [] data)
        {
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine("> commande reçu: " + text);
            string[] command = text.Split(':');
            if (command.Length != 2)
            {
                throw new Exception("Mauvais format de commande");
            }
            return command;
        }

        /// <summary>
        /// Returns all arguments of the given string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string [] GetArguments(string data)
        {
            if(data.Contains(","))
            {
                return data.Split(',');
            }
            return new string[] { data };
        }

        /// <summary>
        /// Returns a valid IPv4. returns the loopBack address if no other address found;
        /// </summary>
        /// <returns></returns>
        private static IPAddress GetIPAddress()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            return IPAddress.Loopback;
        }
    }
}
