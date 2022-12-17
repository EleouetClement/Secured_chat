using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption;

namespace Secured_chat
{
    internal class Chat
    {
        string _receiver;
        RSASmallKey _receiverKey;
        List<Message> _messages;
        ChatWindow _window;

        public Chat(string receiver)
        {
            _receiver = receiver;
        }

        public Chat(string receiver, RSASmallKey receiverKey)
        {
            _receiver = receiver;
            _receiverKey = receiverKey;
            _messages = new List<Message>();
        }

        /// <summary>
        /// True if the form is created
        /// </summary>
        public bool WindowOn
        {
            get
            {
                return _window != null;
            }
        }

        public string Receiver
        {
            get { return _receiver; }
        }

        public RSASmallKey ReceiverKey
        {
            get { return _receiverKey; }
        }

        /// <summary>
        /// Opens the chat Window
        /// </summary>
        public void Show()
        {
            if(!WindowOn)
            {
                _window = new ChatWindow(_receiverKey);
                _window.receiverName.Text = Receiver;
            }
            _window.Show();

        }
        /// <summary>
        /// Add a message to the list in this discussion
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(Message message)
        {
            _messages.Add(message);
        }
    }
}
