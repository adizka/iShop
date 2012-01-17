using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace iStore.Admin.Design
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogOut(object sender, EventArgs e)
        {
            iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();
            if (ubl.CurrentUser != null)
            {
                ubl.LogOut(ubl.CurrentUser.UserID);
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}