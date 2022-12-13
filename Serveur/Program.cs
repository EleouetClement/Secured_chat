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
            Service chatUserManager = new Service(5);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _backlog = 5;
            _port = 5000;
            _buffer = new byte[1024];
            _connectedUsers = new List<User>();
            //_address = GetIPAddress();
            _address = IPAddress.Loopback;
            Console.WriteLine("Serveur ecoute sur " + _address.ToString() + " : " + _port.ToString());
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
                    case "receiver":

                        break;

                    default:

                        break;
                }
                User newUser = new User();
                newUser.SetSocket(socket);
                _connectedUsers.Add(newUser);

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

        private static string GetUserList()
        {
            string userList = string.Empty;
            lock (_connectedUsers)
            {
                _connectedUsers.ForEach(u => userList += u.Name + ",");
            }
            return userList;
        }

        private static void CreateUser(string [] userInfo, Socket userSocket)
        {
            if(userInfo.Length != 3)
            {
                //TO DO...
            }
            User newUser = new User(userInfo[0], GetPublicKey(userInfo));
            //Avoid multiple access on user List
            UpdateUserList(newUser);
            //Send back the list of all users
            string userList = GetUserList();
            byte[] data = Encoding.ASCII.GetBytes(userList);
            userSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), userSocket);
        }


        private static void SendCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
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
            Console.WriteLine("commande reçu: " + text);
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
