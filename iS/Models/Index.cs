using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iS.Models
{
    public class Index
    {
        

        public IQueryable<BL.User> GetAllUsers()
        { 
            BL.Modules.Users.Users user = new BL.Modules.Users.Users();
            return null;
        }
        
    }
}