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
        Dictionary<string, Chat> _chats;

        int _port;
        int _backlog;
        IPAddress _ip;
        RSASmallKey _userKey;

        private Thread _thread;
        private bool _isListening = false;

        static ChatManager Instance;
        private ChatManager()
        {
            _chats = new Dictionary<string, Chat>();            
        }

        public static ChatManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ChatManager();
            }
            return Instance;
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
                    _thread = new Thread(new ThreadStart(this.ListenForMessages));
                    _thread.IsBackground = true;
                    _thread.Start();
                    _isListening = true;
                }catch (Exception ex)
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
            Chat chat = null;
            byte [] received = new byte[Connexion.GetInstance().BufferSize];
            while (true)
            {
                userSocket.Accept();
                int reception = userSocket.Receive(received);
                byte[] data = new byte[reception];
                Array.Copy(received, data, reception);
                string message = Encoding.ASCII.GetString(data);
                string [] messageInfo = message.Split(',');
                lock(_chats)
                {
                    Message newMessage = new Message(messageInfo[1]);
                    if(_chats.TryGetValue(messageInfo[0], out chat))
                    {
                        chat.AddMessage(newMessage);
                    }
                    else
                    {
                        chat = new Chat(messageInfo[0]);
                        chat.AddMessage(newMessage);
                        _chats.Add(messageInfo[0], chat);
                        chat.Show();
                        chat.SetBoxName();
                    }
                }
            }
        }

    }
}
