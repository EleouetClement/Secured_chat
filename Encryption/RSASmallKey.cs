/*
 * Created by SharpDevelop.
 * User: celeouet
 * Date: 05/12/2022
 * Time: 11:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Encryption
{
	/// <summary>
	/// Structure containing all the informations about the RSA keys
	/// </summary>
	public sealed class RSASmallKey : RSAKey<int>
	{
		
		private static readonly string keyFile = ".smallKeyFile.key";

        /// <summary>
        /// Reads all 3 lines from the data array and creates the 3 components of the key
        /// </summary>
        /// <param name="data"></param>
        public override void Load(string [] data)
        {
			e = d = n = 0;
            try
            {
				using (StreamReader rd = new StreamReader(keyFile))
				{
					d = int.Parse(rd.ReadLine());
					e = int.Parse(rd.ReadLine());
					n = int.Parse(rd.ReadLine());
				}
			}
			catch (IOException)
            {
				//File not found a couple of keys need to be created
				throw new Exception("Need to create a key");
            }

			
		}

        public override void Save()
        {
			using(StreamWriter writer = new StreamWriter(keyFile))
            {
				writer.WriteLine(BitConverter.GetBytes(d));
				writer.WriteLine(BitConverter.GetBytes(n));
				writer.WriteLine(BitConverter.GetBytes(e));
            }
        }

		public override void CreateKeys(int p, int q, int e)
		{
			p = FindPrime(p);
			q = FindPrime(q);
			n = p * q;
			int phi_n = Phi(p, q);
			e = Exposant(e, phi_n);
			d = ExtendedEuclide(e, phi_n);
		}


		protected override int ExtendedEuclide(int e, int phi)
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
		

        protected override int FindPrime(int number)
        {
            while(!IsPrime(number))
            {
				number++;
            }
			return number;
        }

		protected override bool IsPrime(int number)
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

		protected override int Phi(int p, int q)
        {
			return (p - 1) * (q - 1);
        }

		protected override int Pgcd(int a, int b)
        {
			int rest = 1;
			int oldRest = 0;
			if(a == 0 || b == 0)
				return 0;
			if(a < b)
            {
				int tmp = a;
				a = b;
				b = tmp;
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

		protected override int Exposant(int e, int phi_n)
        {
			while(Pgcd(e, phi_n) != 1)
            {
				e++;
            }
			return e;
        }

		/// <summary>
		/// Unit test for all functions of this class. A FINIR
		/// </summary>
		/// <param name="key"></param>
		public static void TestPlan(RSASmallKey key)
		{
			#region Test Pgcd
			int[] a = { 221, -98, 465, 86846, -4632, 0, 6584 };
			int[] b = { 782, 651, 423, 219879, 0, 8465, 6846848 };
			int[] expected = { 17, -7, 3, 1, 0, 0, 8 };

			Console.WriteLine("Avec des entiers :");
			Console.WriteLine("Test du pgcd : ");
			bool ok = true;
			for (int i = 0; i < a.Length; i++)
			{
				int result = key.Pgcd(a[i], b[i]);
				Console.Write("Pgcd(" + a[i] + "," + b[i] + ")=" + expected[i] + "? " + result);
				if (result != expected[i])
				{
					ok = false;
					Console.Write("...........KO");
				}
				else
				{
					Console.Write("...........OK");
				}
				Console.WriteLine("");
			}
			Console.WriteLine("Test de pgcd : " + ((ok) ? "Ok" : "Ko"));
			#endregion
			#region Test IsPrime
			Console.WriteLine("Test de primalité : ");
			a = new int[] { 0, 1, 2, 3, -8, 1181, 691, 1110, 50459, 168281 };
			bool[] prime = { false, false, true, true, false, true, true, false, true, true };
			ok = true;
			for (int i = 0; i < a.Length; i++)
			{
				bool result = key.IsPrime(a[i]);
				Console.Write("IsPrime("+a[i]+") expected "+prime[i]+ " got " + key.IsPrime(a[i]));
				if(result != prime[i])
                {
					Console.Write("...........KO");
					ok = false;
                }
				else
                {
					Console.Write("...........OK");
				}
				Console.WriteLine("");
			}
			Console.WriteLine("Test de primalité : " + ((ok) ? "Ok" : "Ko"));
			#endregion
		}
    }
}
