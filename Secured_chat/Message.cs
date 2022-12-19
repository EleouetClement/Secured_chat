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
			_encrypted = string.Empty;
		}

		/// <summary>
		/// TO DO...
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="NotImplementedException"></exception>
        public void Decrypt(RSAKey<int> key)
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// TO DO...
		/// </summary>
		/// <param name="key"></param>
        public void Encrypt(RSAKey<int> key)
        {
            
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
    }
}
