using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;



namespace BL.Modules.News
{
    public class News
    {
        public void AddOrUpdateNews(int id, string title, string desc, string body)
        {
            if (id == -1)
            {
                AddNews(title, desc, body);
            }
            else
            {
                UpdateNews(id, title, desc, body);
            }
        }

        private void AddNews(string title, string desc, string body)
        {
            using (var db = new ShopDataContext())
            {
                BL.News news = new BL.News();
                using (var ts = new TransactionScope())
                {
                    news.NewsTitle = title;
                    news.NewsDescription = desc;
                    news.NewsBody = body;
                    news.CreateDate = DateTime.Now;
                    news.IsActive = false;
                    db.News.InsertOnSubmit(news);
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        private void UpdateNews(int id,string title,string desc,string body)
        {
            using (var db = new ShopDataContext())
            {
                BL.News news = db.News.Where(n => n.NewsID == id).FirstOrDefault();
                if (news != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        news.NewsBody = body;
                        news.NewsDescription = desc;
                        news.NewsTitle = title;
                        db.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
        }

        public static BL.News GetNewsById(int newsId)
        {
            ShopDataContext db = new ShopDataContext();
            return db.News.Where(n => n.NewsID == newsId).FirstOrDefault();
        }

        public void DeleteNewsById(int newsId)
        {
            using (var db = new ShopDataContext())
            {
                BL.News news = db.News.Where(n => n.NewsID == newsId).FirstOrDefault();
                if (news != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        db.News.DeleteOnSubmit(news);
                        db.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
        }

        public static IQueryable<BL.News> GetAllnews()
        {
            ShopDataContext db = new ShopDataContext();
            return db.News.OrderByDescending(n => n.CreateDate);
        }

        public void AcivateNewsById(int newsid)
        {
            using (var db = new ShopDataContext())
            {
                BL.News news = db.News.Where(n => n.NewsID == newsid).FirstOrDefault();
                if (news != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        news.IsActive = true;
                        db.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
        }

        public void DeAcivateNewsById(int newsid)
        {
            using (var db = new ShopDataContext())
            {
                BL.News news = db.News.Where(n => n.NewsID == newsid).FirstOrDefault();
                if (news != null)
                {
                    using (var ts = new TransactionScope())
                    {
                        news.IsActive = false;
                        db.SubmitChanges();
                        ts.Complete();
                    }
                }
            }
        }
    }
}
