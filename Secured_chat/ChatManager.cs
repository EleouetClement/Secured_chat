using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption;

namespace Secured_chat
{
    internal class ChatManager
    {
        Dictionary<string, Chat> _chats;

        RSASmallKey _userKey;


        public static ChatManager Instance;
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


    }
}
