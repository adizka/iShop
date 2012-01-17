using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Design
{
    public partial class AutorizationControl : System.Web.UI.UserControl
    {
        iStore.Modules.Logic.Auth.Users ubl = new iStore.Modules.Logic.Auth.Users();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public BL.User CurrentUser
        {
            get { return ubl.CurrentUser; }
        }

        protected void LogOut(object sender, EventArgs e)
        {
            if (CurrentUser != null)
            {
                ubl.LogOut(CurrentUser.UserID);
            }
            Response.Redirect(Request.Url.AbsolutePath);
        }

        protected void LoginUser(object sender, EventArgs e)
        {
            string login = Server.HtmlEncode(txtLogin.Text);
            string password = Server.HtmlEncode(txtPassword.Text);
            bool saveMe = chbSaveMe.Checked;
            if (ubl.AuthorizationUser(login, password, saveMe))
            {
                Response.Redirect(Request.Url.AbsolutePath);
            }
            else
            {
                Response.Redirect(iStore.Site.SiteUrl + "Users/Login.aspx?error=0");
            }
        }
    }
}       