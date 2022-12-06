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
        public override void Load(string[] data)
        {
            throw new NotImplementedException();
        }
    }
}
