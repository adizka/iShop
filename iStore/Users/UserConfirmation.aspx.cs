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
                errMsg.InnerHtml = "No such user.";
                return;
            }

            var user = ubl.GetUserByLoginOrEmail(email);
            if (user == null)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "No such user.";
                return;
            }
            else if (user.ConfirmationID == null)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "You have already registered.";
                return;
            }
            else if (string.IsNullOrWhiteSpace(confirmationID) || user.ConfirmationID.ToString().ToUpper() != Request.QueryString["id"].ToUpper())
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Your link is disabled.";
                return;
            }

            ubl.ActivateUser(user.Email, user.ConfirmationID.Value);

            errMsg.Visible = true;
            errMsg.InnerHtml = "Your account has been successfully activated.";
            BL.Modules.Mail.Mail.Registration(user);
        }
    }
}