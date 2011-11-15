using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace iStore.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        public iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}