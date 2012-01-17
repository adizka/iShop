using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.Helpers;

namespace BL.Modules.Mail
{
    public class Mail
    {
        public static void Registration(BL.User user)
        {
            string m_subject = "Registration on marvelworldwide.com";

            StringBuilder sb = new StringBuilder();

            string m_link = BL.SiteProperties.SiteUrl + "Users/UserConfirmation.aspx?email=" + user.Email + "&id=" + user.ConfirmationID.ToString();

            sb.Append("<p><h3>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</h3></p><br /><p><h4>You have complited registration form on <a href='");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a>.</h4></p><br /><br /><p>To confirm you registration please follow  link <a href='");
            sb.Append(m_link);
            sb.Append("'>");
            sb.Append(m_link);
            sb.Append("</a></p><br /><br /><p>Br, Administration ");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</p>");
            string m_from = "marinan@wchsb.com";
;

            MailHelper.SendMailMessage(m_from, user.Email, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void RestorePassword(BL.User user)
        {
            string m_subject = "Password recovery on " + SiteProperties.SiteName;

            StringBuilder sb = new StringBuilder();

            string m_link = BL.SiteProperties.SiteUrl + "Users/RestorePassword.aspx?email=" + user.Email + "&id=" + user.ConfirmationID.ToString();
            sb.Append("<p><h3>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</h3></p><br /><p><h4>Password recovery on <a href='");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a>.</h4></p><br /><br /><p>To recover your password please follow link: <a href='");
            sb.Append(m_link);
            sb.Append("'>");
            sb.Append(m_link);
            sb.Append("</a></p><br /><br /><p>Br, Administration ");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</p>");
            string m_from = "marinan@wchsb.com";

            MailHelper.SendMailMessage(m_from, user.Email, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void OrderAccepted(BL.User user)
        {
            string m_subject = "Your order have been received on " + SiteProperties.SiteName;

            StringBuilder sb = new StringBuilder();

            sb.Append("<p><h3>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</h3></p><br /><p><h4>Your order have been received <a href='");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a>.</h4></p><br /><br /><p>To review your order please follow link <a href='");
            sb.Append(BL.SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a></p><br /><br /><p>Br, Administration ");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</p>");
            string m_from = "marinan@wchsb.com";

            MailHelper.SendMailMessage(m_from, user.Email, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void SendFeedBack(string email, string userName, string body)
        {
            string m_subject = "Feed back from " + userName;

            StringBuilder sb = new StringBuilder();

            sb.Append("<p>Contact address ");
            sb.Append(email);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(body);
            sb.Append("</p>");
            string m_from = "marinan@wchsb.com";

            MailHelper.SendMailMessage(m_from, m_from, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void SendThanksFeedBack(string email, string userName)
        {
            string m_subject = "Спасибо вам огромное при огромное " + userName;

            StringBuilder sb = new StringBuilder();

            sb.Append("<p>");
            sb.Append(m_subject);
            sb.Append("</p>");
            sb.Append("<p><a href=\"");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("\">");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a></p>");
            string m_from = "marinan@wchsb.com";

            MailHelper.SendMailMessage(m_from, email, string.Empty, string.Empty, m_subject, sb.ToString());
        }
    }
}