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

            if (body.Length < 3)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Message must be at least 4 characters.";
                return;
            }

            if (body.Length > 500)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Message  must be no longer than 500 characters";
                return;
            }

            if (userName.Length == 4)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "UserName must be at least 4 characters";
                return;
            }

            if (userName.Length > 75)
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "UserName  must be no longer than 75 characters";
                return;
            }
            if (!Regex.IsMatch(email, MatchEmailPattern))
            {
                errMsg.Visible = true;
                errMsg.InnerHtml = "Enter a valid email-address";
                return;
            }

            BL.Modules.Mail.Mail.SendFeedBack(email, userName, body);
            //BL.Modules.Mail.Mail.SendThanksFeedBack(email, userName);
            errMsg.Visible = false;
        }
    }
}