/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Secured_chat
{
	/// <summary>
	/// Structure containing all the informations about the RSA keys
	/// </summary>
	public class RSASmallKey : RSAKey
	{
		int e;
		int d;
		int n;		
				
		/// <summary>
		/// Return the public key as a Tuple, first value is e and the second is n
		/// </summary>
		public Tuple<int, int>PublicKey
		{
			get
			{
				return new Tuple<int, int>(e, n);
			}
		}
		
		/// <summary>
		/// Return the private key as a Tuple, first value is d and the second is n
		/// </summary>
		public Tuple<int, int> PrivateKey
		{
			get
			{
				return new Tuple<int, int>(d, n);
			}
		}

		/// <summary>
		/// Reads all 3 lines from the data array and creates the 3 components of the key
		/// </summary>
		/// <param name="data"></param>
        public override void Parse(string [] data)
        {
			e = d = n = 0;
            if(!int.TryParse(data[0], out n))
            {
				throw new Exception("Impossible de parser la valeur de l'exposant n");
            }

			if (!int.TryParse(data[0], out e))
			{
				throw new Exception("Impossible de parser la valeur de l'exposant e");
			}

			if (!int.TryParse(data[0], out d))
			{
				throw new Exception("Impossible de parser la valeur de la clé privé d");
			}
		}
    }
}
