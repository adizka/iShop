using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace BL.Modules.Users
{
    public class UserRights
    {
        public int Allrights { get { return 1; } }
        public int ReadAndWrite { get { return 2; } }
        public int Read { get { return 3; } }
    }
}
