using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Transactions;

namespace BL.Modules.Pages
{
    public class Pages
    {
        public ShopDataContext db = new ShopDataContext();

        public void AddOrUpdatePage(int pageId, string keywords, string pageName, string pageBody)
        {
            if (pageId == -1)
            {
                AddPage(keywords, pageName, pageBody);
            }
            else
            {
                UpdatePage(pageId, keywords, pageName, pageBody);
            }
        }

        private void AddPage(string keywords, string pageName, string pageBody)
        {
            BL.Page page = new BL.Page();
            using (var ts = new TransactionScope())
            {
                if (string.IsNullOrEmpty(keywords))
                {
                    page.Keywords = string.Empty;
                }
                else
                {
                    page.Keywords = keywords;
                }
                if (string.IsNullOrEmpty(pageBody))
                {
                    page.PageBody = string.Empty;
                }
                else
                {
                    page.PageBody = pageBody;
                }

                page.PageName = pageName;
                db.Pages.InsertOnSubmit(page);
                db.SubmitChanges();
                ts.Complete();
            }
        }

        private void UpdatePage(int pageId, string keywords, string pageName, string pageBody)
        {
            BL.Page page = db.Pages.Where(p => p.PageID == pageId).FirstOrDefault();
            if (page != null)
            {
                using (var ts = new TransactionScope())
                {
                    if (string.IsNullOrEmpty(keywords))
                    {
                        page.Keywords = string.Empty;
                    }
                    else
                    {
                        page.Keywords = keywords;
                    }
                    if (string.IsNullOrEmpty(pageBody))
                    {
                        page.PageBody = string.Empty;
                    }
                    else
                    {
                        page.PageBody = pageBody;
                    }
                    page.PageName = pageName;
                    db.SubmitChanges();
                    ts.Complete();
                }
            }
        }

        public bool DeletePageById(int pageId) 
        {
            BL.Page page = GetPageById(pageId);
            if (page != null)
            {
                using (var ts = new TransactionScope())
                {
                    db.Pages.DeleteOnSubmit(page);
                    db.SubmitChanges();
                    ts.Complete();
                }
                return true;
            }
            else return false;
        }

        public BL.Page GetPageById(int pageId)
        {
            return db.Pages.Where(p => p.PageID == pageId).FirstOrDefault();
        }

        public IQueryable<BL.Page> GetAllPages()
        {
            return db.Pages.OrderByDescending(p => p.PageID);
        }

        public BL.Page GetPageByName(string name)
        {
            return db.Pages.Where(p => p.PageName == name).FirstOrDefault();
        }

        public bool IsPageNameInDB(string pageName)
        {
            BL.Page page = GetPageByName(pageName);
            return (page != null);
        }
    }
}
