using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption;

namespace Serveur
{
    /// <summary>
    /// Defines a user logged in the server
    /// </summary>
    internal class User
    {
        private string name;
        private RSASmallKey publicKey;

        public User(string name, RSASmallKey publicKey)
        {
            this.name = name;
            this.publicKey = publicKey;
        }

        public string Name
        {
            get { return name; }
        }

        public RSASmallKey PublicKey
        {
            get { return publicKey; }
        }

    }
}
