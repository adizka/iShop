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
            string m_subject = "Регистрация на сайте marvelworldwide.com";

            StringBuilder sb = new StringBuilder();

            string m_link = BL.SiteProperties.SiteUrl + "Users/UserConfirmation.aspx?email=" + user.Email + "&id=" + user.ConfirmationID.ToString();

            sb.Append("<p><h3>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</h3></p><br /><p><h4>Вы заплонили регистрационную форму на сайте <a href='");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a>.</h4></p><br /><br /><p>Для подтверждения регистрации, пройдите по следующей ссылке: <a href='");
            sb.Append(m_link);
            sb.Append("'>");
            sb.Append(m_link);
            sb.Append("</a></p><br /><br /><p>C уважением, администрация ");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</p>");
            string m_from = "info@marvel.com";

            MailHelper.SendMailMessage(m_from, user.Email, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void RestorePassword(BL.User user)
        {
            string m_subject = "Востановление пароля на сайте" + SiteProperties.SiteName;

            StringBuilder sb = new StringBuilder();

            string m_link = BL.SiteProperties.SiteUrl + "Users/RestorePassword.aspx?email=" + user.Email + "&id=" + user.ConfirmationID.ToString();
            sb.Append("<p><h3>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</h3></p><br /><p><h4>Востановление пароля на сайте <a href='");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a>.</h4></p><br /><br /><p>Для востановления пароля, пройдите по следующей ссылке: <a href='");
            sb.Append(m_link);
            sb.Append("'>");
            sb.Append(m_link);
            sb.Append("</a></p><br /><br /><p>C уважением, администрация ");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</p>");
            string m_from = "info@marvel.com";

            MailHelper.SendMailMessage(m_from, user.Email, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void OrderAccepted(BL.User user)
        {
            string m_subject = "Принятие заказа на сайте" + SiteProperties.SiteName;

            StringBuilder sb = new StringBuilder();

            sb.Append("<p><h3>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</h3></p><br /><p><h4>Ваш заказ принят на сайте <a href='");
            sb.Append(SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a>.</h4></p><br /><br /><p>Для просмотра вашего заказа пройдете по ссылке: <a href='");
            sb.Append(BL.SiteProperties.SiteUrl);
            sb.Append("'>");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</a></p><br /><br /><p>C уважением, администрация ");
            sb.Append(SiteProperties.SiteName);
            sb.Append("</p>");
            string m_from = "info@marvel.com";

            MailHelper.SendMailMessage(m_from, user.Email, string.Empty, string.Empty, m_subject, sb.ToString());
        }

        public static void SendFeedBack(string email, string userName, string body)
        {
            string m_subject = "Feed back from " + userName;

            StringBuilder sb = new StringBuilder();

            sb.Append("<p>Contact address");
            sb.Append(email);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(body);
            sb.Append("</p>");
            string m_from = "info@marvel.com";

            MailHelper.SendMailMessage(m_from, m_from, string.Empty, string.Empty, m_subject, sb.ToString());
        }
    }
}