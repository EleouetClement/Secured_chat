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
	public class RSASmallKey : RSAKey, RSATools<int>
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
        public override void Load(string [] data)
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

        public override void Save()
        {
            throw new NotImplementedException();
        }

		public void CreateKeys(int p, int q, int e)
		{
			p = FindPrime(p);
			q = FindPrime(q);
			n = p * q;
			int phi_n = Phi(p, q);
			e = Exposant(e, phi_n);
			d = ExtendedEuclide(e, phi_n);
		}


		public int ExtendedEuclide(int e, int phi)
        {
			bool reverse = false;
			if(phi > e)
            {
				int tmp = e;
				e = phi;
				phi = tmp;
				reverse = true;
            }
			int div = (e / phi);
			int mod = e % phi;
			int u0 = 1, v1 = 1;
			int u1 = 0, v0 = 0;
			int u2 = u0 + (-div * u1);
			int v2 = v0 + (-div * v1);
			while(mod > 1)
            {
				e = phi;
				phi = mod;
				mod = e % phi;
				div = (e / phi);
				u0 = u1;
				u1 = u2;
				v0 = v1;
				v1 = v2;
				u2 = u0 + (-div * u1);
				v2 = v0 + (-div * v1);
			}
			if (reverse)
				return v2;
			return u2;

        }
		

        public int FindPrime(int number)
        {
            while(!IsPrime(number))
            {
				number++;
            }
			return number;
        }

        public bool IsPrime(int number)
        {
			if (number < 2 || (number % 2 == 0 && number != 2))
			{
				return false;
			}
			for (int i = 3; i < (int)Math.Sqrt(number) + 1; i += 2)
			{
				if (number % i == 0)
					return false;
			}
			return true;
		}

        public int Phi(int p, int q)
        {
			return (p - 1) * (q - 1);
        }

        public int Pgcd(int a, int b)
        {
			int rest = 1;
			int oldRest = 0;
			if(a < b)
            {
				int tmp = a;
				a = b;
				b = a;
            }
			rest = b;
			while(rest != 0)
            {
				oldRest = rest;
				rest = a % b;
				a = b;
				b = rest;
            }
			return oldRest;
        }

        public int Exposant(int e, int phi_n)
        {
			while(Pgcd(e, phi_n) != 1)
            {
				e++;
            }
			return e;
        }
    }
}
