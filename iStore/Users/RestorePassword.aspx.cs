using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Users
{
    public partial class RestorePassword : System.Web.UI.Page
    {
        BL.Modules.Users.Users ubl = new BL.Modules.Users.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            errMsg.Visible = false;
            var email = Request.QueryString["email"];
            var confirmationID = Request.QueryString["id"];
            if (string.IsNullOrWhiteSpace(email))
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Вы не регистрировались в нашей системе.";
                return;
            }

            
            if (User == null)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Вы не регистрировались в нашей системе.";
                return;
            }
            else if (User.ConfirmationID == null)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Вы уже зарегистрированны.";
                return;
            }
            else if (string.IsNullOrWhiteSpace(confirmationID) || User.ConfirmationID.ToString().ToUpper() != Request.QueryString["id"].ToUpper())
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Ваша ссылка не активна.";
                return;
            }
            NewPasswordForm.Visible = true;
        }

        BL.User _User;
        BL.User User
        {
            get 
            {
                if(_User == null)
                    _User = ubl.GetUserByLoginOrEmail(Request.QueryString["email"]);

                return _User;
            }
        }

        protected void Restore(object obj, EventArgs args)
        {
            if (passwd1.Text != passwd2.Text)
            {
                errMsg.InnerHtml = "Пароли не совпадают";
                errMsg.Visible = true;
                return;
            }
            if (passwd1.Text.Length < 4)
            {
                errMsg.InnerHtml += "Your password must be at least four characters";
                errMsg.Visible = true;
            }
            if (passwd1.Text.Length > 20)
            {
                errMsg.InnerHtml += "Your password must be no more than 20 characters";
                errMsg.Visible = true;
            }

            ubl.SetNewPasswordByUserId(User.UserID, passwd1.Text);
            errMsg.InnerHtml = "Ваш пароль успешно востановлен";
            errMsg.Visible = true;
            errMsg.Visible = false;
        }
    }
}