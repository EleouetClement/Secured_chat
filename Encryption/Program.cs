using System;
using System.Numerics;

namespace Encryption
{
    class Program
    {
        /// <summary>
        /// Test programm for the Encryption library
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Test des outils de chiffrement:");
            RSASmallKey key = new RSASmallKey();
            RSASmallKey.TestPlan(key);
            Console.WriteLine("Fin des tests, pressez une touche pour continuer");
            Console.ReadLine();
        }
    }
}
