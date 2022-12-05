/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 10:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace opsie_chat_project
{
	/// <summary>
	/// Set of tools to encrypt and decrypt messages
	/// </summary>
	public interface IEnpcryptable<T, U>
	{
		/// <summary>
		/// Encrypt the message using RSA algorithm and small integers
		/// </summary>
		/// <param name="m">structure containing the message to encrypt</param>
		/// <param name="key">1st factor of the public key</param>
		void Encrypt(T m, RSAkeys<U> key);
		/// <summary>
		/// Decrypt the message using RSA algorithm and small integers
		/// </summary>
		/// <param name="m"></param>
		/// <param name="key"></param>
		void Decrypt(T m, RSAkeys<U> key);
	}
}
