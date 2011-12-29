using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Users
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (auth.CurrentUser != null)
            {
                Response.Redirect(iStore.Site.SiteUrl);
            }
        }

        protected void Go(object sender, EventArgs e)
        {
            string login = Server.HtmlEncode(txtEmail.Text);
            var ubl = new BL.Modules.Users.Users();
            if (ubl.isLoginAndMailNotInDb(login))
            {
                lblEmailError.Text = "Your password has been sent but please ensure you check your SPAM filters if it doesn't arrive.";
                //Как дадут данные, будем отправлять письмо
            }
            else
            {
                lblEmailError.Text = "Login or Password are wrong";
            }
        }
    }
}