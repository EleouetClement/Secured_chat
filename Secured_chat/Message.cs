/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Secured_chat;
using System;
using Encryption;
using System.Text;

namespace Secured_chat
{
	/// <summary>
	/// Description of Message.
	/// </summary>
	public class Message : IEnpcryptable<int>
	{
		string _data;
		string _encrypted;
		bool _seen = false;
		public Message(string data)
		{
			this._data = data;
			_encrypted = data;
		}

		/// <summary>
		/// decrypt a list of characters and creates the message
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="NotImplementedException"></exception>
        public void Decrypt(RSAKey<int> key)
        {
            string [] encryptedTab = _encrypted.Split('|');
			string tmp = string.Empty;
			Tuple<int, int> priv = key.PrivateKey;
			foreach(string s in encryptedTab)
            {
				if(s!=null && s!= "")
					tmp += Math.Pow(double.Parse(s), priv.Item1) % priv.Item2;
            }
			_data = tmp;
		}

		/// <summary>
		/// Created a string with all caracters encrypted and separated by ","
		/// </summary>
		/// <param name="key"></param>
        public void Encrypt(RSAKey<int> key)
        {
			Tuple<int, int> pub = key.PublicKey;
			_encrypted=string.Empty;
            foreach(char c in _data)
            {
				_encrypted+= ((int)(Math.Pow(CharValue(c), pub.Item1) % pub.Item2)).ToString();
				if(c != _data[_data.Length - 1])
					_encrypted += "|";
            }
        }

		public string Data
        {
			get { return _data; }
        }

		public string Encrypted
        {
			get { return _encrypted; }
        }

		public bool Expired
        {
			get { return _seen; }
        }

		public void MessageSeen()
        {
			_seen = true;
        }

		private double CharValue(char c)
        {
			return (double)c;
        }
    }
}
