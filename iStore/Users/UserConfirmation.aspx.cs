using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Users
{
    public partial class UserConfirmation : System.Web.UI.Page
    {
        BL.Modules.Users.Users ubl = new BL.Modules.Users.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            var email = Request.QueryString["email"];
            var confirmationID = Request.QueryString["id"];
            if (string.IsNullOrWhiteSpace(email))
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Вы не регистрировались в нашей системе.";
                return;
            }

            var user = ubl.GetUserByLoginOrEmail(email);
            if (user == null)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Вы не регистрировались в нашей системе.";
                return;
            }
            else if (user.ConfirmationID == null)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Вы уже зарегистрированны.";
                return;
            }
            else if (string.IsNullOrWhiteSpace(confirmationID) || user.ConfirmationID.ToString().ToUpper() != Request.QueryString["id"].ToUpper())
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Ваша ссылка не активна.";
                return;
            }

            ubl.ActivateUser(user.Email, user.ConfirmationID.Value);

            errMsg.Visible = true;
            errMsg.InnerHtml = "Ваш аккаунт успешно активирован.";
            BL.Modules.Mail.Mail.Registration(user);
        }
    }
}