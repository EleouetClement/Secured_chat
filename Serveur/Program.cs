using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur
{
    internal class Program
    {
        /// <summary>
        /// Serveur initialization and listening startup
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Service chatUserManager = new Service(5);
            chatUserManager.InitializeServer();
            chatUserManager.StartListening();
            Console.WriteLine("Arret du service, Pressez une touche pour continuer...");
            Console.ReadLine();
        }
    }
}
