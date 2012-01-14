using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace iStore.Users
{
    public partial class Profile : System.Web.UI.Page
    {
        public iStore.Modules.Logic.Auth.Users auth = new iStore.Modules.Logic.Auth.Users();

        public BL.Modules.Users.Users ubl = new BL.Modules.Users.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (auth.CurrentUser == null)
            {
                Response.Redirect(iStore.Site.SiteUrl);
            }
        }

        protected void ChangePassword(object sender, EventArgs e)
        {
            //lblNewPasswordError
            string password = Server.HtmlEncode(txtNewPassword.Text);
            bool allRight = false;
            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length > 4 )
                {
                    allRight = true;
                }
            }
            if (allRight)
            {
                if (ubl.ChangePassword(auth.CurrentUser.UserID, auth.CurrentUser.Password, password))
                {
                    lblNewMail.Text = "Password has been changed";
                }
            }
            else
            {
                lblNewMail.Text = "Password must be at least 4 characters";
            }
        }

        protected void ChangeMail(object sender, EventArgs e)
        {
            //lblNewMailError
            string email = Server.HtmlEncode(txtNewMail.Text);
            if (string.IsNullOrEmpty(email))
            {
                if (IsEmail(email))
                {
                    if (ubl.isEmailInDB(email))
                    {
                        lblNewMailError.Text = "Please chouse other email";
                    }
                    else
                    {
                        if (ubl.UpdateUser(auth.CurrentUser.UserID, auth.CurrentUser.Login, email))
                        {
                            lblNewMailError.Text = "Email has been changed";
                        }
                    }
                }
                else
                {
                    lblNewMailError.Text = "Wrong Email format";
                }
            }
            else
            {
                lblNewMailError.Text = "Wrong Email format";
            }
        }


        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


        public bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }
    }
}