/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace opsie_chat_project
{
	/// <summary>
	/// Description of Message.
	/// </summary>
	public class Message : IEnpcryptable<string, int>
	{
		string data;
		string encrypted;
		public Message(string data)
		{
			this.data = data;
		}

		#region IEnpcryptable implementation

		public void Encrypt(string m, RSAkeys<int> key)
		{
			throw new NotImplementedException();
		}

		public void Decrypt(string m, RSAkeys<int> key)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
