using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secured_chat
{
    /// <summary>
    /// Define an RSA key
    /// </summary>
    public abstract class RSAKey
    {

        /// <summary>
        /// Parse the key from a file
        /// </summary>
        /// <param name="data"></param>
        public abstract void Load(string [] data);

        /// <summary>
        /// Save key data into a file
        /// </summary>
        public abstract void Save();
    }
}
