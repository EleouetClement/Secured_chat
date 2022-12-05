/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace opsie_chat_project
{
	/// <summary>
	/// Structure containing all the informations about the RSA keys
	/// </summary>
	public class RSAkeys<T>
	{
		T e;
		T d;
		T n;		
				
		/// <summary>
		/// Return the public key as a Tuple, first value is e and the second is n
		/// </summary>
		public Tuple<T, T>PublicKey
		{
			get
			{
				return new Tuple<T, T>(e, n);
			}
		}
		
		/// <summary>
		/// Return the private key as a Tuple, first value is d and the second is n
		/// </summary>
		public Tuple<T, T>PrivateKey
		{
			get
			{
				return new Tuple<T, T>(d, n);
			}
		}
		
		
	}
}
