using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace iStore.Contacts
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public const string MatchEmailPattern =
    @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


        protected void Send(object obj, EventArgs args)
        {

            var userName = HttpUtility.HtmlEncode(userNameTxt.Text);
            var email = HttpUtility.HtmlEncode(emailTxt.Text);
            var body = HttpUtility.HtmlEncode(msgTxt.Text);

            if (body.Length == 0)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Сообщение не может быть пустым.";
                return;
            }

            if (body.Length >500)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Сообщение не может превышать 500 символов.";
                return;
            }

            if (userName.Length == 0)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Неуказанно имя пользователя.";
                return;
            }

            if (userName.Length > 75)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Имя пользователя не может превышать 75 символов.";
                return;
            }
            if (!Regex.IsMatch(email, MatchEmailPattern))
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Введите корректный e-mail.";
                return;
            }

            BL.Modules.Mail.Mail.SendFeedBack(email, userName, body);

            errMsg.Visible = false;
        }
    }
}