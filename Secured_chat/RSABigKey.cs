using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secured_chat
{
    class RSABigKey : RSAKey<BigInteger>
    {
        private static readonly string keyFile = ".BigKeyFile.key";
        public override void CreateKeys(BigInteger p, BigInteger q, BigInteger e)
        {
            throw new NotImplementedException();
        }

        public override void Load(string[] data)
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

        protected override BigInteger Exposant(BigInteger e, BigInteger phi_n)
        {
            throw new NotImplementedException();
        }

        protected override BigInteger ExtendedEuclide(BigInteger e, BigInteger phi)
        {
            throw new NotImplementedException();
        }

        protected override BigInteger FindPrime(BigInteger number)
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrime(BigInteger number)
        {
            throw new NotImplementedException();
        }

        protected override BigInteger Pgcd(BigInteger a, BigInteger b)
        {
            throw new NotImplementedException();
        }

        protected override BigInteger Phi(BigInteger p, BigInteger q)
        {
            throw new NotImplementedException();
        }
    }
}
