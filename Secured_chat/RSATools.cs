using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secured_chat
{
    public interface RSATools<T>
    {

        /// <summary>
        /// Returns true if number is a prime number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        bool IsPrime(T number);

        /// <summary>
        /// Generate the private and public keys with 3 entries
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="e"></param>
        void CreateKeys(T p, T q, T e);

        /// <summary>
        /// Increase number by one until it becomes a prime number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        T FindPrime(T number);

        /// <summary>
        /// Implementation of the euclide algorithm to find the private key factor d
        /// </summary>
        /// <param name="e"></param>
        /// <param name="phi"></param>
        /// <returns></returns>
        T ExtendedEuclide(T e, T phi);

        /// <summary>
        /// Returns phi = (p-1)*(q-1)
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        T Phi(T p, T q);

        /// <summary>
        /// Returns the Pgcd of a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        T Pgcd(T a, T b);

        /// <summary>
        /// Increase e until pgcd(e, phi_n) == 1
        /// </summary>
        /// <param name="e"></param>
        /// <param name="phi_n"></param>
        /// <returns></returns>
        T Exposant(T e, T phi_n);
    }
}
