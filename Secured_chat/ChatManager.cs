using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secured_chat
{
    internal class ChatManager
    {
        Dictionary<string, Chat> _chats;

        private ChatManager()
        {
            _chats = new Dictionary<string, Chat>();
        }

        public static ChatManager Instance;

        public static ChatManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ChatManager();
            }
            return Instance;
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
