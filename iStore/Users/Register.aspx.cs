using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace iStore.Users
{
    public partial class Register : System.Web.UI.Page
    {
        iStore.Modules.Logic.Auth.Users ul = new iStore.Modules.Logic.Auth.Users();
        BL.Modules.Users.Users ubl = new BL.Modules.Users.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ul.CurrentUser != null)
            {
                Response.Redirect(iStore.Site.SiteUrl);
            }
        }

        protected void RegistrationUser(object sender, EventArgs e)
        {

            string login = Server.HtmlEncode(txtLogin.Text);
            string email = Server.HtmlEncode(txtEmail.Text);
            string password = Server.HtmlEncode(txtPassword.Text);
            bool allRight = CheckAll();
            if (allRight)
            {
                //Зарегестрировать пользователя
                divRegistrationTitle.Visible = false;
                divRegistration.Visible = false;
                divAfterRegistrationTitle.Visible = true;
                divAfterRegistration.Visible = true;
                ubl.RegisterUser(login, email, password);
            }
            return;
        }

        private bool CheckAll()
        {
            bool allRight = true;

            lblLoginError.Text = string.Empty;
            lblEmailError.Text = string.Empty;
            lblPasswordError.Text = string.Empty;
            
            string login = Server.HtmlEncode(txtLogin.Text);
            string email = Server.HtmlEncode(txtEmail.Text);
            string password = Server.HtmlEncode(txtPassword.Text);

            if (login.Length < 4) 
            {
                allRight = false;
                lblLoginError.Text += "Usernames (Login) must be at least 4 characters ";
                lblLoginError.Visible = true;
            }
            if (login.Length > 20) 
            {
                allRight = false;
                lblLoginError.Text += "Username (Login)  must be no longer than 20 characters";
                lblLoginError.Visible = true;
            }
            if (ubl.isLoginInDB(login))
            {
                allRight = false;
                lblLoginError.Text += "Username (Login) occupied. Choose another";
                lblLoginError.Visible = true;
            }
            
            if (!IsEmail(email))
            {
                allRight = false;
                lblEmailError.Text += "Enter a valid email-address";
                lblEmailError.Visible = true;
            }
            if (ubl.isEmailInDB(email))
            {
                allRight = false;
                lblEmailError.Text += "Email occupied. Choose another (email) ";
                lblEmailError.Visible = true;
            }
            if (password.Length < 4)
            {
                allRight = false;
                lblPasswordError.Text += "Your password must be at least four characters";
                lblPasswordError.Visible = true;
            }
            if (password.Length > 20)
            {
                allRight = false;
                lblPasswordError.Text += "Your password must be no more than 20 characters";
                lblPasswordError.Visible = true;
            }
            return allRight;
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