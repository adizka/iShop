using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Users
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = Request.QueryString["error"];
            if (!string.IsNullOrEmpty(error))
            {
                if (error == "0")
                {
                    divError.InnerHtml = "Invalid login-password pair";
                }
            }
        }
        
        protected void Log_in(object sender, EventArgs e)
        {
            
        }
    }
}