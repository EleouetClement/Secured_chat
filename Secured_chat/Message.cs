﻿/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Secured_chat;
using System;

namespace opsie_chat_project
{
	/// <summary>
	/// Description of Message.
	/// </summary>
	public class Message : IEnpcryptable
	{
		string data;
		string encrypted;
		public Message(string data)
		{
			this.data = data;
		}


        public void Decrypt(RSAKey key)
        {
            throw new NotImplementedException();
        }

        public void Encrypt(RSAKey key)
        {
            throw new NotImplementedException();
        }
    }
}
