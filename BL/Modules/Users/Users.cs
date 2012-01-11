using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;



namespace BL.Modules.Users
{
    public class Users
    {
        ShopDataContext db = new ShopDataContext();

        public IQueryable<BL.User> GetAllUsers()
        {
            return db.Users;
        }

        /// <summary>
        /// Registration User, Send Mail With ConfirmI
        /// </summary>
        /// <param name="login"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="usersProperties"></param>
        public void RegisterUser(string login, string email, string password)
        {
            BL.User user = new BL.User();
            BL.Helpers.MD5CryptoServiceProvider md5 = new BL.Helpers.MD5CryptoServiceProvider();
            BL.Modules.Users.UserRoles userRoles = new BL.Modules.Users.UserRoles();
            BL.Modules.Users.UserRights userRights = new BL.Modules.Users.UserRights();
            Guid ConfirmationId = Guid.NewGuid();
            using (var ts = new TransactionScope())
            {
                user.UserID = Guid.NewGuid();
                user.Login = login;
                user.Email = email;
                user.Password = md5.getMd5Hash(password);
                user.IsActive = false;
                user.ConfirmationID = ConfirmationId;
                user.UserRoleID = userRoles.User;
                user.UserRightID = userRights.Read;
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
                ts.Complete();
            }
        }

        public bool UpdateUser(Guid userId, string login, string email)
        {
            BL.User user = db.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                using (var ts = new TransactionScope())
                {
                    user.Login = login;
                    user.Email = email;
                    db.SubmitChanges();
                    ts.Complete();
                }
                return true;
            }
            return false;
        }

        public bool UpdateUserRolesAndUserRightsByUserId(Guid userId, int userRight, int userRole)
        {
            BL.User user = db.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                using (var ts = new TransactionScope())
                {
                    user.UserRoleID = userRole;
                    user.UserRightID = userRight;
                    db.SubmitChanges();
                    ts.Complete();
                }
                return true;
            }
            return false;
        }

        public bool ActivateUser(string mail, Guid condirmationId)
        {
            BL.User user = db.Users.FirstOrDefault(u => u.Email == mail);
            if (user != null)
            {
                if (condirmationId == user.ConfirmationID)
                {
                    using (var ts = new TransactionScope())
                    {
                        user.IsActive = true;
                        user.ConfirmationID = null;
                        db.SubmitChanges();
                        ts.Complete();
                    }
                    return true;
                }
            }
            return false;
        }

        public bool isEmailInDB(string email)
        {
            BL.User user = db.Users.Where(u => u.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
            return (user != null);
        }

        public bool isLoginInDB(string login)
        {
            BL.User user = db.Users.Where(u => u.Login.ToUpper() == login.ToUpper()).FirstOrDefault();
            return (user != null);
        }

        public bool isLoginAndMailNotInDb(string login)
        {
            login = login.ToUpper();
            BL.User user =
                db.Users.Where(u => u.Login.ToUpper() == login || u.Email.ToUpper() == login).FirstOrDefault();
            return (user != null);
        }

        public bool ChangePassword(Guid userId, string oldPassword, string password)
        {
            BL.User user = db.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                BL.Helpers.MD5CryptoServiceProvider md5 = new BL.Helpers.MD5CryptoServiceProvider();
                if (md5.getMd5Hash(oldPassword) == user.Password)
                {
                    using (var ts = new TransactionScope())
                    {
                        user.Password = md5.getMd5Hash(password);
                        db.SubmitChanges();
                        ts.Complete();
                    }
                    return true;
                }
            }
            return false;
        }

        public BL.User GetUserById(Guid userId)
        {
            return db.Users.Where(u => u.UserID == userId).FirstOrDefault();
        }

        public bool UserForgottenPassword(string login)
        {
            BL.User user = db.Users.Where(u => u.Login == login || u.Email == login).FirstOrDefault();
            if (user != null)
            {
                //SendMail(user.Email, user.ConfirmationID);
                return true;
            }
            return false;
        }

        public BL.User ConfirmUser(string email, Guid confirmationId)
        {
            BL.User user = db.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                if (user.ConfirmationID == confirmationId)
                {
                    return user;
                }
            }
            return null;
        }

        public bool SetNewPasswordByUserId(Guid userId, string password)
        {
            BL.User user = GetUserById(userId);
            if (user != null)
            {
                if (user.IsActive)
                {
                    BL.Helpers.MD5CryptoServiceProvider md5 = new BL.Helpers.MD5CryptoServiceProvider();
                    using (var ts = new TransactionScope())
                    {
                        user.Password = md5.getMd5Hash(password);
                        user.ConfirmationID = null;
                        db.SubmitChanges();
                        ts.Complete();
                    }
                    return true;
                }
            }
            return false;
        }

        public bool CheckPasswordByLogin(string login, string password)
        {
            BL.Helpers.MD5CryptoServiceProvider md5 = new Helpers.MD5CryptoServiceProvider();
            BL.User user = db.Users.Where(u => (u.Password == md5.getMd5Hash(password)) && (u.Email.ToUpper() == login.ToUpper() || u.Login.ToUpper() == login.ToUpper()) && u.IsActive).FirstOrDefault();
            return (user == null);
        }

        public bool CheckAdminPasswordByLogin(string login, string password)
        {
            BL.Modules.Users.UserRoles ubl = new BL.Modules.Users.UserRoles();
            BL.Helpers.MD5CryptoServiceProvider md5 = new Helpers.MD5CryptoServiceProvider();
            BL.User user = db.Users.Where(u => (u.Password == md5.getMd5Hash(password)) && (u.Email == login || u.Login == login) && u.IsActive && u.UserRoleID == ubl.Admin).FirstOrDefault();
            return (user == null);
        }

        public BL.User GetUserByLoginOrEmail(string login)
        {
            return db.Users.FirstOrDefault(u => u.Login == login || u.Email == login);
        }

        public User SetConfirmationID(Guid userID)
        {
            var user = db.Users.FirstOrDefault(u => u.UserID == userID);
            if (user == null)
                return user;

            user.ConfirmationID = Guid.NewGuid();
            db.SubmitChanges();
            return user;
        }
    }
}
