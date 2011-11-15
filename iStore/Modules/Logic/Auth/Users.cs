using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace iStore.Modules.Logic.Auth
{
    public class Users
    {
        #region Authorization
        /// <summary>
        /// Authorization
        /// </summary>

        BL.Modules.Users.Users ubl = new BL.Modules.Users.Users();
        BL.Modules.Users.UsersSessions us = new BL.Modules.Users.UsersSessions();

        internal bool AuthorizationUser(string login, string password, bool SaveMe)
        {
            
            if (ubl.CheckPasswordByLogin(login, password))
            {
                //Не прошёл авторизацию
                return false;
            }
            else
            {
                //Прошёл авторизацию
                LoginUser(login, SaveMe);
                return true;
            }
        }

        internal bool AuthorizationAdmin(string login, string password, bool SaveMe)
        {
            if (ubl.CheckAdminPasswordByLogin(login, password))
            {
                //Не прошёл авторизацию
                return false;
            }
            else
            {
                //Прошёл авторизацию
                LoginUser(login, SaveMe);
                return true;
            }
        }

        #endregion

        #region Authentification
        /// <summary>
        /// Authentification
        /// </summary>

        public void LogOut(Guid userId)
        {
            // Извлекаю cookie-набор 
            HttpCookie cookie = HttpContext.Current.Request.Cookies["MySession"];
            // Проверяю, удалось ли обнаружить cookie-набор с таким именем.
            cookie.Expires = DateTime.Now.AddDays(-1);
            DeleteSession(userId);
        }

        private void DeleteSession(Guid userId)
        {
            us.DeleteSession(userId);
        }

        private void LoginUser(string login, bool SaveMe)
        {
            BL.User user = ubl.GetUserByLoginOrEmail(login);
            if (user != null)
            {

                BL.UserSession userSession = us.GetUserSessionByUserId(user.UserID);
                if (userSession != null)
                {
                    UpdateSession(user.UserID, SaveMe);
                }
                else
                {
                    AddNewSession(user.UserID, SaveMe);
                }
            }
        }

        private void UpdateSession(Guid userId, bool SaveMe)
        {
            DeleteSession(userId);
            AddNewSession(userId, SaveMe);
        }

        

        
        private void AddNewSession(Guid userId, bool SaveMe)
        {
            //Создаём объект cookie-набора
            HttpCookie cookie = new HttpCookie("MySession");

            //Устанавливаем в нём значение SessionId (dInoisseS = SessionID)
            System.Guid sessionId = System.Guid.NewGuid();
            cookie["dInoisseS"] = sessionId.ToString();

            if (SaveMe)
            {
                //Этот cookie-набор будет оставаться действительным в течении года
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            // Добавляем его в текущий ответ
            HttpContext.Current.Response.Cookies.Add(cookie);

            // Сохраняем в базу сессию
            AddSession(sessionId, userId);
        }

        private void AddSession(Guid sessionId, Guid userId)
        {
            
            us.AddSession(sessionId, userId);
        }

        public bool CurrentUserIdAdministrator
        {
            get
            {
                BL.User user = CurrentUser;
                if (user == null) return false;
                BL.Modules.Users.UserRoles urbl = new BL.Modules.Users.UserRoles();
                return (user.UserRoleID == urbl.Admin);
            }
        }

        public  BL.User CurrentUser
        {
            get
            {
                BL.UserSession userSession = GetSession();
                if (userSession != null)
                { 
                    Guid userId = userSession.UserID;
                    return ubl.GetUserById(userId);
                }
                return null;
            }
        }

        private BL.UserSession GetSession()
        {
            // Извлекаю cookie-набор 
            HttpCookie cookie = HttpContext.Current.Request.Cookies["MySession"];

            // Проверяю, удалось ли обнаружить cookie-набор с таким именем.
            string session;
            if (cookie != null)
            {
                session = cookie["dInoisseS"];
                System.Guid sessionId = new Guid(session);
                BL.UserSession MySession = us.GetUserSessionBySessionID(sessionId);
                if (MySession != null)
                {
                    return MySession;
                }
                return null;
            }
            else return null;
        }

        #endregion

    }
}