using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace iStore.Admin.Login
{
    public partial class Default : System.Web.UI.Page
    {
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ubl.CurrentUserIdAdministrator)
            {
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "Admin/";
                HttpContext.Current.Response.Redirect(url);
                string message = Request.QueryString["message"];
                if (message != null)
                {
                    veLogin.Visible = true;
                    veLogin.ClearErrors();
                    veLogin.Errors = "Your Login and URL's for Password Recovery  were sent to your email address";
                }
            }
        }

        protected void SignIn(object sender, EventArgs e)
        {
            string login = HttpContext.Current.Server.HtmlEncode(txtLogin.Text);
            string password = HttpContext.Current.Server.HtmlEncode(txrPassword.Text);
            bool saveMe = cbSaveMe.Checked;
            if (ubl.AuthorizationAdmin(login, password, saveMe))
            {
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "Admin/";
                HttpContext.Current.Response.Redirect(iStore.Site.SiteAdminUrl + "Products/");
            }
            else
            {
                veLogin.Visible = true;
                veLogin.ClearErrors();
                veLogin.Errors = "Wrong login or password";
                veLogin.SetErrors();
            }
            
        }

        protected void ForgotPassword(object sender, EventArgs e)
        {
            string login = HttpContext.Current.Server.HtmlEncode(txtLoginOrEmail.Text);
            BL.Modules.Users.Users users = new BL.Modules.Users.Users();
            if (users.UserForgottenPassword(login))
            {
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "Admin/Login/?message=0";
                HttpContext.Current.Response.Redirect(url);
            }
            else
            {
                veForgotPasswords.Visible = true;
                veForgotPasswords.ClearErrors();
                veForgotPasswords.Errors = "User with this login does not exist";
                veForgotPasswords.SetErrors();
            }
        }
    }
}