using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    /// <summary>
    /// Define an RSA key
    /// </summary>
    public abstract class RSAKey<T>
    {
        protected T _e;
        protected T _d;
        protected T _n;

		/// <summary>
		/// Return the public key as a Tuple, first value is e and the second is n
		/// </summary>
		public Tuple<T, T> PublicKey
		{
			get
			{
				return new Tuple<T, T>(_e, _n);
			}
		}

		/// <summary>
		/// Return the private key as a Tuple, first value is d and the second is n
		/// </summary>
		public Tuple<T, T> PrivateKey
		{
			get
			{
				return new Tuple<T, T>(_d, _n);
			}
		}


        public virtual void SetPublicKey(T e, T n)
        {
            this._e = e;
            this._n = n;
        }

		/// <summary>
		/// Parse the key from a file
		/// </summary>
		/// <param name="data"></param>
		public abstract void Load(string [] data);

        /// <summary>
        /// Save key data into a file
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Returns true if number is a prime number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected abstract bool IsPrime(T number);

        /// <summary>
        /// Generate the private and public keys with 3 entries
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="e"></param>
        public abstract void CreateKeys(T p, T q, T e);

        /// <summary>
        /// Increase number by one until it becomes a prime number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected abstract T FindPrime(T number);

        /// <summary>
        /// Implementation of the euclide algorithm to find the private key factor d
        /// </summary>
        /// <param name="e"></param>
        /// <param name="phi"></param>
        /// <returns></returns>
        protected abstract T ExtendedEuclide(T e, T phi);

        /// <summary>
        /// Returns phi = (p-1)*(q-1)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        protected abstract T Phi(T p, T q);

        /// <summary>
        /// Returns the Pgcd of a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected abstract T Pgcd(T a, T b);

        /// <summary>
        /// Increase e until pgcd(e, phi_n) == 1
        /// </summary>
        /// <param name="e"></param>
        /// <param name="phi_n"></param>
        /// <returns></returns>
        protected abstract T Exposant(T e, T phi_n);
    }
}
