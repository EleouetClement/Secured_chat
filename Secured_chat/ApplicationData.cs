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

namespace opsie_chat_project
{
	/// <summary>
	/// Singleton containing the static data needed to encrypte messages
	/// </summary>
	public class ApplicationData<T>
	{
		
		RSAkeys<T> keys;
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
		
		public void GetKeys(string publickeyFile, string privateKeyFile)
		{
			using(StreamReader rd = new StreamReader("publicKeyFile"))
			{
					
			}
		}
	}
}
