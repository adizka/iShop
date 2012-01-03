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
        iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (auth.CurrentUser != null)
            {
                Response.Redirect(iStore.Site.SiteUrl);
            }
            string error = Request.QueryString["error"];
            if (!string.IsNullOrEmpty(error))
            {
                if (error == "0")
                {
                    divError.InnerHtml = "Invalid login-password pair";
                }
            }
            else
            {
                divError.Visible = false;
            }
        }
        
        protected void Log_in(object sender, EventArgs e)
        {
            bool saveMe = chbSaveMe.Checked;
            string login = Server.HtmlEncode(txtLogin.Text);
            string password = Server.HtmlEncode(txtPassword.Text);
            
            if (auth.AuthorizationUser(login, password, saveMe))
            {
                Response.Redirect(iStore.Site.SiteUrl);
            }
            else
            {
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx?error=0");
            }
        }

        
    }
}