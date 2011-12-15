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
                lblLoginError.Text += "Логин должен быть длиной не менее 4х симоволов <br />";
                lblLoginError.Visible = true;
            }
            if (login.Length > 20) 
            {
                allRight = false;
                lblLoginError.Text += "Логин должен быть длиной не более 20 симоволов <br />";
                lblLoginError.Visible = true;
            }
            if (ubl.isLoginInDB(login))
            {
                allRight = false;
                lblLoginError.Text += "Логин занят. Выберите другой логин <br />";
                lblLoginError.Visible = true;
            }
            
            if (!IsEmail(email))
            {
                allRight = false;
                lblEmailError.Text += "Введите валидный email-адрес <br />";
                lblEmailError.Visible = true;
            }
            if (ubl.isEmailInDB(email))
            {
                allRight = false;
                lblEmailError.Text += "Email занят. Выберите другой Email <br />";
                lblEmailError.Visible = true;
            }
            if (password.Length < 4)
            {
                allRight = false;
                lblPasswordError.Text += "Пароль должен быть не менее 4х симоволов <br />";
                lblPasswordError.Visible = true;
            }
            if (password.Length > 20)
            {
                allRight = false;
                lblPasswordError.Text += "Пароль должен быть не более 20 симоволов <br />";
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