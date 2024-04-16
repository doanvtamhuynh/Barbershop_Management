using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barbershopmanagement.Helpers
{
    public class Authorization
    {
        public bool isAdmin(string quyen)
        {
            if (quyen == "admin")
            {
                return true;
            }
            return false;
        }
    }
}