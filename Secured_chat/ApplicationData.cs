/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Numerics;

namespace Secured_chat
{
	/// <summary>
	/// Singleton containing the static data needed to encrypte messages
	/// </summary>
	public class ApplicationData<T>
	{
		
		RSASmallKey key;

		public readonly string smallKeysFile = "";
		public readonly string LargeKeysFile = "";

		static ApplicationData<T> instance;
		
		ApplicationData()
		{
		}
		
		public static ApplicationData<T> GetInstance()
		{
			if(instance == null)
			{
				instance = new ApplicationData<T>();
				return instance;
			}
			return instance;
		}
		
		public void GetSmallKeys(string publickeyFile, string privateKeyFile, out RSASmallKey copy)
		{
			//Fetching public key
			if(key != null)
            {
				copy = key;
            }
			key = new RSASmallKey<T>();
			using(StreamReader rd = new StreamReader("publicKeyFile"))
			{
				string line = rd.ReadLine();

			}

			return keys;
		}
	}
}
