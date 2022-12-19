using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Encryption;

namespace Secured_chat
{
    /// <summary>
    /// Manages all chat boxes and listen to incoming messages
    /// </summary>
    internal class ChatManager
    {
        static Dictionary<string, Chat> _chats;

        int _port;
        int _backlog;
        IPAddress _ip;
        RSASmallKey _userKey;
        private bool _isListening = false;
        byte[] _buffer;
        static ChatManager Instance;
        private ChatManager()
        {
            _chats = new Dictionary<string, Chat>();    
            _buffer = new byte[1024];
        }

        public static ChatManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ChatManager();
            }
            return Instance;
        }

        public static Dictionary<string, Chat> AllChats
        { 
            get { return _chats; } 
        }
        /// <summary>
        /// Sets the listening socket informations
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ip"></param>
        public void SetNetwotkInfo(int port = 25555, IPAddress ip = null, int backlog=2)
        {
            _port = port;
            _backlog = backlog;
            if (ip != null)
            {
                _ip = ip;
            }
            else
            {
                _ip = IPAddress.Loopback;
            }
        }

        public int Port
        {
            get { return _port; }
        }

        public IPAddress IP
        {
            get { return _ip; }
        }

        public RSASmallKey UserKey
        {
            get { return _userKey; }
        }

        public void SetKey(RSASmallKey key)
        {
            _userKey = key;
        }

        /// <summary>
        /// Add the chat in the manager's dictionnary
        /// </summary>
        /// <param name="chat"></param>
        public void AddChat(Chat chat)
        {
            if(!_chats.ContainsKey(chat.Receiver))
                _chats.Add(chat.Receiver, chat);
        }

        /// <summary>
        /// Launch a thread task to listen for any messages coming
        /// </summary>
        public void StartListening()
        {
            if(!_isListening)
            {
                try
                {
                    ListenForMessages();
                }
                catch (Exception ex)
                {
                    _isListening = false;
                    throw new Exception("Impossible de recevoir des messages Erreur : " + ex.Message);
                }
            }
        }
        
        /// <summary>
        /// Listen to incomming messages and send it to the correct chatboxes
        /// </summary>
        private void ListenForMessages()
        {
            Socket userSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            userSocket.Bind(new IPEndPoint(_ip, _port));
            userSocket.Listen(_backlog);
            userSocket.BeginAccept(new AsyncCallback(AcceptCallback), userSocket);

        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket server = (Socket)AR.AsyncState;
            Socket socket = server.EndAccept(AR);           
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
            server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        /// <summary>
        /// Defines the operations to do when a message comes
        /// </summary>
        /// <param name="AR"></param>
        private void ReceiveCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int receivedData = socket.EndReceive(AR);
            byte[] buffer = new byte[receivedData];
            Array.Copy(_buffer, buffer, receivedData);
            string stringData = Encoding.ASCII.GetString(buffer);
            string[] message = stringData.Split(',');
            if(message.Length != 2)
            {

            }
            else
            {
                lock(_chats)
                {
                    Chat chat = null;
                    Message m = new Message(message[1]);
                    if (!_chats.TryGetValue(message[0], out chat))
                    {
                        chat = new Chat(message[0]);                                              
                        chat.AddMessage(m);
                        chat.RequestShow();
                        _chats.Add(message[0], chat);

                    }
                    else
                    {
                        chat.RequestShow();
                        chat.AddMessage(m);
                    }
                }
            }
        }



    }
}
