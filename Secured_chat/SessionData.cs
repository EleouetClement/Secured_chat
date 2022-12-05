using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secured_chat
{
    internal class SessionData
    {
        RSa
        static SessionData instance;
        public SessionData GetInstance()
        {
            if (instance == null)
            {
                instance = new SessionData();
            }
            return instance;
        }


    }
}
