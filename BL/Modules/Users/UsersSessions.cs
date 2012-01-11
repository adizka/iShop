using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;



namespace BL.Modules.Users
{
    public class UsersSessions
    {
        ShopDataContext db = new ShopDataContext();

        public BL.UserSession GetUserSessionByUserId(Guid userId)
        {
            return db.UsersSessions.Where(us => us.UserID == userId).FirstOrDefault();
        }

        public BL.UserSession GetUserSessionBySessionID(Guid sessionId)
        {
            BL.UserSession us = db.UsersSessions.Where(uss => uss.SessionID == sessionId).FirstOrDefault();
            return us;
        }

        public void DeleteSession(Guid userId)
        {
            BL.UserSession userSession = db.UsersSessions.Where(us => us.UserID == userId).FirstOrDefault();
            if (userSession != null)
            {
                using (var ts = new TransactionScope())
                {
                    db.UsersSessions.DeleteOnSubmit(userSession);
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        public void AddSession(Guid sessionId, Guid userId)
        {
            BL.UserSession newSession = new BL.UserSession();
            using (var ts = new TransactionScope())
            {
                newSession.SessionID = sessionId;
                newSession.UserID = userId;

                db.UsersSessions.InsertOnSubmit(newSession);

                db.SubmitChanges();
                ts.Complete();
            }
        }
    }
}
