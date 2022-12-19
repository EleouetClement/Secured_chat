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
        bool _toShow = false;
        public Chat(string receiver)
        {
            _receiver = receiver;
            _messages = new List<Message>();
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

        public bool ToShow
        {
            get
            {
                return _toShow;
            }
        }
        
        /// <summary>
        /// Set toShow as true to notify the UsersList thread the this interface needs to be shown
        /// </summary>
        public void RequestShow()
        {
            _toShow = true;
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
                SetMessagesToDialog();
            }
            _window.Show();
            _toShow = false;

        }
        /// <summary>
        /// Add a message to the list in this discussion
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(Message message)
        {
            _messages.Add(message);
            if(WindowOn)
            {
                string display = _receiver + " : " + message.Data;
                _window.dialogBox.Lines.Append(display);
            }
        }

        public void SetBoxName(string phrase="")
        {
            if(_window != null)
            {
                if(phrase != string.Empty)
                {
                    _window.Text = phrase;
                }
                else
                {
                    _window.Text = "Discussion avec : " + _receiver;
                }
            }
        }

        /// <summary>
        /// Copy all messages into the dialogBox. The form needs to be created.
        /// </summary>
        private void SetMessagesToDialog()
        {
            if(_messages != null)
            {
                foreach(Message message in _messages)
                {
                    if(!message.Expired)
                    {
                        if(!WindowOn)
                        {
                            throw new Exception("Impossible d'ajouter les messages, la fenetre n'existe pas");
                        }
                        string display = _receiver + " : " + message.Data;
                        _window.dialogBox.Lines.Append(display);
                    }
                }
            }
        }
    }
}
